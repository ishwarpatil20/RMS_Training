<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="MrfPendingAllocation.aspx.cs" Inherits="MrfPendingAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript" src="JavaScript/FilterPanel.js">
    </script>

    <script type="text/javascript">

        setbgToTab('ctl00_tabMRF', 'ctl00_spanMRFPendingAllocation');

        var isbutton = false;
        function buClick() {
            isbutton = true;
            alert('button clicked');
        }

        function sendWindow(url) {
            location.href = url;
            //window.open(url);

        }

        function $(v) {
            return document.getElementById(v);
        }

        //This function gives an alert message when "Filter" button is clicked without selecting any Filter criteria.
        function CheckFilter(isButtonClicked) {
            var ddlDepartment = $('<%= ddlDepartment.ClientID %>');
            var ddlProjectName = $('<%= ddlProjectName.ClientID%>');
            var ddlRole = $('<%= ddlRole.ClientID%>');
            var ddlStatus = $('<%= ddlStatus.ClientID%>');       //18402-Ambar

            if (ddlDepartment != null && ddlProjectName != null && ddlRole != null && ddlStatus != null) //18402-Ambar
            {
                if (ddlDepartment.value == "SELECT" && ddlProjectName.value == "SELECT" && ddlRole.value == "SELECT" && ddlStatus.value == "SELECT") //18402-Ambar
                {
                    if (isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");

                    return false;
                }
            }

            if ((ddlDepartment.value != "SELECT") && (ddlDepartment.disabled)) {
                if (ddlProjectName.value == "SELECT" && ddlRole.value == "SELECT") {
                    if (isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");

                    return false;
                }
            }

            //18402-Ambar
            /*if ((ddlStatus.value != "SELECT") && (ddlStatus.disabled)) 
            {
            if (ddlDepartment.value == "SELECT" && ddlProjectName.value == "SELECT" && ddlRole.value == "SELECT") {
            if (isButtonClicked)
            alert("Please select or enter any criteria, to proceed with filter.");

                return false;
            }
           
            }*/
            return true;

        }

        //This function clears the filtering criteria and sets the value of the dropdown to "SELECT".
        function ClearFilter() {
            var ddlDepartment = $('<%= ddlDepartment.ClientID %>');
            var ddlProjectName = $('<%= ddlProjectName.ClientID%>');
            var ddlRole = $('<%= ddlRole.ClientID%>');
            var ddlStatus = $('<%= ddlStatus.ClientID%>');  //18402-Ambar

            //If Department value is not "SELECT" than set the value to "SELECT" and disable ProjectName and
            //Role dropdown.
            if (ddlDepartment.value != "SELECT") 
            {
                ddlDepartment.selectedIndex = 0;
                ddlProjectName.selectedIndex = 0;
                ddlRole.selectedIndex = 0;
                ddlProjectName.disabled = true;
                ddlRole.disabled = true;
                ddlStatus.selectedIndex = 0; //18402-Ambar
            }

            ddlStatus.selectedIndex = 0; //18402-Ambar
            return false;
        }
        
      
       function  fnRaiseHeadCountPopup(pageName)
       {
                     window.showModalDialog(pageName, null,"dialogHeight:400px; dialogLeft:15px; dialogWidth:550px;");
//"dialogHeight:400px; dialogLeft:15px; dialogWidth:550px;"
       }
        
        
        function IsCheckBoxChecked()
        {
            //get reference of GridView control
            var grid = document.getElementById("<%= grdvListofPendingAllocation.ClientID %>");
            //variable to contain the cell of the grid
            var cell;
            var isChecked = false;
            
            if (grid.rows.length > 0)
            {
                //loop starts from 1. rows[0] points to the header.
                for (i=1; i<grid.rows.length; i++)
                {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];
                    
                    //loop according to the number of childNodes in the cell
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type =="checkbox")
                        {
                           if( cell.childNodes[j].checked )
                           {
                            isChecked = true;
                           }
                        }
                    }
                }
                if (!isChecked){
                alert("No MRF is selected.")
                }
            }
           return isChecked;
        }

        
        
        
    </script>

    <table width="100%">
        <tr>
        <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
        <%--    Sanju:Issue Id 50201:End--%>
                <span class="header">List of Pending Allocation</span>
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
                <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext" Visible="false">
                </asp:Label>
            </td>
            <td style="height: 19px; width: 280px;">
                <div id="shelf" class="filter_main" style="width: 280px;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                   <%-- Sanju:Issue Id 50201:Chsnged cursor to pointer--%>
                        <tr style="cursor: pointer;" onclick="javascript:activate_shelf();">
                     <%--   Sanju:Issue Id 50201:End--%>
                            <td class="filter_title header_filter_title" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr= '#5C7CBD' , startColorstr= '#12379A' , gradientType= '1' );">
                                <a id="control_link" href="javascript:activate_shelf();" style="color: White; font-family: Verdana;
                                    font-size: 9pt;"><b>Filter</b></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <%--Sanju:Issue Id 50201:Resolved alignment issue:Changed width--%>
                                <div id="shelf_contents" class="filter_content" style="border: solid 1px black; text-align: center;
                                    width: 274px;">
                                <%--    Sanju:Issue Id 50201:End--%>
                                    <%--<asp:UpdatePanel ID="upFilter" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>--%>
                                    <table style="text-align: left; left: 541px; top: 282px; font-family: Verdana" cellpadding="1">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upFilter" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblDepartment" runat="server" Text="Department" CssClass="txtstyle">
                                                                    </asp:Label>
                                                                   
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                   <%--Sanju:Issue Id 50201:Resolved alignment issue:Changed width--%>  
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" ToolTip="Select Department"
                                                                        Width="266px" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="mandatoryField">
                                                                    </asp:DropDownList>
                                                                     <%--    Sanju:Issue Id 50201:End--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" CssClass="txtstyle">
                                                                    </asp:Label>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                  <%--Sanju:Issue Id 50201:Resolved alignment issue:Changed width--%>  
                                                                    <asp:DropDownList ID="ddlProjectName" runat="server" ToolTip="Select Project Name"
                                                                        Width="266px" CssClass="mandatoryField">
                                                                    </asp:DropDownList>
                                                                     <%--    Sanju:Issue Id 50201:End--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblRole" runat="server" Text="Role" CssClass="txtstyle">
                                                                    </asp:Label>
                                                                   
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                   <%--Sanju:Issue Id 50201:Resolved alignment issue:Changed width--%>  
                                                                    <asp:DropDownList ID="ddlRole" runat="server" ToolTip="Select Role" Width="266px"
                                                                        CssClass="mandatoryField">
                                                                    </asp:DropDownList>
                                                                       <%--    Sanju:Issue Id 50201:End--%>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <!--18402-Ambar-Adding new filter field-->
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="txtstyle">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                 <%--Sanju:Issue Id 50201:Resolved alignment issue:Changed width--%>  
                                                                    <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Select Status" Width="266px"
                                                                        CssClass="mandatoryField">
                                                                    </asp:DropDownList>
                                                                       <%--    Sanju:Issue Id 50201:End--%>
                                                                      
                                                                </td>
                                                            </tr>
                                                            
                                                             <!--vandana-Adding new filter field-->
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblTypeOfAllocation" runat="server" Text="Allocation Type" CssClass="txtstyle">
                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:DropDownList ID="ddlTypeOfAllocation" runat="server" AutoPostBack="true" ToolTip="Select TypeOfAllocation"
                                                                     Width="266px" OnSelectedIndexChanged="ddlTypeOfAllocation_SelectedIndexChanged" CssClass="mandatoryField">
                                                                    </asp:DropDownList>
                                                                      
                                                                </td>
                                                            </tr>
                                                            
                                                             <tr>
                                                                <td style="width: 280px;">
                                                                    <asp:Label ID="lblTypeOfSupply" runat="server" Text="Allocation Supply" CssClass="txtstyle">
                                                                    </asp:Label>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 280px;">
                                                                 <%--Sanju:Issue Id 50201:Resolved alignment issue:Changed width--%>  
                                                                    <asp:DropDownList ID="ddlTypeOfSupply" runat="server" ToolTip="Select TypeOfSupply"
                                                                     Width="266px" CssClass="mandatoryField">
                                                                    </asp:DropDownList>
                                                                   <%--  Sanju:Issue Id 50201:End--%>
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 280px;" align="center">
                                                <br />
                                                <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="button" Width="70px"
                                                    Font-Bold="True" Font-Size="9pt" OnClick="btnFilter_Click" OnClientClick="return CheckFilter(true);" />
                                                &nbsp;
                                                <asp:Button ID="btnClear" runat="server" OnClientClick="return ClearFilter()" Text="Clear"
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
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="UP_PendingAllocation">
                    <ContentTemplate>
                        <asp:GridView ID="grdvListofPendingAllocation" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="grdvListofPendingAllocation_PageIndexChanging"
                            AllowSorting="True" OnSorting="grdvListofPendingAllocation_Sorting" OnRowCreated="grdvListofPendingAllocation_RowCreated"
                            DataKeyNames="ProjectID" OnRowDataBound="grdvListofPendingAllocation_RowDataBound"
                            OnDataBound="grdvListofPendingAllocation_DataBound" Width="100%" OnRowCommand="grdvListofPendingAllocation_RowCommand">
                            <HeaderStyle CssClass="headerStyle" />
                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                            <RowStyle Height="20px" CssClass="txtstyle" />
                            <Columns>
                                <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbxMRFID" runat="server"/>
                                        <asp:HiddenField ID="hfMRFID" runat="server" Value='<%# Bind("MRFId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MRFId" HeaderText="MRF Id" SortExpression="MRFID">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MRFCode" HeaderText="MRF Code" SortExpression="MRFCode">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="RPCode" HeaderText="RP Code" SortExpression="RPCode">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ResourceOnBoard" HeaderText="Required From" SortExpression="ResourceOnBoard"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmployeeName" HeaderText="Raised By" SortExpression="MRFRaisedBy">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DeptName">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RecruiterName" HeaderText="Recruiter Name" SortExpression="RecruiterName">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                                          
                                <asp:BoundField DataField="TypeOfSupplyName" HeaderText="Type OF Supply" SortExpression="TypeOfSupply" >
                                     <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                                         
                                <asp:BoundField DataField="TypeOfAllocationName" HeaderText="Type OF Allocation" SortExpression="TypeOfAllocation" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                 </asp:BoundField>
                                 
                                   <asp:BoundField DataField="FutureAllocateResourceName" HeaderText="FutureResource" SortExpression="FutureAllocateResourceName" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                 </asp:BoundField>
                                
                                
                                <asp:BoundField DataField="FutureAllocationDate" HeaderText="Future Allocation Date" SortExpression="FutureAllocationDate"
                                     DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField Visible="false">
                                     <%--Sanju:Issue Id 50201:Changed cursor to pointer--%>  
                                        <ItemTemplate>                                         
                                                <asp:ImageButton ID ="imgMove" runat = "server" ImageUrl = "~/Images/copy.png" 
                                                style="border: none; cursor: pointer;" ToolTip = "Move MRF" 
                                              CommandName = "MoveMrf" CommandArgument='<%# Eval("MRFId") %>' visible="false"/>
                                        </ItemTemplate>
                                      <%--  Sanju:Issue Id 50201:End--%>
                                        <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
               
                            </Columns>
                            <PagerTemplate>
                                <table class="tablePager">
                                    <tr>
                                        <td align="center">
                                            &lt;&lt;&nbsp;&nbsp;<asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white"
                                                CommandName="Previous" OnCommand="ChangePage" Font-Underline="true" Enabled="True">Previous</asp:LinkButton>
                                            <span>Page</span>
                                            <asp:TextBox ID="txtPages" runat="server" OnTextChanged="txtPages_TextChanged" AutoPostBack="true"
                                                Width="21px" MaxLength="3" onpaste="return false;"></asp:TextBox>
                                            <span>of</span>
                                            <asp:Label ID="lblPageCount" runat="server" ForeColor="white"></asp:Label>
                                            <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                                OnCommand="ChangePage" Font-Underline="true" Enabled="True">Next</asp:LinkButton>&nbsp;&nbsp;&gt;&gt;
                                        </td>
                                    </tr>
                                </table>
                            </PagerTemplate>
                            <SelectedRowStyle BackColor="#0099CC" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="btnRaiseHeadCount" runat="server" Text="Raise Head Count" Visible="true"
                    CssClass="button" OnClick="btnRaiseHeadCount_Click" />
                &nbsp;
                <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" Visible="false"
                    OnClick="btnRemoveFilter_Click" CssClass="button" />
                <asp:HiddenField ID="hidEncryptedQueryString" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
