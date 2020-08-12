<%@ Page Language="C#"  AutoEventWireup="true"
    CodeFile="SeatInformation.aspx.cs" Inherits="SeatInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<head id="Head1" runat="server">
    <%--<base target="_parent" />--%>
    <title></title>
     <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 116px;
        }
        .style3
        {
            height: 26px;
            width: 199px;
        }
        .style4
        {
            width: 199px;
        }
        .style6
        {
            width: 25px;
            height: 26px;
        }
        .style7
        {
            width: 25px;
        }
    </style>
 </head>
 
 <script type="text/javascript" language="javascript"> 
 </script>
<form id="form1" runat = "server" >
<body>

 <table style="width: 100%">
        <tr>     
           <td align="center" 
                style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1'); width: 100%;">
                <asp:Label ID="lblSeatInformation" runat="server" Text="Seat Information" CssClass="header"></asp:Label>
            </td>
        </tr>
    </table>
 
 <table style="width: 104%">
        <tr>
            <td style="width: 860px; height: 18px;">
               
                <asp:Label ID="lblConfirmMessage" runat="server" CssClass="Messagetext"></asp:Label>

            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt" CssClass =" messagetext" visible = "false" >Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
            </td>
        </tr>
    </table>
<%-- <div class="detailsborder">
        
        <table class="detailsbg" width = "100%">
            <tr>
                <td  >
                    <asp:Label ID="lblDetailsGroup" runat="server" Text="Seat Details" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
        </table>
 </div>--%>
 <Table ID="Table1"  style="width: 100%">
     <tr >
        <td class="style1">
         </td>
         <td class="style4">
         </td>
         <td class="style7">
         </td>
         <td>
         </td>
     </tr>
     <tr>
     <td class="style1"></td>
         <td class="style4">
             <asp:Label ID="lblEmpAsset" runat="server" 
                 Text="Employee Or Asset" CssClass="txtstyle" Visible="False"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryEmpAsset" 
                 runat = "server" visible="False" >*</label>
         </td>
         <td class="style7">
         </td>
         <td>
             <asp:DropDownList ID="ddlEmpAsset" runat="server" Width="313px" 
                 Visible="False" onselectedindexchanged="ddlEmpAsset_SelectedIndexChanged" 
                 AutoPostBack="True">
             </asp:DropDownList>
         </td>
     </tr>
       <tr>
     <td class="style1"></td>
         <td class="style4">
         <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code" 
                 CssClass="txtstyle" Visible="False"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryEmployeeCode" 
                 runat = "server" visible="False" >*</label>
         </td>
         <td class="style7">
         </td>
         <td>
             <asp:TextBox ID="tbEmpCode" runat="server" Width="307px" Visible="False"  
                  AutoPostBack="True" MaxLength="8"></asp:TextBox>
                <%--  OnTextChanged = "GetEmployeeName"--%>
             <asp:DropDownList ID="ddlEmployeeCode" runat="server" AutoPostBack="True" 
                 onselectedindexchanged="ddlEmployeeCode_SelectedIndexChanged" Visible="False" 
                 Width="313px">
             </asp:DropDownList>
         </td>
     </tr>
     <tr>
     <td class="style1"></td>
         <td class="style4">
         <asp:Label ID="lblEmpName" runat="server" Text="Employee Name" 
                 CssClass="txtstyle" Visible="False"></asp:Label><label
                                        class="mandatorymark" id = "Label2" 
                 runat = "server" visible="False" >*</label>
         </td>
         <td class="style7">
         </td>
         <td>
             <asp:TextBox ID="tbEmpName" runat="server" Width="307px" Visible="False"></asp:TextBox>
         </td>
     </tr>   
     <tr>
     <td class="style1"></td>
         <td class="style3">
         <asp:Label ID="lblDepartmentName" runat="server" Text="Department Name" 
                 CssClass="txtstyle" Visible="False"></asp:Label><label
                                        class="mandatorymark" 
                 id = "MandatoryMandatoryDepartmentName" runat = "server" visible="False" >*</label>
         </td>
         <td class="style6">
         </td>
         <td style="height: 26px">
             <asp:TextBox ID="tbDepartmentName" runat="server" Width="307px" Visible="False" ></asp:TextBox>
         </td>
     </tr>
     <tr>
     <td class="style1"></td>
         <td class="style4">
             <asp:Label ID="lblprojectName" runat="server" 
                 Text="Project Name" CssClass="txtstyle" Visible="False"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryProjectName" 
                 runat = "server" visible="False" >*</label>
         </td>
         <td class="style7">
         </td>
         <td>
             <asp:TextBox ID="tbProjectName" runat="server" Width="307px" Visible="False"></asp:TextBox>
         </td>
     </tr>
     <tr>
     <td class="style1"></td>
         <td class="style4">
             <asp:Label ID="lblAssetCode" runat="server" 
                 Text="Asset Code" CssClass="txtstyle" Visible="False"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryAssetCode" 
                 runat = "server" visible="False" >*</label>
         </td>
         <td class="style7">
         </td>
         <td>
             <asp:DropDownList ID="ddlAssetCode" runat="server"  Width="313px" 
                 Visible="False">
             </asp:DropDownList>
         </td>
     </tr>
     <tr>
     <td class="style1"></td>
         <td class="style4">
             <asp:Label ID="lblAssetDesc" runat="server" 
                 Text="Asset Description" CssClass="txtstyle" Visible="False"></asp:Label><label
                                        class="mandatorymark" id = "Label10" 
                 runat = "server" visible="False" >*</label>
         </td>
         <td class="style7">
         </td>
         <td>
             <asp:TextBox ID="tbAssetDescription" runat="server" Height="30px" TextMode="MultiLine" 
                 Width="307px" Visible="False" MaxLength="500"></asp:TextBox>
                                                </td>
     </tr>
     <tr>
     <td class="style1">
         &nbsp;</td>
         <td class="style4">
             <asp:Label ID="lblExtnsion" runat="server" 
                 Text="Extension No." CssClass="txtstyle" Visible="False"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryExtn" 
                 runat = "server" visible="False" >*</label>
         </td>
         <td class="style7">
         </td>
         <td>
             <asp:TextBox ID="tbExtension" runat="server" Width="307px" Visible="False" 
                 MaxLength="4"></asp:TextBox>
         </td>
     </tr>
     <tr>
     <td class="style1">
         &nbsp;</td>
         <td class="style4">
             <asp:Label ID="lblLandmark" runat="server" Text="Landmark " 
                 CssClass="txtstyle" Visible="False"></asp:Label><label
                                        class="mandatorymark" id = "MandatoryLandmark" 
                 runat = "server" visible="False" >*</label>
         </td>
         <td class="style7">
         </td>
         <td>
             <asp:TextBox ID="tbLandmark" runat="server" Height="30px" TextMode="MultiLine" 
                 Width="307px" Visible="False" MaxLength="500"></asp:TextBox>
         </td>
     </tr>
     <tr>
     <td class="style1">
         <asp:HiddenField ID="hfSeatID" runat="server" />
         </td>
     <td class="style4" >
         <asp:HiddenField ID="hfEmpID" runat="server" />
     </td>
     <td class="style7" >
     </td>
     <td >
       <asp:Button ID="btnAllocate" runat="server" Text="Allocate" 
             CssClass="button" Visible="False" onclick="btnAllocate_Click" />
     &nbsp;<asp:Button ID="btnSave"  CssClass="button" Visible="False" runat="server" 
             Text="Save" onclick="btnSave_Click"  />
         <asp:Button ID="btnOK"  CssClass="button" Visible="False" runat="server" 
             Text="Close" onclick="btnOK_Click"   />
     </td>
     </tr>
     
</Table>
</body>
</form>
    

