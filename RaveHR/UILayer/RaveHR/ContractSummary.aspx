<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" Trace="false" EnableEventValidation="false"
    CodeFile="ContractSummary.aspx.cs" Inherits="ContractSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script language="javascript" src="JavaScript/CommonValidations.js" type="text/javascript">
    </script>

    <script type="text/javascript" src="JavaScript/FilterPanel.js">
    </script>

    <script type="text/javascript">
        setbgToTab('ctl00_tabContract', 'ctl00_spanContractSummary');
    
         function $(v)
         {
            return document.getElementById(v);
         }
        
        //Check for Special charecter.
        function isNumberKey(evt) {
           
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        
        //Check in filter any value is selected or not?
        function CheckFilter(isButtonClicked)
        {
             var txtDocumentName = document.getElementById('ctl00$cphMainContent$txtDocumentName');
             var vartxtDocumentName;
             //Name: Sanju:Issue Id 50201  Added condition for undefined and null
             if (txtDocumentName != undefined && txtDocumentName !=null)
                  vartxtDocumentName = txtDocumentName.value;
            
             var ddlContractType = document.getElementById('ctl00$cphMainContent$ddlContractType');
             var varddlContractType;
             //Name: Sanju:Issue Id 50201  Added condition for undefined and null
             if (ddlContractType != undefined && ddlContractType != null)
                   varddlContractType = ddlContractType.value;
             
             var ddlContractStatus = document.getElementById('ctl00$cphMainContent$ddlContractStatus');
             var varContractStatus;
             //Name: Sanju:Issue Id 50201  Added condition for undefined and null
             if (ddlContractStatus != undefined && ddlContractStatus != null)
                  varContractStatus = ddlContractStatus.value;
             
             var ddlClientName = $('<%= ddlClientName.ClientID%>');
             var varClientName = ddlClientName.value; 
             
            if (vartxtDocumentName=="" && varddlContractType == "0" && varContractStatus=="0" && varClientName=="0")
            {
                 if(isButtonClicked)
                       alert("Please select or enter any criteria, to proceed with filter.");
                        
                    return false;
            }
        }
    
              
        
        //This function clears the filtering criteria and sets the value of the dropdown to "SELECT".
        function ClearFilter()
        {
            var txtDocumentName = $('<%= txtDocumentName.ClientID %>');
            var ddlContractType = $('<%= ddlContractType.ClientID%>');
            var ddlContractStatus = $('<%= ddlContractStatus.ClientID%>');
            var ddlClientName = $('<%= ddlClientName.ClientID%>');
            
                txtDocumentName.value = "";
                ddlContractType.selectedIndex = 0;
                ddlContractStatus.selectedIndex = 0;
                ddlClientName.selectedIndex = 0;
          return false;
        }
       
       //Deny the special character in textbox.
       function DenySpecialChar()
        {
            if(event.keyCode == 13)
            {
                var txtProjectName = document.getElementById('<%=txtDocumentName.ClientID %>');
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
        
        
        var isbutton = false;
        function buClick(){isbutton = true;}
        
        
 </script>
 
    <table width="100%">
        <tr>
       <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center"  class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                background-color: #7590C8">
                <span class="header">Contract Summary</span>
            </td>
         <%--   Sanju:Issue Id 50201: End--%>
        </tr>
   
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="text" Visible="false"></asp:Label>
            </td>
            <td style="height: 19px; width: 200px;">
                <div id="shelf" class="filter_main" style="width:195px" >
                    <table width="100%" cellpadding="0" cellspacing="0">
                  <%--  Sanju:Issue Id 50201:Changed cursor to pointer--%>
                        <tr style="cursor: pointer;" onclick="javascript:activate_shelf();">
                            <td class="filter_title header_filter_title" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr= '#5C7CBD' , startColorstr= '#12379A' , gradientType= '1' );">
                                <a id="control_link" href="javascript:activate_shelf();" style="color: White; font-family:Verdana; font-size:9pt"><b>Filter</b></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="shelf_contents" class="filter_content" style="border: solid 1px black; text-align: center;
                                    width: 189px;">
                                   <%-- <asp:UpdatePanel runat="server" ID="UP_ContractSummary">
                                    <ContentTemplate>--%>
                                    <table style="text-align: left; left: 541px; top: 282px;" cellpadding="1">
                                        <tr>
                                            <td style="width: 155px;">
                                                <asp:Label ID="lblDocumentName" runat="server" Text="Document Name" CssClass="txtstyle"></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px">
                                                <asp:TextBox ID="txtDocumentName" runat="server" ToolTip="Enter Document Name" Width="149px"
                                                    MaxLength="30" onkeypress="return DenySpecialChar();"></asp:TextBox><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px;">
                                                <asp:Label ID="lblContractType" runat="server" Text="Contract type" CssClass="txtstyle"></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px">
                                                <asp:DropDownList ID="ddlContractType" runat="server" Width="155px" ToolTip="Select Contract Type" CssClass="mandatoryField">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width: 155px;">
                                                <asp:Label ID="lblClientName" runat="server" Text="Client Name" CssClass="txtstyle"></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px">
                                                <asp:DropDownList ID="ddlClientName" runat="server" Width="155px" ToolTip="Select Client Name" CssClass="mandatoryField">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                        
                                        <tr id="trlblStatus" runat="server" visible="true">
                                            <td style="width: 155px;">
                                                <asp:Label ID="lblContractStatus" runat="server" Text="Contract Status" CssClass="txtstyle"></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr id="trddlStatus" runat="server" visible="true">
                                            <td style="width: 155px">
                                                <asp:DropDownList ID="ddlContractStatus" runat="server"  Width="155px"
                                                    ToolTip="Select Contract Status" CssClass="mandatoryField">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 155px;">
                                                <br />
                                                <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClientClick="return CheckFilter(true);" 
                                                    OnClick="btnFilter_Click" CssClass="button" Width="70px" Font-Bold="True"
                                                    Font-Size="9pt" />
                                                &nbsp;
                                                <asp:Button ID="btnClear" OnClientClick="return ClearFilter()" runat="server" Text="Clear"
                                                     CssClass="button" Width="69px" Font-Bold="True" Font-Size="9pt" />
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                    <%--</ContentTemplate>
                                    </asp:UpdatePanel>--%>
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
        <tr  <%--onclick="javascript:__doPostBack('_ctl0$dgRequests$_ctl4$_ctl0','')"--%>>
            <td>
                <%--<asp:UpdatePanel runat="server" ID="UP_ContractSummary">
                    <ContentTemplate>--%>
                        <asp:GridView ID="grdvListofContract" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" AllowSorting="True" OnSorting="grdvListofContract_Sorting"                            
                            Width="987px" OnRowCommand="grdvListofContracts_RowCommand"
                            OnDataBound="grdvListofContract_DataBound" 
                            onrowdatabound="grdvListofContract_RowDataBound" OnRowCreated="grdvListofContract_RowCreated"  DataKeyNames="ContractId">
                            <HeaderStyle CssClass="headerStyle" />
                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                            <RowStyle Height="20px" CssClass="textstyle" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgbtnExpandCollaspeChildGrid" ImageUrl="Images/plus.JPG"
                                            CommandName="ChildGridContractsForProject" CommandArgument='<%#Eval("ContractId") %>'
                                            ToolTip="Expand" Width="13px" Height="13px" />
                                            <asp:HiddenField ID="hfProjectId" runat="server" Value='<%# Bind("ProjectId") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Width="4%" />
                                </asp:TemplateField>
                              
                                <asp:BoundField DataField="ContractCode" HeaderText="Contract Code" SortExpression="ContractCode">
                                    <HeaderStyle Width="6%" HorizontalAlign="Left" />
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="ContractReferenceID" HeaderText="Contract Ref Id" SortExpression="ContractRefId">
                                    <HeaderStyle Width="8%" HorizontalAlign="Left" />
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="DocumentName" HeaderText="Document Name" SortExpression="DocumentName">
                                    <HeaderStyle Width="8%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ContractType" HeaderText="Contract Type" SortExpression="Name">
                                    <HeaderStyle Width="6%" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName">
                                    <HeaderStyle Width="6%" HorizontalAlign="Left" />
                                </asp:BoundField>
                              
                                <asp:TemplateField HeaderText="Account Manager" SortExpression="FirstName">
                                    <HeaderStyle Width="9%" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("FirstName") %>'></asp:Label>
                                        </td></tr>
                                        <tr runat="server" id="tr_ProjectGrid" style="display: none;">
                                            <td colspan="7" width="100%">
                                                <asp:GridView ID="grdProjectGrid" runat="server"  AutoGenerateColumns="false" Width="100%"
                                                    EmptyDataText="No Records Found" OnRowCommand="grdProjectGrid_RowCommand" OnRowDataBound="grdProjectGrid_OnRowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="imgbtnExpandCollaspeRPChildGrid" ImageUrl="Images/plus.JPG"
                                                                    CommandName="ChildGridContractsForRP" CommandArgument='<%#Eval("ContractProjectID") %>'
                                                                    ToolTip="Expand" Width="13px" Height="13px" />
                                                                    <asp:HiddenField ID="hfRPId" runat="server" Value='<%# Bind("RPId") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                            <HeaderStyle Width="8%" />
                                                         </asp:TemplateField>
                                                        <asp:BoundField DataField="ProjectCode" HeaderText="Project Code">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                                                            <HeaderStyle Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectType" HeaderText="Project Type">
                                                            <HeaderStyle Width="15%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ProjectStartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle Width="15%" />
                                                        </asp:BoundField>
                                                         <asp:TemplateField HeaderText="End Date">
                                                         <HeaderStyle Width="18%" HorizontalAlign="Left" />
                                                         <ItemTemplate> 
                                         
                                                               <asp:Label runat="server" ID="lblEndDate" Text='<%#Eval("ProjectEndDate", "{0:dd/MM/yyyy}") %>' DataFormatString="{0:dd/MM/yyyy}"></asp:Label>
                                                                </td></tr>
                                                                <tr runat="server" id="tr_RPGrid" style="display: none;">
                                                                    <td colspan="6" width="100%">
                                                                        <asp:GridView runat="server" ID="grdRPGrid" AutoGenerateColumns="false" Width="100%"
                                                                            EmptyDataText="No Records Found" OnRowCommand="grdRPGrid_OnRowCommand">
                                                                            <Columns>
                                                                                <asp:BoundField  DataField="" HeaderStyle-Width="14%"/>
                                                                                <asp:BoundField DataField="ResourcePlanCode" HeaderText="RP Code" ItemStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle Width="15%"/>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                                    <HeaderStyle Width="12%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                                    <HeaderStyle Width="12%" />
                                                                                </asp:BoundField>
                                                                                
                                                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                                    <HeaderStyle Width="12%" />
                                                                                </asp:BoundField>
                                                                               
                                                                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                                                                    <HeaderStyle Width="15%" />
                                                                                </asp:BoundField>
                                                                                
                                                                                <asp:BoundField DataField="ResourcePlanApprovalStatus" HeaderText="Status">
                                                                                    <HeaderStyle Width="15%" />
                                                                                </asp:BoundField>
                                                                                
                                                                                 <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton runat="server" ID="imgView" ImageUrl="~/Images/view.png" ToolTip="View" Width="20px" Height="20px" CommandName="ViewImageBtn" CommandArgument='<%# Eval("RPId") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                                <HeaderStyle BackColor="#9CC089" Font-Bold="True" font-size= "9pt" Height="30px" Font-Names="Verdana"/>
                                                                                <AlternatingRowStyle BackColor="#FDE8D7" Font-Names="Verdana" font-size= "9pt"/>
                                                                                <RowStyle Height="20px" BackColor ="#D8F3C9" Font-Names="Verdana" font-size= "9pt"/>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                         </ItemTemplate>
                                                         </asp:TemplateField>
                                                      
                                                    </Columns>
                                                   <%-- <HeaderStyle Height="30px" BackColor="#4EE2EC" Font-Size="9pt" Font-Bold="True" />--%>
                                                    <HeaderStyle CssClass="childgridheader" />
                                                    <RowStyle CssClass="childgridRow" />
                                                    <AlternatingRowStyle CssClass="childgridAlternatingRow" />
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
                                            &lt;&lt;&nbsp;&nbsp;<asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white"
                                                CommandName="Previous" OnCommand="ChangePage" Font-Underline="true" Enabled="False">Previous</asp:LinkButton>
                                            <span>Page</span>
                                            <asp:TextBox ID="txtPages" runat="server" AutoPostBack="true" Width="21px" OnTextChanged="txtPages_TextChanged"
                                                MaxLength="3" onpaste="return false;"></asp:TextBox>
                                            <span>of</span>
                                            <asp:Label ID="lblPageCount" runat="server" ForeColor="white"></asp:Label>
                                            <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                                OnCommand="ChangePage" Font-Underline="true" Enabled="False">Next</asp:LinkButton>&nbsp;&nbsp;&gt;&gt;
                                        </td>
                                    </tr>
                                </table>
                            </PagerTemplate>
                        </asp:GridView>
                  <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" Visible="false"
                    CssClass="button" OnClick="btnRemoveFilter_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
