<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
<table width="100%">
    <tr>
        <td class="logo" align="left">
            <asp:Image ID="imgRaveLogo" runat="server" ImageUrl="~/Images/HomeImage.jpg" BackColor="AliceBlue" Height="270px" Width="340px"/>
        </td>
        <td valign="top" align="right">
            <asp:Button runat="server" ID="btnAdvancedSearch" Text="Advanced Search" CssClass="button" Visible = "false" />
        </td>
    </tr>
    <tr><td><asp:Label runat = "server"  ID = "lblMessage"></asp:Label></td></tr>
</table>

<script type = "text/javascript">
    /// <summary>Set background to tabs</summary>
    setbgToTab('ctl00_tabHome', '');
</script>
<div>
   
</div>
</asp:Content>
