<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpSetReleaseDate.aspx.cs"
    Inherits="EmpSetReleaseDate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Set Release Date</title>
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="~/../JavaScript/DatePicker.js"></script>

    <script type="text/javascript" src="~/../JavaScript/CommonValidations.js"></script>
    
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <link href="CSS/jquery.modalDialogContent.css" rel="stylesheet" type="text/css" />
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinnyContent.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialogContent.js" type="text/javascript"></script>
        <%--<script src="JavaScript/skinny.js" type="text/javascript"></script>--%>

    <%--<script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>--%>
    <%--Siddhesh : Issue 55546 : 28/07/2015 : Start--%>
    <%--<script src="JavaScript/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <%--Siddhesh : Issue 55546 : 28/07/2015 : End    --%>
    <script language="javascript" type="text/javascript">
        //Ishwar : Modal PopUp Issue 13012015 Start
        var retVal;
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        retVal = e.data;
                    }
                });

                $('#btnSave').click(function() {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                        //$(this).parents('#form').submit();
                    

                });

//                $(window).on('beforeunload', function() {
//                    var button1 = $('#btnSave');
//                    button1.attr('disabled', 'disabled');
//                    button1.val('Please Wait..');

//                });

            });
        })(jQuery);

        function CloseDialog() {
            jQuery.modalDialog.getCurrent().close();
        };
        
        function popUpEmployeeSearch() {
            jQuery.modalDialog.create({ url: 'EmployeesList.aspx?PageName=MrfRaiseNextOrRaiseHeadCount', maxWidth: 500,
                onclose: function(e) {
                    var EmpName;
                    var EmpId;
                    //Ishwar 13012015 Start
                    var valReturned;
                    valReturned = retVal;
                    //Ishwar 13012015 End
                    var employee = new Array();
                    if (retVal != undefined)
                        employee = valReturned.split(",");
                    for (var i = 0; i < employee.length - 1; i++) {
                        if (EmpId == undefined && EmpName == undefined) {
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
                    }

                    if (EmpId != undefined) {
                        document.getElementById("<%#hidReportingTo.ClientID%>").value = EmpId.trim();
                    }
                    if (EmpName != undefined) {
                        document.getElementById("<%#txtReportingTo.ClientID%>").value = EmpName.trim();
                    }
                }
            }).open();
        }
        //Ishwar : Modal PopUp Issue 13012015 End
        
        //Add New Function 
        //Start-Ishwar--49082
        function EnableRelasedDate(chkRelasedID) {
            if (document.getElementById(chkRelasedID).checked == true) {
                document.getElementById("ucDatePickerReleaseDate_txtDate").disabled = false;
                document.getElementById("ucDatePickerReleaseDate_imgDate").disabled = false;
            }
            else {
                document.getElementById("ucDatePickerReleaseDate_txtDate").disabled = true;
                document.getElementById("ucDatePickerReleaseDate_imgDate").disabled = true;
            }
        }
        //End Function 
        //Ishwar--49082
        
        function PopulateData(empaid, releasedate, reportingTo, billing, utilization, projectname, empname, projectenddate, projectid, clientName, RowIndex) {
            var id;
            var date;
            var rptTo;
            id = empaid * 1;
            date = releasedate;
            rptTo = reportingTo;
            if (date == '01/01/0001') return;

            document.getElementById("hfEmpProjectAllocationId").value = empaid;
            document.getElementById("txtReportingTo").value = reportingTo;
            document.getElementById("txtBilling").value = billing;
            document.getElementById("txtUtilization").value = utilization;
            document.getElementById("ucDatePickerReleaseDate_txtDate").value = releasedate;
            document.getElementById("hfProjectName").value = projectname;
            document.getElementById("hfEmpName").value = empname;
            document.getElementById("hfProjectEndDate").value = projectenddate;
            document.getElementById("hfProjectId").value = projectid;
            document.getElementById("hfRowindex").value = RowIndex;
            document.getElementById("hfClientName").value = clientName;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="detailsborder">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%">
            <tr>
                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <span class="header">Employee Set Release Date</span>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <%--30163-Subhra-start--%>
                <td>
                    <asp:Label ID="lblReleaseWarning" runat="server" Font-Names="Verdana" Font-Size="9pt"
                        ForeColor="Red"></asp:Label>
                </td>
                <asp:HiddenField ID="hfSystemDate" runat="server" />
                <%--30163-Subhra-end--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td style="width: 25%">
                    <asp:Label ID="lblReleaseDate" runat="server" Text="Release Date"></asp:Label>
                </td>
                <td style="width: 25%">
                    <uc1:DatePicker ID="ucDatePickerReleaseDate" runat="server" Width="150" />
                </td>
                <td style="width: 25%">
                    <asp:Label ID="lblReportingTo" runat="server" Text="Accountable To"></asp:Label>
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="txtReportingTo" runat="server" Width="150"></asp:TextBox>
                    <%--31579 Abhishek start--%>
                    <img id="imgReportingToPopulate" runat="server" src="Images/find.png" alt="" />
                    <asp:HiddenField ID="hidReportingTo" runat="server" />
                    <asp:HiddenField ID="hidReportingToOld" runat="server" />
                    <%--end--%>
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    <asp:Label ID="lblUtilization" runat="server" Text="Utilization"></asp:Label>
                </td>
                <td style="width: 25%">
                    <!-- Issue ID : 42856 Mahendra 24-06-2013 Start Desc Added OnTextChangedEvent-->
                    <asp:TextBox ID="txtUtilization" runat="server" Width="150" OnTextChanged="txtUtilization_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td style="width: 25%">
                    <asp:Label ID="lblBillUtilData" runat="server" Text="Utilization Change Date"></asp:Label>
                    <label class="mandatorymark">
                        *</label><br />
                    <label>
                        (Last Modified)</label>
                </td>
                <td style="width: 25%">
                    <uc1:DatePicker ID="ucDatePickerBillUtilDate" runat="server" Width="150" />
                    <span id="spanBillUtilDate" runat="server">
                        <img id="imgcBillUtilDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    <asp:Label ID="lbl" runat="server" Text="Billing"></asp:Label>
                </td>
                <td style="width: 25%">
                <!-- Issue ID : 42856 Mahendra 24-06-2013 Start Desc Added OnTextChangedEvent-->
                    &nbsp;<asp:TextBox ID="txtBilling" runat="server" Width="150" OnTextChanged="txtBilling_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                <td style="width: 25%">
                    <asp:Label ID="lblBillingDate" runat="server" Text="Billing Change Date"></asp:Label>
                    <label class="mandatorymark">
                        *</label><br />
                    <label>
                        (Last Modified)</label>
                </td>
                <td style="width: 25%">
                    <uc1:DatePicker ID="ucDatePickerBillDate" runat="server" Width="150" />
                    <span id="span1" runat="server">
                        <img id="img1" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    <asp:Label ID="Label1" runat="server" Text="Remarks"></asp:Label>
                </td>
                <td style="width: 25%">
                    &nbsp;<asp:TextBox ID="txtReasonforExtension" runat="server" MaxLength="500" Width="150"></asp:TextBox></td>
                <td style="width: 25%">
                    <asp:Label ID="lblMRFBillingDate" runat="server" Text="Resource Billing Date"></asp:Label>
                </td>
                <td style="width: 25%">
                    <uc1:DatePicker ID="ucDatePickerMrfBillingDate" runat="server" Width="150" />
                    <span id="span2" runat="server">
                        <img id="img2" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    <asp:Label ID="lblIsreleased" runat="server" Text="Is Released"></asp:Label>
                </td>
                <td style="width: 25%">
                    <asp:CheckBox ID="chkIsReleased" runat="server" onClick="return EnableRelasedDate(this.id)" />
                </td>
                 <td style="width: 25%">
                    <asp:Label ID="lblByPass4CValidation" runat="server" Text="ByPass 4C Validation" Visible="false"></asp:Label>
                </td>
                 <td style="width: 25%">
                    <asp:CheckBox ID="Chk4CByPassValidation" runat="server" Visible="false"/>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:UpdatePanel runat="server" ID="UP_EmployeeSummary">
                        <ContentTemplate>
                            <div style="height: 100px">
                                <asp:GridView ID="grdvListofEmployees" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" AllowSorting="True" DataKeyNames="EmpProjectAllocationId"
                                    Width="955px" OnSorting="grdvListofEmployees_Sorting" OnRowCreated="grdvListofEmployees_RowCreated"
                                    OnPageIndexChanging="grdvListofEmployees_PageIndexChanging" OnDataBound="grdvListofEmployees_DataBound"
                                    OnRowDataBound="grdvListofEmployees_RowDataBound">
                                    <HeaderStyle CssClass="headerStyle" />
                                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                    <RowStyle Height="20px" CssClass="textstyle" />
                                    <Columns>
                                        <asp:BoundField DataField="EMPId" HeaderText="Employee ID" SortExpression="EMPCode"
                                            Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="11%" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpProjectAllocationId" HeaderText="Employee PAID" SortExpression="EMPCode"
                                            Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="11%" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EMPCode" HeaderText="Employee Code" SortExpression="EMPCode">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="5%" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="FirstName">
                                            <HeaderStyle Width="17%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName">
                                            <HeaderStyle Width="15%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName">
                                            <HeaderStyle Width="15%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role">
                                            <HeaderStyle Width="15%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProjectReleaseDate" HeaderText="Exp. Release Date" SortExpression="EndDate"
                                            DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle Width="11%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Utilization" HeaderText="Util.(%)" SortExpression="Utilization">
                                            <HeaderStyle Width="5%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Billing" HeaderText="Bill.(%)" SortExpression="Billing">
                                            <HeaderStyle Width="5%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="DeptName">
                                            <HeaderStyle Width="15%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReportingTo" HeaderText="ReportingTo" SortExpression="ReportingToId">
                                            <HeaderStyle Width="15%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProjectEndDate" HeaderText="Project EndDate" SortExpression="ProjectEndDate"
                                            DataFormatString="{0:dd/MM/yyyy}" Visible="false">
                                            <HeaderStyle Width="11%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProjectId" HeaderText="ProjectId" SortExpression="ProjectId"
                                            Visible="false">
                                            <HeaderStyle Width="11%" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerTemplate>
                                        <table class="tablePager" width="100%">
                                            <tr>
                                                <td align="center">
                                                    &lt;&lt; &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white" CommandName="Previous"
                                                        OnCommand="ChangePage" Font-Underline="true" Enabled="False">
                                                Previous
                                                    </asp:LinkButton>
                                                    <span>Page</span>
                                                    <asp:TextBox ID="txtPages" runat="server" OnTextChanged="txtPages_TextChanged" AutoPostBack="true"
                                                        Width="21px" MaxLength="3" onpaste="return false;">
                                                    </asp:TextBox>
                                                    <span>of</span>
                                                    <asp:Label ID="lblPageCount" runat="server" ForeColor="white">
                                                    </asp:Label>
                                                    <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                                        OnCommand="ChangePage" Font-Underline="true" Enabled="False">
                                                Next
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp; &gt;&gt;
                                                </td>
                                            </tr>
                                        </table>
                                    </PagerTemplate>
                                </asp:GridView>
                            </div>
                            <div>
                                <asp:Label ID="lblResoursePlan" runat="server" Text="Resourse Plan Detail" Visible="false"
                                    Font-Bold="true"></asp:Label>
                            </div>
                            <div>
                                <asp:GridView ID="grdvListofEmpProject" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" AllowSorting="True" Width="966px" OnRowCommand="grdvListofEmpProject_RowCommand">
                                    <HeaderStyle CssClass="headerStyle" />
                                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                    <RowStyle Height="20px" CssClass="textstyle" />
                                    <Columns>
                                        <asp:BoundField DataField="ResourceLocation" HeaderText="Location">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="11%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Utilization" HeaderText="Utilization(%)">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="11%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Billing" HeaderText="Billing(%)">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="11%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProjectLocation" HeaderText="Project Location">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="12%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ResourceStartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="12%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ResourceEndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="12%" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:HiddenField ID="hfEmpProjectAllocationId" runat="server" />
                            <asp:HiddenField ID="hfEmpID" runat="server" />
                            <asp:HiddenField ID="hfProjectName" runat="server" />
                            <asp:HiddenField ID="hfProjectEndDate" runat="server" />
                            <asp:HiddenField ID="hfProjectId" runat="server" />
                            <asp:HiddenField ID="hfEmpName" runat="server" />
                            <asp:HiddenField ID="hfReportingToID" runat="server" />
                            <asp:HiddenField ID="hfRowindex" runat="server" />
                            <asp:HiddenField ID="hfClientName" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td style="width: 80%; text-align: right">
                </td>
                <td style="width: 10%; text-align: right">
                    <%--Siddhesh : Issue 55546 : 28/07/2015 : Start--%>
                    <%--Description: Validate Billing Value before release. Added OnClientClick() on client side and removed from server side.--%>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="true" CssClass="button"
                        OnClick="btnSave_Click"  OnClientClick="return ValidateAll();" />
                    <%--Siddhesh : Issue 55546 : 28/07/2015 : End--%>
                </td>
                <td style="width: 10%; text-align: right">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClientClick="CloseDialog();"/>
                    <%--Ishwar : PopUp Issue 13012015 Start
                    OnClick="btnCancel_Click"
                    Ishwar : PopUp Issue 13012015 End--%>
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script language="javascript" type="text/javascript">
        var intBilling = document.getElementById("txtBilling").value;
        var intUtilization = document.getElementById("txtUtilization").value;
        var intucDataPickerBillUtil = document.getElementById('ucDatePickerBillUtilDate_txtDate').value;
        var intucDatePickerBillDate = document.getElementById('ucDatePickerBillDate_txtDate').value;

        function SaveClickValidate() {

            var lblMandatory;
            var spanlist = "";
            var Billing;
            var Utilization;
            var UtilizationBillingDate;
            var chekBoxChecked;
            Billing = document.getElementById("txtBilling").value;
            Utilization = document.getElementById("txtUtilization").value;
            UtilizationBillingDate = document.getElementById('ucDatePickerBillUtilDate_txtDate').value;

            var BillAndUtilizationDate = 'ucDatePickerBillUtilDate_txtDate';

            chekBoxChecked = document.getElementById('<%=chkIsReleased.ClientID%>');
            if (spanlist == "") {
                var controlList = BillAndUtilizationDate;
            }
            else {
                var controlList = spanlist + "," + BillAndUtilizationDate;
            }

            if (!chekBoxChecked.checked) {

                if ((Utilization != intUtilization)) {

                    if (UtilizationBillingDate == "") {
                        if (ValidateControlOnClickEvent(controlList) == false) {

                            var lblError = document.getElementById('<%=lblError.ClientID %>');
                            lblError.innerText = "Please fill all mandatory fields.";
                            lblError.style.color = "Red";
                            return false;
                        }
                        else return true;
                    }

                }
            }


            var nwspanlist = ""
            var nwChangeUtilBillDate = document.getElementById('ucDatePickerBillUtilDate_txtDate').value;
            var nwBillAndUtilizationDate = 'ucDatePickerBillUtilDate_txtDate';
            if (nwspanlist == "") {
                var nwcontrolList = nwBillAndUtilizationDate;
            }
            else {
                var nwcontrolList = spanlist + "," + nwBillAndUtilizationDate;
            }

            if (nwChangeUtilBillDate == "" && intucDataPickerBillUtil != "") {

                if (ValidateControlOnClickEvent(nwcontrolList) == false) {

                    var nwlblError = document.getElementById('<%=lblError.ClientID %>');
                    nwlblError.innerText = "The Date field Can not be null.";
                    nwlblError.style.color = "Red";
                    return false;
                }
                else return true;

            }




        }

        function BillingChangeValidate() {

            var lblMandatory;
            var spanlist = "";
            var Billing;
            var Utilization;
            var BillingChangeDate;
            var chekBoxChecked;
            Billing = document.getElementById("txtBilling").value;
            BillingChangeDate = document.getElementById('ucDatePickerBillDate_txtDate').value;

            var BillingChangeDateText = 'ucDatePickerBillDate_txtDate';

            chekBoxChecked = document.getElementById('<%=chkIsReleased.ClientID%>');
            if (spanlist == "") {
                var controlList = BillingChangeDateText;
            }
            else {
                var controlList = spanlist + "," + BillingChangeDateText;
            }


            
            
            if (!chekBoxChecked.checked) {

                if ((Billing != intBilling)) {

                    if (BillingChangeDate == "") {
                        if (ValidateControlOnClickEvent(controlList) == false) {

                            var lblError = document.getElementById('<%=lblError.ClientID %>');
                            lblError.innerText = "Please fill all mandatory fields.";
                            lblError.style.color = "Red";
                            return false;
                        }
                        else
                            return true;
                    }

                }
            }



            
            
            
            var nwspanlist = ""
            var nwChangeBillChangeDate = document.getElementById('ucDatePickerBillDate_txtDate').value;
            var nwBillChangeDate = 'ucDatePickerBillDate_txtDate';
            if (nwspanlist == "") {
                var nwcontrolList = nwBillChangeDate;
            }
            else {
                var nwcontrolList = spanlist + "," + nwBillChangeDate;
            }

            if (nwChangeBillChangeDate == "" && intucDatePickerBillDate != "") {

                if (ValidateControlOnClickEvent(nwcontrolList) == false) {

                    var nwlblError = document.getElementById('<%=lblError.ClientID %>');
                    nwlblError.innerText = "The Date field Can not be null.";
                    nwlblError.style.color = "Red";
                    return false;
                }
                else return true;

            }


        }

        function ValidateAll() {
            //30163-Subhra-start The below validation was made to restrict the employee release at a future date              
            var chekBoxIsReleased = document.getElementById('<%=chkIsReleased.ClientID %>');
            var errmsg = document.getElementById('<%=lblReleaseWarning.ClientID %>');
            var date = new Date();
            var relDateArr = document.getElementById("ucDatePickerReleaseDate_txtDate").value.split('/');
            var relDate = relDateArr[1] + '/' + relDateArr[0] + '/' + relDateArr[2];
            var releasedate = new Date(relDate);
            //Siddhesh : Issue 55546 : 28/07/2015 : Start
            //Description: Flag used for setting validation value.
            var valFlag = true;
            //Siddhesh : Issue 55546 : 28/07/2015 : Ends       

            //Start-Ishwar--49082
            if (document.getElementById("ucDatePickerReleaseDate_txtDate").value == "") {
                errmsg.innerHTML = "Release date required."
                valFlag = false;
            }
            //End-49082

            //Siddhesh : Issue 55546 : 28/07/2015 : Start
            //Description: Validate Billing Value before release.
            if (valFlag) {
                if ($('#' + '<%=txtUtilization.ClientID %>').val() <= 0) {
                    if ($('#' + '<%=txtBilling.ClientID %>').val() > 0) {
                        errmsg.innerHTML = "As Utilization is zero. Billing value cannot be greater than zero.";
                        valFlag = false;
                    }
                }
            }

            if (valFlag) {
                if ((chekBoxIsReleased.checked) && (releasedate > date)) {
                    errmsg.innerHTML = "Employee can't be released in future date";
                    valFlag = false;
                }
                //30163-Subhra-end  
                else {
                    errmsg.innerHTML = "";

                    // 33627 Mahendra Start
                    if (document.getElementById("ucDatePickerBillDate_txtDate").value != "") {

                        var relDateArr = document.getElementById("ucDatePickerReleaseDate_txtDate").value.split('/');
                        var relDate = relDateArr[1] + '/' + relDateArr[0] + '/' + relDateArr[2];
                        var releasedate = new Date(relDate);
                        var relBillingDateArr = document.getElementById("ucDatePickerBillDate_txtDate").value.split('/');
                        var relBillingDate = relBillingDateArr[1] + '/' + relBillingDateArr[0] + '/' + relBillingDateArr[2];
                        var billingDt = new Date(relBillingDate)

                        if (billingDt > releasedate) {
                            errmsg.innerHTML = "Billing date can't be greater than release date.";
                            valFlag = false;
                        }
                    }
                    // 33627 Mahendra end

                    if (valFlag) {
                        var aa = confirm("Please Click on Ok to save the record.");
                        var isbillingChangeValid = BillingChangeValidate();

                        if (aa) {
                            var isSaveClickValid = SaveClickValidate();

                            if ((isbillingChangeValid) == false || (isSaveClickValid) == false)
                                valFlag = false;
                        }
                        else {
                            valFlag = false;
                        }
                    }
                }
            }
            return valFlag;
            //Siddhesh : Issue 55546 : 28/07/2015 : End
        }    
    </script>

</body>
</html>
