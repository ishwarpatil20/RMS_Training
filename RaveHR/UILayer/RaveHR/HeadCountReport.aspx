<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="HeadCountReport.aspx.cs" Inherits="HeadCountReport" Title="Head Count Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script src="JavaScript/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="JavaScript/ScrollableGrid.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
        jQuery('#<%=grdvHeadCount.ClientID %>').Scrollable();
        }
    )
    
    </script>

   <%-- <div class="detailsborder">--%>
        <table width="100%">
            <tr>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                    <span class="header">Head Count Report</span>
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
        </table>
        <div style="border: solid 1px black">
            <table width="100%">
                <tr>
                    <td style="width: 50%;" valign="top">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 141px;">
                                    <asp:Label ID="lblDivision" runat="server" Text="Division" CssClass="txtstyle"></asp:Label>
                                </td>
                                <td style="width: 278px">
                                    <asp:DropDownList ID="ddlDivision" runat="server" ToolTip="Select Division" Width="212px"
                                        CssClass="mandatoryField" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 149px">
                                    <asp:Label ID="lblBusinessArea" runat="server" Text="Business Area" CssClass="txtstyle"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBusinessArea" runat="server" ToolTip="Select Business Area"
                                        Width="212px" CssClass="mandatoryField" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessArea_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr width="210px">
                                <td style="height: 26px; width: 141px;">
                                    <asp:Label ID="lblBusinessSegment" runat="server" Text="Business Segment" CssClass="txtstyle"></asp:Label>
                                </td>
                                <td style="height: 26px; width: 278px;">
                                    <asp:DropDownList ID="ddlBusinessSegment" runat="server" ToolTip="Select Business Segment"
                                        Enabled="false" Width="215px" CssClass="mandatoryField">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 400px; height: 26px;" colspan="2">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                                    &nbsp;<asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="button" OnClick="BtnClear_Click" />
                                    &nbsp;<asp:Button runat="server" ID="btnExport" Text="Export to Excel" CssClass="button"
                                        Visible="false" OnClick="btnExport_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        
        <!--Siddharth 29 April 2015 Showing the Count of resources in Header Start-->
        <div id="divCount" >
         <b>
          <asp:Label ID="lblCount" runat="server" Text="Total Count : " CssClass="txtstyle" visible="false"></asp:Label>
         </b>
        </div>
        <!--Siddharth 29 April 2015 Showing the Count of resources in Header End-->
         <br />
        
        
        
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <%--<div style="max-height: 500px; width: 100%; overflow: auto;">--%>
                        <asp:GridView ID="grdvHeadCount" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="false" AllowSorting="true" OnSorting="grdvHeadCount_Sorting" OnRowCreated="grdvHeadCount_RowCreated" >
                            <HeaderStyle CssClass="headerStyleFreeze" />
                            <AlternatingRowStyle CssClass="alternatingrowStyleFreeze" />
                            <RowStyle Height="20px" CssClass="textstyleFreeze" />
                            <Columns>
                                <asp:TemplateField ItemStyle-VerticalAlign="Top" HeaderText="Division" ItemStyle-Width="10%"
                                    HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" SortExpression="Division">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Projects.Division")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-VerticalAlign="Top" HeaderText="Business Area" ItemStyle-Width="12%"
                                    HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" SortExpression="BusinessArea">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Projects.BusinessArea")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-VerticalAlign="Top" HeaderText="Business Segment" ItemStyle-Width="20%"
                                    HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" SortExpression="BusinessSegment">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Projects.BusinessSegment")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CostCode" HeaderText="Cost Code" ItemStyle-VerticalAlign="Top"
                                    ItemStyle-Width="25%" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" SortExpression="CostCode">
                                </asp:BoundField>
                                <asp:BoundField DataField="ResourceType" HeaderText="Resource Type" ItemStyle-VerticalAlign="Top"
                                    ItemStyle-Width="10%" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" SortExpression="ResourceType">
                                </asp:BoundField>
                                <asp:BoundField DataField="ResourceTypeCount" HeaderText="Count" ItemStyle-VerticalAlign="Top"
                                    ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left" SortExpression="ResourceTypeCount">
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    <%--</div>--%>
                </td>
            </tr>
        </table>
    <%--</div>--%>
</asp:Content>
