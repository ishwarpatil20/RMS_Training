<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="4CLogin.aspx.cs" Inherits="_4CLogin" Title="4C Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" Runat="Server">

 <table width="50%">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext" Visible ="false"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </td>
            </tr>
        </table>
    
        <div id="divLogin" runat="server" visible="false" defaultbutton="btnLogin">
            <asp:RadioButtonList ID="rdbl4COption" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbl4COption_SelectedIndexChanged">
            <asp:ListItem Text="Add / Review Four C" Value="1" Selected="True"></asp:ListItem>
            <asp:ListItem Text="4C report" Value="2" ></asp:ListItem>
             <asp:ListItem Text="View My 4C" Value="3" ></asp:ListItem>
            </asp:RadioButtonList>
        <table width="50%">
            <tr>
            <td style="width:30%"> 
                      <asp:Label ID="lblUsername" runat="server" Text="Enter User Name : " Font-Bold="true"></asp:Label>
                </td>
                <td style="width:70%">
                    <span class="mandatorymark">*</span>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" Text="." ControlToValidate="txtUsername" ErrorMessage="Please Enter Username"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="button" />
                </td>
            </tr>
        </table>        
        </div>
</asp:Content>

