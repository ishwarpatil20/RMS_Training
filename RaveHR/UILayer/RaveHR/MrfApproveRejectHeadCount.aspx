<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="MrfApproveRejectHeadCount.aspx.cs" Inherits="MrfApproveRejectHeadCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <table width="100%">
    <%--Poonam Issue : 54321 : 2 Headings coming : Starts --%>
        <%--<tr>
         Sanju:Issue Id 50201:Added new class so that header display in other browsers also
            <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
            Sanju:Issue Id 50201:End
                <span class="header">List of Pending Head Count Approve/Reject</span>
            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="Label1" runat="server" CssClass="Messagetext"></asp:Label>
            </td>
        </tr>--%>
    <%--Poonam Issue : 54321 : 2 Headings coming : Ends --%>
        
    </table>
    <asp:Panel ID="pnlApprove" runat="server" Visible="false" Width="100%">
        <table width="100%">
            <tr>
                <td style="width: 50%" align="left" class="rowbgStyle">
                    <asp:Label ID="lbl" runat="server" Text="Comment" CssClass="txtstyle">
                    </asp:Label>
                    <%--<label class="mandatorymark">
                        *</label>--%>
                </td>
                <td style="width: 30%" align="left" class="rowbgStyle">
                    <asp:TextBox ID="txtComment" runat="server" CssClass="mandatoryField" Width="300px"
                        ToolTip="Enter Comment For Approval" MaxLength="100">
                    </asp:TextBox>
                </td>
                <td style="width: 20%" align="right" class="rowbgStyle">
                    <asp:Button ID="btnSaveApprove" runat="server" Text="Save" CssClass="button" OnClick="btnSaveApprove_Click" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlReject" runat="server" Visible="false" Width="100%">
        <table width="100%">
            <tr>
                <td style="width: 50%" align="left" class="rowbgStyle">
                    <asp:Label ID="lblReject" runat="server" Text="Reason for Rejection" CssClass="txtstyle">
                    </asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td style="width: 30%" align="left" class="rowbgStyle">
                    <asp:TextBox ID="txtReject" runat="server" CssClass="mandatoryField" Width="300px"
                        MaxLength="100" ToolTip="Enter reason for Rejection">
                    </asp:TextBox>
                </td>
                <td style="width: 20%" align="right" class="rowbgStyle">
                    <asp:Button ID="btnSaveReject" runat="server" Text="Save" CssClass="button" OnClick="btnSaveReject_Click" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
        <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
         <%--   Sanju:Issue Id 50201:End--%>
                <span class="header">List of Pending Approval of MRF</span>
            </td>
        </tr>
       <%--Poonam Issue : 54321 : 2 Headings coming : Starts --%>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext"></asp:Label>
            </td>
        </tr>
      <%--Poonam Issue : 54321 : 2 Headings coming : Ends --%>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
      
    </table>
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:GridView ID="grdvListofPendingApproveRejectHeadCount" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" OnPageIndexChanging="grdvListofPendingApproveRejectHeadCount_PageIndexChanging"
                    AllowSorting="True" OnSorting="grdvListofPendingApproveRejectHeadCount_Sorting"
                    OnRowCreated="grdvListofPendingApproveRejectHeadCount_RowCreated" OnRowDataBound="grdvListofPendingApproveRejectHeadCount_RowDataBound"
                    OnDataBound="grdvListofPendingApproveRejectHeadCount_DataBound" OnRowCommand="grdvListofPendingApproveRejectHeadCount_RowCommand"
                    DataKeyNames="MRFId" Width="100%">
                    <HeaderStyle CssClass="headerStyle" />
                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                    <RowStyle Height="20px" CssClass="textstyle" />
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MRFId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MRFCode" HeaderText="MRF Code" SortExpression="MRFCode"
                            HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="ResourceOnBoard" HeaderText="Start Date" SortExpression="StartDate"
                            DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="ResourceReleased" HeaderText="End Date" SortExpression="EndDate"
                            DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="EmployeeName" HeaderText="Raised By" SortExpression="MRFRaisedBy"
                            HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="Department"
                            HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName"
                            HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName"
                            HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="Utilization" HeaderText="Util(%)" SortExpression="Utilization"
                            HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="Billing" HeaderText="Bill(%)" SortExpression="Billing"
                            HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" HeaderStyle-Width="8%"
                            ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="MRFCTCString" HeaderText="Target CTC (Lks)" SortExpression="MRFCTC"
                            HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="ExperienceString" HeaderText="Experience (yrs)" SortExpression="Experience"
                            HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="RecruitmentManager" HeaderText="Recruitment Manager" SortExpression="RecruitmentManager"
                            HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName"
                            HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="MRFPurpose" HeaderText="MRF Purpose" SortExpression="MRFPurpose"
                            HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        
                      <%--    <asp:BoundField DataField="SOWNo" HeaderText="SOW NO" SortExpression="SOWNo"
                            HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="SOWStartDate" HeaderText="Sow Start Date" SortExpression="SOWStartDate"
                            DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="Center">
                         </asp:BoundField>
                         <asp:BoundField DataField="SOWEndDate" HeaderText="Sow End Date" SortExpression="SOWEndDate"
                            DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="Center">
                         </asp:BoundField>--%>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgApprove" ImageUrl="~/Images/approve.jpg" ToolTip="Approve"
                                    Width="20px" Height="20px" CommandName="Approve" CommandArgument='<%# Container.DisplayIndex %>' />
                            </ItemTemplate>
                            <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgReject" ImageUrl="~/Images/reject.jpg" ToolTip="Reject"
                                    Width="20px" Height="20px" CommandName="Reject" CommandArgument='<%# Container.DisplayIndex %>' />
                            </ItemTemplate>
                            <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgView" ImageUrl="~/Images/view.png" ToolTip="View"
                                    Width="20px" Height="20px" CommandName="View" CommandArgument='<%# Container.DisplayIndex %>' />
                                <asp:HiddenField ID="hdfRecruiterEmailId" Value='<%#Eval("EmailId")%>' runat="server" />
                                <asp:HiddenField ID="hdfMRFStatusId" Value='<%#Eval("StatusId")%>' runat="server" />
                                <asp:HiddenField ID="HfExpectedClosureDate" Value='<%#Eval("ExpectedClosureDate")%>'
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        <table class="tablePager" width="100%">
                            <tr>
                                <td align="center">
                                    &lt;&lt;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white" CommandName="Previous"
                                        OnCommand="ChangePage" Font-Underline="true" Enabled="False">
                                        Previous
                                    </asp:LinkButton>
                                    <span>Page</span>
                                    <asp:TextBox ID="txtPages" runat="server" OnTextChanged="txtPages_TextChanged" AutoPostBack="true"
                                        Width="21px" MaxLength="3" onpaste="return false;"></asp:TextBox>
                                    <span>of</span>
                                    <asp:Label ID="lblPageCount" runat="server" ForeColor="white"></asp:Label>
                                    <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                        OnCommand="ChangePage" Font-Underline="true" Enabled="False">
                                        Next
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;&gt;&gt;
                                </td>
                            </tr>
                        </table>
                    </PagerTemplate>
                    <SelectedRowStyle BackColor="#0099CC" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="btnCancel" Visible="true" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                    CssClass="button" />
            </td>
        </tr>
    </table>
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        var retVal;
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";

                $('#ctl00_cphMainContent_btnSaveApprove').click(function() {
                    $(this).val("Please Wait..");
                    $(this).attr('disabled', 'disabled');

                });

                $('#ctl00_cphMainContent_btnSaveReject').click(function() {
                    debugger;
                    if (ButtonRejectClickValidate()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                    }

                });

            });
        })(jQuery);
    
    setbgToTab('ctl00_tabMRF', 'ctl00_spanAppRejHeadCount');
    
function $(v){return document.getElementById(v);}

function ButtonRejectClickValidate()
{
   // debugger;
   var lblMandatory = $( '<%=lblMandatory.ClientID %>');
   var controlList = $( '<%=txtReject.ClientID %>').id;
   var Reject = $( '<%=txtReject.ClientID %>').value;
   var txtReject = $( '<%=txtReject.ClientID %>');
   
   if(trim(Reject) == "")
   {
       lblMandatory.innerText = "Kindly enter data in the reason for rejection textbox.";
       lblMandatory.style.color = "Red";
    
       txtReject.style.borderStyle = "Solid";
       txtReject.style.borderWidth = "2";
       txtReject.style.borderColor = "Red";
       txtReject.focus();
       return false;
   }

   if (ValidateControlOnClickEvent(controlList) == false) {
       return ValidateControlOnClickEvent(controlList);
   }
   else
       return true;
   
  
}

function RejectClickMessage()
{    
    alert("MRF is rejected, email notification is sent to RPM");
}

/*function ButtonApproveClickValidate()
{

      var lblMandatory = $( '<%=lblMandatory.ClientID %>');
      var txtApproveControl=$( '<%=txtComment.ClientID %>').id;
      var comment = $( '<%=txtComment.ClientID %>').value;
      var txtComment = $('<%=txtComment.ClientID %>');        
      
      if(trim(comment) == "")
      {            
            lblMandatory.innerText = "Kindly enter data in the comment textbox.";
            lblMandatory.style.color = "Red";
            
            txtComment.style.borderStyle = "Solid";
            txtComment.style.borderWidth = "2";
            txtComment.style.borderColor = "Red";
            txtComment.focus();
            return false;
      }
      
      if(ValidateControlOnClickEvent(txtApproveControl) == false)
      {            
            return ValidateControlOnClickEvent(txtApproveControl); 
      }
      
}*/

    </script>

</asp:Content>
