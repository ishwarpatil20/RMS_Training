<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" 
AutoEventWireup="true" CodeFile="EditRPOptions.aspx.cs" Inherits="EditRPOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" Runat="Server">
    
   <script language="javascript" type ="text/javascript">
   
    setbgToTab('ctl00_tabProject', 'ctl00_divProjectSummary');
    
    function validateViewButton()
    {

        var varObjDdlActiveRP = document.getElementById('<%=ddlActiveRP.ClientID %>');
        
        var varGetDdlActiveRP = varObjDdlActiveRP.options[varObjDdlActiveRP.selectedIndex].value;
                
        var varIsActiveDdlChecked = document.getElementById('ctl00_cphMainContent_rbRPActiveStatus').checked;
        
        var varObjDdlRejectRP = document.getElementById('<%=ddlRejectRP.ClientID %>');
        var varGetDdlRejectRP = varObjDdlRejectRP.options[varObjDdlRejectRP.selectedIndex].value;
        
        var varIsRejectDdlChecked = document.getElementById('ctl00_cphMainContent_rbRPRejectStatus').checked;

        var varLblMessage = document.getElementById("<%=lblMessage.ClientID %>");
        varLblMessage.innerHTML = "";

        if ((varIsActiveDdlChecked == true) && (varGetDdlActiveRP == 0)) {
           
            varLblMessage.style.color = "Red";
            varLblMessage.innerHTML = "Please select Resource Code.";
            return false;
        }
                
       else if ((varIsRejectDdlChecked == true) && (varGetDdlRejectRP == 0))
        {
            varLblMessage.style.color = "Red";
            varLblMessage.innerHTML = "Please select Resource Code.";
            return false;
        }
    }
    </script>
    
    <table width="100%">
        <tr>
            <td style="height: 2pt">
            </td>
        </tr>
        <tr>
          <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile"  style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <span class="header">Edit Resource Plan</span>
            </td>
              <%--   Sanju:Issue Id 50201:End--%>
        </tr>
        
        <tr>
            <td style="width: 860px;">
                <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <%--<asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>--%>
            </td>
        </tr>
        
    </table>
    
    <table width="100%" cellpadding="0" cellspacing="1" border="0">
        <tr class="gridheaderStyle">
            <td align="center" style="width:11%">Select</td>
            <td align="left" style="width:31%">Resource Plan</td>
            <td align="left" style="width:33%">Resource Code</td>
            <td align="left" style="width:25%">Date</td>
        </tr>
        <tr class="alternatingrowStyle">
            <td align="center">
                <asp:RadioButton ID="rbRPActiveStatus" runat="server" Checked="true" GroupName="SelectRPStatus" />
            </td>
            <td align="left" class="txtstyle">Active Resource Plan</td>
            <td align="left"><asp:DropDownList ID="ddlActiveRP" runat="server" Width="250" 
                    onselectedindexchanged="ddlActiveRP_SelectedIndexChanged" 
                    AutoPostBack="True"></asp:DropDownList></td>
            <td align="left"><asp:Label ID="lblActiveRPDate" runat="server"></asp:Label></td>
        </tr>
        <tr class="alternatingrowStyle">
            <td align="center">
                <asp:RadioButton ID="rbRPRejectStatus" runat="server" GroupName="SelectRPStatus" />
            </td>
            <td align="left" class="txtstyle">Reject Resource Plan</td>
            <td align="left"><asp:DropDownList ID="ddlRejectRP"  runat="server" Width="250" 
                    AutoPostBack="True" onselectedindexchanged="ddlRejectRP_SelectedIndexChanged"></asp:DropDownList></td>
            <td align="left"><asp:Label ID="lblRejectRPDate" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
    </table>
    
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="btnCreateRP" runat="server" Width="175" CssClass="button" 
                    Text="Create Resource Plan" oncommand="btnCreateRP_Click" />&nbsp;
                <asp:Button ID="btnEditRP" runat="server" CssClass="button" Text="Edit" 
                    onclick="btnEditRP_Click" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" 
                    onclick="btnCancel_Click"/>
            </td>
        </tr>
    </table>
</asp:Content>

