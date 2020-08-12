<%@ Page Language="C#" AutoEventWireup="true" CodeFile="listOfProject.aspx.cs" Inherits="listOfProject" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Summary</title>
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="JavaScript/CommonValidations.js" type="text/javascript">
    </script>

    <script type="text/javascript" src="JavaScript/FilterPanel.js">
    </script>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <link href="CSS/jquery.modalDialogContent.css" rel="stylesheet" type="text/css" />
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinnyContent.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialogContent.js" type="text/javascript"></script>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->

    <script type="text/javascript">

        function CheckFilter(isButtonClicked) {
            var txtName = document.getElementById('<% = txtProjectName.ClientID %>');
            var ddlClientName = document.getElementById('<% = ddlClientName.ClientID %>');
            if (document.getElementById('<% = ddlStatus.ClientID %>') != null) {
                var ddlStatus = document.getElementById('<% = ddlStatus.ClientID %>');
            }
            var btnFilter = document.getElementById('<% = btnFilter.ClientID %>');
            if (document.getElementById('<% = ddlStatus.ClientID %>') != null) {
                if ((txtName != null) && (ddlClientName != null) && (ddlStatus != null)) {

                    if ((trim(txtName.value) == "") && (ddlClientName.value == "Select") && (ddlStatus.value == "Select")) {
                        txtName.value = "";

                        if (isButtonClicked)
                            alert("Please select or enter any criteria, to proceed with filter.");

                        return false;
                    }
                }
            }

            if (document.getElementById('<% = ddlStatus.ClientID %>') == null) {
                if ((txtName != null) && (ddlClientName != null)) {
                    if ((trim(txtName.value) == "") && (ddlClientName.value == "Select")) {
                        txtName.value = "";

                        if (isButtonClicked)
                            alert("Please select or enter any criteria, to proceed with filter.");

                        return false;
                    }
                }
            }

            if ((txtName != null) && (ddlClientName != null) && (ddlStatus != null)) {

                if ((trim(txtName.value) == "") && (ddlClientName.value == "Select") && (ddlStatus.value == "Select")) {
                    txtName.value = "";

                    if (isButtonClicked)
                        alert("Please select or enter any criteria, to proceed with filter.");

                    return false;
                }
            }
        }

    </script>

    <script type="text/javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function ClearFilter() {
            var txtProjectName = document.getElementById('<%=txtProjectName.ClientID %>');
            var ddlClientName = document.getElementById('<%=ddlClientName.ClientID %>');
            var ddlStatus = document.getElementById('<%=ddlStatus.ClientID %>');

            txtProjectName.value = "";
            txtProjectName.focus();

            ddlClientName.selectedIndex = 0;

            if (ddlStatus != null) {
                ddlStatus.selectedIndex = 0;
            }

            return false;
        }

        function DenySpecialChar() {
            if (event.keyCode == 13) {
                var txtProjectName = document.getElementById('<%=txtProjectName.ClientID %>');
                if (txtProjectName.value == "") {
                    CheckFilter(false);
                }
                else {
                    var btnFilter = document.getElementById('<%=btnFilter.ClientID %>');
                    btnFilter.click();
                }
            }
            if ((event.keyCode > 32 && event.keyCode < 45) || (event.keyCode > 46 && event.keyCode < 48) || (event.keyCode > 57 && event.keyCode < 65) || (event.keyCode > 90 && event.keyCode < 97) || (event.keyCode > 122 && event.keyCode < 127))
                event.returnValue = false;

        }

        function hideProejctStage() {
            var ddlStatus = document.getElementById('<%=ddlStatus.ClientID %>');
        }
        
    </script>

    <script type="text/javascript" language="javascript">
        var isRadioSeleted = '';
        function Selected() {
            isRadioSeleted = "yes";
        }

        function isNumberKey(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function RadioChecked1() {
            {
                var radioSelected = false;
                var radio = false;
                var frm = document.forms[0];
                // Take all elements of the form
                for (i = 0; i < frm.length; i++) {
                    // itinerate the elements searching "RadioButtons"
                    if (frm.elements[i].type == "radio") {
                        radio = true;
                        // Unchecked if the RadioButton is != param
                        if (frm.elements[i].checked) {
                            radioSelected = true;

                            // window.close();
                        }
                    }
                }
                if (radio) {
                    if (!radioSelected) {
                        alert("Please select a project.");
                        return false;
                    }
                    else {
                        //window.close();
                    }
                }
                else {
                    alert("Please search for required project");
                    return false;
                }
            }
        }
    </script>

    <style type="text/css">
        .style1
        {
            height: 25px;
        }
        .style2
        {
            width: 714px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <cc1:ToolkitScriptManager ID="ScriptManagerMaster" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upListOfProjects" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->        
            <table width="100%">
                <tr>
                    <td align="center" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');"
                        class="style1" colspan="2">
                        <asp:Label ID="Label1" runat="server" Text="List Of Projects" CssClass="header">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                    </td>
                    <td style="height: 19px; width: 200px;">
                        <div id="shelf" class="filter_main" style="width: 196px;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <%--   Sanju:Issue Id 50201:Changed cursor value to pointer--%>
                                <tr style="cursor: pointer;" onclick="javascript:activate_shelf();">
                                    <%--  Sanju:Issue Id 50201:End--%>
                                    <td class="filter_title header_filter_title" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr= '#5C7CBD' , startColorstr= '#12379A' , gradientType= '1' );">
                                        <a id="control_link" href="javascript:activate_shelf();" style="color: White;"><b>Filter</b></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="shelf_contents" class="filter_content" style="border: solid 1px black; text-align: center;
                                            width: 189px;">
                                            <table style="text-align: left; left: 541px; top: 282px;" cellpadding="1">
                                                <tr>
                                                    <td style="width: 155px;">
                                                        <asp:Label ID="lblName" runat="server" Text="Project Name" Font-Size="9pt"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 155px">
                                                        <asp:TextBox ID="txtProjectName" runat="server" ToolTip="Enter Project Name" Width="149px"
                                                            MaxLength="30" onkeypress="return DenySpecialChar();"></asp:TextBox><br />
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
                                                <tr id="trddlStatus" runat="server" visible="true">
                                                    <td style="width: 155px">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Verdana" Width="155px"
                                                            ToolTip="Select Project Status" onchange=" return hideProejctStage();">
                                                        </asp:DropDownList>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr id="trlblWorkFlowStatus" runat="server" visible="false">
                                                    <td style="width: 155px;">
                                                        <asp:Label ID="lblWorkFlowStatus" runat="server" Text="Project Stage" Font-Size="9pt"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr id="trddlWorkFlowStatus" runat="server" visible="false">
                                                    <td style="width: 155px">
                                                        <asp:DropDownList ID="ddlWorkFlowStatus" runat="server" ToolTip="Select Project Stage"
                                                            Width="155px">
                                                        </asp:DropDownList>
                                                        <br />

                                                        <script type="text/javascript">                                                    hideProejctStage();</script>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 155px;">
                                                        <br />
                                                        <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click"
                                                            OnClientClick="return CheckFilter(true);" CssClass="button" Width="70px" Font-Bold="True"
                                                            Font-Size="9pt" />
                                                        &nbsp;
                                                        <asp:Button ID="btnClear" OnClientClick="return ClearFilter()" runat="server" Text="Clear"
                                                            CssClass="button" Width="69px" Font-Bold="True" Font-Size="9pt" />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 155px;">
                                                        <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove Filter" OnClick="btnRemoveFilter_Click"
                                                            CssClass="button" Width="120px" Font-Bold="True" Font-Size="9pt" />
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
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="grdvListofProjects" runat="server" AutoGenerateColumns="False"
                AllowPaging="True" AllowSorting="true" GridLines="Both" Width="100%" OnPageIndexChanging="grdvListofProjects_OnPageIndexChanging"
                OnSorting="grdvListofProjects_Sorting" OnRowCreated="grdvListofProjects_RowCreated">
                <PagerSettings Position="Bottom" />
                <HeaderStyle CssClass="addrowheader" />
                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                <RowStyle CssClass="textstyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select" ControlStyle-Width="15px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%--                          <asp:RadioButton runat="server" ID="rdListOfProj"   AutoPostBack="true" Checked="false"  GroupName="selectProject" OnCheckedChanged=""/>
--%>
                            <input name="CommonRow" type="radio" id="rdoButton" onclick="Selected()" value='<%# Eval("ProjectCode") %>' />
                            <input type="hidden" id="hidProjectId" runat="server" value='<%#Eval("ProjectID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Code" SortExpression="PROJECTCODE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblProjectCode" align="center" Text='<%#Eval("ProjectCode") %>'></asp:Label></td>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Name" SortExpression="PROJECTNAME">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblProjectName" align="center" Text='<%#Eval("ProjectName") %>'></asp:Label></td>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Client Name" SortExpression="ClientName">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblClientName" align="center" Text='<%#Eval("ClientName") %>'></asp:Label></td>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contract Code" SortExpression="ContractCode">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblContractCode" align="center" Text='<%#Eval("ContractCode") %>'></asp:Label></td>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Document Name" SortExpression="DocumentName">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDocumentName" align="center" Text='<%#Eval("DocumentName") %>'></asp:Label></td>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type of Contract" SortExpression="Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblContractType" align="center" Text='<%#Eval("ContractType") %>'></asp:Label></td>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                    <table class="tablePager">
                        <tr>
                            <td align="center">
                                <span class="txtstyle">&lt;&lt;&nbsp;&nbsp;</span>
                                <asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white" CommandName="Previous"
                                    OnCommand="ChangePage" Font-Underline="true" Enabled="False" CssClass="txtstyle">Previous</asp:LinkButton>
                                <span class="txtstyle">Page</span>
                                <asp:TextBox ID="txtPages" runat="server" OnTextChanged="txtPages_TextChanged" AutoPostBack="true"
                                    Width="21px" MaxLength="3" onpaste="return false;" CssClass="txtstyle"></asp:TextBox>
                                <span class="txtstyle">of</span>
                                <asp:Label ID="lblPageCount" runat="server" ForeColor="white" CssClass="txtstyle"></asp:Label>
                                <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                    OnCommand="ChangePage" Font-Underline="true" Enabled="False" CssClass="txtstyle">Next</asp:LinkButton><span
                                        class="txtstyle">&nbsp;&nbsp;&gt;&gt;</span>
                            </td>
                        </tr>
                    </table>
                </PagerTemplate>
            </asp:GridView>
            <table width="100%">
                <%--<tr>
            <td align="center" style="height:25px;filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <asp:Label ID="lblListOfProjects" runat="server" Text="List Of Projects"  CssClass = "header">
                </asp:Label>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>--%>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnOK" runat="server" Text="OK" Width="119px" CssClass="button" TabIndex="2"
                                OnClick="btnOK_Click" />
                            &nbsp
                            <asp:Button ID="btnCancle" runat="server" Text="Cancel" Width="119px" CssClass="button"
                                TabIndex="3" OnClick="btnCancle_Click" />
                        </td>
                    </tr>
            </table>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnOK" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancle" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    </form>
</body>
</html>
