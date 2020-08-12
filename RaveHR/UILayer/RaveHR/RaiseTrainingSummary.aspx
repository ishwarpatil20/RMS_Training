<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="RaiseTrainingSummary.aspx.cs" Inherits="RaiseTrainingSummary" Title="Raise Training Summary" %>

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
                                        <a id="control_link" href="javascript:activate_shelf();EnableDisableFilter();" style="color: White;">
                                            <b>Filter</b></a>
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
                                                    <%--OnSelectedIndexChanged="ddlFilterStatus_OnSelectedIndexChanged" AutoPostBack="true"--%>
                                                        Status :
                                                        <asp:DropDownList ID="ddlFilterStatus" runat="server" Width="200px" onchange="HideShow_RequestBy()">
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
                                                        <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_OnClick"
                                                            OnClientClick="return CheckFilter(true);" CssClass="button" Width="70px" Font-Bold="True"
                                                            Font-Size="9pt" />
                                                        &nbsp;
                                                        <asp:Button ID="btnClear" OnClientClick="return ClearFilter()" runat="server" Text="Clear"
                                                            CssClass="button" Width="69px" Font-Bold="True" Font-Size="9pt" OnClick="btnClear_OnClick" />
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
            <table style="width: 100%">
                <tr>
                    <td style="width: 120px">
                        <b>Training Type  : </b>
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="RBLTrainingType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RBLTrainingType_OnSelectedIndexChanged" AutoPostBack="true">
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <br />
           <%-- <asp:Panel ID="CommentPanel" runat="server" Visible="false">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td class="rowbgStyle" style="width: 10%">
                            <asp:Label ID="lblComments" runat="server" Text="Reason for cancel :"></asp:Label>
                            <span runat="server" id="spanComments" style="color: Red">*</span>
                        </td>
                        <td class="rowbgStyle" align="left" style="width: 20%">
                            <asp:TextBox runat="server" ID="txtComments" MaxLength="500" Width="300px" ToolTip="Enter Comments"
                                Text=" "></asp:TextBox>
                        </td>
                        <td class="rowbgStyle" align="left" style="width: 6%">
                            <asp:Button ID="btnSaveComment" runat="server" CssClass="button" Text="Save" Width="80px"
                                OnClientClick="return ValidationCommentPanel();" OnClick="btnSaveComment_OnClick" />
                        </td>
                        <td class="rowbgStyle" align="left">
                            <asp:Button ID="btnCancelComment" runat="server" CssClass="button" Text="Cancel"
                                Width="80px" OnClick="btnCancelComment_OnClick" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>--%>
            <table style="width: 100%">
                <tr>
                    <td>
                        <div style="border: solid 1px black">
                          <%--  <table width="100%">
                                <tr>
                                    <td style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                                        width: 100%;">
                                        <asp:Label ID="Label1" runat="server" Text="View All Training" CssClass="detailsheader"
                                            Style="color: White;"></asp:Label>
                                    </td>
                                </tr>
                            </table>--%>
                            <table style="width: 100%" class="detailsbg" id="tblTechTraining" runat="server"
                                visible="true">
                                <tr>
                                    <%-- <td style="width: 18px">
                                        <asp:ImageButton runat="server" ID="IBTechnicalPlus" ImageUrl="~/Images/plus.JPG"
                                            ToolTip="Approval" Width="15px" Height="15px" OnClick="IBTechnicalPlus_Click" />
                                    </td>--%>
                                    <td style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                                        width: 100%;">
                                        <asp:Label ID="lbltechsoftsummary" runat="server" Text="Technical" CssClass="detailsheader"
                                            Style="color: White;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" runat="server" id="tblTechTraininggv" visible="true">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <asp:GridView ID="gvTechnicalTrainingSummary" runat="server" AutoGenerateColumns="false"
                                            AllowPaging="true" AllowSorting="true" Width="100%" DataKeyNames="RaiseID" OnRowCommand="gvTechnicalTrainingSummary_RowCommand"
                                            OnRowDataBound="gvTechnicalTrainingSummary_OnRowDataBound" OnDataBound="gvTechnicalTrainingSummary_OnDataBound"
                                            OnPageIndexChanging="gvTechnicalTrainingSummary_OnPageIndexChanging" OnSorting="gvTechnicalTrainingSummary_Sorting">
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
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/Images/Edit.gif" ToolTip="Edit"
                                                            Width="20px" Height="20px" CommandName="EditCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                        <asp:HiddenField ID="hfRequestByID" runat="server" Value='<%#Eval("RequestedByID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/Images/Delete.gif" ToolTip="Delete"
                                                            OnClientClick="return DeleteRecord();" Width="20px" Height="20px" CommandName="DeleteCommand"
                                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
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
                                                <asp:TemplateField HeaderText="Close">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgClose" ImageUrl="~/Images/Lock.jpg" ToolTip="Close"
                                                        OnClientClick="return CloseRecord();" Width="20px" Height="20px" CommandName="CloseCommand" 
                                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
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
                           <%-- <table style="width: 100%" class="detailsbg" id="tblSoftTraining" runat="server" visible="true">
                                <tr>
                                    <td style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                                        width: 100%;">
                                        <b Class="detailsheader" Style="color: White;">SoftSkills Training</b>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" runat="server" id="tblSoftTraininggv" visible="true">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <asp:GridView ID="gvSoftSkillsTrainingSummary" runat="server" AutoGenerateColumns="false"
                                            AllowPaging="true" AllowSorting="true" Width="100%" DataKeyNames="RaiseID" OnRowCommand="gvSoftSkillsTrainingSummary_RowCommand"
                                            OnDataBound="gvSoftSkillsTrainingSummary_OnDataBound" OnPageIndexChanging="gvSoftSkillsTrainingSummary_OnPageIndexChanging"
                                            OnRowDataBound="gvSoftSkillsTrainingSummary_OnRowDataBound"
                                            OnSorting="gvSoftSkillsTrainingSummary_Sorting">
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
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/Images/Edit.gif" ToolTip="Edit"
                                                            Width="20px" Height="20px" CommandName="EditCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                        <asp:HiddenField ID="hfRequestByID" runat="server" Value='<%#Eval("RequestedByID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/Images/Delete.gif" ToolTip="Delete"
                                                            OnClientClick="return DeleteRecord();" Width="20px" Height="20px" CommandName="DeleteCommand"
                                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
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
                            <table style="width: 100%" class="detailsbg" id="tblSeminars" runat="server" visible="true">
                                <tr>
                                    <%--<td style="width: 18px">
                                        <asp:ImageButton runat="server" ID="IBSeminars" ImageUrl="~/Images/plus.JPG" ToolTip="Approval"
                                            Width="15px" Height="15px" OnClick="IBSeminars_Click" />
                                    </td>--%>
                                    <td style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                                        width: 100%;">
                                        <b Class="detailsheader" Style="color: White;">Seminar</b>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" runat="server" id="tblSeminarsgv" visible="true">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <asp:GridView ID="gvSeminarsSummary" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                            AllowSorting="true" Width="100%" DataKeyNames="RaiseID" OnRowCommand="gvSeminarsSummary_RowCommand"
                                            OnRowDataBound="gvSeminarsSummary_OnRowDataBound" OnPageIndexChanging="gvSeminarsSummary_OnPageIndexChanging"
                                            OnDataBound="gvSeminarsSummary_OnDataBound" OnSorting="gvSeminarsSummary_Sorting">
                                            <HeaderStyle CssClass="headerStyle" />
                                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                            <RowStyle Height="20px" CssClass="textstyle" />
                                            <Columns>
                                                <%-- <asp:BoundField DataField="SerialNo" HeaderText="Sr.No." SortExpression="SerialNo"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />--%>
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
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/Images/Edit.gif" ToolTip="Edit"
                                                            Width="20px" Height="20px" CommandName="EditCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                        <asp:HiddenField ID="hfRequestByID" runat="server" Value='<%#Eval("RequestedByID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/Images/Delete.gif" ToolTip="Delete"
                                                            OnClientClick="return DeleteRecord();" Width="20px" Height="20px" CommandName="DeleteCommand"
                                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
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
                                                <asp:TemplateField HeaderText="Close">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgClose" ImageUrl="~/Images/Lock.jpg" ToolTip="Close"
                                                            Width="20px" Height="20px"
                                                            OnClientClick="return CloseRecord();" CommandName="CloseCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
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
                            <table style="width: 100%" class="detailsbg" id="tblKSS" runat="server" visible="true">
                                <tr>
                                    <%--<td style="width: 18px">
                                        <asp:ImageButton runat="server" ID="IBKSS" ImageUrl="~/Images/plus.JPG" ToolTip="Approval"
                                            Width="15px" Height="15px" OnClick="IBKSS_Click" />
                                    </td>--%>
                                    <td style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                                        width: 100%;">
                                        <b Class="detailsheader" Style="color: White;">Knowledge Sharing Session</b>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" runat="server" id="tblKSSgv" visible="true">
                                <tr>
                                    <td style="padding-left: 20px">
                                        <asp:GridView ID="gvKSSTrainingSummary" runat="server" AutoGenerateColumns="false"
                                            AllowPaging="true" AllowSorting="true" Width="100%" DataKeyNames="RaiseID" OnRowCommand="gvKSSTrainingSummary_RowCommand"
                                            OnRowDataBound="gvKSSTrainingSummary_OnRowDataBound" OnDataBound="gvgvKSSTrainingSummary_OnDataBound"
                                            OnPageIndexChanging="gvKSSTrainingSummary_OnPageIndexChanging" OnSorting="gvKSSTrainingSummary_Sorting">
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
                                                    <asp:BoundField DataField="Status" HeaderText="Status" Visible="false" SortExpression="Status" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/Images/Edit.gif" ToolTip="Edit"
                                                            Width="20px" Height="20px" CommandName="EditCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/Images/Delete.gif" ToolTip="Delete"
                                                            OnClientClick="return DeleteRecord();" Width="20px" Height="20px" CommandName="DeleteCommand"
                                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
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
                                                <%--<asp:TemplateField HeaderText="Close" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgCancel" ImageUrl="~/Images/close.gif" ToolTip="Close"
                                                            Width="20px" Height="20px" CommandName="CloseCommand" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RaiseID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>
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
                                <tr>
                                    <td>
                                        <asp:Label ID="lblkss" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinny.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    
    <script type="text/javascript" language="javascript">
        var TechnicalTrainingID = 1207;
        var SoftSkillsTrainingID = 1208;
        var KSSID = 1210;
        var SeminarsID = 1209;
        var Rejected = "Rejected";
        var Deleted = "Deleted";
        var Select = "Select";

        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
            });
        })(jQuery);

        function openWindow(URLParameter) {
            jQuery.modalDialog.create({ url: URLParameter, maxWidth: 1000 }).open();
        }
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        function $(v) {
            return document.getElementById(v);
        }
        function HideShow_RequestBy() {
            var StatusSelect = document.getElementById('<%=ddlFilterStatus.ClientID %>');
            var val_StatusVal = StatusSelect.options[StatusSelect.selectedIndex].text;
            if (val_StatusVal == Rejected || val_StatusVal == Deleted) {

                var RequestBySelect = document.getElementById('<%=ddlFilterRequestBy.ClientID %>');
                var val_RequestVal = RequestBySelect.options[RequestBySelect.selectedIndex].text;
                if (val_RequestVal != Select) {
                    $('<%= ddlFilterRequestBy.ClientID %>').disabled = true;
                }
            }
            else {
                $('<%= ddlFilterRequestBy.ClientID %>').disabled = false;
            }
        }

        function EnableDisableFilter() {
            var StatusSelect = document.getElementById('<%=ddlFilterStatus.ClientID %>');
            var val_StatusVal = StatusSelect.options[StatusSelect.selectedIndex].text;
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
            
            if (val_StatusVal == Rejected || val_StatusVal == Deleted) {
                $('<%= ddlFilterRequestBy.ClientID %>').disabled = true;
            }
            else {
                $('<%= ddlFilterRequestBy.ClientID %>').disabled = false;
            }
        }

//        function ValidationCommentPanel() {
//            var Comment = $('<%=ddlFilterRequestBy.ClientID %>').value;
//            if (Comment.trim() == "") {
//                $('<%=lblMandatory.ClientID %>').innerText = "Please provide the reason for cancel.";
//                return false;
//            }
//            return true;
//        }

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
            ddlStatus.selectedIndex = 0;
            ddlRequestBy.selectedIndex = 0;
            ddlQuarter.selectedIndex = 0;

            return false;
        }
        function DeleteRecord() {
            var conf = confirm("Are you sure you want to delete this training request?");
            if (conf == true) {
                return true;
            }
            return false;
        }

        function CloseRecord() {
            var conf = confirm("Are you sure you want to close this training request?");
            if (conf == true) {
                return true;
            }
            return false;
        }
    </script>

</asp:Content>
