<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddProject.aspx.cs" Inherits="AddProject" Title="Add Project" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript" src="JavaScript/CommonValidations.js"></script>

    <script type="text/javascript">


        setbgToTab('ctl00_tabProject', 'ctl00_divProjectSummary');
        var isSubmitClicked = false;

        function $(v) { return document.getElementById(v); }

        function Validate() {
            debugger;
            var textboxesAreValid = true;
            var dropdownsAreValid = true;
            var fieldFormatsAreValid = true;
            var checkProjectStatus = $('<%=ddlStatus.ClientID %>').id;

            isSubmitClicked = true;

            //Check Mandatory Textbox fields
            var controlIds = document.getElementById("<%=txtProjectName.ClientID %>").id;

            controlIds = controlIds + "," + document.getElementById("<%=ucDatePickerStartDate.ClientID %>").id;
            controlIds = controlIds + "," + document.getElementById("<%=ucDatePickerEndDate.ClientID %>").id;

            controlIds = controlIds + "," + document.getElementById("<%=txtDescription.ClientID %>").id;
            controlIds = controlIds + "," + document.getElementById("<%=txtClientName.ClientID %>").id;

            if (document.getElementById("<%=txtDeletion.ClientID %>") != null)
                controlIds = controlIds + "," + document.getElementById("<%=txtDeletion.ClientID %>").id;

            
            textboxesAreValid = CheckRequiredTextboxFields(controlIds);

            //Check mandatory Dropdown fields

            //NIS DropDowns
            // Mohamed : Issue  : 23/09/2014 : Starts
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
            

            var ddlControlIds = "";
            if (document.getElementById("<%=ddlPrjDivision.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlPrjDivision.ClientID %>").id;


            if (document.getElementById("<%=ddlProjecBusinessArea.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlProjecBusinessArea.ClientID %>").id;



            if (document.getElementById("<%=ddlProjectBusinessSegment.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlProjectBusinessSegment.ClientID %>").id;


            if (document.getElementById("<%=ddlStatus.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlStatus.ClientID %>").id;

            //if(checkProjectStatus.options[checkProjectStatus.selectedIndex].Text == "delivery")
            //{   
            if (document.getElementById("<%=ddlStandardHours.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlStandardHours.ClientID %>").id;
            //}   

            if (document.getElementById("<%=ddlProjectGroup.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlProjectGroup.ClientID %>").id;

            if (document.getElementById("<%=ddlOnGoingProjectStatus.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlOnGoingProjectStatus.ClientID %>").id;

            //Siddharth 12th March 2015 Start
            if (document.getElementById("<%=ddlProjectModel.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlProjectModel.ClientID %>").id;
            //Siddharth 12th March 2015 End

            //Rakesh : Actual vs Budget 20/06/2016 Begin

            if (document.getElementById("<%=ddlProjectHead.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlProjectHead.ClientID %>").id;
            //Rakesh : Actual vs Budget 20/06/2016 End

            //if(checkProjectStatus.value == "delivery")
            var spanControlIds = ",spanProjectDivision,spanBusinessArea,spanBusinessSegment,spanStatus,spanStandardHours,spanProjectGroup,spanOnGoingProjectStatus,SpanProjectModel,SpanProjectHead";
            //else
            // var spanControlIds = "spanStatus,spanProjectGroup";

            dropdownsAreValid = CheckRequiredDropdownFields(ddlControlIds, spanControlIds, textboxesAreValid);

            //Check for field format errors
            //var errorImgIDs = "imgProjectName,imgStartDate,imgEndDate,imgClientName,imgDeletion";            
            //fieldFormatsAreValid = HighlightIncorrectFormatFields(errorImgIDs);

            if (textboxesAreValid && dropdownsAreValid && fieldFormatsAreValid) {
                //return true;
            }
            else {
                var lblMandatory = document.getElementById("<%=lblMandatory.ClientID %>");
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";

                return false;
            }

            //49176 Ishwar Start
            var CntrEndDt = document.getElementById("<%=hidContractEndDt.ClientID %>").value;
            var ProEndDt = document.getElementById("<%=ucDatePickerEndDate.ClientID %>").value;

            var ContractEndDate = new Date(ddmmyyTommddyyConverter(trim(CntrEndDt)));
            var ProjectEndDate = new Date(ddmmyyTommddyyConverter(trim(ProEndDt)));

            if (ContractEndDate < ProjectEndDate) {
                //var lblMandatory = document.getElementById("<%=lblMandatory.ClientID %>");
                lblMandatory.innerText = "Project end date (" + ProEndDt + ") is greater than contract end date (" + CntrEndDt + ").";
                lblMandatory.style.color = "Red";
                return false;
            }
            return true;
            //49176 Ishwar End
        }

        function CheckRequiredTextboxFields(controlIDs) {
            var allFieldsAreValid = true;
            var hasFocus = false;

            //Split textbox IDs.
            var controlIDArray = controlIDs.split(',');
            for (i = 0; i < controlIDArray.length; i++) {
                var control = document.getElementById(controlIDArray[i]);
                if (trim(control.value) == "") {
                    control.style.borderStyle = "Solid";
                    control.style.borderWidth = "2";
                    control.style.borderColor = "Red";
                    //Set focus to 1st error field
                    if (!hasFocus) {
                        control.focus();
                        hasFocus = true;
                    }

                    allFieldsAreValid = false;
                }
                else {
                    control.style.borderWidth = "1";
                    control.style.borderColor = "#7F9DB9";
                }
            }

            return allFieldsAreValid;
            alert(allFieldsAreValid);
        }

        function CheckRequiredDropdownFields(ddlControlIDs, spanControlIDs, textboxesAreValid) {
            var allFieldsAreValid = true;
            var hasFocus = false;

            //Split dropdown IDs
            var controlIDArray = ddlControlIDs.split(',');
            //Split <span> IDs
            var spanIDArray = spanControlIDs.split(',');
            
            for (i = 0; i < controlIDArray.length; i++) {
                var control = document.getElementById(controlIDArray[i]);
                if (control != null) {
                    var span = document.getElementById(spanIDArray[i]);
                    if (control.id.match("ddlStatus") == "ddlStatus") {
                        //When 'Project Status is other than Closed then highlight all dependent fields                    
                        var status = control.options[control.selectedIndex].text;
                        if (status != "Closed") {
                            //allFieldsAreValid = HighlightDependentFields(); 

                            //Check rows in gridview
                            var gridTechnology = document.getElementById('<%=gvCategoryTechnology.ClientID %>');
                            var gridDomain = document.getElementById('<%=gvDomainSubDomain.ClientID %>');
                            var txtProjectManager = document.getElementById('<%=txtProjectManager.ClientID %>');
                            var ddlStandardHours = document.getElementById("<%=ddlStandardHours.ClientID %>");
                            var lbCategorylError = document.getElementById('ctl00_cphMainContent_lbCategorylError');
                            //Siddharth 12th March 2015 Start
                            var ddlProjectModel = document.getElementById("<%=ddlProjectModel.ClientID %>");
                            //Siddharth 12th March 2015 Start
                            allFieldsAreValid = HighlightDependentFields(ddlStandardHours.id, 'spanStandardHours');


                            // 36234-Ambar-01082012-Start
                            // Commented out following to remove validation for Technology and Domain Grid
                            /*
                            if (gridTechnology == null) {
                            var ddlControlIds = document.getElementById("<%=ddlCategory.ClientID %>").id;
                            ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlTechnology.ClientID %>").id;
                            var spanControlIds = "spanCategory,spanTechnology";

                                HighlightDependentFields(ddlControlIds, spanControlIds);

                                //var lbCategorylError = document.getElementById('<%=lbCategorylError.ClientID %>');
                            lbCategorylError.innerText = "Please add Technology Detail.";

                                allFieldsAreValid = false;

                                if (status == "Pre-Sales") {
                            ResetDependentFieldsStyle();
                            lbCategorylError.innerText = "";
                            allFieldsAreValid = true;
                            var lblTechDetails = document.getElementById('lblTechDetails');
                            lblTechDetails.style.display = "none";
                            }
                            }


                            if (gridDomain == null) {
                            var ddlControlIds = document.getElementById("<%=ddlDomain.ClientID %>").id;
                            //ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlSubDomain.ClientID %>").id;
                            //var spanControlIds = "spanDomain,spanSubDomain";
                            var spanControlIds = "spanDomain";
                            HighlightDependentFields(ddlControlIds, spanControlIds);

                                var lblDomainError = document.getElementById('<%=lblDomainError.ClientID %>');
                            lblDomainError.innerText = "Please add Domain Detail.";

                                allFieldsAreValid = false;
                            }
                            */
                            // 36234-Ambar-01082012-End


                            //                           if(txtProjectManager.value == "")
                            //                           {
                            //                               txtProjectManager.style.borderStyle = "Solid";
                            //                               txtProjectManager.style.borderWidth = "2";
                            //                               txtProjectManager.style.borderColor = "Red";
                            //                                
                            //                               allFieldsAreValid = false;
                            //                           }
                        }
                        else {
                            ResetDependentFieldsStyle();
                        }
                    }

                    //Highlight other fields independent of 'Project Status' dropdown
                    if ((control.value == "Select" || control.value == "SELECT") && !(control.disabled)) {
                        span.style.borderStyle = "Solid";
                        span.style.borderWidth = "2";
                        span.style.borderColor = "Red";

                        //Set focus to 1st error field
                        if (!hasFocus && textboxesAreValid) {
                            control.focus();
                            hasFocus = true;
                        }

                        allFieldsAreValid = false;
                    }
                    else {
                        span.style.borderWidth = "0";
                    }
                }
            }

            return allFieldsAreValid;
        }

        function HighlightDependentFields(ddlControlIds, spanControlIds) {

            var allFieldsAreValid = true;
            var hasFocus = false;

            //            var ddlControlIds = document.getElementById("<%=ddlCategory.ClientID %>").id;
            //            ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlTechnology.ClientID %>").id;
            //            ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlDomain.ClientID %>").id;
            //            ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlSubDomain.ClientID %>").id;
            //            
            //            var spanControlIds = "spanCategory,spanTechnology,spanDomain,spanSubDomain";

            //CheckRequiredDropdownFields(ddlControlIds, spanControlIds);

            var controlIDArray = ddlControlIds.split(',');
            var spanIDArray = spanControlIds.split(',');

            for (j = 0; j < controlIDArray.length; j++) {
                var control = document.getElementById(controlIDArray[j]);
                var span = document.getElementById(spanIDArray[j]);
                if (control.value == "Select" && !(control.disabled)) {
                    span.style.borderStyle = "Solid";
                    span.style.borderWidth = "2";
                    span.style.borderColor = "Red";

                    //Set focus to 1st error field
                    if (!hasFocus) {
                        control.focus();
                        hasFocus = true;
                    }

                    allFieldsAreValid = false;
                }
                else {
                    span.style.borderWidth = "0";
                }
            }

            //Highlight 'Project Manager' textbox
            //            var txtProjectManager = document.getElementById('<%=txtProjectManager.ClientID %>');
            //            if(txtProjectManager.value == "")
            //            {
            //                txtProjectManager.style.borderStyle = "Solid";
            //                txtProjectManager.style.borderWidth = "2";
            //                txtProjectManager.style.borderColor = "Red";
            //                
            //                allFieldsAreValid = false;
            //            }
            //            else
            //            {
            //                txtProjectManager.style.borderWidth = "1";
            //                txtProjectManager.style.borderColor = "#7F9DB9";
            //            }

            return allFieldsAreValid;
        }

        function ResetDependentFieldsStyle() {
            //var spanControlIds = "spanCategory,spanTechnology,spanDomain,spanSubDomain";
            var spanControlIds = "spanCategory,spanTechnology,spanDomain";
            var spanIDArray = spanControlIds.split(',');

            for (j = 0; j < spanIDArray.length; j++) {
                var span = document.getElementById(spanIDArray[j]);
                span.style.borderWidth = "0";
            }

            var txtProjectManager = document.getElementById('<%=txtProjectManager.ClientID %>');
            txtProjectManager.style.borderWidth = "1";
            txtProjectManager.style.borderColor = "#7F9DB9";
        }

        function HighlightIncorrectFormatFields(imageIds) {
            var fieldFormatsAreValid = true;
            var txtProjectName = document.getElementById("<%=txtProjectName.ClientID %>");


            var txtStartDate = document.getElementById("<%=ucDatePickerStartDate.ClientID %>");
            var txtEndDate = document.getElementById("<%=ucDatePickerEndDate.ClientID %>");

            var txtClientName = document.getElementById("<%=txtClientName.ClientID %>");

            //Split error image IDs.
            var errorImgIDArray = imageIds.split(',');
            for (i = 0; i < errorImgIDArray.length - 1; i++) {
                var img = document.getElementById(errorImgIDArray[i]);
                if (img.style.display == "inline") {
                    //Highlight 'ProjectName' textbox
                    if (img.id.match("ProjectName")) {
                        txtProjectName.style.borderStyle = "Solid";
                        txtProjectName.style.borderWidth = "2";
                        txtProjectName.style.borderColor = "Red";
                    }
                    //Highlight 'StartDate' textbox
                    if (img.id.match("StartDate")) {
                        txtStartDate.style.borderStyle = "Solid";
                        txtStartDate.style.borderWidth = "2";
                        txtStartDate.style.borderColor = "Red";
                    }
                    //Highlight 'EndDate' textbox
                    if (img.id.match("EndDate")) {
                        txtEndDate.style.borderStyle = "Solid";
                        txtEndDate.style.borderWidth = "2";
                        txtEndDate.style.borderColor = "Red";
                    }
                    //Highlight 'ClientName' textbox
                    if (img.id.match("ClientName")) {
                        txtClientName.style.borderStyle = "Solid";
                        txtClientName.style.borderWidth = "2";
                        txtClientName.style.borderColor = "Red";
                    }

                    fieldFormatsAreValid = false;
                }
            }

            return fieldFormatsAreValid;
        }


        function SelectPM() {
            var txtProjectManagerID = document.getElementById("<%=txtProjectManager.ClientID %>").id;
            var hidProjectManagerID = document.getElementById("<%=hidProjectManagerId.ClientID %>").id;

            window.open('ProjectManager.aspx?field=' + txtProjectManagerID + '&fieldres=' + hidProjectManagerID + '', 'ProjectManagerPopup', 'titlebar=no,left=470,top=100,width=500,height=500,resizable=yes');

            //window.open('ProjectManager.aspx?field=' + strProjectManager +'','ProjectManagerPopup','titlebar=no,left=470,top=100,width=500,height=500,resizable=yes');                                         
        }

        function ClearPM() {
            var txtProjectManager = document.getElementById("<%=txtProjectManager.ClientID %>");
            var hidProjectManager = document.getElementById("<%=hidProjectManagerId.ClientID %>");
            var hidUpdateProjectManager = document.getElementById("<%=hidUpdateProjectManagerID.ClientID %>");
            var imgClearPM = document.getElementById("<%=imgClearPM.ClientID %>");

            txtProjectManager.value = "";
            hidProjectManager.value = "";
            hidUpdateProjectManager.value = "";
            imgClearPM.style.display = "none";
        }

        function ShowClearPMImage() {
            var txtProjectManager = document.getElementById("<%=txtProjectManager.ClientID %>");
            var imgClearPM = document.getElementById("<%=imgClearPM.ClientID %>");
            if (txtProjectManager.value != "")
                imgClearPM.style.display = "inline";
            else
                imgClearPM.style.display = "none";
        }


        function ValidateProjectName(sender, args) {
            var txtProjectName = document.getElementById("<%=txtProjectName.ClientID %>");
            var imgProjectName = document.getElementById('imgProjectName');

            if (txtProjectName.value == "") {
                imgProjectName.style.display = "none";

                //Reset Textbox highlighting
                txtProjectName.style.borderWidth = "1";
                txtProjectName.style.borderColor = "#7F9DB9";

                args.IsValid = false;
                return false;
            }
            else {
                if (validateText(txtProjectName.value)) {
                    imgProjectName.style.display = "inline";

                    //Highlight Textbox
                    txtProjectName.style.borderStyle = "Solid";
                    txtProjectName.style.borderWidth = "2";
                    txtProjectName.style.borderColor = "Red";

                    args.IsValid = false;
                    return false;
                }
                else {
                    //Reset Textbox highlighting
                    if (!isSubmitClicked) {
                        txtProjectName.style.borderWidth = "1";
                        txtProjectName.style.borderColor = "#7F9DB9";
                    }

                    imgProjectName.style.display = "none";
                    imgProjectName.alt = "";

                    args.IsValid = true;
                    return true;
                }
            }
        }

       function ValidateClientName(sender, args) {
            var txtClientName = document.getElementById("<%=txtClientName.ClientID %>");
            var imgClientName = document.getElementById('imgClientName');

            if (txtClientName.value == "") {
                imgClientName.style.display = "none";

                //Reset Textbox highlighting
                txtClientName.style.borderWidth = "1";
                txtClientName.style.borderColor = "#7F9DB9";

                args.IsValid = false;
                return false;
            }
            else {
                if (validateText(txtClientName.value)) {
                    imgClientName.style.display = "inline";
                    //imgClientName.alt = "Client Name is not in correct format.";

                    //Highlight Textbox
                    txtClientName.style.borderStyle = "Solid";
                    txtClientName.style.borderWidth = "2";
                    txtClientName.style.borderColor = "Red";

                    args.IsValid = false;
                    return false;
                }
                else {
                    //Reset Textbox highlighting
                    if (!isSubmitClicked) {
                        txtClientName.style.borderWidth = "1";
                        txtClientName.style.borderColor = "#7F9DB9";
                    }

                    imgClientName.style.display = "none";
                    imgClientName.alt = "";

                    args.IsValid = true;
                    return true;
                }
            }
        }

        function CheckDates(sender, args) {
            var dateIsValid = true;
            var imgStartDate = document.getElementById('imgStartDate');
            var imgEndDate = document.getElementById('imgEndDate');



            var txtStartDate = document.getElementById('<%=ucDatePickerStartDate.ClientID %>');
            var txtEndDate = document.getElementById('<%=ucDatePickerEndDate.ClientID %>');



            var SDate = document.getElementById('<%=ucDatePickerStartDate.ClientID %>').value;
            var EDate = document.getElementById('<%=ucDatePickerEndDate.ClientID %>').value;

            var endDate = new Date(ddmmyyTommddyyConverter(trim(EDate)));
            var startDate = new Date(ddmmyyTommddyyConverter(trim(SDate)));

            //Check Start Date and End Date range
            if (SDate != '' && EDate != '' && startDate >= endDate) {
                imgStartDate.style.display = "inline";
                //imgStartDate.alt = "End Date must be greater than start date.";
                imgStartDate.onmouseover = function() { ShowTooltip('spanStartDateTooltip', 'End Date must be greater than Start date.'); };
                imgStartDate.onmouseout = function() { HideTooltip('spanStartDateTooltip'); };

                //Highlight Textbox
                txtStartDate.style.borderStyle = "Solid";
                txtStartDate.style.borderWidth = "2";
                txtStartDate.style.borderColor = "Red";

                imgEndDate.style.display = "inline";
                //imgEndDate.alt = "End Date must be greater than start date.";
                imgEndDate.onmouseover = function() { ShowTooltip('spanEndDateTooltip', 'End Date must be greater than Start date.'); };
                imgEndDate.onmouseout = function() { HideTooltip('spanEndDateTooltip'); };

                //Highlight Textbox
                txtEndDate.style.borderStyle = "Solid";
                txtEndDate.style.borderWidth = "2";
                txtEndDate.style.borderColor = "Red";

                args.IsValid = false;
                dateIsValid = false;
            }
            else {
                //Check Start Date
                if (trim(SDate) != '' && validateDate(trim(SDate))) {
                    imgStartDate.style.display = "inline";
                    //imgStartDate.alt = "Start Date is not in correct format.";
                    imgStartDate.onmouseover = function() { ShowTooltip('spanStartDateTooltip', 'Start Date is not in correct format.'); };
                    imgStartDate.onmouseout = function() { HideTooltip('spanStartDateTooltip'); };

                    //Highlight Textbox
                    txtStartDate.style.borderStyle = "Solid";
                    txtStartDate.style.borderWidth = "2";
                    txtStartDate.style.borderColor = "Red";

                    args.IsValid = false;
                    dateIsValid = false;
                }
                else {
                    //Reset Textbox highlighting
                    if (!isSubmitClicked || (trim(SDate) == '' && imgStartDate.style.display == "inline")) {
                        txtStartDate.style.borderWidth = "1";
                        txtStartDate.style.borderColor = "#7F9DB9";
                    }

                    imgStartDate.style.display = "none";
                    imgStartDate.alt = "";

                    args.IsValid = true;
                }

                //Check End Date
                if (trim(EDate) != '' && validateDate(trim(EDate))) {
                    imgEndDate.style.display = "inline";
                    //imgEndDate.alt = "End Date is not in correct format.";
                    imgEndDate.onmouseover = function() { ShowTooltip('spanEndDateTooltip', 'End Date is not in correct format.'); };
                    imgEndDate.onmouseout = function() { HideTooltip('spanEndDateTooltip'); };

                    //Highlight Textbox
                    txtEndDate.style.borderStyle = "Solid";
                    txtEndDate.style.borderWidth = "2";
                    txtEndDate.style.borderColor = "Red";

                    args.IsValid = false;
                    dateIsValid = false;
                }
                else {
                    //Reset Textbox highlighting
                    if (!isSubmitClicked || (trim(EDate) == '' && imgEndDate.style.display == "inline")) {
                        txtEndDate.style.borderWidth = "1";
                        txtEndDate.style.borderColor = "#7F9DB9";
                    }

                    imgEndDate.style.display = "none";
                    imgEndDate.alt = "";

                    args.IsValid = true;
                }
            }

            return dateIsValid;
        }

        function fnValidateInput(sender, args) {
            debugger;
            var Status;
            var ProjectManager;
            
            args.Value = trim(args.Value);

            if ((args.Value.length == 0) || (args.Value == "Select")) {
                args.IsValid = false;
                return false;
            }
            else {
                args.IsValid = true;
                return true;
            }

        }

        function fnValidateInputforEnabled(sender, args) {
            debugger;
            if (!args.disabled) {
                args.Value = trim(args.Value);
                if ((args.Value.length == 0) || (args.Value == "Select") || (args.Value == "SELECT")) {
                    args.IsValid = false;
                    return false;
                }
                else {
                    args.IsValid = true;
                    return true;
                }
            }
        }

        function fnStatusCheck() {
            var Status;
            Status = document.getElementById('<%=ddlStatus.ClientID%>').options[document.getElementById('<%=ddlStatus.ClientID%>').selectedIndex].text;
            //Status = document.getElementById('<%=ddlStatus.ClientID%>').value;
            var lblTechDetails = document.getElementById('lblTechDetails');
            var lblDomainDetails = document.getElementById('lblDomDetails');
            var lblStandardHours = document.getElementById('lblHrs');

            if (Status == "Delivery" || Status == "Pre-Sales") {
                if (Status == "Pre-Sales")
                    lblTechDetails.style.display = "none";
                else
                    lblTechDetails.style.display = "inline";

                lblDomainDetails.style.display = "inline";
                lblStandardHours.style.display = "inline";
            }
            else {
                lblTechDetails.style.display = "none";
                lblDomainDetails.style.display = "none";
                lblStandardHours.style.display = "none";
            }

            if (Status == "Closed") {
                document.getElementById('<%=ddlStandardHours.ClientID %>').disabled = true;
            }
            else {
                document.getElementById('<%=ddlStandardHours.ClientID %>').disabled = false;
            }

        }

        function fnValidateInputForPM(sender, args) {
            var Status;
            var ProjectManager;

            Status = document.getElementById('<%=ddlStatus.ClientID%>').options[document.getElementById('<%=ddlStatus.ClientID%>').selectedIndex].text;

            ProjectManager = document.getElementById('<%=txtProjectManager.ClientID%>');
            ProjectManager = ProjectManager.value;
            if ((Status == "Delivery") && (ProjectManager == "")) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function fnValidateInputForCategory(sender, args) {
            var Status;
            var Category;

            Status = document.getElementById('<%=ddlStatus.ClientID%>').options[document.getElementById('<%=ddlStatus.ClientID%>').selectedIndex].text;
            Category = document.getElementById('<%=ddlCategory.ClientID%>').options[document.getElementById('<%=ddlCategory.ClientID%>').selectedIndex].text;

            var hasTechnologies = "false";

            if (document.getElementById('<%=gvCategoryTechnology.ClientID%>') != null) {
                if (document.getElementById('<%=gvCategoryTechnology.ClientID%>').hasChildNodes()) {
                    hasTechnologies = "true";
                }

            }
            if ((Status == "Delivery") && hasTechnologies == "false") {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }

        function fnValidateInputForTechnology(sender, args) {
            var Category;
            var Technology;

            Category = document.getElementById('<%=ddlCategory.ClientID%>').value;
            Technology = document.getElementById('<%=ddlTechnology.ClientID%>').value;
            if ((Category != "Select") && (Technology == "Select")) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }

        }

        function fnValidateInputForDomain(sender, args) {
            var Status;
            var Domain;

            Status = document.getElementById('<%=ddlStatus.ClientID%>').options[document.getElementById('<%=ddlStatus.ClientID%>').selectedIndex].text;

            //Status = document.getElementById('<%=ddlStatus.ClientID%>').value;                      
            Domain = document.getElementById('<%=ddlDomain.ClientID%>').options[document.getElementById('<%=ddlDomain.ClientID%>').selectedIndex].text;
            //SubDomain = document.getElementById('<%=ddlSubDomain.ClientID%>').value;
            //SubDomain = SubDomain.value; 

            var hasTechnologies = "false";

            if (document.getElementById('<%=gvDomainSubDomain.ClientID%>') != null) {
                if (document.getElementById('<%=gvDomainSubDomain.ClientID%>').hasChildNodes()) {
                    hasTechnologies = "true";
                }

            }

            if ((Status == "Delivery") && hasTechnologies == "false") {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function fnDeleteProject() {
            var txtDelete = document.getElementById('<%=txtDeletion.ClientID%>');
            var reason = document.getElementById('<%=txtDeletion.ClientID%>').value;
            if (trim(reason) != "") {
                return confirm("Are you sure you want to delete the record?");
            }
            else {
                //                document.getElementById("<%=lblMessage.ClientID %>").innerHTML  = "Please enter the reason for deletion.";
                //                document.getElementById('<%=txtDeletion.ClientID%>').focus();

                //Highlight Textbox
                txtDelete.style.borderStyle = "Solid";
                txtDelete.style.borderWidth = "2";
                txtDelete.style.borderColor = "Red";
                txtDelete.focus();
                return false;
            }
        }

        //        function fnRejectProject()
        //        {
        //            
        //            var reason = document.getElementById('<%=txtDeletion.ClientID%>').value;
        //            if  (trim(reason) !="")
        //            {
        //             return true;
        //            }
        //            else
        //            {
        //                document.getElementById("<%=lblMessage.ClientID %>").innerHTML  = "Please enter the reason for rejection.";
        //                document.getElementById('<%=txtDeletion.ClientID%>').focus();
        //                return false;
        //            }
        //        }

        function fnValidateInputForSubDomain(sender, args) {
            var Domain;
            var SubDomain;

            //Domain = document.getElementById('<%=ddlDomain.ClientID%>').options[document.getElementById('<%=ddlDomain.ClientID%>').selectedIndex].text;           
            //SubDomain = document.getElementById('<%=ddlSubDomain.ClientID%>').options[document.getElementById('<%=ddlSubDomain.ClientID%>').selectedIndex].text;
            Domain = document.getElementById('<%=ddlDomain.ClientID%>').value;
            //alert(document.getElementById('<%=ddlDomain.ClientID%>').value);
            SubDomain = document.getElementById('<%=ddlSubDomain.ClientID%>').value;
            //alert(Domain1 + "***" +SubDomain1 );                                       
            if ((Domain != "Select") && (SubDomain == "Select")) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }


        function fnValidateProjDesc(ControlId, MaxLen) {
            var ClientControlId;

            ClientControlId = document.getElementById(ControlId);
            ClientControlId = ClientControlId.value.length;
            return (ClientControlId < MaxLen);
        }

        // This function checks the maxlength of field when data is paste in control
        function fnLimitPaste(targetControl, maxLength) {
            var clipboardText = window.clipboardData.getData("Text").length;
            var totalLength = clipboardText + targetControl.value.length;

            if (totalLength > maxLength) {
                return false;
            }
            return true;
        }

        /// <summary>Set background to tabs</summary>
        /// <author>Prashant Mala</author>
        /// <CreatedDate>14th April 2009</CreatedDate>
        /// <ModifiedDate>09th July 2009</ModifiedDate>
        /// <returns>void</returns>
        //fn_MenuTab('Projects', 'ctl00_divAddProject');

        function removeText() {
            var msg = document.getElementById('<%=lblDomainError.ClientID %>');
            if (msg.innerHTML != '') {
                msg.innerHTML = '';
            }
            return false;
        }

        function removeTextCategoryError() {
            var msg = document.getElementById('<%=lbCategorylError.ClientID %>');
            if (msg.innerHTML != '') {
                msg.innerHTML = '';
            }
            return false;
        }
        
    </script>

    <table width="100%">
        <tr>
            <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <%--   Sanju:Issue Id 50201:End--%>
                <%--<asp:Label ID="lblAddProject" runat="server" Text="Add Project" Font-Bold="True"
                    Font-Names="Verdana" Font-Size="9pt" ForeColor="White" Height="25px">--%>
                <asp:Label ID="lblAddProject" runat="server" Text="Add Project" CssClass="header"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="text-align: center">
                <asp:ValidationSummary ID="valsumAddProject" runat="server" ValidationGroup="vgAddProject"
                    ShowMessageBox="false" ShowSummary="false" DisplayMode="BulletList" />
                <asp:ValidationSummary ID="valsumCategory" runat="server" ValidationGroup="vgCategory"
                    ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" />
                <asp:ValidationSummary ID="valsumDomain" runat="server" ValidationGroup="vgDomain"
                    ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" />
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:Label ID="lblMessage" runat="server" ForeColor="Red" Width="400px"></asp:Label>--%>
                <asp:Label ID="lblMessage" runat="server" CssClass="text"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
            </td>
        </tr>
    </table>
    <%--<div style="border: solid 1px black">--%>
    <div class="detailsborder">
        <%--<table width="100%" style="background-color: #BFBFBF">--%>
        <table width="100%" class="detailsbg">
            <tr>
                <td>
                    <%--<asp:Label ID="lblProjectDetailsGroup" runat="server" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="10pt" Text="Project Details">
                    </asp:Label>--%>
                    <asp:Label ID="lblProjectDetailsGroup" runat="server" Text="Project Details" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
            <%--By Rakesh removed height: 361px;--%>
                <td style="width: 50%; " valign="top">
                    <table width="100%">
                        <%--// Mohamed : Issue  : 23/09/2014 : Starts                        			  
                        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS--%>
                        <tr>
                            <td style="width: 140px;">
                                <asp:Label ID="lblPrjDivision" runat="server" Text="Division" CssClass="textstyle"></asp:Label><label
                                    class="mandatorymark">*</label>
                            </td>
                            <td>
                                <span id="spanProjectDivision">
                                    <asp:DropDownList ID="ddlPrjDivision" runat="server" Width="155px" ToolTip="Select Project Division"
                                        CssClass="mandatoryField" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlPrjDivision_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </span>
                                <asp:CustomValidator runat="server" ID="cvProjectDivision" ControlToValidate="ddlPrjDivision"
                                    ClientValidationFunction="fnValidateInput" ValidateEmptyText="true" EnableClientScript="true"
                                    meta:resourcekey="cvProjectDivision" SetFocusOnError="true" ValidationGroup="vgAddProject"
                                    Display="Dynamic"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 140px;">
                                <asp:Label ID="lblPrjBusinessSegment" runat="server" Text="Business Segment" CssClass="textstyle"></asp:Label><label
                                    class="mandatorymark">*</label>
                            </td>
                            <td>
                                <span id="spanBusinessSegment">
                                    <asp:DropDownList ID="ddlProjectBusinessSegment" runat="server" Width="155px" ToolTip="Select Business Segment"
                                        CssClass="mandatoryField" TabIndex="2" Enabled="false">
                                    </asp:DropDownList>
                                </span>
                                <asp:CustomValidator runat="server" ID="cvBusinessSegment" ControlToValidate="ddlProjectBusinessSegment"
                                    ClientValidationFunction="fnValidateInputforEnabled" ValidateEmptyText="true"
                                    EnableClientScript="true" meta:resourcekey="cvBusinessSegment" SetFocusOnError="true"
                                    ValidationGroup="vgAddProject" Display="Dynamic"></asp:CustomValidator>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td style="width: 130px;">
                                <asp:Label ID="lblProjectAlias" runat="server" Text="Project Alias" CssClass="textstyle"></asp:Label><label
                                    class="mandatorymark">*</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProjectAlias" runat="server" CssClass="mandatoryField" MaxLength="30"
                                    ToolTip="Enter Project Name" BorderStyle="Solid" TabIndex="1" AutoComplete="off"></asp:TextBox>
                                <asp:CustomValidator runat="server" ID="cvProjectAlias" ControlToValidate="txtProjectAlias"
                                    ClientValidationFunction="ValidateProjectAlias" ValidateEmptyText="true" SetFocusOnError="true"
                                    ValidationGroup="vgAddProject"></asp:CustomValidator>
                                <span id="spanProjectAlias">
                                    <img id="imgProjectAlias" src="Images/cross.png" alt="" style="display: none; border: none;"
                                        onmouseover="ShowTooltip('spanProjectAlias', 'Project Alias is not in correct format.');"
                                        onmouseout="HideTooltip('spanProjectAlias');" />
                                </span>
                            </td>
                        </tr>--%>
                        <%--// Mohamed : Issue  : 23/09/2014 : Ends        --%>
                        <tr>
                            <td style="width: 130px;">
                                <%--<asp:Label ID="lblStartDate" runat="server" Text="Start Date" Width="65px" Font-Names="Verdana" Font-Size="9pt"></asp:Label><label style="color: Red">*</label>--%>
                                <asp:Label ID="lblStartDate" runat="server" Text="Start Date" CssClass="textstyle"></asp:Label><%--<label class="mandatorymark">*</label>--%>
                            </td>
                            <%-- Sanju:Issue Id 50201: Alignment issue resolved--%>
                            <td style="/*padding-left: 6px;*/">
                                <%--Sanju:Issue Id 50201: End--%>
                                <%--<asp:TextBox runat="server" ID="txtStartDate" CssClass="mandatoryField" MaxLength="10" ToolTip="Enter Start Date in DD/MM/YYYY format" TabIndex="3" AutoComplete="off"></asp:TextBox>--%>
                                <%-- <asp:ImageButton ID="imgCalStartDate" runat="server" ImageUrl="Images/Calendar_scheduleHS.png" CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="3" />--%>
                                <%--<cc1:CalendarExtender ID="calendarStartDate" runat="server" PopupButtonID="imgCalStartDate" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>--%>
                                <uc1:DatePicker ID="ucDatePickerStartDate" runat="server" style="width: 153px;" />
                                <%--<asp:RequiredFieldValidator runat="server" ID="rfvStartDate" ControlToValidate="ucDatePickerStartDate" SetFocusOnError="true" ValidationGroup="vgAddProject" meta:resourcekey="rfvStartDate" Display="none"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revStartDate" ControlToValidate="ucDatePickerStartDate" Enabled="false" ValidationGroup="vgAddProject" Display="None" SetFocusOnError="true" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d" meta:resourcekey="revStartDate"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="cvStartDateFormat" runat="server" ControlToValidate="ucDatePickerStartDate" ValidateEmptyText="true" SetFocusOnError="true" ClientValidationFunction="CheckDates" ValidationGroup="vgAddProject"></asp:CustomValidator>
                                --%>
                                <span id="spanStartDateTooltip">
                                    <img id="imgStartDate" src="Images/cross.png" alt="" style="display: none; border: none;" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px;">
                                <asp:Label ID="lblEndDate" runat="server" Text="End Date" CssClass="textstyle">
                                </asp:Label><%--
                                <label class="mandatorymark">
                                    *</label>--%>
                            </td>
                            <%-- Sanju:Issue Id 50201: Alignment issue resolved--%>
                            <td style="/*padding-left: 6px;*/">
                                <%--  Sanju:Issue Id 50201: end--%>
                                <%--<asp:TextBox runat="server" ID="txtEndDate" CssClass="mandatoryField" MaxLength="10" Width="150px" ToolTip="Enter End Date in DD/MM/YYYY format" TabIndex="5" AutoComplete="off"></asp:TextBox>--%>
                                <%--<asp:ImageButton ID="imgCalEndDate" runat="server" ImageUrl="Images/Calendar_scheduleHS.png" CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="5" />--%>
                                <%-- <cc1:CalendarExtender ID="calendarEndDate" runat="server" PopupButtonID="imgCalEndDate" TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>--%>
                                <uc1:DatePicker ID="ucDatePickerEndDate" runat="server" style="width: 153px;" />
                                <%--<asp:RequiredFieldValidator runat="server" ID="rfvEndDate" ControlToValidate="ucDatePickerEndDate" SetFocusOnError="true" ValidationGroup="vgAddProject" meta:resourcekey="rfvEndDate" Display="None"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revEndDate" ControlToValidate="ucDatePickerEndDate" Enabled="false" ValidationGroup="vgAddProject" Display="None" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\d\d" meta:resourcekey="revEndDate" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="cvStartEndDate" runat="server" ControlToValidate="ucDatePickerEndDate" ValidateEmptyText="true" SetFocusOnError="true" ClientValidationFunction="CheckDates" ValidationGroup="vgAddProject"></asp:CustomValidator>
                                --%>
                                <span id="spanEndDateTooltip">
                                    <img id="imgEndDate" src="Images/cross.png" alt="" style="display: none; border: none;" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px;">
                                <%--<asp:Label ID="lblStandardHours" runat="server" Text="Project Std Hrs " Width="98px" Font-Names="Verdana" Font-Size="9pt"></asp:Label><label style="color: Red">*</label>--%>
                                <asp:Label ID="lblStandardHours" runat="server" Text="Project Std Hrs " CssClass="textstyle">
                                </asp:Label><label class="mandatorymark" id="lblHrs">*<label>
                            </td>
                            <td>
                               <span id="spanStandardHours">
                                    <asp:DropDownList ID="ddlStandardHours" runat="server" ToolTip="Select Project std Hours"
                                        Width="153px" CssClass="mandatoryField" TabIndex="7">
                                    </asp:DropDownList>
                               </span>
                                <asp:TextBox ID="txtStandardHours" runat="server" Width="150px" Visible="false" TabIndex="7"
                                    CssClass="mandatoryField"></asp:TextBox>
                                <asp:CustomValidator runat="server" ID="cvStandardHours" ControlToValidate="ddlStandardHours"
                                    ClientValidationFunction="fnValidateInput" ValidateEmptyText="true" EnableClientScript="true"
                                    meta:resourcekey="cvStandardHours" SetFocusOnError="true" ValidationGroup="vgAddProject"
                                    Display="Dynamic"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px;">
                                <asp:Label ID="LblOnGoingProjectStatus" runat="server" Text="Ongoing Project Status"
                                    CssClass="textstyle">
                                </asp:Label><label class="mandatorymark" id="ManMarkProjOnGoingStatus">*</label>
                            </td>
                            <td>
                                  <span id="spanOnGoingProjectStatus">
                                    <asp:DropDownList ID="ddlOnGoingProjectStatus" runat="server" ToolTip="Select Project std Hours"
                                        Width="153px" CssClass="mandatoryField" TabIndex="8">
                                    </asp:DropDownList>
                                 </span> 
                                <asp:TextBox ID="txtOnGoingProjectStatus" runat="server" Width="150px" Visible="false"
                                    TabIndex="8" CssClass="mandatoryField">
                                </asp:TextBox>
                                <asp:CustomValidator runat="server" ID="CvOnGoingPrjSt" ControlToValidate="ddlOnGoingProjectStatus"
                                    ClientValidationFunction="fnValidateInput" ValidateEmptyText="true" EnableClientScript="true"
                                    meta:resourcekey="CvOnGoingPrjSt" SetFocusOnError="true" ValidationGroup="vgAddProject"
                                    Display="Dynamic">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        
                        
                         <!--Siddharth 12th March 2015 Add Project Model Drop Down --Start -->
                        <tr>
                            <td style="width: 140px;">
                                <asp:Label ID="LblProjectModel" runat="server" Text="ProjectModel" CssClass="textstyle"></asp:Label><label
                                    class="mandatorymark">*</label>
                            </td>
                            <td>
                                <span id="SpanProjectModel">
                                    <asp:DropDownList ID="ddlProjectModel" runat="server" Width="155px" ToolTip="Select Project Model"
                                        CssClass="mandatoryField" TabIndex="1" Enabled="false" AutoPostBack="true">
                                    </asp:DropDownList>
                                </span>
                                <asp:CustomValidator runat="server" ID="CustomValidator1" ControlToValidate="ddlProjectModel"
                                    ClientValidationFunction="fnValidateInputforEnabled" ValidateEmptyText="true"
                                    EnableClientScript="true" meta:resourcekey="cvProjectModel" SetFocusOnError="true"
                                    ValidationGroup="vgAddProject" Display="Dynamic"></asp:CustomValidator>
                            </td>
                          
                        </tr>
                       <!--Siddharth 12th March 2015 Add Project Model Drop Down --End -->
                        
                        
                        <tr style="display: none;">
                            <td style="width: 130px;">
                                <%--      <asp:Label ID="lblProjectManager" runat="server" Text="Project Manager " Width="106px" Font-Names="Verdana" Font-Size="9pt"></asp:Label><label style="color: Red; display: none;" id="lblProjManager">*</label>--%>
                                <asp:Label ID="lblProjectManager" runat="server" Text="Project Manager " CssClass="textstyle"></asp:Label><label
                                    style="display: none;" class="mandatorymark" id="lblProjManager">*="mandatorymark"
                                    id="lblProjManager">*</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProjectManager" runat="server" CssClass="mandatoryField" Width="150px"
                                    MaxLength="50" ReadOnly="true" ToolTip="Browse for a Project Manager" TabIndex="8"></asp:TextBox>
                                <img id="imgSearchPM" runat="server" src="Images/find.png" alt="Search" style="border: none;
                                    cursor: hand;" onclick="SelectPM();" />
                                <img id="imgClearPM" runat="server" src="Images/trash.gif" alt="Clear" style="border: none;
                                    cursor: hand; display: none;" onclick="ClearPM();" />
                                <asp:CustomValidator runat="server" ID="cvProjectManager" ControlToValidate="txtProjectManager"
                                    ClientValidationFunction="fnValidateInputForPM" ValidateEmptyText="true" EnableClientScript="true"
                                    meta:resourcekey="cvProjectManager" SetFocusOnError="true" ValidationGroup="vgAddProject"
                                    Display="Dynamic">
                                </asp:CustomValidator>
                                <input type="hidden" id="hidProjectManagerId" runat="server" style="width: 47px" />
                                <input type="hidden" id="hidUpdateProjectManagerID" runat="server" style="width: 40px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <%--By Rakesh removed height: 361px;--%>
                <td style="width: 50%; " valign="top" align="right">
                    <table width="100%">
                        <tr>
                            <td>
                                <table width="80%" style="text-align: left;">
                                    <%--// Mohamed : Issue  : 23/09/2014 : Starts                        			  
                                    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS--%>
                                    <tr>
                                        <td style="width: 140px;">
                                            <asp:Label ID="lblProjectBusinessArea" runat="server" Text="BusinessArea" CssClass="textstyle"></asp:Label><label
                                                class="mandatorymark">*</label>
                                        </td>
                                        <td>
                                            <span id="spanBusinessArea">
                                                <asp:DropDownList ID="ddlProjecBusinessArea" runat="server" Width="155px" ToolTip="Select Business Area"
                                                    CssClass="mandatoryField" TabIndex="1" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlProjecBusinessArea_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </span>
                                            <asp:CustomValidator runat="server" ID="cvBusinessArea" ControlToValidate="ddlProjecBusinessArea"
                                                ClientValidationFunction="fnValidateInputforEnabled" ValidateEmptyText="true"
                                                EnableClientScript="true" meta:resourcekey="cvBusinessArea" SetFocusOnError="true"
                                                ValidationGroup="vgAddProject" Display="Dynamic"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 130px;">
                                            <asp:Label ID="lblProjectName" runat="server" Text="Project Name" CssClass="textstyle"></asp:Label><%--<label class="mandatorymark">*</label>--%>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="mandatoryField" MaxLength="30"
                                                ToolTip="Enter Project Name" BorderStyle="Solid" TabIndex="1" AutoComplete="off"></asp:TextBox><asp:CustomValidator
                                                    runat="server" ID="cvProjectName" ControlToValidate="txtProjectName" ClientValidationFunction="ValidateProjectName"
                                                    ValidateEmptyText="true" SetFocusOnError="true" ValidationGroup="vgAddProject"></asp:CustomValidator><span
                                                        id="spanProjectNameTooltip"><img id="imgProjectName" src="Images/cross.png" alt=""
                                                            style="display: none; border: none;" onmouseover="ShowTooltip('spanProjectNameTooltip', 'Project Name is not in correct format.');"
                                                            onmouseout="HideTooltip('spanProjectNameTooltip');" />
                                                    </span>
                                        </td>
                                    </tr>
                                    <%--// Mohamed : Issue  : 23/09/2014 : Ends        --%>
                                    <tr>
                                        <td style="width: 140px;">
                                            <%--<asp:Label ID="lblProjectID" runat="server" Text="Project ID " Visible="False" Width="63px" Font-Names="Verdana" Font-Size="9pt"></asp:Label>--%>
                                            <asp:Label ID="lblProjectID" runat="server" Text="Project Code " Visible="False"
                                                CssClass="textstyle"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProjectCode" runat="server" Visible="false" CssClass="mandatoryField"
                                                Width="150px">
                                            </asp:TextBox>
                                            <asp:TextBox ID="txtProjectID" runat="server" CssClass="mandatoryField" Visible="false"
                                                Width="150px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 140px;">
                                            <%--<asp:Label ID="lblProjectGroup" runat="server" Text="Project Group" Width="86px" Font-Names="Verdana" Font-Size="9pt"></asp:Label><label style="color: Red">*</label>--%>
                                            <asp:Label ID="lblProjectGroup" runat="server" Text="Project Group" CssClass="textstyle"></asp:Label><label
                                                class="mandatorymark">*</label>
                                        </td>
                                        <td>
                                            <span id="spanProjectGroup">
                                                <asp:DropDownList ID="ddlProjectGroup" runat="server" Width="155px" ToolTip="Select Project Group"
                                                    CssClass="mandatoryField" TabIndex="2">
                                                </asp:DropDownList>
                                            </span>
                                            <asp:TextBox ID="txtProjectGroup" runat="server" Width="150px" Visible="false" CssClass="mandatoryField"
                                                TabIndex="4"></asp:TextBox>
                                            <asp:CustomValidator runat="server" ID="cvProjectGroup" ControlToValidate="ddlProjectGroup"
                                                ClientValidationFunction="fnValidateInput" ValidateEmptyText="true" EnableClientScript="true"
                                                meta:resourcekey="cvProjectGroup" SetFocusOnError="true" ValidationGroup="vgAddProject"
                                                Display="Dynamic"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 140px;">
                                            <asp:Label ID="lblStatus" runat="server" Text="Project Status " CssClass="textstyle"></asp:Label><label
                                                class="mandatorymark">*</label>
                                        </td>
                                        <td>
                                            <span id="spanStatus">
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="155px" ToolTip="Select Project Status"
                                                    CssClass="mandatoryField" onchange="return fnStatusCheck();" TabIndex="4">
                                                </asp:DropDownList>
                                            </span>
                                            <asp:TextBox ID="txtStatus" runat="server" Width="150px" Visible="false" TabIndex="3"
                                                CssClass="mandatoryField"></asp:TextBox>
                                            <asp:CustomValidator runat="server" ID="cvStatus" ControlToValidate="ddlStatus" ClientValidationFunction="fnValidateInput"
                                                ValidateEmptyText="true" EnableClientScript="true" meta:resourcekey="cvStatus"
                                                SetFocusOnError="true" ValidationGroup="vgAddProject" Display="Dynamic"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 140px;">
                                            <%--                                            <asp:Label ID="lblDescription" runat="server" Text="Project Executive Summary" Width="119px" Font-Names="Verdana" Font-Size="9pt"></asp:Label><label style="color: Red">*</label>
--%>
                                            <asp:Label ID="lblDescription" runat="server" Text="Project Executive Summary" CssClass="textstyle"></asp:Label><label
                                                class="mandatorymark">*</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="150px"
                                                ToolTip="Enter Project Executive Summary" MaxLength="5000" CssClass="mandatoryField"
                                                TabIndex="6" AutoComplete="off" onpaste="return fnLimitPaste(this,5000);"></asp:TextBox>
                                            <asp:CustomValidator runat="server" ID="cvDescription" ControlToValidate="txtDescription"
                                                ClientValidationFunction="fnValidateInput" ValidateEmptyText="true" EnableClientScript="true"
                                                meta:resourcekey="cvDescription" SetFocusOnError="true" ValidationGroup="vgAddProject"
                                                Display="Dynamic"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    
                                      
                            <!-- Siddharth 3rd August 2015 Start -->
                                    <tr>
                                         <td style="width: 140px;">
                                            <asp:Label ID="LblBusinessVertical" runat="server" Text="BusinessVertical" CssClass="textstyle"></asp:Label><label
                                                class="mandatorymark">*</label>
                                        </td>
                                        <td>
                                            <span id="SpanBusinessVertical">
                                                <asp:DropDownList ID="ddlBusinessVertical" runat="server" Width="155px" ToolTip="Select Business Vertical"
                                                    CssClass="mandatoryField" TabIndex="1" Enabled="false" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </span>
                                            <asp:CustomValidator runat="server" ID="CustomValidator2" ControlToValidate="ddlBusinessVertical"
                                                ClientValidationFunction="fnValidateInputforEnabled" ValidateEmptyText="true"
                                                EnableClientScript="true" meta:resourcekey="cvBusinessVertical" SetFocusOnError="true"
                                                ValidationGroup="vgAddProject" Display="Dynamic"></asp:CustomValidator>
                                        </td>
                                    </tr>
                            <!-- Siddharth 3rd August 2015 End -->
                                    <!-- <tr>
                                        <td style="width: 140px;">
                                            <%--      <asp:Label ID="lblDeletion" runat="server" Text="Reason for Deletion " Width="130px" Visible="False" Font-Names="Verdana" Font-Size="9pt"></asp:Label>
                                            <asp:Label ID="lblDeletionMandatory" runat="server" Style="color: Red" Visible="False" Text="*"></asp:Label>--%>
                                            <asp:Label ID="lblDeletion" runat="server" Text="Reason for Deletion " Visible="False"
                                                CssClass="textstyle"></asp:Label>
                                            <%--<asp:Label ID="lblDeletionMandatory" runat="server" Visible="False" Text="*" class="mandatorymark"></asp:Label>--%>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDeletion" runat="server"  TextMode="MultiLine" Visible="False"
                                                Width="150px" MaxLength="100" CssClass="mandatoryField" AutoComplete="off"
                                                onpaste="return fnLimitPaste(this,100);"></asp:TextBox>
                                            <img id="imgDeletion" src="Images/cross.png" alt="" style="display: none; border: none;" />
                                        </td>
                                    </tr>-->
                                    <tr>
                                         <td style="width: 140px;">
                                             Head of Project    <label
                                                class="mandatorymark">*</label></td>
                                        <td>
                                            <span id="SpanProjectHead">
                                                <asp:DropDownList ID="ddlProjectHead" runat="server" Width="155px" ToolTip="Select Project Head"
                                                    CssClass="mandatoryField" TabIndex="1" Enabled="false" 
                                                AutoPostBack="false">
                                                </asp:DropDownList>
                                            <asp:CustomValidator runat="server" ID="CustomValidator3" ControlToValidate="ddlProjectHead"
                                                ClientValidationFunction="fnValidateInputforEnabled" ValidateEmptyText="true"
                                                EnableClientScript="true" meta:resourcekey="cvProjectHead" SetFocusOnError="true"
                                                ValidationGroup="vgAddProject" Display="Dynamic"></asp:CustomValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 402px">
                    <input type="hidden" id="hidPrevProjectId" runat="server" />
                    <input type="hidden" id="hidNextProjectId" runat="server" />
                    <input type="hidden" id="hidSortDir" runat="server" />
                    <input type="hidden" id="hidSortExpr" runat="server" />
                    <input type="hidden" id="hidWorkflowStatus" runat="server" />
                    <input type="hidden" id="hidProjectManagerName" runat="server" />
                    <input type="hidden" id="hidProjectManagerEmail" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="border: solid 1px black">
        <table width="100%" style="background-color: #BFBFBF">
            <tr>
                <td style="width: 3px">
                    <asp:Label ID="lblClientDetails" runat="server" Font-Names="Verdana" Font-Size="10pt"
                        Text="Client Details" Font-Bold="True" Width="97px"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 50%;">
                    <table width="100%">
                        <tr>
                            <td style="width: 130px;">
                                <%--<asp:Label ID="lblClientName" runat="server" Text="Client Name " Font-Names="Verdana" Font-Size="9pt"></asp:Label><label style="color: Red">*</label>--%>
                                <asp:Label ID="lblClientName" runat="server" Text="Client Name " CssClass="textstyle"></asp:Label><%--<label class="mandatorymark">*</label>--%>
                            </td>
                            <td>
                                <asp:TextBox ID="txtClientName" runat="server" MaxLength="20" ToolTip="Enter Client Name"
                                    Width="150px" CssClass="mandatoryField" TabIndex="10" AutoComplete="off"></asp:TextBox>
                                <asp:CustomValidator runat="server" ID="cvClientName" ControlToValidate="txtClientName"
                                    ClientValidationFunction="ValidateClientName" ValidateEmptyText="true" SetFocusOnError="true"
                                    ValidationGroup="vgAddProject"></asp:CustomValidator>
                                <span id="spanClientNameTooltip">
                                    <img id="imgClientName" src="Images/cross.png" alt="" style="display: none; border: none;"
                                        onmouseover="ShowTooltip('spanClientNameTooltip', 'Client Name is not in correct format.');"
                                        onmouseout="HideTooltip('spanClientNameTooltip');" />
                                </span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%;" align="right">
                    <table width="100%">
                        <tr>
                            <td>
                                <table width="80%" style="text-align: left;">
                                    <tr>
                                        <td style="width: 140px;">
                                            <%--<asp:Label ID="lblLocation" runat="server" Text="Location " Font-Names="Verdana" Font-Size="9pt"></asp:Label><label style="color: Red">*</label>--%>
                                            <asp:Label ID="lblLocation" runat="server" Text="Location " CssClass="textstyle"></asp:Label><%--<label class="mandatorymark">*</label>--%>
                                        </td>
                                        <td>
                                            <span id="spanLocation">
                                                <asp:DropDownList ID="ddlLocation" runat="server" Width="155px" ToolTip="Select Location"
                                                    CssClass="mandatoryField" TabIndex="11">
                                                </asp:DropDownList>
                                            </span>
                                            <asp:TextBox ID="txtLocation" runat="server" Width="150px" Visible="false" CssClass="mandatoryField"
                                                TabIndex="12">
                                            </asp:TextBox>
                                            <asp:CustomValidator runat="server" ID="cvLocation" ControlToValidate="ddlLocation"
                                                ClientValidationFunction="fnValidateInput" ValidateEmptyText="true" EnableClientScript="true"
                                                meta:resourcekey="cvLocation" SetFocusOnError="true" ValidationGroup="vgAddProject"
                                                Display="Dynamic"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="border: solid 1px black" id="divTechnologyDetails" runat="server">
        <table width="100%" style="background-color: #BFBFBF">
            <tr>
                <td>
                    <%--<asp:Label ID="lblTechnologyDetails" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" Text="Technology Details"></asp:Label><label style="color: Red; display: none;" id="lblTechDetails">*</label>--%>
                    <asp:Label ID="lblTechnologyDetails" runat="server" Text="Technology Details" CssClass="detailsheader"></asp:Label><label
                        style="display: none;" id="lblTechDetails" class="mandatorymark">*</label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlTechnologyControls" runat="server" Visible="true">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 50%">
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 130px">
                                                                <%--<asp:Label ID="lblCategories" runat="server" Text="Category" Font-Size="9pt" Font-Names="Verdana"></asp:Label>--%><asp:Label
                                                                    ID="lblCategories" runat="server" Text="Category" CssClass="textstyle"></asp:Label></td>
                                                            <td>
                                                                <span id="spanCategory">
                                                                    <%--<asp:DropDownList ID="ddlCategory" TabIndex="12" runat="server" CssClass="mandatoryField" ToolTip="Select Category" Width="155px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="True">
                                                                    </asp:DropDownList>--%>
                                                                    <%-- Sanju:Issue Id 50201:Added border style property(Chrome issue) --%>
                                                                    <cc2:ComboBox ID="ddlCategory" runat="server" TabIndex="12" CssClass="mandatoryField"
                                                                        ToolTip="Select Category" Style="border-style: none;" Width="155px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                                                        AutoPostBack="True" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown">
                                                                    </cc2:ComboBox>
                                                                    <%--   Sanju:Issue Id 50201:End--%>
                                                                </span>
                                                                <asp:CustomValidator ID="cvCategory" runat="server" ValidationGroup="vgAddProject"
                                                                    SetFocusOnError="true" ValidateEmptyText="true" ClientValidationFunction="fnValidateInputForCategory"
                                                                    ControlToValidate="ddlCategory" Display="Dynamic" meta:resourcekey="cvCategory"
                                                                    EnableClientScript="true"></asp:CustomValidator>
                                                                <asp:CustomValidator ID="cvCategoy1" runat="server" ValidationGroup="vgCategory"
                                                                    SetFocusOnError="true" ValidateEmptyText="true" ClientValidationFunction="fnValidateInputForCategory"
                                                                    ControlToValidate="ddlCategory" Display="Dynamic" meta:resourcekey="cvCategory"
                                                                    EnableClientScript="true">
                                                                </asp:CustomValidator>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="width: 50%" align="right">
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table style="text-align: left" width="80%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 140px">
                                                                                <asp:Label ID="lblTechnologies" runat="server" Text="Technology" CssClass="textstyle"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span id="spanTechnology">
                                                                                                    <%-- <asp:DropDownList ID="ddlTechnology" TabIndex="13" runat="server" ToolTip="Select Technology" Enabled="False" Width="155px">
                                                                                                    </asp:DropDownList>--%>
                                                                                                    <%--<uc1:SuggestionBox ID="ddlTechnology" runat="server" />--%>
                                                                                                    <%-- Sanju:Issue Id 50201:Added border style property(Chrome issue) --%>
                                                                                                    <cc2:ComboBox ID="ddlTechnology" runat="server" TabIndex="13" ToolTip="Select Technology"
                                                                                                        Style="border-style: none;" Width="155px" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown"
                                                                                                        CssClass="mandatoryField">
                                                                                                    </cc2:ComboBox>
                                                                                                    <%--Sanju:Issue Id 50201: End     --%>
                                                                                                </span>
                                                                                                <asp:CustomValidator ID="cvTechnology" runat="server" ValidationGroup="vgCategory"
                                                                                                    SetFocusOnError="true" ValidateEmptyText="true" ClientValidationFunction="fnValidateInputForTechnology"
                                                                                                    ControlToValidate="ddlTechnology" Display="Dynamic" meta:resourcekey="cvTechnology"
                                                                                                    EnableClientScript="true"></asp:CustomValidator>
                                                                                            </td>
                                                                                            <td align="right">
                                                                                                <asp:Button ID="btnAdd" TabIndex="14" OnClick="btnAdd_Click" runat="server" Text="Add Row"
                                                                                                    CssClass="button" OnClientClick="removeTextCategoryError();" Width="75px"></asp:Button>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:GridView ID="gvCategoryTechnology" runat="server" Width="100%" Height="1px"
                                AutoGenerateColumns="False" EditIndex="0" OnRowDeleting="gvCategoryTechnology_RowDeleting"
                                SelectedIndex="0" DataKeyNames="category">
                                <%--<HeaderStyle CssClass="addrowheader" />
                                <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                                <RowStyle CssClass="textstyle" />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Category" HeaderStyle-Width="33%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("category")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CategoryID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryID" runat="server" Text='<%#Eval("categoryId")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Technology" HeaderStyle-Width="33%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTechnology" runat="server" Text='<%#Eval("technologyName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TechnologyID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTechnologyID" runat="server" Text='<%#Eval("technologyID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="33%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnRemove" Text="Remove" runat="server" CommandName="Delete"
                                                CommandArgument='<%#Eval("category") %>' Font-Underline="True" ForeColor="Red"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheaderStyle" />
                                <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                                <RowStyle Height="20px" CssClass="txtstyle" />
                                <EmptyDataRowStyle CssClass="text" />
                            </asp:GridView>
                            <asp:Label ID="lbCategorylError" runat="server" CssClass="text" EnableViewState="False"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel runat="server" ID="pnlNoTechnologyRecords" Visible="false">
        <table width="100%" style="border: solid 1px black">
            <tr style="background-color: #BFBFBF">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Technology Details" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="text">
                    No Records found.
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <div style="border: solid 1px black" runat="server" id="divDomainDetails">
        <table width="100%" style="background-color: #BFBFBF">
            <tr>
                <td>
                    <%--<asp:Label ID="lblDomainDetails" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" Text="Domain Details"></asp:Label><label style="color: Red; display: none;" id="lblDomDetails">*</label>--%>
                    <asp:Label ID="lblDomainDetails" runat="server" Text="Domain Details" CssClass="detailsheader"></asp:Label><label
                        style="display: none;" id="lblDomDetails" class="mandatorymark">*</label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlDomainControls" runat="server" Visible="true">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 50%">
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 130px">
                                                                <asp:Label ID="lblDomains" runat="server" Text="Domain" CssClass="textstyle"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <span id="spanDomain">
                                                                    <%--
                                                                    <asp:DropDownList ID="ddlDomain" TabIndex="15" runat="server" CssClass="mandatoryField" ToolTip="Select Domain" Width="155px" OnSelectedIndexChanged="ddlDomain_SelectedIndexChanged" AutoPostBack="True">
                                                                    </asp:DropDownList>--%>
                                                                    <%-- Sanju:Issue Id 50201:Added border style property(Chrome issue) --%>
                                                                    <cc2:ComboBox ID="ddlDomain" runat="server" TabIndex="15" CssClass="mandatoryField"
                                                                        ToolTip="Select Domain" Style="border-style: none;" Width="155px" OnSelectedIndexChanged="ddlDomain_SelectedIndexChanged"
                                                                        AutoPostBack="True" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown">
                                                                    </cc2:ComboBox>
                                                                    <%-- Sanju:Issue Id 50201:End --%>
                                                                </span>
                                                                <asp:CustomValidator ID="cvDomain" runat="server" ValidationGroup="vgAddProject"
                                                                    SetFocusOnError="true" ValidateEmptyText="true" ClientValidationFunction="fnValidateInputForDomain"
                                                                    ControlToValidate="ddlDomain" Display="Dynamic" meta:resourcekey="cvDomain" EnableClientScript="true"></asp:CustomValidator>
                                                                <asp:CustomValidator ID="cvDomain1" runat="server" ValidationGroup="vgDomain" SetFocusOnError="true"
                                                                    ValidateEmptyText="true" ClientValidationFunction="fnValidateInputForDomain"
                                                                    ControlToValidate="ddlDomain" Display="Dynamic" meta:resourcekey="cvDomain" EnableClientScript="true">
                                                                </asp:CustomValidator>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="width: 50%" align="right">
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table style="text-align: left" width="80%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 140px">
                                                                                <asp:Label ID="lblSubDomains" runat="server" Text="SubDomain" CssClass="textstyle"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <table width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span id="spanSubDomain">
                                                                                                    <%-- <asp:DropDownList ID="ddlSubDomain" TabIndex="16" runat="server" ToolTip="Select SubDomain" Enabled="False" Width="155px">
                                                                                                    </asp:DropDownList>--%>
                                                                                                    <%-- Sanju:Issue Id 50201:Added border style property(Chrome issue) --%>
                                                                                                    <cc2:ComboBox ID="ddlSubDomain" runat="server" TabIndex="16" runat="server" Style="border-style: none;"
                                                                                                        CssClass="mandatoryField" ToolTip="Select SubDomain" Width="155px" AutoPostBack="True"
                                                                                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDown">
                                                                                                    </cc2:ComboBox>
                                                                                                    <%-- Sanju:Issue Id 50201:End --%>
                                                                                                </span>
                                                                                                <asp:CustomValidator ID="cvSubDomain" runat="server" ValidationGroup="vgDomain" SetFocusOnError="true"
                                                                                                    ValidateEmptyText="true" ClientValidationFunction="fnValidateInputForSubDomain"
                                                                                                    ControlToValidate="ddlSubDomain" Display="Dynamic" meta:resourcekey="cvSubDomain"
                                                                                                    EnableClientScript="true"></asp:CustomValidator>
                                                                                            </td>
                                                                                            <td align="right">
                                                                                                <asp:Button ID="btnAddDomain" TabIndex="17" OnClick="btnAddDomain_Click" runat="server"
                                                                                                    Text="Add Row" CssClass="button" OnClientClick="removeText();" Width="75px">
                                                                                                </asp:Button>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            <asp:GridView ID="gvDomainSubDomain" runat="server" Visible="False" Width="100%"
                                Height="26px" AutoGenerateColumns="False" EditIndex="0" OnRowDeleting="gvDomainSubDomain_RowDeleting"
                                SelectedIndex="0" DataKeyNames="domain">
                                <%--<HeaderStyle CssClass="addrowheader" />
                                <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                                <RowStyle CssClass="textstyle" />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="DomainId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="hdnDomainId" runat="server" Text='<%#Eval("ID")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Domain" HeaderStyle-Width="33%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDomain" runat="server" Text='<%#Eval("domain")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubDomain" HeaderStyle-Width="33%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubDomain" runat="server" Text='<%#Eval("subDomain") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubDomainID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubDomainID" runat="server" Text='<%#Eval("subDomainId") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="33%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnRemove" Text="Remove" runat="server" CommandName="Delete"
                                                CommandArgument='<%#Eval("domain") %>' Font-Underline="True" ForeColor="Red">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheaderStyle" />
                                <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                                <RowStyle Height="20px" CssClass="txtstyle" />
                                <EmptyDataRowStyle CssClass="text" />
                            </asp:GridView>
                            <asp:Label ID="lblDomainError" runat="server" CssClass="text" EnableViewState="False">
                            </asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel runat="server" ID="pnlNoDomainRecords" Visible="false">
        <table width="100%" style="border: solid 1px black">
            <tr style="background-color: #BFBFBF">
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Domain Details" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="text">
                    No Records found.
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click"
                    Visible="False" BackColor="#003399" ForeColor="White" Width="119px" CausesValidation="false" />
                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" Visible="False"
                    BackColor="#003399" ForeColor="White" Width="119px" CausesValidation="false" />
            </td>
            <td align="right">
                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" Visible="false"
                    Width="119px" CssClass="button" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CausesValidation="true"
                    ValidationGroup="vgAddProject" Width="119px" OnClientClick="return Validate();"
                    CssClass="button" TabIndex="18" />
                <%--Sanju:Issue Id 50201:changed width(firefox issue )--%>
                <asp:Button ID="btnEditResourcePlan" runat="server" Text="Edit Resource Plan" OnClick="btnEditResourcePlan_Click"
                    Visible="false" Width="160px" CssClass="button" />
                <%-- Sanju:Issue Id 50201 End--%>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                    Width="119px" CausesValidation="false" CssClass="button" TabIndex="19" />
                <asp:HiddenField ID="hidmainTabProject" runat="server" Value="0" />
                <asp:HiddenField ID="hidContractEndDt" runat="server" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfProjectEndtDts" runat="server" />
</asp:Content>
