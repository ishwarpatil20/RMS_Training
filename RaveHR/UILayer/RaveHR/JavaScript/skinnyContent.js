/*! skinny.js v0.1.0 | Copyright 2013 Vistaprint | vistaprint.github.io/SkinnyJS/LICENSE 
http://vistaprint.github.io/SkinnyJS/download-builder.html?modules=jquery.clientRect,jquery.contentSize,jquery.customEvent,jquery.delimitedString,jquery.queryString,pointy,jquery.postMessage*/

(function($) {

    // Expose support flag. Aids in unit testing.
    $.support.getBoundingClientRect = "getBoundingClientRect" in document.documentElement;

    // Gets the window containing the specified element.
    function getWindow(elem) {
        return $.isWindow(elem) ?
            elem :
            elem.nodeType === 9 ?
            elem.defaultView || elem.parentWindow :
            false;
    }

    // Returns a rect for the first element in the jQuery object.
    $.fn.clientRect = function() {
        var rect = {
            top: 0,
            left: 0,
            width: 0,
            height: 0,
            bottom: 0,
            right: 0
        };

        if (this.length === 0) {
            return rect;
        }

        var elem = this[0];
        var doc = elem.ownerDocument;
        var docElem = doc.documentElement;
        var box;

        // Make sure we're not dealing with a disconnected DOM node
        if (!$.contains(docElem, elem)) {
            return rect;
        }

        // Make modern browsers wicked fast
        if ($.support.getBoundingClientRect) {
            // This is derived from the internals of jQuery.fn.offset
            try {
                box = elem.getBoundingClientRect();
            } catch (e) {
                // OldIE throws an exception when trying to get a client rect for an element
                // that hasn't been rendered, or isn't in the DOM.
                // For consistency, return a 0 rect.
            }

            if (!box) {
                return rect;
            }

            // TODO needs a unit test to verify the returned rect always has the same properties (i.e. bottom, right)
            // If the rect has no area, it needs no further processing
            if (box.right === box.left &&
                box.top === box.bottom) {
                return rect;
            }

            // Handles some quirks in the oldIE box model, including some bizarre behavior around the starting coordinates.
            var win = getWindow(doc);

            rect.top = box.top + (win.pageYOffset || docElem.scrollTop) - (docElem.clientTop || 0);
            rect.left = box.left + (win.pageXOffset || docElem.scrollLeft) - (docElem.clientLeft || 0);

            rect.width = box.right - box.left;
            rect.height = box.bottom - box.top;
        } else {
            // Support ancient browsers by falling back to jQuery.outerWidth/Height()
            if (this.css("display") == "none") {
                return rect;
            }

            rect = this.offset();
            rect.width = this.outerWidth();
            rect.height = this.outerHeight();
        }

        rect.bottom = rect.top + rect.height;
        rect.right = rect.left + rect.width;

        return rect;
    };

})(jQuery);
; /// <reference path="jquery.clientRect.js" ></reference>

(function($) {

    var addMargin = function(node, styleProp, rectProp, rect) {
        var margin = parseInt($(node).css(styleProp), 10);
        if (margin) {
            rect[rectProp] += margin;
        }
    };

    // Size gets continuously populated as this recurses through the DOM, building the max size of the page.
    var gatherSize = function(size, node, includeChildrenOnly, includeWidth, includeHeight) {
        var rect;

        // Only look at elements
        if (node.nodeType != 1) {
            return;
        }

        if (!includeChildrenOnly) {
            try {
                rect = $(node).clientRect();
            } catch (ex) {
                // Couldn't get the size, so let's just return.
                return;
            }

            //if the node is not rendered, don't factor in its size
            if (rect.height === 0 && rect.width === 0) {
                return;
            }

            if (node.tagName == "BODY") {
                addMargin(node, "marginRight", "right", rect);
                addMargin(node, "marginBottom", "bottom", rect);
            }

            if (includeHeight) {
                size.height = Math.max(rect.bottom, size.height);
            }

            if (includeWidth) {
                size.width = Math.max(rect.right, size.width);
            }

            // If the node is a vertical scrolling container, don't look at its children for the purposes of calculating height
            if ($(node).css("overflowX") != "visible" &&
                ($(node).css("height") != "auto" || $(node).css("maxHeight") != "none")) {
                includeHeight = false;
            }

            // If the node is a horizontal scrolling container, don't look at its children for the purposes of calculating width
            if ($(node).css("overflowY") != "visible" &&
                ($(node).css("width") != "auto" || $(node).css("maxWidth") != "none")) {
                includeWidth = false;
            }

            // Optimization- if we don't need to measure any children, stop recursing.
            if (!includeHeight && !includeWidth) {
                return;
            }
        }

        // Recurse
        if (node.tagName !== "OBJECT") {
            var len = node.childNodes.length;
            for (var i = 0; i < len; i++) {
                gatherSize(size, node.childNodes[i], false, includeWidth, includeHeight);
            }
        }
    };

    // Returns the height and width of the total page: the total scrolling size.
    $.fn.contentSize = function(excludeScrollbars) {
        var el = this[0];

        if (!el) {
            throw new Error("Element required");
        }

        // If el is a window or a document, pay attention to excludeScrollbars
        // doc will be null if el is not a window or document
        var doc = el.document || (el.documentElement ? el : (el.tagName == "BODY" ? el.ownerDocument : null));

        //Exclude scrollbars- browsers don't offer any way to ignore the scrollbar
        //when calculating content dimensions, so just hide/restore

        var currentOverflow;
        if (excludeScrollbars && doc) {
            currentOverflow = doc.documentElement.style.overflow;
            doc.documentElement.style.overflow = "hidden";
        }

        var size = {
            width: 0,
            height: 0
        };
        var startingNode = doc ? doc.body : el;
        var includeChildrenOnly = false;

        if (startingNode.tagName == "BODY") {
            includeChildrenOnly = true;
        }

        gatherSize(size, doc ? doc.body : el, includeChildrenOnly, true, true);

        if (excludeScrollbars && doc) {
            doc.documentElement.style.overflow = currentOverflow;
        }

        return size;
    };

})(jQuery);
; (function($) {

    // Utility class to manage multiple callbacks.

    // * {object} host: The object owning the event
    // * {string} eventType: The event type (i.e. "close", open")
    $.CustomEvent = function(host, eventType) {
        this._host = host;
        this.eventType = eventType;
        this._callbacks = new $.Callbacks();
    };


    // Triggers the event, and returns a jQuery.Event object.

    // * {object} data: Any data that should appended to the event object
    // * {object} host: Defines "this" in handlers. If not specified, the default host object is used.
    $.CustomEvent.prototype.fire = function(data, host) {
        var evt = new $.Event(this.eventType);
        $.extend(evt, data);
        evt.data = $.extend({}, evt.data, data);

        this._callbacks.fireWith(host || this._host, [evt]);

        return evt;
    };


    // Assigns an event handler

    // * {Function} callback: The event handler
    $.CustomEvent.prototype.add = function(callback) {
        if (callback) {
            this._callbacks.add(callback);
        }
    };

    // * {Function} callback: The event handler
    $.CustomEvent.prototype.one = function(callback) {
        if (!callback) {
            return;
        }

        var me = this;

        var wrapper = $.proxy(function() {
            try {
                callback.apply(this, arguments);
            } finally {
                me.remove(wrapper);
            }

        }, this);

        this.add(wrapper);
    };

    // Assigns an event handler
    // * {Function} callback: The event handler
    $.CustomEvent.prototype.remove = function(callback) {
        if (callback) {
            this._callbacks.remove(callback);
        }
    };

    // Removes all event handlers
    $.CustomEvent.prototype.empty = function() {
        this._callbacks.empty();
    };

    // Utility for adding an event to an object more tersely

    // * {object} host: The object owning the event
    // * {string} eventType: The event type (i.e. "close", open")
    $.CustomEvent.create = function(host, eventType) {
        var onEventType = "on" + eventType;
        var evt = new $.CustomEvent(host, eventType);
        host[onEventType] = evt;
        return evt;
    };

})(jQuery);
; (function($) {

    // Takes a plain javascript object (key value pairs), and encodes it as a string 
    // using the specified delimiters and encoders
    $.encodeDelimitedString = function(data, itemDelimiter, pairDelimiter, keyEncoder, valueEncoder) {
        if (!data) {
            return "";
        }

        keyEncoder = keyEncoder || function(s) {
            return s;
        };
        valueEncoder = valueEncoder || keyEncoder;

        var sb = [];

        for (var key in data) {
            if (data.hasOwnProperty(key)) {
                sb.push(keyEncoder(key) + pairDelimiter + valueEncoder(data[key]));
            }
        }

        return sb.join(itemDelimiter);
    };

    // Takes an encoded string, and parses it into a plain javascript object (key value pairs)
    // using the specified delimiters and decoders
    $.parseDelimitedString = function(delimitedString, itemDelimiter, pairDelimiter, keyDecoder, valueDecoder) {
        keyDecoder = keyDecoder || function(s) {
            return s;
        };
        valueDecoder = valueDecoder || keyDecoder;

        var ret = {};

        if (delimitedString) {
            var pairs = delimitedString.split(itemDelimiter);
            var len = pairs.length;
            for (var i = 0; i < len; i++) {
                var pair = pairs[i];

                if (pair.length > 0) {
                    var delimIndex = pair.indexOf(pairDelimiter);
                    var key, value;

                    if (delimIndex > 0 && delimIndex <= pair.length - 1) {
                        key = pair.substring(0, delimIndex);
                        value = pair.substring(delimIndex + 1);
                    } else {
                        key = pair;
                    }

                    ret[keyDecoder(key)] = valueDecoder(value);
                }
            }
        }

        return ret;
    };

})(jQuery);
; /// <reference path="jquery.delimitedString.js" ></reference>

(function($) {
    var PLUS_RE = /\+/gi;

    var urlDecode = function(s) {
        // Specifically treat null/undefined as empty string
        if (s == null) {
            return "";
        }

        // Replace plus with space- jQuery.param() explicitly encodes them,
        // and decodeURIComponent explicitly does not.
        return decodeURIComponent(s.replace(PLUS_RE, " "));
    };

    // Given a querystring (as a string), deserializes it to a javascript object.
    $.deparam = function(queryString) {
        if (typeof queryString != "string") {
            throw new Error("$.deparam() expects a string for 'queryString' argument.");
        }

        // Remove "?", which starts querystrings
        if (queryString && queryString.charAt(0) == "?") {
            queryString = queryString.substring(1, queryString.length);
        }

        return $.parseDelimitedString(queryString, "&", "=", urlDecode);
    };

    // Alias
    $.parseQueryString = $.deparam;

    // Gets the querystring from the current document.location as a javascript object.
    $.currentQueryString = function() {
        return $.deparam(window.location.search);
    };

    // Given a url (pathname) and an object representing a querystring, constructs a full URL
    $.appendQueryString = function(url, parsedQueryString) {
        var qs = $.param(parsedQueryString);
        if (qs.length > 0) {
            qs = "?" + qs;
        }

        return url + qs;
    };

})(jQuery);
; /*!
 * Pointy.js
 * Pointer Events polyfill for jQuery
 * https://github.com/vistaprint/PointyJS
 *
 * Depends on jQuery, see http://jquery.org
 *
 * Developed by Vistaprint.com
 */

(function($, window, document, undefined) {

    var support = {
        touch: "ontouchend" in document,
        pointer: !!(navigator.pointerEnabled || navigator.msPointerEnabled)
    };

    $.extend($.support, support);

    function triggerCustomEvent(elem, eventType, originalEvent) {
        // support for IE7-IE8
        originalEvent = originalEvent || window.event;

        // Create a writable copy of the event object and normalize some properties
        var event = new jQuery.Event(originalEvent);
        event.type = eventType;

        // Copy over properties for ease of access
        var i, copy = $.event.props.concat($.event.pointerHooks.props);
        i = copy.length;
        while (i--) {
            var prop = copy[i];
            event[prop] = originalEvent[prop];
        }

        // Support: IE<9
        // Fix target property (#1925)
        if (!event.target) {
            event.target = originalEvent.srcElement || document;
        }

        // Support: Chrome 23+, Safari?
        // Target should not be a text node (#504, #13143)
        if (event.target.nodeType === 3) {
            event.target = event.target.parentNode;
        }

        // Support: IE<9
        // For mouse/key events, metaKey==false if it's undefined (#3368, #11328)
        event.metaKey = !!event.metaKey;

        // run the filter now
        event = $.event.pointerHooks.filter(event, originalEvent);

        // trigger the emulated pointer event
        // the filter can return an array (only if the original was a touchmove),
        // which means we need to trigger independent events
        if ($.isArray(event)) {
            $.each(event, function(i, ev) {
                $.event.dispatch.call(elem, ev);
            });
        } else {
            $.event.dispatch.call(elem, event);
        }

        // return the manipulated jQuery event
        return event;
    }

    function addEvent(elem, type, selector, func) {
        // when we have a selector, let jQuery do the delegation
        if (selector) {
            func._pointerEventWrapper = function(event) {
                return func.call(elem, event.originalEvent);
            };

            $(elem).on(type, selector, func._pointerEventWrapper);
        }

        // if we do not have a selector, we optimize by cutting jQuery out
        else {
            if (elem.addEventListener) {
                elem.addEventListener(type, func, false);
            } else if (elem.attachEvent) {

                // bind the function to correct "this" for IE8-
                func._pointerEventWrapper = function(e) {
                    return func.call(elem, e);
                };

                elem.attachEvent("on" + type, func._pointerEventWrapper);
            }
        }
    }

    function removeEvent(elem, type, selector, func) {
        // Make sure for IE8- we unbind the wrapper
        if (func._pointerEventWrapper) {
            func = func._pointerEventWrapper;
        }

        if (selector) {
            $(elem).off(type, selector, func);
        } else {
            $.removeEvent(elem, type, func);
        }
    }

    // get the standardized "buttons" property as per the Pointer Events spec from a mouse event
    function getStandardizedButtonsProperty(event) {
        // in the DOM LEVEL 3 spec there is a new standard for the "buttons" property
        // sadly, no browser currently supports this and only sends us the single "button" property
        if (event.buttons) {
            return event.buttons;
        }

        // standardize "which" property for use
        var which = event.which;
        if (!which && event.button !== undefined) {
            which = (event.button & 1 ? 1 : (event.button & 2 ? 3 : (event.button & 4 ? 2 : 0)));
        }

        // no button down (can happen on mousemove)
        if (which === 0) {
            return 0;
        }
        // left button
        else if (which === 1) {
            return 1;
        }
        // middle mouse
        else if (which === 2) {
            return 4;
        }
        // right mouse
        else if (which === 3) {
            return 2;
        }

        // unknown?
        return 0;
    }

    function returnFalse() { return false; }
    function returnTrue() { return true; }

    var POINTER_TYPE_UNAVAILABLE = "unavailable";
    var POINTER_TYPE_TOUCH = "touch";
    var POINTER_TYPE_PEN = "pen";
    var POINTER_TYPE_MOUSE = "mouse";

    // indicator to mark whether touch events are in progress
    // when null, it means we have never received a touch event
    // when number, user is currently touching something
    // when false, user just released their finger (reset on mousedown if needed)
    var _touching = null;

    // bitmask to identify which buttons are currently down
    var _buttons = 0;

    // storage of the last seen touches provided by the native touch events spec
    var _lastTouches = [];

    // ------ NOTE: THIS IS UNUSED, WE DO NOT ASSIGN BUTTON ------
    // pointer events defines the "button" property as:
    // mouse move (no buttons down)                         -1
    // left mouse, touch contact and normal pen contact     0
    // middle mouse                                         1
    // right mouse, pen with barrel button pressed          2
    // x1 (back button on mouse)                            3
    // x2 (forward button on mouse)                         4
    // pen contact with eraser button pressed               5
    // ------ NOTE: THIS IS UNUSED, WE DO NOT ASSIGN BUTTON ------

    // pointer events defines the "buttons" property as:
    // mouse move (no buttons down)                         0
    // left mouse, touch contact, and normal pen contact    1
    // middle mouse                                         4
    // right mouse, pen contact with barrel button pressed  2
    // x1 (back) mouse                                      8
    // x2 (forward) mouse                                   16
    // pen contact with eraser button pressed               32

    // add our own pointer event hook/filter
    $.event.pointerHooks = {
        props: "pointerType pointerId pressure buttons clientX clientY relatedTarget fromElement offsetX offsetY pageX pageY screenX screenY width height toElement".split(" "),
        filter: function(event, original) {
            // Calculate pageX/Y if missing and clientX/Y available
            // this is just copied from jQuery's standard pageX/pageY fix
            if (!original.touches && event.pageX == null && original.clientX != null) {
                var eventDoc = event.target.ownerDocument || document;
                var doc = eventDoc.documentElement;
                var body = eventDoc.body;

                event.pageX = original.clientX + (doc && doc.scrollLeft || body && body.scrollLeft || 0) - (doc && doc.clientLeft || body && body.clientLeft || 0);
                event.pageY = original.clientY + (doc && doc.scrollTop || body && body.scrollTop || 0) - (doc && doc.clientTop || body && body.clientTop || 0);
            }

            // Add relatedTarget, if necessary
            // also copied from jQuery's standard event fix
            if (!event.relatedTarget && original.fromElement) {
                event.relatedTarget = original.fromElement === event.target ? original.toElement : original.fromElement;
            }

            // Add pointerType
            if (!event.pointerType || typeof event.pointerType === "number") {
                if (event.pointerType == 2) {
                    event.pointerType = POINTER_TYPE_TOUCH;
                } else if (event.pointerType == 3) {
                    event.pointerType = POINTER_TYPE_PEN;
                } else if (event.pointerType == 4) {
                    event.pointerType = POINTER_TYPE_MOUSE;
                } else if (/^touch/i.test(original.type)) {
                    event.pointerType = POINTER_TYPE_TOUCH;
                    event.buttons = original.type === "touchend" || original.type === "touchcancel" ? 0 : 1;
                } else if (/^mouse/i.test(original.type) || original.type === "click") {
                    event.pointerId = 1; // as per the pointer events spec, the mouse is always pointer id 1
                    event.pointerType = POINTER_TYPE_MOUSE;
                    event.buttons = original.type === "mouseup" ? 0 : getStandardizedButtonsProperty(original);
                } else {
                    event.pointerType = POINTER_TYPE_UNAVAILABLE;
                    event.buttons = 0;
                }
            }

            // if we have the bitmask for the depressed buttons from the mouse events polyfill, use it to mimic buttons for
            // browsers that do not support the HTML DOM LEVEL 3 events spec
            if (event.type !== "pointerdown" && event.pointerType === "mouse" && _touching === null && _buttons !== event.buttons) {
                event.buttons = _buttons;
            }

            // standardize the pressure attribute
            if (!event.pressure) {
                event.pressure = event.buttons > 0 ? 0.5 : 0;
            }

            // standardize the width and height, these represent the contact geometry
            if (event.width === undefined || event.height === undefined) {
                event.width = event.height = 0;
            }

            // prevent the following native "click" event from occurring, can be used to prevent
            // clicks on "pointerdown" or "pointerup" or from gestures like "press" and "presshold"
            event.preventClick = function() {
                event.isClickPrevented = returnTrue;
                $(event.target).one("click", returnFalse);
            };

            event.isClickPrevented = returnFalse;

            // touch events send an array of touches we need to convert to the pointer events format
            // which means we need to fire multiple events per touch
            if (original.touches && event.type !== "pointercancel") {
                var touches = original.touches;
                var events = [];
                var ev, i, j;

                // the problem with this is that on "touchend" it will remove the
                // touch which has ended from the touches list, this means we do
                // not want to fire "pointerup" for touches that are still there,
                // we instead want to send a "pointerup" with the removed touch's identifier
                if (event.type === "pointerup") {
                    // convert TouchList to a standard array
                    _lastTouches = Array.prototype.slice.call(_lastTouches);

                    // find the touch that was removed
                    for (i = 0; i < original.touches.length; i++) {
                        for (j = 0; j < _lastTouches.length; j++) {
                            if (_lastTouches[j].identifier === original.touches[i].identifier) {
                                _lastTouches.splice(j, 1);
                            }
                        }
                    }

                    // if we narrowed down the ended touch to one, then we found it
                    if (_lastTouches.length === 1) {
                        event.pointerId = _lastTouches[0].identifier;
                        _lastTouches = original.touches;
                        return event;
                    }
                }
                // on "pointerdown" we need to only trigger a new "pointerdown" for the new touches (fingers),
                // and not the touches that were already active. Therefore we filter out all of the touches
                // based on their identifier that we already knew were active before
                else if (event.type === "pointerdown") {
                    // convert TouchList to a standard array
                    touches = Array.prototype.slice.call(original.touches);

                    // find the new touch that was just added
                    for (i = 0; i < touches.length; i++) {
                        // last touches will be a list with one less touch
                        for (j = 0; j < _lastTouches.length; j++) {
                            if (touches[i].identifier === _lastTouches[j].identifier) {
                                touches.splice(i, 1);
                            }
                        }
                    }
                }

                // this will be used on pointermove and pointerdown
                for (i = 0; i < original.touches.length; i++) {
                    var touch = original.touches[i];
                    ev = $.extend({}, event);
                    // copy over information from the touch to the event
                    ev.clientX = touch.clientX;
                    ev.clientY = touch.clientY;
                    ev.pageX = touch.pageX;
                    ev.pageY = touch.pageY;
                    ev.screenX = touch.screenX;
                    ev.screenY = touch.screenY;
                    // the touch id on emulated touch events from chrome is always 0 (zero)
                    ev.pointerId = touch.identifier;
                    events.push(ev);
                }

                // do as little processing as you can here, this is done on touchmove and
                // there can be a lot of those events firing quickly, we do not want the
                // polyfill slowing down the application
                _lastTouches = original.touches;
                return events;
            }

            return event;
        }
    };

    $.event.delegateSpecial = function(setup) {
        return function(handleObj) {
            var thisObject = this,
                data = jQuery._data(thisObject);

            if (!data.pointerEvents) {
                data.pointerEvents = {};
            }

            if (!data.pointerEvents[handleObj.type]) {
                data.pointerEvents[handleObj.type] = [];
            }

            if (!data.pointerEvents[handleObj.type].length) {
                setup.call(thisObject, handleObj);
            }

            data.pointerEvents[handleObj.type].push(handleObj);
        };
    };

    $.event.delegateSpecial.remove = function(teardown) {
        return function(handleObj) {
            var handlers,
                thisObject = this,
                data = jQuery._data(thisObject);

            if (!data.pointerEvents) {
                data.pointerEvents = {};
            }

            handlers = data.pointerEvents[handleObj.type];

            handlers.splice(handlers.indexOf(handleObj), 1);

            if (!handlers.length) {
                teardown.call(thisObject, handleObj);
            }
        };
    };

    // allow jQuery's native $.event.fix to find our pointer hooks
    $.extend($.event.fixHooks, {
        pointerdown: $.event.pointerHooks,
        pointerup: $.event.pointerHooks,
        pointermove: $.event.pointerHooks,
        pointerover: $.event.pointerHooks,
        pointerout: $.event.pointerHooks,
        pointercancel: $.event.pointerHooks
    });

    // if browser does not natively handle pointer events,
    // create special custom events to mimic them
    if (!support.pointer) {
        // stores the scroll-y offest on "touchstart" and is compared
        // on touchend to see if we should trigger a click event
        var _startScrollOffset;

        // utility to return the scroll-y position
        var scrollY = function() {
            return Math.floor(window.scrollY || $(window).scrollTop());
        };

        $.event.special.pointerdown = {
            touch: function(event) {
                // set the pointer as currently down to prevent chorded "pointerdown" events
                _touching = event.timeStamp;

                // trigger a new "pointerdown" event
                triggerCustomEvent(this, "pointerdown", event);

                // set the scroll offset which is compared on TouchEnd
                _startScrollOffset = scrollY();

                // no matter what, you cannot simply always call preventDefault() on "touchstart"
                // this disables scrolling when touching the binded element, which is not appropriate.
            },
            mouse: function(event) {
                // if we just had a "touchstart", ignore this "mousedown" event, to prevent double firing of "pointerdown"
                if (typeof _touching === "number") {
                    return;
                }

                // we reset the touch to null, to indicate that we're listening to mouse events currently
                _touching = null;

                // update the _buttons bitmask
                var button = getStandardizedButtonsProperty(event);
                var wasAButtonDownAlready = _buttons !== 0;
                _buttons |= button;

                // do not trigger another "pointerdown" event if a button is currently down,
                // this prevents chorded "pointerdown" events which is defined in the Pointer Events spec
                if (wasAButtonDownAlready && _buttons !== button) {
                    // per the Pointer Events spec, when the active buttons change it fires only a "pointermove" event
                    triggerCustomEvent(this, "pointermove", event);
                    return;
                }

                triggerCustomEvent(this, "pointerdown", event);
            },
            add: $.event.delegateSpecial(function(handleObj) {
                // bind to touch events, some devices (chromebook) can send both touch and mouse events
                if (support.touch) {
                    addEvent(this, "touchstart", handleObj.selector, $.event.special.pointerdown.touch);
                }

                // bind to mouse events
                addEvent(this, "mousedown", handleObj.selector, $.event.special.pointerdown.mouse);

                // ensure we also bind to "pointerup" to properly clear signals and fire click event on "touchend"
                handleObj.pointerup = $.noop;
                $(this).on("pointerup", handleObj.selector, handleObj.pointerup);
            }),
            remove: $.event.delegateSpecial.remove(function(handleObj) {
                // unbind touch events
                if (support.touch) {
                    removeEvent(this, "touchstart", handleObj.selector, $.event.special.pointerdown.touch);
                }

                // unbind mouse events
                removeEvent(this, "mousedown", handleObj.selector, $.event.special.pointerdown.mouse);

                // unbind the special "pointerup" we added for cleanup
                if (handleObj.pointerup) {
                    $(this).off("pointerup", handleObj.selector, handleObj.pointerup);
                }
            })
        };

        $.event.special.pointerup = {
            touch: function(event) {
                // prevent default to prevent the emulated "mouseup" event from being triggered
                event.preventDefault();

                // safety check, if _touching is null then we just had a mouse event and shouldn't
                // listen to touch events right now
                if (_touching === null) {
                    return;
                }

                // timeStamp of when the last "touchstart" event was triggered,
                // used to prevent double fire issues on iOS 7+ devices when needed
                var lastTouchStarted = _touching;

                // trigger the emulated "pointerup" event, getting back the event
                var jEvent = triggerCustomEvent(this, "pointerup", event);

                // ensure we have a target before we attempt to deal with a "click" event
                if (!event.target) {
                    _touching = false; // release the "pointerdown" lock
                    return;
                }

                // while you may think if we are preventing the next "click" event (see jEvent.isClickPrevented)
                // we could simply not trigger one below, that turns out to be a bad assumption. Due to the
                // complex issue of dealing with various devices, often a click event is triggered regardless of
                // calling preventDefault on "touchend" so we have to still go on and determine if a "click"
                // event was triggered, if so, render it noop, if not, we don't have to trigger one then.

                // we confirm that the user did not scroll, as touch events are very related to scrolling on
                // touch devices, it's possible we may mis-fire a click event on an <a> anchor tag causing
                // navigation even though this was a scroll attempt. We let the browser's built in scrolling
                // threshold prevent accidental scrolling so we only need to check if they scrolled at all.
                if (_startScrollOffset !== scrollY()) {
                    _touching = false; // release the "pointerdown" lock
                    return;
                }

                // on "touchend", calling prevent default prevents the "mouseup" and "click" event
                // however on native "mouseup" events preventing default does not cancel the "click" event
                // as per the pointer event spec on "pointerup" preventing default should not cancel the "click" event
                //
                // so we really want to have a "click" event all the time. If a function binded to this emulated
                // "poiunterup" calls prevent default it would result in preventing the "click" event, which would
                // cause inconsistent behavior. To prevent the possibility of two click events though, we call
                // prevent default all the time (see above) and then trigger a "click" event here.
                //
                // Additionally, we are currently looking at the "touchend" event, mobile devices usually add a 300ms
                // delay before triggering click to check for double tap events (zooming action on most devices).
                // In certain situations the mobile device will still fire a "click" event even if preventDefault is
                // called on "touchend", so we wait the 300ms and look for a click, then only fire a click if none
                // was fired by the browser
                var clickTimer = setTimeout(function() {
                    if (_touching === lastTouchStarted) {
                        _touching = false; // release the "pointerdown" lock
                    }

                    // at this point, if no click event was triggered, and we don't want to to trigger a "click"
                    // event, we can simply not trigger one to begin with.
                    if (jEvent.isClickPrevented()) {
                        $(event.target).off("click", returnFalse);
                        return;
                    }

                    if (event.target.click) {
                        event.target.click();
                    } else {
                        // iOS 5 and older Android browsers do not define native .click events on all elements
                        $(event.target).click();
                    }
                }, 300);

                $(event.target).one("click", function() {
                    if (_touching === lastTouchStarted) {
                        _touching = false;
                    }

                    if (clickTimer) {
                        clearTimeout(clickTimer);
                    }
                });
            },
            mouse: function(event) {
                // if originally we had a "touchstart" or we ended with a "touchend" event, ignore this "mouseup"
                if (_touching !== null) {
                    return;
                }

                // on mouseup, the event.button will be the button that was just released
                _buttons ^= getStandardizedButtonsProperty(event);

                // we only trigger a "pointerup" event if no buttons are down, prevent chorded PointerDown events
                if (_buttons === 0) {
                    triggerCustomEvent(this, "pointerup", event);
                } else {
                    // per the Pointer Events spec, when the active buttons change it fires only a PointerMove event
                    triggerCustomEvent(this, "pointermove", event);
                }

                // Mouse Events spec shows that after a "mouseup" it fires a "mousemove", which will trigger
                // the "pointermove" needed to follow the Pointer Events spec which describes the same thing
            },
            add: $.event.delegateSpecial(function(handleObj) {
                // bind to touch events, some devices (chromebook) can send both touch and mouse events
                if (support.touch) {
                    addEvent(this, "touchend", handleObj.selector, $.event.special.pointerup.touch);
                }

                // bind mouse events
                addEvent(this, "mouseup", handleObj.selector, $.event.special.pointerup.mouse);
            }),
            remove: $.event.delegateSpecial.remove(function(handleObj) {
                // unbind touch events
                if (support.touch) {
                    removeEvent(this, "touchend", handleObj.selector, $.event.special.pointerup.touch);
                }

                // unbind mouse events
                removeEvent(this, "mouseup", handleObj.selector, $.event.special.pointerup.mouse);
            })
        };

        $.event.special.pointermove = {
            touch: function(event) {
                triggerCustomEvent(this, "pointermove", event);
            },
            mouse: function(event) {
                // _touching will be a number if they currently have their finger (touch only) down
                // because we cannot call preventDefault on the "touchmove" without preventing
                // scrolling on most things, we do this check to ensure we don't double fire
                // move events.
                if (typeof _touching === "number") {
                    return;
                }

                triggerCustomEvent(this, "pointermove", event);
            },
            add: $.event.delegateSpecial(function(handleObj) {
                // bind to touch events, some devices (chromebook) can send both touch and mouse events
                if (support.touch) {
                    addEvent(this, "touchmove", handleObj.selector, $.event.special.pointermove.touch);
                }

                // bind mouse events
                addEvent(this, "mousemove", handleObj.selector, $.event.special.pointermove.mouse);
            }),
            remove: $.event.delegateSpecial.remove(function(handleObj) {
                // unbind touch events
                if (support.touch) {
                    removeEvent(this, "touchmove", handleObj.selector, $.event.special.pointermove.touch);
                }

                // unbind mouse events
                removeEvent(this, "mousemove", handleObj.selector, $.event.special.pointermove.mouse);
            })
        };

        jQuery.each({
            pointerover: {
                mouse: "mouseover"
            },
            pointerout: {
                mouse: "mouseout"
            },
            pointercancel: {
                touch: "touchcancel"
            }
        }, function(pointerEventType, natives) {
            function onTouch(event) {
                // pointercancel, reset everything
                if (event.type === "touchcancel") {
                    _touching = null;
                    _buttons = 0;
                }

                triggerCustomEvent(this, pointerEventType, event);
            }

            function onMouse(event) {
                triggerCustomEvent(this, pointerEventType, event);
            }

            $.event.special[pointerEventType] = {
                setup: function() {
                    if (support.touch && natives.touch) {
                        addEvent(this, natives.touch, null, onTouch);
                    }
                    if (natives.mouse) {
                        addEvent(this, natives.mouse, null, onMouse);
                    }
                },
                teardown: function() {
                    if (support.touch && natives.touch) {
                        removeEvent(this, natives.touch, null, onTouch);
                    }
                    if (natives.mouse) {
                        removeEvent(this, natives.mouse, null, onMouse);
                    }
                }
            };
        });
    }

    // for IE10 specific, we proxy though events so we do not need to deal
    // with the various names or renaming of events.
    else if (navigator.msPointerEnabled && !navigator.pointerEnabled) {
        $.extend($.event.special, {
            pointerdown: {
                delegateType: "MSPointerDown",
                bindType: "MSPointerDown"
            },
            pointerup: {
                delegateType: "MSPointerUp",
                bindType: "MSPointerUp"
            },
            pointermove: {
                delegateType: "MSPointerMove",
                bindType: "MSPointerMove"
            },
            pointerover: {
                delegateType: "MSPointerOver",
                bindType: "MSPointerOver"
            },
            pointerout: {
                delegateType: "MSPointerOut",
                bindType: "MSPointerOut"
            },
            pointercancel: {
                delegateType: "MSPointerCancel",
                bindType: "MSPointerCancel"
            }
        });

        $.extend($.event.fixHooks, {
            MSPointerDown: $.event.pointerHooks,
            MSPointerUp: $.event.pointerHooks,
            MSPointerMove: $.event.pointerHooks,
            MSPointerOver: $.event.pointerHooks,
            MSPointerOut: $.event.pointerHooks,
            MSPointerCancel: $.event.pointerHooks
        });
    }

    // add support for pointerenter and pointerlave
    // pointerenter and pointerleave were added in IE11, they do not exist in IE10
    if (!support.pointer || (navigator.msPointerEnabled && !navigator.pointerEnabled)) {
        // Create a wrapper similar to jQuery's mouseenter/leave events
        // using pointer events (pointerover/out) and event-time checks
        jQuery.each({
            pointerenter: navigator.msPointerEnabled ? "MSPointerOver" : "mouseover",
            pointerleave: navigator.msPointerEnabled ? "MSPointerOut" : "mouseout"
        }, function(orig, fix) {
            jQuery.event.special[orig] = {
                delegateType: fix,
                bindType: fix,
                handle: function(event) {
                    var ret,
                        target = this,
                        related = event.relatedTarget,
                        handleObj = event.handleObj;

                    // For mousenter/leave call the handler if related is outside the target.
                    // NB: No relatedTarget if the mouse left/entered the browser window
                    if (!related || (related !== target && !jQuery.contains(target, related))) {
                        event.type = handleObj.origType;
                        ret = handleObj.handler.apply(this, arguments);
                        event.type = fix;
                    }
                    return ret;
                }
            };
        });
    }

})(jQuery, window, document);
; /* globals Window */

(function(window, $) {
    var cacheBuster = 1;

    var browserSupportsPostMessage = !!window.postMessage;

    // Given a URL, returns the domain portion (i.e. http://www.somedomain.com)
    function getDomainFromUrl(url) {
        return url.replace(/([^:]+:\/\/[^\/]+).*/, "$1");
    }

    // Given a domain pattern (i.e. http://somedomain.com) matches against a specified domain

    // * {String or Function} originPatternOrFunction: A pattern or a function to match against sourceOrigin
    // * {String} sourceOrigin: The string to match using the originPatternOrFunction
    function isOriginMatch(originPatternOrFunction, sourceOrigin) {
        if (typeof (originPatternOrFunction) == "string" &&
            sourceOrigin !== originPatternOrFunction &&
            originPatternOrFunction !== "*") {
            return false;
        } else if ($.isFunction(originPatternOrFunction) && !originPatternOrFunction(sourceOrigin)) {
            return false;
        }

        return true;
    }

    // Try to find the relationship between the current window
    // and a provided window reference.

    // * {Window} window: Current window or window sending event.
    // * {Window} target: Target window
    // * {number} level: Do not pass originally. Used only by recursion.

    // Will return a short reference string or false if cannot be found.
    function transverseLevel(window, target, level) {
        var i;

        if (typeof level == "undefined") {
            level = 0;
        }

        // Try to find the target in window.frames
        if (window.frames) {
            try {
                for (i = 0; i < window.frames.length; i++) {
                    try {
                        if (window.frames[i] === target) {
                            return "f," + i;
                        }
                    } catch (e) {
                        if (e.number !== -2147024891) // WTF is this?
                        {
                            throw e;
                        }
                    }
                }
            } catch (ex) {
                if (ex.number !== -2146823279) // and, WTF is this?
                {
                    throw ex;
                }
            }
        }

        // Try to find the target in window.parent
        if (window.parent && window.parent === target) {
            return "p";
        }

        // Try to find the target in window.opener
        if (window.opener && window.opener === target) {
            return "o";
        }

        // Prevent infinite recursion. 
        // There's really no good reason you need 4 levels deep of frames!
        if (level >= 4) {
            return false;
        }

        var ref;

        // Recurse through window.frames
        if (window.frames && window.frames.length > 0) {
            for (i = 0; i < window.frames.length; i++) {
                ref = transverseLevel(window.frames[i], target, level + 1);
                if (ref) {
                    return "f," + i + "." + ref;
                }
            }
        }

        // Recurse through window.parent
        if (window.parent && window.parent !== window) {
            ref = transverseLevel(window.parent, target, level + 1);
            if (ref) {
                return "p." + ref;
            }
        }

        // Recurse through window.opener
        if (window.opener && window.opener !== window) {
            ref = transverseLevel(window.opener, target, level + 1);
            if (ref) {
                return "o" + ref;
            }
        }

        return false;
    }

    // 1. Find the relationship between current and target window.
    // 2. Serialize a string path from the current to the target window.
    // Example: f,0.f,0 translates to window.frames[0].frames[0]
    // Example: p.p translates to window.parent.parent

    // * {Window} currentWindow: Starting window
    // * {Window|string} targetWindow: Window to determine reference to.
    function serializeWindowReference(currentWindow, targetWindow) {
        // If the target window was opened with window.open(), its name is the only
        // way to get to it. This makes for a yucky API, unfortunately.
        if (typeof (targetWindow) == "string") {
            return ":" + targetWindow;
        }

        // first see if we can quickly find the reference
        if (currentWindow === targetWindow) {
            throw new Error("Trying to postMessage to self. Pointlessly useless.");
        }

        // see if the target is simple the parent
        if (currentWindow.parent && currentWindow.parent !== currentWindow && currentWindow.parent === targetWindow) {
            return "p";
        }

        // see if the target is simply the opener
        if (currentWindow.opener && currentWindow.opener !== currentWindow && currentWindow.opener === targetWindow) {
            return "o";
        }

        // Try to determine the relationship through recursion.
        var ref = transverseLevel(currentWindow, targetWindow);
        if (ref) {
            return ref;
        } else {
            throw new Error("Couldn't serialize window reference");
        }
    }

    // Sends a message to a window in a different domain.
    // * {String} message: The message to send
    // * {String} targetHost: The domain of the window to which the message should be sent
    //                               (i.e. http://www.something.com)
    // * {Window} targetWindow: A reference to the target window to which the message should be sent
    // * {string} targetWindowName: If the target window is a child window (not a frame), the window name
    //                               is required for browsers that don"t support postMessage() natively.
    $.postMessage = function(message, targetHost, targetWindow, /* optional */targetWindowName) {
        if (!targetHost) {
            throw new Error("targetHost argument was not supplied to jQuery.postMessage");
        }

        if (!targetWindow) {
            throw new Error("No targetWindow specified");
        }

        targetHost = getDomainFromUrl(targetHost);

        // native works for:

        // * Opera 12.12 (build 1707, x64, Win7)
        // * Chrome 24.0.1312.56 m (Win7)
        // * Firefox 18.0.1 (Win7)
        if (browserSupportsPostMessage) {
            try {
                targetWindow.postMessage(message, targetHost);
                return;
            } catch (ex) {
                // In IE (all known versions), postMessage() works only for iframes within the same
                // top-level window, and will fail with "No such interface supported" for calls between top-level windows.

                // * <http://blogs.msdn.com/b/ieinternals/archive/2009/09/16/bugs-in-ie8-support-for-html5-postmessage-sessionstorage-and-localstorage.aspx>
                // * <http://blogs.msdn.com/b/thebeebs/archive/2011/12/21/postmessage-popups-and-ie.aspx>

                // No such interface supported. Fall through to the polyfill technique.
                if (ex.number != -2147467262) {
                    throw ex;
                }
            }
        }

        // The browser does not support window.postMessage.
        // First, lets see if we can get direct access to the window instead.
        // This will only work if the target window is in the same domain.
        try {
            var postMessageDirect = targetWindow.__receiveMessageHook;
            if (postMessageDirect) {
                postMessageDirect(message, targetHost);
                return;
            }
        } catch (ex) { }

        // Direct access wont work because the targetWindow is in a different domain.
        // Create an iframe in the same domain as the target window and use it as a proxy to talk
        // to the target window. Pass the proxy information in an encoded URL fragment,
        // (not a querystring, which would cause it to load from the server)
        var serializedWindowRef = serializeWindowReference(window, targetWindowName || targetWindow),
            thisDomain = getDomainFromUrl(document.location.href),
            iframe = document.createElement("iframe");

        if (!targetHost || targetHost == "*") {
            throw new Error("$.postMessage(): Must specify targetHost on browsers that don't support postMessage natively (cannot be '*').");
        }

        $("body").append(
            $(iframe)
            .hide()
            .attr("src", targetHost + getPolyfillPath() + "#" +
        // When server side debugging, add (+new Date()) here
                (+new Date()) + cacheBuster + "&" +
                serializedWindowRef + "&" + thisDomain + "&" + encodeURIComponent(message)
            )
            .load(function() {
                // remove this DOM iframe once it is no longer needed
                $(iframe).remove();
            })
        );

        cacheBuster++;
    };

    // Assigns an event handler (callback) to receive messages sent to the window, from the specified origin.

    // * {function(Object)} callback: The event handler function to call when a message is received
    // * {string|function(string)} allowedOriginOrFunction: Either a domain string (i.e. http://www.something.com),
    //                                                     a wildcard (i.e. "*"), or a function that takes domain
    //                                                     strings and returns true or false.
    $.receiveMessage = function(callback, allowedOriginOrFunction) {
        if (!callback) {
            throw new Error("No callback function specified");
        }

        if (!allowedOriginOrFunction) {
            allowedOriginOrFunction = "*";
        }

        $(window).on("message", function(event, data, origin) {
            if (!data) {
                data = event.originalEvent ? event.originalEvent.data : event.data;
            }

            if (!origin) {
                origin = event.originalEvent ? event.originalEvent.origin : event.origin;
            }

            return isOriginMatch(allowedOriginOrFunction, event.originalEvent ? event.originalEvent.origin : origin) ?
                callback({
                    "data": data,
                    "origin": origin
                }) :
                false;
        });
    };

    // Windows in IE can only handle onmessage events from IFRAMEs within the same parent window only.
    // Messages sent between top level windows will fail. Unfortunately, we don't know if the calling window is
    // an IFrame or top-level window. To work around, listen for calls from the polyfill technique for IE in all cases.
    window.__receiveMessageHook = function(message, origin) {
        var $evt = new $.Event("message");
        $evt.data = message;
        $evt.origin = origin;

        $(window).trigger($evt, [$evt.data, $evt.origin]);
    };

    // Convenience wrapper for windows wrapped in jQuery objects
    $.fn.postMessage = function(message, targetHost, /* optional */targetWindowName) {
        this.each(function(i, el) {
            if (!(el instanceof Window)) {
                throw new Error("postMessage can only be sent to a window");
            }

            $.postMessage(message, targetHost, el, targetWindowName);
        });

        return this;
    };

    $.event.special.message = {
        add: function(handlerData) {
            var origHandler = handlerData.handler;

            handlerData.handler = function(e, message, origin) {
                e.data = e.originalEvent ? e.originalEvent.data : message;
                e.origin = e.originalEvent ? e.originalEvent.origin : origin;

                return origHandler.call(this, e, e.data, e.origin);
            };
        }
    };

    var getPolyfillPath = function() {
        if (!window._jqueryPostMessagePolyfillPath) {
            throw new Error("Must configure jquery.postMessage() with window._jqueryPostMessagePolyfillPath for IE7 support. Should be '/root-relative-path-on-my-server/postmessage.htm'");
        }

        return window._jqueryPostMessagePolyfillPath;
    };

})(window, jQuery);
