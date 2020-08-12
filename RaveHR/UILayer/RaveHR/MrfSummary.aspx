<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="MrfSummary.aspx.cs" Inherits="MrfSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript" src="JavaScript/FilterPanel.js">
    </script>

    <script type="text/javascript">

        setbgToTab('ctl00_tabMRF', 'ctl00_spanMRFSummary');

        function $(v) {
            return document.getElementById(v);
        }

        //This function gives an alert message when "Filter" button is clicked without selecting any Filter criteria.
        function CheckFilter(isButtonClicked) {
            var ddlDepartment = $('<%= ddlDepartment.ClientID %>');
            var ddlProjectName = $('<%= ddlProjectName.ClientID%>');
            var ddlRole = $('<%= ddlRole.ClientID%>');
            var ddlStatus = $('<%= ddlStatus.ClientID%>');

            //This condition checks for the RoleCOO,RoleCEO and RoleRPM. When user is in either of the role,
            //all the dropdown's default value is set to "SELECT".
            if (ddlDepartment != null && ddlProjectName != null && ddlRole != null && ddlStatus != null) {
                if (ddlDepartment.value == "SELECT" && ddlProjectName.value == "SELECT" && ddlRole.value == "SELECT" && ddlStatus.value == "SELECT") {
                    if (isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");

                    return false;
                }
            }

            //This condition checks for the RolePM,RoleCFM,RoleFM and RolePreSales. When user is in either of the role,
            //Department dropdown value will be "Projects" for rolePM and "Finance" for roleCFM/RoleFM and "PreSales" for rolePresales respectively and
            //Department dropdown is disabled. The rest of the dropdown's default value is set to "SELECT".
            if ((ddlDepartment.value != "SELECT") && (ddlDepartment.disabled)) {
                if (ddlProjectName.value == "SELECT" && ddlRole.value == "SELECT" && ddlStatus.value == "SELECT") {
                    if (isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");

                    return false;
                }
            }

            //This condition checks for the RoleRecruitment. When user is in this role,
            //Status dropdown value will be "Pending External Allocation"  and
            //Status dropdown is disabled. The rest of the dropdown's default value is set to "SELECT".
            if ((ddlStatus.value != "SELECT") && (ddlStatus.disabled)) {
                if (ddlDepartment.value == "SELECT" && ddlProjectName.value == "SELECT" && ddlRole.value == "SELECT") {
                    if (isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");

                    return false;
                }
            }
        }

        //This function clears the filtering criteria and sets the value of the dropdown to "SELECT".
        function ClearFilter() {
            var ddlDepartment = $('<%= ddlDepartment.ClientID %>');
            var ddlProjectName = $('<%= ddlProjectName.ClientID%>');
            var ddlRole = $('<%= ddlRole.ClientID%>');
            var ddlStatus = $('<%= ddlStatus.ClientID%>');

            //If Department value is not "SELECT" than set the value to "SELECT" and disable ProjectName and
            //Role dropdown.
            if ((ddlDepartment.disabled) && (ddlProjectName.disabled)) {
                ddlRole.selectedIndex = 0;
                ddlStatus.selectedIndex = 0;
            }

            else if (ddlDepartment.disabled) {
                ddlProjectName.selectedIndex = 0;
                ddlRole.selectedIndex = 0;
                ddlStatus.selectedIndex = 0;
            }

            else if (ddlStatus.disabled) {
                ddlDepartment.selectedIndex = 0;
                ddlProjectName.selectedIndex = 0;
                ddlRole.selectedIndex = 0;
            }

            else {
                ddlDepartment.selectedIndex = 0;
                ddlProjectName.selectedIndex = 0;
                ddlRole.selectedIndex = 0;
                ddlStatus.selectedIndex = 0;

                ddlProjectName.disabled = true;
                ddlRole.disabled = true;
            }

            return false;
        }
    
    </script>

    <table width="100%">
        <tr>
        <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <span class="header">List of MRF </span>
             <%--   Sanju:Issue Id 50201:End--%>
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
                <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext" Visible="false">
                </asp:Label>
            </td>
            <td style="height: 19px; width: 280px;">
                <div id="shelf" class="filter_main" style="width: 280px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                  <%--  Sanju:Issue Id 50201:Changed cursor to pointer--%>
                        <tr style="cursor: pointer;" onclick="javascript:activate_shelf();">
                   <%--     Sanju:Issue Id 50201:End--%>
                            <td class="filter_title header_filter_title" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr= '#5C7CBD' , startColorstr= '#12379A' , gradientType= '1' );">
                                <a id="control_link" href="javascript:activate_shelf();" style="color: White; font-family: Verdana;
                                    font-size: 9pt;"><b>Filter</b></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                          <%--  Sanju:Issue Id 50201:Alignment issue Changed width--%>
                                <div id="shelf_contents" class="filter_content" style="border: solid 1px black; text-align: center;
                                    width: 274px;">
                             <%--       Sanju:Issue Id 50201:end--%>
                                    <%--<asp:UpdatePanel ID="upFilter" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>--%>
                                    <table style="text-align: left; left: 525px; top: 282px; font-family: Verdana" cellpadding="1">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upFilter" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblDepartment" runat="server" Text="Department" CssClass="txtstyle">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                  <%--  Sanju:Issue Id 50201:Alignment issue Changed width--%>
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" ToolTip="Select Department"
                                                                        Width="266px" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="mandatoryField">
                                                                    </asp:DropDownList>
                                                                      <%--       Sanju:Issue Id 50201:end--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" CssClass="txtstyle">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                 <%--  Sanju:Issue Id 50201:Alignment issue Changed width--%>
                                                                    <asp:DropDownList ID="ddlProjectName" runat="server" ToolTip="Select Project Name"
                                                                        Width="266px" CssClass="mandatoryField">
                                                                    </asp:DropDownList>
                                                                      <%--       Sanju:Issue Id 50201:end--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 2870px;">
                                                                    <asp:Label ID="lblRole" runat="server" Text="Role" CssClass="txtstyle">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                 <%--  Sanju:Issue Id 50201:Alignment issue Changed width--%>
                                                                    <asp:DropDownList ID="ddlRole" runat="server" ToolTip="Select Role" Width="266px"
                                                                        CssClass="mandatoryField">
                                                                    </asp:DropDownList>
                                                                     <%--       Sanju:Issue Id 50201:end--%> 
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 280px;">
                                                <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="txtstyle">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 280px;">
                                            <%--  Sanju:Issue Id 50201:Alignment issue Changed width--%>
                                                <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Select Status" Width="266px"
                                                    CssClass="mandatoryField">
                                                </asp:DropDownList>
                                                  <%--       Sanju:Issue Id 50201:end--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 280px;" align="center">
                                                <br />
                                                <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="button" Width="70px"
                                                    Font-Bold="True" Font-Size="9pt" OnClick="btnFilter_Click" OnClientClick="return CheckFilter(true);" />
                                                &nbsp;
                                                <asp:Button ID="btnClear" runat="server" OnClientClick="return ClearFilter()" Text="Clear"
                                                    CssClass="button" Width="69px" Font-Bold="True" Font-Size="9pt" />
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                    <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <%--Ishwar Patil 29092014 For NIS : Start--%>
        <tr runat="server" id="trRMSMRF">
            <td>
                <asp:RadioButtonList ID="RBRMSMRF" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_RBRMSMRF" Width="120px">
                    <asp:ListItem Value="1" Selected="True">Rave</asp:ListItem>
                    <asp:ListItem Value="0">NPS</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <%--Ishwar Patil 29092014 For NIS : End--%>
    </table>
    <br />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gvListOfMrf" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                    AllowSorting="true" Width="100%" OnSorting="gvListOfMrf_Sorting" OnPageIndexChanging="gvListOfMrf_PageIndexChanging"
                    OnDataBound="gvListOfMrf_DataBound" OnRowCreated="gvListOfMrf_RowCreated" OnRowDataBound="gvListOfMrf_RowDataBound"
                    DataKeyNames="MRFID" OnRowCommand="gvListOfMrf_RowCommand">
                    <HeaderStyle CssClass="headerStyle" />
                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                    <RowStyle Height="20px" CssClass="textstyle" />
                    <Columns>
                        <asp:BoundField DataField="MRFID" HeaderText="MRF ID" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" Visible="true" />
                        <asp:BoundField DataField="MrfCode" HeaderText="MRF Code" SortExpression="MrfCode"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                        <%--<asp:BoundField DataField="RpCode" HeaderText="RP Code" SortExpression="RpCode" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />--%>
                        <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="ResourceOnBoard" HeaderText="Required From" SortExpression="ResourceOnBoard"
                            DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="12%" />
                        <asp:TemplateField Visible="false" HeaderText="Expected Closure Date" SortExpression="ExpectedClosureDate">
                            <ItemTemplate>
                                <asp:Label ID="lblClosureDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ExpectedClosureDate") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DeptName"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />
                        <asp:BoundField DataField="RecruitmentManager" HeaderText="Recruitment Manager" SortExpression="RecruitmentManager"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />
                        <asp:BoundField DataField="RecruitmentAssignDate" HeaderText="Recruitment Assign Date" SortExpression="RecruitmentAssignDate"
                            DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AbortedOrClosedDate" HeaderText="" SortExpression="LastModifiedDate"
                            DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AllocationDate" HeaderText="" SortExpression="AllocationDate"
                            DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                        <asp:TemplateField Visible="false" HeaderText="Resource Name" SortExpression="EmployeeName">
                            <ItemTemplate>
                                <asp:Label ID="lblEmployeeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EmployeeName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgMove" runat="server" ImageUrl="~/Images/copy.png" Style="border: none;
                                    cursor: hand;" ToolTip="Move MRF" CommandName="MoveMrf" CommandArgument='<%# Eval("MRFID") %>' /><%-- OnClick = "btnDeleteImg_Click" --%>
                            </ItemTemplate>
                            <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%----29173-Ambar-Start-05092011--%>
                        <asp:BoundField DataField="TypeOFMRF" HeaderText="MRF Type" SortExpression="TypeOFMRF"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                        <%----29173-AMbar-End-05092011--%>                                                
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
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
            </td>
            <td align="right">
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click"
                    CssClass="button" />
                &nbsp;
                <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" Visible="false"
                    OnClick="btnRemoveFilter_Click" CssClass="button" />
            </td>
        </tr>
    </table>
</asp:Content>
