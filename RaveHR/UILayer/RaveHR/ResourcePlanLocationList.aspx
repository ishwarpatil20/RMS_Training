<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourcePlanLocationList.aspx.cs"
    Inherits="ResourcePlanLocationList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Resource Break-Up Duration</title>
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
</head>
<body>
    <form id="form1" runat="server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <cc1:ToolkitScriptManager ID="ScriptManagerMaster" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="updesignation" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
            <table style="width: 40%">
                <tr>
                    <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                        background-color: #7590C8">
                        <span class="header">Resource Break-Up Duration</span>
                    </td>
                </tr>
                <tr>
                    <td style="height: 1pt">
                    </td>
                </tr>
            </table>
            <table align="center">
                <asp:GridView ID="grdvResourcePlanLocationList" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" Width="40%">
                    <HeaderStyle CssClass="headerStyle" />
                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                    <RowStyle Height="20px" CssClass="textstyle" />
                    <Columns>
                        <asp:BoundField DataField="ResourcePlanStartDate" HeaderText="StartDate" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ResourcePlanEndDate" HeaderText="EndDate" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ResourceLocation" HeaderText="Location">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </table>
            <table width="100%">
                <tr>
                </tr>
                <tr>
                    <td width="20%" margin:left="130px">
                        <asp:Button ID="btnOk" runat="server" Text="Close" Width="60px" CssClass="button"
                            TabIndex="0" OnClick="btnOK_Click" />&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    function CheckOtherIsCheckedByGVID(spanChk) {

        var IsChecked = spanChk.checked;
        var CurrentRdbID = spanChk.id;
        var Chk = spanChk;
        Parent = document.getElementById('grdvListofInternalResource');
        var items = Parent.getElementsByTagName('input');
        for (i = 0; i < items.length; i++) {
            if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                if (items[i].checked) {
                    items[i].checked = false;
                }
            }
        }
    }

    function IsResourceSelected() {
        Parent = document.getElementById('grdvListofInternalResource');
        var items = Parent.getElementsByTagName('input');
        var resourceSelected = "yes";
        for (i = 0; i < items.length; i++) {
            if (items[i].type == "radio") {
                if (items[i].checked) {
                    resourceSelected = true;
                }
            }
        }

        if (resourceSelected == "yes") {
            alert("Select the Resource");
            return false;
        }
        else {
            return true;
        }
    }
</script>

