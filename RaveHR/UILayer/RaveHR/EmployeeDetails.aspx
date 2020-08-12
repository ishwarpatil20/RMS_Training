<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeDetails.aspx.cs" Inherits="EmployeeDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        var retVal;
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        retVal = e.data;
                    }
                });
            });
        })(jQuery);

        function popUpEmployeeEmailPopulate() {
            jQuery.modalDialog.create({ url: "EmployeesEmailList.aspx", maxWidth: 550,
                onclose: function(e) {
                    var txtEmailID = jQuery('#<%=txtEmailID.ClientID %>');
                    var hdtxtEmail = jQuery('#<%=hdtxtEmail.ClientID %>');

                    if (retVal == undefined) {
                        txtEmailID.val("");
                        hdtxtEmail.val();
                    }
                    else {
                        txtEmailID.val(retVal);
                        hdtxtEmail.val(retVal);
                    }
                }
            }).open();
        };

        function popUpFunctionalManagerSearch() {
            jQuery.modalDialog.create({ url: "EmployeesList.aspx?str=functionalMngr", maxWidth: 550,
                onclose: function(e) {
                    var txtReportingTo = jQuery('#<%=txtReportingFM.ClientID %>');
                    var hidReportingTo = jQuery('#<%=hidReportingFM.ClientID %>');
                    var hidReportingToFM = jQuery('#<%=HfReportingToFM.ClientID %>');
                    var hidReportingToValue = jQuery('#<%=hidReportingFM.ClientID %>').val();

                    var EmpName;
                    var EmpId;
                    var employee = new Array();
                    if (retVal != undefined)
                        employee = retVal.split(",");
                    for (var i = 0; i < employee.length - 1; i++) {
                        var emp = employee[i];
                        var emp1 = new Array();
                        var emp1 = emp.split("_");
                        if (EmpId == undefined) {
                            EmpId = emp1[0];
                        }
                        // 20776-Ambar-Start-Uncomment following code
                        else {
                            EmpId = EmpId + "," + emp1[0];
                        }
                        // 20776-Ambar-End
                        if (EmpName == undefined) {
                            EmpName = emp1[1];
                        }
                        // 20776-Ambar-Start-Uncomment following code
                        else {
                            EmpName = EmpName + "," + emp1[1];
                        }
                        // 20776-Ambar-End
                    }
                    if (EmpId != undefined) {
                        hidReportingTo.val(EmpId.trim());
                    }
                    if (EmpName != undefined) {
                        txtReportingTo.val(EmpName.trim());
                        hidReportingToFM.val(EmpName.trim());
                    }
                }
            }).open();
        };

        function popUpEmployeeSearch() {
            jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                onclose: function(e) {
                    var txtReportingTo = jQuery('#<%=txtReportingTo.ClientID %>');
                    var hidReportingTo = jQuery('#<%=hidReportingTo.ClientID %>');
                    var hidReportingToName = jQuery('#<%=HfReportingToName.ClientID %>');
                    var hidReportingToValue = jQuery('#<%=hidReportingTo.ClientID %>').val();

                    var EmpName;
                    var EmpId;
                    var employee = new Array();
                    if (retVal != undefined)
                        employee = retVal.split(",");
                    for (var i = 0; i < employee.length - 1; i++) {
                        var emp = employee[i];
                        var emp1 = new Array();
                        var emp1 = emp.split("_");
                        if (EmpId == undefined) {
                            EmpId = emp1[0];
                        }
                        else {
                            EmpId = EmpId + "," + emp1[0];
                        }
                        if (EmpName == undefined) {
                            EmpName = emp1[1];
                        }
                        else {
                            EmpName = EmpName + "," + emp1[1];
                        }
                    }
                    if (EmpId != undefined) {
                        hidReportingTo.val(EmpId.trim());
                    }
                    if (EmpName != undefined) {
                        hidReportingToName.val(EmpName.trim());
                        txtReportingTo.val(EmpName.trim());
                    }
                }
            }).open();
        };
        //Umesh: Issue 'Modal Popup issue in chrome' Ends


        //function added to check data has been modified or not on Page.
        function myfn() {

            var IsDataModified = $('<%=HfIsDataModified.ClientID %>').value;
            alert("data modify");
            if (IsDataModified == "Yes")
                javascript: __doPostBack('ctl00$cphMainContent$lnkSaveBtn', '')
        }
    </script>

    <asp:LinkButton runat="server" ID="lnkSaveBtn" OnClick="lnkSaveBtn_Click" Visible="false" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%><td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <%--   Sanju:Issue Id 50201:End--%><asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label></td>
            <%-- Sanju: Added background color property so that all browsers display header--%><td align="right" style="height: 25px; width: 20%; background-color: #7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
                <%--   Sanju:Issue Id 50201:End--%><asp:Label ID="lblempName" runat="server" CssClass="header"></asp:Label></td>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
    </table>
    <asp:Table ID="tblMain" runat="server" Width="100%" BorderColor="AliceBlue" BorderStyle="Solid"
        BorderWidth="2" Height="100%">
        <asp:TableRow ID="TableRow1" runat="server" Width="100%" VerticalAlign="Top">
            <asp:TableCell ID="tcellIndex" Width="15%" Height="100%" runat="server" BorderColor="Beige"
                BorderWidth="1"><!-- Panel for user control --><asp:Panel 
    ID="pnlUserControl" runat="server">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell ID="tcellContent" Width="85%" Height="100%" runat="server"><!-- Dump aspx code here --><script type="text/javascript">
                
                    setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmployeeProfile');


                    function ValidateEmployeeCode() {
                        //                       
                        if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) {
                            event.returnValue = true;

                        }
                        else {
                            event.returnValue = false;
                            alert("Please input alphanumeric value only");
                        }

                        //                       -- var keychar = String.fromCharCode(key);

                    }


                    function ValidateBloodGroup() {
                        if ((event.keyCode == 65) || (event.keyCode == 66) || (event.keyCode == 79) || (event.keyCode == 43) || (event.keyCode == 45)) {
                            // ascii code for + is 43
                            //ascii code for - is 45
                            event.returnValue = true;

                        }
                        else {
                            event.returnValue = false;
                            alert("Please enter valid blood group");
                        }

                    }
                    function $(v) { return document.getElementById(v); }

                    function GetTrueKeyCode(e) {
                        //alert("hi");
                        var evtobj = window.event ? event : e;
                        var unicode = evtobj.charCode ? e.charCode : evtobj.keyCode;
                        var JoiningDate1 = $('<%=ucDatePickerJoiningDate.ClientID %>').id;
                        if ((unicode == 37) || (unicode == 38) || (unicode == 39) || (unicode == 40)) {
                            //document.getElementById(JoiningDate1).readOnly = true;
                            return false;
                        }
                        else {
                            return true;
                        }
                        ///document.getElementById(JoiningDate1).readOnly = false;

                    }

                    function cancelKey(evt) {


                        if (evt.preventDefault) {
                            evt.preventDefault();
                            return false;
                        }
                        else {
                            evt.keyCode = 0;
                            evt.returnValue = false;
                        }
                    }

                    function ButtonDOB() {

                        var spanlist = "";
                        var DOBValue = $('<%=ucDatePickerDOB.ClientID %>').value;
                        var JoiningDateValue = $('<%=ucDatePickerJoiningDate.ClientID %>').value;
                        if (DOBValue > JoiningDateValue) {

                            var sDOB = $('<%=spanDOB.ClientID %>').id;
                            spanlist = spanlist + sDOB + ",";
                            var controlList = DOB
                            if (ValidateControlOnClickEvent(controlList) == false) {
                                lblMessage = $('<%=lblMessage.ClientID %>')
                                lblMessage.innerText = "The DOB can not be greater that Joining date";
                                lblMessage.style.color = "Red";

                                lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                lblConfirmMsg.innerText = "";
                            }
                        }
                    }
                    function ButtonClickValidate(bool) {
                        var lblMandatory;
                        //Poonam : 1/09/2015 : Starts
                        //Desc : Validation to make Designation Changed Date and Department Changed Date mandatory
                        var lblMessage = $('<%=lblMessage.ClientID %>');
                        lblMessage.innerHTML = "";
                        //Poonam : 1/09/2015 : Ends
                        var controlList;
                        var spanlist = "";
                        var bool;
                        //Mohamed : 24-02-2015 :Starts
                        //Desc : if the user is from NIS then dont check the control validation                        
                        if (document.getElementById("<%=pnlEmployeeDetails.ClientID %>").getAttribute('disabled') != "disabled" ||
                               document.getElementById("<%=pnlEmpPersonalDetails.ClientID %>").getAttribute('disabled') != "disabled") {
                            // employee details controls 
                            var Prefix = $('<%=ddlPrefix.ClientID %>').value;
                            var EmployeeCode = $('<%=txtEmployeeCode.ClientID %>').id;
                            var FirstName = $('<%=txtFirstName.ClientID %>').id;
                            var LastName = $('<%=txtLastName.ClientID %>').id;
                            var EmailID = $('<%=txtEmailID.ClientID %>').id;
                            var Department = $('<%=ddlDepartment.ClientID %>').value;
                            var EmployeeType = $('<%=ddlEmployeeType.ClientID %>').value;
                            var Band = $('<%=ddlBand.ClientID %>').value;
                            var Designation = $('<%=ddlDesignation.ClientID %>').value;
                            var JoiningDate = $('<%=ucDatePickerJoiningDate.ClientID %>').id;
                            var ReportingTo = $('<%=txtReportingTo.ClientID %>').id;
                            var txtReportingFM = $('<%=txtReportingFM.ClientID %>').id;
                            var Location = $('<%=ddlEmpLocation.ClientID %>').value;
                            var ResourceBusinessUnitID = $('<%=ddlResourceBussinesUnit.ClientID %>').id;

                            var ucDatePickerDepartmentChangeDate = $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').value;
                            var ucDatePickerDesignationChangeDate = $('<%=ucDatePickerDesignationChangeDate.ClientID %>').value;


                            //Siddharth 27th August 2015 Start
                            var ResourceBusinessUnit = $('<%=ddlResourceBussinesUnit.ClientID %>').value;
                            //Siddharth 27th August 2015 End

                            // personal details controls
                            var DOB = $('<%=ucDatePickerDOB.ClientID %>').id
                            var Gender = $('<%=ddlGender.ClientID %>').value;
                            var MaritalStatus = $('<%=ddlMaritalStatus.ClientID %>').value;
                            var BloodGroup = $('<%=txtBloodGroup.ClientID %>').id

                            if (Prefix == "" || Prefix == "SELECT") {
                                var sPrefix = $('<%=spanzPrefix.ClientID %>').id;
                                spanlist = spanlist + sPrefix + ",";
                            }

                            if (Department == "" || Department == "SELECT") {
                                var sDepartment = $('<%=spanzDepartment.ClientID %>').id;
                                spanlist = spanlist + sDepartment + ",";
                            }

                            if (EmployeeType == "" || EmployeeType == "SELECT") {
                                var sEmployeeType = $('<%=spanzEmployeeType.ClientID %>').id;
                                spanlist = spanlist + sEmployeeType + ",";
                            }

                            if (Band == "" || Band == "SELECT") {
                                var sBand = $('<%=spanzBand.ClientID %>').id;
                                spanlist = spanlist + sBand + ",";
                            }

                            if (Designation == "" || Designation == "SELECT") {
                                var sDesignation = $('<%=spanzDesignation.ClientID %>').id;
                                spanlist = spanlist + sDesignation + ",";
                            }

                            if (Location == "" || Location == "SELECT") {
                                var sLocation = $('<%=spanzEmpLocation.ClientID %>').id;
                                spanlist = spanlist + sLocation + ",";
                            }

                            if (ucDatePickerDepartmentChangeDate == "") {
                                var sucDatePickerDepartmentChangeDate = $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').id;
                                spanlist = spanlist + sucDatePickerDepartmentChangeDate + ",";
                            }

                            if (ucDatePickerDesignationChangeDate == "") {
                                var sucDatePickerDesignationChangeDate = $('<%=ucDatePickerDesignationChangeDate.ClientID %>').id;
                                spanlist = spanlist + sucDatePickerDesignationChangeDate + ",";
                            }

                            //Siddharth 27th August 2015 Start
                            //Make Resourec Business Unit Dropdown Mandatory field
                            if (ResourceBusinessUnit == "" || ResourceBusinessUnit == "SELECT") {
                                var sLocation1 = $('<%=spanzResourceBussinesUnit.ClientID %>').id;
                                spanlist = spanlist + sLocation1 + ",";
                            }
                            //Siddharth 27th August 2015 End
                            if (bool == false) {
                                if (Gender == "" || Gender == "SELECT") {
                                    var sGender = $('<%=spanzGender.ClientID %>').id;
                                    spanlist = spanlist + sGender + ",";
                                }

                                if (MaritalStatus == "" || MaritalStatus == "SELECT") {
                                    var sMaritalStatus = $('<%=spanzMaritalStatus.ClientID %>').id;
                                    spanlist = spanlist + sMaritalStatus + ",";
                                }


                                if (MaritalStatus == "M") {
                                    var SpouseName = $('<%=txtSpouseName.ClientID %>').id

                                    if (spanlist == "") {

                                        controlList = EmployeeCode + "," + FirstName + "," + LastName + "," + EmailID + "," + JoiningDate + "," + ReportingTo + "," + DOB + "," + BloodGroup + "," + SpouseName + "," + txtReportingFM;
                                    }
                                    else {
                                        controlList = spanlist + EmployeeCode + "," + FirstName + "," + LastName + "," + EmailID + "," + JoiningDate + "," + ReportingTo + "," + DOB + "," + BloodGroup + "," + SpouseName + "," + txtReportingFM;
                                    }
                                }
                                else {
                                    if (spanlist == "") {

                                        controlList =  EmployeeCode + "," + FirstName + "," + LastName + "," + EmailID + "," + JoiningDate + "," + ReportingTo + "," + DOB + "," + BloodGroup + "," + txtReportingFM;
                                    }
                                    else {
                                        controlList = spanlist + $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').id + "," + $('<%=ucDatePickerDesignationChangeDate.ClientID %>').id + "," + EmployeeCode + "," + FirstName + "," + LastName + "," + EmailID + "," + JoiningDate + "," + ReportingTo + "," + DOB + "," + BloodGroup + "," + txtReportingFM;
                                    }
                                }
                                if (ValidateControlOnClickEvent(controlList) == false) {
                                    //lblMessage = $('<%=lblMessage.ClientID %>')
                                    lblMessage.innerText = "Please fill all mandatory fields.";
                                    lblMessage.style.color = "Red";

                                    lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                    lblConfirmMsg.innerHTML = "";
                                }

                            }
                            else
                                if (bool == true) {
                                if (MaritalStatus == "M") {
                                    var SpouseName = $('<%=txtSpouseName.ClientID %>').id

                                    if (spanlist == "") {

                                        controlList = EmployeeCode + "," + FirstName + "," + LastName + "," + EmailID + "," + JoiningDate + "," + ReportingTo + "," + SpouseName + "," + txtReportingFM;
                                    }
                                    else {
                                        controlList = spanlist + EmployeeCode + "," + FirstName + "," + LastName + "," + EmailID + "," + JoiningDate + "," + ReportingTo + "," + SpouseName + "," + txtReportingFM;
                                    }
                                }
                                else {
                                    if (spanlist == "") {

                                        controlList = EmployeeCode + "," + FirstName + "," + LastName + "," + EmailID + "," + JoiningDate + "," + ReportingTo + "," + txtReportingFM;
                                    }
                                    else {
                                        controlList = ResourceBusinessUnitID + "," + $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').id + "," + $('<%=ucDatePickerDesignationChangeDate.ClientID %>').id + "," + EmployeeCode + "," + FirstName + "," + LastName + "," + EmailID + "," + JoiningDate + "," + ReportingTo + "," + DOB + "," + BloodGroup + "," + txtReportingFM + "," + spanlist;
                                    }
                                }

                                //                            }

                                //Mohamed : Issue 39509/41062 : 06/03/2013 : Starts
                                //Desc :  Adding new Columns date for Probation
                                //                            if (bool == true) {
                                var spanlistDate = "";
                                var currentTime = new Date();
                                //Mohamed : Issue 48655 : 20/01/2014 : Starts
                                //Desc :  Designation change date in system - Getting Error
                                var month = currentTime.getMonth();
                                var day = currentTime.getDate();
                                var year = currentTime.getFullYear();
                                var TodaysDate = Date.UTC(year, month, day); //day + "/" + month + "/" + year;

                                //Poonam : 1/09/2015 : Starts
                                //Desc : Validation to make Designation Changed Date and Department Changed Date mandatory
//                                var ucDatePickerDepartmentChangeDate = $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').value;
//                                var ucDatePickerDesignationChangeDate = $('<%=ucDatePickerDesignationChangeDate.ClientID %>').value;
                                var hfDesignationDate = $('<%=hfDesignationChangeDate.ClientID %>').value;
                                var hfDepartmentDate = $('<%=hfDepartmentChangeDate.ClientID %>').value;
                                //Poonam : 1/09/2015 : Ends

                                var TempDepartmentChangeDate = $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').value.split("/");
                                var DepartmentChangeDate = Date.UTC(TempDepartmentChangeDate[2], TempDepartmentChangeDate[1] - 1, TempDepartmentChangeDate[0]);  //New Departement Date
                                var TempDesignationChangeDate = $('<%=ucDatePickerDesignationChangeDate.ClientID %>').value.split("/");
                                var DesignationChangeDate = Date.UTC(TempDesignationChangeDate[2], TempDesignationChangeDate[1] - 1, TempDesignationChangeDate[0]);  //New Designation date
                                var TempConfirmedDate = $('<%=ucDatePickerConfirmedDate.ClientID %>').value.split("/");
                                var ConfirmedDate = Date.UTC(TempConfirmedDate[2], TempConfirmedDate[1] - 1, TempConfirmedDate[0]);  //New Confirmed Date
                                var TempDesignationDate = $('<%=hfDesignationChangeDate.ClientID %>').value.split("/");
                                var DesignationDate = Date.UTC(TempDesignationDate[2], TempDesignationDate[1] - 1, TempDesignationDate[0]);  // old Designation Date
                                var TempDepartmentDate = $('<%=hfDepartmentChangeDate.ClientID %>').value.split("/");
                                var DepartmentDate = Date.UTC(TempDepartmentDate[2], TempDepartmentDate[1] - 1, TempDepartmentDate[0]);  // Old Departement date
                                var TempConfirmed = $('<%=hfConfirmedDate.ClientID %>').value.split("/");
                                var Confirmed = Date.UTC(TempConfirmed[2], TempConfirmed[1] - 1, TempConfirmed[0]);  // Old Confirm Date

                                //Siddharth 25th June 2015 Start
                                //debugger;
                                var ucdatepickerCompletionDate = $('<%=ucdatepickerCompletionDate.ClientID %>').value;
                                var CCSplit = ucdatepickerCompletionDate.split("/");
                                var CCDate = new Date(CCSplit[2], CCSplit[1] - 1, CCSplit[0]);

                                var ucDatePickerJoiningDate = $('<%=ucDatePickerJoiningDate.ClientID %>').value;
                                var JdSplit = ucDatePickerJoiningDate.split("/");
                                var JDate = new Date(JdSplit[2], JdSplit[1] - 1, JdSplit[0]);

                                var d = new Date();
                                var dropDownId = $('<%=ucdatepickerCompletionDate.ClientID %>');

                                var defaultDate = new Date(1901, 0, 1);

                                if (ucdatepickerCompletionDate != '') {
                                    if (CCDate.valueOf() != defaultDate.valueOf()) {
                                        if (JDate > CCDate) {
                                            //lblMessage = $('<%=lblMessage.ClientID %>')
                                            lblMessage.innerText = "BPSS Completion Date cannot be less than Employee Joining Date";
                                            lblMessage.style.color = "Red";
                                            dropDownId.style.borderStyle = "Solid";
                                            dropDownId.style.borderWidth = "1px";
                                            dropDownId.style.borderColor = "Red";
                                            return false;
                                        }
                                        if (CCDate > d) {
                                            //lblMessage = $('<%=lblMessage.ClientID %>')
                                            lblMessage.innerText = "BPSS Completion Date cannot be set to future date";
                                            lblMessage.style.color = "Red";
                                            dropDownId.style.borderStyle = "Solid";
                                            dropDownId.style.borderWidth = "1px";
                                            dropDownId.style.borderColor = "Red";
                                            return false;
                                        }
                                    }
                                }
                                //Siddharth 25th June 2015 End

                                //Poonam : 1/09/2015 : Starts
                                //Desc : Validation to make Designation Changed Date and Department Changed Date mandatory
                                if (ucDatePickerDepartmentChangeDate != "" && ucDatePickerDesignationChangeDate != "") {
                                    if (DepartmentChangeDate >= DepartmentDate && DesignationChangeDate >= DesignationDate) {
                                        if (DepartmentChangeDate > TodaysDate && DesignationChangeDate > TodaysDate) {
                                            var controlList1 = $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').id + "," + $('<%=ucDatePickerDesignationChangeDate.ClientID %>').id;
                                            lblMessage.innerHTML = "Department and Designation change date should not be greater than today's date.";
                                            lblMessage.style.color = "Red";

                                            lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                            lblConfirmMsg.innerHTML = "";
                                            
                                            ValidateNotNullControls(controlList1);
                                            return false;
                                        }
                                        else if (DepartmentChangeDate > TodaysDate) {
                                        var controlList1 = $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').id;
                                            lblMessage.innerHTML = "Department change date should not be greater than today's date.";
                                            lblMessage.style.color = "Red";
                                            
                                            lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                            lblConfirmMsg.innerHTML = "";
                                            ValidateNotNullControls(controlList1);
                                            return false;
                                            
                                        }
                                        else if (DesignationChangeDate > TodaysDate) {
                                        var controlList1 = $('<%=ucDatePickerDesignationChangeDate.ClientID %>').id;
                                            lblMessage.innerHTML = lblMessage.innerHTML + "\n" + "Designation change date should not be Blank or greater than today's date.";
                                            lblMessage.style.color = "Red";
                                            
                                            lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                            lblConfirmMsg.innerHTML = "";
                                            ValidateNotNullControls(controlList1);
                                            return false;
                                         
                                        }
                                    }
                                    else if (DepartmentChangeDate < DepartmentDate && DesignationChangeDate < DesignationDate) {
                                        var controlList1 = $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').id +"," + $('<%=ucDatePickerDesignationChangeDate.ClientID %>').id;
                                        lblMessage.innerHTML = "Department change date should be greater than previously entered date " + hfDepartmentDate + "." + "<br>" + "Designation change date should be greater than previously entered date " + hfDesignationDate + ".";
                                        lblMessage.style.color = "Red";
                                        
                                        lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                        lblConfirmMsg.innerHTML = "";
                                        ValidateNotNullControls(controlList1);
                                        return false;
                                       

                                    }
                                    else if (DepartmentChangeDate < DepartmentDate) {
                                        var controlList1 = $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').id;
                                        lblMessage.innerHTML = "Department change date should be greater than previously entered date " + hfDepartmentDate + ".";
                                        lblMessage.style.color = "Red";
                                        lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                        lblConfirmMsg.innerHTML = "";
                                        ValidateNotNullControls(controlList1);
                                        return false;

                                    }
                                    else if (DesignationChangeDate < DesignationDate) {
                                    var controlList1 = $('<%=ucDatePickerDesignationChangeDate.ClientID %>').id;
                                    lblMessage.innerHTML = "Designation change date should be greater than previously entered date " + hfDesignationDate + ".";
                                        lblMessage.style.color = "Red";
                                        lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                        lblConfirmMsg.innerHTML = "";
                                        ValidateNotNullControls(controlList1);
                                        return false;
                                       
                                    }

                                }

                                if (DesignationChangeDate < DepartmentChangeDate && ucDatePickerDepartmentChangeDate != "" && ucDatePickerDesignationChangeDate != "") {
                                    var controlList1 = $('<%=ucDatePickerDesignationChangeDate.ClientID %>').id;
                                    lblMessage.innerHTML = "Designation change date should lie between " + ucDatePickerDepartmentChangeDate + " to " + currentTime.to.toDateString();
                                    lblMessage.style.color = "Red";
                                    lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                    lblConfirmMsg.innerHTML = "";
                                    ValidateNotNullControls(controlList1);
                                    return false;

                                }

                                //Mohamed : Issue 48655 : 20/01/2014 : Ends

                                //Poonam : 1/09/2015 : Ends

                                if (Confirmed != ConfirmedDate) {
                                    if (confirmDate == "" || ConfirmedDate > TodaysDate) {
                                        controlList = $('<%=ucDatePickerConfirmedDate.ClientID %>').id;
                                        if (ValidateControlOnClickEvent(controlList)) {
                                            //lblMessage = $('<%=lblMessage.ClientID %>')
                                            lblMessage.innerText = "Confirmed date should not be Blank or greater than today's date.";
                                            lblMessage.style.color = "Red";

                                            lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                            lblConfirmMsg.innerText = "";
                                            return false;
                                        }
                                    }
                                }
                                //Mohamed : Issue 54919 : 11/03/2015 : Starts
                                //Desc :  Check for department & designation change date less than joining date

                                var ChkTempJoiningDate = $('<%=ucDatePickerJoiningDate.ClientID %>').value.split("/");
                                var ChkJoiningDate = Date.UTC(ChkTempJoiningDate[2], ChkTempJoiningDate[1] - 1, ChkTempJoiningDate[0]);  // old Designation Date

                                if (DepartmentChangeDate < ChkJoiningDate) {
                                    var controlList1 = $('<%=ucDatePickerDepartmentChangeDate.ClientID %>').id;
                                        //lblMessage = $('<%=lblMessage.ClientID %>')
                                        lblMessage.innerHTML = "Department change date should be greater than or equal to joining date.";
                                        lblMessage.style.color = "Red";

                                        lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                        lblConfirmMsg.innerText = "";
                                        ValidateNotNullControls(controlList1);
                                        return false;
                                    
                                }

                                if (DesignationChangeDate < ChkJoiningDate) {
                                    var controlList1 = $('<%=ucDatePickerDesignationChangeDate.ClientID %>').id;
                                        //lblMessage = $('<%=lblMessage.ClientID %>')
                                        lblMessage.innerHTML = "Designation change date should be greater than or equal to joining date.";
                                        lblMessage.style.color = "Red";

                                        lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                        lblConfirmMsg.innerText = "";
                                        ValidateNotNullControls(controlList1);
                                        return false;
                                    
                                }
                                //Mohamed : Issue 54919 : 11/03/2015 : Ends
                            }
                            //Mohamed : Issue 39509/41062 : 06/03/2013 : Ends

                            var spanlist1 = "";

                            var DOBValue = $('<%=ucDatePickerDOB.ClientID %>').value;
                            var JoiningDateValue = $('<%=ucDatePickerJoiningDate.ClientID %>').value;
                            if (Date.parse(DOBValue) > Date.parse(JoiningDateValue)) {

                                var sDOB = $('<%=spanDOB.ClientID %>').id;
                                spanlist1 = spanlist + sDOB + ",";
                                controlList = DOB;
                                if (ValidateControlOnClickEvent(controlList)) {
                                    //lblMessage = $('<%=lblMessage.ClientID %>')
                                    lblMessage.innerText = "The Date Of Birth can not be greater than Joining date";
                                    lblMessage.style.color = "Red";

                                    lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                    lblConfirmMsg.innerText = "";
                                    return false;
                                }
                            }

                            //26873-Ambar-Start
                            // 29501-Ambar-Added if condition to skip HR and RPM from this error
                            if (bool == false) {
                                var MinYear = 0;
                                var MaxYear = 0;

                                var currentTime = new Date()
                                var month = currentTime.getMonth()
                                var day = currentTime.getDate()
                                var year = currentTime.getFullYear()

                                MinYear = currentTime.getFullYear() - 60;
                                MaxYear = currentTime.getFullYear() - 18;

                                var DOBValue1 = new Date()
                                var DOBValue1 = $('<%=ucDatePickerDOB.ClientID %>').value;
                                var newyear = DOBValue1.substring(6, 10);

                                if (newyear < MinYear || newyear > MaxYear) {
                                    var sDOB = $('<%=spanDOB.ClientID %>').id;
                                    spanlist1 = spanlist + sDOB + ",";
                                    controlList = DOB;
                                    if (ValidateControlOnClickEvent(controlList)) {
                                        //lblMessage = $('<%=lblMessage.ClientID %>')
                                        lblMessage.innerText = "Birth date should be between 1-Jan-" + MinYear + " and 31-Dec-" + MaxYear + ".";
                                        lblMessage.style.color = "Red";

                                        lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                        lblConfirmMsg.innerText = "";
                                        return false;
                                    }
                                }
                            }
                            //26873-Ambar-End
                            //controlList = controlList + "," + controlList1 + ",";
                            if (ValidateControlOnClickEvent(controlList) == false) {
                                lblMessage.innerHTML = "Please fill all mandatory fields.";
                                lblMessage.style.color = "Red";

                                lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                                lblConfirmMsg.innerHTML = "";
                            }
                            return ValidateControlOnClickEvent(controlList);
                        }
                        //Mohamed : 24-02-2015 :Ends
                        //Desc : if the user is from NIS then dont check the control validation
                    }
                    function ValidateNotNullControls(controlobject) {
                        var arraycontrolobj = controlobject.split(',');
                        for (var i = 0; i < arraycontrolobj.length; i++) {
                            var controlName = document.getElementById(arraycontrolobj[i]);
                            if (controlName != null) {
                                controlName.style.borderStyle = "Solid";
                                controlName.style.borderWidth = "1px";
                                controlName.style.borderColor = "Red";
                            }
                        }
                        
                          
                    }
                  
                    //Mohamed : Issue 39509/41062 : 06/03/2013 : Starts
                    //Desc :  Adding new Columns date for Probation
                    function ControlAsPerUser() {

                        var deptdatetr = document.getElementById('DeptDate');
                        deptdatetr.style.display = "none";
                        var confirmdatetr = document.getElementById('confirmDate');
                        confirmdatetr.style.display = "none";
                    }

                    //Mohamed : Issue 39509/41062 : 06/03/2013 : Ends

             
                    
                </script><div class="detailsborder">
                    <table width="100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:ValidationSummary ID="valsumEmployee" runat="server" ValidationGroup="vgEmployee"
                                    ShowMessageBox="false" ShowSummary="false" DisplayMode="BulletList" />
                                <asp:ValidationSummary ID="valsumCategory" runat="server" ValidationGroup="vgCategory"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" />
                                <asp:ValidationSummary ID="valsumDomain" runat="server" ValidationGroup="vgDomain"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblConfirmMsg" runat="server" CssClass="Messagetext"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" CssClass="text"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div id="divEmpDetails" class="detailsborder">
                        <table width="100%" class="detailsbg">
                            <tr>
                                <td>
                                    <asp:Label ID="lblEmployeeDetailsGroup" runat="server" Text="Employee Details" CssClass="detailsheader"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlEmployeeDetails" runat="server">
                            <table width="100%">
                                <tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblPrefix" runat="server" Text="Prefix" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <span id="spanzPrefix" runat="server">
                                            <%--Ambar - Added selectedindexchanged event--%><asp:DropDownList ID="ddlPrefix" runat="server" Width="190px" ToolTip="Select Location"
                                                OnSelectedIndexChanged="ddlPrefix_SelectedIndexChanged" AutoPostBack="True" CssClass="mandatoryField">
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                    <td colspan="2" rowspan="4" align="right">
                                        <asp:Image ID="imgEmp" runat="server" Height="100px" Width="100px" />
                                        <asp:ImageButton ID="imgbtnUpload" runat="server" ImageUrl="~/Images/upload.JPG"
                                            OnClick="imgbtnUpload_Click" ToolTip="Upload Image" />
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="mandatoryField" MaxLength="10"
                                            ToolTip="Enter Employee Code" BorderStyle="Solid" Width="185px" OnKeyPress="ValidateEmployeeCode()"></asp:TextBox>
                                        <span id="spanEmployeeCode" runat="server">
                                            <img id="imgEmployeeCode" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" /></span></td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblFirstName" runat="server" Text="First Name" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="mandatoryField" MaxLength="50"
                                            ToolTip="Enter First name" BorderStyle="Solid" Width="185px"></asp:TextBox>
                                        <span id="spanFirstName" runat="server">
                                            <img id="imgFirstName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" /></span></td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtMiddleName" runat="server" MaxLength="50" ToolTip="Enter Middle name"
                                            BorderStyle="Solid" Width="185px" CssClass="mandatoryField"></asp:TextBox>
                                        <span id="spanMiddleName" runat="server">
                                            <img id="imgMiddleName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" /></span></td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblLastName" runat="server" Text="Last Name" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="mandatoryField" MaxLength="50"
                                            ToolTip="Enter Last Name" BorderStyle="Solid" Width="185px"></asp:TextBox>
                                        <span id="spanLastName" runat="server">
                                            <img id="imgLastName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" /></span></td>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblBrowseImage" runat="server" Text="Browse Image" CssClass="textstyle"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="250px" />
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblGroup" runat="server" Text="Department" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <span id="spanzDepartment" runat="server">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="190px" ToolTip="Select Group"
                                                CssClass="mandatoryField" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                    <%--Mohamed : Issue 39509/41062 : 06/03/2013 : Starts                        			  
                                     Desc :  Adding new Columns date for Designation and Departement--%><td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblDesignation" runat="server" Text="Designation" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <span id="spanzDesignation" runat="server">
                                            <asp:DropDownList ID="ddlDesignation" runat="server" Width="190px" ToolTip="Select Designation"
                                                CssClass="mandatoryField" AutoPostBack="true" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                </tr>
                                <tr style="width: 100%" id="DeptDate">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblDepartmentChangeDate" runat="server" Text="Department Change Date"
                                            CssClass="textstyle"></asp:Label><label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <span id="spanDepartmentChangeDate" runat="server">
                                        <uc1:DatePicker ID="ucDatePickerDepartmentChangeDate" runat="server" />
                                    <img id="img2" runat="server" src="Images/Calendar_scheduleHS.png" alt="" style="display: none;
                                        border: none;"/></span></td>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblDesignationChangeDate" runat="server" Text="Designation Change Date"
                                            CssClass="textstyle"></asp:Label><label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 10%;">
                                        <span id="spanDesignationChangeDate" runat="server">
                                            <uc1:DatePicker ID="ucDatePickerDesignationChangeDate" runat="server" />
                                       <%-- </span>
                                        <span id= "spanDesignationDte" runat = "server">--%><img id="img3" runat="server" src="Images/Calendar_scheduleHS.png" alt="" style="display: none;
                                        border: none;"/></span></td>
                                </tr>
                                <%--Mohamed : Issue 39509/41062 : 06/03/2013 : Ends--%><tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblBand" runat="server" Text="Band" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <span id="spanzBand" runat="server">
                                            <asp:DropDownList ID="ddlBand" runat="server" Width="190px" ToolTip="Select Band"
                                                CssClass="mandatoryField">
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                    <%--Mohamed : Issue 39509/41062 : 06/03/2013 : Starts                        			  
                                     Desc :  Adding new Columns date for Designation and Departement--%><td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblEmailID" runat="server" Text="Email ID" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="mandatoryField" MaxLength="50"
                                            ToolTip="Enter Email ID" BorderStyle="Solid" Width="185px"></asp:TextBox>
                                        <asp:HiddenField ID="hdtxtEmail" runat="server" />
                                        <%-- Sanju:Issue Id 50201: Added class for pointer--%><img id="imgEmpEmailPopulate" runat="server" src="Images/find.png" alt="" class="cursor_pointer" /><%-- Sanju:Issue Id 50201:End--%><span id="spanEmailID" runat="server"><img id="imgLastUsedDateError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" /></span></td>
                                </tr>
                                <%--Mohamed : Issue 39509/41062 : 06/03/2013 : Ends--%><tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblJoiningDate" runat="server" Text="Joining Date" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</label></td>
                                    <td style="width: 30%">
                                        <uc1:DatePicker ID="ucDatePickerJoiningDate" runat="server" />
                                        <%--<%--<asp:ImageButton ID="imgJoiningDate" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                                            CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="5" />--%>
                                        <%--<cc1:CalendarExtender ID="calendarJoiningDate" runat="server" PopupButtonID="imgJoiningDate"
                                            TargetControlID="txtJoiningDate" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>--%>
                                        <span id="spanJoiningDate" runat="server">
                                            <img id="imgJoiningDateError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblEmployeeType" runat="server" Text="Employee Type" CssClass="textstyle"></asp:Label><label
                                            class="mandatorymark">*</label>
                                    </td>
                                    <td style="width: 30%">
                                        <span id="spanzEmployeeType" runat="server">
                                            <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="190px" ToolTip="Select Employee Status"
                                                CssClass="mandatoryField" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lbReportingTo" runat="server" Text="Line Manager" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtReportingTo" runat="server" CssClass="mandatoryField" MaxLength="30"
                                            ToolTip="Enter Line Manager" BorderStyle="Solid" Width="185px" TextMode="MultiLine">
                                        </asp:TextBox>
                                        <img id="imgReportingToPopulate" runat="server" src="Images/find.png" alt="" />
                                        <span id="spanReportingTo" runat="server">
                                            <img id="imgReportingTo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lbReportingFM" runat="server" Text="Functional Manager" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtReportingFM" runat="server" CssClass="mandatoryField" MaxLength="30"
                                            ToolTip="Enter Functional Manager" BorderStyle="Solid" Width="185px" TextMode="MultiLine"></asp:TextBox>
                                        <img id="imgReportingFM" runat="server" src="Images/find.png" alt="" />
                                        <span id="spanzReportingFM" runat="server">
                                            <img id="imgcReportingFM" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="LblEmpLocation" runat="server" Text="Location" CssClass="textstyle"></asp:Label><label
                                            class="mandatorymark">*</label>
                                    </td>
                                    <td style="width: 30%">
                                        <span id="spanzEmpLocation" runat="server">
                                            <asp:DropDownList ID="ddlEmpLocation" runat="server" Width="190px" ToolTip="Select Employee Location"
                                                CssClass="mandatoryField" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                    <%--24070-Ambar-Start--%>
                                    <%--Moved following code from pnlEmpPersonalDetails--%>
                                    <td style="width: 19%" class="txtstyle">
                                        <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                                        <asp:Label ID="lblMandatory_Gender" runat="server" class="mandatorymark" Visible="true">
                                            *</asp:Label>
                                    </td>
                                    <td style="width: 31%" align="left">
                                        <span id="spanzGender" runat="server">
                                            <asp:DropDownList ID="ddlGender" runat="server" Width="190px" CssClass="mandatoryField">
                                                <asp:ListItem Selected="True" Value="SELECT">SELECT</asp:ListItem>
                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                    <%--24070-Ambar-End--%>
                                </tr>
                                <%--Mohamed : Issue 39509/41062 : 06/03/2013 : Starts                        			  
                                     Desc :  Adding new Columns date for Probation--%>
                                <tr style="width: 100%" id="confirmDate">
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblConfirmedDate" runat="server" Text="Confirmed Date" CssClass="textstyle"></asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <uc1:DatePicker ID="ucDatePickerConfirmedDate" runat="server" />
                                        <span id="spanzConfirmedDate" runat="server">
                                            <img id="ConfirmedDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblStatusLabel" runat="server" Text="Status" CssClass="textstyle"></asp:Label>
                                    </td>
                                    <td style="width: 30%" class="txtstyle">
                                        <asp:Label ID="lblStatus" runat="server" Text="-" CssClass="textstyle"></asp:Label>
                                    </td>
                                </tr>
                                <%--Mohamed : Issue 39509/41062 : 06/03/2013 : Ends--%>
                                <!--Siddharth 25th August 2015 Start-->
                                <%--Task Id:- 46058 RMGroup should able to change RBU from employee profile page.--%>
                                <tr id="trResourceBusinessUnit" style="display:none" runat="server">
                                     <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblResourceBussinesUnit" runat="server" Text="Resource Business Unit"></asp:Label>
                                        <label class="mandatorymark">*</label>
                                    </td>
                                    <td style="width: 25%">
                                      <span id="spanzResourceBussinesUnit" runat="server">
                                            <asp:DropDownList ID="ddlResourceBussinesUnit" runat="server" Width="190px">
                                            </asp:DropDownList>
                                       </span>
                                    </td>
                                 </tr>         
                                <!--Siddharth 25th August 2015 End-->
                                
                            </table>
                        </asp:Panel>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divEmpPersonalDetails" class="detailsborder">
                        <table width="100%" class="detailsbg">
                            <tr>
                                <td>
                                    <asp:Label ID="lbEmpPersonalDetails" runat="server" Text="Personal Details" CssClass="detailsheader"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlEmpPersonalDetails" runat="server">
                            <table width="100%">
                                <tr>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblDOB" runat="server" Text="Date Of Birth"></asp:Label>
                                        <asp:Label ID="lblMandatory_DOB" runat="server" class="mandatorymark" Visible="true">
                                            *</asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <uc1:DatePicker ID="ucDatePickerDOB" runat="server" />
                                        <%--<cc1:CalendarExtender ID="CalendarDOB" runat="server" PopupButtonID="imgDOB" TargetControlID="txtDOB"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>--%>
                                        <span id="spanDOB" runat="server">
                                            <img id="imgDOBError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <%--24070-Ambar-Start--%>
                                    <%--Moved following code from pnlEmpPersonalDetails--%>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblBloodGroup" runat="server" Text="Blood Group"></asp:Label>
                                        <asp:Label ID="lblMandatory_BloodGroup" class="mandatorymark" runat="server" Visible="true">
                                            *</asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:TextBox ID="txtBloodGroup" runat="server" MaxLength="3" Width="185px" OnKeyPress="ValidateBloodGroup()"></asp:TextBox>
                                        <span id="spanBloodGroup" runat="server">
                                            <img id="imgBloodGroup" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <%--24070-Ambar-End--%>
                                </tr>
                                <tr>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
                                        <asp:Label ID="lblMandatory_Marital" class="mandatorymark" runat="server" Visible="true">
                                            *</asp:Label>
                                    </td>
                                    <td style="width: 30%">
                                        <span id="spanzMaritalStatus" runat="server">
                                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" Width="190px" OnSelectedIndexChanged="ddlMaritalStatus_SelectedIndexChanged"
                                                CssClass="mandatoryField" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="SELECT">SELECT</asp:ListItem>
                                                <asp:ListItem Value="M">Married</asp:ListItem>
                                                <asp:ListItem Value="S">Single</asp:ListItem>
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                    <td style="width: 19%" class="txtstyle">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Label ID="lblSpouseName" runat="server" Text="Spouse Name"></asp:Label>
                                                <label id="lblmandatorymark" class="mandatorymark" runat="server">
                                                    *</label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlMaritalStatus" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 31%">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtSpouseName" runat="server" MaxLength="20" Width="185px" CssClass="mandatoryField"></asp:TextBox>
                                                <span id="spanSpouseName" runat="server">
                                                    <img id="imgSpouseName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlMaritalStatus" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 19%" class="txtstyle">
                                        <asp:Label ID="lblIsFresher" runat="server" Text="Are you a Fresher?"></asp:Label>
                                        <asp:Label ID="lblMandatory_Fresher" class="mandatorymark" runat="server" Visible="true">
                                            *</asp:Label>
                                    </td>
                                    <td style="width: 31%;">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rblIsFresher" runat="server" RepeatDirection="Horizontal"
                                                    Width="75%" AutoPostBack="True">
                                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:HiddenField ID="hidReportingTo" runat="server" />
                        <asp:HiddenField ID="hidReportingFM" runat="server" />
                        <asp:HiddenField ID="HfReportingToName" runat="server" />
                        <asp:HiddenField ID="HfReportingToFM" runat="server" />
                        <asp:HiddenField ID="HfIsDataModified" runat="server" />
                        <%--Mohamed : Issue 39509/41062 : 06/03/2013 : Starts                        			  
                        Desc :  Adding new Columns date for Probation--%>
                        <asp:HiddenField ID="hfDesignationChange" runat="server" />
                        <asp:HiddenField ID="hfDepartmentChange" runat="server" />
                        <asp:HiddenField ID="hfConfirmedDate" runat="server" />
                        <asp:HiddenField ID="hfDesignationChangeDate" runat="server" />
                        <asp:HiddenField ID="hfDepartmentChangeDate" runat="server" />
                        <%--Mohamed : Issue 39509/41062 : 06/03/2013 : Ends--%>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                    
                    <!-- Siddharth 9th June 2015 Start-->
                    
                      <div id="divSecurityDetails" class="detailsborder">
                        <table width="100%" class="detailsbg">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSecurityDetails" runat="server" Text="Security Details" CssClass="detailsheader"></asp:Label>
                                </td>
                            </tr>
                        </table>
                         <asp:Panel ID="pnlEmpSecurityDetails" runat="server">
                            <table width="100%">
                                <tr>
                                 <td style="width: 20%" class="txtstyle">
                                    <asp:Label ID="lblBPSSVersion" runat="server" Text="BPSS Version"></asp:Label>
                                 </td>
                                 <td style="width: 30%">
                                        <span id="span2" runat="server">
                                            <%--Ambar - Added selectedindexchanged event--%>
                                            <asp:DropDownList ID="ddlBPSSVersion" runat="server" Width="190px" ToolTip="Select BPSS Version"
                                              AutoPostBack="false"  CssClass="mandatoryField">
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblCompletionDate" runat="server" Text="Completion Date"></asp:Label>
                                  <%--      <asp:Label ID="Label2" runat="server" class="mandatorymark" Visible="true">
                                            *</asp:Label>--%>
                                    </td>
                                    <td style="width: 30%">
                                        <uc1:DatePicker ID="ucdatepickerCompletionDate" runat="server" />
                                        <span id="span1" runat="server">
                                            <img id="img1" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                 </tr>
                             </table>
                        </asp:Panel>
                      </div>
                      <!-- Siddharth 9th June 2015 End-->
                    
                    
                    <%--<div id="divEmpTabDetails" class="detailsborder">
                    </div>--%>
                    <%--Ishwar Patil NISRMS 20112014 Start--%>
                  <!-- <asp:Panel runat="server" ID="pnlCostCode" class="detailsborder" Visible ="false">
                        <div>
                           <%-- <table style="width:100%">
                                <tr>
                                    <td style="width: 20%" class="txtstyle">
                                        <asp:Label ID="lblCostCode" runat="server" Text="Cost Code" CssClass="textstyle"></asp:Label>
                                    </td>
                                    <td style="width: 80%" align="left">
                                        <asp:TextBox ID="txtCostCode" runat="server" CssClass="mandatoryField" MaxLength="250"
                                            ToolTip="Enter Cost Code" BorderStyle="Solid" Width="185px">
                                        </asp:TextBox>
                                        <span id="spancostcode" runat="server">
                                            <img id="img1" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                            </table>--%>
                            
                      <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                
                                <!--Siddharth 7 April 2015 Start -->
                             <%--   <asp:UpdatePanel ID="EmpCostCodeUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                                    <asp:GridView ID="gvCostCode" runat="server" AutoGenerateColumns="False" 
                                    OnRowDataBound="gvCostCode_RowDataBound"  OnRowCommand="gvCostCode_RowCommand" 
                                     GridLines="None" Width="100%">
                           
                                <HeaderStyle CssClass="headerStyle" />
                                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                <RowStyle Height="20px" CssClass="textstyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Project" ItemStyle-Width="160px" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="HFSkillNo" runat="server" Value='<%#Eval("ProjectNo") %>' />    
                                            <asp:HiddenField ID="HFSkillName" runat="server" Value='<%#Eval("ProjectName") %>' />    
                                            <span id="spanzProject" runat="server">
                                            <asp:DropDownList ID="ddlProject" runat="server" ToolTip="Select Project" Width="200px"
                                            OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AutoPostBack="true" />
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost Code" ItemStyle-Width="200px" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                           <label id="lblmandatorymarkCostCode" class="mandatorymark" runat="server" visible="false">
                                                    *</label> 
                                           <span id="spanzCostCode" runat="server">
                                           <asp:TextBox ID="txtCostCode" runat="server" ToolTip="Enter Cost Code" V Text= '<%#Eval("CostCode") %>'  Width="150px" />
                                           </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Billing" ItemStyle-Width="200px" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                         <label id="lblmandatorymarkBilling" class="mandatorymark" runat="server" visible="false">
                                                    *</label> 
                                                    <span id="spanzBilling" runat="server">
                                           <asp:TextBox ID="txtBilling" runat="server" ToolTip="Enter Billing"  Text= '<%#Eval("Billing") %>'  Width="150px" />
                                           </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                                CommandName="DeleteRow" ToolTip="Delete Row" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                               </ContentTemplate>
                    </asp:UpdatePanel>--%>
                            <!--Siddharth 7 April 2015 End -->
                        <!--  </td>
                        </tr>
                    </table>-->
        
                   <%--  <table style="width: 100%">
                         <tr>
                             <td align="center">
                                 <asp:Button ID="btnAddRow" runat="server" Text="Add New Row" OnClick="btnAddRow_Click"
                              CssClass="button" />
                          </td>
                        </tr>
                     </table>--%>
                            
                            
                      <!--  </div>
                    </asp:Panel> -->
                    <%--Ishwar Patil NISRMS 20112014 End--%>
                    <div class="detailsborder">
                        <table width="100%">
                            <tr>
                                <td style="width: 10%" align="left">
                                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="button" OnClick="btnPrevious_Click"
                                        Visible="false" />
                                </td>
                                <td style="width: 10%" align="left">
                                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="button" OnClick="btnNext_Click"
                                        Visible="false" />
                                </td>
                                <td style="width: 80%" align="right">
                                    <asp:Button ID="btnResume" runat="server" Text="Resume" CssClass="button" OnClick="btnResume_Click"
                                        Visible="false" />
                                    <%--<asp:Button ID="btnRelease" runat="server" Text="Release" CssClass="button" Visible="false" />--%>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="button" OnClick="btnEdit_Click"
                                        Visible="false" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" OnClick="btnUpdate_Click"
                                    Visible="false" />
                                    <%--OnClientClick="if (!ValidateForm()) { return false;};" Visible="false" />--%>
                                    <%--OnClientClick="return ValidateForm();" Visible="false" />--%>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" OnClick="btnDelete_Click"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div>
           <asp:HiddenField id="Hidden_EmployeeID" runat="server"/>
           <asp:HiddenField id="Hidden_LoggedInUser" runat="server"/>    
    </div>
    
</asp:Content>
