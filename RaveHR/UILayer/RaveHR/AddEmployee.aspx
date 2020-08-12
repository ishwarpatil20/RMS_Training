<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddEmployee.aspx.cs" Inherits="AddEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script type="text/javascript" src="JavaScript/CommonValidations.js"></script>

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        var retVal;
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $("#<%=imgEmpEmailPopulate.ClientID %>").on("click", function(e) {
                    $.modalDialog.create({ url: "EmployeesEmailList.aspx", maxWidth: 550,
                        onclose: function(e) {
                            var txtEmailID = $('#<%=txtEmailID.ClientID %>');
                            if (retVal == undefined) {
                                txtEmailID.val("");
                            }
                            else {
                                txtEmailID.val(retVal);
                            }
                        }
                    }).open();
                });

                $("#<%=imgReportingFM.ClientID %>").on("click", function(e) {
                    $.modalDialog.create({ url: "EmployeesList.aspx?str=functionalMngr", maxWidth: 550,
                        onclose: function(e) {
                            var txtReportingTo = $('#<%=txtReportingFM.ClientID %>');
                            var hidReportingTo = $('#<%=hidReportingToFM.ClientID %>');
                            var hidReportingToValue = $('#<%=hidReportingToFM.ClientID %>').val();

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

                                if (EmpName == undefined) {
                                    EmpName = emp1[1];
                                }
                            }
                            if (EmpId != undefined) {
                                hidReportingTo.val(EmpId.trim());
                            }
                            if (EmpName != undefined) {
                                txtReportingTo.val(EmpName.trim());
                            }
                        }
                    }).open();
                });

                $("#<%=imgReportingToPopulate.ClientID %>").on("click", function(e) {
                    $.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                        onclose: function(e) {
                            var txtReportingTo = $('#<%=txtReportingTo.ClientID %>');
                            var hidReportingTo = $('#<%=hidReportingTo.ClientID %>');
                            var hidReportingToValue = $('#<%=hidReportingTo.ClientID %>').val();

                            //Sanju:Issue Id 50201: End
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
                                txtReportingTo.val(EmpName.trim());
                            }
                        }
                    }).open();
                });

                $("#<%=imgWinowsUsername.ClientID %>").on("click", function(e) {
                    $.modalDialog.create({ url: "EmployeesWindowsUsernameList.aspx", maxWidth: 550,
                        onclose: function(e) {
                            var txtUsername = $('#<%=txtWindowsUsername.ClientID %>');
                            if (retVal == undefined) {
                                txtUsername.val("");
                            }
                            else {
                                txtUsername.val(retVal);
                            }
                        }
                    }).open();
                });

                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        retVal = e.data;
                    }
                });
            });
        })(jQuery);
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        setbgToTab('ctl00_tabEmployee', 'ctl00_SpanAddEmployee');
        function $(v) { return document.getElementById(v); }

        function ButtonClickValidate() {
            //debugger;
            var lblMandatory;
            var spanlist = "";

            var txtEmployeeCode = $('<%=txtEmployeeCode.ClientID %>').id
            var txtFirstName = $('<%=txtFirstName.ClientID %>').id
            var txtLastName = $('<%=txtLastName.ClientID %>').id
            var txtEmailID = $('<%=txtEmailID.ClientID %>').id
            var txtUsername = $('<%=txtWindowsUsername.ClientID %>').id
            var txtJoiningDate = $('<%=ucDatePicker.ClientID %>').id
            var txtReportingTo = $('<%=txtReportingTo.ClientID %>').id
            var txtReportingFM = $('<%=txtReportingFM.ClientID %>').id

            var ddlPrefix = $('<%=ddlPrefix.ClientID %>').value;
            var ddlMRFCode = $('<%=ddlMRFCode.ClientID %>').value;
            var ddlDepartment = $('<%=ddlDepartment.ClientID %>').value;
            var ddlEmployeeType = $('<%=ddlEmployeeType.ClientID %>').value;
            var ddlBand = $('<%=ddlBand.ClientID %>').value;
            var ddlDesignation = $('<%=ddlDesignation.ClientID %>').value;
            var ddlResourceBussinesUnit = $('<%=ddlResourceBussinesUnit.ClientID %>').value;


            if (ddlPrefix == "" || ddlPrefix == "SELECT") {
                var sPrefix = $('<%=spanzPrefix.ClientID %>').id;
                spanlist = spanlist + sPrefix + ",";
            }

            if (ddlMRFCode == "" || ddlMRFCode == "SELECT") {
                var sMRFCode = $('<%=spanzMRFCode.ClientID %>').id;
                spanlist = spanlist + sMRFCode + ",";
            }

            if (ddlDepartment == "" || ddlDepartment == "SELECT") {
                var sDepartment = $('<%=spanzDepartment.ClientID %>').id;
                spanlist = spanlist + sDepartment + ",";
            }

            if (ddlEmployeeType == "" || ddlEmployeeType == "SELECT") {
                var sEmployeeType = $('<%=spanzEmployeeType.ClientID %>').id;
                spanlist = spanlist + sEmployeeType + ",";
            }

            if (ddlBand == "" || ddlBand == "SELECT") {
                var sBand = $('<%=spanzBand.ClientID %>').id;
                spanlist = spanlist + sBand + ",";
            }

            if (ddlDesignation == "" || ddlDesignation == "SELECT") {
                var sddlDesignation = $('<%=spanzDesignation.ClientID %>').id;
                spanlist = spanlist + sddlDesignation + ",";
            }

            if (ddlResourceBussinesUnit == "" || ddlResourceBussinesUnit == "SELECT") {
                var sResourceBussinesUnit = $('<%=spanzResourceBussinesUnit.ClientID %>').id;
                spanlist = spanlist + sResourceBussinesUnit + ",";
            }

            if (spanlist == "") {
                var controlList = txtEmployeeCode + "," + txtFirstName + "," + txtLastName + "," + txtEmailID + "," + txtJoiningDate + "," +
                              txtReportingTo + "," + txtReportingFM + "," + txtUsername
            }
            else {
                var controlList = spanlist + txtEmployeeCode + "," + txtFirstName + "," + txtLastName + "," + txtEmailID + "," +
                              txtJoiningDate + "," + txtReportingTo + "," + txtReportingFM + "," + txtUsername
            }

            if (ValidateControlOnClickEvent(controlList) == false) {
                lblMandatory = $('<%=lblMandatory.ClientID %>')
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
            }

            //        if(txtDepartment.value == "0")
            //        {
            //            return false;
            //        }


            return ValidateControlOnClickEvent(controlList);


        }

        function fnConfirm() {
            return confirm('Do you realy want to Abort');

        }

        //Check Emailid validation.
        function validateEmailId(emailId) {
            //Check if email image button is not visible(means null) then need to do validation.
            if ($('<%=imgEmpEmailPopulate.ClientID %>') == null) {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

                if (reg.test(emailId) == false) {
                    alert('Invalid Email Id');
                    return false;
                }
            }

        }

        // Ambar-26755-Start
        //This function allows only to enter alphabate values.
        function Alphabatesonly(event, targetControl) {
            var Point = targetControl.value;
            var regExObj = new RegExp();
            window.clipboardData.clearData();
            if ((event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122) || (event.keyCode == 32)) {
                return true;
            }
            else {
                return false;
            }
        }
        // Ambar-26755-End
        
    </script>

    <table width="100%">
        <tr>
            <%-- Sanju:Issue Id 50201: Added new class so that all browsers display header--%>
            <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <%--    Sanju:Issue Id 50201: End--%>
                <asp:Label ID="lblAddEmployee" runat="server" Text="Add Employee" CssClass="header"></asp:Label>
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
                <asp:Label ID="lblMessage" runat="server" CssClass="text"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfirmationMessage" runat="server" CssClass="Messagetext"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
            </td>
        </tr>
    </table>
    <div class="detailsborder">
        <table width="100%" class="detailsbg">
            <tr>
                <td>
                    <asp:Label ID="lblEmployeeDetailsGroup" runat="server" Text="Employee Details" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblMRFCode" runat="server" Text="MRF Code" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%">
                </td>
                <td colspan="3">
                    <span id="spanzMRFCode" runat="server">
                        <asp:DropDownList ID="ddlMRFCode" runat="server" Width="340px" ToolTip="Select MRF Code"
                            CssClass="mandatoryField" TabIndex="11" OnSelectedIndexChanged="ddlMRFCode_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </span>
                </td>
                </td>
                <td rowspan="4" align="right">
                    <asp:Image ID="imgEmp" runat="server" Width="100px" Height="100px" />
                    <asp:ImageButton ID="imgbtnUpload" runat="server" ImageUrl="~/Images/upload.JPG"
                        OnClick="imgbtnUpload_Click" ToolTip="Upload Image" />
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblPrefix" runat="server" Text="Prefix" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                    <span id="spanzPrefix" runat="server">
                        <asp:DropDownList ID="ddlPrefix" runat="server" Width="190px" ToolTip="Select Prefix"
                            CssClass="mandatoryField" TabIndex="11">
                        </asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblEmpCode" runat="server" Text="Employee Code" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%" valign="top" align="right">
                    <span id="spanzEmployeeCode" runat="server">
                        <img id="imgEmployeeCode" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="mandatoryField" MaxLength="30"
                        ToolTip="Enter Employee Code" BorderStyle="Solid" TabIndex="1" Width="185px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%" valign="top" align="right">
                    <span id="spanzFirstName" runat="server">
                        <img id="imgFirstName" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 25%">
                    <%--Ambar-26755-Start : Added onkeypress event--%>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="mandatoryField" MaxLength="50"
                        ToolTip="Enter First name" BorderStyle="Solid" TabIndex="1" Width="185px" onkeypress="return Alphabatesonly(event,this)">
                    </asp:TextBox>
                </td>
            </tr>
            <!-- sudip -->
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name" CssClass="textstyle"></asp:Label>
                </td>
                <td width="5%" valign="top" align="right">
                    <span id="spanMiddleName" runat="server">
                        <img id="imgMiddleName" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 25%">
                    <%--Ambar-26755-Start : Added onkeypress event--%>
                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="mandatoryField" MaxLength="50"
                        ToolTip="Enter Middle Name" BorderStyle="Solid" TabIndex="1" Width="185px" onkeypress="return Alphabatesonly(event,this)">
                    </asp:TextBox>
                </td>
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblBrowseImage" runat="server" Text="Browse Image" CssClass="textstyle"></asp:Label>
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="250px" />
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td width="5%" valign="top" align="right">
                    <span id="spanzLastName" runat="server">
                        <img id="imgLastName" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 25%">
                    <%--Ambar-26755-Start : Added onkeypress event--%>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="mandatoryField" MaxLength="50"
                        ToolTip="Enter Last Name" BorderStyle="Solid" TabIndex="1" Width="185px" onkeypress="return Alphabatesonly(event,this)">
                    </asp:TextBox>
                </td>
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblEmailID" runat="server" Text="Email ID" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%" valign="top" align="right">
                    <span id="spanzEmailID" runat="server">
                        <img id="imgEmailIDspan" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtEmailID" runat="server" CssClass="mandatoryField" MaxLength="50"
                        ToolTip="Enter Email ID" BorderStyle="Solid" TabIndex="1" Width="185px" onblur="return validateEmailId(this.value)">
                    </asp:TextBox>
                    <img id="imgEmpEmailPopulate" runat="server" src="Images/find.png" alt="" />
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblDepartment" runat="server" Text="Department" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                    <span id="spanzDepartment" runat="server">
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="190px" ToolTip="Select Group"
                            CssClass="mandatoryField" TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                        </asp:DropDownList>
                    </span>
                </td>
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblEmployeeType" runat="server" Text="Employee Type" CssClass="textstyle"></asp:Label><label
                        class="mandatorymark">*</label>
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                    <span id="spanzEmployeeType" runat="server">
                        <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="190px" ToolTip="Select Employee Status"
                            CssClass="mandatoryField" TabIndex="11" 
                        onselectedindexchanged="ddlEmployeeType_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblBand" runat="server" Text="Band" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                    <span id="spanzBand" runat="server">
                        <asp:DropDownList ID="ddlBand" runat="server" Width="190px" ToolTip="Select Band"
                            TabIndex="11">
                        </asp:DropDownList>
                    </span>
                </td>
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblDesignation" runat="server" Text="Designation" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                    <span id="spanzDesignation" runat="server">
                        <asp:UpdatePanel ID="updesignation" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlDesignation" runat="server" Width="210px" ToolTip="Select Designation"
                                    CssClass="mandatoryField" TabIndex="11">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlDepartment" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </span>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblJoiningDate" runat="server" Text="Joining Date" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%" valign="top" align="right">
                    <span id="spanzJoiningDate" runat="server">
                        <img id="imgJoiningDateError" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 25%">
                    <%--<asp:TextBox ID="txtJoiningDate" runat="server" CssClass="mandatoryField" MaxLength="30"
                        ToolTip="Enter Joining Date" BorderStyle="Solid" TabIndex="1" Width="185px"></asp:TextBox>
                    <asp:ImageButton ID="imgJoiningDate" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                        CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="5" />--%>
                    <uc1:DatePicker ID="ucDatePicker" runat="server" />
                    <%--<cc1:CalendarExtender ID="calendarJoiningDate" runat="server" PopupButtonID="imgJoiningDate"
                        TargetControlID="txtJoiningDate" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>--%>
                </td>
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblResourceBussinesUnit" runat="server" Text="Resource Business Unit"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                    <span id="spanzResourceBussinesUnit" runat="server">
                        <asp:DropDownList ID="ddlResourceBussinesUnit" runat="server" TabIndex="7" Width="190px">
                        </asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lbReportingTo" runat="server" Text="Accountable To" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%" valign="top" align="right">
                    <span id="spanzReportingTo" runat="server">
                        <img id="imgReportingTo" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtReportingTo" runat="server" CssClass="mandatoryField" MaxLength="30"
                        ToolTip="Enter Accountable To" BorderStyle="Solid" TabIndex="1" Width="185px"
                        TextMode="MultiLine"></asp:TextBox>
                    <img id="imgReportingToPopulate" runat="server" src="Images/find.png" alt="" />
                </td>
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lbReportingFM" runat="server" Text="Reporting To" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%">
                    <span id="spanzReportingFM" runat="server">
                        <img id="imgcReportingFM" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtReportingFM" runat="server" CssClass="mandatoryField" ToolTip="Enter Reporting To"
                        BorderStyle="Solid" TabIndex="1" Width="185px" TextMode="MultiLine"></asp:TextBox>
                    <img id="imgReportingFM" runat="server" src="Images/find.png" alt="" />
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblExternalWorkExp" runat="server" Text="External Work Experience"
                        CssClass="textstyle"></asp:Label>
                </td>
                <td style="width: 5%" valign="top" align="right">
                    <span id="spanExternalWorkExp" runat="server">
                        <img id="imgExternalWorkExp" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtReleventYears" runat="server" CssClass="mandatoryField" Width="35px"
                        ReadOnly="true">
                    </asp:TextBox>&nbsp;Years <span id="spanReleventYears" runat="server">
                        <img id="imgReleventYears" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                    <asp:TextBox ID="txtReleventMonths" runat="server" CssClass="mandatoryField" Width="35px"
                        ReadOnly="true">
                    </asp:TextBox>&nbsp;Months <span id="spanReleventMonths" runat="server">
                        <img id="imgReleventMonths" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="lblLocation" runat="server" Text="Location" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                    <span id="SpanLocation" runat="server">
                        <asp:DropDownList ID="ddlLocation" runat="server" TabIndex="10" Width="190px">
                        </asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%" class="txtstyle">
                    <asp:Label ID="Label1" runat="server" Text="Windows User Name" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 5%" valign="top" align="right">
                    <span id="spanWindowsUsername" runat="server">
                    </span>
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtWindowsUsername" runat="server" CssClass="mandatoryField" MaxLength="30"
                        ToolTip="Enter Windows Username" BorderStyle="Solid" TabIndex="1"></asp:TextBox>
                    <img id="imgWinowsUsername" runat="server" src="Images/find.png" alt="" />
                </td>
                <td style="width: 20%" class="txtstyle">
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 20%">
                    <asp:HiddenField ID="hidReportingTo" runat="server" />
                </td>
                <td style="width: 5%" valign="top" align="right">
                </td>
                <td style="width: 25%">
                    <asp:HiddenField ID="hidReportingToFM" runat="server" />
                </td>
                <td style="width: 20%">
                </td>
                <td style="width: 5%">
                </td>
                <td style="width: 25%">
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table width="100%">
        <tr>
            <td style="width: 25%">
                <asp:HiddenField ID="hidmainTabProject" runat="server" Value="0" />
            </td>
            <td style="width: 25%">
                <asp:HiddenField ID="hdEmpMailID" runat="server" Value="0" />
            </td>
            <td style="width: 12.5%" align="left">
                <asp:Button ID="btnPrevious" runat="server" Text="Previous" Visible="False" BackColor="#003399"
                    ForeColor="White" CausesValidation="false" CssClass="button" />
            </td>
            <td style="width: 12.5%" align="left">
                <asp:Button ID="btnNext" runat="server" Text="Next" Visible="False" BackColor="#003399"
                    ForeColor="White" CausesValidation="false" CssClass="button" />
            </td>
            <td style="width: 12.5%" align="left">
                <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="true" CssClass="button"
                    TabIndex="18" OnClick="btnAdd_Click" />
            </td>
            <td style="width: 12.5%" align="left">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                    CssClass="button" TabIndex="19" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
