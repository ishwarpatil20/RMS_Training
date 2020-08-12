<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup.aspx.cs" Inherits="Popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 963px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <table width="100%">
        <tr>
            <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                background-color: #7590C8">
                <span class="header">Search Employee</span>
            </td>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
    </table>
    <div style="overflow: auto; height: 315px; width:100%;">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="style1">
                <asp:GridView ID="gvPopUp" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                    AllowSorting="true" Width="100%">
                    <HeaderStyle CssClass="headerStyle" />
                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                    <RowStyle Height="20px" CssClass="textstyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" AutoPostBack="false"/>
                                 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EmployeeId" HeaderText="EmpId"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name"  ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Button ID="btnSelect" runat="server" Text = "Select and Close" 
                    onclick="btnSelect_Click" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
