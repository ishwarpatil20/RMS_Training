<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="MrfPendingApproval.aspx.cs" Inherits="MrfPendingApproval" Title="List Of Pending Approval of MRF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <table width="100%">
       <%--Poonam Issue : 54321 : 2 Headings coming : Starts --%>
       <%--<tr> 
         Sanju:Issue Id 50201:Added new class so that header display in other browsers also
            <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
            Sanju:Issue Id 50201:End
                <span class="header">Pending Approval of MRF </span>
            </td>
        </tr> 
        <tr>
            <td style="height: 1pt">
                <asp:Label ID="lblMandatory" runat="server" CssClass="txtstyle" Visible="false">Fields marked with <span class="mandatorymark" >*</span> are mandatory.</asp:Label>
                <asp:Label ID="lblConfirmationMessage" runat="server" CssClass="Messagetext" Visible="false">
                </asp:Label>
            </td>
        </tr>--%>
        <%--Poonam Issue : 54321 : Ends--%>
        
    </table>
    <asp:Panel ID="pnlApprove" runat="server" Visible="false" Width="100%">
        <table width="100%">
            <tr>
                <td style="width: 50%" align="left" class="rowbgStyle">
                    <asp:Label ID="lblComment" runat="server" Text="Comment" CssClass="txtstyle">
                    </asp:Label>
                    <%--<label class="mandatorymark">
                        *</label>--%>
                </td>
                <td style="width: 30%" align="left" class="rowbgStyle">
                    <asp:TextBox ID="txtComment" runat="server" CssClass="mandatoryField" Width="300px"
                        ToolTip="Enter Comment" AutoComplete="off" MaxLength="100" EnableViewState="False"></asp:TextBox>
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
                        ToolTip="Enter Reason for Rejection" AutoComplete="off" MaxLength="100" EnableViewState="False"></asp:TextBox>
                </td>
                <td style="width: 20%" align="right" class="rowbgStyle">
                    <asp:Button ID="btnSaveReject" runat="server" Text="Save" CssClass="button" OnClick="btnSaveReject_Click"
                        OnClientClick="return RejectValidate();" />
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
           <%-- Sanju:Issue Id 50201:End--%>
                <span class="header">List of Pending Approve/Reject MRF </span>
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
                <asp:Label ID="lblConfirmationMessage" runat="server" CssClass="Messagetext"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
        <%--Poonam Issue : 54321 : 2 Headings coming : End --%>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gvPendingApprovalOfMrf" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" AllowSorting="true" Width="100%" OnDataBound="gvPendingApprovalOfMrf_DataBound"
                    OnPageIndexChanging="gvPendingApprovalOfMrf_PageIndexChanging" OnRowCreated="gvPendingApprovalOfMrf_RowCreated"
                    OnSorting="gvPendingApprovalOfMrf_Sorting" OnRowCommand="gvPendingApprovalOfMrf_RowCommand">
                    <HeaderStyle CssClass="headerStyle" />
                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                    <RowStyle Height="20px" CssClass="textstyle" />
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblMrfId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MRFId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="0%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="MrfCode" HeaderText="MRF Code" SortExpression="MrfCode"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Resource Name" SortExpression="ResourceName"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="ResourceOnBoard" HeaderText="Start Date" SortExpression="StartDate"
                            DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="7%" />
                        <asp:BoundField DataField="ResourceReleased" HeaderText="End Date" SortExpression="EndDate"
                            DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="7%" />
                        <asp:BoundField DataField="Billing" HeaderText="Billing(%)" SortExpression="Billing"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="Department"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" />
                        <asp:BoundField DataField="RaisedBy" HeaderText="Raised By" SortExpression="RaisedBy"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" />
                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />
                        <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="MRFCTCString" HeaderText="Target CTC(Lks)" SortExpression="TargetCTC"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />
                            
                        <asp:BoundField DataField="SOWNo" HeaderText="SOW NO" SortExpression="SOWNo"
                            HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="SowStartDtString" HeaderText="Sow Start Date" SortExpression="SOWStartDate"
                            DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="Center">
                         </asp:BoundField>
                         <asp:BoundField DataField="SowEndDtString" HeaderText="Sow End Date" SortExpression="SOWEndDate"
                            DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="Center">
                          </asp:BoundField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEMPId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EmployeeId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                            </ItemTemplate>
                            <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFunctionalManager" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FunctionalManager") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        <table class="tablePager" width="100%">
                            <tr>
                                <td align="center">
                                    &lt;&lt; &nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white" CommandName="Previous"
                                        OnCommand="ChangePage" Font-Underline="true" Enabled="False">
                                        Previous
                                    </asp:LinkButton>
                                    <span>Page</span>
                                    <asp:TextBox ID="txtPages" runat="server" OnTextChanged="txtPages_TextChanged" AutoPostBack="true"
                                        Width="21px" MaxLength="3" onpaste="return false;">
                                    </asp:TextBox>
                                    <span>of</span>
                                    <asp:Label ID="lblPageCount" runat="server" ForeColor="white">
                                    </asp:Label>
                                    <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                        OnCommand="ChangePage" Font-Underline="true" Enabled="False">
                                        Next
                                    </asp:LinkButton>
                                    &nbsp;&nbsp; &gt;&gt;
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
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" Visible="false"
                    OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var retVal;
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";

                $('#ctl00_cphMainContent_btnSaveApprove').click(function() {
                    $(this).val("Please Wait..");
                    $(this).attr('disabled', 'disabled');


                });

                $('#ctl00_cphMainContent_btnSaveReject').click(function() {
                    if (RejectValidate()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                    }

                });


            });
        })(jQuery);
    
    setbgToTab('ctl00_tabMRF', 'ctl00_spanMRFPendingApproval');
    
    function $(v){return document.getElementById(v);}
    
    /*Changes made to make "comment" textbox not mandatory*/
    /*
    function ApproveValidate()
    {
        var lblMandatory = $( '<%=lblMandatory.ClientID %>');
        var controlList = $( '<%=txtComment.ClientID %>').id; 
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
      
        if(ValidateControlOnClickEvent(controlList) == false)
        {                                 
           return ValidateControlOnClickEvent(controlList);
        }
    }
    */
    
    function RejectValidate()
    {
        var lblMandatory = $( '<%=lblMandatory.ClientID %>');
        var controlList = $( '<%=txtReject.ClientID %>').id; 
        var reject =  $( '<%=txtReject.ClientID %>').value;
        var txtReject = $( '<%=txtReject.ClientID %>');
        
        if(trim(reject) == "")
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
    
    </script>

</asp:Content>
