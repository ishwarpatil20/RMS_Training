<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPage.master"
CodeFile="AddRowTest.aspx.cs" Inherits="AddRowTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
<asp:Content>
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
         <!-- OnRowDataBound="gvCostCode_RowDataBound"  OnRowCommand="gvCostCode_RowCommand" -->
          <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
        <div style="border: solid 1px black">
            <asp:GridView ID="gvCostCode" runat="server" AutoGenerateColumns="False"  GridLines="None" Width="100%">
           
                <HeaderStyle CssClass="headerStyle" />
                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                <RowStyle Height="20px" CssClass="textstyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Project" ItemStyle-Width="160px" HeaderStyle-Width="160px">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlSkill" runat="server" ToolTip="Select Project" Width="230px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost Code" ItemStyle-Width="160px" HeaderStyle-Width="160px">
                        <ItemTemplate>
                           <asp:TextBox ID="txtCostCode" runat="server" ToolTip="Enter Cost Code" Width="230px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Utilization" ItemStyle-Width="160px" HeaderStyle-Width="160px">
                        <ItemTemplate>
                           <asp:TextBox ID="txtUtilization" runat="server" ToolTip="Enter Utilization" Width="230px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                    <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                CommandName="DeleteSkill" ToolTip="Delete Row" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        </td>
        </tr>
        </table>
        
  <table style="width: 100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnAddRow" runat="server" Text="Add New Row" OnClick="btnAddRow_Click"
                        CssClass="button" />
                   <%-- &nbsp;<asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="button" OnClick="BtnSearch_Click"/>
                    &nbsp;<asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="button" OnClick="BtnClear_OnClick"/>
                    &nbsp;<asp:Button runat="server" ID="btnExport" Text="Export to Excel" CssClass="button"
                        Visible="false" OnClick="btnExport_Click" />--%>
                </td>
            </tr>
  </table>
</asp:Content>
</asp:Content>