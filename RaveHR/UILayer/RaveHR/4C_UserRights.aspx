<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="4C_UserRights.aspx.cs" Inherits="FourCModule_4C_UserRights" Title="4C Admin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script language="javascript" src="JavaScript/CommonValidations.js" type="text/javascript">
    </script>

    <script type="text/javascript" src="JavaScript/FilterPanel.js">
    </script>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        var retVal;
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        retVal = e.data;
                    }
                });
            });
        })(jQuery);
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        function popUpEmployeeSearch(strValue) {
            if (strValue == "Creator" || strValue == "Reviewer") {
                var ddlDepartment = $('<%= ddlDepartment.ClientID %>');
                var ddlProjectName = $('<%= ddlProject.ClientID%>');

                if (ddlDepartment.selectedIndex > 0) {
                    //                 if (ddlDepartment.value == "1" && ddlProjectName.selectedIndex == 0) {
                    //                     alert("Please select Project.")
                    //                     return;
                    //                 }
                }
                else {
                    alert("Please select department.")
                    return;
                }
            }

            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
            onclose: function(e) {
                    var EmpName;
                    var EmpId;
                    //Ishwar 09012015 Start
                    var valReturned;
                    valReturned = retVal;
                    //Ishwar 09012015 End
                    var employee = new Array();
                    if (retVal != undefined)
                        employee = valReturned.split(",");

                    for (var i = 0; i < employee.length - 1; i++) {
                        var emp = employee[i];
                        var emp1 = new Array();
                        var emp1 = emp.split("_");

                        if (EmpId == undefined) {
                            EmpId = emp1[0];
                        }
                        else {
                            EmpId = EmpId + "," + emp1[0];
                        }

                        if (EmpName == undefined) {
                            EmpName = emp1[1];
                        }
                        else {
                            EmpName = EmpName + "," + emp1[1];
                        }
                    }
                    if (EmpName == undefined) {
                        //document.getElementById(strFM).value = "";
                    }
                    else {
                        //document.getElementById("grdvListofReportingEmployees_ctl03_txtRMToName").value = EmpName;

                        if (EmpName != null && EmpName != "") {
                            if (strValue == "Creator") {
                                jQuery('#<%=HfCreator.ClientID%>').val(EmpId);
                                jQuery('#<%=txtCreator.ClientID%>').val(EmpName);
                            } else if (strValue == "Reviewer") {
                                jQuery('#<%=HfReviewer.ClientID%>').val(EmpId);
                                jQuery('#<%=txtReviewer.ClientID%>').val(EmpName);
                            } else if (strValue == "SearchEmployee") {
                                jQuery('#<%=HfViewRights.ClientID%>').val(EmpId);
                                jQuery('#<%=txtEmployeeName.ClientID%>').val(EmpName);
                            }
                        }
                    }
                }
            }).open();
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }

        setbgToTab('ctl00_tab4C', 'ctl00_span4CAdmin');

        function $(v) {
            return document.getElementById(v);
        }

        function func_AskUser() {
            // debugger;
            return confirm("Are you sure, you want to delete the record ?");
        }

        //This function gives an alert message when "Filter" button is clicked without selecting any Filter criteria.
        function CheckFilter(isButtonClicked) {
            //ctl00_cphMainContent_ddlDepartmentFilter

            //alert(document.getElementById("<%=ddlDepartmentFilter.ClientID %>"));
            var ddlDepartmentFilter = $('<%= ddlDepartmentFilter.ClientID %>');
            var ddlProjectNameFilter = $('<%= ddlProjectNameFilter.ClientID%>');

            //This condition checks for the RoleCOO,RoleCEO and RoleRPM. When user is in either of the role,
            //all the dropdown's default value is set to "SELECT".
            if (ddlDepartmentFilter != null && ddlProjectNameFilter != null) {
                if (ddlDepartmentFilter.value == "SELECT" && ddlProjectNameFilter.value == "SELECT") {
                    if (isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");

                    return false;
                }
            }

            //This condition checks for the RolePM,RoleCFM,RoleFM and RolePreSales. When user is in either of the role,
            //Department dropdown value will be "Projects" for rolePM and "Finance" for roleCFM/RoleFM and "PreSales" for rolePresales respectively and
            //Department dropdown is disabled. The rest of the dropdown's default value is set to "SELECT".
            if ((ddlDepartmentFilter.value != "SELECT") && (ddlDepartmentFilter.disabled)) {
                if (ddlProjectNameFilter.value == "SELECT") {
                    if (isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");

                    return false;
                }
            }

        }

        //This function clears the filtering criteria and sets the value of the dropdown to "SELECT".
        function ClearFilter() {
            var ddlDepartmentFilter = $('<%= ddlDepartmentFilter.ClientID %>');
            var ddlProjectNameFilter = $('<%= ddlProjectNameFilter.ClientID%>');

            if (ddlDepartmentFilter.disabled) {
                ddlProjectNameFilter.selectedIndex = 0;
            }

            else {
                ddlDepartmentFilter.selectedIndex = 0;
                ddlProjectNameFilter.selectedIndex = 0;

                ddlProjectNameFilter.disabled = true;
            }

            return false;
        }

        var gridViewCtlId = '<%=grdvCreatorApprover.ClientID%>';
        var gridViewCtl = null;
        var curSelRow = null;
        function getGridViewControl() {
            if (null == gridViewCtl) {
                gridViewCtl = document.getElementById(gridViewCtlId);
            }
        }

        function onGridViewRowSelected(rowIdx) {
            var selRow = getSelectedRow(rowIdx);
            if (curSelRow != null) {
                curSelRow.style.backgroundColor = '#ffffff';
            }

            if (null != selRow) {
                curSelRow = selRow;
                curSelRow.style.backgroundColor = '#ff0022';
            }
        }

        function getSelectedRow(rowIdx) {
            getGridViewControl();
            if (null != gridViewCtl) {
                return gridViewCtl.rows[rowIdx];
            }
            return null;
        }


        function chekValidation() {
            var ddlDepartment = $('<%= ddlDepartment.ClientID %>');
            var ddlProjectName = $('<%= ddlProject.ClientID%>');

            var txtCreator = $('<%= txtCreator.ClientID %>');
            var txtReviewer = $('<%= txtReviewer.ClientID%>');

            if (ddlDepartment.selectedIndex > 0) {
                //             if (ddlDepartment.value == "1" && ddlProjectName.selectedIndex == 0) {
                //                 alert("Please select Project.")
                //                 return false;
                //             }
            }
            else {
                alert("Please select department.")
                return false;
            }

            if (txtCreator.value.length == 0) {
                alert("Please select Creator.")
                return false;
            }

            if (txtReviewer.value.length == 0) {
                alert("Please select Reviewer.")
                return false;
            }

            return true;
        }

        function chekValidationEmployee() {
            var txtEmpName = $('<%= txtEmployeeName.ClientID %>');
            if (txtEmpName.value.length == 0) {
                alert("Please select Employee Name.")
                return false;
            }
            return true;
        }
     
     
    </script>

    <div style="font-family: Verdana; font-size: 9pt;">
        <table width="100%">
            <tr>
                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <span class="header">4C User Rights</span>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="upMessage" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server" CssClass="text" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divDetail" runat="server">
            <table width="100%" id="tblAVP">
                <tr>
                    <td align="center">
                        <div style="text-align: justify;">
                            <table width="35%; ">
                                <tr class="detailsbg">
                                    <td class="detailsborder" align="center">
                                        <asp:RadioButtonList ID="rblAdminSelectionOption" runat="server" RepeatDirection="Horizontal"
                                            Font-Bold="true" OnSelectedIndexChanged="rblAdminSelectionOption_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Selected="True" Value="CreaterReviewer">Assign Creator / Reviewer</asp:ListItem>
                                            <asp:ListItem Value="ViewAccessRights">Assign View Access Rights</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="upDetails" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <fieldset>
    <LEGEND><b><asp:Label ID="feildLabel" runat="server" Text="Creator / Reviewer"></asp:Label></b></LEGEND>
    <div runat="server" id="divCreatorReviewer" visible="true">
    <table width="100%" border="0">
        <tr>
            <td style="width:10%">
               <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label>
            </td>
            <td style="width:40%">
                <asp:DropDownList ID="ddlDepartment" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" Width="250px" AutoPostBack="true"> 
                </asp:DropDownList>
            </td>
           
             <td style="width:10%">
               <asp:Label ID="lblProject" runat="server" Text="Project"></asp:Label>
            </td>
            <td style="width:40%">
                <asp:DropDownList ID="ddlProject" runat="server" Width="250px" Enabled="false">
                </asp:DropDownList>
            </td>
           
        </tr>
        <tr>
            <td style="vertical-align:top">
                <br />
               <asp:Label ID="lblCreator" runat="server" Text="Creator"></asp:Label>
            </td>
            <td style="vertical-align:bottom">
            <br />
                <asp:TextBox ID="txtCreator" runat="server" ToolTip="Enter Creator Name" ReadOnly="true"  Width="225px" TextMode="MultiLine"></asp:TextBox>
                 <asp:HiddenField ID="HfCreator" runat="server" />    
                 <asp:Image runat="server" id="imgCreator" ImageUrl = "Images/find.png" />
            </td>
            
             <td style="vertical-align:top">
              <br />
               <asp:Label ID="lblReviewer" runat="server" Text="Reviewer"></asp:Label>
            </td>
            <td style="vertical-align:bottom">
             <br />
                <asp:TextBox ID="txtReviewer" runat="server" ToolTip="Enter Reviewer Name"  ReadOnly="true" Width="225px"  TextMode="MultiLine"></asp:TextBox>
                 <asp:HiddenField ID="HfReviewer" runat="server" />    
                 <asp:Image runat="server" id="imgReviewer" ImageUrl = "Images/find.png" />
            </td>
            
        </tr>
       
    </table>
    
      </div>
       <div runat="server" id="divViewRights" visible="false">
        <table width="50%" border="0">
        <tr>
            <td style="width:20%" align="center">
                <asp:Label ID="lblEmployee" runat="server" Text="Employee Name"></asp:Label>
            </td>
            <td align="left" style="width:30%">
                 <asp:TextBox ID="txtEmployeeName" runat="server" ToolTip="Enter Employee Name"  ReadOnly="true"  TextMode="MultiLine"></asp:TextBox>
                 <asp:HiddenField ID="HfViewRights" runat="server" />    
                 <asp:Image runat="server" id="imgSearchEmployee" ImageUrl = "Images/find.png" />
            </td>
             <td align="left" style="width:50%">
                    <asp:Button ID="btnAddEmployee" runat="server" Text="Add" CssClass="button" OnClick="btnAddEmployee_Click" OnClientClick="return chekValidationEmployee();" />
            </td>
        </tr>
      </div>
      
    
    <table width="100%">
         <tr>
            <td>
            
            </td style="width:70%">
            <td style="width:10%" align="right">
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" OnClick="btnAdd_Click" OnClientClick="return chekValidation();" />
            </td>
            
            <td style="width:10%" align="center">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" Enabled="false" OnClick="btnUpdate_Click" />
            </td>
            <td style="width:10%" align="left">
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click" />
            </td>
        </tr>
    </table>
    
    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <div style="width: 20%" class="detailsborder" runat="server" id="divHeaderLabel">
            <table width="100%">
                <tr class="detailsbg">
                    <td>
                        <asp:Label ID="lblSummary" runat="server" Text="Creator / Reviewer Summary" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <table width="100%">
            <tr>
                <td style="width: 80%">
                    <asp:UpdatePanel ID="upGridDetails" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:GridView ID="grdvCreatorApprover" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="true" OnRowCommand="grdvCreatorApprover_RowCommand" OnRowDataBound="grdvCreatorApprover_RowDataBound"
                                            OnPageIndexChanging="grdvCreatorApprover_PageIndexChanging" Width="100%" AllowSorting="True"
                                            PageSize="10" DataKeyNames="ProjectId" GridLines="Both" EmptyDataText="No Record Found."
                                            EmptyDataRowStyle-CssClass="Messagetext">
                                            <HeaderStyle CssClass="headerStyle" />
                                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                            <RowStyle Height="20px" CssClass="textstyle" />
                                            <Columns>
                                                <asp:BoundField DataField="DEPTName" HeaderText="Department Name">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreatorName" HeaderText="Creator">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ReviewerName" HeaderText="Reviewer">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:TemplateField Visible="true">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Images/Edit.gif" Style="border: none;
                                                            cursor: hand;" ToolTip="Edit" CommandArgument='<%# Eval("ProjectId") %>' CommandName="Edt" />
                                                        <asp:HiddenField ID="hfProjectId" runat="server" Value='<%# Eval("ProjectId") %>' />
                                                        <asp:HiddenField ID="HfDepartmentId" runat="server" Value='<%# Eval("DEPTId") %>' />
                                                        <asp:HiddenField ID="HfReviewerGrd" runat="server" Value='<%# Eval("Reviewer") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="true">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/Images/Delete.gif" Style="border: none;
                                                            cursor: hand;" ToolTip="Delete" CommandArgument='<%# Eval("DEPTId") %>' CommandName="Del" />
                                                        <asp:HiddenField ID="HfCreatorGrd" runat="server" Value='<%# Eval("Creator") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="tablePager" HorizontalAlign="Center" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 20%" valign="top" id="tdFilter" runat="server">
                    <div id="shelf" class="filter_main" style="width: 280px;">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr style="cursor: hand;" onclick="javascript:activate_shelf();">
                                <td class="filter_title" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr= '#5C7CBD' , startColorstr= '#12379A' , gradientType= '1' );">
                                    <a id="control_link" href="javascript:activate_shelf();" style="color: White; font-family: Verdana;
                                        font-size: 9pt"><b>Filter</b></a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="shelf_contents" class="filter_content" style="border: solid 1px black; text-align: center;
                                        width: 272px;">
                                        <table style="text-align: left; left: 525px; top: 282px; font-family: Verdana" cellpadding="1">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upFilter" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 280px;">
                                                                        <asp:Label ID="Label1" runat="server" Text="Department" CssClass="txtstyle">
                                                                        </asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 280px;">
                                                                        <asp:DropDownList ID="ddlDepartmentFilter" runat="server" AutoPostBack="true" ToolTip="Select Department"
                                                                            Width="250px" OnSelectedIndexChanged="ddlDepartmentFilter_SelectedIndexChanged"
                                                                            CssClass="mandatoryField">
                                                                        </asp:DropDownList>
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
                                                                        <asp:DropDownList ID="ddlProjectNameFilter" runat="server" ToolTip="Select Project Name"
                                                                            Width="250px" CssClass="mandatoryField">
                                                                        </asp:DropDownList>
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
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
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
        <table width="100%" border="0">
            <tr>
                <td style="width: 60%" align="right">
                </td>
                <td style="width: 20%;" align="right">
                </td>
                <td style="width: 10%;" align="center">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                </td>
                <td style="width: 10%;" align="left">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
