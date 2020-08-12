<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MrfListOfInternalResource.aspx.cs"
    Inherits="MrfListOfInternalResource"  %>

<head id="Head1" runat="server">
<base target="_parent" />
    <title>List Of Internal Resource</title>
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />
    
</head>
<form id="form1" runat="server">
<body>
    <table style="width: 100%">
        <tr>
            <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                background-color: #7590C8">
                <span class="header">List of Internal Resource</span>
            </td>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlDepartmentDropDown" runat="server" Visible=true Width=100%>
    <table>
    <tr>
     <td width="20%" align="left">
                            Department
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td width="5%">
                        </td>
                        <td width="30%" align="left">
                            <span id="spanzDepartment" runat="server">
                                <asp:DropDownList ID="ddlDepartment" runat="server" Width="190px" ToolTip="Select Department"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                                    TabIndex="2">
                                </asp:DropDownList>
                            </span>
                        </td>
                        </tr>
                        <tr></tr>
</table>
    </asp:Panel>
    <asp:GridView ID="grdvListofInternalResource" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" OnPageIndexChanging="grdvListofInternalResource_PageIndexChanging"
        AllowSorting="True" OnSorting="grdvListofInternalResource_Sorting" OnRowCreated="grdvListofInternalResource_RowCreated"
        OnDataBound="grdvListofInternalResource_DataBound" 
        OnRowCommand="grdvListofInternalResource_RowCommand" Width="90%"
        DataKeyNames="EmployeeId">
        <HeaderStyle CssClass="headerStyle" />
        <AlternatingRowStyle CssClass="alternatingrowStyle" />
        <RowStyle Height="20px" CssClass="textstyle" />
        <Columns>
        <asp:TemplateField Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbl" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"EmployeeId") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:RadioButton ID="radioId" runat="server" OnCheckedChanged="radioId_CheckedChanged"
                        onclick="javascript:CheckOtherIsCheckedByGVID(this);"  GroupName="ID" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="EmployeeName" HeaderText="Resource Name" SortExpression="ResourceName">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle  HorizontalAlign="center" />
            </asp:BoundField>
            <asp:BoundField DataField="ProjectName" HeaderText="Current Project " SortExpression="CurrentProjectName">
                <HeaderStyle  HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Billing" HeaderText="Billing(%)" SortExpression="Billing">
                <HeaderStyle  HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Utilization" HeaderText="Utilization (%)" SortExpression="Utilization">
                <HeaderStyle  HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Remarks" HeaderText="Release Date" SortExpression="ReleaseDate" >
                <HeaderStyle  HorizontalAlign="Left" />
            </asp:BoundField>
           <asp:BoundField DataField="DepartmentName" HeaderText="Designation" SortExpression="Designation">
                                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        <PagerTemplate>
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
                            OnCommand="ChangePage" Font-Underline="true" Enabled="False">Next</asp:LinkButton>
                        &nbsp;&nbsp;&gt;&gt;
                    </td>
                </tr>
            </table>
        </PagerTemplate>
        <SelectedRowStyle BackColor="#0099CC" />
    </asp:GridView>
    <table width="100%">
        <tr>
            <td width="20%" align="right">
                        <asp:Button ID="btnOk" runat="server" Text="OK" Width="120px"
                        CssClass="button" TabIndex="15" onclick="btnOk_Click"/>&nbsp;&nbsp;<asp:Button ID="btnCancel" 
                            runat="server" Text="Cancel" Width="120px"
                        CssClass="button" TabIndex="16" onclick="btnCancel_Click" /></td>
        </tr>
    </table>
</body>
</form>

<script language="javascript" src="JavaScript/CommonValidations.js" type="text/javascript">
</script>

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

