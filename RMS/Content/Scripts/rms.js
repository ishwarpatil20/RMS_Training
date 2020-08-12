
function hidewaiting() { $("#wait").hide(); }

function showaiting() { $("#wait").show(); }

//For Reset all controls in form
function ClearBorderColor() {

    var isValid = true;

    //Validation for Dropdownlist
    $('select.required').each(function () {
        $(this).removeClass("requiredHiglight");
    });

    $('input[type="text"].required').each(function () {
        $(this).removeClass("requiredHiglight");
    });

    //Validation for TextArea
    $('textarea.required').each(function () {
        $(this).removeClass("requiredHiglight");
    });

    $('input[type="checkbox"].required').each(function () {
        $(this).removeClass("requiredHiglight");
    });
        

    if (isValid == false) {
        return isValid;
        //e.preventDefault();
    }
}


function Validation() {
    debugger;
    ClearBorderColor();
    var isValid = true;

    //Validation for Dropdownlist
    $('select.required').each(function () {
        if ($.trim($(this).val()) == "" || $.trim($(this).val()) == "0") {
            debugger;
            isValid = false;
            $(this).addClass("requiredHiglight");
        }
        else {
            $(this).removeClass("requiredHiglight");
        }
    });

    //Validation for TextBox
    $('input[type="text"].required').each(function () {
        if ($.trim($(this).val()) == '') {
            isValid = false;
            $(this).addClass("requiredHiglight");
        }
        else {
            $(this).removeClass("requiredHiglight");
        }
    });

    $('input[type="checkbox"].required').each(function () {
        if ($.trim($(this).val()) == "" || $.trim($(this).val()) == "0") {
            isValid = false;
            $(this).addClass("requiredHiglight");
        }
        else {
            $(this).removeClass("requiredHiglight");
        }
    });

    //Validation for TextArea
    $('textarea.required').each(function () {
        if ($.trim($(this).val()) == '') {
            isValid = false;
            $(this).addClass("requiredHiglight");
        }
        else {
            $(this).removeClass("requiredHiglight");
        }
    });

    if (isValid == false) {
        $("#message").addClass("MessageStyleFail");
        $('#message').html("Please fill all mandatory fields.");
        return isValid;
        //e.preventDefault();
    }
    return isValid;
    //else
    //alert('Thank you for submitting');
}

function clearvalidation(object){   
    $(object).removeClass("requiredHiglight");
}
//Poonam : 26/04/2016 Starts
//Issue : Sorting not working on start date, end date and last date of nomination on View Course page

function DatatableSorting() {

    jQuery.extend(jQuery.fn.dataTableExt.oSort, {
        "date-uk-pre": function (a) {
            var ukDatea = a.split('/');
            return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
        },

        "date-uk-asc": function (a, b) {
            return ((a < b) ? -1 : ((a > b) ? 1 : 0));
        },

        "date-uk-desc": function (a, b) {
            return ((a < b) ? 1 : ((a > b) ? -1 : 0));
        },

    });
}
//Poonam : 26/04/2016 Ends

function IsValidUrl(userInput) {
    
    var res = userInput.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);
    if (res == null)
        return false;
    else
        return true;
}

