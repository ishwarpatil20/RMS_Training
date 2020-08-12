<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SkillsList.aspx.cs" Inherits="SkillsList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Skills List</title>
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery.modalDialogContent.css" rel="stylesheet" type="text/css" />
    
    <script src="JavaScript/CommonValidations.js" type="text/javascript"></script>
    <script src="JavaScript/FilterPanel.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinnyContent.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialogContent.js" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
    <cc1:toolkitscriptmanager id="ScriptManagerMaster" runat="server">
    </cc1:toolkitscriptmanager>
    <asp:UpdatePanel ID="upSkillslist" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMessage" runat="server" CssClass="text" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td width="15%" align="left">
                            <asp:Label ID="lblSkillsName" runat="server" Text="Skills" CssClass="txtstyle"></asp:Label>
                        </td>
                        <td width="40%" align="left">
                            <asp:TextBox ID="txtSkillsName" runat="server" Width="80%" ToolTip="Select Skills"></asp:TextBox>
                        </td>
                        <td width="40%" align="left">
                            <asp:Button ID="btnListofSkills" Text="Search" runat="server" CssClass="button"
                                OnClick="btnListofSkills_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
                <table width="50%" cellspacing="0" cellpadding="0">
                    <asp:GridView ID="grdvListofSkill" runat="server" AutoGenerateColumns="False"
                        Width="500" AllowPaging="True" AllowSorting="True" 
                        DataKeyNames="SkillId" OnPageIndexChanging="grdvListofSkill_PageIndexChanging"
                        OnRowDataBound="grdvListofSkill_RowDataBound" ShowFooter="True" EmptyDataText="No Record Found."
                        EmptyDataRowStyle-CssClass="Messagetext">
                        <HeaderStyle CssClass="headerStyle" />
                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                        <RowStyle Height="20px" CssClass="textstyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <HeaderStyle HorizontalAlign="center"/>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false" />
                                    <asp:HiddenField ID="hfSkillsID" runat="server" Value='<%# Bind("SkillId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Skill" HeaderText="Skills" SortExpression="Skill">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="center" />
                            </asp:BoundField>
                        </Columns>

                    </asp:GridView>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="button" OnClick="btnSelect_Click" />
                            <asp:HiddenField ID="hidSkillsCount" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" OnClick="btnClose_Click" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hidPageName" runat="server" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnListofSkills" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSelect" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClose" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </form>

    <script language="javascript" type="text/javascript">
        function $(v) { return document.getElementById(v); }

        function FnCheck(chkid, skillscount) {
            var number = 0;
            var CheckBoxID = $(chkid).checked;
            var HidEmployee = $(skillscount).value;
            var Counter = 0;

            if ($('<%=hidSkillsCount.ClientID %>').value == "") {
                Counter = 0;
            }
            else {
                Counter = $('<%=hidSkillsCount.ClientID %>').value;
                number = parseInt(Counter);
            }
            if (CheckBoxID == true) {
                number = number + 1;

            }
            else {
                number = number - 1;
            }

            $('<%=hidSkillsCount.ClientID %>').value = number;
        }
            
        function Validate() {
            if ($('<%=hidSkillsCount.ClientID %>').value == "" || $('<%=hidSkillsCount.ClientID %>').value == '0') {
                alert("Please Select the Record.");
                return false;
            }
        }
    </script>

</body>
</html>

