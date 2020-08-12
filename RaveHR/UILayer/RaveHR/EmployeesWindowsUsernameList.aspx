<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeesWindowsUsernameList.aspx.cs"
    Inherits="EmployeesWindowsUsernameList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employees Email List</title>
    <base target="_self" />
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />
    
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <link href="CSS/jquery.modalDialogContent.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinnyContent.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialogContent.js" type="text/javascript"></script>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->

    <script language="javascript" type="text/javascript">
        function $(v){return document.getElementById(v);}
        
        function FnCheck(chkid,emptext)
        {
            var number = 0;
            var CheckBoxID = $(chkid).checked;
            var HidEmployee = $(emptext).value;
            var Counter= 0;
            
          if($( '<%=hidEmployeeCount.ClientID %>').value == "")
          {
                Counter= 0;
          }
          else
          {
             Counter = $( '<%=hidEmployeeCount.ClientID %>').value;
             number = parseInt(Counter);
          }
            if(CheckBoxID == true)
            {
                number = number + 1 ;
               
            }
            else
            {
                number = number - 1 ;
            }
            
            $( '<%=hidEmployeeCount.ClientID %>').value = number;
            
            if($( '<%=hidEmployeeCount.ClientID %>').value > 1)
            {
                number = number - 1 ; 
                $( '<%=hidEmployeeCount.ClientID %>').value = number; 
                
                alert("You cannot select more then one email.");
                $(chkid).checked = false;
                return false;
            }
           
        }
        
        function Validate()
        {
            if($( '<%=hidEmployeeCount.ClientID %>').value == "" || $( '<%=hidEmployeeCount.ClientID %>').value == '0')
            {
                alert("Please Select the Record.");
                return false;
            }
        }
        
        function RadioValidation()
        {
            var i,radiochoice;
            radiochoice=false;

            var rbOriginal=document.getElementsByName("rbEmailList");
            for(i=0;i<rbOriginal.length;i++)
            {
              if (rbOriginal(i).checked)
              {
                radiochoice=true;  
                break;   
              }
            }
            if(! radiochoice)
            {
                alert("Please select username.")
                return false;
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <cc1:ToolkitScriptManager ID="ScriptManagerMaster" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="upEmailList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
            <div>
                <table width="100%" class="detailsbg">
                    <tr>
                        <td>
                            <asp:Label ID="lblEmployeeEmailList" runat="server" Text="Employees Email List" CssClass="detailsheader"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEmpName" runat="server" Text="Employee Name :" CssClass="txtstyle"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="tbxEmpName" runat="server" Width="210px" ToolTip="Enter Employee Name"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSearchEmpName" runat="server" Text="Search" CssClass="button"
                                OnClick="btnSearchEmpName_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:GridView ID="grvEmployeeEmail" runat="server" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="grvEmployeeEmail_PageIndexChanging" PageSize="15"
                                OnRowDataBound="grvEmployeeEmail_RowDataBound" EmptyDataText="No Record Found."
                                EmptyDataRowStyle-CssClass="Messagetext">
                                <HeaderStyle CssClass="headerStyle" />
                                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                <RowStyle Height="20px" CssClass="textstyle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderText="Select">
                                        <ItemTemplate>
                                            <input type='radio' name="rbEmailList" value='<%# Eval("Username") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="EMPCode">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="45%" HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Username" HeaderText="Employee Windows Username" SortExpression="EMPCode">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="45%" HorizontalAlign="center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="button" OnClick="btnSelect_Click" />
                            <asp:TextBox ID="hidEmployeeCount" runat="server" Width="0"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearchEmpName" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSelect" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    </form>
</body>
</html>
