<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeesList.aspx.cs" Inherits="EmployeesList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee List</title>
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
    <asp:UpdatePanel ID="upemplist" runat="server" UpdateMode="Conditional">
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
                        <td width="25%" align="left">
                            <asp:Label ID="lblResourceName" runat="server" Text="Employee Name" CssClass="txtstyle"></asp:Label>
                        </td>
                        <td width="40%" align="left">
                            <asp:TextBox ID="txtResourceName" runat="server" Width="70%" ToolTip="Enter Employee Name"></asp:TextBox>
                        </td>
                        <td width="40%" align="left">
                            <asp:Button ID="btnListofInternalREsources" Text="Search" runat="server" CssClass="button"
                                OnClick="btnListofInternalREsources_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
                <table width="50%" cellspacing="0" cellpadding="0">
                    <asp:GridView ID="grdvListofEmployees" runat="server" AutoGenerateColumns="False"
                        Width="500" AllowPaging="True" AllowSorting="True" OnSorting="grdvListofEmployees_Sorting"
                        DataKeyNames="EMPId" OnPageIndexChanging="grdvListofEmployees_PageIndexChanging"
                        OnRowDataBound="grdvListofEmployees_RowDataBound" ShowFooter="True" EmptyDataText="No Record Found."
                        EmptyDataRowStyle-CssClass="Messagetext">
                        <HeaderStyle CssClass="headerStyle" />
                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                        <RowStyle Height="20px" CssClass="textstyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <HeaderStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EMPId" HeaderText="Employee ID" SortExpression="EMPId">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMPCode" HeaderText="Employee Code" SortExpression="EMPCode">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="FullName">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <%--////Poonam : Issue 56396 : 19/08/2015 : Starts--%>
                            <asp:BoundField DataField="Designation" HeaderText="Designation Name" SortExpression="Designation">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            
                            <%--////Poonam : Issue 56396 : 19/08/2015 : Ends--%>
                            
                            <%--<asp:BoundField DataField="EmployeeType" HeaderText="Status" SortExpression="Status">
                        <HeaderStyle Width="17%" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName">
                        <HeaderStyle Width="17%" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ClientName" HeaderText="ClientName" SortExpression="ClientName">
                        <HeaderStyle Width="11%" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role">
                        <HeaderStyle Width="11%" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation">
                        <HeaderStyle Width="11%" HorizontalAlign="Left" />
                    </asp:BoundField>--%>
                        </Columns>
                        <%--<PagerTemplate>
                    <table class="tablePager">
                        <tr>
                            <td align="center">
                                &lt;&lt;&nbsp;&nbsp;<asp:LinkButton ID="lbtnPrevious" runat="server" ForeColor="white"
                                    CommandName="Previous" OnCommand="ChangePage" Font-Underline="true" Enabled="False">Previous</asp:LinkButton>
                                <span>Page</span>
                                <asp:TextBox ID="txtPages" runat="server" OnTextChanged="txtPages_TextChanged" AutoPostBack="true"
                                    Width="21px" MaxLength="3" onpaste="return false;"></asp:TextBox>
                                <span>of</span>
                                <asp:Label ID="lblPageCount" runat="server" ForeColor="white"></asp:Label>
                                <asp:LinkButton ID="lbtnNext" runat="server" ForeColor="white" CommandName="Next"
                                    OnCommand="ChangePage" Font-Underline="true" Enabled="False">Next</asp:LinkButton>&nbsp;&nbsp;&gt;&gt;
                            </td>
                        </tr>
                    </table>
                </PagerTemplate>--%>
                    </asp:GridView>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="button" OnClick="btnSelect_Click" />
                            <asp:TextBox ID="hidEmployeeCount" runat="server" Width="0" Style="visibility: hidden;"></asp:TextBox>
                            <%--Aarohi : Issue 28572(CR) : 11/01/2012 : Start--%>
                            <%--<asp:TextBox ID="hidEMPId" runat="server" Width="0" Style="visibility: hidden;"></asp:TextBox>--%>
                            <%--Aarohi : Issue 28572(CR) : 11/01/2012 : End--%>
                            <asp:TextBox ID="hidFunctionalManager" runat="server" Width="0" Style="visibility: hidden;"></asp:TextBox>
                        </td>
                        <%-- 28512-Ambar-Start-02092011--%>
                        <td>
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" OnClick="btnClose_Click" />
                        </td>
                        <%-- 28512-Ambar-Start-02092011--%>
                    </tr>
                </table>
                <asp:HiddenField ID="hidPageName" runat="server" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnListofInternalREsources" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSelect" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClose" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </form>

    <script language="javascript" type="text/javascript">
        function $(v) { return document.getElementById(v); }

        //Aarohi : Issue 28572(CR) : 05/01/2012 : Start
        //Added a new parameter 'EmpId'
        //function FnCheck(chkid, emptext, strFunctionalManager, EMPId) {
        function FnCheck(chkid, emptext, strFunctionalManager) {
            var number = 0;
            var CheckBoxID = $(chkid).checked;
            var HidEmployee = $(emptext).value;
            var Counter = 0;
            var strFunctionalManager = strFunctionalManager;

            if ($('<%=hidEmployeeCount.ClientID %>').value == "") {
                Counter = 0;
            }
            else {
                Counter = $('<%=hidEmployeeCount.ClientID %>').value;
                number = parseInt(Counter);
            }
            if (CheckBoxID == true) {
                number = number + 1;

            }
            else {
                number = number - 1;
            }

            $('<%=hidEmployeeCount.ClientID %>').value = number;
            var strFunctionalManager = document.getElementById('<%=hidFunctionalManager.ClientID %>').value;

            if (strFunctionalManager == "") {

                //Aarohi : Issue 28572(CR) : 11/01/2012 : Start
                //Changed condition
                if ($('<%=hidPageName.ClientID %>').value == "MrfRaiseNextOrRaiseHeadCount") {


                    if ($('<%=hidEmployeeCount.ClientID %>').value > 1) {
                        number = number - 1;
                        $('<%=hidEmployeeCount.ClientID %>').value = number;
                        alert("You cannot select more than one responsible person name.");
                        $(chkid).checked = false;
                        return false;
                    }
                }
                else if ($('<%=hidEmployeeCount.ClientID %>').value > 400) {
                    number = number - 1;
                    $('<%=hidEmployeeCount.ClientID %>').value = number;
                    alert("You cannot select more than 4 responsible person name.");
                    $(chkid).checked = false;
                    return false;
                }
            }
            else {
                if ($('<%=hidEmployeeCount.ClientID %>').value > 4) {
                    number = number - 1;
                    $('<%=hidEmployeeCount.ClientID %>').value = number;
                    alert("You cannot select more than 4 Functional Manager.");
                    $(chkid).checked = false;
                    return false;
                }
            }

        }
        function Validate() {
            if ($('<%=hidEmployeeCount.ClientID %>').value == "" || $('<%=hidEmployeeCount.ClientID %>').value == '0') {
                alert("Please Select the Record.");
                return false;
            }
        }
    </script>

</body>
</html>
