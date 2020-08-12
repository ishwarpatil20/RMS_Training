<%@ page language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" CodeFile ="~/AddContract.aspx.cs" inherits="AddContract" enableeventvalidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %> 

<asp:Content ID ="Content" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinny.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $("#<%=BtnSelectExistingPrj.ClientID %>").on("click", function(e) {
                    $.modalDialog.create({ url: "listofProject.aspx", maxWidth: 1000,
                        onclose: function(e) {
                            location.reload(true);
                        }
                    }).open();
                });
            });
        })(jQuery);
        //Umesh: Issue 'Modal Popup issue in chrome' Ends
        
        setbgToTab('ctl00_tabContract', 'ctl00_spanAddContract');

        function $(v)
        { return document.getElementById(v); }

        function ButtonClickValidateContract() {

            debugger;
            var spanlist = "";
            var txtContractValue = $('<%=txtContractValue.ClientID %>');

            var txtContractrefId = $('<%=txtContractrefId.ClientID %>').id;

            var txtDocumentName = $('<%=txtDocumentName.ClientID %>').id;

            var ContractTypeDLL = $('<%=ddlContractType.ClientID %>').value;

            var ProjectHeadDLL = $('<%=ddlProjectHead.ClientID %>').value;

            //Siddharth 13th March 2015 Start
            var ProjectModel = $('<%=ddlProjectModel.ClientID %>').value;
            //Siddharth 13th March 2015 Start
                    

            var EmailId = $('<%=txtEmailId.ClientID %>').id;

            var PrjDetails = $('<%=IsProjectDetailsVisible.ClientID %>').value;

            if (ContractTypeDLL == "0") {
                var sTypeOfcontract = $('<%=spanzContractType.ClientID %>').id;
                spanlist += sTypeOfcontract + ",";
            }


            if (ProjectHeadDLL == "0") {
                var sProjectHead = $('<%=spanzProjectHead.ClientID %>').id;
                spanlist += sProjectHead + ",";
            }

//            //Siddharth 13th March 2015 Start
//            if (ProjectModel == "Select") {
//                var sProjectModel = $('<%=spanzProjectModel.ClientID %>').id;
//                spanlist += sProjectModel + ",";
//            }
//            //Siddharth 13th March 2015 End


            var AccountManagerDLL = $('<%=ddlAccountManager.ClientID %>').value;

            if (AccountManagerDLL == "0") {
                var sAM = $('<%=spanzAccountManager.ClientID %>').id;
                spanlist += sAM + ",";
            }

            var ddlClientName = $('<%=ddlClientName.ClientID %>').value;

            if (ddlClientName == "0") {
                var sClientName = $('<%=spanzClientName.ClientID %>').id;
                spanlist += sClientName + ",";
            }

            var ddlLocation = $('<%=ddlLocation.ClientID %>').value;

            if (ddlLocation == "0") {
                var sLocation = $('<%=spanzLocation.ClientID %>').id;
                spanlist += sLocation + ",";
            }


            var txtClientAbbrValue = $('<%=txtClientAbbr.ClientID %>').id;


            var ddlCurrencyType = $('<%=ddlcurrencyType.ClientID %>').value;
            
            
            

            var txtContractStartDateValue = $('<%=ucContractStartDate.ClientID %>').id;

            var txtContractEndDateValue = $('<%=ucContractEndDate.ClientID %>').id;

            //If Contract value is not blank  then currency type should be mandatory.
            if (txtContractValue.value != "" && txtContractValue.value != "0.00000" && ddlCurrencyType == "0") {
                var sCurrencyType = $('<%=spanzCurrencyType.ClientID %>').id;
                spanlist += sCurrencyType + ",";
            }

            var controlList;
            //fills the defined array with all the controls that are empty.
            if (spanlist == "") {
                //If Contract value is not blank  then currency type should be mandatory.
                if (txtContractValue.value.trim() == "" || txtContractValue.value != "0.00000") {
                    controlList = txtContractrefId + "," + txtDocumentName + "," + EmailId + "," + txtClientAbbrValue + "," + txtContractStartDateValue + "," + txtContractEndDateValue;
                }
                else {
                    controlList = txtContractrefId + "," + txtDocumentName + "," + EmailId + "," + txtClientAbbrValue + "," + txtContractValue.id + "," + txtContractStartDateValue + "," + txtContractEndDateValue;
                }
            }
            else {
                //If Contract value is not blank  then currency type should be mandatory.
                if (txtContractValue.value.trim() == "" || txtContractValue.value != "0.00000") {
                    controlList = spanlist + txtContractrefId + "," + txtDocumentName + "," + EmailId + "," + txtClientAbbrValue + "," + txtContractStartDateValue + "," + txtContractEndDateValue;
                }
                else {
                    controlList = spanlist + txtContractrefId + "," + txtDocumentName + "," + EmailId + "," + txtClientAbbrValue + "," + txtContractValue.id + "," + txtContractStartDateValue + "," + txtContractEndDateValue;
                }
            }

            if (ValidateControlOnClickEvent(controlList) == false) {
                lblMandatory = $('<%=lblMandatory.ClientID %>')
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
            }
            else 
            {
                return CheckContractDate();
            }

            var PrjNameid = $('<%=txtPrjName.ClientID %>');

            if (PrjNameid != null) {

                var PrjName = $('<%=txtPrjName.ClientID %>').value;

                var txtNoOfResources = $('<%=txtNoOfResources.ClientID %>').value;

                var txtStartDate = $('<%=ucDatePickerStart.ClientID %>').value;

                var EndDate = $('<%=ucDatePickerEnd.ClientID %>').value;

                var ProjectTypeDLL = $('<%=ddlTypeOfPrj.ClientID %>').value;

                var ProjGroupDLL = $('<%=ddlPrjGroup.ClientID %>').value;

                var ProjDivisionDLL = $('<%=ddlPrjDivision.ClientID %>').value;


                var ProjectHeadDLL = $('<%=ddlProjectHead.ClientID %>').value;
                
                var ProjBusinessAreaDLL = $('<%=ddlPrjBusinessArea.ClientID %>').value;
                
                var ProjBusinessSegmentDLL = $('<%=ddlPrjBusinessSegment.ClientID %>').value;

                var ProjectLocationDLL = $('<%=ddlPrjLocation.ClientID %>').value;

                var ProjectAbbr = $('<%=txtProjectAbbr.ClientID %>').id;

                var PhaseValue = $('<%=txtPhase.ClientID %>').id;


                var AllControlEmpty;

                if (PrjName == "" && txtNoOfResources == "" && txtStartDate == "" && EndDate == "" && ProjectTypeDLL == "0" && ProjectLocationDLL == "0" && ProjGroupDLL == "0"
                       && ProjAlias == "" && ProjBusinessSegmentDLL == "0" && ProjBusinessAreaDLL == "0" && ProjDivisionDLL == "0" && ProjectHeadDLL == "0") {
                    AllControlEmpty = "Yes";
                }
            }
            if (PrjDetails == "Yes" && AllControlEmpty != "Yes") {
                alert("To save the details of the project entered, kindly click on Add row");
                return false;
            }

            if (ValidateControlOnClickEvent(controlList) == true) {
                if (AllControlEmpty == "Yes" || PrjNameid == null) {
                    return Overwrite();
                }
            }
            

            return ValidateControlOnClickEvent(controlList);
        }

        

        function Overwrite() {
            var griddata = $('<%=gvProjectDetails.ClientID %>');
            var text = $('<%=lblAddContract.ClientID %>').innerText;

            if (griddata == null) {
                if (text == "Add Contract") {
                    //confirm("Are you sure, you want to Delete Contract?");     
                    if (confirm("Are you sure, you want to Save Contract without any project?") == true) return true;
                    else
                        return false;
                }
                if (text == "Edit Contract") {
                    if (confirm("Are you sure, you want to Edit Contract without any project?") == true) return true;
                    else
                        return false;
                }
            }

        }

        function SureTODelete() {
            var text = $('<%=tbReasonForDeletion.ClientID %>').value;
            var spanlist = null;
            if (text == "") {
                var reasonForDeletion = $('<%=tbReasonForDeletion.ClientID %>').id;
                spanlist = reasonForDeletion;
            }
            if (spanlist != null && ValidateControlOnClickEvent(spanlist) == false) {
                var lblMandatory = $('<%=lblMandatory.ClientID %>');
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
                return false;
            }
            else {
                if (spanlist == null) {
                    if (confirm("Are you sure, you want to Delete Contract?") == true) return true;
                    else
                        return false;
                }
            }

        }

        function CheckDate() {
            var txtStartDate = $('<%=ucDatePickerStart.ClientID %>').value;
            var txtEndDate = $('<%=ucDatePickerEnd.ClientID %>').value;
            if (compareDates(txtStartDate, 'dd/MM/yyyy', txtEndDate, 'dd/MM/yyyy') == 1) {
                alert("End date should be greater than start date.");
                return false;
            }
            else
                return true;
        }

        //checks the special character.
        function DenySpecialChar(evt) {
            if ((event.keyCode > 32 && event.keyCode < 45) || (event.keyCode > 46 && event.keyCode < 48) || (event.keyCode > 57 && event.keyCode < 65) || (event.keyCode > 90 && event.keyCode < 97) || (event.keyCode > 122 && event.keyCode < 127))
                event.returnValue = false;
            return true;
        }

        function fnValidateInput(sender, args) {
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

        // This function checks the maxlength of field when data is paste in control
        function fnLimitPaste(targetControl, maxLength) {
            var clipboardText = window.clipboardData.getData("Text").length;
            var totalLength = clipboardText + targetControl.value.length;

            if (totalLength > maxLength) {
                return false;
            }
            return true;
        }

        function fnLimitCheckOnEnter(targetControl, maxLength, e) {

            if (window.event) {
                e = window.event;
            }
            if (e.keyCode == 13) {
                return fnLimitPaste(targetControl, maxLength);
            }
            else {
                return true;
            }
        }

        //checks the special character apart from [(,),.,-] .
        function DenySpecialCharExceptSome(evt) {
            //".","(",")","-",""" ,"/"        
            if (event.keyCode == 46 || event.keyCode == 40 || event.keyCode == 41 || event.keyCode == 45 || event.keyCode == 48 || event.keyCode == 47) {
                return true;
            }
            else {
                if ((event.keyCode > 32 && event.keyCode < 45) || (event.keyCode > 46 && event.keyCode < 48) || (event.keyCode > 57 && event.keyCode < 65) || (event.keyCode > 90 && event.keyCode < 97) || (event.keyCode > 122 && event.keyCode < 127))
                    event.returnValue = false;
                return true;
            }
        }

        function RestrictDateTypingAndPaste(targetControl) {

            if (targetControl.value != "") {
                targetControl.value = "";
            }
        }

        //Function to allow only integer and Decimal point
        // restricted multiple decimal point and only two digit after Decimal.
        function IntWithDecimal(event, targetControl) {
            var Point = targetControl.value.split('.');
            var regExObj = new RegExp();
            regExObj = /^([1-9]{0,1})([0-9]{0,1})([0-9]{0,1})+((\.{0,1})([0-9]{0,1}))$/;

            //Sanju:Issue Id 50201:Added condition for firefox events.which event(backspace and delete were not working)
            var code = (event.keyCode) ? event.keyCode : event.which;
            if (code == 8 || code == 46)
                return true;
            //Sanju:Issue Id 50201 End

            if (code == 46 || (code >= 48 && code <= 57)) {

                if (targetControl.value != '') {
                    //Condition to check multiple Decimal point.
                    if (Point.length == 2 && code == 46) {
                        return false;
                    }
                    if (regExObj.test(targetControl.value, '')) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
            }
            else {
                return false;
            }
        }

        function ButtonClickValidate() {
            debugger;
            var lblMandatory;
            var spanlist = "";
            var PrjNameId = $('<%=txtPrjName.ClientID %>').id;
            var txtNoOfResources = $('<%=txtNoOfResources.ClientID %>').id;
            var txtStartDate = $('<%=ucDatePickerStart.ClientID %>').id;

            var SelectDate = $('<%=ucDatePickerStart.ClientID %>').id;

            var EndDate = $('<%=ucDatePickerEnd.ClientID %>').id;

            var txtStartDateVal = $('<%=ucDatePickerStart.ClientID %>').value;

            var SelectDateVal = $('<%=ucDatePickerStart.ClientID %>').value;

            var EndDateVal = $('<%=ucDatePickerEnd.ClientID %>').value;

            var ProjectTypeDLL = $('<%=ddlTypeOfPrj.ClientID %>').value;

            var ProjModelDLL = $('<%= ddlProjectModel.ClientID %>').value;


            var ProjGroupDLL = $('<%=ddlPrjGroup.ClientID %>').value;

            var ProjectHeadDLL = $('<%=ddlProjectHead.ClientID %>').value;
            
            

            var ProjDivisionDLL = $('<%=ddlPrjDivision.ClientID %>').value;
            var ProjBusinessAreaDLL = $('<%=ddlPrjBusinessArea.ClientID %>').value;
            var ProjBusinessAreaDLLEnabled = $('<%=ddlPrjBusinessArea.ClientID %>').disabled;
            var ProjBusinessSegmentDLL = $('<%=ddlPrjBusinessSegment.ClientID %>').value;
            var ProjBusinessSegmentDLLEnabled = $('<%=ddlPrjBusinessSegment.ClientID %>').disabled;

          
            var txtProjectDescriptionVal = $('<%=txtDescription.ClientID %>').value;

            var txtProjectDescription = $('<%=txtDescription.ClientID %>').id;

            var txtProjectCategory = $('<%=ddlProjectCategory.ClientID %>').value;

            var txtProjectAbbr = $('<%=txtProjectAbbr.ClientID %>').id;

            var txtPhase = $('<%=txtPhase.ClientID %>').id;


            if ($('<%=IsNewProject.ClientID %>').value != 'No') {
                if (ProjectTypeDLL == "0") {
                    var sTypeOfPrj = $('<%=spanzTypeOfPrj.ClientID %>').id;
                    spanlist += sTypeOfPrj + ",";
                }

                if (txtProjectCategory == "" || txtProjectCategory == "SELECT" || txtProjectCategory == "0") {
                    var sProjectCategory = $('<%=spanzProjectCategory.ClientID %>').id;
                    spanlist = spanlist + sProjectCategory + ",";
                }

                if (ProjGroupDLL == "" || ProjGroupDLL == "Select" || ProjGroupDLL == "0") {
                    var sProjGroup = $('<%=spanzPrjGroup.ClientID %>').id;
                    spanlist += sProjGroup + ",";
                }

                
                if (ProjModelDLL == "" || ProjModelDLL == "Select" || ProjModelDLL == "0") {
                    var sProjModel = $('<%=spanzProjectModel.ClientID %>').id;
                    spanlist += sProjModel + ",";
                }
                if (ProjectHeadDLL == "" || ProjectHeadDLL == "SELECT" || ProjectHeadDLL == "0") {
                    var sProjectHead = $('<%=spanzProjectHead.ClientID %>').id;
                    spanlist += sProjectHead + ",";
                }

                if (ProjDivisionDLL == "" || ProjDivisionDLL == "Select" || ProjDivisionDLL == "0") {
                    var sProjDivision = $('<%=spanzPrjDivision.ClientID %>').id;
                    spanlist += sProjDivision + ",";
                }
                if (!ProjBusinessAreaDLLEnabled) {
                    if (ProjBusinessAreaDLL == "" || ProjBusinessAreaDLL == "Select" || ProjBusinessAreaDLL == "0") {
                        var sProjBusinessArea = $('<%=spanzPrjBusinessArea.ClientID %>').id;
                        spanlist += sProjBusinessArea + ",";
                    }
                }
                if (!ProjBusinessSegmentDLLEnabled) {
                    if (ProjBusinessSegmentDLL == "" || ProjBusinessSegmentDLL == "Select" || ProjBusinessSegmentDLL == "0") {
                        var sProjBusinessSegment = $('<%=spanzPrjBusinessSegment.ClientID %>').id;
                        spanlist += sProjBusinessSegment + ",";
                    }
                }
                
              }
              
            // 36233-Ambar-29062012            
            // Added txtProjectDescription to the validation list
            if (spanlist == "") {
                var controlList = PrjNameId + "," + SelectDate + "," + EndDate + "," + txtProjectAbbr + "," + txtPhase +","+ txtProjectDescription ;
            }
            else {
                var controlList = spanlist + PrjNameId + "," + SelectDate + "," + EndDate + "," + txtProjectAbbr + "," + txtPhase +","+txtProjectDescription;
            }

            if (ValidateControlOnClickEvent(controlList) == false) {
                var lblMandatory = $('<%=lblMandatory.ClientID %>');
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
                return ValidateControlOnClickEvent(controlList);
            }
            else {
                return CheckDate();
            }
            return ValidateControlOnClickEvent(controlList);

        }

        function CRButtonClickValidate() {
            var lblMandatory;
            var spanlist = "";
            var CRReferenceNo = $('<%=txtCRReferenceNo.ClientID %>').id;
            var DatePickerCRStartDate = $('<%=ucDatePickerCRStartDate.ClientID %>').id;
            var DatePickerCREndDate = $('<%=ucDatePickerCREndDate.ClientID %>').id;

            if (spanlist == "") {
                var controlList = CRReferenceNo + "," + DatePickerCRStartDate + "," + DatePickerCREndDate; //+","+ txtProjectDescription ;

            }

            if (ValidateControlOnClickEvent(controlList) == false) {
                var lblMandatory = $('<%=lblMandatory.ClientID %>');
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
                return ValidateControlOnClickEvent(controlList);
            }
            else {
                return CheckCRDate();
            }
            return ValidateControlOnClickEvent(controlList);

        }

        function CheckContractDate() {
            var txtStartDate = $('<%=ucContractStartDate.ClientID %>').value;
            var txtEndDate = $('<%=ucContractEndDate.ClientID %>').value;
            if (compareDates(txtStartDate, 'dd/MM/yyyy', txtEndDate, 'dd/MM/yyyy') == 1) {
                alert("Contract End Date should be greater than Contract Start Date.");
                return false;
            }
            else
                return true;
        }

        function CheckCRDate() {
            var txtStartDate = $('<%=ucDatePickerCRStartDate.ClientID %>').value;
            var txtEndDate = $('<%=ucDatePickerCREndDate.ClientID %>').value;
            if (compareDates(txtStartDate, 'dd/MM/yyyy', txtEndDate, 'dd/MM/yyyy') == 1) {
                alert("End Date should be greater than Start Date.");
                return false;
            }
            else
                return true;
        }

 </script>
   
    <table>
 <table style="width: 100%">
        <tr>     
            <%--Siddharth 28th August 2015 Start--%>
           <td align="center" class="header_employee_profile"
                style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1'); width: 100%;">
               <%--Siddharth 28th August 2015 End--%>
                <asp:Label ID="lblAddContract" runat="server" Text="Add Contract" CssClass="header"></asp:Label>
            </td>
        </tr>
    </table>
 <table style="width: 104%">
        <tr>
            <td style="width: 860px">
                <%--Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                       Desc : project group should be mandatory while creation of project otherwise Allocation  and Billing report gives error --%>
                <asp:Label ID="lblConfirmMessage" runat="server" CssClass="Messagetext"></asp:Label>

            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt" CssClass ="Messagetext" >Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
            </td>
        </tr>
    </table>
 <div class="detailsborder">
        
        <table class="detailsbg" width = "100%">
            <tr>
                <td>
                    <asp:Label ID="lblContractDetailsGroup" runat="server" Text="Contract Details" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="border: solid 1px black">
            <table width="100%">
                <tr>
                    <td style="width: 50%;" valign="top">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 141px;">
                                    <asp:Label ID="lblContractCode" runat="server" Text="Contract Code" CssClass="txtstyle"></asp:Label>                                   
                                </td>
                                <td "width: 4%" visible="false">
                           <span id="spanContractCode" runat="server">
                                 <img id="imgContractCode" runat="server" src="Images/cross.png" alt="" 
                                        style="display: none; border: none;" align="left"/>
                            </span>
                           </td>
                                <td style="width: 278px">
                                    <asp:TextBox ID="txtContractCode" runat="server" CssClass="mandatoryField" MaxLength="30"
                                        ToolTip="Contract Code" BorderStyle="Solid" 
                                        AutoComplete="off" Enabled="False" Width="210px"></asp:TextBox>
                                    <span id="spanzProjectNameTooltip" runat="server">
                                        <img id="imgProjectName" src="Images/cross.png" alt="" style="display: none; border: none;"
                                            onmouseover="ShowTooltip('spanProjectNameTooltip', 'Project Name is not in correct format.');"
                                            onmouseout="HideTooltip('spanProjectNameTooltip');" />
                                    </span>
                                </td>
                                <td style="width: 149px">
                                <asp:Label ID="lblContractStatus" runat="server" Text="Contract Status" CssClass="txtstyle"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryContractStatus" runat = "server" visible = "false">*</label>
                                </td>
                                <td>
                                 <span id="spanzContractStatus" runat = "server" Width="179px">
                                        <asp:DropDownList ID="ddlContractStatus" runat="server" ToolTip="Select Contract Status"
                                            Width="215px" CssClass="mandatoryField" >
                                        </asp:DropDownList>
                                    </span>
                                </td>
                                </tr>
                            <tr width="210px">
                                <td style="height: 26px; width: 141px;">
                                 <asp:Label ID="lblContractType" runat="server" Text="Contract Type" CssClass="txtstyle"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryContractType" runat = "server" >*</label>
                                </td>
                                <td style="width: 4%; height: 26px;"></td>
                                <td style="height: 26px; width: 278px;">
                                 <span id="spanzContractType" runat = "server">
                                        <asp:DropDownList ID="ddlContractType" runat="server" ToolTip="Select contract type"
                                            Width="215px" CssClass="mandatoryField" 
                                        OnSelectedIndexChanged="ddlContractType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </span> 
                                </td>
                                <td style="width: 149px; height: 26px;">
                                    <asp:Label ID="lblAccountManager" runat="server" Text="Account Manager" CssClass="txtstyle"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryAccountManager" runat = "server">*</label>
                                </td>
                                <td style="height: 26px">
                                     <span id="spanzAccountManager" runat = "server"  Width="179px">
                                        <asp:DropDownList ID="ddlAccountManager" runat="server" ToolTip="Select Account Manager"
                                            Width="215px" CssClass="mandatoryField" 
                                        OnSelectedIndexChanged="ddlAccountManager_SelectedIndexChanged" 
                                        AutoPostBack="True">
                                        </asp:DropDownList>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 141px;">
                                    <asp:Label ID="lblContractrefId" runat="server" Text="Contract Ref Id" CssClass="txtstyle"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryContractrefId" runat = "server">*</label>
                                </td>
                                <td style="width: 4%">                                
                                <span id="spanContractrefId" runat="server">
                                 <img id="imgContractrefId" runat="server" src="Images/cross.png" alt="" 
                                        style="display: none; border: none;" align="left" />
                                 </span>
                                 </td>
                                <td style="width: 278px">
                                    <span id="spanzContractrefId" runat = "server">
                                        <asp:TextBox ID="txtContractrefId" runat="server" CssClass="mandatoryField" MaxLength="100"
                                            ToolTip="Enter Contract Ref Id." BorderStyle="Solid" 
                                        AutoComplete="off" Width="210px" onkeypress="return DenySpecialCharExceptSome(event);"></asp:TextBox>
                                    </span>
                                </td>
                                <td style="width: 149px">
                                   <asp:Label ID="lblEmailId" runat="server" Text="Email Id " CssClass="txtstyle"></asp:Label>
                                    <label class="mandatorymark" id = "MandatoryEmailId" runat = "server">
                                        *</label>
                                </td>
                                <td>                              
                                    
                                     <span id="spanzEmailId" runat="server">
                                        <asp:TextBox ID="txtEmailId" runat="server" CssClass="mandatoryField" MaxLength="30"
                                            ToolTip="Email Id" BorderStyle="Solid" AutoComplete="off"  
                                        ReadOnly = "true" Width="210px" Enabled="False"></asp:TextBox>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 141px;">
                                    <asp:Label ID="lblContractValue" runat="server" Text="Contract Value" CssClass="txtstyle"></asp:Label>
                                    <%--  <tr>
                    <td style="width: 251px">
                       
                        
                       
                    </td>
                    <td "width: 3%" visible="false" style="width: 72px">
                     
                    </td>
                    <td style="width: 372px;" >
                        
                      
                        
                    </td>
                    </tr>--%>
                                </td>
                                <td style="width: 4%">
                                <span id="SpanConractValue" runat="server">
                                 <img id="imgContractvalue" runat="server" src="Images/cross.png" alt="" 
                                        style="display: none; border: none;" align="left" />
                                 </span>
                                </td>
                                <td style="width: 278px"> 
                                    <span id="spanTbContractValue" runat = "server">
                                        <asp:TextBox ID="txtContractValue" runat="server" CssClass="mandatoryField" MaxLength="24"
                                            ToolTip="Enter Contract Value" BorderStyle="Solid" 
                                        AutoComplete="off" Width="210px" 
                                        onkeypress="return IntWithDecimal(event,this);"></asp:TextBox>
                                    </span>
                                </td>
                                <td style="width: 149px">
                                    <asp:Label ID="LblCurrency" runat="server" Text="Currency Type" CssClass="txtstyle"></asp:Label>
                                    <%-- </table>
            <table>--%>
                                </td>
                                <td>
                                    <span id="spanzCurrencyType" runat = "server" Width="179px">
                                        <asp:DropDownList ID="ddlcurrencyType" runat="server" ToolTip="Select Currency Type"
                                            Width="215px" CssClass="mandatoryField" >
                                        </asp:DropDownList>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 141px;">
                                    <asp:Label ID="Label3" runat="server" Text="Contract Start Date" CssClass="txtstyle"></asp:Label>
                                    <label class="mandatorymark" id = "Label6" runat = "server">
                                        *</label>
                                    </td>
                                <td style="width: 4%">
                                     </td>
                                <td style="width: 278px"> 
                                    <uc1:DatePicker ID = "ucContractStartDate" runat="server"  />
                                    <span id="SpanErrorContractStartDate" runat="server">
                                 <img id="imgErrorContractStartDate" runat="server" src="Images/cross.png" alt="" 
                                        style="display: none; border: none;" align="left" />
                                 </span>
                                    </td>
                                <td style="width: 149px">
                                   <asp:Label ID="Label4" runat="server" Text="Contract End Date" CssClass="txtstyle"></asp:Label>
                                    <label class="mandatorymark" id = "Label5" runat = "server">
                                        *</label></td>
                                <td>
                                    <span id="SpanErrorContractEndDate" runat="server">
                                 <img id="imgErrorContractEndDate" runat="server" src="Images/cross.png" alt="" 
                                        style="display: none; border: none;" align="left" />
                                 </span>
                                    <uc1:DatePicker ID = "ucContractEndDate" runat="server" />
                                    
                                    </td>
                            </tr>
                            
                            <!-- Siddharth 13 march 2015 Start-->
                            <%-- Mohamed : Issue 49791 : 15/09/2014 : Ends--%>
                            <!-- Siddharth 13 march 2015 End-->
                            
                            </table>
                        <table>
                            <tr>
                                <td style="width: 141px;">
                                    <asp:Label ID="lblDocumentName" runat="server" Text="Document Name" CssClass="txtstyle">
                                    </asp:Label>
                                    <label class="mandatorymark" id = "MandatoryDocumentName" runat = "server" >
                                        *</label>
                                </td>
                                <%--Sanju:Issue Id 50201:Alignment issue resolved--%>
                                <td class="contract_document_name">
                                    <%--Sanju:Issue Id 50201:End--%>
                                <span id="spanDocumentName" runat="server">
                                 <img id="imgDocumentName" runat="server" src="Images/cross.png" alt="" 
                                        style="display: none; border: none;" align="left" />
                                 </span>
                                </td>
                                <td style="width: 737px">
                                    <span id="spanzDocumentName" runat = "server">
                                        <asp:TextBox ID="txtDocumentName" runat="server" CssClass="mandatoryField" MaxLength="100"
                                            ToolTip="Enter Document Name" BorderStyle="Solid" 
                                        AutoComplete="off" Width="433px" 
                                        onkeypress="return DenySpecialCharExceptSome(event);" Height="33px" 
                                        TextMode="MultiLine"></asp:TextBox>
                                    </span>
                                </td>
                                <%-- OnClick = "btnDeleteImg_Click" --%>
                            </tr>
                            
                            <tr>
                                <td style="width: 141px;">                                    
                                   <asp:Label ID="lblReasonOfDeletion" runat="server" Text="Reason for Deletion" Visible="False" CssClass="txtstyle"></asp:Label>
                                    <label class="mandatorymark" id = "MandatoryReasonOfDeletion" runat = "server" visible= "false" >*</label>
                                </td>
                                <td style="width: 4%">
                                <span id="spanReasonForDeletion" runat = "server">
                                 <img id="imgReasonForDeletion" runat="server" src="Images/cross.png" alt="" 
                                        style="display: none; border: none;" align="left" />
                                 </span>
                                </td>
                                <td style="width: 737px">
                                <span id="span1" runat = "server">                                
                                   <asp:TextBox ID="tbReasonForDeletion" runat="server" TextMode="MultiLine" ToolTip="Enter Reason for Deletion"
                                        Visible="False" Width="431px" MaxLength="100" 
                                        ></asp:TextBox>
                                   </span>
                                    <%--OnClick = "btnEdit_Click"--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div style="border: solid 1px black">
            <table  class="detailsbg" style="width: 100%">
                <tr>
                    <td style="width: 100%" >
                        <asp:Label ID="lblClientlable" CssClass="detailsheader" runat="server" Text="Client Details" ></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 151px;">
                        <asp:Label ID="lblClientName" runat="server" Text="Client Name" CssClass="txtstyle"></asp:Label>
                        <label class="mandatorymark" id = "MandatoryClientName" runat = "server">
                            *</label>
                    </td>
                    <td style="width: 3%">
                    </td>
                    <td style="width: 282px">
                        <span id="spanzClientName" runat = "server" Width="155px">
                            <asp:DropDownList ID="ddlClientName" runat="server" ToolTip="Select Client Name"
                                Width="212px" CssClass="mandatoryField" 
                            onselectedindexchanged="ddlClientName_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </span>
                    </td>
                    
                    <td style="width: 147px;">
                        <asp:Label ID="lblLocation" runat="server" Text="Location" CssClass="txtstyle"></asp:Label>
                        <label class="mandatorymark" id = "MandatoryLocation" runat = "server">
                            *</label>
                    </td>
                    <td>
                        <span id="spanzLocation" runat = "server" Width="155px">
                            <asp:DropDownList ID="ddlLocation" runat="server" ToolTip="Select Location" Width="215px"
                                CssClass="mandatoryField" >
                            </asp:DropDownList>
                        </span>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 151px;">
                        <asp:Label ID="lblDivision" runat="server" Text="Division" CssClass="txtstyle"></asp:Label>                        
                    </td>
                    <td style="width: 3%">
                    </td>
                    <td style="width: 282px">
                        
                        <asp:TextBox ID="txtDivision" runat="server" Width="210px" MaxLength="50"></asp:TextBox>
                        
                    </td>
                    
                    <td style="width: 147px;">
                        <asp:Label ID="lblSponsor" runat="server" Text="Sponsor" CssClass="txtstyle"></asp:Label>
                        
                    </td>
                    <td>
                        <asp:TextBox ID="txtSponsor" runat="server" Width="210px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 151px;">
                        <asp:Label ID="lblClientAbbr" runat="server" Text="Client Abbreviation" CssClass="txtstyle"></asp:Label>
                         <label class="mandatorymark" id = "lblMandatoryClientAbbr" runat = "server">
                            *&nbsp; </label>                         
                    &nbsp;</td>
                    <td style="width: 3%">
                        &nbsp;</td>
                    <td style="width: 282px">
                        <asp:TextBox ID="txtClientAbbr" runat="server" Width="210px" MaxLength="12"></asp:TextBox>
                        &nbsp;</td>
                    
                    <td style="width: 147px;">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <br />
        <table style="width: 100%">
            <tr>
                <%--Sanju:Issue Id 50201:Alignment issue resolved--%>            
                <td align="center" style="width: 175px">
                    &nbsp;<input id="BtnSelectExistingPrj" runat="server" value="Select existing Project"
                        class="button" style="width:162px;"  /></td>
                <td align="Left">
                    <%--Sanju:Issue Id 50201:End--%>
                    <asp:Button ID="btnAddNewPrj" runat="server" Text="Add new Project" Width="150px"
                        CssClass="button" OnClick="btnAddNewPrj_Click" />&nbsp;&nbsp;&nbsp;
                   
                </td>
                <td>
                </td>
            </tr>
        </table>
        <%--
                    <td style="width: 224px">
                      
                    </td>
                  
                    <td "width: 4%" visible="false" style="width: 50px">
                   
                    </td>
                    
                    <td style="width: 640px">
                        
                    </td>--%>
       <asp:Panel id="divCRDetails" runat = "server"  Visible="false">
            <table class="detailsbg" style="width: 100%">
                <tr>
                    <td style="width: 100%">
                        <asp:Label ID="Label1" runat="server" Text="CR Details" CssClass="detailsheader" ></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlCRDetails" runat="server">
            <table style="width: 100%">         
                <tr>
                    <td style="width: 328px">             
                        <asp:Label ID="lblCRReferenceNo" runat="server" Text="CR Reference No"  CssClass="txtstyle" ></asp:Label>
                       <label id="Label2" class="mandatorymark"  runat = "server">
                            *</label>
                    </td>
                    <td "width: 3%" style="width: 48px">
                     
                    </td>
                    <td style="width: 402px;" >
                        
                      <span id="spanCRReferenceNo" runat="server" >
                            <asp:TextBox ID="txtCRReferenceNo" runat="server" CssClass="mandatoryField" MaxLength="20"
                                ToolTip="Enter CR Reference No" BorderStyle="Solid" 
                            AutoComplete="off"   Width="210px"  
                            ></asp:TextBox>
                        </span>
                        
                    </td>
                    <td style="width: 216px">
                        <asp:Label ID="lblCRStartDate" runat="server" Text="Start Date"  CssClass="txtstyle"></asp:Label>
                        <label id="Label12" class="mandatorymark"  runat = "server" >
                            *</label>
                    </td>
                    <td "width: 4%"  style="width: 80px">
                         <span id="span12" runat="server">
                             <img id="img6" runat="server" src="Images/cross.png" alt="" 
                             style="display: none; border: none;" align="left"/>
                        </span>
                    </td>
                    <td style="width: 640px">
                        <span id="span15" runat="server">
                             <uc1:DatePicker ID = "ucDatePickerCRStartDate" runat="server" />
                            
                            </span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 328px">
                        <asp:Label ID="Label13" runat="server" Text="End Date"  CssClass="txtstyle"></asp:Label>
                        <label id= "Label14" class="mandatorymark" runat = "server">
                            *</label>
                    </td>
                    <td "width: 3%" style="width: 48px">
                         <span id="span14" runat="server">
                             <img id="img7" runat="server" src="Images/cross.png" alt="" 
                             style="display: none; border: none;" align="left"/>
                        </span>
                    </td>
                    <td style="width: 402px">
                        <span id="span17" runat="server">                        
                             <uc1:DatePicker ID = "ucDatePickerCREndDate" runat="server" />
                             </span>
                        
                    </td>
                    <td style="width: 216px;">
                        <asp:Label ID="Label17" runat="server" Text="Remarks" CssClass="txtstyle" ></asp:Label>
                         
                   </td>
                    <td  "width: 4%"  style="width: 80px">
                        <span id="span16" runat="server">
                             <img id="img8" runat="server" src="Images/cross.png" alt="" 
                            style="display: none; border: none;" align="left"/>
                        </span>
                    </td>
                    <td style="width: 640px">
                       <span id="spanRemarks" runat="server">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="mandatoryField" MaxLength="100"
                                ToolTip="Enter Remarks" BorderStyle="Solid" AutoComplete="off"
                                Width ="210px" ></asp:TextBox>
                        </span> 
                    </td>
                </tr>              
                </table>
            </asp:Panel>
            <table>
                <tr>
                    <td style="width: 266px">
                        <asp:HiddenField ID="HdfContractId" runat="server"/></td>
                   <td style="width: 46px">
                   </td>
                    <td style="width: 400px">
                        <asp:HiddenField ID="HdfProjectCode" runat="server"/>
                                                        </td>
                  <%--<td style="width: 182px">
                       
                    </td>--%>
                    <%--<td style="width: 50px">
                        &nbsp;</td>--%>
                    <td align="left" style="width: 490px" colspan="2">
                       <span>
                        <asp:Button ID="btnAddCRRow" runat="server" Text="Add Row"  CssClass="button"
                            onclick="btnAddCRRow_Click"   />                           
                        </span>&nbsp;                        
                        <span>                        
                        <asp:Button ID="btnCancelCRRow" runat="server" Text="Cancel" CssClass="button"
                         onclick="btnCancelCRRow_Click"  />&nbsp;  
                        <asp:Button ID="btnUpdateCRRow" runat="server" Text="Update" CssClass="button"
                         onclick="btnUpdateCRRow_Click"  Visible="false"/>&nbsp;&nbsp;  
                        <asp:Button ID="btnUpdateCancelCRRow" runat="server" Text="Cancel" CssClass="button"
                         onclick="btnUpdateCancelCRRow_Click" Visible="false" />
                        </span>
                        &nbsp;&nbsp; 
                   
                    </td>
                </tr> 
            </table>
        <%--</div>--%>
        <asp:GridView ID="gvCRDetails" runat="server" Height="1px" AutoGenerateColumns="False"
                                EditIndex="0" SelectedIndex="0"  Width="100%" 
                                onrowdeleting="gvCRDetails_RowDeleting" onrowediting="gvCRDetails_RowEditing">
                                <HeaderStyle CssClass="addrowheader" />
                                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                <RowStyle CssClass="textstyle" />
                                <Columns >
                                
                                    <asp:TemplateField Visible="false">
                                     <ItemTemplate>
                                        <asp:Label ID="CRId" runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.CRId") %>'></asp:Label>
                                      </ItemTemplate>
                                     </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="CRProjectCode" HeaderText="Project Code" ReadOnly = "true" />
                                    
                                    <asp:BoundField DataField="CRReferenceNo" HeaderText="CR Reference No" ReadOnly = "true" />
                                   
                                    <asp:BoundField DataField="CRStartDate" HeaderText="Start Date"  DataFormatString="{0:dd/MM/yyyy}" ReadOnly = "true"/>
                                   
                                    <asp:BoundField DataField="CREndDate" HeaderText="End Date"  DataFormatString="{0:dd/MM/yyyy}" ReadOnly = "true"  />

                                    <asp:BoundField DataField="CRRemarks" HeaderText="Remarks" ReadOnly = "true" />
                                    
                                    <asp:TemplateField Visible="true">    
                                        <ItemTemplate>                                         
                                                <asp:ImageButton ID ="imgDelete" runat = "server" ImageUrl = "~/Images/Delete.gif" 
                                                style="border: none; cursor: hand;" ToolTip = "Delete" 
                                                 CommandArgument='<%# Eval("CRId") %>' CommandName="Delete"/>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField Visible="true">                 
                                        <ItemTemplate>                                         
                                                <asp:ImageButton ID ="imgEdit" runat = "server" ImageUrl = "~/Images/Edit.gif" 
                                                style="border: none; cursor: hand;" ToolTip = "Edit"   CommandArgument='<%# Eval("CRId") %>' CommandName="Edit" /> 
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
        </asp:Panel>
        <br />
        <div id="divProjectDetails" runat = "server"  >
        <asp:Panel ID="PanelProjectDetails" Visible="false" runat="server">
            <table class="detailsbg" style="width: 100%" visible= "false">
                <tr>
                    <td style="width: 100%">
                        <asp:Label ID="lblProjectDetails" runat="server" Text="Project Details" CssClass="detailsheader" Visible = "true" ></asp:Label>
                    </td>
                </tr>
            </table>
            </asp:Panel>
            <table  visible="false" style="width: 101%">
                <tr>
                    <td style="width: 251px" visible="false" >
                        <asp:Label ID="lblProjectCode" runat="server" Height="22px" Text="Project Code" Visible="false" CssClass="txtstyle"></asp:Label>
                    </td>
                    <td "width: 3%" visible="false" style="width: 72px"  >
                        <span id="spanzProjectCodes" runat="server">
                             <img id="imgProjectCode" runat="server" src="Images/cross.png" alt="" 
                            style="display: none; border: none;" align="left"/>
                        </span>
                    </td>
                    <td style="width: 372px">
                        <span id="spanzProjectCode" runat="server">
                            <asp:TextBox ID="txtProjectCode" runat="server" CssClass="mandatoryField" MaxLength="30"
                                ToolTip="Enter Project Code" BorderStyle="Solid" AutoComplete="off"
                                Enabled="False"  Width ="210px"  Visible="false"  ></asp:TextBox>
                        </span>
                    </td>
                    <td style="width: 216px;">  
                      <asp:Label ID="lblProjectCategory" runat="server" Text="Project Category" 
                                       CssClass="txtstyle" Visible="False"></asp:Label>
                        <label id="MandatoryProjectCategory" class="mandatorymark" runat = "server" 
                            visible = "false">
                            *</label>              
                    </td>
                    <td "width: 4%" visible="false" style="width: 38px">  
                     <span id="spanPrjCategory" runat="server">
                             <img id="imgProjectCategory" runat="server" src="Images/cross.png" alt="" 
                                style="display: none; border: none;"/>
                        </span>                
                    </td>
                    <td style="width: 640px">                        
                     <span id="spanzProjectCategory" runat="server"  width="155px" >
                            <asp:DropDownList ID="ddlProjectCategory" runat="server" ToolTip="Select Project Category"
                                Width="215px" CssClass="mandatoryField" Visible="False" >
                            </asp:DropDownList>
                        </span></td>
                </tr>
                <%--Mohamed : Issue  : 19/09/2014 : Starts                        			  
                Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page--%>
                <tr>
                    <td style="width: 251px" visible="false" >
                         <asp:Label ID="lblPrjDivision" runat="server" Text="Division" visible="false" CssClass="txtstyle"></asp:Label>
                        <label id = "MandatoryProjectjDivision" class="mandatorymark" runat="server" visible= "false">
                            *</label>
                    </td>
                    <td "width: 3%" visible="false" style="width: 72px"  >
                        <span id="SpanzProjectDivision" runat="server">
                             <img id="imgProjectjDivision" runat="server" src="Images/cross.png" alt="" 
                            style="display: none; border: none;" align="left"/>
                        </span>  
                    </td>
                    <td style="width: 372px">
                       <span id="spanzPrjDivision" runat="server">
                            <asp:DropDownList ID="ddlPrjDivision" runat="server" ToolTip="Select Project Division"
                                Width="212px" CssClass="mandatoryField" visible="false" 
                            onselectedindexchanged="ddlPrjDivision_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </span>
                    </td>
                    <td style="width: 216px;">
                        <asp:Label ID="lblPrjBusinessArea" runat="server" Text="Business Area" visible="false" CssClass="txtstyle"></asp:Label>
                        <label class="mandatorymark" id= "MandatoryProjectjBusinessArea" runat = "server" visible= "false">
                            *</label>
                    </td>
                    <td "width: 4%" visible="false" style="width: 38px">
                    <span id="spanzProjectBusinessArea" runat="server">
                             <img id="imgProjectBusinessArea" runat="server" src="Images/cross.png" alt="" style="display: none; border: none;"/>
                        </span>
                    </td>
                    <td style="width: 640px">
                        <span id="spanzPrjBusinessArea" runat="server" Width="155px" >
                            <asp:DropDownList ID="ddlPrjBusinessArea" runat="server" ToolTip="Select Project Business Area"
                                Width="212px" CssClass="mandatoryField" visible="false" 
                            Enabled ="false"  AutoPostBack="true"
                            onselectedindexchanged="ddlPrjBusinessArea_SelectedIndexChanged">
                            </asp:DropDownList>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 251px" visible="false" >
                         <asp:Label ID="lblPrjBusinessSegment" runat="server" Text="Business Segment" visible="false" CssClass="txtstyle"></asp:Label>
                        <label id = "MandatoryProjectBusinessSegment" class="mandatorymark" runat="server" visible= "false">
                            *</label>
                    </td>
                    <td "width: 3%" visible="false" style="width: 72px"  >
                        <span id="Span2" runat="server">
                             <img id="imgProjectBusinessSegment" runat="server" src="Images/cross.png" alt="" 
                            style="display: none; border: none;" align="left"/>
                        </span>  
                    </td>
                    <td style="width: 372px">
                       <span id="spanzPrjBusinessSegment" runat="server">
                            <asp:DropDownList ID="ddlPrjBusinessSegment" runat="server" ToolTip="Select Project Business Segment"
                                Width="212px" CssClass="mandatoryField" visible="false" Enabled="false">
                            </asp:DropDownList>
                        </span>
                    </td>
                    <td style="width: 251px" visible="false" >
                         <asp:Label ID="lblPrjName" runat="server" Text="Project Name" visible="false" CssClass="txtstyle"></asp:Label>
                        <label id = "MandatoryProjectName" class="mandatorymark" runat="server" visible= "false">
                            *</label>
                    </td>
                    <td "width: 3%" visible="false" style="width: 72px"  >
                        <span id="spanzProjectName" runat="server">
                             <img id="imgPrjNames" runat="server" src="Images/cross.png" alt="" 
                            style="display: none; border: none;" align="left"/>
                        </span>  
                    </td>
                    <td style="width: 372px">
                       <span id="spanPrjName" runat="server">
                            <asp:TextBox ID="txtPrjName" runat="server" CssClass="mandatoryField" MaxLength="50"
                                ToolTip="Enter Project Name" BorderStyle="Solid" 
                            AutoComplete="off" visible="false" Width="210px" 
                            onkeypress="return DenySpecialChar(event);"></asp:TextBox>
                        </span>
                    </td>
                </tr>
                <%--Mohamed : Issue  : 19/09/2014 : Ends--%>
                 <tr>
                    <td style="width: 251px" visible="false" >
                    <asp:Label ID="lblTypeOfProj" runat="server" Text="Type of Project" visible="false" CssClass="txtstyle"></asp:Label>
                        <label class="mandatorymark" id= "MandatoryTypeOfProj" runat = "server" visible= "false">
                            *</label>
                        <%-- <asp:Label ID="lblPrjAlias" runat="server" Text="Project Alias" visible="false" CssClass="txtstyle"></asp:Label>
                        <label id = "MandatoryPrjAlias" class="mandatorymark" runat="server" visible= "false">
                            *</label>--%>
                    </td>
                    <td "width: 3%" visible="false" style="width: 72px"  >
                        <span id="span3" runat="server">
                             <img id="img2" runat="server" src="Images/cross.png" alt="" style="display: none; border: none;"/>
                        </span>
                       <%-- <span id="spanzProjectAlias" runat="server">
                             <img id="imgProjectAlias" runat="server" src="Images/cross.png" alt="" 
                            style="display: none; border: none;" align="left"/>
                        </span> --%> 
                    </td>
                    <td style="width: 372px">
                     <span id="spanzTypeOfPrj" runat="server" Width="155px" >
                            <asp:DropDownList ID="ddlTypeOfPrj" runat="server" ToolTip="Select Type Of Project"
                                Width="212px" CssClass="mandatoryField" visible="false" 
                            onselectedindexchanged="ddlTypeOfPrj_SelectedIndexChanged">
                            </asp:DropDownList>
                        </span>
                       <%--<span id="spanPrjAlias" runat="server" Width="155px">
                            <asp:TextBox ID="txtPrjAlias" runat="server" CssClass="mandatoryField" MaxLength="50"
                                ToolTip="Enter Project Alias" BorderStyle="Solid" 
                            AutoComplete="off" visible="false" Width="210px" 
                            onkeypress="return DenySpecialChar(event);"></asp:TextBox>
                        </span>--%>
                    </td>
                    <td style="width: 216px;">
                       <asp:Label ID="lblPrjLocation" runat="server" Text="Project Location" visible="false" CssClass="txtstyle" ></asp:Label>
                    </td>
                    <td "width: 4%" visible="false" style="width: 38px">
                    
                    </td>
                    <td style="width: 640px">
                         <span id="spanzPrjLocation" runat = "server">
                            <asp:DropDownList ID="ddlPrjLocation" runat="server" ToolTip="Select Project Location"  CssClass="mandatoryField"
                                Width="215px"  visible="false">
                            </asp:DropDownList>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 251px">                       
                       <%--Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                       Desc : project group should be mandatory while creation of project otherwise Allocation  and Billing report gives error --%>
                        <asp:Label ID="lblPrjGroup" runat="server" Text="Project Group" visible="false" CssClass="txtstyle" ></asp:Label>
                        <label id = "MandatoryProjGroup" class="mandatorymark" runat="server" visible= "false">
                            *</label>                       
                    </td>
                    <td "width: 3%" visible="false" style="width: 72px">                     
                    </td>
                    <td style="width: 372px;" >
                        
                        <span id="spanzPrjGroup" runat = "server" Width="155px" >
                            <asp:DropDownList ID="ddlPrjGroup" runat="server" ToolTip="Select Project Group"  CssClass="mandatoryField"
                                Width="215px"  visible="false">
                            </asp:DropDownList>
                        </span>
                        
                        <%-- Mohamed : Issue 49791 : 15/09/2014 : Ends--%>
                    </td>
                    <td style="width: 216px">
                        <asp:Label ID="lblNoOfResources" runat="server" Text="No of Resources" visible="false" CssClass="txtstyle"></asp:Label>
                    </td>
                    <td "width: 4%" visible="false" style="width: 38px">
                         <span id="spanzNoOFResource" runat="server">
                             <img id="imgNoOfResources" runat="server" src="Images/cross.png" alt="" 
                             style="display: none; border: none;" align="left"/>
                        </span>
                    </td>
                    <td style="width: 640px">
                        <span id="spanzNoOfResources" runat="server" >
                            <asp:TextBox ID="txtNoOfResources" runat="server" CssClass="mandatoryField" MaxLength="5"
                                ToolTip="Enter No of Resources" BorderStyle="Solid" 
                            AutoComplete="off" visible="false"  Width="210px"  
                            onkeypress="return IntWithDecimal(event,this);"></asp:TextBox>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 251px">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date" visible="false" CssClass="txtstyle"></asp:Label>
                        <label id="MandatoryStartDate" class="mandatorymark"  runat = "server" visible = "false">
                            *</label>
                    </td>
                    <td "width: 3%" visible="false" style="width: 72px">
                         <span id="spanzStartDt" runat="server">
                             <img id="imgStartDate" runat="server" src="Images/cross.png" alt="" 
                             style="display: none; border: none;" align="left"/>
                        </span>
                    </td>
                    <td style="width: 372px">
                        <span id="spanzStartDate" runat="server">
                             <uc1:DatePicker ID = "ucDatePickerStart" runat="server" visible="false"/>
                            
                            </span>
                    </td>
                    <td style="width: 216px;" atomicselection="True">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date" visible= "false" CssClass="txtstyle"></asp:Label>
                        <label id= "MandatoryEndDate" class="mandatorymark" runat = "server"   visible= "false">
                            *</label>
                    </td>
                    <td "width: 3%" visible="false" style="width: 38px">
                        <span id="spanEndDt" runat="server">
                             <img id="imgEndDate" runat="server" src="Images/cross.png" alt="" 
                            style="display: none; border: none;" align="left"/>
                        </span>
                    </td>
                    <td style="width: 640px">
                        <span id="spanzEndDate" runat="server">                        
                             <uc1:DatePicker ID = "ucDatePickerEnd" runat="server"  visible="false"/>
                             </span>
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 251px">
                           <asp:Label ID="lblProjectAbbr" runat="server" Text="Project Abbreviation" CssClass="txtstyle" Visible="false"></asp:Label>
                         <label class="mandatorymark" id = "lblMandatoryProjectAbbr" runat = "server" Visible="false">
                            *</label></td>
                    <td "width: 1%" visible="false" style="width: 72px">
                         &nbsp;</td>
                    <td style="width: 372px">
                        <span id="spanzProjectAbbr" runat="server">
                            <asp:TextBox ID="txtProjectAbbr" runat="server" CssClass="mandatoryField" MaxLength="12"
                                ToolTip="Enter Project Abbrivation" BorderStyle="Solid" AutoComplete="off"
                                Width ="210px" Visible="false"></asp:TextBox>
                        </span></td>
                    <td style="width: 216px;">
                         <asp:Label ID="lblPhase" runat="server" Text="Phase/Sprint No" CssClass="txtstyle" Visible="false"></asp:Label>
                         <label class="mandatorymark" id = "lblMandatorymarkPhase" runat = "server" Visible="false">
                            *</label>&nbsp;</td>
                    <td "width: 4%" visible="false" style="width: 38px">
                        &nbsp;</td>
                    <td style="width: 640px">
                        <span id="spanPhase" runat="server">
                            <asp:TextBox ID="txtPhase" runat="server" CssClass="mandatoryField" MaxLength="3"
                                ToolTip="Enter Project Abbrivation" BorderStyle="Solid" AutoComplete="off"
                                Width ="210px" Visible="false"></asp:TextBox>
                        </span></td>
                </tr>
                
                
                <%--Siddharth 28th August 2015 Start--%>
                 <tr>
                    <td style="height: 26px; width: 141px;">
                     <asp:Label ID="LblProjectModel" runat="server" Text="Project Model" CssClass="txtstyle" Visible="false"></asp:Label><label
                            class="mandatorymark" id = "lblMandatorymarkProjectModel" runat = "server" Visible="false">*</label>
                    </td>
                    <td style="width: 4%; height: 26px;" visible="false"></td>
                    <td style="height: 26px; width: 278px;" visible="false">
                     <span id="spanzProjectModel" runat = "server">
                            <asp:DropDownList ID="ddlProjectModel" runat="server" ToolTip="Select Project Model"
                                Width="215px" CssClass="mandatoryField"  Visible="false">
                            </asp:DropDownList>
                        </span> 
                    </td>
                    
                     
                     <td style="width: 140px;">
                        <asp:Label ID="LblBusinessVertical" runat="server" Text="BusinessVertical" CssClass="txtstyle" Visible="false"></asp:Label>
                        <label class="mandatorymark" id = "lblMandatorymarkBusinessVertical" runat = "server" Visible="false">*</label>
                    </td>
                    <td style="width: 4%; height: 26px;" visible="false"></td>
                    <td visible="false">
                        <span id="SpanBusinessVertical" runat = "server">
                            <asp:DropDownList ID="ddlBusinessVertical" runat="server" Width="155px" ToolTip="Select Business Vertical"
                                CssClass="mandatoryField" TabIndex="1" AutoPostBack="false" Visible="false">
                            </asp:DropDownList>
                        </span>
                        <asp:CustomValidator runat="server" ID="CustomValidator2" ControlToValidate="ddlBusinessVertical"
                            ClientValidationFunction="fnValidateInputforEnabled" ValidateEmptyText="true"
                            EnableClientScript="true" meta:resourcekey="cvBusinessVertical" SetFocusOnError="true"
                            ValidationGroup="vgAddProject" Display="Dynamic"></asp:CustomValidator>
                    </td>
                                    
                </tr>
                <%--Siddharth 28th August 2015 End--%>
                
                
                <%--Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                       Desc : project group should be mandatory while creation of project otherwise Allocation  and Billing report gives error --%>
              <%--  <tr>
                    <td style="width: 251px">
                       
                        
                       
                    </td>
                    <td "width: 3%" visible="false" style="width: 72px">
                     
                    </td>
                    <td style="width: 372px;" >
                        
                      
                        
                    </td>
                    </tr>--%>
               <%-- </table>
            <table>--%>
            <%-- Mohamed : Issue 49791 : 15/09/2014 : Ends--%>
                 <tr id="trProjectHead" runat="server" visible="false">
                    <td style="height: 26px; width: 141px;">
                     <asp:Label ID="LblProjectModel0" runat="server" Text="Project Head" 
                            CssClass="txtstyle" ></asp:Label><label
                            class="mandatorymark" id = "lblMandatorymarkProjectModel0" 
                            runat = "server" >*</label>
                    </td>
                    <td style="width: 4%; height: 26px;" >&nbsp;</td>
                    <td style="height: 26px; width: 278px;" >
                     <span id="spanzProjectHead" runat = "server">
                            <asp:DropDownList ID="ddlProjectHead" runat="server" ToolTip="Select Project Head"
                                Width="215px" CssClass="mandatoryField" >
                            </asp:DropDownList>
                            
                        </span> 
                    </td>
                    
                     
                     <td style="width: 140px;">
                         
                    </td>
                    <td style="width: 4%; height: 26px;" visible="false">&nbsp;</td>
                    <td visible="false">
                        &nbsp;</td>
                                    
                </tr>
                <tr>
                <%--Sanju:Issue Id 50201:Alignment issue resolved--%>
                    <td style="width: 251px">
                     <%--Sanju:Issue Id 50201:End--%>
                        <asp:Label ID="LabelProjectDescription" runat="server" Text="Project Executive Summary" visible="false" CssClass="txtstyle"></asp:Label>
                        <label id="mandatoryProjectDescription" class="mandatorymark" runat = "server" visible = "false">
                            *</label>
                     </td>
                    <td style="width: 8px">                       
                       <span id="spanimgDescription" runat="server">
                        <img id="imgDescription" runat="server" src="Images/cross.png" alt="" style="display: none; border: none;" align="left"/>
                      </span>
                    </td>
                    <td colspan="4">
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="431px" 
                            ToolTip="Enter Project Description" MaxLength="5000" CssClass="mandatoryField" 
                            AutoComplete="off" 
                            Visible="False" ></asp:TextBox>
                    <asp:CustomValidator runat="server" ID="cvDescription"  ControlToValidate="txtDescription" ClientValidationFunction="fnValidateInput" ValidateEmptyText="true" EnableClientScript="true" meta:resourcekey="cvDescription" SetFocusOnError="true" ValidationGroup="vgAddProject" Display="Dynamic"></asp:CustomValidator>
                     </td>
                </tr>
                
                </table>
            <table>
                <tr>
                    <td style="width: 266px">
                        <asp:HiddenField ID="IsNewProject" runat="server" />
                    </td>
                    <td style="width: 46px">
                       <span id="spanProjectLocation" runat="server" >
                             <img id="imgProjectLocation" runat="server" src="Images/cross.png" alt="" style="display: none; border: none;"/>
                        </span>
                    </td>
                    <td style="width: 440px">
                        <asp:HiddenField ID="IsProjectDetailsVisible" runat="server"/>
                    </td>
                   <td style="width: 182px">
                       <asp:HiddenField ID="IsGridEdited" runat="server" />
                    </td>
                    <td style="width: 50px">
                        &nbsp;</td>
                    <td align="left" style="width: 640px">
                       <span>
                        <asp:Button ID="btnAddRow" runat="server" Text="Add Row"  CssClass="button"
                            OnClick="btnAddRow_Click" Visible="false" />                           
                        </span>&nbsp                        
                        <span>                        
                        <asp:Button ID="btnPrjCancle" runat="server" Text="Cancel" CssClass="button"
                        Visible="false" onclick="btnPrjCancle_Click" />
                        </span> 
                   
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    <table style="width: 100%">
                        <tr width="100%">
                            <asp:GridView ID="gvProjectDetails" runat="server" Height="1px" AutoGenerateColumns="False"
                                EditIndex="0" SelectedIndex="0"  Width="100%" 
                                onrowcommand="gvProductDetails_RowCommand" >
                                <HeaderStyle CssClass="addrowheader" />
                                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                <RowStyle CssClass="textstyle" />
                                <Columns >
                                
                                    <asp:BoundField DataField="ProjectId" Visible = "false" ReadOnly = "true" />
                                    
                                    <asp:BoundField DataField="ProjectCode" HeaderText="Project Code" ReadOnly = "true" />
                                   
                                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly = "true" />
                                    
                                    <asp:BoundField DataField="ProjectType" HeaderText="Type of Project"  ReadOnly = "true"/>
                                   
                                    <asp:BoundField DataField="ProjectStartDate" HeaderText="Start Date"  DataFormatString="{0:dd/MM/yyyy}" ReadOnly = "true"/>
                                   
                                    <asp:BoundField DataField="ProjectEndDate" HeaderText="End Date"  DataFormatString="{0:dd/MM/yyyy}" ReadOnly = "true"  />
                                    
                                  
                                    <asp:BoundField DataField="NoOfResources" HeaderText="No of Resources" ReadOnly = "true"  Visible="false"/>
                                    
                                    <asp:BoundField DataField="ProjectCategoryName" HeaderText="Project Category"  ReadOnly = "true"/>
                                   
                                   <asp:BoundField DataField="ProjectHeadName" HeaderText="Project Head"  ReadOnly = "true"/>
                                    <asp:TemplateField Visible="true">
                                    
                                        <ItemTemplate>                                         
                                                <asp:ImageButton ID ="imgDelete" runat = "server" ImageUrl = "~/Images/Delete.gif" 
                                                style="border: none; cursor: hand;" ToolTip = "Delete" 
                                                CommandName = "DeleteContract" CommandArgument='<%# Eval("ProjectName") %>' /><%-- OnClick = "btnDeleteImg_Click" --%>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                    
                                        <ItemTemplate>                                         
                                                <asp:ImageButton ID ="imgEdit" runat = "server" ImageUrl = "~/Images/Edit.gif" 
                                                style="border: none; cursor: hand;" ToolTip = "Edit"  CommandName = "EditContract" CommandArgument='<%# Eval("ProjectName") %>'  /> <%--OnClick = "btnEdit_Click"--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                     
                                        <ItemTemplate>                                         
                                                <asp:Button ID="imgBtnAdd" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="AddCR" CommandArgument='<%# Eval("ProjectCode") %>'
                                            ToolTip="Add CR" Text="Add CR" CssClass="button" Width="80px"/>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
                        </tr>
                    </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <table style="width: 105%">
            <tr>
                <td style="width: 305px; margin-left: 40px">
                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="button" Width="118px"
                        Visible="False" OnClick="btnPrevious_Click" />
                    
                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="button" Width="118px"
                        Visible="False" OnClick="btnNext_Click" />
                </td> 
                              
                <td Width="120px">
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="118px" CssClass="button"
                        Visible="false" OnClick="btnEdit_Click" />
                  </td>
                    <td id="tdSavButton" Width="118px" runat="server">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="118px" CssClass="button"
                        OnClick="btnSave_Click" />
                        </td>
                       <td id="tdDelButton" Width="1px" runat = "server">
                       <asp:Button ID="btnDelete" runat="server" Text="Delete"  ForeColor = "White" BackColor="#003399"
                        Visible ="false" onclick="btnDelete_Click" Font-Bold="True" 
                               Font-Size="Small" Width="0px"  />
                        </td>
                        <td Width="118px">
                    <asp:Button ID="btnCancle" runat="server" Text="Cancel" CssClass="button"
                        OnClick="btnCancle_Click"  />
                       </td>                    
            </tr>
        </table>
    </div>
</table>
  
</asp:Content>
