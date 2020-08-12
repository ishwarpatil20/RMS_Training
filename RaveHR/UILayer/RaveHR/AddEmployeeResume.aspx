<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddEmployeeResume.aspx.cs" Inherits="AddEmployeeResume" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
         <%-- Sanju:Issue Id 50201: Added new class so that all browsers display header--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label>
            </td>
              <%--    Sanju:Issue Id 50201: End--%>
                <%-- Sanju:Issue Id 50201: Added background color property so that all browsers display header--%>
            <td align="right" style="height: 25px; width: 20%;background-color:#7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
             <%--    Sanju:Issue Id 50201: End--%>
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
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
                    <contenttemplate>
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
                                <tr class="detailsbg">
                                    <td>
                                        <asp:Label ID="lblResume" runat="server" Text="Employee Resume" CssClass="detailsheader"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="EmployeeResumedetails" runat="server">
                                <table width="100%">
                                    <tr> 
                                        <td>
                                            
                                            <asp:FileUpload  ID="fileResume" runat="server" width="550px"/>
                                            
                                        </td>
                                    </tr>
                                        <td style="width: 25%;" colspan="2" align="right">
                                            <asp:Button ID="btnUpload" runat="server" CssClass="button" OnClick="btnUpload_Click"
                                                TabIndex="14" Text="Upload" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"
                                                />
                                        </td>
                                   
                                </table>
                                
                                <div id="divGVResume">
                                    <asp:GridView ID="gvEmployeeResume" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnRowCommand="gvEmployeeResume_RowCommand" OnRowDeleting="gvEmployeeResume_RowDeleting">
                                        <HeaderStyle CssClass="headerStyle" />
                                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                        <RowStyle Height="20px" CssClass="textstyle" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" HeaderText="Resume">
                                                <ItemTemplate >
                                                <asp:LinkButton ID="_lnkResume" runat="server" ControlStyle-Font-Bold="False" CommandName="View"
                                                ControlStyle-Font-Size="11px" ControlStyle-Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                ControlStyle-Font-Underline="true" ControlStyle-ForeColor="Blue"  Text='<%# DataBinder.Eval(Container, "DataItem.DocumentName") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ModifyDate"  ItemStyle-Width="20%" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ModifyBy" ItemStyle-Width="20%" HeaderText="Uploaded By"  />
                                           <asp:TemplateField Visible="false">
                                           <ItemTemplate>
                                           <asp:Label ID="lblExtension" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FileExtension") %>'> </asp:Label>
                                           </ItemTemplate>
                                           </asp:TemplateField>
                                           <%--19645-Ambar-Start--%>
                                           <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                <ItemTemplate>
                                                   <%--29769-Ambar-Added onClientClick Parameter--%>                                                  
                                                   <asp:LinkButton ID="_lnkDelResume" runat="server" ControlStyle-Font-Bold="False" CommandName="Delete"
                                                        ControlStyle-Font-Size="11px" ControlStyle-Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                        ControlStyle-Font-Underline="true" ControlStyle-ForeColor="Blue"  Text="Delete"                                                        
                                                        OnClientClick="return confirm('Are you sure, you want to delete this resume?')"
                                                        >
                                                   </asp:LinkButton>                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%--19645-Ambar-End--%>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="EMPId" runat="server" />
                                    <asp:HiddenField ID="HfIsDataModified" runat="server" />
                                    <asp:HiddenField ID="HfOldDocumentName" runat="server" />
                                </div>
                            </asp:Panel>
                            <div id="buttonControl">
                                <table width="100%">
                                    <tr align="right">
                                        <td style="width: 30%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 70%" align="right">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td style="width: 30%">
                                        </td>
                                        <td align="right" style="width: 70%">
                                           <%--<asp:Button ID="btnEdit" runat="server" CausesValidation="true" CssClass="button"
                                                OnClick="btnEdit_Click"  Text="Edit" Visible="false" Width="119px" />
                                            <asp:Button ID="btnEditCancel" runat="server" CausesValidation="true" CssClass="button"
                                                OnClick="btnEditCancel_Click" Text="Cancel" Visible="false" Width="119px" /> --%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </contenttemplate>
                    <%-- </asp:UpdatePanel>--%>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
