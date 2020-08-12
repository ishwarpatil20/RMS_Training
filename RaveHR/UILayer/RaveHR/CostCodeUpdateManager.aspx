<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"  CodeFile="CostCodeUpdateManager.aspx.cs"
 Inherits="CostCodeUpdateManager" EnableEventValidation="false"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Update Cost Code</title>
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />
    <script src="JavaScript/CommonValidations.js" type="text/javascript"></script>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <link href="CSS/jquery.modalDialogContent.css" rel="stylesheet" type="text/css" />
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinnyContent.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialogContent.js" type="text/javascript"></script>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
</head>
<body>
    <form id="form1" runat="server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <cc1:ToolkitScriptManager ID="ScriptManagerMaster" runat="server">
    </cc1:ToolkitScriptManager>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    <div>
        <table width="100%">
            <tr>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <span class="header">Update Cost Code</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" CssClass="text" Visible="true"></asp:Label>
                </td>
            </tr>
        </table>
        <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
        <asp:UpdatePanel ID="updesignation" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
                
                
                 <table style="width: 100%; display:none;">
                        <tr>
                            <td align="left">
                               <asp:Label ID="lblUpdateType" Text="Update Type" CssClass="txtstyle" runat="server"></asp:Label>
                              </td>
                              <td style="width: 80%">
                                <asp:RadioButtonList  ID="ddlMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged = "ddlMode_SelectedIndexChanged" RepeatDirection="Horizontal" >
                                <%--<asp:ListItem Text="Project Wise" Value="Project" Selected="True"></asp:ListItem>--%>
                                <asp:ListItem Text="Employee Wise" Value="Employee" Selected="True"></asp:ListItem>
                               </asp:RadioButtonList>
                            </td>
                        </tr>
                      <tr>
                            <td align="left" style="width: 20%">
                                <%--<asp:Label ID="lblCostCode" runat="server" Text="Cost Code" CssClass="txtstyle"></asp:Label>--%>
                                </td>
                            <td style="width: 80%">
                                <%--<asp:DropDownList ID="DdlCostCode" runat="server" >
                                </asp:DropDownList>--%>
                            </td>
                        </tr>
                    </table>
                
                <%--VRP : Actual vs Budget :20 jun 2016
                Commented--%>
                <asp:Panel id="pnlProjectSpecificCostCode" visible="true" runat="server">
                 
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblSelectProject" runat="server" Text="Select Project" CssClass="txtstyle"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="ddlProjectList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <span id="dataViewForProject" runat="server">
                   <!-- OnRowDataBound="grdvListofProjects_RowDataBound" -->
                    <asp:GridView ID="grdvListofProjects" runat="server" AutoGenerateColumns="False"
                        OnSorting="grdvListofProjects_Sorting" Width="650" AllowSorting="false"
                        OnRowCreated="grdvListofProjects_RowCreated" ShowFooter="True" EmptyDataText="No Record Found."
                        EmptyDataRowStyle-CssClass="Messagetext" style="margin-left:30px; margin-top:30px">
                        <HeaderStyle CssClass="headerStyle" />
                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                        <RowStyle Height="20px" CssClass="textstyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select All">
                                <HeaderStyle HorizontalAlign="center" Width="85px" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Text="Select All" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                     <span id="spanChkSelect" runat="server">
                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="Check_Click(this)" />
                                    <%--AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged"--%>
                                    </span>
                                    <asp:HiddenField ID="HfEmpID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"EmpID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Employee Code">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="EmpCode" runat="server"  Text='<%# Bind("EmpCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Employee Name" SortExpression="Name">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                <%--36732-Ambar-Change Alignment from Center to Left--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="Name" runat="server"  Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Cost Code" SortExpression="CostCode">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                <%--36732-Ambar-Change Alignment from Center to Left--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="CostCode"  runat="server" Text='<%# Bind("CostCode") %>' ReadOnly="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--36732-Ambar-Start--%>
                            
                            <asp:TemplateField HeaderText="Billing" SortExpression="Billing">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" Width="50px" />
                                <HeaderStyle HorizontalAlign="center" Wrap="false" Width="50px"/>
                                <ItemTemplate>
                                    <asp:Label ID="Billing" runat="server"  Text='<%# Bind("Billing") %>' ReadOnly="true" Width="50px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                          </Columns>
                    </asp:GridView>
                  </span>
                </asp:Panel>
                <%--VRP : Actual vs Budget :20 jun 2016
                Commented--%>
                 <asp:Panel id="pnlEmployeeSpecificCostCode" visible="false" runat="server">
                      <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblSelectEmployee" runat="server" Text="Select Employee" CssClass="txtstyle"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="ddlEmployeeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblCostCode" runat="server" Text="Cost Code" CssClass="txtstyle"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="DdlCostCode" runat="server" >
                                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="TrOverrideCostcode" runat="server" visible="false">
                        <td style="width: 20%"></td>
                        <td style="width: 80%" align="right" >
                            <asp:Label ID="lblOverrideCostcode" runat="server" Text="" CssClass="txtstyle"></asp:Label>
                            <asp:CheckBox ID="chkOverrideCostcode" runat="server" Text ="Add another CostCode" />
                                
                        </td>
                    </tr>
                </table>
                <span id="dataViewForEmployee" runat="server">
                    <!-- OnRowDataBound="grdvListofEmployees_RowDataBound" -->
                    <asp:GridView ID="grdvListofEmployees" runat="server" AutoGenerateColumns="False"
                        OnSorting="grdvListofEmployees_Sorting" Width="650" AllowSorting="false" OnRowCommand ="grdvListofEmployees_RowCommand" OnRowDataBound ="grdvListofEmployees_RowDataBound"
                        OnRowCreated="grdvListofEmployees_RowCreated" ShowFooter="True" EmptyDataText="No Record Found."
                        EmptyDataRowStyle-CssClass="Messagetext" style="margin-left:30px; margin-top:30px">
                        <HeaderStyle CssClass="headerStyle" />
                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                        <RowStyle Height="20px" CssClass="textstyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select All">
                                <HeaderStyle HorizontalAlign="center" Width="85px" />
                                <HeaderTemplate >
                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Text="Select All" />
                                </HeaderTemplate>
                                 <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                     <span id="spanChkSelect" runat="server">
                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="Check_Click(this)" />
                                     <asp:HiddenField ID="HfPrjID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ProjectID") %>' />
                                     <%--<asp:HiddenField ID="HfEmpCCId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"EmpCCId") %>' />--%>
                                    <%--AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged"--%>
                                   </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                           <%-- <asp:TemplateField HeaderText="Project Code">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="EmpCode" runat="server" Text='<%# Bind("EmpCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            
                            <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                <%--36732-Ambar-Change Alignment from Center to Left--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="ProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                    <asp:HiddenField ID="hfldProjectId"  runat="server" Value='<%# Bind("ProjectId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Cost Code" SortExpression="CostCode">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                <%--36732-Ambar-Change Alignment from Center to Left--%>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="CostCode"  runat="server" Text='<%# Bind("CostCode") %>' ></asp:Label>
                                    <asp:HiddenField ID="hfldCostcodeId"  runat="server" Value='<%# Bind("CostCodeId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--36732-Ambar-Start--%>
                            <asp:TemplateField HeaderText="Billing" SortExpression="Billing">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" Width="50px"/>
                                <HeaderStyle HorizontalAlign="center" Wrap="false" Width="50px" />
                                <ItemTemplate>
                                    <asp:Label ID ="Billing" runat="server" Text='<%# Bind("Billing") %>' ></asp:Label>
                                    <%--<asp:TextBox ID ="TextBox1" runat="server" Text='<%# Bind("Billing") %>' Width="50px" 
                                    onkeypress="return isNumberKey(event)" ReadOnly="true" ></asp:TextBox>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgDeleteCC" ImageUrl="~/Images/reject.jpg"
                                    ToolTip="Delete costcode" Width="20px" Height="20px" CommandName="DeleteCostcode" OnClientClick ="return confirm('Are you sure, you want to delete?')"
                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"EmpCCId") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                          </Columns>
                    </asp:GridView>
                    
                    
                    
                    <span id="spanPartiallyBilledEmployees" runat="server" visible="false">
                    <asp:GridView ID="grdvListofPartiallyBilledEmployees" runat="server" AutoGenerateColumns="False" 
                                    OnRowDataBound="gvCostCode_RowDataBound"  OnRowCommand="gvCostCode_RowCommand" 
                                     GridLines="None" Width="100%" >
                           
                                <HeaderStyle CssClass="headerStyle" />
                                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                <RowStyle Height="20px" CssClass="textstyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Project" Visible="false">
                                         <ItemTemplate>
                                         <!-- ItemStyle-Width="160px" HeaderStyle-Width="200px" -->
                                           <%-- <asp:HiddenField ID="HFSkillNo" runat="server" Value='<%#Eval("ProjectNo") %>' />    --%>
                                            <asp:HiddenField ID="HFSkillName" runat="server" Value='<%#Eval("ProjectName") %>' />    
                                            <span id="spanzProject" runat="server">
                                            <asp:HiddenField ID="ddlProject" runat="server" Value='<%#Eval("ProjectName") %>'  />
                                            <!--ToolTip="Select Project" Width="200px"
                                            OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AutoPostBack="true" Enabled="false" -->
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost Code" ItemStyle-Width="200px" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                        <asp:HiddenField ID="HFSkillNo" runat="server" Value='<%#Eval("ProjectNo") %>' />  
                                           <label id="lblmandatorymarkCostCode" class="mandatorymark" runat="server" visible="false">
                                                    *</label> 
                                           <span id="spanzCostCode" runat="server">
                                           <asp:TextBox ID="txtCostCode" runat="server" ToolTip="Enter Cost Code"  Text= '<%#Eval("CostCode") %>'  Width="150px" />
                                           </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Billing" ItemStyle-Width="200px" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                         <label id="lblmandatorymarkBilling" class="mandatorymark" runat="server" visible="false">
                                                    *</label> 
                                                    <span id="spanzBilling" runat="server">
                                           <asp:TextBox ID="txtBilling" runat="server" ToolTip="Enter Billing" MaxLength="3"  Text= '<%#Eval("Billing") %>'  Width="150px" />
                                           </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                                CommandName="DeleteRow" ToolTip="Delete Row. Click on Submit button to Save changes." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                      <table style="width: 100%">
                         <tr>
                             <td align="center">
                                 <asp:Button ID="btnAddRow" runat="server" Text="Add New Row" OnClick="btnAddRow_Click" Visible="false"
                              CssClass="button" />
                          </td>
                        </tr>
                     </table>
                    </span>
                  </span>
                </asp:Panel>
                
                   <!-- Siddharth Common Save Functionality Start-->
                    <table border="0">
                        <tr>
                            <td style="width: 60%">
                            </td>
                            <td style="width: 15%" align="right">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="button" OnClick="btnSave_Click"
                                    />
                                    <!-- Commented By Siddharth -->
                                    <!-- OnClientClick="return CheckIfRowChecked()" -->
                            </td>
                            <td style="width: 15%" align="center">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click" />
                            </td>
                            <td style="width: 10%" align="left">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" OnClientClick="CloseDialog();" />
                            </td>
                        </tr>
                    </table>
                    
                   <!-- Siddharth Common Save Functionality End-->
                    
        <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnClose" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    </div>
    </form>

    <script type="text/javascript">
        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        var retVal;
        (function($) {
            $(document).ready(function() {
                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        if (e.data.indexOf(",") != -1) {
                            retVal = e.data;
                        }
                    }
                });
            });
        })(jQuery);

        function CloseDialog() {
            $.modalDialog.getCurrent().close();
        };
        //Umesh: Issue 'Modal Popup issue in chrome' Ends


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function CheckIfRowChecked() {
            var valid = false;
            // var gv = document.getElementById("<%=grdvListofProjects.ClientID%>");
            var gv = document.getElementById("<%=grdvListofProjects.ClientID%>").getElementsByTagName("input"); //get only input elements
            if (gv != null) {
                // for (var i = 0; i < gv.all.length; i++) {     //all method of javascript does not support latest browser.
                //  var node = gv.all[i];
                for (var i = 0; i < gv.length; i++) {
                    var node = gv[i];
                    if (node != null && node.type == "checkbox" && node.checked) {
                        valid = true;
                        break;
                    }
                }
                if (!valid) {
                    alert("Please select at least one record.");
                    // if (gv.all.length > 0 && gv.outerText != "No Record Found.") {   //all method of javascript does not support latest browser
                    if (gv.length > 0 && gv.outerText != "No Record Found.") {
                      
                    }
                    return valid;
                }
            } else {
                alert("Please select at least one record.");
                return valid;
            }
        };

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        };
        //Siddharth 6th May 2015 Start
        //Checkbox Check-Uncheck event handled here
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox") {
                    if (!inputList[i].checked == true) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        };
        //Siddharth 6th May 2015 End
        
        
        function Check_ClickOld(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        };


        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        function popUpFunctionalManagerSearch() {
            var valReturned;
            var EmpId;
            var iFlag = CheckIfRowChecked();
            if (iFlag != false) {
                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                    onclose: function(e) {
                        valReturned = retVal;
                        var EmpName;
                        var employee = new Array();
                        if (valReturned != undefined)
                            employee = valReturned.split(",");

                        if (employee != null && (employee.length - 1) > 1) {
                            alert("Please select only one Functional Manager.")
                            return;
                        }


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
                        }
                        else {
                        }

                        var gvDrv = document.getElementById("<%= grdvListofProjects.ClientID %>");

                        for (iRow = 1; iRow <= gvDrv.rows.length - 1; iRow++) {
                            if (iRow != gvDrv.rows.length - 1) {
                                var cltRow = iRow + 1;
                                var empIdEmp;
                                if (cltRow <= 9) {
                                    //if row is checked and emp name is not null then 
                                    if (document.getElementById("grdvListofProjects_ctl0" + cltRow + "_txtFMToName") != null && document.getElementById("grdvListofProjects_ctl0" + cltRow + "_txtFMToName").disabled == false && document.getElementById("grdvListofProjects_ctl0" + cltRow + "_txtFMToName") != null && EmpName != null && EmpName != "" && document.getElementById("grdvListofProjects_ctl0" + cltRow + "_chkSelect").checked) {

                                        //new code added - if more than two row for same employee then reflect functional manager name for both row regardless checkbox selected or not.
                                        empIdEmp = document.getElementById("grdvListofProjects_ctl0" + cltRow + "_HfEmpId").value;

                                        //find out selected employee from all grid and replace functional manager
                                        for (iRowIn = 1; iRowIn <= gvDrv.rows.length - 1; iRowIn++) {

                                            if (iRowIn != gvDrv.rows.length - 1) {

                                                var cltRowIn = iRowIn + 1;

                                                if (cltRowIn <= 9) {
                                                    if (empIdEmp == document.getElementById("grdvListofProjects_ctl0" + cltRowIn + "_HfEmpId").value) {

                                                        document.getElementById("grdvListofProjects_ctl0" + cltRowIn + "_txtFMToName").value = EmpName;
                                                        document.getElementById("grdvListofProjects_ctl0" + cltRowIn + "_HfFunctionalToName").value = EmpId;
                                                    }
                                                }
                                                else {
                                                    if (empIdEmp == document.getElementById("grdvListofProjects_ctl" + cltRowIn + "_HfEmpId").value) {

                                                        document.getElementById("grdvListofProjects_ctl" + cltRowIn + "_txtFMToName").value = EmpName;
                                                        document.getElementById("grdvListofProjects_ctl" + cltRowIn + "_HfFunctionalToName").value = EmpId;
                                                    }
                                                }
                                            }
                                        }
                                        //end
                                        //document.getElementById("grdvListofProjects_ctl0" + cltRow + "_txtFMToName").value = EmpName;
                                        //document.getElementById("grdvListofProjects_ctl0" + cltRow + "_HfFunctionalToName").value = EmpId;
                                    }
                                }
                                else {
                                    if (document.getElementById("grdvListofProjects_ctl" + cltRow + "_txtFMToName") != null && document.getElementById("grdvListofProjects_ctl" + cltRow + "_txtFMToName").disabled == false && document.getElementById("grdvListofProjects_ctl" + cltRow + "_txtFMToName") != null && EmpName != null && EmpName != "" && document.getElementById("grdvListofProjects_ctl" + cltRow + "_chkSelect").checked) {
                                        empIdEmp = document.getElementById("grdvListofProjects_ctl" + cltRow + "_HfEmpId").value;

                                        for (iRowInside = 1; iRowInside <= gvDrv.rows.length - 1; iRowInside++) {

                                            if (iRowInside != gvDrv.rows.length - 1) {

                                                var cltRowInside = iRowInside + 1;


                                                if (cltRowInside <= 9) {
                                                    if (empIdEmp == document.getElementById("grdvListofProjects_ctl0" + cltRowInside + "_HfEmpId").value) {
                                                        document.getElementById("grdvListofProjects_ctl0" + cltRowInside + "_txtFMToName").value = EmpName;
                                                        document.getElementById("grdvListofProjects_ctl0" + cltRowInside + "_HfFunctionalToName").value = EmpId;
                                                    }
                                                }
                                                else {
                                                    if (empIdEmp == document.getElementById("grdvListofProjects_ctl" + cltRowInside + "_HfEmpId").value) {
                                                        document.getElementById("grdvListofProjects_ctl" + cltRowInside + "_txtFMToName").value = EmpName;
                                                        document.getElementById("grdvListofProjects_ctl" + cltRowInside + "_HfFunctionalToName").value = EmpId;
                                                    }
                                                }
                                            }
                                        }
                                        //document.getElementById("grdvListofProjects_ctl" + cltRow + "_txtFMToName").value = EmpName;
                                        //document.getElementById("grdvListofProjects_ctl" + cltRow + "_HfFunctionalToName").value = EmpId;
                                    }
                                }
                            }
                        }
                    }
                }).open();
            }
        };

        function popUpEmployeeSearch() {
            var valReturned;
            var iFlag = CheckIfRowChecked();
            if (iFlag != false) {
                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                    onclose: function(e) {
                        valReturned = retVal;
                        var EmpName;
                        var EmpId;
                        var employee = new Array();
                        if (valReturned != undefined)
                            employee = valReturned.split(",");


                        if (employee != null && (employee.length - 1) > 1) {
                            alert("Please select only one Line Manager.")
                            return;
                        }

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
                       }
                        else {
                        }

                        var gvDrv = document.getElementById("<%= grdvListofProjects.ClientID %>");

                        for (iRow = 1; iRow <= gvDrv.rows.length - 1; iRow++) {

                            if (iRow != gvDrv.rows.length - 1) {

                                var cltRow = iRow + 1;



                                if (cltRow <= 9) {

                                    if (document.getElementById("grdvListofProjects_ctl0" + cltRow + "_txtRMToName").disabled == false && EmpName != null && EmpName != "" && document.getElementById("grdvListofProjects_ctl0" + cltRow + "_chkSelect").checked) {
                                        document.getElementById("grdvListofProjects_ctl0" + cltRow + "_txtRMToName").value = EmpName;
                                        document.getElementById("grdvListofProjects_ctl0" + cltRow + "_HfReportingToName").value = EmpId;
                                    }
                                }
                                else {
                                    if (document.getElementById("grdvListofProjects_ctl" + cltRow + "_txtRMToName").disabled == false && EmpName != null && EmpName != "" && document.getElementById("grdvListofProjects_ctl" + cltRow + "_chkSelect").checked) {
                                        document.getElementById("grdvListofProjects_ctl" + cltRow + "_txtRMToName").value = EmpName;
                                        document.getElementById("grdvListofProjects_ctl" + cltRow + "_HfReportingToName").value = EmpId;
                                    }
                                }
                            }
                        }
                    }
                }).open();
            }
        };

        function popUpEmployeeSearchFMIndv(strFM) {
            var valReturned;
            var flag = false;
            var row = parseInt(strFM) + 2;

            if (row <= 9) {
                flag = document.getElementById("grdvListofProjects_ctl0" + row + "_chkSelect").checked;
            }
            else
                flag = document.getElementById("grdvListofProjects_ctl" + row + "_chkSelect").checked;

            if (flag) {
                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                    onclose: function(e) {
                        valReturned = retVal;
                        var EmpName;
                        var EmpId;
                        var employee = new Array();
                        if (valReturned != undefined)
                            employee = valReturned.split(",");

                        if (employee != null && (employee.length - 1) > 1) {
                            alert("Please select only one Functional Manager.")
                            return;
                        }

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
                            //document.getElementById("grdvListofProjects_ctl03_txtRMToName").value = EmpName;
                            if (row <= 9) {
                                if (EmpName != null && EmpName != "") {
                                    document.getElementById("grdvListofProjects_ctl0" + row + "_txtFMToName").value = EmpName;
                                    document.getElementById("grdvListofProjects_ctl0" + row + "_HfFunctionalToName").value = EmpId;
                                }
                            }
                            else {
                                if (EmpName != null && EmpName != "") {
                                    document.getElementById("grdvListofProjects_ctl" + row + "_txtFMToName").value = EmpName;
                                    document.getElementById("grdvListofProjects_ctl" + row + "_HfFunctionalToName").value = EmpId;
                                }
                            }
                        }

                        //Sanju:Issue Id 50201
                        //Added condition for EmpName not equal to undefined since other browsers were taking undefined value if we wont select any employee.
                        if (EmpName != undefined) {
                            //new code added
                            // find emp Id which is selected
                            var empIdEmp;

                            var gvDrv = document.getElementById("<%= grdvListofProjects.ClientID %>");
                            if (row <= 9) {
                                empIdEmp = document.getElementById("grdvListofProjects_ctl0" + row + "_HfEmpId").value;
                            } else {
                                empIdEmp = document.getElementById("grdvListofProjects_ctl" + row + "_HfEmpId").value;
                            }

                            // find same employee name in all grid view and replace functional manager name
                            for (iRow = 1; iRow <= gvDrv.rows.length - 1; iRow++) {

                                if (iRow != gvDrv.rows.length - 1) {

                                    var cltRow = iRow + 1;


                                    if (cltRow <= 9) {
                                        if (empIdEmp == document.getElementById("grdvListofProjects_ctl0" + cltRow + "_HfEmpId").value) {

                                            //alert(document.getElementById("grdvListofProjects_ctl0" + cltRow + "_HfEmpId").value);

                                            document.getElementById("grdvListofProjects_ctl0" + cltRow + "_txtFMToName").value = EmpName;
                                            document.getElementById("grdvListofProjects_ctl0" + cltRow + "_HfFunctionalToName").value = EmpId;
                                        }
                                    }
                                    else {
                                        if (empIdEmp == document.getElementById("grdvListofProjects_ctl" + cltRow + "_HfEmpId").value) {

                                            //alert(document.getElementById("grdvListofProjects_ctl" + cltRow + "_HfEmpId").value);

                                            document.getElementById("grdvListofProjects_ctl" + cltRow + "_txtFMToName").value = EmpName;
                                            document.getElementById("grdvListofProjects_ctl" + cltRow + "_HfFunctionalToName").value = EmpId;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }).open();
            }
            else {
                alert("Please select checkbox first");
            }
        };

        function popUpEmployeeSearchRMIndv(strRM) {

            var valReturned;
            //var retVal;
            var flag = false;
            var row = parseInt(strRM) + 2;


            if (row <= 9) {
                flag = document.getElementById("grdvListofProjects_ctl0" + row + "_chkSelect").checked;
            }
            else
                flag = document.getElementById("grdvListofProjects_ctl" + row + "_chkSelect").checked;

            if (flag) {

                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                    onclose: function(e) {
                        valReturned = retVal;
                        var EmpName;
                        var EmpId;
                        var employee = new Array();
                        if (valReturned != undefined)
                            employee = valReturned.split(",");
                        //Sanju:Issue Id 50201:Added condition for employee equal to blank
                        if (employee != null && (employee.length - 1) > 1 && employee != "") {
                            alert("Please select only one Line Manager.")
                            return;
                        }

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
                            //document.getElementById(strRM).value = "";
                        }
                        else {
                            if (row <= 9) {
                                if (EmpName != null && EmpName != "") {
                                    document.getElementById("grdvListofProjects_ctl0" + row + "_txtRMToName").value = EmpName;
                                    document.getElementById("grdvListofProjects_ctl0" + row + "_HfReportingToName").value = EmpId;
                                }
                            }
                            else {
                                if (EmpName != null && EmpName != "") {
                                    document.getElementById("grdvListofProjects_ctl" + row + "_txtRMToName").value = EmpName;
                                    document.getElementById("grdvListofProjects_ctl" + row + "_HfReportingToName").value = EmpId;
                                }
                            }
                        }
                    }
                }).open();
            }
            else {
                alert("Please select checkbox first");
            }
        };
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        
    </script>

</body>
</html>
