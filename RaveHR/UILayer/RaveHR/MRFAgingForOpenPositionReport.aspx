<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"  EnableEventValidation = "false"
    CodeFile="MRFAgingForOpenPositionReport.aspx.cs" Inherits="MRFAgingForOpenPositionReport"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script src="JavaScript/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="JavaScript/ScrollableGrid.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
        jQuery('#<%=GVMRFAgingForOpenPositionReport.ClientID %>').Scrollable();
        }
    )
    </script>

    <div style="font-family: Verdana; font-size: 9pt;">
        <table width="100%">
            <tr>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                    <span class="header">MRF Aging Report For Open Positions</span>
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
                    <asp:Button runat="server" ID="btnExport" Text="Export to Excel" CssClass="button"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<div style="max-height: 500px; width: 100%; overflow: auto;">--%>
                    <asp:GridView ID="GVMRFAgingForOpenPositionReport" runat="server" AutoGenerateColumns="False"
                        AllowSorting="true" OnSorting="GVMRFAgingForOpenPositionReport_Sorting" OnRowCreated="GVMRFAgingForOpenPositionReport_RowCreated"
                        AllowPaging="false" Width="100%">
                        <HeaderStyle CssClass="headerStyleFreeze" />
                        <AlternatingRowStyle CssClass="alternatingrowStyleFreeze" />
                        <RowStyle Height="20px" CssClass="textstyleFreeze" />
                        <Columns>
                            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName">
                                <HeaderStyle HorizontalAlign="Left" Width="15%"/>
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MRFCode" HeaderText="MRFCode" SortExpression="MRFCode">
                                <HeaderStyle HorizontalAlign="Left"  Width="20%"/>
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Technology" HeaderText="Technology" SortExpression="Technology">
                                <HeaderStyle HorizontalAlign="Left" Width="15%"/>
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation">
                                <HeaderStyle HorizontalAlign="Left" Width="15%"/>
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RecruitmentStartDate" HeaderText="Recruitment Start Date"
                                SortExpression="RecruitmentStartDate">
                                <HeaderStyle HorizontalAlign="Left" Width="8%"/>
                                <ItemStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Aging" HeaderText="Aging" SortExpression="Aging">
                                <HeaderStyle HorizontalAlign="Left" Width="5%"/>
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AvgAging" HeaderText="Avg Aging" SortExpression="AvgAging">
                                <HeaderStyle  HorizontalAlign="Left" Width="5%"/>
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <%--</div>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
