<%@ Page Language="C#" MasterPageFile="~/MasterPage/Employee.master" AutoEventWireup="true"
    CodeFile="EmpOtherDetails.aspx.cs" Inherits="EmpOtherDetails" Title="Other Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEmployeeContent" runat="Server">
    <div class="detailsborder">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
                <table width="100%" class="detailsbg">
                    <tr>
                        <td>
                            <asp:Label ID="lblEmpOtherDetails" runat="server" Text="Other Details" CssClass="detailsheader"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Otherdetails" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblRelocationIndia" runat="server" Text="Are you okay with relocation to any part of India?"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rbtnRelocateIndia" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rbtnRelocateIndia_SelectedIndexChanged" AutoPostBack="true"
                                    ToolTip="Choose Yes or No">
                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblReasonRelocationIndia" runat="server" Text="If no Please provide a reason"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtNoRelocationIndiaReason" runat="server" TextMode="MultiLine" MaxLength="500" ></asp:TextBox>
                                <span id="spanNoRelocationIndiaReason" runat="server">
                                <img id="imgNoRelocationIndiaReason" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRelocationOtherCountry" runat="server" Text="Are you okay to relocate to any other country?"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rbtnRelocateOther" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rbtnRelocateOther_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblReasonRelocationOtherCountry" runat="server" Text="If no Please provide a reason"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtNoRelocationOtherCountryReason" runat="server" TextMode="MultiLine" MaxLength="500">
                                </asp:TextBox>
                                <span id="spanNoRelocationOtherCountryReason" runat="server">
                                <img id="imgNoRelocationOtherCountryReason" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDuration" runat="server" Text="If yes then select the duration"></asp:Label>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rbtnDuration" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rbtnRelocateOther_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>0-3Mths</asp:ListItem>
                                    <asp:ListItem>3-6Mths</asp:ListItem>
                                    <asp:ListItem Value="6-1Year">6-1Year</asp:ListItem>
                                    <asp:ListItem>1Year+</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table width="100%">
                    <tr>
                        <td align="right" colspan="3">
                            <asp:HiddenField ID="EMPId" runat="server" />
                            <asp:Button ID="btnPrevious" runat="server" CssClass="button" OnClick="btnPrevious_Click"
                                Text="Previous" />
                            <asp:Button ID="btnSave" runat="server" CausesValidation="true" CssClass="button" 
                                OnClick="btnSave_Click" TabIndex="18" Text="Save" Width="119px" Visible="false"/>
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick="btnCancel_Click"
                                Text="Cancel" Visible="false"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
    
    <script language="javascript" type="text/javascript">

    //Highlighting the tabs by passing individaul tab id
    setbackcolorToTab('divMenu_7');
    
    </script>
    
</asp:Content>
