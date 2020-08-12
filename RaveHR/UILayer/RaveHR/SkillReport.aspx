<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="SkillReport.aspx.cs" Inherits="SkillReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <style type="text/css">
        .TitleCaseText
        {
            text-transform: capitalize;
        }
    </style>

    <script src="JavaScript/CommonValidations.js" type="text/javascript"></script>

    <script src="JavaScript/FilterPanel.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>

    <script type="text/javascript">

        //setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmpSummary');

        function $(v) {
            return document.getElementById(v);
        }

        function CheckLevel(ddl){

            var panel0 = document.getElementById('<%= PnlLevel0.ClientID %>');
            var panel1 = document.getElementById('<%= PnlLevel1.ClientID %>');
            var panel2 = document.getElementById('<%= PnlLevel2.ClientID %>');
            var panel3 = document.getElementById('<%= PnlLevel3.ClientID %>');
            var BtnExport = document.getElementById('<%= btnExport.ClientID %>');

            if ((ddl.value == "Level 0" && panel0 != null) || (ddl.value == "Level 1" && panel1 != null) 
                    ||(ddl.value == "Level 2" && panel2 != null) ||(ddl.value == "Level 3" && panel3 != null)){
                BtnExport.style.visibility = "visible";
            }
            else {
                if (BtnExport != null) {
                    BtnExport.style.visibility = "hidden"; //display = 'none';
                }
            }
        }
       
    </script>

    <div style="font-family: Verdana; font-size: 9pt;">
        <table width="100%">
            <tr>
                <%--Sanju:Issue Id 50201: Added new class header_employee_profile so that the header color is same for all browsers--%>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                    <span class="header">Skill Report</span>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="center">
                    <table width="80%">
                        <tr>
                            <td align="center">
                                Level :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLevel" runat="server" ToolTip="Select Project" onchange="CheckLevel(this)">
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="button" Font-Bold="True"
                                    Font-Size="9pt" OnClick="btnFilter_Click" ValidationGroup="Filter"/>&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnExport" Text="Export to Excel" CssClass="button"
                                    Visible="false" UseSubmitBehavior="false" OnClick="btnExport_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLevel" ValidationGroup="Filter"
                                    ErrorMessage="* Please select Level" InitialValue="Select"></asp:RequiredFieldValidator>
                                 <%--<asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red" Text="0"/>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="PnlLevel0" runat="server">
            <table cellspacing="0" cellpadding="0" width="60%">
                <tr class="detailsbg" style="height: 25px">
                    <td class="detailsheader">
                        Level 0- Skills Report <asp:Label ID="Level0Count" runat="server" style="padding-left:10px"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLevel0" runat="server" AllowSorting="true" AllowPaging="false" OnSorting="gvLevel0_Sorting" OnRowCreated="gvLevel0_RowCreated" 
                            Width="100%" >
                            <HeaderStyle CssClass="headerStyle" />
                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                            <RowStyle Height="20px" CssClass="textstyle" />
                        </asp:GridView>
                     </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlLevel1" runat="server">
            <table width="60%" cellspacing="0" cellpadding="0">
                <tr class="detailsbg" style="height: 25px">
                    <td class="detailsheader">
                        Level 1- Skills Report (Division) - NPS <asp:Label ID="Level1NPSCount" runat="server" style="padding-left:10px"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLevel1NPS" runat="server" AllowSorting="true" AllowPaging="false" OnSorting="gvLevel1NPS_Sorting" OnRowCreated="gvLevel1NPS_RowCreated" 
                            Width="100%">
                            <HeaderStyle CssClass="headerStyle" />
                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                            <RowStyle Height="20px" CssClass="textstyle" />
                        </asp:GridView>
                        <br />
                        &nbsp;
                    </td>
                </tr>
                <tr class="detailsbg" style="height: 25px">
                    <td class="detailsheader">
                        Level 1- Skills Report (Division) - NGA <asp:Label ID="Level1NGSCount" runat="server" style="padding-left:10px"> </asp:Label>
                    </td>
                </tr>
                <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvLevel1NGA" runat="server" AllowSorting="true" AllowPaging="false" OnSorting="gvLevel1NGA_Sorting" OnRowCreated="gvLevel1NGA_RowCreated"
                            Width="100%">
                            <HeaderStyle CssClass="headerStyle" />
                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                            <RowStyle Height="20px" CssClass="textstyle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlLevel2" runat="server">
            <table cellspacing="0" cellpadding="0" width="60%">
                 <tr class="detailsbg" style="height: 25px">
                    <td class="detailsheader">
                        Level 2- Skills Report( Business Area) <asp:Label ID="Level2Count" runat="server" style="padding-left:10px"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <!--<asp:UpdatePanel runat="server" ID="UP_EmployeeSummary"> 
                            <ContentTemplate>-->
                                <asp:GridView ID="gvLevel2" runat="server" AutoGenerateColumns="false" Width="100%"
                                    OnRowCommand="gvLevel2_RowCommand" AllowSorting="true" OnSorting="gvLevel2_Sorting" OnRowCreated="gvLevel2_RowCreated">
                                    <HeaderStyle CssClass="headerStyle" />
                                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                    <RowStyle Height="20px" CssClass="textstyle" />
                                    <Columns>
                                        
                                        <asp:BoundField DataField="Business Area" HeaderText="Business Area" ItemStyle-Width="40%" ShowHeader="true" SortExpression="Business Area" >
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnExpandCollaspeChildGrid" runat="server" CommandArgument='<%#Eval("Business Area")+","+ Eval("Resource Type") %>'
                                                    CommandName="ChildGridSkillsForResource" Height="13px" ImageUrl="Images/plus.JPG"
                                                    ToolTip="Expand" Width="16px" />
                                                <%--<asp:HiddenField ID="hfProjectCount" runat="server" Value='<%# Bind("ProjectCount") %>' />
                                                <asp:HiddenField ID="hfClientCount" runat="server" Value='<%# Bind("ClientCount") %>' />--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource Type" SortExpression="Resource Type">
                                            <HeaderStyle Width="40%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("Resource Type") %>'></asp:Label>
                                                </td></tr>
                                                <tr id="tr_DetalGrid" style="display: none; width: 100%" runat="server">
                                                    <td>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gvDetailSkills" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            EmptyDataText="No Records Found" ShowHeader="false" AllowSorting="false" >
                                                            <Columns>
                                                                <%--<asp:BoundField HeaderText="" SortExpression="" Visible="true">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle Width="40%" HorizontalAlign="center" />
                                                                </asp:BoundField>--%>
                                                                <asp:BoundField HeaderText="" SortExpression="" Visible="true" >
                                                                    <ItemStyle HorizontalAlign="Center" Width="7%"/>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Skills" HeaderText="" >
                                                                </asp:BoundField>
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
                                </asp:GridView>
                            <!-- </ContentTemplate>
                        </asp:UpdatePanel> -->
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlLevel3" runat="server">
            <table cellspacing="0" cellpadding="0" width="60%">
                 <tr class="detailsbg" style="height: 25px">
                    <td class="detailsheader">
                        Level 3- Skills Report( Business Segment) <asp:Label ID="Level3Count" runat="server" style="padding-left:10px"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <asp:GridView ID="gvLevel3" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnRowCommand="gvLevel3_RowCommand" AllowSorting="true" OnSorting="gvLevel3_Sorting" OnRowCreated="gvLevel3_RowCreated">
                                    <HeaderStyle CssClass="headerStyle" />
                                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                    <RowStyle Height="20px" CssClass="textstyle" />
                                    <Columns>
                                        
                                        <asp:BoundField DataField="Business Segment" HeaderText="Business Segment" ItemStyle-Width="40%" SortExpression="Business Segment">
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnExpandCollaspeChildGrid3" runat="server" CommandArgument='<%#Eval("Business Segment")+","+ Eval("Resource Type") %>'
                                                    CommandName="ChildGridSkillsForResource" Height="13px" ImageUrl="Images/plus.JPG"
                                                    ToolTip="Expand" Width="16px" />
                                                <%--<asp:HiddenField ID="hfProjectCount" runat="server" Value='<%# Bind("ProjectCount") %>' />
                                                <asp:HiddenField ID="hfClientCount" runat="server" Value='<%# Bind("ClientCount") %>' />--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource Type" SortExpression="Resource Type">
                                            <HeaderStyle Width="40%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("Resource Type") %>'></asp:Label>
                                                </td></tr>
                                                <tr id="tr_DetalGrid3" style="display: none; width: 100%" runat="server">
                                                    <td>&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:GridView ID="gvDetailSkills3" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            EmptyDataText="No Records Found" ShowHeader="false" AllowSorting="false">
                                                            <Columns>
                                                                <%--<asp:BoundField HeaderText="" SortExpression="" Visible="true">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle Width="40%" HorizontalAlign="center" />
                                                                </asp:BoundField>--%>
                                                                <asp:BoundField  SortExpression="" Visible="true" >
                                                                    <ItemStyle HorizontalAlign="Center" Width="7%"/>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Skills" >                                                                    
                                                                </asp:BoundField>
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
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
