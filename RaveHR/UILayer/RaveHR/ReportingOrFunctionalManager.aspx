<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ReportingOrFunctionalManager.aspx.cs"
    Inherits="ReportingOrFunctionalManager" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Functional Manager/Reporting Manager List</title>
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />
    <script src="JavaScript/CommonValidations.js" type="text/javascript"></script>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <link href="CSS/jquery.modalDialogContent.css" rel="stylesheet" type="text/css" />
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinnyContent.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialogContent.js" type="text/javascript"></script>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
</head>
<body>
    <form id="form1" runat="server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <cc1:ToolkitScriptManager ID="ScriptManagerMaster" runat="server">
    </cc1:ToolkitScriptManager>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    <div>
        <table width="100%">
            <tr>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <span class="header">Update Functional Manager/Line Manager</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" CssClass="text" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
        <asp:UpdatePanel ID="updesignation" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblSelectEmployee" runat="server" Text="Select Manager" CssClass="txtstyle"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="ddlEmployeeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <span id="dataView" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblPMName" runat="server" Text="Line Manager" CssClass="txtstyle"></asp:Label>
                                <asp:TextBox ID="txtRMName" runat="server" ToolTip="Enter PM Name" ReadOnly="true"
                                    TextMode="MultiLine"></asp:TextBox>
                                <asp:HiddenField ID="HfReportingToNameSelectAll" runat="server" />
                                <%--<img id="imgPM" runat="server" src="Images/find.png" alt="" />--%>
                                <asp:Image runat="server" ID="imgPMSelectAll" ImageUrl="Images/find.png" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblFMName" runat="server" Text="Functional Manager" CssClass="txtstyle"></asp:Label>
                                <asp:TextBox ID="txtFMName" runat="server" ToolTip="Enter FM Name" ReadOnly="true"
                                    TextMode="MultiLine"></asp:TextBox>
                                <asp:HiddenField ID="HfFunctionalToNameSelectAll" runat="server" />
                                <%--<img id="imgFM" runat="server" src="Images/find.png" alt="" />--%>
                                <asp:Image runat="server" ID="imgFMSelectAll" ImageUrl="Images/find.png" />
                            </td>
                        </tr>
                    </table>
                    <%--Siddhesh Arekar : Added scrollbars to gridview to if Gridview Expands : 17/06/2015 : Starts--%>
                    <div style='overflow:scroll; width:1150px; height:400px;'>
                    <%--Siddhesh Arekar : Added scrollbars to gridview to if Gridview Expands : 17/06/2015 : Ends--%>
                    <asp:GridView ID="grdvListofReportingEmployees" runat="server" AutoGenerateColumns="False"
                        OnSorting="grdvListofReportingEmployees_Sorting" Width="500" AllowSorting="True"
                        DataKeyNames="EMPId" OnRowDataBound="grdvListofReportingEmployees_RowDataBound"
                        OnRowCreated="grdvListofReportingEmployees_RowCreated" ShowFooter="True" EmptyDataText="No Record Found."
                        EmptyDataRowStyle-CssClass="Messagetext">
                        <HeaderStyle CssClass="headerStyle" />
                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                        <RowStyle Height="20px" CssClass="textstyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select All">
                                <HeaderStyle HorizontalAlign="center" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Text="Select All" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%-- <span id="spanChkSelect" runat="server">--%>
                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="Check_Click(this)" />
                                    <%--AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged"--%>
                                    <%--</span>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpCode" HeaderText="Employee Code">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                <%--36732-Ambar-Change Alignment from Center to Left--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                <%--36732-Ambar-Change Alignment from Center to Left--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                            </asp:BoundField>
                            <%--36732-Ambar-Start--%>
                            <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                            </asp:BoundField>
                            <%--36732-Ambar-End--%>
                            <asp:TemplateField HeaderText="Line Manager">
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRMToName" runat="server" AutoPostBack="false" ReadOnly="true"
                                        Text='<%#DataBinder.Eval(Container.DataItem,"RepotingName") %>' Enabled='<%# (Convert.ToBoolean(Eval("RepotingToPresent")))  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:Image ID="imgReportingToPopulate" runat="server" ImageUrl="Images/find.png"
                                        Visible='<%# (Convert.ToBoolean(Eval("RepotingToPresent")))  %>' />
                                    <%--alt="" onclick="return popUpEmployeeSearchRMIndv(7);"--%>
                                    <asp:HiddenField ID="HfProjectId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ProjectId") %>' />
                                    <asp:HiddenField ID="HfEmpId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"EMPId") %>' />
                                    <asp:HiddenField ID="HfReportingToName" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ReportingToId") %>' />
                                    <asp:HiddenField ID="HfReportingToCommonName" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"CommonReportingToId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Functional Manager">
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFMToName" runat="server" AutoPostBack="false" ReadOnly="true"
                                        Text='<%#DataBinder.Eval(Container.DataItem,"FunctionalName") %>' Enabled='<%# (Convert.ToBoolean(Eval("FunctionalToPresent")))  %>'
                                        Visible='<%# (Convert.ToBoolean(Eval("FunctionalVisibility")))  %>' />
                                    <asp:HiddenField ID="HfFunctionalToName" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"FunctionalManagerId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:Image ID="imgFunctionalToPopulate" runat="server" ImageUrl="Images/find.png"
                                        Visible='<%# (Convert.ToBoolean(Eval("FunctionalToPresent")))  %>' /><%-- alt="" onclick="return popUpEmployeeSearchFMIndv(4);"--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>                     
                    <%--Siddhesh Arekar : Added scrollbars to gridview to if Gridview Expands : 17/06/2015 : Starts--%> 
                    </div>
                    <%--Siddhesh Arekar : Added scrollbars to gridview to if Gridview Expands : 17/06/2015 : Ends--%>
                    <table border="0">
                        <tr>
                            <td style="width: 60%">
                            </td>
                            <td style="width: 15%" align="right">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click"
                                    OnClientClick="return CheckIfRowChecked()" />
                            </td>
                            <td style="width: 15%" align="center">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click" />
                            </td>
                            <td style="width: 10%" align="left">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" OnClientClick="CloseDialog();" />
                            </td>
                        </tr>
                    </table>
                </span>
        <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnClose" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    </div>
    </form>

    <script type="text/javascript">
        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        var retVal;
        (function($) {
            $(document).ready(function() {
                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        if (e.data.indexOf(",") != -1) {
                            retVal = e.data;
                        }
                    }
                });
            });
        })(jQuery);

        function CloseDialog() {
            $.modalDialog.getCurrent().close();
        };
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        function CheckIfRowChecked() {
            var valid = false;
            // var gv = document.getElementById("<%=grdvListofReportingEmployees.ClientID%>");
            var gv = document.getElementById("<%=grdvListofReportingEmployees.ClientID%>").getElementsByTagName("input"); //get only input elements
            if (gv != null) {
                // for (var i = 0; i < gv.all.length; i++) {     //all method of javascript does not support latest browser.
                //  var node = gv.all[i];
                for (var i = 0; i < gv.length; i++) {
                    var node = gv[i];
                    if (node != null && node.type == "checkbox" && node.checked) {
                        valid = true;
                        break;
                    }
                }
                if (!valid) {
                    alert("Please select at least one record.");
                    // if (gv.all.length > 0 && gv.outerText != "No Record Found.") {   //all method of javascript does not support latest browser
                    if (gv.length > 0 && gv.outerText != "No Record Found.") {
                        document.getElementById('<%=txtFMName.ClientID%>').value = "";
                        document.getElementById('<%=HfFunctionalToNameSelectAll.ClientID%>').value = "";

                        document.getElementById('<%=HfReportingToNameSelectAll.ClientID%>').value = "";
                        document.getElementById('<%=txtRMName.ClientID%>').value = "";
                    }
                    return valid;
                }
            } else {
                alert("Please select at least one record.");
                return valid;
            }
        };

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        };

        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        };

        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        function popUpFunctionalManagerSearch() {
            var valReturned;
            var EmpId;
            var iFlag = CheckIfRowChecked();
            if (iFlag != false) {
                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                    onclose: function(e) {
                        valReturned = retVal;
                        var EmpName;
                        var employee = new Array();
                        if (valReturned != undefined)
                            employee = valReturned.split(",");

                        if (employee != null && (employee.length - 1) > 1) {
                            alert("Please select only one Functional Manager.")
                            return;
                        }


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
                        if (EmpName == undefined) {
                            document.getElementById('<%=txtFMName.ClientID%>').value = "";
                        }
                        else {
                            document.getElementById('<%=txtFMName.ClientID%>').value = EmpName;
                            document.getElementById('<%=HfFunctionalToNameSelectAll.ClientID%>').value = EmpId;

                        }

                        var gvDrv = document.getElementById("<%= grdvListofReportingEmployees.ClientID %>");

                        for (iRow = 1; iRow <= gvDrv.rows.length - 1; iRow++) {
                            if (iRow != gvDrv.rows.length - 1) {
                                var cltRow = iRow + 1;
                                var empIdEmp;
                                if (cltRow <= 9) {
                                    //if row is checked and emp name is not null then 
                                    if (document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_txtFMToName") != null && document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_txtFMToName").disabled == false && document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_txtFMToName") != null && EmpName != null && EmpName != "" && document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_chkSelect").checked) {

                                        //new code added - if more than two row for same employee then reflect functional manager name for both row regardless checkbox selected or not.
                                        empIdEmp = document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_HfEmpId").value;

                                        //find out selected employee from all grid and replace functional manager
                                        for (iRowIn = 1; iRowIn <= gvDrv.rows.length - 1; iRowIn++) {

                                            if (iRowIn != gvDrv.rows.length - 1) {

                                                var cltRowIn = iRowIn + 1;

                                                if (cltRowIn <= 9) {
                                                    if (empIdEmp == document.getElementById("grdvListofReportingEmployees_ctl0" + cltRowIn + "_HfEmpId").value) {

                                                        document.getElementById("grdvListofReportingEmployees_ctl0" + cltRowIn + "_txtFMToName").value = EmpName;
                                                        document.getElementById("grdvListofReportingEmployees_ctl0" + cltRowIn + "_HfFunctionalToName").value = EmpId;
                                                    }
                                                }
                                                else {
                                                    if (empIdEmp == document.getElementById("grdvListofReportingEmployees_ctl" + cltRowIn + "_HfEmpId").value) {

                                                        document.getElementById("grdvListofReportingEmployees_ctl" + cltRowIn + "_txtFMToName").value = EmpName;
                                                        document.getElementById("grdvListofReportingEmployees_ctl" + cltRowIn + "_HfFunctionalToName").value = EmpId;
                                                    }
                                                }
                                            }
                                        }
                                        //end
                                        //document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_txtFMToName").value = EmpName;
                                        //document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_HfFunctionalToName").value = EmpId;
                                    }
                                }
                                else {
                                    if (document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_txtFMToName") != null && document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_txtFMToName").disabled == false && document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_txtFMToName") != null && EmpName != null && EmpName != "" && document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_chkSelect").checked) {
                                        empIdEmp = document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_HfEmpId").value;

                                        for (iRowInside = 1; iRowInside <= gvDrv.rows.length - 1; iRowInside++) {

                                            if (iRowInside != gvDrv.rows.length - 1) {

                                                var cltRowInside = iRowInside + 1;


                                                if (cltRowInside <= 9) {
                                                    if (empIdEmp == document.getElementById("grdvListofReportingEmployees_ctl0" + cltRowInside + "_HfEmpId").value) {
                                                        document.getElementById("grdvListofReportingEmployees_ctl0" + cltRowInside + "_txtFMToName").value = EmpName;
                                                        document.getElementById("grdvListofReportingEmployees_ctl0" + cltRowInside + "_HfFunctionalToName").value = EmpId;
                                                    }
                                                }
                                                else {
                                                    if (empIdEmp == document.getElementById("grdvListofReportingEmployees_ctl" + cltRowInside + "_HfEmpId").value) {
                                                        document.getElementById("grdvListofReportingEmployees_ctl" + cltRowInside + "_txtFMToName").value = EmpName;
                                                        document.getElementById("grdvListofReportingEmployees_ctl" + cltRowInside + "_HfFunctionalToName").value = EmpId;
                                                    }
                                                }
                                            }
                                        }
                                        //document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_txtFMToName").value = EmpName;
                                        //document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_HfFunctionalToName").value = EmpId;
                                    }
                                }
                            }
                        }
                    }
                }).open();
            }
        };

        function popUpEmployeeSearch() {
            var valReturned;
            var iFlag = CheckIfRowChecked();
            if (iFlag != false) {
                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                    onclose: function(e) {
                        valReturned = retVal;
                        var EmpName;
                        var EmpId;
                        var employee = new Array();
                        if (valReturned != undefined)
                            employee = valReturned.split(",");


                        if (employee != null && (employee.length - 1) > 1) {
                            alert("Please select only one Line Manager.")
                            return;
                        }

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
                        if (EmpName == undefined) {
                            document.getElementById('<%=txtRMName.ClientID%>').value = "";
                        }
                        else {
                            document.getElementById('<%=HfReportingToNameSelectAll.ClientID%>').value = EmpId;
                            document.getElementById('<%=txtRMName.ClientID%>').value = EmpName;
                        }

                        var gvDrv = document.getElementById("<%= grdvListofReportingEmployees.ClientID %>");

                        for (iRow = 1; iRow <= gvDrv.rows.length - 1; iRow++) {

                            if (iRow != gvDrv.rows.length - 1) {

                                var cltRow = iRow + 1;



                                if (cltRow <= 9) {

                                    if (document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_txtRMToName").disabled == false && EmpName != null && EmpName != "" && document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_chkSelect").checked) {
                                        document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_txtRMToName").value = EmpName;
                                        document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_HfReportingToName").value = EmpId;
                                    }
                                }
                                else {
                                    if (document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_txtRMToName").disabled == false && EmpName != null && EmpName != "" && document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_chkSelect").checked) {
                                        document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_txtRMToName").value = EmpName;
                                        document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_HfReportingToName").value = EmpId;
                                    }
                                }
                            }
                        }
                    }
                }).open();
            }
        };

        function popUpEmployeeSearchFMIndv(strFM) {
            var valReturned;
            var flag = false;
            var row = parseInt(strFM) + 2;

            if (row <= 9) {
                flag = document.getElementById("grdvListofReportingEmployees_ctl0" + row + "_chkSelect").checked;
            }
            else
                flag = document.getElementById("grdvListofReportingEmployees_ctl" + row + "_chkSelect").checked;

            if (flag) {
                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                    onclose: function(e) {
                        valReturned = retVal;
                        var EmpName;
                        var EmpId;
                        var employee = new Array();
                        if (valReturned != undefined)
                            employee = valReturned.split(",");

                        if (employee != null && (employee.length - 1) > 1) {
                            alert("Please select only one Functional Manager.")
                            return;
                        }

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
                        if (EmpName == undefined) {
                            //document.getElementById(strFM).value = "";
                        }
                        else {
                            //document.getElementById("grdvListofReportingEmployees_ctl03_txtRMToName").value = EmpName;
                            if (row <= 9) {
                                if (EmpName != null && EmpName != "") {
                                    document.getElementById("grdvListofReportingEmployees_ctl0" + row + "_txtFMToName").value = EmpName;
                                    document.getElementById("grdvListofReportingEmployees_ctl0" + row + "_HfFunctionalToName").value = EmpId;
                                }
                            }
                            else {
                                if (EmpName != null && EmpName != "") {
                                    document.getElementById("grdvListofReportingEmployees_ctl" + row + "_txtFMToName").value = EmpName;
                                    document.getElementById("grdvListofReportingEmployees_ctl" + row + "_HfFunctionalToName").value = EmpId;
                                }
                            }
                        }

                        //Sanju:Issue Id 50201
                        //Added condition for EmpName not equal to undefined since other browsers were taking undefined value if we wont select any employee.
                        if (EmpName != undefined) {
                            //new code added
                            // find emp Id which is selected
                            var empIdEmp;

                            var gvDrv = document.getElementById("<%= grdvListofReportingEmployees.ClientID %>");
                            if (row <= 9) {
                                empIdEmp = document.getElementById("grdvListofReportingEmployees_ctl0" + row + "_HfEmpId").value;
                            } else {
                                empIdEmp = document.getElementById("grdvListofReportingEmployees_ctl" + row + "_HfEmpId").value;
                            }

                            // find same employee name in all grid view and replace functional manager name
                            for (iRow = 1; iRow <= gvDrv.rows.length - 1; iRow++) {

                                if (iRow != gvDrv.rows.length - 1) {

                                    var cltRow = iRow + 1;


                                    if (cltRow <= 9) {
                                        if (empIdEmp == document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_HfEmpId").value) {

                                            //alert(document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_HfEmpId").value);

                                            document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_txtFMToName").value = EmpName;
                                            document.getElementById("grdvListofReportingEmployees_ctl0" + cltRow + "_HfFunctionalToName").value = EmpId;
                                        }
                                    }
                                    else {
                                        if (empIdEmp == document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_HfEmpId").value) {

                                            //alert(document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_HfEmpId").value);

                                            document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_txtFMToName").value = EmpName;
                                            document.getElementById("grdvListofReportingEmployees_ctl" + cltRow + "_HfFunctionalToName").value = EmpId;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }).open();
            }
            else {
                alert("Please select checkbox first");
            }
        };

        function popUpEmployeeSearchRMIndv(strRM) {

            var valReturned;
            //var retVal;
            var flag = false;
            var row = parseInt(strRM) + 2;


            if (row <= 9) {
                flag = document.getElementById("grdvListofReportingEmployees_ctl0" + row + "_chkSelect").checked;
            }
            else
                flag = document.getElementById("grdvListofReportingEmployees_ctl" + row + "_chkSelect").checked;

            if (flag) {

                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                    onclose: function(e) {
                        valReturned = retVal;
                        var EmpName;
                        var EmpId;
                        var employee = new Array();
                        if (valReturned != undefined)
                            employee = valReturned.split(",");
                        //Sanju:Issue Id 50201:Added condition for employee equal to blank
                        if (employee != null && (employee.length - 1) > 1 && employee != "") {
                            alert("Please select only one Line Manager.")
                            return;
                        }

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
                        if (EmpName == undefined) {
                            //document.getElementById(strRM).value = "";
                        }
                        else {
                            if (row <= 9) {
                                if (EmpName != null && EmpName != "") {
                                    document.getElementById("grdvListofReportingEmployees_ctl0" + row + "_txtRMToName").value = EmpName;
                                    document.getElementById("grdvListofReportingEmployees_ctl0" + row + "_HfReportingToName").value = EmpId;
                                }
                            }
                            else {
                                if (EmpName != null && EmpName != "") {
                                    document.getElementById("grdvListofReportingEmployees_ctl" + row + "_txtRMToName").value = EmpName;
                                    document.getElementById("grdvListofReportingEmployees_ctl" + row + "_HfReportingToName").value = EmpId;
                                }
                            }
                        }
                    }
                }).open();
            }
            else {
                alert("Please select checkbox first");
            }
        };
        //Umesh: Issue 'Modal Popup issue in chrome' Ends
    </script>

</body>
</html>
