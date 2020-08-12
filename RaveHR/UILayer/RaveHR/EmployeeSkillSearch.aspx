<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeSkillSearch.aspx.cs" Inherits="EmployeeSkillSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script src="JavaScript/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="JavaScript/ScrollableGrid.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            jQuery('#<%=grdvListofEmployees.ClientID %>').Scrollable();
        }
    )
        //Poonam : Issue : 54921 : Starts
        //Desc: Validation for Skills Dropdown	
        function ValidateSearchBtn() {
            var gridview = document.getElementById("ctl00_cphMainContent_gvSkillCriteria_ctl02_ddlSkill");
            var dropdown = gridview.options[gridview.selectedIndex].value;
            var dval = gridview.selectedIndex;
            
            var lblMessage = document.getElementById('ctl00_cphMainContent_lblError');
            lblMessage.innerHTML = "";
            
            if (dropdown == "SELECT" || dval == 0) {
                    //lblMessage.style.color = "RED";
                    lblMessage.innerHTML = "Please select Skill.";
                    return false;
            }

        }
        //Poonam : Issue : 54921 : Ends
    </script>
    
    <%--<div class="detailsborder">--%>
        <table class="detailsbg" width="100%">
            <tr>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                   <span class="header">Employee Skill Search</span>
               </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style = "width : 860px; height : 10px;">
                    <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="border: solid 1px black">
            <asp:GridView ID="gvSkillCriteria" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSkillCriteria_RowDataBound"
                OnRowCommand="gvSkillCriteria_RowCommand" GridLines="None" Width="100%">
                <HeaderStyle CssClass="headerStyle" />
                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                <RowStyle Height="20px" CssClass="textstyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Skill" ItemStyle-Width="160px" HeaderStyle-Width="160px">
                        <ItemTemplate>
                            <asp:HiddenField ID="HFSkillNo" runat="server" Value='<%#Eval("SkillNo") %>' />    
                            <asp:HiddenField ID="HFSkill" runat="server" Value='<%#Eval("SkillName") %>' />
                            <asp:DropDownList ID="ddlSkill" runat="server" ToolTip="Select skill" Width="230px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="200px" HeaderStyle-Width="200px">
                        <ItemTemplate>
                            <asp:HiddenField ID="HFMandatoryOptional" runat="server" Value='<%#Eval("SearchMode") %>' />
                            <asp:RadioButtonList ID="rblMandatoryOptional" runat="server" RepeatDirection="Horizontal"
                                Width="200px">
                                <asp:ListItem Value="0" Selected="True">Mandatory</asp:ListItem>
                                <asp:ListItem Value="1">Optional</asp:ListItem>
                            </asp:RadioButtonList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                    <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                CommandName="DeleteSkill" CommandArgument='<%#Eval("SkillNo")%>' ToolTip="Delete Skill" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <table style="width: 100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnAddRow" runat="server" Text="Add New Row" OnClick="btnAddRow_Click"
                        CssClass="button" />
                    &nbsp;<asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="button" OnClick="BtnSearch_Click"/>
                    &nbsp;<asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="button" OnClick="BtnClear_OnClick"/>
                    &nbsp;<asp:Button runat="server" ID="btnExport" Text="Export to Excel" CssClass="button"
                        Visible="false" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <%--<div style="max-height: 450px; width: 99%; overflow: auto;">--%>
                        <asp:GridView ID="grdvListofEmployees" runat="server" AutoGenerateColumns="False" Width="100%" 
                            AllowPaging="false" AllowSorting="true" OnDataBound="grdvListofEmployees_DataBound" OnSorting="grdvListofEmployees_Sorting" OnRowCreated="grdvListofEmployees_RowCreated">
                            <HeaderStyle CssClass="headerStyleFreeze" />
                            <AlternatingRowStyle CssClass="alternatingrowStyleFreeze" />
                            <RowStyle Height="20px" CssClass="textstyleFreeze" />
                            <Columns>
                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-VerticalAlign="Top" SortExpression="EmployeeName">
                                <ItemStyle Width="150px" />
                                <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation" ItemStyle-VerticalAlign="Top" SortExpression="Designation">
                                 <ItemStyle Width="10%" />
                                <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Department" HeaderText="Department" ItemStyle-VerticalAlign="Top" SortExpression="Department">
                                <ItemStyle Width="10%" />
                                <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ProjectsAllocated" HeaderText="Project Name" ItemStyle-VerticalAlign="Top" SortExpression="ProjectsAllocated">
                                <ItemStyle Width="10%" />
                                <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PrimarySkills" HeaderText="Primary Skill" ItemStyle-VerticalAlign="Top" SortExpression="PrimarySkills">
                                <ItemStyle Width="10%" />
                                <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SecondarySkills" HeaderText="Skill" ItemStyle-VerticalAlign="Top">
                                <ItemStyle Width="10%" />
                                <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SkillVersion" HeaderText="Skill Version" ItemStyle-VerticalAlign="Top">
                                <ItemStyle Width="10%" />
                                <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ExperienceInMonth" HeaderText="Experience (Months)" ItemStyle-VerticalAlign="Top">
                                <ItemStyle Width="7%" />
                                <HeaderStyle Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Proficiency" HeaderText="Proficiency" ItemStyle-VerticalAlign="Top">
                                <ItemStyle Width="10%" />
                                <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LastUsedDate" HeaderText="Last Used">
                                <ItemStyle Width="7%" />
                                <HeaderStyle Width="7%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                   <%-- </div>--%>
                </td>
            </tr>
        </table>
    <%--</div>--%>
</asp:Content>
