<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="RecruitmentSummary.aspx.cs" Inherits="RecruitmentSummary" Title="Recruitment Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript" src="JavaScript/FilterPanel.js">
    </script>

    <script type="text/javascript">
    
    setbgToTab('ctl00_tabRecruitment', 'ctl00_spanRecruitmentSummary');
    
    function $(v)
    {
       return document.getElementById(v);
    }
    
    //This function gives an alert message when "Filter" button is clicked without selecting any Filter criteria.
    function CheckFilter(isButtonClicked)
    {
        var ddlDepartment = $('<%= ddlDepartment.ClientID %>');
        var ddlProjectName = $('<%= ddlProjectName.ClientID%>');
        var ddlRole = $('<%= ddlRole.ClientID%>');
        var ddlStatus = $('<%= ddlStatus.ClientID%>');

        if (ddlDepartment != null && ddlProjectName != null && ddlRole != null)
        {
            if (ddlDepartment.value == "SELECT" && ddlProjectName.value == "SELECT" && ddlRole.value == "SELECT" && ddlStatus.value == "SELECT")  
            {
                 if(isButtonClicked)
                       alert("Please select or enter any criteria, to proceed with filter.");
                        
                    return false;
            }
        }
    }
    
     //This function clears the filtering criteria and sets the value of the dropdown to "SELECT".
    function ClearFilter()
    {
        var ddlDepartment = $('<%= ddlDepartment.ClientID %>');
        var ddlProjectName = $('<%= ddlProjectName.ClientID%>');
        var ddlRole = $('<%= ddlRole.ClientID%>');        
        
        //If Department value is not "SELECT" than set the value to "SELECT" and disable ProjectName and
        //Role dropdown.
        if(ddlDepartment.value != "SELECT")
        {
            ddlDepartment.selectedIndex = 0;
            ddlProjectName.selectedIndex = 0;
            ddlRole.selectedIndex = 0;            
            
            ddlProjectName.disabled = true;
            ddlRole.disabled = true;
        }                                       
        
        return false;
    }
    
    </script>

    <table width="100%">
        <tr>
         <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <span class="header">Recruitment Summary</span>
            </td>
              <%--   Sanju:Issue Id 50201:End--%>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext" Visible="false">
                </asp:Label>
            </td>
            <td style="height: 19px; width: 280px;">
                <div id="shelf" class="filter_main" style="width: 280px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                      <%--   Sanju:Issue Id 50201:Changed cursor property--%>
                        <tr style="cursor: pointer;" onclick="javascript:activate_shelf();">
                          <%--   Sanju:Issue Id 50201:End--%>
                            <td class="filter_title header_filter_title" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr= '#5C7CBD' , startColorstr= '#12379A' , gradientType= '1' );">
                                <a id="control_link" href="javascript:activate_shelf();" style="color: White; font-family: Verdana;
                                    font-size: 9pt;"><b>Filter</b></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                              <%--   Sanju:Issue Id 50201:Alignment issue resolved--%>
                                <div id="shelf_contents" class="filter_content" style="border: solid 1px black; text-align: center;
                                    width: 274px;">
                                      <%--   Sanju:Issue Id 50201:End--%>
                                    <table style="text-align: left; left: 525px; top: 282px; font-family: Verdana;" cellpadding="1">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upFilter" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblDepartment" runat="server" Text="Department" Font-Size="9pt">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                             <%--   Sanju:Issue Id 50201:Alignment issue resolved--%>
                                                                <td style="width: 280px;">
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" ToolTip="Select Department"
                                                                        Width="266px" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                     <%--   Sanju:End--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" Font-Size="9pt">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                 <%--   Sanju:Issue Id 50201:Alignment issue resolved--%>
                                                                    <asp:DropDownList ID="ddlProjectName" runat="server" ToolTip="Select Project Name"
                                                                        Width="266px">
                                                                  <%--   Sanju:End--%>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblRole" runat="server" Text="Role" Font-Size="9pt">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                 <%--   Sanju:Issue Id 50201:Alignment issue resolved--%>
                                                                    <asp:DropDownList ID="ddlRole" runat="server" ToolTip="Select Role" Width="266px"> 
                                                             <%--   Sanju:Issue Id 50201:End--%>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblStatus" runat="server" Text="Status" Font-Size="9pt">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                 <%--   Sanju:Issue Id 50201:Alignment issue resolved--%>
                                                                    <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Select Status" Width="266px">
                                                             <%--   Sanju:Issue Id 50201:End--%>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td style="width: 280px;" align="center">
                                                <br />
                                                <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="button" Width="70px"
                                                    Font-Bold="True" Font-Size="9pt" OnClick="btnFilter_Click" OnClientClick="return CheckFilter(true);" />
                                                &nbsp;
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClientClick="return ClearFilter()"
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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gvRecruitmentSummary" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" AllowSorting="true" Width="100%" DataKeyNames="CandidateId"
                    OnSorting="gvRecruitmentSummary_Sorting" OnPageIndexChanging="gvRecruitmentSummary_PageIndexChanging"
                    OnDataBound="gvRecruitmentSummary_DataBound" OnRowCreated="gvRecruitmentSummary_RowCreated"
                    OnRowDataBound="gvRecruitmentSummary_RowDataBound" OnRowCommand="gvRecruitmentSummary_RowCommand">
                    <HeaderStyle CssClass="headerStyle" />
                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                    <RowStyle Height="20px" CssClass="textstyle" />
                    <Columns>
                        <asp:BoundField DataField="CandidateId" HeaderText="CID" Visible="false" />
                        <asp:BoundField DataField="MrfCode" HeaderText="MRF Code" SortExpression="MrfCode"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="16%" />
                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="ResourceName" HeaderText="Candidate Name" SortExpression="ResourceName"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="ExpectedJoiningDate" HeaderText="Expected Date Of Joining"
                            SortExpression="ExpectedDateOfJoining" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="14%" />
                        <asp:BoundField DataField="RecruitmentManager" HeaderText="Recruiter Name" SortExpression="RecruitmentManager"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="12%" />
                        <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department"
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgCandidateNotJoining" ImageUrl="~/Images/reject.jpg"
                                    ToolTip="Candidate not Joining" Width="20px" Height="20px" CommandName="CandidateNotJoiningCommand"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"CandidateId") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
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
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" Visible="false"
                    OnClick="btnRemoveFilter_Click" CssClass="button" />
            </td>
        </tr>
    </table>
</asp:Content>
