<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="MRFAgingReport.aspx.cs" Inherits="MRFAgingReport" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script src="JavaScript/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="JavaScript/ScrollableGrid.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
        jQuery('#<%=GVMRFAgingReport.ClientID %>').Scrollable();
        }
    )
    </script>

    <div style="font-family: Verdana; font-size: 9pt;">
        <table width="100%">
            <tr>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                    <span class="header">MRF Aging Report</span>
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
                    <asp:GridView ID="GVMRFAgingReport" runat="server" AutoGenerateColumns="False" AllowSorting="true" OnSorting="GVMRFAgingReport_Sorting" OnRowCreated="GVMRFAgingReport_RowCreated"
                        AllowPaging="false" Width="100%">
                        <HeaderStyle CssClass="headerStyleFreeze" />
                        <AlternatingRowStyle CssClass="alternatingrowStyleFreeze" />
                        <RowStyle Height="20px" CssClass="textstyleFreeze" />
                        <Columns>
                            <asp:BoundField DataField="Technology" HeaderText="Technology" SortExpression="Technology">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AverageTimeTaken" HeaderText="Average Time Taken" SortExpression="AverageTimeTaken">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <%--</div>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
