<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveRejectRP.aspx.cs" Inherits="ApproveRejectRP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript">
    
    //--Set bg to tabs
    setbgToTab('ctl00_tabProject', 'ctl00_divApproveRejectResourcePlan');
        
    
    function $(v){return document.getElementById(v);}
    
    function ButtonClickValidate()
    {
        var Flag=false;
        var lblMandatory;
        var obj = $('<%=txtComments.ClientID %>').value
        var txtcomment=$('<%=txtComments.ClientID %>')
        var hdnFieldAppRej=$('<%=hdnFieldApproveOrReject.ClientID%>').value
        
        if(obj=="")
        {
            HighLightControl(txtcomment)
            lblMandatory = $( '<%=lblMandatory.ClientID %>')
            
            //--for approval
            if(hdnFieldAppRej=="approved")
            {
            lblMandatory.innerText = "Kindly enter data in the comment textbox.";
            }
            
            //--for rejection
            if(hdnFieldAppRej=="rejected")
            {
            lblMandatory.innerText = "Kindly enter data in the reason for rejection textbox.";
            }
            
            lblMandatory.style.color = "Red";
        }
        else
        {
            
//            if(confirm("Do you want to continue?") == true)    
//            {
//                Flag=true;  
//            }
//            else
//            {
//                Flag=false;  
//            }
            Flag = true;
        }
        
        return Flag;
        
    }
    
    function HighLightControl(control)
        {
            control.style.borderStyle = "Solid";
            control.style.borderWidth = "2";
            control.style.borderColor = "Red";                    
        }
    </script>

    <table width="100%">
        <tr>
        <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center"  class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1'); ">
              <%--   Sanju:Issue Id 50201:End--%>
                <span class="header">Approve/Reject Resource Plan</span>
            </td>
        </tr>
        <tr><td align = "left"><asp:Label ID="lblMandatory" runat="server" class="txtstyle" Font-Names="Verdana" Font-Size="9pt" visible ="false"></asp:Label>&nbsp;</td></tr>
    </table>
    <div class="detailsborder">
        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="reasonForRejectionIDTable">
            <asp:Panel ID="CommentPanel" runat="server" Visible="false">
                <tr id="reasonForRejectionIDRow">
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="rowbgStyle" width="30%" align="left">
                                    <asp:Label runat="server" ID="lblComments" Text="Comment"></asp:Label><span class="mandatorymark"><%--*--%></span>&nbsp;
                                </td>
                                <td class="rowbgStyle" width="20%" valign="top" align="right">
                                </td>
                                <td class="rowbgStyle" align="left" style="width: 40%">
                                    <asp:TextBox runat="server" ID="txtComments" Width="300px" ToolTip="Enter Comments" Text = " "></asp:TextBox>
                                </td>
                                <td class="rowbgStyle" align="left" style="width: 7%">
                                    <asp:Button ID="saveBtn" runat="server" CssClass="button" Text="Save" Width="58px" OnClick="btnSaveComment_ClickEvtHandler" />
                                </td>
                                <td width="12%" class="rowbgStyle" align="left">
                                    <asp:Button ID="cancelBtn" runat="server" CssClass="button" Text="Cancel" Width="60px" OnClick="btnCancelComment_ClickEvtHandler" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td>
                    <table width="100%" class="detailsbg" cellspacing="0" cellpadding="4" border="0">
                        <tr>
                            <td class="detailsheader">
                                List of Resource Plan
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="tr_PendingResourcePlan" visible="true" runat="server">
                <td style="width: 99%">
                    <asp:GridView runat="server" ID="grdPendingResourcePlan" AutoGenerateColumns="False" Width="100%" EmptyDataText="No Records Found" OnRowCommand="grdPendingResourcePlan_RowCommand" AllowPaging="True" AllowSorting="True" OnRowCreated="grdPendingResourcePlan_RowCreated" OnDataBound="grdPendingResourcePlan_DataBound" OnSorting="grdPendingResourcePlan_Sorting" OnRowDataBound="grdPendingResourcePlan_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgbtnExpandCollaspeChildGrid" ImageUrl="Images/plus.JPG" Width="13px" Height="13px" CommandName="ChildGridApproveRejectResourcePlan" ToolTip="Expand" CommandArgument='<%#Eval("ProjectId")%>' />
                                    <asp:HiddenField ID="hdnFAppRej" runat="server" Value='<%#Eval("ProjectId")%>' Visible="false" />
                                </ItemTemplate>
                                <ItemStyle Width="7%" VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProjectCodeAbbrevation" HeaderText="Project Code" SortExpression="ProjectCodeAbbrevation">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="14%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectName" runat="server" Text='<%#Eval("ProjectName")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="14%" VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProjectStatus" HeaderText="Project Status" SortExpression="Status">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="14%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Project Manager">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectManager" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="14%" VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="13%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="StartDate">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="11%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="End Date" SortExpression="EndDate">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="11%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEndDate" Text='<%#Eval("EndDate", "{0:dd/MM/yyyy}") %>' DataFormatString="{0:dd/MM/yyyy}"></asp:Label>
                                    </td></tr>
                                    <tr runat="server" id="tr_ResourcePlan" style="display: none;">
                                        <td colspan="8" width="100%">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td class="childgridheader">
                                                        <span class="detailsheader">List of Resource Plan For "<asp:Label ID="lblchildProjectName" runat="server" />"</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView runat="server" ID="grdResourcePlan" AutoGenerateColumns="False" Width="100%" EmptyDataText="No Records Found" OnRowCommand="grdChildRowPendingResourcePlan_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnFRPID" runat="server" Value='<%#Eval("RPId")%>' Visible="false" EnableViewState="true" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="3%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Resource Plan Code">
                                                                    <ItemTemplate><asp:Label runat = "server" ID = "RPCode" Text = '<%#Eval("ResourcePlanCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <HeaderStyle Width="14%" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <HeaderStyle Width="14%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <HeaderStyle Width="14%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <HeaderStyle Width="14%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <HeaderStyle Width="14%" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate><asp:Label runat = "server" ID = "lblRPApprovalStatusId" Text = '<%#Eval("ResourcePlanApprovalStatus") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <HeaderStyle Width="14%" />
                                                                    </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="imgApprove" ImageUrl="~/Images/approve.jpg" ToolTip="Approve" Width="20px" Height="20px" CommandName="ApproveImageBtn" CommandArgument='<%# Container.DisplayIndex %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="imgReject" ImageUrl="~/Images/reject.jpg" ToolTip="Reject" Width="20px" Height="20px" CommandName="RejectImageBtn" CommandArgument='<%# Container.DisplayIndex %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton runat="server" ID="imgView" ImageUrl="~/Images/view.png" ToolTip="View" Width="20px" Height="20px" CommandName="ViewImageBtn" CommandArgument='<%# Container.DisplayIndex %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataRowStyle CssClass="text" />
                                                            <HeaderStyle CssClass="childgridheader" />
                                                            <RowStyle CssClass="childgridRow" />
                                                            <AlternatingRowStyle CssClass="childgridAlternatingRow" />
                                                            <SelectedRowStyle BackColor="#0099CC" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                            <table class="tablePager">
                                <tr>
                                    <td align="center">
                                        <span class="txtstyle">&lt;&lt;&nbsp;&nbsp;</span><asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white" CommandName="Previous" OnCommand="ChangePage" Font-Underline="true" Enabled="False" CssClass="txtstyle">Previous</asp:LinkButton>
                                        <span class="txtstyle">Page</span>
                                        <asp:TextBox ID="txtPages" runat="server" OnTextChanged="txtPages_TextChanged" AutoPostBack="true" Width="21px" MaxLength="3" onpaste="return false;" CssClass="txtstyle"></asp:TextBox>
                                        <span class="txtstyle">of</span>
                                        <asp:Label ID="lblPageCount" runat="server" ForeColor="white" CssClass="txtstyle"></asp:Label>
                                        <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next" OnCommand="ChangePage" Font-Underline="true" Enabled="False" CssClass="txtstyle">Next</asp:LinkButton><span class="txtstyle">&nbsp;&nbsp;&gt;&gt;</span>
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
                        <HeaderStyle CssClass="gridheaderStyle" />
                        <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                        <RowStyle Height="20px" CssClass="txtstyle" />
                        <EmptyDataRowStyle CssClass="text" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnFieldApproveOrReject" runat="server"  />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
