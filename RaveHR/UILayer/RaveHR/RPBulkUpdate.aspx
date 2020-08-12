<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPBulkUpdate.aspx.cs" Inherits="RPBulkUpdate" %>

<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk RP Update</title>
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />
    <script src="JavaScript/DatePicker.js" type="text/javascript"></script>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <link href="CSS/jquery.modalDialogContent.css" rel="stylesheet" type="text/css" />
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinnyContent.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialogContent.js" type="text/javascript"></script>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->

    <script type="text/javascript">

        function checkAll(cb) {
            var ctrls = document.getElementsByTagName('input');
            for (var i = 0; i < ctrls.length; i++) {
                var cbox = ctrls[i];
                if (cbox.type == "checkbox") {
                    cbox.checked = cb.checked;
                }
            }
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
     <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <%--<asp:ScriptManager ID="ScriptManagerMaster" runat="server">
    </asp:ScriptManager>--%>
   
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="updesignation" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
            <table cellpadding="0" align="center" cellspacing="0" border="0">
                <tr>
                    <td style="padding-left: 2px; padding-right: 2px; padding-bottom: 2px;">
                        <table cellpadding="0" align="center" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                                    background-color: #7590C8">
                                    <span class="header">RP Update</span>
                                    <asp:DropDownList runat="server" ID="ddlLocation" Visible="false">
                                    </asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="ddlProjectLocation" Visible="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblMessage" CssClass="txtstyle" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="100%" style="padding-left: 2px; padding-right: 2px; padding-bottom: 2px;">
                        <table cellpadding="0" align="center" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="txtstyle" align="left" style="width: 10%; padding-left: 5px;">
                                    End Date<span class="mandatorymark">*</span>
                                </td>
                                <td width="2%">
                                    &nbsp;
                                </td>
                                <td align="left" style="width: 20%">
                                    <uc1:DatePicker ID="ucDatePickerEndDate" runat="server" Width="80" />
                                    <span id="spanResourceDurationEndDateTooltip" runat="server">
                                <img id="imgErrorResourceDurationEndDate" runat="server" src="Images/cross.png" alt=""
                                    style="display: none; border: none;" />
                            </span>
                                </td>
                                <td style="width: 68%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" width="100%" style="padding-left: 2px; padding-right: 2px; padding-bottom: 2px;">
                        <div id="dvData" style="overflow: auto; width: 985px; height: 410px; text-align: center;
                            border: 1px solid #1A6976;">
                            <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr class="repeaterheader">
                                            <th align="center" style="width: 3%;">
                                                <asp:CheckBox ID="chkAll" runat="server" OnClick="checkAll(this)" />
                                            </th>
                                            <th align="center" style="width: 3%;">
                                                <span>ID</span>
                                            </th>
                                            <th align="left" style="width: 15%; padding-left: 5px;">
                                                <span>Role</span>
                                            </th>
                                            <th align="left" style="width: 20%; padding-left: 5px;">
                                                <span>Resource Name</span>
                                            </th>
                                            <th align="left" style="width: 29%; padding-left: 5px;">
                                                <span>MRF Code</span>
                                            </th>
                                            <th align="left" style="width: 10%; padding-left: 5px;">
                                                <span>MRF Status</span>
                                            </th>
                                            <th align="center" style="width: 10%;">
                                                <span>Start Date</span>
                                            </th>
                                            <th align="center" style="width: 10%;">
                                                <span>End Date</span>
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="repeaterrow">
                                        <td align="center" style="width: 3%;">
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                            <asp:HiddenField ID="hfRPDurationId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ResourcePlanDurationId") %>' />
                                            <asp:HiddenField ID="hfRPDetailId" runat="server" />
                                            <asp:HiddenField ID="hfUtilization" runat="server" />
                                            <asp:HiddenField ID="hfBilling" runat="server" />
                                            <asp:HiddenField ID="hfResourceLocation" runat="server" />
                                            <asp:HiddenField ID="hfProjectLocation" runat="server" />
                                        </td>
                                        <td align="center" style="width: 3%;">
                                            <span>
                                        <%#DataBinder.Eval(Container.DataItem, "RPDuRowNo")%></span>
                                        </td>
                                        <td align="left" style="width: 15%; padding-left: 5px;">
                                            <span>
                                        <%#DataBinder.Eval(Container.DataItem, "Role")%></span>
                                        </td>
                                        <td align="left" style="width: 20%; padding-left: 5px;">
                                            <span>
                                        <%#DataBinder.Eval(Container.DataItem, "ResourceName")%></span>
                                        </td>
                                        <td align="left" style="width: 29%; padding-left: 5px;">
                                            <span>
                                        <%#DataBinder.Eval(Container.DataItem, "MRFCode")%></span>
                                        </td>
                                        <td align="left" style="width: 10%; padding-left: 5px;">
                                            <span>
                                        <%#DataBinder.Eval(Container.DataItem, "MRFStatus")%></span>
                                        </td>
                                        <td align="center" style="width: 10%;">
                                            <asp:Label ID="lblStartDate" Text='<%# String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "StartDate")) %>'
                                                runat="server"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 10%;">
                                            <span>
                                        <%# String.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container.DataItem, "EndDate")) %></span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" width="100%" style="padding-left: 2px; padding-right: 2px; padding-bottom: 2px;">
                        <table cellpadding="0" align="center" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td style="width: 50%">
                                </td>
                                <td align="right" style="width: 50%; padding-right: 10px;">
                                    <asp:Button runat="server" ID="btnUpdate" Text="RP Update" CssClass="button" OnClientClick="return confirm('Are you sure, you want to apply end date to selected records ?')"
                                        OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="button" 
                                        onclick="btnCancel_Click"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    </form>
</body>
</html>
