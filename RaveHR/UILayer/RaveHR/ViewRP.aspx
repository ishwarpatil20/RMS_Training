<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ViewRP.aspx.cs" Inherits="ViewRP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" Runat="Server">
        <script language="javascript" type ="text/javascript">
   
    setbgToTab('ctl00_tabProject', 'ctl00_divProjectSummary');
   
    function validateViewButton()
    {
        var varObjDdlInactiveRP = document.getElementById('ctl00$cphMainContent$ddlInactiveRP'); 
        var varGetDdlInactiveRP = varObjDdlInactiveRP.value;
        
        var varIsInactiveDdlChecked = document.getElementById('ctl00_cphMainContent_rbRPInactiveStatus').checked;      
       
        var varObjDdlActiveRP = document.getElementById('ctl00$cphMainContent$ddlActiveRP'); 
        var varGetDdlActiveRP = varObjDdlActiveRP.value;
        
        var varIsActiveDdlChecked = document.getElementById('ctl00_cphMainContent_rbRPActiveStatus').checked;
        var varLblMessage=document.getElementById("<%=lblMessage.ClientID %>");      
       
       if (((varIsInactiveDdlChecked == true) && (varGetDdlInactiveRP == "0")))
        {
            varLblMessage.innerText = "Please Select Resource Plan";
            return false;
        }
        else if (((varIsActiveDdlChecked == true) && (varGetDdlActiveRP == "0")))
        {
            varLblMessage.innerText = "Please Select Resource Plan";
            return false;
        }
    }
    </script> 
    <table width="100%">
        <tr>
          <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');"><span class="header">View Resource Plan</span></td>
              <%--   Sanju:Issue Id 50201:End--%>
        </tr>
        <tr>
            <td><asp:Label ID="lblMessage" runat="server" CssClass="text"></asp:Label>&nbsp;</td>
        </tr>
    </table>
    <div>
      <table width="100%" cellpadding="0" cellspacing="1" border="0">
        <tr class="gridheaderStyle">
            <td align="center" style="width:11%">Select</td>
            <td align="left" style="width:31%">Resource Plan</td>
            <td align="left" style="width:33%">Resource Code</td>
            <td align="left" style="width:25%">Date</td>
        </tr>
        <tr class="alternatingrowStyle">
            <td align="center">
                <asp:RadioButton ID="rbRPInactiveStatus" runat="server" GroupName="SelectRPStatus" />
            </td>
            <td align="left" class="txtstyle">Inactive Resource Plan</td>
            <td align="left"><asp:DropDownList ID="ddlInactiveRP" runat="server" Width="250" 
                    onselectedindexchanged="ddlInactiveRP_SelectedIndexChanged" 
                    AutoPostBack="True">
                    </asp:DropDownList>
            </td>
            <td align="left"><asp:Label ID="lblInactiveRPDate" runat="server"></asp:Label></td>
        </tr>
        <tr style="background-color:#E0F8FC" class="alternatingrowStyle">
            <td align="center">
                <asp:RadioButton ID="rbRPActiveStatus" runat="server" GroupName="SelectRPStatus" Checked="true"/>
            </td>
            <td align="left" class="txtstyle">Active Resource Plan</td>
            <td align="left"><asp:DropDownList ID="ddlActiveRP"  runat="server" Width="250" 
                    AutoPostBack="True" onselectedindexchanged="ddlactiveRP_SelectedIndexChanged">
                    </asp:DropDownList>
            </td>
            <td align="left"><asp:Label ID="lblActiveRPDate" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
    </table>
      <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="btnViewRP" runat="server"  CssClass="button" Text="View" 
                    onclick="btnViewRP_Click" />&nbsp;
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" 
                    onclick="btnCancel_Click"/>
            </td>
        </tr>
    </table>
    </div>

</asp:Content>

