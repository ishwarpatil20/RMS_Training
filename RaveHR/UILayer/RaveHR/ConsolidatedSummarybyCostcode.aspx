<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" 
CodeFile="ConsolidatedSummarybyCostcode.aspx.cs" Inherits="ConsolidatedSummarybyCostcode" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script src="JavaScript/CommonValidations.js" type="text/javascript"></script>

    <script src="JavaScript/FilterPanel.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>    

    <script src="JavaScript/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="JavaScript/ScrollableGrid.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
        jQuery('#<%=GVResourcesOnboard.ClientID %>').Scrollable();
        }
    )
    
        $(document).ready(function() {
        jQuery('#<%=GVResourcesReleased.ClientID %>').Scrollable();
        }
    )
    </script>

    <script type="text/javascript">

        //setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmpSummary');

        function $(v) {
            return document.getElementById(v);
        }

       
    </script>

    <script type="text/javascript">
        //var isSubmitClicked = false;
        //function $(v) { return document.getElementById(v); }

        function Validate() {

            var dropdownsAreValid = true;
            isSubmitClicked = true;

            var ddlControlIds = "";
            if (document.getElementById("<%=ddlProject.ClientID %>") != null)
                ddlControlIds = ddlControlIds + "," + document.getElementById("<%=ddlProject.ClientID %>").id;
            if (document.getElementById("<%=ddlProject.ClientID %>").value == "SELECT") {
                var lblMandatory = document.getElementById("<%=lblMandatory.ClientID %>");
                lblMandatory.innerText = "Please select the team.";
                lblMandatory.style.color = "Red"
                return false;
            }
            return true;
            //49176 Ishwar End
        }
    </script>

    <div style="font-family: Verdana; font-size: 9pt;">
        <table width="100%">
            <tr>
                <%--Sanju:Issue Id 50201: Added new class header_employee_profile so that the header color is same for all browsers--%>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                    <span class="header">Consolidated Report By CostCode</span>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
        <table width="99%" align="center">
            <tr>
                <td align="center" style="width: 10%">
                    Cost Code :
                </td>
                <td align="left">
                    <span id="spanProject">
                        <asp:DropDownList ID="ddlProject" runat="server" ToolTip="Select Team" CssClass="mandatoryField">
                        </asp:DropDownList>
                    </span>&nbsp;&nbsp;
                    <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="button" Font-Bold="True"
                        OnClientClick="return Validate();" Font-Size="9pt" OnClick="btnFilter_Click" />&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnExport" Text="Export to Excel" CssClass="button"
                        OnClientClick="return Validate();" Visible="false" OnClick="btnExport_Click" />
                </td>
                <td align="left">
                    <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="99%" cellspacing="0" cellpadding="0">
            <tr class="detailsbg" style="height: 25px" id="TROnBoard" runat="server">
                <td class="detailsheader">
                    Resources On-board
                </td>
            </tr>
        </table>
        <table width="99%" cellspacing="0" cellpadding="0">
            <tr>
                <td>  
                    <asp:GridView ID="GVResourcesOnboard" runat="server" AutoGenerateColumns="False"
                     AllowSorting="true" OnSorting="GVResourcesOnboard_Sorting" AllowPaging="false" Width="100%" 
                     OnRowCreated="GVResourcesOnboard_RowCreated"> 
                        <HeaderStyle CssClass="headerStyleFreeze" />
                        <AlternatingRowStyle CssClass="alternatingrowStyleFreeze" />
                        <RowStyle Height="20px" CssClass="textstyleFreeze" />
                        <Columns>
                            <asp:BoundField DataField="SRNO" HeaderText="Sr. No.">
                                <ItemStyle Width="5%" />
                                <HeaderStyle Width="5%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMPCode" HeaderText="Employee Code" SortExpression="EMPCode">
                                <ItemStyle Width="8%" />
                                <HeaderStyle Width="8%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="Name">
                                <ItemStyle Width="15%" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Job title" HeaderText="Job title" SortExpression="[Job Title]">
                                <ItemStyle Width="15%" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Skills" HeaderText="Skills" SortExpression="[Skills]">
                                <ItemStyle Width="15%" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                           <%--New column added Resource Type --Mohamed--%>
                            <asp:BoundField DataField="Resource Type" HeaderText="Resource Type" SortExpression="[Resource Type]">
                                <ItemStyle Width="8%" />
                                <HeaderStyle Width="8%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <%--Mohamed -- End    --%>
                               <%--New column added CostCode --Siddharth 23 April 2015 --%>
                           <%-- <asp:BoundField DataField="CostCode" HeaderText="Cost Code">
                                <ItemStyle Width="8%" />
                                <HeaderStyle Width="8%" HorizontalAlign="Left" />
                            </asp:BoundField>--%>
                            <%--Siddharth  23 April 2015 -- End  --%>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
      <!--  <table width="99%" cellspacing="0" cellpadding="0">
            <tr class="detailsbg" style="height: 25px" id="TRRelease" runat="server">
                <td class="detailsheader">
                    Resources in the past
                </td>
            </tr>
        </table>-->
       <!-- <table width="99%" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:GridView ID="GVResourcesReleased" runat="server" AutoGenerateColumns="False"
                        AllowPaging="false" AllowSorting="true" OnSorting="GVResourcesReleased_Sorting" Width="100%"
                        OnRowCreated="GVResourcesReleased_RowCreated"> 
                        
                        <HeaderStyle CssClass="headerStyleFreeze" />
                        <AlternatingRowStyle CssClass="alternatingrowStyleFreeze" />
                        <RowStyle Height="20px" CssClass="textstyleFreeze" />
                        <Columns>
                            <asp:BoundField DataField="SRNO" HeaderText="Sr. No.">
                                <ItemStyle Width="5%" />
                                <HeaderStyle Width="5%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMPCode" HeaderText="Employee Code" SortExpression="EMPCode">
                                <ItemStyle Width="8%" />
                                <HeaderStyle Width="8%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="FirstName">
                                <ItemStyle Width="15%" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Job title" HeaderText="Job title" SortExpression="[Job Title]">
                                <ItemStyle Width="15%" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Skills" HeaderText="Skills" SortExpression="[Skills]">
                                <ItemStyle Width="15%" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProjectStartDate" HeaderText="Start date" SortExpression="ProjectStartDate"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="8%" />
                                <HeaderStyle Width="8%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProjectEndDate" HeaderText="End date" SortExpression="ProjectEndDate"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="8%" />
                                <HeaderStyle Width="8%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <%--New column added Resource Type --Mohamed--%>
                            <asp:BoundField DataField="Resource Type" HeaderText="Resource Type" SortExpression="[Resource Type]">
                                <ItemStyle Width="8%" />
                                <HeaderStyle Width="8%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <%--Mohamed -- End    --%>
                         </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table> -->
    </div>
</asp:Content>
