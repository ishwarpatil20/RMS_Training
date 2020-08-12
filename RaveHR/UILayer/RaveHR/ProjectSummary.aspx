<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ProjectSummary.aspx.cs" Inherits="ProjectSummary" Title="Resource Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script language="javascript" src="JavaScript/CommonValidations.js" type="text/javascript">
    </script>

    <script type="text/javascript" src="JavaScript/FilterPanel.js">
    </script>

    <script type="text/javascript">   
    /// <summary>Set background to tabs</summary>
    //fn_MenuTab('Projects', 'ctl00_divProjectSummary');
    setbgToTab('ctl00_tabProject', 'ctl00_divProjectSummary');
    
    function CheckFilter(isButtonClicked)
    {
        var txtName = document.getElementById('<% = txtProjectName.ClientID %>');
        var ddlClientName = document.getElementById('<% = ddlClientName.ClientID %>');
        //For rolePreSales this drop down is not displayed. So no check required
        if(document.getElementById('<% = ddlStatus.ClientID %>') != null)
        {
            var ddlStatus = document.getElementById('<% = ddlStatus.ClientID %>');
        }
        //For rolePM this drop down is not displayed. So no check required
        /*if(document.getElementById('<% = ddlWorkFlowStatus.ClientID %>') != null)
        {            
            var ddlWorkFlowStatus = document.getElementById('<% = ddlWorkFlowStatus.ClientID %>');
        }*/        
        var btnFilter = document.getElementById('<% = btnFilter.ClientID %>');
        //For roleRPG all the drop down displayed. So check required for all the fields
        //if(document.getElementById('<% = ddlWorkFlowStatus.ClientID %>') != null && document.getElementById('<% = ddlStatus.ClientID %>') != null)
        if(document.getElementById('<% = ddlStatus.ClientID %>') != null)
        {
            //if ((txtName != null) && (ddlClientName != null) && (ddlStatus != null)&& (ddlWorkFlowStatus != null))
            if ((txtName != null) && (ddlClientName != null) && (ddlStatus != null))
            {    
                        
                //if((trim(txtName.value) == "") && (ddlClientName.value == "Select") && (ddlStatus.value == "Select") && (ddlWorkFlowStatus.value == "Select"))
                if((trim(txtName.value) == "") && (ddlClientName.value == "Select") && (ddlStatus.value == "Select"))
                {
                    txtName.value = "";
                    
                    if(isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");
                        
                    return false;
                }
            }
        }
        
        //For rolePreSales ProjectStatus dropdown is not displayed. So check is required for remaining 3 fields.
        if(document.getElementById('<% = ddlStatus.ClientID %>') == null)
        {
            //if ((txtName != null) && (ddlClientName != null) && (ddlWorkFlowStatus != null))
            if ((txtName != null) && (ddlClientName != null))
            {    
                        
                //if((trim(txtName.value) == "") && (ddlClientName.value == "Select") && (ddlWorkFlowStatus.value == "Select"))
                if((trim(txtName.value) == "") && (ddlClientName.value == "Select"))
                {
                    txtName.value = ""; 
                    
                    if(isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");
                        
                    return false;
                }
            }
        }
        
        //For rolePM WorkFlowStatus dropdown is not displayed. So check is required for remaining 3 fields.
        //if(document.getElementById('<% = ddlWorkFlowStatus.ClientID %>') == null)
        //{
             if ((txtName != null) && (ddlClientName != null) && (ddlStatus != null))
            {    
                        
                if((trim(txtName.value) == "") && (ddlClientName.value == "Select") && (ddlStatus.value == "Select"))
                {
                    txtName.value = "";
                    
                    if(isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");
                        
                    return false;
                }
            }
        }//
        
    //}
    </script>

    <script type="text/javascript">       
        
        function isNumberKey(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57) )
                return false;
                
             return true;
        }
        
        function ClearFilter()
        {                                    
            var txtProjectName = document.getElementById('<%=txtProjectName.ClientID %>');
            var ddlClientName = document.getElementById('<%=ddlClientName.ClientID %>');
            var ddlStatus = document.getElementById('<%=ddlStatus.ClientID %>');
            //var ddlWorkFlowStatus = document.getElementById('<%=ddlWorkFlowStatus.ClientID %>');
            
            txtProjectName.value="";
            txtProjectName.focus();
            
            ddlClientName.selectedIndex =0;
            
            if(ddlStatus != null)
            {
                ddlStatus.selectedIndex=0;
            }
            /*if(ddlWorkFlowStatus != null)
            {
                ddlWorkFlowStatus.selectedIndex = 0;
                ddlWorkFlowStatus.disabled = false; 
            }*/            
            return false;
        }
        
        function DenySpecialChar()
        {
            if(event.keyCode == 13)
            {
                var txtProjectName = document.getElementById('<%=txtProjectName.ClientID %>');
                if(txtProjectName.value == "")
                {
                    CheckFilter(false);
                }
                else
                {
                    var btnFilter = document.getElementById('<%=btnFilter.ClientID %>');                
                    btnFilter.click();
                }
            }
            if ((event.keyCode > 32 && event.keyCode < 45) || (event.keyCode > 46 && event.keyCode < 48) || (event.keyCode > 57 && event.keyCode < 65) || (event.keyCode > 90 && event.keyCode < 97) || (event.keyCode > 122 && event.keyCode < 127))
             event.returnValue = false;
            
        }
        
        function hideProejctStage()
        {
            var ddlStatus = document.getElementById('<%=ddlStatus.ClientID %>');
            //var ddlWorkFlowStatus = document.getElementById('<%=ddlWorkFlowStatus.ClientID %>');            
            
            /*if (ddlStatus != null)
            { 
                if(ddlStatus.options[ddlStatus.selectedIndex].text == "Deleted")
                {
                    ddlWorkFlowStatus.selectedIndex = 0;
                    ddlWorkFlowStatus.disabled = true;                
                }
                else
                {
                    ddlWorkFlowStatus.disabled = false; 
                }
            }*/
        }
        
    </script>

    <script type="text/javascript">
        /// <summary>
        /// JS function to validate view details based on event
        /// </summary>
        /// <author>Prashant M</author>
        /// <CreatedDate>05th Aug 2009</CreatedDate>
        /// <LastModifiedDate>05th Aug 2009</LastModifiedDate>
        var isbutton = false;
        function buClick(){isbutton = true;}
       
        function sendWindow(url)
        {if (isbutton == true){isbutton = false;}
        else{location.href = url;}
        }
    </script>

    <div style="font-family: Verdana; font-size: 9pt;">
        <table width="100%">
            <tr>
            <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
                <td align="center"  class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
             <%--   Sanju:Issue Id 50201:End--%>
                    <%--<span style="color: White; vertical-align: middle; font-size: 9pt; font-weight: bold">
                        Project Summary </span>--%>
                    <span class="header">List of Projects</span>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <%--<asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>--%>
                    <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext" Visible="false"></asp:Label>
                </td>
                <td style="height: 19px; width: 200px;">
                    <div id="shelf" class="filter_main" style="width: 196px;">
                        <table width="100%" cellpadding="0" cellspacing="0">
                           <%--  Sanju:Issue Id 50201:Changed cursor to pointer--%>
                            <tr style="cursor: pointer;" onclick="javascript:activate_shelf();">
                          <%--  Sanju:Issue Id 50201:End--%>
                        <%--  Sanju:Issue Id 50201: Added css to filter header--%>
                                <td class="filter_title header_filter_title" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr= '#5C7CBD' , startColorstr= '#12379A' , gradientType= '1' );">
                           <%--     Sanju:Issue Id 50201:End--%>
                                    <a id="control_link" href="javascript:activate_shelf();" style="color: White;"><b>Filter</b></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="shelf_contents" class="filter_content" style="border: solid 1px black; text-align: center; width: 189px;">
                                        <table style="text-align: left; left: 541px; top: 282px;" cellpadding="1">
                                        
                                             <tr>
                                                <td style="width: 155px;">
                                                    <asp:Label ID="lblName" runat="server" Text="Project Name" Font-Size="9pt"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 155px">
                                                    <asp:TextBox ID="txtProjectName" runat="server" ToolTip="Enter Project Name" Width="149px" MaxLength="30" onkeypress="return DenySpecialChar();"></asp:TextBox><br />
                                                </td>
                                            </tr>
                                            <tr>
                                            
                                                <td style="width: 155px;">
                                               
                                                    <asp:Label ID="lblClient" runat="server" Text="Client Name" Font-Size="9pt"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 155px">
                                                    <asp:DropDownList ID="ddlClientName" runat="server" Width="155px" ToolTip="Select Client Name">
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="trlblStatus" runat="server" visible="true">
                                                <td style="width: 155px;">
                                                    <asp:Label ID="lblStatus" runat="server" Text="Project Status" Font-Size="9pt"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>   
                                                     <%-- Subhra-Added update panel for filter region--%>                                         
                                                    <asp:UpdatePanel ID="upProjectFilter" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <table>
                                                               <%-- <tr>
                                                <td style="width: 155px;">
                                                    <asp:Label ID="lblName" runat="server" Text="Project Name" Font-Size="9pt"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 155px">
                                                    <asp:TextBox ID="txtProjectName" runat="server" ToolTip="Enter Project Name" Width="149px" MaxLength="30" onkeypress="return DenySpecialChar();"></asp:TextBox><br />
                                                </td>
                                            </tr>
                                            <tr>
                                            
                                                <td style="width: 155px;">
                                               
                                                    <asp:Label ID="lblClient" runat="server" Text="Client Name" Font-Size="9pt"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 155px">
                                                    <asp:DropDownList ID="ddlClientName" runat="server" Width="155px" ToolTip="Select Client Name">
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr id="trlblStatus" runat="server" visible="true">
                                                <td style="width: 155px;">
                                                    <asp:Label ID="lblStatus" runat="server" Text="Project Status" Font-Size="9pt"></asp:Label><br />
                                                </td>
                                            </tr>--%>
                                            <tr id="trddlStatus" runat="server" visible="true">
                                                <td style="width: 155px">
                                                    <%--27631-Subhra- Added OnSelectedIndexChanged Attribute--%>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true"
                                                        Width="155px" ToolTip="Select Project Status">
                                                        <%-- onchange="return hideProejctStage();">--%>
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                          </table>
                                         </ContentTemplate>
                                        </asp:UpdatePanel>
                                       </td>
                                      </tr>      
                                            <tr id="trlblWorkFlowStatus" runat="server" visible="false">
                                                <td style="width: 155px;">
                                                    <asp:Label ID="lblWorkFlowStatus" runat="server" Text="Project Stage" Font-Size="9pt"></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr id="trddlWorkFlowStatus" runat="server" visible="false">
                                                <td style="width: 155px">
                                                    <asp:DropDownList ID="ddlWorkFlowStatus" runat="server" ToolTip="Select Project Stage" Width="155px">
                                                    </asp:DropDownList>
                                                    <br />

                                                    <script type="text/javascript">hideProejctStage();</script>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 155px;">
                                                    <br />
                                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" OnClientClick="return CheckFilter(true);" CssClass="button" Width="70px" Font-Bold="True" Font-Size="9pt" />
                                                    &nbsp;
                                                    <asp:Button ID="btnClear" OnClientClick="return ClearFilter()" runat="server" Text="Clear" CssClass="button" Width="69px" Font-Bold="True" Font-Size="9pt" />
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
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:UpdatePanel runat="server" ID="UP_ProjectSummary">
                        <ContentTemplate>
                            <asp:GridView ID="grdvListofProjects" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdvListofProjects_PageIndexChanging" AllowSorting="True" OnSorting="grdvListofProjects_Sorting" OnRowCreated="grdvListofProjects_RowCreated" DataKeyNames="ProjectID" OnRowDataBound="grdvListofProjects_RowDataBound" OnDataBound="grdvListofProjects_DataBound" Width="987px" OnRowCommand="grdvListofProjects_RowCommand">
                                <HeaderStyle CssClass="headerStyle" />
                                <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                                <%--<RowStyle Height="20px" CssClass="textstyle" />--%>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgbtnExpandCollaspeChildGrid" ImageUrl="Images/plus.JPG" CommandName="ChildGridContractsForProject" CommandArgument='<%#Eval("ProjectID") %>' ToolTip="Expand" Width="13px" Height="13px" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProjectCode" HeaderText="Project Code" SortExpression="ProjectCode">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="15%" HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName">
                                        <HeaderStyle Width="20%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName">
                                        <HeaderStyle Width="25%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProjectStatus" HeaderText="Project Status" SortExpression="ProjectStatus">
                                        <HeaderStyle Width="15%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="20%" HorizontalAlign="center" />
                                        <HeaderTemplate>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="2" style="height: 25px; border-bottom: solid 1px #ECE9D8">
                                                        <span>MRF</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%; height: 25px; border-right: solid 1px #ECE9D8">
                                                        <span>Total</span>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <span>Open</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 50%; height: 20px; text-align: center; border-right: solid 1px #ECE9D8">
                                                        <asp:HyperLink ID="lnkTotal" runat="server" Text='<%#Eval("TotalMRF")%>'></asp:HyperLink>
                                                    </td>
                                                    <td style="width: 50%; height: 20px; text-align: center">
                                                        <asp:HyperLink ID="lnkOpen" runat="server" Text='<%#Eval("OpenMRF")%>'></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                            </td></tr>
                                            <tr runat="server" id="tr_ContractGrid" style="display: none;">
                                                <td colspan="6" width="100%">
                                                    <asp:GridView runat="server" ID="grdContractGrid" AutoGenerateColumns="false" Width="100%" EmptyDataText="No Records Found">
                                                        <Columns>
                                                            <asp:BoundField>
                                                                <HeaderStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ContractCode" HeaderText="Contract Code">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle Width="24%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ContractReferenceID" HeaderText="Contract Ref ID">
                                                                <HeaderStyle Width="24%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ContractType" HeaderText="Contract Type">
                                                                <HeaderStyle Width="24%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DocumentType" HeaderText="Document Type">
                                                                <HeaderStyle Width="24%" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="text" />
                                                        <HeaderStyle CssClass="childgridheader" />
                                                        <RowStyle CssClass="childgridRow" />
                                                        <AlternatingRowStyle CssClass="childgridAlternatingRow" />
                                                        <SelectedRowStyle BackColor="#0099CC" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <table class="tablePager">
                                        <tr>
                                            <td align="center">
                                                &lt;&lt;&nbsp;&nbsp;<asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white" CommandName="Previous" OnCommand="ChangePage" Font-Underline="true" Enabled="False">Previous</asp:LinkButton>
                                                <span>Page</span>
                                                <asp:TextBox ID="txtPages" runat="server" OnTextChanged="txtPages_TextChanged" AutoPostBack="true" Width="21px" MaxLength="3" onpaste="return false;"></asp:TextBox>
                                                <span>of</span>
                                                <asp:Label ID="lblPageCount" runat="server" ForeColor="white"></asp:Label>
                                                <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next" OnCommand="ChangePage" Font-Underline="true" Enabled="False">Next</asp:LinkButton>&nbsp;&nbsp;&gt;&gt;
                                            </td>
                                        </tr>
                                    </table>
                                </PagerTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" Visible="false" OnClick="btnRemoveFilter_Click" CssClass="button" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <asp:HiddenField ID="hidRoleCOO" runat="server" EnableViewState="true" />
                    <asp:HiddenField ID="hidRolePresales" runat="server" EnableViewState="true" />
                    <asp:HiddenField ID="hidRolePM" runat="server" EnableViewState="true" />
                    <asp:HiddenField ID="hidmainTabProject" runat="server" Value="0" />
                    <asp:HiddenField ID="hidRoleRPM" runat="server" EnableViewState="true" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
