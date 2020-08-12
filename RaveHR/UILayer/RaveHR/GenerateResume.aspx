<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="GenerateResume.aspx.cs" Inherits="GenerateResume" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="detailsborder">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                background-color: #7590C8">
                <span class="header">Generate Resume</span>
            </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="width: 25%">
                </td>
                <td style="width: 25%">
                </td>
                <td style="width: 25%">
                </td>
                <td style="width: 25%">
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                </td>
                <td style="width: 20%" align ="center">
                    <asp:Label ID="lblRaveFormat" runat="server" Text="Rave Format"></asp:Label>
                </td>
                <td style="width: 30%" align="left">
                    <asp:RadioButton ID="rBtnRaveFormat" runat="server" 
                        oncheckedchanged="rBtnRaveFormat_CheckedChanged" AutoPostBack="True" />
                </td>
                <td style="width: 20%">
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                </td>
                <td style="width: 25%" align="center">
                    <asp:Label ID="lblClientFormat" runat="server" Text="Client Format"></asp:Label>
                </td>
                <td style="width: 25%" align="left">
                    <asp:RadioButton ID="rBtnClientFormat" runat="server" 
                        oncheckedchanged="rBtnClientFormat_CheckedChanged" AutoPostBack="True" />
                </td>
                <td style="width: 25%">
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                </td>
                <td style="width: 20%">
                </td>
                <td style="width: 30%">
                </td>
                <td style="width: 20%">
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                </td>
                <td style="width: 20%">
                </td>
                <td style="width: 30%">
                </td>
                <td style="width: 20%">
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                </td>
                <td style="width: 20%">
                </td>
                <td colspan="2">
                    <asp:Button ID="btnGenerateResume" runat="server" Text="Generate Resume" 
                        CssClass="button" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" 
                        onclick="btnCancel_Click" />
                </td>
                
            </tr>
        </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
