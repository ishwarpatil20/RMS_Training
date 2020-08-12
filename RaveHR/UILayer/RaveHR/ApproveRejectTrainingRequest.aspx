<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="ApproveRejectTrainingRequest.aspx.cs" Inherits="ApproveRejectTrainingRequest"
    Title="Approve/Reject Training Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript" src="JavaScript/FilterPanel.js">
    </script>

    <asp:UpdatePanel ID="UPRaiseTrainingSummary" runat="server">
        <ContentTemplate>
         <table style="width: 100%">
                <tr>
                    <td style="height: 20px">
                        <asp:Label ID="lblConfirmMessage" CssClass="Messagetext" runat="server"></asp:Label>
                        <asp:Label ID="lblMandatory" Style="color: Red" runat="server"></asp:Label>
                    </td>
                    <td style="height: 20px; width: 240px;">
                        <div id="shelf" class="filter_main" style="width: 240px;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr style="cursor: hand;" onclick="javascript:activate_shelf(); EnableDisableFilter();">
                                    <td class="filter_title" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr= '#5C7CBD' , startColorstr= '#12379A' , gradientType= '1' );">
                                        <a id="control_link" href="javascript:activate_shelf();EnableDisableFilter();" style="color: White;"><b>Filter</b></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="shelf_contents" class="filter_content" style="border: solid 1px black; text-align: center;
                                            width: 230px;">
                                            <table style="text-align: left;" cellpadding="2">
                                                <tr>
                                                    <td>
                                                        Training Type :
                                                        <%--OnSelectedIndexChanged="ddlFilterTrainingType_OnSelectedIndexChanged"--%>
                                                        <asp:DropDownList ID="ddlFilterTrainingType" runat="server" Width="200px" onchange="EnableDisableFilter()">
                                                            <asp:ListItem Text="Technical"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Priority :
                                                    <asp:DropDownList ID="ddlFilterPriority" runat="server" Width="200px">
                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="High"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Medium"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Low"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Status :
                                                    
                                                        <asp:DropDownList ID="ddlFilterStatus" runat="server" Width="200px" Enabled="false">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Requested By :
                                                    <asp:DropDownList ID="ddlFilterRequestBy" runat="server" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Quarter :
                                                        <asp:DropDownList ID="ddlFilterQuarter" runat="server" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td style="width: 280px;">
                                                    <br />
                                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_OnClick" OnClientClick="return CheckFilter(true);"
                                                        CssClass="button" Width="70px" Font-Bold="True" Font-Size="9pt" />
                                                    &nbsp;
                                                    <asp:Button ID="btnClear" OnClientClick="return ClearFilter()" runat="server" Text="Clear"
                                                        CssClass="button" Width="69px" Font-Bold="True" Font-Size="9pt" />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="CommentPanel" runat="server" Visible="false">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="rowbgStyle" style="width: 10%">
                                    <asp:Label ID="lblComments" runat="server" Text="Comments (if any) :"></asp:Label>
                                    <span runat="server" id="spanComments" style="color: Red">*</span>
                                </td>
                                <td class="rowbgStyle" align="left" style="width:20%">
                                    <asp:TextBox runat="server" ID="txtComments" MaxLength="500" Width="300px" ToolTip="Enter Comments"
                                        Text=" "></asp:TextBox>
                                </td>
                                <td class="rowbgStyle" align="left" style="width:6%">
                                    <asp:Button ID="btnSaveComment" runat="server" CssClass="button" Text="Save" Width="80px"
                                        OnClick="btnSaveComment_OnClick" />
                                </td>
                                <td class="rowbgStyle" align="left">
                                    <asp:Button ID="btnCancelComment" runat="server" CssClass="button" Text="Cancel"
                                        Width="80px" OnClick="btnCancelComment_OnClick" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td>
                        <div style="border: solid 1px black">
                            <table width="100%">
                                <tr>
                                    <td style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                                        width: 100%;">
                                        <asp:Label ID="Label1" runat="server" Text="Approve/Reject Training Request" CssClass="detailsheader"
                                            Style="color: White;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%" class="detailsbg" id="tblTechSoftTraining" runat="server" visible="true">
                                <tr>
                                    <%--<td style="width: 18px">
                                        <asp:ImageButton runat="server" ID="IBTechnicalPlus" ImageUrl="~/Images/plus.JPG"
                                            ToolTip="Approval" Width="15px" Height="15px"  OnClick="IBTechnicalPlus_Click"/>
                                    </td>--%>
                                    <td align="left">
                                        <b>Technical / Soft Skill</b>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" runat="server" id="tblTechnicalTraininggv" visible="true">
                                <tr>
                                    <td style="padding-left: 20px">
                                    <asp:GridView ID="gvApproveRejectTechnicalTraining" runat="server" AutoGenerateColumns="false"
                                            OnRowCommand="gvApproveRejectTechnicalTraining_RowCommand"
                                            OnRowDataBound="gvApproveRejectTechnicalTraining_OnRowDataBound"
                                            OnDataBound="gvApproveRejectTechnicalTraining_OnDataBound"
                                            OnPageIndexChanging="gvApproveRejectTechnicalTraining_OnPageIndexChanging"
                                            OnSorting="gvApproveRejectTechnicalTraining_Sorting"
                                            AllowPaging="true" AllowSorting="true" Width="100%" DataKeyNames="RaiseID">
                                            <HeaderStyle CssClass="headerStyle" />
                                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                            <RowStyle Height="20px" CssClass="textstyle" />
                                            <Columns>
                                               <%-- <asp:BoundField DataField="SerialNo" HeaderText="Sr.No." SortExpression="SerialNo"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" />--%>
                                                <asp:BoundField DataField="TrainingName" HeaderText="Name" SortExpression="TrainingName"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Quarter" HeaderText="Quarter" SortExpression="Quarter"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="RequestedBy" HeaderText="Requested By" SortExpression="RequestedBy"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Raised On" SortExpression="CreatedDate"
                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="16%" />
                                                <asp:TemplateField HeaderText="Approve">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgApprove" ImageUrl="~/Images/Approve.GIF" ToolTip="Approve"
                                                            Width="20px" Height="20px" CommandName="ApproveCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                        <asp:HiddenField ID="hfRequestByID" runat="server" Value='<%#Eval("RequestedByID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reject">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgReject" ImageUrl="~/Images/reject.GIF" ToolTip="Reject"
                                                            Width="20px" Height="20px" CommandName="RejectCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgView" ImageUrl="~/Images/View.GIF" ToolTip="View"
                                                            Width="20px" Height="20px" />
                                                        <asp:HiddenField ID="hfgvRaiseID" runat="server" Value='<%#Eval("RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
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
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <%-- <table style="width: 100%" class="detailsbg">
                                <tr>
                                    <td style="width: 18px">
                                        <asp:ImageButton runat="server" ID="IBSoftPlus" ImageUrl="~/Images/plus.JPG" ToolTip="Approval"
                                            Width="15px" Height="15px" OnClick="IBSoftPlus_Click" />
                                    </td>
                                    <td align="left">
                                        <b>Soft-Skills Training</b>
                                    </td>
                                </tr>
                            </table>
                             <table style="width: 100%;" runat="server" id="tblSoftSkillTraining" visible="true">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <asp:GridView ID="gvApproveRejectSoftSkillTrainingSummary" runat="server" AutoGenerateColumns="false"
                                            AllowPaging="true" AllowSorting="true" Width="100%" DataKeyNames="RaiseID" OnRowCommand="gvApproveRejectSoftSkillTrainingSummary_RowCommand"
                                            OnDataBound="gvApproveRejectSoftSkillTrainingSummary_OnDataBound" OnPageIndexChanging="gvApproveRejectSoftSkillTrainingSummary_OnPageIndexChanging"
                                            OnRowDataBound="gvApproveRejectSoftSkillTrainingSummary_OnRowDataBound"
                                            OnSorting="gvApproveRejectSoftSkillTrainingSummary_Sorting">
                                            <HeaderStyle CssClass="headerStyle" />
                                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                            <RowStyle Height="20px" CssClass="textstyle" />
                                            <Columns>
                                                <asp:BoundField DataField="TrainingName" HeaderText="Name" SortExpression="TrainingName"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Quarter" HeaderText="Quarter" SortExpression="Quarter"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="RequestedBy" HeaderText="Requested By" SortExpression="RequestedBy"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Raised On" SortExpression="CreatedDate"
                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="16%" />
                                                <asp:TemplateField HeaderText="Approve">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgApprove" ImageUrl="~/Images/Approve.GIF" ToolTip="Approve"
                                                            Width="20px" Height="20px" CommandName="ApproveCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                        <asp:HiddenField ID="hfRequestByID" runat="server" Value='<%#Eval("RequestedByID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reject">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgReject" ImageUrl="~/Images/reject.GIF" ToolTip="Reject"
                                                            Width="20px" Height="20px" CommandName="RejectCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgView" ImageUrl="~/Images/View.GIF" ToolTip="View"
                                                            Width="20px" Height="20px" />
                                                        <asp:HiddenField ID="hfgvRaiseID" runat="server" Value='<%#Eval("RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table class="tablePager" width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            &lt;&lt; &nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white" CommandName="Previous"
                                                                OnCommand="SoftSkillsChangePage" Font-Underline="true" Enabled="False">
                                        Previous
                                                            </asp:LinkButton>
                                                            <span>Page</span>
                                                            <asp:TextBox ID="txtPages" runat="server" AutoPostBack="true" Width="21px" MaxLength="3"
                                                                OnTextChanged="SoftSkillstxtPages_TextChanged" onpaste="return false;">
                                                            </asp:TextBox>
                                                            <span>of</span>
                                                            <asp:Label ID="lblPageCount" runat="server" ForeColor="white">
                                                            </asp:Label>
                                                            <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                                                OnCommand="SoftSkillsChangePage" Font-Underline="true" Enabled="False">
                                        Next
                                                            </asp:LinkButton>
                                                            &nbsp;&nbsp; &gt;&gt;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>--%>
                             <table style="width: 100%" class="detailsbg" id="tblSeminarsTraining" runat="server" visible="true">
                                <tr>
                                    <%--<td style="width: 18px">
                                        <asp:ImageButton runat="server" ID="IBSeminars" ImageUrl="~/Images/plus.JPG" ToolTip="Approval"
                                            Width="15px" Height="15px" OnClick="IBSeminars_Click"/>
                                    </td>--%>
                                    <td align="left">
                                        <b>Seminar</b>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" runat="server" id="tblSeminarsTraininggv" visible="true">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <asp:GridView ID="gvApproveRejectSeminarsSummary" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                            AllowSorting="true" Width="100%" DataKeyNames="RaiseID" OnRowCommand="gvApproveRejectSeminarsSummary_RowCommand"
                                            OnRowDataBound="gvApproveRejectSeminarsSummary_OnRowDataBound" OnPageIndexChanging="gvApproveRejectSeminarsSummary_OnPageIndexChanging"
                                            OnDataBound="gvApproveRejectSeminarsSummary_OnDataBound"
                                            OnSorting="gvApproveRejectSeminarsSummary_Sorting">
                                            <HeaderStyle CssClass="headerStyle" />
                                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                            <RowStyle Height="20px" CssClass="textstyle" />
                                            <Columns>
                                                <asp:BoundField DataField="SeminarsName" HeaderText="Name" SortExpression="SeminarsName"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:dd/MM/yyyy}"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="RequestedBy" HeaderText="Requested By" SortExpression="RequestedBy"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Raised On" SortExpression="CreatedDate"
                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="16%" />
                                                <asp:TemplateField HeaderText="Approve">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgApprove" ImageUrl="~/Images/Approve.gif" ToolTip="Approve"
                                                            Width="20px" Height="20px" CommandName="ApproveCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                        <asp:HiddenField ID="hfRequestByID" runat="server" Value='<%#Eval("RequestedByID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reject">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgReject" ImageUrl="~/Images/reject.gif" ToolTip="Reject"
                                                            Width="20px" Height="20px" CommandName="RejectCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgView" ImageUrl="~/Images/View.GIF" ToolTip="View"
                                                            Width="20px" Height="20px" />
                                                        <asp:HiddenField ID="hfgvRaiseID" runat="server" Value='<%#Eval("RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table class="tablePager" width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            &lt;&lt; &nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white" CommandName="Previous"
                                                                OnCommand="SeminarsChangePage" Font-Underline="true" Enabled="False">
                                        Previous
                                                            </asp:LinkButton>
                                                            <span>Page</span>
                                                            <asp:TextBox ID="txtPages" OnTextChanged="SeminarstxtPages_TextChanged" runat="server"
                                                                AutoPostBack="true" Width="21px" MaxLength="3" onpaste="return false;">
                                                            </asp:TextBox>
                                                            <span>of</span>
                                                            <asp:Label ID="lblPageCount" runat="server" ForeColor="white">
                                                            </asp:Label>
                                                            <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                                                OnCommand="SeminarsChangePage" Font-Underline="true" Enabled="False">
                                        Next
                                                            </asp:LinkButton>
                                                            &nbsp;&nbsp; &gt;&gt;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                             <table style="width: 100%" class="detailsbg" id="tblKSSTraining" runat="server" visible="true">
                                <tr>
                                    <%--<td style="width: 18px">
                                        <asp:ImageButton runat="server" ID="IBKSS" ImageUrl="~/Images/plus.JPG" ToolTip="Approval"
                                            Width="15px" Height="15px" OnClick="IBKSS_Click"/>
                                    </td>--%>
                                    <td align="left">
                                        <b>Knowledge Sharing Session</b>
                                    </td>
                                </tr>
                            </table>
                             <table style="width: 100%;" runat="server" id="tblKSSTraininggv" visible="true">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <asp:GridView ID="gvApproveRejectKSSTrainingSummary" runat="server" AutoGenerateColumns="false"
                                            AllowPaging="true" AllowSorting="true" Width="100%" DataKeyNames="RaiseID" OnRowCommand="gvApproveRejectKSSTrainingSummary_RowCommand"
                                            OnRowDataBound="gvApproveRejectKSSTrainingSummary_OnRowDataBound" OnDataBound="gvApproveRejectKSSTrainingSummary_OnDataBound"
                                            OnPageIndexChanging="gvApproveRejectKSSTrainingSummary_OnPageIndexChanging"
                                            OnSorting="gvApproveRejectKSSTrainingSummary_Sorting">
                                            <HeaderStyle CssClass="headerStyle" />
                                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                            <RowStyle Height="20px" CssClass="textstyle" />
                                            <Columns>
                                                <%--<asp:BoundField DataField="SerialNo" HeaderText="Sr.No." SortExpression="SerialNo"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />--%>
                                                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Topic" HeaderText="Topic" SortExpression="Topic" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:dd/MM/yyyy}"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Raised On" SortExpression="CreatedDate"
                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="16%" />
                                               <%-- <asp:TemplateField HeaderText="Approve">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgApprove" ImageUrl="~/Images/Approve.gif" ToolTip="Approve"
                                                            Width="20px" Height="20px" CommandName="ApproveCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reject">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgReject" ImageUrl="~/Images/reject.GIF" ToolTip="Reject"
                                                            Width="20px" Height="20px" CommandName="RejectCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgView" ImageUrl="~/Images/View.GIF" ToolTip="View"
                                                            Width="20px" Height="20px" />
                                                        <asp:HiddenField ID="hfgvRaiseID" runat="server" Value='<%#Eval("RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table class="tablePager" width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            &lt;&lt; &nbsp;&nbsp;
                                                            <asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white" CommandName="Previous"
                                                                OnCommand="KSSChangePage" Font-Underline="true" Enabled="False">
                                        <%--OnCommand="ChangePage" --%>
                                        Previous
                                                            </asp:LinkButton>
                                                            <span>Page</span>
                                                            <asp:TextBox ID="txtPages" runat="server" AutoPostBack="true" Width="21px" MaxLength="3"
                                                                OnTextChanged="KSStxtPages_TextChanged" onpaste="return false;">
                                                            </asp:TextBox>
                                                            <span>of</span>
                                                            <asp:Label ID="lblPageCount" runat="server" ForeColor="white">
                                                            </asp:Label>
                                                            <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                                                OnCommand="KSSChangePage" Font-Underline="true" Enabled="False">
                                        <%--OnCommand="ChangePage" --%>
                                        Next
                                                            </asp:LinkButton>
                                                            &nbsp;&nbsp; &gt;&gt;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                             
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        var TechnicalTrainingID = 1207;
        var SoftSkillsTrainingID = 1208;
        var KSSID = 1210;
        var SeminarsID = 1209;
        
        function $(v) {
            return document.getElementById(v);
        }

        function EnableDisableFilter() {
            var ddlTrainingTypeID = $('<%= ddlFilterTrainingType.ClientID %>').value;
            
            $('<%= ddlFilterPriority.ClientID %>').disabled = false;
            $('<%= ddlFilterQuarter.ClientID %>').disabled = false;
            $('<%= ddlFilterRequestBy.ClientID %>').disabled = false;
            
            if (ddlTrainingTypeID == KSSID) {
                $('<%= ddlFilterPriority.ClientID %>').disabled = true;
                $('<%= ddlFilterQuarter.ClientID %>').disabled = true;
                $('<%= ddlFilterRequestBy.ClientID %>').disabled = true;
            }
            else if (ddlTrainingTypeID == SeminarsID) {
                $('<%= ddlFilterPriority.ClientID %>').disabled = true;
                $('<%= ddlFilterQuarter.ClientID %>').disabled = true;
            }
        }
        
        function CheckFilter(isButtonClicked) {
            var ddlTrainingType = $('<%= ddlFilterTrainingType.ClientID %>').value;
            var ddlPriority = $('<%= ddlFilterPriority.ClientID %>').value;
            var ddlStatus = $('<%= ddlFilterStatus.ClientID %>').value;
            var ddlRequestBy = $('<%= ddlFilterRequestBy.ClientID %>').value;
            var ddlQuarter = $('<%= ddlFilterQuarter.ClientID %>').value;
            /*if (ddlTrainingType.toUpperCase() == "SELECT" && ddlPriority.toUpperCase() == "0" && ddlStatus.toUpperCase() == "SELECT" && ddlRequestBy.toUpperCase() == "SELECT" && ddlQuarter.toUpperCase() == "SELECT") {
                if (isButtonClicked)
                    alert("Please select or enter any criteria, to proceed with filter.");

                return false;
            }*/
        }

        function ClearFilter() {
        
            $('<%= ddlFilterPriority.ClientID %>').disabled = false;
            $('<%= ddlFilterQuarter.ClientID %>').disabled = false;
            $('<%= ddlFilterRequestBy.ClientID %>').disabled = false;
            
            var ddlTrainingType = $('<%= ddlFilterTrainingType.ClientID %>');
            var ddlPriority = $('<%= ddlFilterPriority.ClientID %>');
            var ddlStatus = $('<%= ddlFilterStatus.ClientID %>');
            var ddlRequestBy = $('<%= ddlFilterRequestBy.ClientID %>');
            var ddlQuarter = $('<%= ddlFilterQuarter.ClientID %>');

            ddlTrainingType.selectedIndex = 0;
            ddlPriority.selectedIndex = 0;
            ddlStatus.selectedIndex = 1;
            ddlRequestBy.selectedIndex = 0;
            ddlQuarter.selectedIndex = 0;

            return false;
        }
        </script>
</asp:Content>
