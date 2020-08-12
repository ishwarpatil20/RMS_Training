/*! skinny.js v0.1.0 | Copyright 2013 Vistaprint | vistaprint.github.io/SkinnyJS/LICENSE 
http://vistaprint.github.io/SkinnyJS/download-builder.html?modules=jquery.modalDialogContent*/

/// <reference path="jquery.contentSize.js" ></reference>
/// <reference path="jquery.customEvent.js" ></reference>
/// <reference path="jquery.disableEvent.js" ></reference>
/// <reference path="jquery.postMessage.js" ></reference>
/// <reference path="jquery.queryString.js" ></reference>

(function($) {
    $.modalDialog = $.modalDialog || {};

    var _ua = $.modalDialog._ua = (function() {
        var ua = navigator.userAgent;

        // Internet Explorer 7 specific checks
        if (ua.indexOf("MSIE 7.0") > 0) {
            return {
                ie: true,
                ie7: true,
                version: 7,
                compat: ua.indexOf("compatible") > 0
            };
        }

        // Internet Explorer 8 specific checks
        if (ua.indexOf("MSIE 8.0") > 0) {
            return {
                ie: true,
                ie8: true,
                version: 8,
                compat: ua.indexOf("compatible") > 0
            };
        }

        return {};
    })();

    var _isSmallScreenOverride;

    $.modalDialog.setSmallScreen = function(isSmallScreen) {
        _isSmallScreenOverride = isSmallScreen;
    };

    // Returns true if we're on a small screen device like a smartphone.
    // Dialogs behave slightly different on small screens, by convention.
    $.modalDialog.isSmallScreen = function() {
        if (typeof (_isSmallScreenOverride) != "undefined") {
            return _isSmallScreenOverride;
        }

        // Detect Internet Explorer 7/8, force them to desktop mode
        if (_ua.ie7 || _ua.ie8) {
            return false;
        }

        var width = $(window).width();
        return (typeof window.orientation == "number" ? Math.min(width, $(window).height()) : width) <= 480;
    };

})(jQuery);

// Support reading settings from a node dialog's element

(function($) {
    var ATTR_PREFIX = "data-dialog-";

    var getKeys = function(obj) {

        if (Object.keys) {
            return Object.keys(obj);
        }

        var keys = [];
        for (var key in obj) {
            if (obj.hasOwnProperty(key)) {
                keys[keys.length] = key;
            }
        }
        return keys;
    };

    var parseNone = function(s) {
        if (s === "") {
            return s;
        }
        return s || null;
    };

    var parseBool = function(s) {
        if (s) {
            s = s.toString().toLowerCase();
            switch (s) {
                case "true":
                case "yes":
                case "1":
                    return true;
                default:
                    break;
            }
        }

        return false;
    };

    var parseFunction = function(body) {
        // Evil is necessary to turn inline HTML handlers into functions
        /* jshint evil: true */

        if (!body) {
            return null;
        }

        return new Function("event", body);
    };

    // The properties to copy from HTML data-dialog-* attributes
    // to the dialog settings object
    var _props = {
        "title": parseNone,
        "onopen": parseFunction,
        "onbeforeopen": parseFunction,
        "onclose": parseFunction,
        "onbeforeclose": parseFunction,
        "maxWidth": parseInt,
        "initialHeight": parseInt,
        "ajax": parseBool,
        "onajaxerror": parseFunction,
        "destroyOnClose": parseBool,
        "skin": parseNone,
        "enableHistory": parseBool,
        "zIndex": parseInt
    };

    $.modalDialog = $.modalDialog || {};

    // Copies the HTML data-dialog-* attributes to the settings object
    $.modalDialog.getSettings = function($el) {
        var settings = {};

        $.each(getKeys(_props), function(i, key) {
            // $.fn.attr is case insensitive
            var value = $el.attr(ATTR_PREFIX + key);
            if (typeof value != "undefined") {
                var parser = _props[key];
                settings[key] = parser(value);
            }
        });

        return settings;
    };

})(jQuery);

// This is a library for use in content windows that live inside a FramedDialog.
// All its methods work cross-domain.

/*
Example usage:

Creating a second level dialog:
$.modalDialog.create()

*/

(function($) {
    if ($.modalDialog && $.modalDialog._isHost) {
        throw new Error("Attempt to load jquery.modalDialogContent.js in the same window as jquery.modalDialog.js.");
    }

    var _dialogIdCounter = -1;
    var DIALOG_NAME_PREFIX = "dialog";

    // Gets a unique ID for this window.
    var getDialogId = function() {
        _dialogIdCounter++;
        return DIALOG_NAME_PREFIX + _dialogIdCounter;
    };

    // Utility class to manage multiple callbacks.
    var DialogProxyEvent = function(eventType) {
        this.eventType = eventType;
        this.callbacks = new $.Callbacks();

        for (var i = 1; i < arguments.length; i++) {
            this.callbacks.add(arguments[i]);
        }
    };

    DialogProxyEvent.prototype = {
        // Triggers the event and calls all handlers.
        fire: function(data) {
            var evt = new $.Event(this.eventType);
            $.extend(evt, data);

            this.callbacks.fire(evt);

            return evt;
        },

        // Adds a handler to the event.
        add: function(callback) {
            if (callback) {
                this.callbacks.add(callback);
            }
        }
    };

    // Utility to initialize event objects on the FramedDialogProxy.
    var initEvent = function(dialogProxy, eventType) {
        var onEventType = "on" + eventType;
        dialogProxy[onEventType] = new DialogProxyEvent(eventType, dialogProxy.settings[onEventType]);
        delete dialogProxy.settings[onEventType];
    };

    // A proxy object that has all the methods of the host window FramedDialog,
    // but uses postMessage to communicate with the host window dialog.
    var FramedDialogProxy = function(settings) {
        this.settings = settings;

        initEvent(this, "open");
        initEvent(this, "close");
        initEvent(this, "beforeclose");

        // If _window is passed, this is a proxy for the dialog in the specified window.
        if (this.settings._window) {
            // The window object should not be kept in settings, because it can't be serialized
            // and sent to the parent window via postMessage.
            this._window = settings._window;
            delete settings._window;

            // The ID of this window is stored in its name- 
            // This is the one property that can be read from any window, across domains.
            this.settings._fullId = this._window.name;

            // dialog ids are in the format grandParentId_parentId_thisId.
            var idParts = this.settings._fullId.split("_");
            idParts.pop();

            if (idParts.length > 0) {
                this.settings.parentId = idParts.join("_");
                this._parentWindow = parent.frames[this.settings.parentId];
            }
        }
        // If _parentWindow is passed, this is a proxy for a new window, child of the current window
        else if (this.settings._parentWindow) {
            this.settings.parentId = settings._parentWindow.name;
            this._parentWindow = settings._parentWindow;
            delete settings._parentWindow;

            this.settings._fullId = this.settings.parentId + "_" + getDialogId();

            this._postCommandToParent("create", this.settings);
        }
    };

    // Implements the same interface as the parent FramedDialog, 
    // but uses postMessage() to communicate with the parent.
    FramedDialogProxy.prototype = {
        dialogType: "iframe",

        open: function() {
            this._postCommandToParent("open");
        },

        close: function() {
            this._postCommandToParent("close");
        },

        getParent: function() {
            if (typeof (this._parentDialog) == "undefined") {
                this._parentDialog = null;

                if (this.settings.parentId) {
                    this._parentDialog = new FramedDialogProxy({
                        _window: parent.frames[this.settings.parentId]
                    });
                }
            }

            return this._parentDialog;
        },

        getWindow: function() {
            if (!this._window) {
                this._window = this._window || parent.frames[this.settings._fullId];
            }

            return this._window;
        },

        setHeight: function(contentHeight, center, skipAnimation) {
            if ($.modalDialog.autoSizing && !this._internalCall) {
                throw new Error("Auto sizing is enabled, so manual size setting is disallowed.");
            }

            // Ensure content height is rounded so its easy to compare to itself
            contentHeight = Math.round(contentHeight);

            // Optimization: don't set the height if its already set
            // Also prevents resizing while a resize animation is progress

            // TODO: could this be a problem when the content gets too large for the window, and the max height kicks in?
            // contentHeight might never match _currentFrameHeight.
            if (this._currentFrameHeight && this._currentFrameHeight == contentHeight) {
                return;
            }

            this._currentFrameHeight = contentHeight;

            this._postCommandToParent("setHeight", {
                height: contentHeight,
                center: !!center,
                skipAnimation: !!skipAnimation
            });
        },

        setHeightFromContent: function(center, skipAnimation) {
            var height;
            if ($.modalDialog.sizeElement) {
                // This mechanism handles body margins, other factors that affect position.
                var rect = $.modalDialog.sizeElement.clientRect();
                height = rect.top + rect.height;
            } else {
                height = $(window).contentSize().height;
            }

            this.setHeight(height, center, skipAnimation);
        },

        _setHeightFromContentInternal: function() {
            this._internalCall = true;
            this.setHeightFromContent.apply(this, arguments);
            this._internalCall = false;
        },

        center: function() {
            this._postCommandToParent("center");
        },

        setTitle: function(title, initializing) {
            this._postCommandToParent("setTitle", {
                title: title,
                initializing: !!initializing
            });
        },

        setTitleFromContent: function(initializing) {
            this.setTitle(document.title, initializing);
        },

        notifyReady: function() {
            // Initialize the size element
            if ($.modalDialog.sizeElement) {
                if (!($.modalDialog.sizeElement instanceof jQuery)) {
                    $.modalDialog.sizeElement = $($.modalDialog.sizeElement);
                }

                if ($.modalDialog.sizeElement.length === 0) {
                    $.modalDialog.sizeElement = null;
                }
            }

            // Set the dialog title
            if ($.modalDialog.useTitleTag) {
                this.setTitleFromContent(true);
            }

            // Don't center, it will be done by the dialog open
            // Don't animate, the dialog is invisible anyway
            this._setHeightFromContentInternal(false, true);

            var hostname = document.location.protocol + "//" + document.location.hostname;

            if (document.location.port) {
                hostname += ":" + document.location.port;
            }

            // Pass the hostname of the window so that subsequent postMessage() requests can be directed to the right domain.
            this._postCommandToParent("notifyReady", {
                hostname: hostname
            });

            // Poll for content size changes. This appears to be more foolproof and cheaper than any other method
            // (i.e. manually calling it, etc
            if ($.modalDialog.autoSizing) {
                this.enableAutoSizing();
            }
        },

        enableAutoSizing: function(enable) {
            if (typeof enable == "undefined") {
                enable = true;
            }

            if (enable) {
                this._autoSizeInterval = setInterval(
                    $.proxy(function() {
                        this._setHeightFromContentInternal(false, true);
                    }, this),
                    100);
            } else {
                if (this._autoSizeInterval) {
                    window.clearInterval(this._autoSizeInterval);
                }
            }
        },

        postMessageToParent: function(message) {
            // Try to detect if the parent window is directly accessible.
            // This is significantly less expensive on old browsers that don't support postMessage.
            if (typeof this._postMessageDirect == "undefined") {
                try {
                    // This logs a warning in some browsers if it fails, but it isn't exposed to users, so its not worth worrying about.
                    this._postMessageDirect = parent._dialogReceiveMessageManual;
                    if (!this._postMessageDirect) {
                        this._postMessageDirect = null;
                    }
                } catch (ex) {
                    this._postMessageDirect = null;
                }
            }

            if (this._postMessageDirect) {
                this._postMessageDirect(message, document.location.protocol + "//" + document.location.host);
            } else {
                // parentHostName could be *, but $.postMessage() polyfill requires parentHostName.
                if (!$.modalDialog.parentHostName) {
                    throw new Error("Must specify $.modalDialog.parentHostName because the parent is in a different domain");
                }

                // The postmessage plugin is loaded- its probably because we're on an old browser.
                if ($.postMessage) {
                    $.postMessage(message, $.modalDialog.parentHostName, parent);
                } else {
                    parent.postMessage(message, $.modalDialog.parentHostName);
                }
            }
        },

        _postCommandToParent: function(command, data) {
            var messageData = {
                dialogCmd: command,
                _fullId: this.settings._fullId
            };

            if (data) {
                $.extend(messageData, data);
            }

            var message = $.param(messageData);

            this.postMessageToParent(message);
        },

        _trigger: function(eventType, data) {
            var event = this["on" + eventType];
            if (event) {
                event.fire(data);
            }
        }
    };

    // A map of dialog full IDs to dialog proxy instances (only proxies created in this window)
    var _fullIdMap = {};

    // Duplicates the host window static API, but acts on FramedDialogProxy objects
    $.modalDialog = $.modalDialog || {};

    $.modalDialog._isContent = true;

    // By default, the dialog content will notify the parent when it is ready to be show on load.
    // Set this to true to override this behavior. dialog.notifyReady() should be called when it is ready to be shown.
    $.modalDialog.manualNotifyReady = false;

    // Specifies a selector for an element that determines the size of the dialog content.
    $.modalDialog.sizeElement = ".dialog-content-size";

    // If true, the dialog will automatically resize to fit its content, even when it changes dynamically
    $.modalDialog.autoSizing = true;

    // If the dialog is in a different domain then the host window, this needs to be set to the host window domain, i.e. "http://www.vistaprint.com"
    $.modalDialog.parentHostName = null;

    // If true, the HTML TITLE tag is used for the dialog title automatically (when notifyReady() is called)
    $.modalDialog.useTitleTag = true;

    // Creates a new dialog
    $.modalDialog.create = function(settings) {
        settings = $.extend({}, settings);

        // When creating a new window, FramedDialogProxy needs the parent window
        // to choose an ID, because dialog "fullId"s represent the full parent-child relationships.
        settings._parentWindow = window;

        var dialogProxy = new FramedDialogProxy(settings);

        // Cache the dialog.
        _fullIdMap[dialogProxy.settings._fullId] = dialogProxy;

        return dialogProxy;
    };

    // Gets the current dialog's object
    $.modalDialog.getCurrent = function() {
        if (window.name.indexOf("dialog") !== 0) {
            return null;
        }

        // Get the dialog proxy from the cache if it has been created in this window already.
        // This is necessary to preserve settings between calls.

        var dialogProxy = getDialogByFullId(window.name);
        if (!dialogProxy) {
            dialogProxy = new FramedDialogProxy({
                _window: window
            });
            _fullIdMap[window.name] = dialogProxy;
        }

        return dialogProxy;
    };

    var getDialogByFullId = function(fullId) {
        return _fullIdMap[fullId];
    };

    // A map of cross-window command names to actions to be called on the dialog proxy
    var messageActions = {
        setHeightFromContent: function(dialog, qs) {
            dialog.setHeightFromContent(qs.center === "true", qs.skipAnimation === "true");
        },
        setTitleFromContent: function(dialog) {
            dialog.setTitleFromContent();
        },
        eventclose: function(dialog, qs) {
            dialog._trigger("close", qs);
        },
        eventbeforeclose: function(dialog, qs) {
            dialog._trigger("beforeclose", qs);
        }
    };

    var messageHandler = function(e) {
        //console.log("content receive: " + (e.originalEvent ? e.originalEvent.data : e.data));

        var qs;

        try {
            qs = $.deparam(e.originalEvent ? e.originalEvent.data : e.data);
        } catch (ex) {
            //ignore- it wasn't a message for the dialog framework
        }

        if (!qs.dialogCmd) {
            //ignore- it wasn't a message for the dialog framework
            return;
        }

        var action = messageActions[qs.dialogCmd];
        if (action) {
            var dialogProxy = null;

            // If a ID was passed for a specific dialog, only call the
            // methods for that specific dialog. Otherwise, operate on the 
            // current dialog. This allows the parent to "broadcast" events
            // to all windows, because any one of them may have a dialog proxy
            // for the target window.

            if (qs._eventDialogId) {
                dialogProxy = getDialogByFullId(qs._eventDialogId);
                delete qs._eventDialogId;
            } else {
                dialogProxy = $.modalDialog.getCurrent(window);
            }

            if (dialogProxy) {
                action(dialogProxy, qs);
            }
        }
    };

    // If available, use the jQuery.postMessage plugin
    if ($.receiveMessage) {
        $.receiveMessage(messageHandler, "*");
    } else {
        // Use native postMessage
        $(window).on("message", messageHandler);
    }

    // HACK: Jquery mobile specific hacks.

    if ($.mobile && $.mobile.resetActivePageHeight) {
        // Jquery mobile sets min-heights on page content to the current body width.
        // This makes calculating the content height difficult, and isn't necessary within
        // an iframe dialog anyway.

        $.mobile.document.off("pageshow", $.mobile.resetActivePageHeight);
        $.mobile.window.off("throttledresize", $.mobile.resetActivePageHeight);
        $(".ui-page").css("min-height", 0);

        $.mobile.resetActivePageHeight = $.noop;

        // jQuery mobile calls focusPage when the first page initializes. 
        // This causes the parent window to scroll to the top of the page.

        $.mobile.focusPage = $.noop;
    }

    $(window).load(function() {
        var currentDialog = $.modalDialog.getCurrent();
        if (currentDialog) {
            // Notify the opener that we're ready to be made visible
            if (!$.modalDialog.manualNotifyReady) {
                currentDialog.notifyReady();
            }
        }
    });

})(jQuery);

// iOS
// iOS has a bug where text fields in an iFrame misbehave if there are touch events assigned to the 
// host window. This fix disables them while iFrame dialogs are open.

// Android
// Older versions of Android stock browser, particularly ones whose manufacturers customized the browser
// with proprietary text field overlays, have trouble with complex positioning and transforms.
// This becomes exacerbated by the complexity of the modal dialog DOM, especially when an IFrame 
// is involved.
// The result is that the proprietary text field overlays are positioned incorrectly (best case),
// or that they start producing nonsensical focus events, which cause the browser to scroll wildly.
// http://stackoverflow.com/questions/8860914/on-android-browser-the-whole-page-jumps-up-and-down-when-typing-inside-a-textbo

// Newer android browsers (4+) support the CSS property: -webkit-user-modify: read-write-plaintext-only;
// This will prevent the proprietary text field overlay from showing (though also HTML5 custom ones, such as email keyboards).
// http://stackoverflow.com/questions/9423101/disable-android-browsers-input-overlays
// https://code.google.com/p/android/issues/detail?id=30964

// To work around this problem in older Android (2.3), we have to hide elements that have any CSS transforms.
// The cleanest way is to remove ALL content in the DOM in the main panel. This will make the screen behind the dialog turn
// completely gray, which isn't a big deal- many dialog frameworks do this anyway.
// To do this, add the attribute to the element:
// data-dialog-main-panel="true"

// Otherwise, you can hide specific problematic elements by adding this attribute:
// data-dialog-hide-onopen="true"

(function($) {
    var SELECTOR_MAIN_PANEL = "[data-dialog-main-panel='true']";
    var SELECTOR_BAD_ELEMENT = "[data-dialog-hide-onopen='true']";

    var preventWindowTouchEvents = function(dialog, fix) {
        // The bug only affects iFrame dialogs
        if (dialog.dialogType != "iframe") {
            return;
        }

        $([window, document]).enableEvent("touchmove touchstart touchend", !fix);
    };

    var getWindowHeight = function() {
        return window.innerHeight || $(window).height();
    };

    var initializeShimming = function() {
        // First, see if the main panel is specified.
        // If so, it's the best choice of elements to hide.
        var $badEls = $(SELECTOR_MAIN_PANEL);
        if ($badEls.length === 0) {
            // Otherwise, look for individually marked bad elements to hide.
            $badEls = $(SELECTOR_BAD_ELEMENT);
        }

        // Cache original values to restore when the dialog closes
        var _scrollTop = 0;
        var _height = 0;

        $.modalDialog.onbeforeopen.add(function() {
            if (this.level === 0) {
                // Cache scroll height and body height so we can restore them when the dialog is closed
                _scrollTop = $(document).scrollTop();
                _height = document.body.style.height;

                // Cache the parent for each element we need to remove from the DOM.
                // This is important to fix the various WebKit text overlay bugs (described above in the header).
                // Hiding them wont do it.
                $badEls.each(function(i, el) {
                    $(el).data("dialog-parent", el.parentNode);
                })
                    .detach();

                // HACK: setting the body to be larger than the screen height prevents the address bar from showing up in iOS
                document.body.style.height = (getWindowHeight() + 50) + "px";

                window.scrollTo(0, 1);
            }
        });

        $.modalDialog.onopen.add(function() {
            if (this.level === 0) {
                // Ensure the body/background is bigger than the dialog,
                // otherwise we see the background "end" above the bottom
                // of the dialog.
                var height = Math.max(this.$container.height(), getWindowHeight()) + 20;

                document.body.style.height = height + "px";
                $(".dialog-background").css({
                    height: height
                });

                window.scrollTo(0, 1);
            }
        });

        $.modalDialog.onclose.add(function() {
            if (this.level === 0) {
                // Restore body height, elements, and scroll position
                document.body.style.height = _height;

                $badEls.each(function(i, el) {
                    $($(el).data("dialog-parent")).append(el);
                });

                window.scrollTo(0, _scrollTop);
            }
        });
    };

    $(function() {
        if (!$.modalDialog.isSmallScreen()) {
            return;
        }

        // When removing the host window content from the DOM, make the veil opaque to hide it.
        $.modalDialog.veilClass = "dialog-veil-opaque";

        // This will run in a content window. They need the events disabled immediately.
        if ($.modalDialog && $.modalDialog._isContent) {
            var dialog = $.modalDialog.getCurrent();
            if (dialog) {
                $(window).on("load", function() {
                    preventWindowTouchEvents(dialog, true);
                });
            }
        } else {
            // This is for the host window.
            $.modalDialog.onopen.add(function() {
                preventWindowTouchEvents(this, true);
            });
            $.modalDialog.onbeforeclose.add(function() {
                preventWindowTouchEvents(this, false);
            });

            initializeShimming();
        }
    });

})(jQuery);

/*
Uses declarative syntax to define a dialog. Syntax:

<a 
href="{selector or url"
data-rel="modalDialog"
data-dialog-title="{title}"
data-dialog-onopen="{onopen handler}"
data-dialog-onbeforeopen="{onbeforeopen handler}"
data-dialog-onclose="{onclose handler}"
data-dialog-onnbeforeclose="{onbeforeclose handler}"
data-dialog-maxWidth="{maxWidth}"
data-dialog-skin="{skin}"
data-dialog-ajax="{true or false}"
data-dialog-destroyonclose="{true or false}"
data-dialog-zIndex="{default zIndex}"
>link</a>

For node dialogs, these same properties can also be put on the dialog node as well.

TODO: Move some of the declarative settings into the core, because it is useful regardless of making
the trigger tag unobtrusive

TODO Make the dialog veil hide earlier when closing dialogs. It takes too long.
*/

(function($) {
    var DIALOG_DATA_KEY = "modalDialogUnobtrusive";

    // Click handler for all links which open dialogs
    var dialogLinkHandler = function(e) {
        e.preventDefault();

        var $link = $(e.currentTarget);

        var href = $link.attr("data-dialog-url") || $link.attr("href");

        if (!href) {
            throw new Error("no href specified with data-rel='modalDialog'");
        }

        // Create a dialog settings object
        var settings = {
            contentOrUrl: href
        };

        // Duplicate values on the link will win over values on the dialog node
        var linkSettings = $.modalDialog.getSettings($link);
        $.extend(settings, linkSettings);

        // Give unobtrusive scripts a chance to modify the settings
        var evt = new $.Event("dialogsettingscreate");
        evt.dialogSettings = settings;

        $link.trigger(evt);

        if (evt.isDefaultPrevented()) {
            return;
        }

        var dialog = $link.data(DIALOG_DATA_KEY);

        // If the dialog has been previously opened, ensure that the settings haven't changed.
        // If so, discard the cached dialog and create a new one.
        if (dialog) {
            var processedSettings = $.modalDialog._ensureSettings(settings);

            if (!$.modalDialog._areSettingsEqual(dialog.settings, processedSettings)) {
                dialog._destroy();
                dialog = null;
            }
        }

        if (!dialog) {
            dialog = $.modalDialog.create(settings);

            // Give unobtrusive scripts a chance to modify the dialog
            evt = new $.Event("dialogcreate");
            evt.dialogSettings = settings;
            evt.dialog = dialog;

            $link.trigger(evt);

            if (evt.isDefaultPrevented()) {
                return;
            }

            // Unless destroyOnClose is specified,
            // cache the dialog object so it won't be initialized again
            if (!settings.destroyOnClose) {
                $link.data(DIALOG_DATA_KEY, dialog);
            }
        }

        dialog.open();
    };

    // Assign handlers to all dialog links
    $(document).on("click", "[data-rel='modalDialog']", dialogLinkHandler);

    // Helpful utility: A class that will make a button close dialogs by default
    $(document).on("click", ".close-dialog", function(e) {
        e.preventDefault();

        // Defer to the next tick of the event loop. It makes it more useful
        // to apply this class without having to worry if the close handler will
        // run before any other handlers.
        setTimeout(function() {
            var dialog = $.modalDialog.getCurrent();
            if (dialog) {
                dialog.close();
            }
        }, 0);
    });

})(jQuery);
