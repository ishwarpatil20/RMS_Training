<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeOtherDetails.aspx.cs" Inherits="EmployeeOtherDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
        <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
       <%--     Sanju:Issue Id 50201:end--%>
                <asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label>
            </td>
             <%-- Sanju:Issue Id 50201:Issue Id 50201: Added background color property so that all browsers display header--%>
            <td align="right" style="height: 25px; width: 20%;background-color:#7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
              <%--  Sanju:Issue Id 50201:End--%>
                <asp:Label ID="lblempName" runat="server" CssClass="header"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
    </table>
    <asp:Table ID="tblMain" runat="server" Width="100%" BorderColor="AliceBlue" BorderStyle="Solid"
        BorderWidth="2" Height="100%">
        <asp:TableRow ID="TableRow1" runat="server" Width="100%" VerticalAlign="Top">
            <asp:TableCell ID="tcellIndex" Width="15%" Height="100%" runat="server" BorderColor="Beige"
                BorderWidth="1">
                <!-- Panel for user control -->
                <asp:Panel ID="pnlUserControl" runat="server">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell ID="tcellContent" Width="85%" Height="100%" runat="server">
                <!-- Dump aspx code here -->
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
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
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
                                        <td class="txtstyle">
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
                                        <td class="txtstyle">
                                            <asp:Label ID="lblReasonRelocationIndia" runat="server" Text="If No Please provide a reason"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtNoRelocationIndiaReason" runat="server" TextMode="MultiLine"
                                                MaxLength="500" CssClass="mandatoryField"></asp:TextBox>
                                            <span id="spanNoRelocationIndiaReason" runat="server">
                                                <img id="imgNoRelocationIndiaReason" runat="server" src="Images/cross.png" alt=""
                                                    style="display: none; border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtstyle">
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
                                        <td class="txtstyle">
                                            <asp:Label ID="lblReasonRelocationOtherCountry" runat="server" Text="If no Please provide a reason"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtNoRelocationOtherCountryReason" runat="server" TextMode="MultiLine" CssClass="mandatoryField"
                                                MaxLength="500">
                                            </asp:TextBox>
                                            <span id="spanNoRelocationOtherCountryReason" runat="server">
                                                <img id="imgNoRelocationOtherCountryReason" runat="server" src="Images/cross.png"
                                                    alt="" style="display: none; border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblDuration" runat="server" Text="If Yes then select the duration"></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            <asp:RadioButtonList ID="rbtnDuration" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rbtnRelocateOther_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem>0-3Mths</asp:ListItem>
                                                <asp:ListItem>3-6Mths</asp:ListItem>
                                                <asp:ListItem Value="6Mths-1Year">6Mths-1Year</asp:ListItem>
                                                <asp:ListItem> More than 1 Year</asp:ListItem>
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
                                        <asp:HiddenField ID="HfIsDataModified" runat="server" />
                                        <asp:Button ID="btnSave" runat="server" CausesValidation="true" CssClass="button"
                                            OnClick="btnSave_Click" TabIndex="18" Text="Save" Width="119px"  />
                                        <asp:Button ID="btnEdit" runat="server" CausesValidation="true" CssClass="button"
                                                OnClick="btnEdit_Click" TabIndex="18" Text="Edit" Visible="false" Width="119px" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick="btnCancel_Click"
                                            Text="Cancel"/>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <script language="javascript" type="text/javascript">
                    setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmployeeProfile');
                </script>

            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
