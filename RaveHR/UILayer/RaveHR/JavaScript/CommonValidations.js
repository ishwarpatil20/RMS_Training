/*JScript File
 
<FileHeader>
     <FileName>Validation.js</FileName>
         <Description>
            It will hold data related to field validation
         </Description>
         <Author>Kanchan</Author>
         <DateCreated date="13-Apr-2009"/>
         <RevisionHistory>
         <Revision date="" author="">
         Please add the revision history here.
         </Revision>
         </RevisionHistory>
</FileHeader>
*/

function ValidateSpecialCharacters() {
    if (event.keyCode == 46 || event.keyCode == 32 || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) {
        event.returnValue = true;

    }
    else {
        event.returnValue = false;
        alert("Please input alphanumeric value only");
    }

}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function onlyNumerics(no) {
    var strValidChars = "0123456789";
    var strChar;
    var blnResult = true;
    var i;

    for (i = 0; i < no.length && blnResult == true; i++) {
        strChar = no.charAt(i);

        if (strValidChars.indexOf(strChar) == -1) {
            blnResult = false;
        }
        else {
            blnResult = true;
        }
    }

    if (blnResult == false) {
        return false;
    }
    else {
        return true;
    }

}

/// <method>trim</method>
/// <author>Prashant Mala</author>
/// <CreatedDate>14th April 2009</CreatedDate>
/// <summary>Trim values</summary>
/// <param name="stringToTrim">String to trim</param>    
/// <returns>Void</returns>
function trim(stringToTrim)
{ return stringToTrim.replace(/^\s+|\s+$/g, ""); }

/// <method>Block</method>
/// <author>Prashant Mala</author>
/// <CreatedDate>14th April 2009</CreatedDate>
/// <summary>block values</summary>
/// <returns>bool</returns>
function Block()
{ return false; }

function SelectOne(rdoId, gridName) {
    var rdo = document.getElementById(rdoId);
    /* Getting an array of all the "INPUT" controls on the form.*/
    var all = document.getElementsByTagName("input");
    for (i = 0; i < all.length; i++) {
        /*Checking if it is a radio button, and also checking if the
        id of that radio button is different than "rdoId" */
        if (all[i].type == "radio" && all[i].id != rdo.id) {
            var count = all[i].id.indexOf(gridName);
            if (count != -1) {
                all[i].checked = false;
            }
        }
    }
    rdo.checked = true; /* Finally making the clicked radio button CHECKED */
}

//Start : Following code is added by kanchan to implement new javascript tab functionality

var mastertabvar = new Object()
mastertabvar.baseopacity = 0
mastertabvar.browserdetect = ""

function showsubmenu(masterid, id) {
    if (typeof highlighting != "undefined")
        clearInterval(highlighting)
    submenuobject = document.getElementById(id)
    mastertabvar.browserdetect = submenuobject.filters ? "ie" : typeof submenuobject.style.MozOpacity == "string" ? "mozilla" : ""
    hidesubmenus(mastertabvar[masterid])
    submenuobject.style.display = "block"
    instantset(mastertabvar.baseopacity)
    highlighting = setInterval("gradualfade(submenuobject)", 50)
}

function hidesubmenus(submenuarray) {
    for (var i = 0; i < submenuarray.length; i++)
        document.getElementById(submenuarray[i]).style.display = "none"
}

function instantset(degree) {
    if (mastertabvar.browserdetect == "mozilla")
        submenuobject.style.MozOpacity = degree / 100
    else if (mastertabvar.browserdetect == "ie")
        submenuobject.filters.alpha.opacity = degree
}


function gradualfade(cur2) {
    if (mastertabvar.browserdetect == "mozilla" && cur2.style.MozOpacity < 1)
        cur2.style.MozOpacity = Math.min(parseFloat(cur2.style.MozOpacity) + 0.1, 0.99)
    else if (mastertabvar.browserdetect == "ie" && cur2.filters.alpha.opacity < 100)
        cur2.filters.alpha.opacity += 10
    else if (typeof highlighting != "undefined") //fading animation over
        clearInterval(highlighting)
}

function initalizetab(tabid) {
    mastertabvar[tabid] = new Array()
    var menuitems = document.getElementById(tabid).getElementsByTagName("li")
    for (var i = 0; i < menuitems.length; i++) {
        if (menuitems[i].getAttribute("rel")) {
            menuitems[i].setAttribute("rev", tabid) //associate this submenu with main tab
            mastertabvar[tabid][mastertabvar[tabid].length] = menuitems[i].getAttribute("rel") //store ids of submenus of tab menu

            if (menuitems[i].className == "selected")
                showsubmenu(tabid, menuitems[i].getAttribute("rel"))
            menuitems[i].getElementsByTagName("a")[0].onmouseover = function() {
                showsubmenu(this.parentNode.getAttribute("rev"), this.parentNode.getAttribute("rel"))
            }
        }
    }
}


/// <method>trim</method>
/// <author>Vineet Kulkarni</author>
/// <CreatedDate>22th May 2009</CreatedDate>
/// <summary>Custom Paging Text Box Validation</summary>
/// <param name="stringToTrim">String to trim</param>    
/// <returns>Void</returns>
function onlyNumericsForCustomPaging(no) {
    var strValidChars = "123456789";
    var strChar;
    var blnResult = true;
    var i;

    for (i = 0; i < no.length && blnResult == true; i++) {
        strChar = no.charAt(i);

        if (strValidChars.indexOf(strChar) == -1) {
            blnResult = false;
        }
        else {
            blnResult = true;
        }
    }

    if (blnResult == false) {
        return false;
    }
    else {
        return true;
    }
}
//End

//Function to check LeapYear
function CheckLeapYear(date) {

    var date_array = date.split('/');
    if (date_array.length == 3) {
        var day = date_array[0];

        // Attention! Javascript consider months in the range 0 - 11
        var month = date_array[1] - 1;
        var year = date_array[2];

        if (year.length < 4) {
            return false;
        }

        // This instruction will create a date object
        source_date = new Date(year, month, day);



        if (year != source_date.getFullYear()) {
            return false;
        }

        if (month != source_date.getMonth()) {
            return false;
        }

        if (day != source_date.getDate()) {
            return false;
        }
        return true;
    }
    else {
        return false;
    }
}

//Validate date
function validateDate(date) {
    //var RegExPattern = /^(?=\d)(?:(?:(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})|(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2}))($|\ (?=\d)))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\ [AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;

    var RegExPattern = /^(((((0?[1-9])|(1\d)|(2[0-8]))\/((0?[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0?[1,3-9])|(1[0-2])))))\/((20[0-9][0-9]))|(29\/0?2\/20(([02468][048])|([13579][26]))))$/;
    //Returns "true" when fails
    if (!(date.match(RegExPattern)) && (date != '')) {
        if (CheckLeapYear(date))
            return false;

        return true;
    }


}

function validateText(text) {

    //var RegExPattern = new RegExp("^[a-zA-Z0-9]+(\s-.[a-zA-Z0-9]+)*$");
    //var RegExPattern = new RegExp("^[a-zA-Z0-9]+((\.|-|s)[a-zA-Z0-9]+)*$"); -- original one but accepted underscore
    var RegExPattern = new RegExp("^[a-zA-Z0-9]+([-. ][a-zA-Z0-9]+)*$");
    if (!(text.match(RegExPattern)) && (text != '')) {
        return true;
    }
}

function ddmmyyTommddyyConverter(date) {
    var tmpDate = date.split("/")
    return tmpDate[1] + "/" + tmpDate[0] + "/" + tmpDate[2];
}

/*************************************** Tooltip Start ***********************************************************/
function ShowTooltip(parentControlId, message) {
    // This is the control to which error image is attached (e.g. span, div, etc..)
    var parentControl = document.getElementById(parentControlId);
    parentControl.style.position = "relative";
    parentControl.style.fontSize = "10pt";
    //parentControl.style.zIndex = "50";

    // This is the main "DIV" which has callout image and tooltip "DIV"
    var mainDiv = document.createElement("div");
    mainDiv.style.position = "absolute";
    mainDiv.style.left = -20;
    mainDiv.style.top = 20;
    mainDiv.style.zIndex = "50";

    // This is the callout image
    var imgStem = document.createElement("img");
    imgStem.style.position = "absolute";
    imgStem.border = 0;
    imgStem.style.zIndex = "150";


    //This is the tooltip div
    var div = document.createElement("div");
    div.style.position = "absolute";
    div.style.border = "1px solid #000000";
    div.style.padding = "5px";
    div.style.zIndex = "100";
    div.style.backgroundColor = "#FFFFE1";
    div.style.width = "150px";

    div.innerText = message;

    //Check for page width and calaulate appropriate tooltip position
    if (((parentControl.offsetLeft + 150) > parentControl.offsetParent.clientWidth)
        && ((parentControl.offsetTop + 15) > parentControl.offsetParent.clientHeight)) {
        imgStem.src = "Images/StemBottomRight.gif";
        imgStem.style.left = 10;
        imgStem.style.top = -30;
        div.style.left = -110;
        div.style.top = -70;
    }
    else if (((parentControl.offsetLeft + 150) < parentControl.offsetParent.clientWidth)
        && ((parentControl.offsetTop + 15) > parentControl.offsetParent.clientHeight)) {
        imgStem.src = "Images/StemBottomLeft.gif";
        imgStem.style.left = 28;
        imgStem.style.top = -30;
        div.style.left = 0;
        div.style.top = -70;
    }
    else {
        div.style.top = 14;
        if ((parentControl.offsetLeft + 150) > parentControl.offsetParent.clientWidth) {
            imgStem.src = "Images/StemTopRight.gif";
            imgStem.style.left = 10;
            div.style.left = -110;
        }
        else {
            imgStem.src = "Images/StemTopLeft.gif";
            imgStem.style.left = 30;
            div.style.left = 0;
        }
    }

    //Add callout image
    mainDiv.appendChild(imgStem);
    //Add callout "DIV"
    mainDiv.appendChild(div);

    //alert(hi);

    parentControl.appendChild(mainDiv);
}

function HideTooltip(parentControlId) {
    // This is the image to which tooltip is attached
    var parentControl = document.getElementById(parentControlId);
    for (i = 0; i < parentControl.childNodes.length; i++) {
        if (parentControl.childNodes[i].tagName == "DIV") {
            //Remove tooltip "DIV"
            var mainDiv = parentControl.childNodes[i];
            parentControl.removeChild(mainDiv);
            parentControl.style.position = "static";
        }
    }
}


/*************************************** Tooltip End *************************************************************/

//Function will Validate the Control
function ValidateControl(controlobj, imgobj, functionName) {
    var bool;
    var controlName = document.getElementById(controlobj);
    if (imgobj != "") {
        var imgName = document.getElementById(imgobj);

        if (controlName.value == "") {
            imgName.style.display = "none";

            //Reset Control highlighting
            controlName.style.borderStyle = "Solid";
            controlName.style.borderWidth = "2";
            controlName.style.borderColor = "Red";
            // controlName.focus();
            bool = false;
        }
        else {
            if (window[functionName](controlName.value)) {
                imgName.style.display = "inline";

                //Highlight Control
                controlName.style.borderStyle = "Solid";
                controlName.style.borderWidth = "2";
                controlName.style.borderColor = "Red";
                controlName.value = "";
                bool = false;
            }
            else {
                controlName.style.borderWidth = "1";
                controlName.style.borderColor = "#7F9DB9";
                imgName.style.display = "none";
                imgName.alt = "";
                bool = true;
            }
        }
    }
    else {
        if (controlName.value == "") {
            //Reset Control highlighting
            controlName.style.borderStyle = "Solid";
            controlName.style.borderWidth = "2";
            controlName.style.borderColor = "Red";
            bool = false;
        }
        else {
            controlName.style.borderWidth = "1";
            controlName.style.borderColor = "#7F9DB9";
            bool = true;
        }

    }
    return bool;
}


//Function will Validate the Control
function ValidateBillingControl(controlobj, imgobj, functionName) {
    var bool;
    var controlName = document.getElementById(controlobj);
    if (imgobj != "") {
        var imgName = document.getElementById(imgobj);

        if (controlName.value == "") {
            imgName.style.display = "none";

            //Reset Control highlighting
            controlName.style.borderStyle = "Solid";
            controlName.style.borderWidth = "2";
            controlName.style.borderColor = "Red";
            // controlName.focus();
            bool = false;
        }
        else {
            if (controlName.value == "0") {
                controlName.style.borderWidth = "1";
                controlName.style.borderColor = "#7F9DB9";
                imgName.style.display = "none";
                imgName.alt = "";
                bool = true;
            }
            else {
                if (window[functionName](controlName.value)) {
                    imgName.style.display = "inline";

                    //Highlight Control
                    controlName.style.borderStyle = "Solid";
                    controlName.style.borderWidth = "2";
                    controlName.style.borderColor = "Red";
                    controlName.value = "";
                    bool = false;
                }
                else {
                    controlName.style.borderWidth = "1";
                    controlName.style.borderColor = "#7F9DB9";
                    imgName.style.display = "none";
                    imgName.alt = "";
                    bool = true;
                }
            }
        }
    }
    else {
        if (controlName.value == "") {
            //Reset Control highlighting
            controlName.style.borderStyle = "Solid";
            controlName.style.borderWidth = "2";
            controlName.style.borderColor = "Red";
            bool = false;
        }
        else {
            controlName.style.borderWidth = "1";
            controlName.style.borderColor = "#7F9DB9";
            bool = true;
        }

    }
    return bool;
}


//Function will Validate the Control on CLick Event
//Sanju:Issue Id 50201:Changed the logic for displaying error
function ValidateControlOnClickEvent(controlobj) {
    debugger;
    var flag = true;
    var splitchar;
    var arrycontrolobj = controlobj.split(',');
    for (var i = 0; i < arrycontrolobj.length; i++) {
        var controlName = document.getElementById(arrycontrolobj[i]);
        
          //Ishwar : Training Module : 09/04/2014 : Starts
        if (controlName != null) {
            //Ishwar : Training Module : 09/04/2014 : End


            splitchar = controlName.id.split("z");
            //(printvalue.value).replace(/^\s*|\s*$/g,'');
            //.replace(/^\s*|\s*$/g,'')
            var controlValue = controlName.value;
            if (controlName.value != "" && controlName.value != undefined) {
                controlValue = controlValue.trim();
            }
            if (controlValue == "") {
                controlName.style.borderStyle = "Solid";
                controlName.style.borderWidth = "1px";
                controlName.style.borderColor = "Red";
                flag = false;
            }

            else {
                if (controlName.id.indexOf('z') != -1) {


                    if (controlName.firstChild.nextSibling != null && controlName.firstChild.nextSibling.id != undefined) {
                        var dropDownId = document.getElementById(controlName.firstChild.nextSibling.id);
                        if (dropDownId.id.indexOf('ddl') != -1) {
                            dropDownId.style.borderStyle = "Solid";
                            dropDownId.style.borderWidth = "1px";
                            dropDownId.style.borderColor = "Red";
                            flag = false;
                        }
                    }
                    else {

                        if (controlName.id.match(splitchar[1])) {
                            //Highlight Control
                            //  controlName.firstChild.style.borderStyle = "Solid";
                            controlName.style.borderStyle = "Solid";
                            controlName.style.borderWidth = "1px";
                            controlName.style.borderColor = "Red";
                            flag = false;
                        }
                    }
                }
                else {
                    controlName.style.borderWidth = "1";
                    controlName.style.borderColor = "#7F9DB9";
                    if (flag != false) {
                        flag = true;
                    }
                }
            }
        }
    }
    return flag;
}
//Sanju:Issue Id 50201:End

//Function will Check that input text is numeric or not
function IsNumeric(text) {


    var txtval = parseInt(text);

    if (txtval == 0) {
        return true;
    }

    var RegExPattern = new RegExp("^[0-9]*$");
    if (!(RegExPattern.test(text)) && (text != '')) {
        return true;
    }
}
//Function will Check Alpha Numeric 
function IsAplhaNumeric(text) {

    var RegExPattern = new RegExp("^[a-zA-Z0-9]+([-. ][a-zA-Z0-9]+)*$");
    if (!(text.match(RegExPattern)) && (text != '')) {
        return true;
    }
}

//Function will Check Alphabet
function IsAlphabet(text) {
    var RegExPattern = new RegExp("^[a-zA-Z]*$");
    if (!(text.match(RegExPattern)) && (text != '')) {
        return true;
    }
}

// Function will check the max length of Multiline text box.
function MultiLineTextBox(controlobj, maxlength, imgobj,event) {

    var textbox = document.getElementById(controlobj);
    //Sanju:Issue Id 50201:Added condition for firefox events.which event(backspace and delete were not working)
    if (event != undefined) {
        var code = (event.keyCode) ? event.keyCode : event.which;
        if (code == 8 || code == 46)
            return true;
    }
    //Sanju:Issue Id 50201 End



    if (textbox != null) {
        var textlength = textbox.value.length;
        var maxlen = maxlength;
        textlength = textlength * 1;
        maxlen = maxlen * 1;
        if (parseInt(textlength) > parseInt(maxlen) - 1) {
            alert("Maximum " + maxlength + " characters allowed");
            textbox.focus()
            return false;
        }
    }
}


function ALPHANUMERIC_WITHSPACE(text) {

    var TextValue = text.trim();
    var regex = /^[0-9A-Za-z.,\s#-]+$/; //^[a-zA-z]+$/
    if (regex.test(TextValue)) {
        return false;
    }
    else {
        return true;
    }
}

function ALPHABET_WITHSPACE(text) {

    var TextValue = text.trim();
    var regex = /^[A-Za-z.,\s#-]+$/; //^[a-zA-z]+$/
    if (regex.test(TextValue)) {
        return false;
    }
    else {
        return true;
    }
}

//Function will Check Alphabet
function ALPHABET_WITHSPECIALCHAR(text) {
    if (text == '-') {
        return true;
    }

    var RegExPattern = new RegExp("^[a-zA-Z-]*$");
    if (!(text.match(RegExPattern)) && (text == '')) {
        return true;

    }
}

function Decimal(text) {

    var txt = parseInt(text);

    //if(txt == 0)
    //{
    //   return true;
    //}
    var TextValue = text.trim();

    var regex = /^\d+(?:\.\d{0,2})?$/;
    //var RegExPattern = new RegExp("^[0-9]+([.][0-9]+)*$");
    if (regex.test(TextValue)) {
        return false;
    }
    else {
        return true;
    }
}

function DecimalActualCTC(text) {

    var txt = parseInt(text);

    //if(txt == 0)
    //{
    //   return true;
    //}
    var TextValue = text.trim();

    var regex = /^\d+(?:\.\d{0,99})?$/;
    var regexComma = /^\d+\,\d+(?:\,\d{0,99})?$/;

    //var RegExPattern = new RegExp("^[0-9]+([.][0-9]+)*$");
    if (regex.test(TextValue)) {
        if (TextValue <= 0) {
            return true;
        }
        return false;
    }
    else {
        if (regexComma.test(TextValue)) {
            if (TextValue <= 0) {
                return true;
            }
            return false;
        }
        else {
            return true;
        }
    }
}

//Function will Check that input text is numeric
function IsBillingNumeric(text) {


    var txtval = parseInt(text);

    var RegExPattern = new RegExp("^[0-9]*$");
    if (!(RegExPattern.test(text)) && (text != '')) {
        return true;
    }
}

/* SOURCE FILE: date.js Added by Sunil Mishra*/

// HISTORY
// ------------------------------------------------------------------
// May 17, 2003: Fixed bug in parseDate() for dates <1970
// March 11, 2003: Added parseDate() function
// March 11, 2003: Added "NNN" formatting option. Doesn't match up
//                 perfectly with SimpleDateFormat formats, but 
//                 backwards-compatability was required.

// ------------------------------------------------------------------
// These functions use the same 'format' strings as the 
// java.text.SimpleDateFormat class, with minor exceptions.
// The format string consists of the following abbreviations:
// 
// Field        | Full Form          | Short Form
// -------------+--------------------+-----------------------
// Year         | yyyy (4 digits)    | yy (2 digits), y (2 or 4 digits)
// Month        | MMM (name or abbr.)| MM (2 digits), M (1 or 2 digits)
//              | NNN (abbr.)        |
// Day of Month | dd (2 digits)      | d (1 or 2 digits)
// Day of Week  | EE (name)          | E (abbr)
// Hour (1-12)  | hh (2 digits)      | h (1 or 2 digits)
// Hour (0-23)  | HH (2 digits)      | H (1 or 2 digits)
// Hour (0-11)  | KK (2 digits)      | K (1 or 2 digits)
// Hour (1-24)  | kk (2 digits)      | k (1 or 2 digits)
// Minute       | mm (2 digits)      | m (1 or 2 digits)
// Second       | ss (2 digits)      | s (1 or 2 digits)
// AM/PM        | a                  |
//
// NOTE THE DIFFERENCE BETWEEN MM and mm! Month=MM, not mm!
// Examples:
//  "MMM d, y" matches: January 01, 2000
//                      Dec 1, 1900
//                      Nov 20, 00
//  "M/d/yy"   matches: 01/20/00
//                      9/2/00
//  "MMM dd, yyyy hh:mm:ssa" matches: "January 01, 2000 12:30:45AM"
// ------------------------------------------------------------------

var MONTH_NAMES = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec');
var DAY_NAMES = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat');
function LZ(x) { return (x < 0 || x > 9 ? "" : "0") + x }

// ------------------------------------------------------------------
// isDate ( date_string, format_string )
// Returns true if date string matches format of format string and
// is a valid date. Else returns false.
// It is recommended that you trim whitespace around the value before
// passing it to this function, as whitespace is NOT ignored!
// ------------------------------------------------------------------
function isDate(val, format) {
    var date = getDateFromFormat(val, format);
    if (date == 0) { return false; }
    return true;
}

// -------------------------------------------------------------------
// compareDates(date1,date1format,date2,date2format)
//   Compare two date strings to see which is greater.
//   Returns:
//   1 if date1 is greater than date2
//   0 if date2 is greater than date1 of if they are the same
//  -1 if either of the dates is in an invalid format
// -------------------------------------------------------------------
function compareDates(date1, dateformat1, date2, dateformat2) {
    var d1 = getDateFromFormat(date1, dateformat1);
    var d2 = getDateFromFormat(date2, dateformat2);
    if (d1 == 0 || d2 == 0) {
        return -1;
    }
    else if (d1 > d2) {
        return 1;
    }
    return 0;
}

// ------------------------------------------------------------------
// formatDate (date_object, format)
// Returns a date in the output format specified.
// The format string uses the same abbreviations as in getDateFromFormat()
// ------------------------------------------------------------------
function formatDate(date, format) {
    format = format + "";
    var result = "";
    var i_format = 0;
    var c = "";
    var token = "";
    var y = date.getYear() + "";
    var M = date.getMonth() + 1;
    var d = date.getDate();
    var E = date.getDay();
    var H = date.getHours();
    var m = date.getMinutes();
    var s = date.getSeconds();
    var yyyy, yy, MMM, MM, dd, hh, h, mm, ss, ampm, HH, H, KK, K, kk, k;
    // Convert real date parts into formatted versions
    var value = new Object();
    if (y.length < 4) { y = "" + (y - 0 + 1900); }
    value["y"] = "" + y;
    value["yyyy"] = y;
    value["yy"] = y.substring(2, 4);
    value["M"] = M;
    value["MM"] = LZ(M);
    value["MMM"] = MONTH_NAMES[M - 1];
    value["NNN"] = MONTH_NAMES[M + 11];
    value["d"] = d;
    value["dd"] = LZ(d);
    value["E"] = DAY_NAMES[E + 7];
    value["EE"] = DAY_NAMES[E];
    value["H"] = H;
    value["HH"] = LZ(H);
    if (H == 0) { value["h"] = 12; }
    else if (H > 12) { value["h"] = H - 12; }
    else { value["h"] = H; }
    value["hh"] = LZ(value["h"]);
    if (H > 11) { value["K"] = H - 12; } else { value["K"] = H; }
    value["k"] = H + 1;
    value["KK"] = LZ(value["K"]);
    value["kk"] = LZ(value["k"]);
    if (H > 11) { value["a"] = "PM"; }
    else { value["a"] = "AM"; }
    value["m"] = m;
    value["mm"] = LZ(m);
    value["s"] = s;
    value["ss"] = LZ(s);
    while (i_format < format.length) {
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token += format.charAt(i_format++);
        }
        if (value[token] != null) { result = result + value[token]; }
        else { result = result + token; }
    }
    return result;
}

// ------------------------------------------------------------------
// Utility functions for parsing in getDateFromFormat()
// ------------------------------------------------------------------
function _isInteger(val) {
    var digits = "1234567890";
    for (var i = 0; i < val.length; i++) {
        if (digits.indexOf(val.charAt(i)) == -1) { return false; }
    }
    return true;
}
function _getInt(str, i, minlength, maxlength) {
    for (var x = maxlength; x >= minlength; x--) {
        var token = str.substring(i, i + x);
        if (token.length < minlength) { return null; }
        if (_isInteger(token)) { return token; }
    }
    return null;
}

// ------------------------------------------------------------------
// getDateFromFormat( date_string , format_string )
//
// This function takes a date string and a format string. It matches
// If the date string matches the format string, it returns the 
// getTime() of the date. If it does not match, it returns 0.
// ------------------------------------------------------------------
function getDateFromFormat(val, format) {
    val = val + "";
    format = format + "";
    var i_val = 0;
    var i_format = 0;
    var c = "";
    var token = "";
    var token2 = "";
    var x, y;
    var now = new Date();
    var year = now.getYear();
    var month = now.getMonth() + 1;
    var date = 1;
    var hh = now.getHours();
    var mm = now.getMinutes();
    var ss = now.getSeconds();
    var ampm = "";

    while (i_format < format.length) {
        // Get next token from format string
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token += format.charAt(i_format++);
        }
        // Extract contents of value based on format token
        if (token == "yyyy" || token == "yy" || token == "y") {
            if (token == "yyyy") { x = 4; y = 4; }
            if (token == "yy") { x = 2; y = 2; }
            if (token == "y") { x = 2; y = 4; }
            year = _getInt(val, i_val, x, y);
            if (year == null) { return 0; }
            i_val += year.length;
            if (year.length == 2) {
                if (year > 70) { year = 1900 + (year - 0); }
                else { year = 2000 + (year - 0); }
            }
        }
        else if (token == "MMM" || token == "NNN") {
            month = 0;
            for (var i = 0; i < MONTH_NAMES.length; i++) {
                var month_name = MONTH_NAMES[i];
                if (val.substring(i_val, i_val + month_name.length).toLowerCase() == month_name.toLowerCase()) {
                    if (token == "MMM" || (token == "NNN" && i > 11)) {
                        month = i + 1;
                        if (month > 12) { month -= 12; }
                        i_val += month_name.length;
                        break;
                    }
                }
            }
            if ((month < 1) || (month > 12)) { return 0; }
        }
        else if (token == "EE" || token == "E") {
            for (var i = 0; i < DAY_NAMES.length; i++) {
                var day_name = DAY_NAMES[i];
                if (val.substring(i_val, i_val + day_name.length).toLowerCase() == day_name.toLowerCase()) {
                    i_val += day_name.length;
                    break;
                }
            }
        }
        else if (token == "MM" || token == "M") {
            month = _getInt(val, i_val, token.length, 2);
            if (month == null || (month < 1) || (month > 12)) { return 0; }
            i_val += month.length;
        }
        else if (token == "dd" || token == "d") {
            date = _getInt(val, i_val, token.length, 2);
            if (date == null || (date < 1) || (date > 31)) { return 0; }
            i_val += date.length;
        }
        else if (token == "hh" || token == "h") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 12)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "HH" || token == "H") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 23)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "KK" || token == "K") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 11)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "kk" || token == "k") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 24)) { return 0; }
            i_val += hh.length; hh--;
        }
        else if (token == "mm" || token == "m") {
            mm = _getInt(val, i_val, token.length, 2);
            if (mm == null || (mm < 0) || (mm > 59)) { return 0; }
            i_val += mm.length;
        }
        else if (token == "ss" || token == "s") {
            ss = _getInt(val, i_val, token.length, 2);
            if (ss == null || (ss < 0) || (ss > 59)) { return 0; }
            i_val += ss.length;
        }
        else if (token == "a") {
            if (val.substring(i_val, i_val + 2).toLowerCase() == "am") { ampm = "AM"; }
            else if (val.substring(i_val, i_val + 2).toLowerCase() == "pm") { ampm = "PM"; }
            else { return 0; }
            i_val += 2;
        }
        else {
            if (val.substring(i_val, i_val + token.length) != token) { return 0; }
            else { i_val += token.length; }
        }
    }
    // If there are any trailing characters left in the value, it doesn't match
    if (i_val != val.length) { return 0; }
    // Is date valid for month?
    if (month == 2) {
        // Check for leap year
        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) { // leap year
            if (date > 29) { return 0; }
        }
        else { if (date > 28) { return 0; } }
    }
    if ((month == 4) || (month == 6) || (month == 9) || (month == 11)) {
        if (date > 30) { return 0; }
    }
    // Correct hours value
    if (hh < 12 && ampm == "PM") { hh = hh - 0 + 12; }
    else if (hh > 11 && ampm == "AM") { hh -= 12; }
    var newdate = new Date(year, month - 1, date, hh, mm, ss);
    return newdate.getTime();
}

// ------------------------------------------------------------------
// parseDate( date_string [, prefer_euro_format] )
//
// This function takes a date string and tries to match it to a
// number of possible date formats to get the value. It will try to
// match against the following international formats, in this order:
// y-M-d   MMM d, y   MMM d,y   y-MMM-d   d-MMM-y  MMM d
// M/d/y   M-d-y      M.d.y     MMM-d     M/d      M-d
// d/M/y   d-M-y      d.M.y     d-MMM     d/M      d-M
// A second argument may be passed to instruct the method to search
// for formats like d/M/y (european format) before M/d/y (American).
// Returns a Date object or null if no patterns match.
// ------------------------------------------------------------------
function parseDate(val) {
    var preferEuro = (arguments.length == 2) ? arguments[1] : false;
    generalFormats = new Array('y-M-d', 'MMM d, y', 'MMM d,y', 'y-MMM-d', 'd-MMM-y', 'MMM d');
    monthFirst = new Array('M/d/y', 'M-d-y', 'M.d.y', 'MMM-d', 'M/d', 'M-d');
    dateFirst = new Array('d/M/y', 'd-M-y', 'd.M.y', 'd-MMM', 'd/M', 'd-M');
    var checkList = new Array('generalFormats', preferEuro ? 'dateFirst' : 'monthFirst', preferEuro ? 'monthFirst' : 'dateFirst');
    var d = null;
    for (var i = 0; i < checkList.length; i++) {
        var l = window[checkList[i]];
        for (var j = 0; j < l.length; j++) {
            d = getDateFromFormat(val, l[j]);
            if (d != 0) { return new Date(d); }
        }
    }
    return null;
}

function scroll(seed) {
var m1 = "Welcome to the Resource Management System ";
var msg=m1;
var out = " ";
var c = 1;
if (seed > 100) {
seed--;
cmd="scroll("+seed+")";
timerTwo=window.setTimeout(cmd,100);
}
else if (seed <= 100 && seed > 0) {
for (c=0 ; c < seed ; c++) {
out+=" ";
}
out+=msg;
seed--;
window.status=out;
cmd="scroll("+seed+")";
timerTwo=window.setTimeout(cmd,100);
}
else if (seed <= 0) {
if (-seed < msg.length) {
out+=msg.substring(-seed,msg.length);
seed--;
window.status=out;
cmd="scroll("+seed+")";
timerTwo=window.setTimeout(cmd,100);
}
else {
window.status=" ";
timerTwo=window.setTimeout("scroll(100)",75);
}
}
}

//Sanju:Issue Id 50201 Start (Chrome issue)
//Added the below script so that dropdown that are dynamically loaded using ajax does not throw error(Uncaught Sys.ScriptLoadFailedException)
//Added check for chrome browser
var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
if (is_chrome) {
    Sys.Browser.WebKit = {};
    if (navigator.userAgent.indexOf('WebKit/') > -1) {
        Sys.Browser.agent = Sys.Browser.WebKit;
        Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
        Sys.Browser.name = 'WebKit';
    }
}
//Sanju:Issue Id 50201 End





