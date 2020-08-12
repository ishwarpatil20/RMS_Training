<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeSummary.aspx.cs" Inherits="EmployeeSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <style type="text/css">
        .TitleCaseText
        {
            text-transform: capitalize;
        }
    </style>

    <script src="JavaScript/CommonValidations.js" type="text/javascript"></script>

    <script src="JavaScript/FilterPanel.js" type="text/javascript"></script>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->

    <script type="text/javascript">
        //Ishwar : Modal PopUp Issue 13012015 Start
        var retVal;
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        if (e.data.indexOf(",") != -1) {
                            $.modalDialog.getCurrent().postMessage(e.data);
                        }
                        retVal = e.data;

                        if (retVal == "ReloadEmpSummaryPage") {
                            window.open('EmployeeSummary.aspx', '_self', '');
                        }
                    }
                });
            });
        })(jQuery);

        function OpenUpdateManagerPopUp() {
            //alert(parameters);
            jQuery.modalDialog.create({ url: "ReportingOrFunctionalManager.aspx?page=employeesummary", maxWidth: 1200 }).open();
        }

        function OpenCostCodeManagerPopUp() {
            //alert(parameters);
            jQuery.modalDialog.create({ url: "CostCodeUpdateManager.aspx?page=employeesummary", maxWidth: 750 }).open();
        }
        
        function popUpEmployeeReleasePopulate(queryuString) {
            jQuery.modalDialog.create({ url: 'EmpSetReleaseDate.aspx?' + queryuString, maxWidth: 1000 }).open();
        }
        //Ishwar : Modal PopUp Issue 13012015 End
        setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmpSummary');

        function $(v) {
            return document.getElementById(v);
        }

        //This function clears the filtering criteria and sets the value of the dropdown to "SELECT".
        function ClearFilter() {
            //var txtEmpName = $('<%= txtEployeeName.ClientID %>');
            var ddlDepartment = $('<%= ddlDepartment.ClientID %>');
            var ddlProjectName = $('<%= ddlProject.ClientID%>');
            var ddlRole = $('<%= ddlRole.ClientID%>');
            var ddlStatus = $('<%= ddlStatus.ClientID%>');

            //txtEmpName.Value = '';
            $('<%= txtEployeeName.ClientID %>').value = "";
            ddlDepartment.selectedIndex = 0;
            ddlProjectName.selectedIndex = 0;
            ddlRole.selectedIndex = 0;
            ddlStatus.selectedIndex = 0;
            return false;
        }

        function DenySpecialChar() {
            if (event.keyCode == 13) {
                var txtEployeeName = document.getElementById('<%=txtEployeeName.ClientID %>');
                if (txtEployeeName.value == "") {
                    CheckFilter(false);
                }
                else {
                    var btnFilter = document.getElementById('<%=btnFilter.ClientID %>');
                    btnFilter.click();
                }
            }
            if ((event.keyCode > 32 && event.keyCode < 45) || (event.keyCode > 46 && event.keyCode < 48) || (event.keyCode > 57 && event.keyCode < 65) || (event.keyCode > 90 && event.keyCode < 97) || (event.keyCode > 122 && event.keyCode < 127))
                event.returnValue = false;

        }

        //Calling release from emp summary page.


        //Check in filter any value is selected or not?
        function CheckFilter(isButtonClicked) {
            var txtEmployeeName = document.getElementById('ctl00_cphMainContent_txtEployeeName');
            var vartxtEmployeeName = txtEmployeeName.value;

            var ddlDepartment = document.getElementById('ctl00_cphMainContent_ddlDepartment');
            var varddlDepartment = ddlDepartment.value;

            var ddlRole = document.getElementById('ctl00_cphMainContent_ddlRole');
            var varddlRole = ddlRole.value;

            var ddlProject = document.getElementById('ctl00_cphMainContent_ddlProject');
            var varddlProject = ddlProject.value;

            var ddlStatus = document.getElementById('ctl00_cphMainContent_ddlStatus');
            var varddlStatus = ddlStatus.value;

            if (vartxtEmployeeName == "" && varddlDepartment == "SELECT" && varddlRole == "SELECT" && varddlProject == "SELECT" && varddlStatus == "SELECT") {
                if (isButtonClicked)
                    alert("Please select or enter any criteria, to proceed with filter.");

                return false;
            }
        }

        //Umesh: Issue 'Modal Popup issue in chrome' Starts
//        (function($) {
//            $(document).ready(function() {
//                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
//                //Issue Id : 28572 Mahendra START
//                $("#<%=btnRMFMHTML.ClientID%>").on("click", function(e) {
//                    $.modalDialog.create({ url: "ReportingOrFunctionalManager.aspx?page=employeesummary", maxWidth: 1200 }).open();
//                });

//                $(window).on("message", function(e) {
//                    if (e.data != undefined) {
//                        if (e.data.indexOf(",") != -1) {
//                            $.modalDialog.getCurrent().postMessage(e.data);
//                        }
//                    }
//                });
//            });
//            //Issue Id : 28572 Mahendra END

//        })(jQuery);
        //Umesh: Issue 'Modal Popup issue in chrome' Ends
    </script>

    <div style="font-family: Verdana; font-size: 9pt;">
        <table width="100%">
            <tr>
                <%--Sanju:Issue Id 50201: Added new class header_employee_profile so that the header color is same for all browsers--%>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                    <span class="header">Employee Summary</span>
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
                    <table>
                        <tr>
                            <td>
                                <%-- Sanju:Issue Id 50201:Changed width resolved alignment issue--%>
                                <asp:Button runat="server" ID="btnEmployeeDetails" Text="Export to Excel" CssClass="button"
                                    Width="110" OnClick="btnEmployeeDetails_Click" UseSubmitBehavior="false" />
                                <%--     Sanju:Issue Id 50201:End--%>
                                <%--Issue Id : 28572 Mahendra START --%>
                                <input id="btnRMFMHTML" type="button" value="Update Manager" runat="server" class="button" />
                                <%--Issue Id : 28572 Mahendra END--%>
                                
                                <!-- Siddharth 9th April 2015 Start -->
                                <input id="BtnCostCodeManager" type="button" value="Update CostCode" runat="server" class="button" />
                                <!-- Siddharth 9th April 2015 End -->
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                <%--Ishwar Patil 30092014 For NIS : Start--%>
                                <asp:RadioButtonList ID="RBRMSEmp" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                    OnSelectedIndexChanged="OnSelectedIndexChanged_RBRMSEmp" Width="120px">
                                    <asp:ListItem Value="1" Selected="True">Rave</asp:ListItem>
                                    <asp:ListItem Value="0">NPS</asp:ListItem>
                                </asp:RadioButtonList>
                                <%--Ishwar Patil 30092014 For NIS : End--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="height: 19px; width: 280px;">
                    <div id="shelf" class="filter_main" style="width: 280px;">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <%--Sanju:Issue Id 50201: Changed cursor to pointer so that all browser support cursor property--%>
                            <tr style="cursor: pointer;" onclick="javascript:activate_shelf();">
                                <%--Sanju:Issue Id 50201: End--%>
                                <td class="filter_title header_filter_title" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr= '#5C7CBD' , startColorstr= '#12379A' , gradientType= '1' );">
                                    <a id="control_link" href="javascript:activate_shelf();" style="color: White;"><b>Filter</b></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="shelf_contents" class="filter_content" style="border: solid 1px black; text-align: center;
                                        width: 274px;">
                                        <table style="text-align: left; left: 541px; top: 282px;" cellpadding="1">
                                            <tr>
                                                <td style="width: 280px;">
                                                    <asp:Label ID="lblName" runat="server" Text="Employee Name" Font-Size="9pt"></asp:Label>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 280px">
                                                    <asp:TextBox ID="txtEployeeName" runat="server" ToolTip="Enter Employee Name" Width="260px"
                                                        MaxLength="30" onkeypress="return DenySpecialChar();"></asp:TextBox>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 280px;">
                                                    <asp:Label ID="lblDepartment" runat="server" Text="Department" Font-Size="9pt"></asp:Label>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 280px">
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="266px" ToolTip="Select Client Name"
                                                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 280px;">
                                                    <asp:Label ID="lblRole" runat="server" Text="Designation" Font-Size="9pt"></asp:Label>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 280px">
                                                    <asp:DropDownList ID="ddlRole" runat="server" Width="266px" ToolTip="Select Role">
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 280px;">
                                                    <asp:Label ID="lblProject" runat="server" Text="Project" Font-Size="9pt"></asp:Label>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 280px">
                                                    <asp:DropDownList ID="ddlProject" runat="server" Width="266px" ToolTip="Select Project Name">
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upFilter" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <table>
                                                                <%--   <tr>
                                                                    <td style="width: 280px;">
                                                                        <asp:Label ID="lblName" runat="server" Text="Employee Name" Font-Size="9pt"></asp:Label>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 280px">
                                                                        <asp:TextBox ID="txtEployeeName" runat="server" ToolTip="Enter Eployee Name" Width="260px"
                                                                            MaxLength="30" onkeypress="return DenySpecialChar();"></asp:TextBox>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 280px;">
                                                                        <asp:Label ID="lblDepartment" runat="server" Text="Department" Font-Size="9pt"></asp:Label>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 280px">
                                                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="280px" ToolTip="Select Client Name"
                                                                            OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 280px;">
                                                                        <asp:Label ID="lblRole" runat="server" Text="Designation" Font-Size="9pt"></asp:Label>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 280px">
                                                                        <asp:DropDownList ID="ddlRole" runat="server" Width="280px" ToolTip="Select Role">
                                                                        </asp:DropDownList>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 280px;">
                                                                        <asp:Label ID="lblProject" runat="server" Text="Project" Font-Size="9pt"></asp:Label>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 280px">
                                                                        <asp:DropDownList ID="ddlProject" runat="server" Width="280px" ToolTip="Select Project Name">
                                                                        </asp:DropDownList>
                                                                        <br />
                                                                    </td>
                                                                </tr>--%>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr id="trlblStatus" runat="server" visible="true">
                                                <td style="width: 280px;">
                                                    <asp:Label ID="lblStatus" runat="server" Text="Employee Status" Font-Size="9pt"></asp:Label>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="trddlStatus" runat="server" visible="true">
                                                <td style="width: 280px">
                                                    <%--Sanju:Issue Id 50201:Alignment issue resolved :Changed width--%>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Width="266px"
                                                        ToolTip="Select Employee Status">
                                                    </asp:DropDownList>
                                                    <%--   Sanju:Issue Id 50201:End--%>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 280px;">
                                                    <br />
                                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClientClick="return CheckFilter(true);"
                                                        CssClass="button" Width="70px" Font-Bold="True" Font-Size="9pt" OnClick="btnFilter_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="btnClear" OnClientClick="return ClearFilter()" runat="server" Text="Clear"
                                                        CssClass="button" Width="69px" Font-Bold="True" Font-Size="9pt" />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:UpdatePanel runat="server" ID="UP_EmployeeSummary">
                        <ContentTemplate>
                            <asp:GridView ID="grdvListofEmployees" runat="server" AutoGenerateColumns="False"
                                AllowPaging="True" AllowSorting="True" DataKeyNames="EmpProjectAllocationId"
                                Width="100%" OnSorting="grdvListofEmployees_Sorting" OnRowCreated="grdvListofEmployees_RowCreated"
                                OnPageIndexChanging="grdvListofEmployees_PageIndexChanging" OnDataBound="grdvListofEmployees_DataBound"
                                OnRowDataBound="grdvListofEmployees_RowDataBound">
                                <HeaderStyle CssClass="headerStyle" />
                                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                <RowStyle Height="20px" CssClass="textstyle" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnExpandCollaspeChildGrid" runat="server" CommandArgument='<%#Eval("EMPId") %>'
                                                CommandName="ChildGridProjectsForEmployee" Height="13px" ImageUrl="Images/plus.JPG"
                                                ToolTip="Expand" Width="16px" />
                                            <asp:HiddenField ID="hfProjectCount" runat="server" Value='<%# Bind("ProjectCount") %>' />
                                            <asp:HiddenField ID="hfClientCount" runat="server" Value='<%# Bind("ClientCount") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle Width="3%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EMPCode" HeaderText="Employee Code" SortExpression="EMPCode">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="9%" HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="FirstName">
                                        <ItemStyle CssClass="TitleCaseText" />
                                        <HeaderStyle Width="17%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation">
                                        <HeaderStyle Width="20%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JoiningDate" HeaderText="Company Joining Date" SortExpression="JoiningDate"
                                        DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="11%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="DeptName">
                                        <HeaderStyle Width="11%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CostCode" HeaderText="CostCode" SortExpression="CostCode">
                                        <HeaderStyle Width="12%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Resignation Date" SortExpression="ResignationDate">
                                        <HeaderStyle Width="11%" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblFirstName" Text='<%# Convert.ToString(Eval("ResignationDate","{0:dd/MM/yyyy}")) != "01/01/0001" ? Convert.ToString(Eval("ResignationDate","{0:dd/MM/yyyy}")) : "-" %>'></asp:Label>
                                            <tr id="tr_EmpGrid" style="display: none;" runat="server">
                                                <td colspan="8" width="100%">
                                                    <asp:GridView ID="grdvDetailedListofEmployees" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" EmptyDataText="No Records Found" OnRowDataBound="grdvDetailedListofEmployees_onRowDataBound">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="" SortExpression="" Visible="true">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle Width="4%" HorizontalAlign="center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EmpProjectAllocationId" HeaderText="Employee PAID" SortExpression="EMPCode"
                                                                Visible="false">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle Width="11%" HorizontalAlign="center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName">
                                                                <HeaderStyle Width="12%" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName">
                                                                <HeaderStyle Width="23%" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectStartDate" HeaderText="Proj. Joining Date" SortExpression="StartDate"
                                                                DataFormatString="{0:dd/MM/yyyy}">
                                                                <HeaderStyle Width="12%" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ProjectReleaseDate" HeaderText="Exp.Release Date" SortExpression="EndDate"
                                                                DataFormatString="{0:dd/MM/yyyy}">
                                                                <HeaderStyle Width="12%" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                          
                                                             <asp:BoundField DataField="CostCode" HeaderText="Cost Code" SortExpression="CostCode">
                                                                <HeaderStyle Width="12%" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                           
                                                            <asp:TemplateField HeaderText="Utilization" SortExpression="Utilization">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUtilization" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Utilization") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="7%" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Billing" SortExpression="Billing">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBilling" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Billing") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="7%" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ReportingTo" HeaderText="Accountable To" SortExpression="ReportingTo">
                                                                <HeaderStyle Width="18%" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="imgRelease" ImageUrl="~/Images/reject.jpg" ToolTip="Release"
                                                                        Width="20px" Height="20px" CommandName="ReleaseImageBtn" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"EMPId") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="text" />
                                                        <HeaderStyle CssClass="childgridheader" />
                                                        <RowStyle CssClass="childgridRow" />
                                                        <AlternatingRowStyle CssClass="childgridAlternatingRow" />
                                                        <SelectedRowStyle BackColor="#0099CC" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <table id="tblPaging" runat="server" class="tablePager" width="100%">
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
                                                <span>Page Size: </span>
                                               <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true">
                                               </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </PagerTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:HiddenField ID="hfEmpid" runat="server" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" Visible="false"
                        CssClass="button" OnClick="btnRemoveFilter_Click" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
