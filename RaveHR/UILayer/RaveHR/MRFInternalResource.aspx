<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MRFInternalResource.aspx.cs"
    Inherits="MRFInternalResource" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List of Internal Resource</title>
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />
    
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <link href="CSS/jquery.modalDialogContent.css" rel="stylesheet" type="text/css" />
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinnyContent.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialogContent.js" type="text/javascript"></script>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnListofInternalREsources">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <cc1:ToolkitScriptManager ID="ScriptManagerMaster" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upemplist" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->        
            <table style="width: 100%">
                <tr>
                    <%--Sanju:Issue Id 50201: Added new class header_employee_profile so that the header color is same for all browsers--%>
                    <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                        background-color: #7590C8">
                        <%--  Sanju:Issue Id 50201: End--%>
                        <span class="header">List of Internal Resource</span>
                    </td>
                </tr>
                <tr>
                    <td style="height: 1pt">
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td width="20%" align="left">
                        <asp:Label ID="lblResourceName" runat="server" Text="Resource Name" CssClass="txtstyle"></asp:Label>
                    </td>
                    <td width="40%" align="left">
                        <asp:TextBox ID="txtResourceName" runat="server" Width="70%" ToolTip="Enter Resource Name"
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td width="40%" align="left">
                        <asp:Button ID="btnListofInternalREsources" Text="Search" runat="server" CssClass="button"
                            OnClick="btnSearch_Click" TabIndex="2"></asp:Button>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="grdvListofInternalResource" runat="server" AutoGenerateColumns="False"
                AllowPaging="True" OnPageIndexChanging="grdvListofInternalResource_PageIndexChanging"
                AllowSorting="True" OnSorting="grdvListofInternalResource_Sorting" OnRowCommand="grdvListofInternalResource_RowCommand"
                Width="100%" DataKeyNames="EmployeeId">
                <HeaderStyle CssClass="headerStyle" />
                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                <RowStyle Height="20px" CssClass="textstyle" />
                <Columns>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EmployeeId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:RadioButton ID="radioId" runat="server" OnCheckedChanged="radioId_CheckedChanged"
                                onclick="javascript:CheckOtherIsCheckedByGVID(this);" GroupName="ID" />
                            <asp:HiddenField ID="hdEmpJoinigDay" Value='<%# DataBinder.Eval(Container.DataItem,"EmployeeJoiningDate") %>'
                                runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="EmployeeName" HeaderText="Resource Name" SortExpression="ResourceName">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DepartmentName" HeaderText="Designation" SortExpression="Designation">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmailId" HeaderText="EmailId" SortExpression="EmailId">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ResignationDate" HeaderText="Resignation Date" SortExpression="ResignationDate"
                        DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <SelectedRowStyle BackColor="#0099CC" />
            </asp:GridView>
            <table width="100%">
                <tr>
                    <td width="20%" align="right">
                        <asp:Button ID="btnOk" runat="server" Text="OK" Width="120px" CssClass="button" TabIndex="3"
                            OnClick="btnOk_Click" />&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                Width="120px" CssClass="button" TabIndex="4" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
            <div>
            </div>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnListofInternalREsources" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    </form>
</body>

<script language="javascript" type="text/javascript">

function CheckOtherIsCheckedByGVID(spanChk)
{
    var IsChecked = spanChk.checked;
    var CurrentRdbID = spanChk.id;
    var Chk = spanChk;
    Parent = document.getElementById('grdvListofInternalResource');
    var items = Parent.getElementsByTagName('input');
    for(i=0;i<items.length;i++)
    {
        if(items[i].id != CurrentRdbID && items[i].type=="radio"){
            if(items[i].checked){
                items[i].checked = false;}
             }
        }
      }

function IsResourceSelected()
{
    Parent = document.getElementById('grdvListofInternalResource');
    var items = Parent.getElementsByTagName('input');
    var resourceSelected="yes";
    for(i=0;i<items.length;i++)
    {
        if( items[i].type=="radio"){
            if(items[i].checked){
                resourceSelected=true;
                }
             }
        }
      
      if(resourceSelected=="yes")
      {
            alert("Select the Resource");
            return false;
      }
      else
      {
            return true;
      }
     }
</script>

</html>
