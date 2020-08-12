<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MrfRaisePrevious.aspx.cs"
    Inherits="MrfRaisePrevious" MasterPageFile="~/MasterPage/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GroupDropDownList" Namespace="GroupDropDownList" TagPrefix="cc2" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height: 27px">
        <tr>
         <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center"  class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1')";
                >
             <%--   Sanju:Issue Id 50201:End--%>
                <span class="header">Raise MRF</span>
            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
            </td>
        </tr>
        <tr height="10px">
            <td>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" border="0">
                    <tr>
                        <%--25988-Ambar-Start- Added New Drop Down for Status--%>
                        <td align="left" style="width: 7%">
                            <asp:Label ID="lbldpstatus" Text="Dept/Project Status" runat="server" />
                            
                            </td>
                            
                         <td align="left" style="width: 20%">
                             <span id="spanzddldpstatus" runat="server">
                            <asp:DropDownList ID="ddldpstatus" runat="server"
                                Width="100px" ToolTip="Select Status" TabIndex="1" CssClass="mandatoryField" 
                                OnSelectedIndexChanged="ddlDPStatus_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </span>
                        </td>
                        </tr>
                        <tr>
                        <%--25988-Ambar-End--%>
                        <td align="left" style="width: 7%">
                            <asp:Label ID="lblDept" Text="Dept/Project Name" runat="server" />
                            
                            </td>
                            
                         <td align="left" style="width: 20%">
                             <span id="spanzddlMRFDeptCopy" runat="server">
                            <asp:DropDownList ID="ddlMRFDeptCopy" runat="server"
                                Width="322px" ToolTip="Select Dept" TabIndex="1" CssClass="mandatoryField" 
                                OnSelectedIndexChanged="ddlMRFDeptCopy_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </span>
                        </td>
                        
                         <td align="left" width="5%"> 
                            <asp:Label ID="LblMRFRole" Text="MRF Role" runat="server" Visible="False" />
                            
                        </td>
                         <td align="left" width="17%" class="txtstyle">
                        <span id="spanzddlMRFFilterRoleCopy" runat="server">
                            <asp:DropDownList ID="ddlMRFFilterRoleCopy" runat="server"
                                Width="266px" ToolTip="Select Dept" TabIndex="1" CssClass="mandatoryField" 
                                OnSelectedIndexChanged="ddlMRFFilterRoleCopy_SelectedIndexChanged" 
                                AutoPostBack="True" Visible="False">
                            </asp:DropDownList>
                        </span>
                        </td>
                        <%-- <td align="left" width="20%">
                             &nbsp;</td>--%>
                        
                    </tr> 
                    </table>
                <%--Sanju:Issue Id 50201-Start- Added width so that the property is left align--%>
                <table width="100%">
                 <%--Sanju:Issue Id 50201 End--%>
                    <tr>
                     <td align="left" style="width: 14.20%">    
                           <asp:Label ID="lblPrevMrf" Text="MRFs" runat="server"  />  
                        </td>
                         <%--<td align="left" width="1%">
                             &nbsp;</td>--%>
                  <%--Sanju:Issue Id 50201-Start-dropdown alignment issue resolved:Added width--%>     
                   <td align="left" style="width: 27%;width:38%">
                    <%--Sanju:Issue Id 50201 End--%>
                        <span id="spanzdddlMRFFilterCopy" runat="server">
                            <asp:DropDownList ID="ddlMRFFilterCopy" runat="server"
                                Width="522px" ToolTip="Select MRF" TabIndex="1" 
                            CssClass="mandatoryField"  >
                                    
                            </asp:DropDownList>
                        </span>
                    </td> 
                     <td align="left" width="1%">        
                            &nbsp;</td>
                   
                     <td align="left" >
                        <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="button"
                                OnClick="btnCopy_Click" />
                    </td>
                    <td>
                            <span id="spanzddlPreviousMRF" runat="server">
                                <cc2:GroupDropDownList ID="GroupDropDownList2" runat="server" 
                            Width="180px" ToolTip="Select Previous MRF Code"
                                    CssClass="mandatoryField" Visible="False" >
                                
                                </cc2:GroupDropDownList>
                            </span>
                    </td>
                    </tr>
                    
                    
                    
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="detailsbg" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="MRF Detail" CssClass="detailsheader"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="10px">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td align="left" style="width: 20%" class="txtstyle">
                            MRF Type
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 3%">
                        </td>
                        <td align="left" style="width: 27%">
                            <span id="spanzMRFType" runat="server">
                            <asp:DropDownList ID="ddlMRFType" runat="server"
                                Width="225px" ToolTip="Select MRF Type" TabIndex="1" CssClass="mandatoryField">
                            </asp:DropDownList>
                            </span>
                        </td>
                        <td width="20%" align="left" class="txtstyle">
                            Department
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td width="3%">
                        </td>
                        <td width="27%" align="left">
                            <span id="spanzDepartment" runat="server"><asp:DropDownList ID="ddlDepartment"
                                runat="server" Width="225px" ToolTip="Select Department" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" TabIndex="2" CssClass="mandatoryField">
                            </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr id="ProjectDetails" runat="server">
                        <td align="left" style="width: 20%" class="txtstyle">
                            Project Name
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 3%">
                        </td>
                        <td align="left" style="width: 27%">
                            <span id="spanzProjectName" runat="server"><asp:DropDownList ID="ddlProjectName"
                                runat="server" Width="225px" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged"
                                ToolTip="Select Project Name" TabIndex="3" CssClass="mandatoryField">
                                <asp:ListItem Text="SELECT" Value="0" Enabled="true" />
                            </asp:DropDownList>
                            </span>
                        </td>
                        <td align="left" width="20%" class="txtstyle">
                            Resource Plan Code
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td width="3%">
                        </td>
                        <td align="left" width="27%">
                            <span id="spanzResourcePlanCode" runat="server"><asp:DropDownList ID="ddlResourcePlanCode"
                                runat="server" Width="225px" ToolTip="Select Resource Plan Code" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlResourcePlanCode_SelectedIndexChanged" TabIndex="4"
                                CssClass="mandatoryField">
                                <asp:ListItem Text="SELECT" Value="0" Enabled="true" />
                            </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 20%" class="txtstyle">
                            Role
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 3%">
                        </td>
                        <td align="left" style="width: 27%">
                            <span id="spanzRole" runat="server"><asp:DropDownList ID="ddlRole" runat="server"
                                Width="225px" ToolTip="Select Role" AutoPostBack="True" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"
                                TabIndex="5" CssClass="mandatoryField">
                                <asp:ListItem Text="SELECT" Value="0" Enabled="true" />
                            </asp:DropDownList>
                            </span>
                        </td>
                        <td align="left" width="20%" class="txtstyle">
                            Date of Requisition
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td width="3%">
                        </td>
                        <td align="left" width="27%" >
                            <uc1:DatePicker ID="ucDatePicker" runat="server" />
                        </td>
                    </tr>                   
                    <tr>
                        <td align="left" style="width: 20%" class="txtstyle">
                            No of Resources Required
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 3%">
                            <span id="spanNoOfResources" runat="server">
                                <img id="imgNoOfResources" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" style="width: 27%">
                            <asp:TextBox ID="txtNoOfResources" runat="server" CssClass="mandatoryField"
                                MaxLength="2" ToolTip="Enter No of Resources" TabIndex="9" Width="221px" ></asp:TextBox></td>
                        <td align="left" width="20%" id="ProjDesc1" runat="server">
                            <asp:Label ID="lblProjDescription" runat="server" Text="Project Description" CssClass="txtstyle">
                            </asp:Label>
                            <asp:Label ID="lblClientName" runat="server" Text="Client Name" Visible="false" CssClass="txtstyle">
                            </asp:Label>
                            <%--Project Description--%>
                            <label class="mandatorymark" id="labProjectDescription" runat="server">
                                *</label>
                        </td>
                        <td width="3%" id="ProjDesc2" runat="server">
                            <span id="spanClientName" runat="server">
                                <img id="imgClientName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="27%" id="ProjDesc3" runat="server">
                            <asp:TextBox ID="txtProjectDesc" runat="server" CssClass="mandatoryField" MaxLength="30"
                                ToolTip="Enter Project Description" TabIndex="13" AutoComplete="off" Width="221px"
                                ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="txtClientName" runat="server" Visible="false" CssClass="mandatoryField"
                                MaxLength="30" ToolTip="Enter Client Name" TabIndex="13" AutoComplete="off" Width="221px">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                    <td align="left" style="width: 20%" >
                            
                         <asp:Label ID="lblSOWNo" Text="SOW No" class="txtstyle" Visible="false" runat="server" />
                         <asp:Label class="mandatorymark" ID="SOWNoMandatoryMark" Visible="false"  runat="server">
                            *</asp:Label>
                        </td>
                        <td style="width: 3%">
                            <span id="spanSOWNo" runat="server">
                                <img id="imgSOWNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" style="width: 27%">
                            <asp:TextBox ID="txtSOWNo" runat="server" CssClass="mandatoryField" Visible="false" AutoPostBack="True" OnTextChanged="txtSOWNo_TextChanged"
                                MaxLength="20" ToolTip="Enter SOW No" TabIndex="9" Width="221px" >
                         </asp:TextBox></td>
                    
                    
                        <td align="left" style="width: 20%" >
                           <asp:Label ID="lblSOWStDt" Text="SOW Start Date" class="txtstyle" Visible="false" runat="server" />
                           <asp:Label class="mandatorymark" ID="SOWStartDatemandatorymark" Visible="false"  runat="server">
                            *</asp:Label>
                        </td>
                        <td style="width: 3%" >
                            <span id="spanSOWStartDate"   runat="server">
                                <img id="imgSOWStartDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" style="width: 27%">
                            <uc1:DatePicker ID="DtPckrSOWStartDate"  Visible="false" runat="server" />
                        </td>
                        
                        
                    </tr>
                    <tr> 
                    <%--  Rajan Kumar : Issue 45752 : 03/01/2013 : Starts                        			 
                 Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).--%>
                    <td align="left" style="width: 20%">
                    
                    <asp:Label ID="lblRequiredFrom" Text="Required From" class="txtstyle" runat="server" />                            
                    <label class="mandatorymark" ID="RequiredFrommandatorymark" runat="server">
                    *</label>
                        </td>
                        <td style="width: 3%">
                            <span id="spanRequiredFrom" runat="server">
                                <img id="imgRequiredFromError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" style="width: 27%">
                            <uc1:DatePicker ID="ucDatePicker2" runat="server"  />
                            <span id="spanDate" runat="server">
                                    <img id="img1" runat="server" src="Images/Calendar_scheduleHS.png" alt="" style="display: none;
                                        border: none;"/>
                            </span>
                        </td>
                         <%-- Rajan Kumar : Issue 45752 : 03/01/2014: Ends     --%>              
                        <td align="left" width="20%" >                            
                            <asp:Label ID="lblSOWEndDt" Text="SOW End Date" class="txtstyle" Visible="false" runat="server" />
                            <asp:Label class="mandatorymark" ID="SOWEndDatemandatorymark" Visible="false"  runat="server">
                            *</asp:Label>
                        </td>
                        <td width="3%">
                            <span id="spanSOWEndDate" runat="server">
                                <img id="imgSOWEndDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="27%">
                            <uc1:DatePicker ID="DatePickerSOWEndDate" Visible="false" runat="server" />
                        </td>
                    </tr>
          <%--          // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).--%>
                     <tr id="MrfDateRange" runat="server" visible="false">
                        <%--<td align="left" style="width: 20%" class="txtstyle">
                            Required From
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 3%">
                            <span id="spanRequiredFrom" runat="server">
                                <img id="imgRequiredFromError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" style="width: 27%">
                            <uc1:DatePicker ID="ucDatePicker2" runat="server" />
                        </td>--%>
                        <td align="left" width="20%" class="txtstyle">
                            Required Till
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td width="3%">
                            <span id="spanRequiredTill" runat="server">
                                <img id="imgRequiredTillError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="27%">
                            <uc1:DatePicker ID="ucDatePicker1" runat="server" />
                        </td>
                    </tr>
                    <%-- // Rajan Kumar : Issue 45752 : 03/01/2014: Ends --%>
                    <tr>
                        <td align="left" width="100%" colspan="6">
                            <asp:GridView ID="grdresource" runat="server" Width="70%" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="grdresource_RowDataBound"
                                OnRowCommand="grdresource_RowCommand">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" AutoPostBack="false" Checked='<%# DataBinder.Eval(Container.DataItem,"CheckGridValue")  %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                type="checkbox" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRPDuId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ResourcePlanDurationId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" /> 
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ResourcePlanStartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="ResourcePlanEndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="ResourceLocation" HeaderText="Location" />
                                    <asp:TemplateField Visible="true" HeaderText="Utilization">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUtilization" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Utilization") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" /> 
                                    </asp:TemplateField>  
                                    <asp:TemplateField Visible="true" HeaderText="Billing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBilling" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Billing") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" /> 
                                    </asp:TemplateField>                                                                      
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        Billing Date
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign ="Center" />
                                        <ItemTemplate>
                                            <uc1:DatePicker ID="billingDatePicker" runat="server"  style="Width:40px;"/>
                                        </ItemTemplate>                                       
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" /> 
                                    </asp:TemplateField>                      
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgView" ImageUrl="~/Images/view.png" ToolTip="View"
                                                Width="20px" Height="20px" CommandName="View" CommandArgument='<%# Container.DisplayIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>              
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#7590C8" Font-Bold="True" ForeColor="White" HorizontalAlign ="Center" />                                
                                <EditRowStyle BackColor="#2461BF" />
                                <RowStyle Height="20px" CssClass="txtstyle" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            
                        </td>
                    </tr>
                    <%--<tr >
                        <td style="width: 36%" colspan="6">
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="width: 20%">
                        </td>
                        <td style="width: 3%">
                        </td>
                        <td style="width: 27%">
                        </td>
                        <td width="20%">
                        </td>
                        <td width="3%">
                        </td>
                        <td width="27%">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="120px" CssClass="button"
                                TabIndex="11" OnClick="btnCancel_Click" />&nbsp;&nbsp;<asp:Button ID="btnNext" runat="server"
                                    Text="Next" Width="120px" CssClass="button" TabIndex="10" OnClick="btnNext_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:HiddenField ID="hidRequiredFrom" runat="server" />
                            <asp:HiddenField ID="hidRequiredTill" runat="server" />
                            <asp:HiddenField ID="hidRPId" runat="server" />
                            <asp:HiddenField ID="hidEncryptedQueryString" runat="server" />
                            <asp:HiddenField ID="hidDepartment" runat="server" />
                            <asp:HiddenField ID="hidResourcesCount" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";

                $('#ctl00_cphMainContent_btnNext').click(function() {
                    if (ButtonClickValidate()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                        //$(this).parents('#form').submit();
                    }

                });

            });
        })(jQuery);

        
        function popUpResourceSplitDuration(varhidRPID) {
            //varhidRPID=$( '<%=hidRPId.ClientID %>').value;
            var encryptedQueryString = jQuery('#<%=hidEncryptedQueryString.ClientID %>').val();
            jQuery.modalDialog.create({ url: encryptedQueryString, maxWidth: 500 }).open();
            return false;
        }
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        setbgToTab('ctl00_tabMRF', 'ctl00_spanRaiseMRF');


        function $(v) { return document.getElementById(v); }

        //    function fnWidthSize(ddlRole)
        //    {
        //        if (ddlRole.style.width != "200px") 
        //        {
        //            ddlRole.style.width = "200";
        //        }
        //        else
        //        {
        //            ddlRole.style.width = "100";
        //        }
        //    }



        function CopyButtonClick() {
            //debugger;
            var PreviuosMRF = $('<%=ddlMRFDeptCopy.ClientID %>').value;

            if (PreviuosMRF == "" || PreviuosMRF == "SELECT") {
                var sPreviuosMRF = $('<%=spanzdddlMRFFilterCopy.ClientID %>').id;

                if (ValidateControlOnClickEvent(sPreviuosMRF) == false) {
                    lblMandatory = $('<%=lblMandatory.ClientID %>')
                    lblMandatory.innerText = "Please select a Dept/Project Name to perform the copy operation.";
                    lblMandatory.style.color = "Red";
                }

                return ValidateControlOnClickEvent(sPreviuosMRF);
            }
        }
        function ButtonClickValidate() {
            // debugger
            var lblMandatory;
            var spanlist = "";
            var MRFType = $('<%=ddlMRFType.ClientID %>').value;


            if (MRFType == "" || MRFType == "SELECT") {
                var sMRFType = $('<%=spanzMRFType.ClientID %>').id;
                spanlist = sMRFType + ",";
            }
            var Department = $('<%=ddlDepartment.ClientID %>').value;
            if (Department == "" || Department == "SELECT") {
                var sDepartment = $('<%=spanzDepartment.ClientID %>').id;
                spanlist = spanlist + sDepartment + ",";
                //Poonam : Issue : 54907 : Starts
                var ProjectName = $('<%=ddlProjectName.ClientID %>').value;
                if (ProjectName == 0 || ProjectName == "SELECT") {
                    var sProjectName = $('<%=spanzProjectName.ClientID %>').id;
                    spanlist = spanlist + sProjectName + ",";
                }

                var ResourcePlanCode = $('<%=ddlResourcePlanCode.ClientID %>').value;
                if (ResourcePlanCode == 0 || ResourcePlanCode == "SELECT") {
                    var sResourcePlanCode = $('<%=spanzResourcePlanCode.ClientID %>').id;
                    spanlist = spanlist + sResourcePlanCode + ",";
                }
                //Poonam : Issue : 54907 : Ends
            }
            
            if (Department == "1" || Department == "SELECT") {
                var ProjectName = $('<%=ddlProjectName.ClientID %>').value;
                if (ProjectName == 0 || ProjectName == "SELECT") {
                    var sProjectName = $('<%=spanzProjectName.ClientID %>').id;
                    spanlist = spanlist + sProjectName + ",";
                }

                var ResourcePlanCode = $('<%=ddlResourcePlanCode.ClientID %>').value;
                if (ResourcePlanCode == 0 || ResourcePlanCode == "SELECT") {
                    var sResourcePlanCode = $('<%=spanzResourcePlanCode.ClientID %>').id;
                    spanlist = spanlist + sResourcePlanCode + ",";
                }
            }

            var Role = $('<%=ddlRole.ClientID %>').value;
            if (Role == 0 || Role == "SELECT") {
                var sRole = $('<%=spanzRole.ClientID %>').id;
                spanlist = spanlist + sRole + ",";
            }
           
            var DateofReq = $('<%=ucDatePicker.ClientID %>').id;
            /*Changes Made by Gaurav as per new Requirement*/
            var NoOfResources = $('<%=txtNoOfResources.ClientID %>').id;
            //var ProjectDesc = $( '<%=txtProjectDesc.ClientID %>').id;
                        
            if (spanlist == "") {
                if (Department == "1" || Department == "SELECT") {
                    /*Changes Made by Gaurav as per new Requirement*/
                    /*If Department is "Projects" than we don't require "Required From and "Required Till" date fields.*/
                    /*If Department is "Projects" than "Project Description" field is required*/
                    var ProjectDesc = $('<%=txtProjectDesc.ClientID %>').id;
                    //Poonam : Issue : 54907 : Starts
                    if (Department == "SELECT") {
                        var RequiredFrom = $('<%=ucDatePicker2.ClientID %>').id;
                        var controlList = spanlist + DateofReq + "," + RequiredFrom + "," /*+RequiredTill + ","*/ + NoOfResources + "," + ProjectDesc;
                    }
                    else {
                        var controlList = spanlist + DateofReq + "," + NoOfResources + "," + ProjectDesc;
                    }
                    //Poonam : Issue : 54907 : Ends
                    
                    var controlList1 = DateofReq + "," /*+ RequiredFrom +","+RequiredTill + ","*/ + NoOfResources;
                }
                else {
                    /*Changes Made by Gaurav as per new Requirement*/
                    /*If Department is not "Projects" than we  require "Required From and "Required Till" date fields.*/
                    var RequiredFrom = $('<%=ucDatePicker2.ClientID %>').id;
                    //var RequiredTill = $('<%=ucDatePicker1.ClientID %>').id
                    var Dept = $('<%=ddlDepartment.ClientID %>').options[$('<%=ddlDepartment.ClientID %>').selectedIndex].text;

                    /*Modified By Gaurav Thakkar to implement the mandatory field "Client Name"*/
                    if (Dept == "RaveForecastedProjects") {
                        var ClientName = $('<%=txtClientName.ClientID %>').id;
                        var controlList = DateofReq + "," + RequiredFrom + "," + NoOfResources + "," + ClientName;
                        var controlList1 = DateofReq + "," + RequiredFrom + "," + NoOfResources + "," + ClientName;
                    }
                    else {
                        var controlList = DateofReq + "," + RequiredFrom + "," + NoOfResources;
                        var controlList1 = DateofReq + "," + RequiredFrom + "," + NoOfResources;
                    }
                    /*End of changes*/
                }
            }
            else {
                if (Department == "1" || Department == "SELECT") {
                    /*Changes Made by Gaurav as per new Requirement*/
                    /*If Department is "Projects" than we don't require "Required From and "Required Till" date fields.*/
                    /*If Department is "Projects" than "Project Description" field is required*/
                    var ProjectDesc = $('<%=txtProjectDesc.ClientID %>').id;
                    //Poonam : Issue : 54907 : Starts
                    if (Department == "SELECT") {
                        var RequiredFrom = $('<%=ucDatePicker2.ClientID %>').id;
                        var controlList = spanlist + DateofReq + "," + RequiredFrom + "," /*+RequiredTill + ","*/ + NoOfResources + "," + ProjectDesc;
                    } 
                    else {
                        var controlList = spanlist + DateofReq + "," + NoOfResources + "," + ProjectDesc;
                    }
                    //Poonam : Issue : 54907 : Ends
                    var controlList1 = spanlist + DateofReq + "," /*+ RequiredFrom +","+RequiredTill + ","*/ + NoOfResources;
                }
                else {
                    /*Changes Made by Gaurav as per new Requirement*/
                    var RequiredFrom = $('<%=ucDatePicker2.ClientID %>').id
                    var Dept = $('<%=ddlDepartment.ClientID %>').options[$('<%=ddlDepartment.ClientID %>').selectedIndex].text;
                    /*Modified By Gaurav Thakkar to implement the mandatory field "Client Name"*/
                    if (Dept == "RaveForecastedProjects") {
                        var ClientName = $('<%=txtClientName.ClientID %>').id;
                        var controlList = spanlist + DateofReq + "," + RequiredFrom + "," + NoOfResources + "," + ClientName;
                        var controlList1 = spanlist + DateofReq + "," + RequiredFrom + "," + NoOfResources + "," + ClientName;
                    }
                    else {
                        var controlList = spanlist + DateofReq + "," + RequiredFrom + "," + NoOfResources;
                        var controlList1 = spanlist + DateofReq + "," + RequiredFrom + "," + NoOfResources;
                    }

                    /*End of changes*/
                }
            }


            if (ValidateControlOnClickEvent(controlList) == false) {
                lblMandatory = $('<%=lblMandatory.ClientID %>')
                lblMandatory.innerHTML = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
                $('<%=lblMessage.ClientID %>').value = "";
            }
            if (ValidateControlOnClickEvent(controlList1) == true) {
                if (Department == "1") {
                    ValidateProjectDescription();
                }
            }

            //            if (ValidateControlOnClickEvent(controlList1) == false)
            //             {  
            //                    ValidateDateOfRequsition();
            //            }      



            return ValidateControlOnClickEvent(controlList);
        }

        //        function ValidateDateOfRequsition(dateofReq) {
        //            //debugger;
        //            var dateofReq = $('<%=ucDatePicker.ClientID %>').value;
        //            var dateofReqForFocus = $('<%=ucDatePicker.ClientID %>').id;
        //            var date = new Date();
        //            var day = date.getDate();
        //            var month = date.getMonth();
        //            month++;
        //            var year = date.getFullYear();
        //            var comDate = day + "/" + month + "/" + year;

        //            if (dateofReq > comDate)
        //             {
        //              //  var lblMandatory = document.getElementById('<%=lblMandatory.ClientID %>');
        //                    alert('Invalid Date');
        //                    document.getElementById('<%=ucDatePicker.ClientID %>').focus();
        //                    return false;               
        //            }
        //        }

        function ValidateProjectDescription() {
            var ProjectName = $('<%=ddlProjectName.ClientID %>').value;
            if (ProjectName != "" || ProjectName != "SELECT") {
                var ProjectDesc = $('<%=txtProjectDesc.ClientID %>').id;
                if (ValidateControlOnClickEvent(ProjectDesc) == false) {
                    lblMandatory = $('<%=lblMandatory.ClientID %>')
                    lblMandatory.innerText = "Project Description field is not supplied.";
                    lblMandatory.style.color = "Red";
                    $('<%=lblMessage.ClientID %>').value = "";
                    return false;
                }

            }
            return true;
        }

        function ValidatedateforotherDepartments() {
            var locRequiredFromDate = $('<%=ucDatePicker2.ClientID %>').value;
            var locRequiredTillDate = $('<%=ucDatePicker1.ClientID %>').value;

            //Compare Required From greater then Required Till Date 
            if (compareDates(locRequiredFromDate, 'dd/MM/yyyy', locRequiredTillDate, 'dd/MM/yyyy') == 1) {
                alert("Please select the required from date lesser then required till date.");
                $('<%=txtNoOfResources.ClientID %>').value = "";
                return;
            }

            if (compareDates(locRequiredTillDate, 'dd/MM/yyyy', locRequiredFromDate, 'dd/MM/yyyy') == 0) {
                alert("Please select the required from date lesser then required till date.");
                $('<%=txtNoOfResources.ClientID %>').value = "";
                return;
            }


            ValidateControl($('<%=txtNoOfResources.ClientID %>').id, $('<%=imgNoOfResources.ClientID %>').id, 'IsNumeric');
        }
        //function ValidateCheckBox(chkid,RPDate,RPEndDate)
        function ValidateCheckBox(chkid) {
            //debugger;


            //        var locProjectEndDate = $( '<%=ucDatePicker1.ClientID %>').value;
            //        
            //        var locActualProjectStartDate = $( '<%=hidRequiredFrom.ClientID %>').value;
            //        var locActualProjectEndDate = $( '<%=hidRequiredTill.ClientID %>').value;
            //        
            //        //Check Required From is empty
            //        if(locProjectStartDate == "")
            //        {
            //            alert("Please Select Required From Date");
            //             $(chkid).checked = false;
            //            return;
            //        }
            //        
            //        //Check Required Till is empty
            //        if(locProjectEndDate == "")
            //        {
            //            alert("Please Select Required Till Date");
            //             $(chkid).checked = false;
            //            return;
            //        }
            //        
            //        //Compare Required From greater then Required Till Date 
            //        if(compareDates(locProjectStartDate,'dd/MM/yyyy',locProjectEndDate,'dd/MM/yyyy') == 1) 
            //        {
            //         alert("Please select the required from date lesser then required till date.");
            //            $(chkid).checked = false;
            //            return;
            //        }
            //        
            //        if(compareDates(locProjectEndDate,'dd/MM/yyyy',locProjectStartDate,'dd/MM/yyyy') == 0) 
            //        {
            //            alert("Please select the required from date lesser then required till date.");
            //            $(chkid).checked = false;
            //            return;
            //        }
            //        
            //        //Compare Required From greater then Project Start Date 
            //        if(compareDates(locProjectStartDate,'dd/MM/yyyy',locActualProjectStartDate,'dd/MM/yyyy') == 1) 
            //        {
            //         alert("Please select required from same as project start date.i.e. " + locActualProjectStartDate);
            //            $(chkid).checked = false;
            //            return;
            //        }
            //        
            //        //Compare Required From less then Project Start Date 
            //        if(compareDates(locActualProjectStartDate,'dd/MM/yyyy',locProjectStartDate,'dd/MM/yyyy') == 1) 
            //        {
            //         alert("Please select required from same as project start date.i.e " + locActualProjectStartDate);
            //            $(chkid).checked = false;
            //            return;
            //        }
            //        
            //        //Compare Required Till Greater then Project End Date
            //        if(compareDates(locProjectEndDate,'dd/MM/yyyy',locActualProjectEndDate,'dd/MM/yyyy') == 1) 
            //        {
            //         alert("Please select required till same as project end date.i.e " + locActualProjectEndDate);
            //            $(chkid).checked = false;
            //            return;
            //        }
            //        
            //        //Compare Required Till less then Project End Date
            //        if(compareDates(locActualProjectEndDate,'dd/MM/yyyy',locProjectEndDate,'dd/MM/yyyy') == 1) 
            //        {
            //         alert("Please select required till same as project end date.i.e " + locActualProjectEndDate);
            //            $(chkid).checked = false;
            //            return;
            //        }
            //        
            //        //Compare Required From to RP Start Date
            //        if(compareDates(locProjectStartDate,'dd/MM/yyyy',RPDate,'dd/MM/yyyy') == 1) 
            //        {
            //         alert("Please select those rows which fits in the range of required from and till.");
            //            $(chkid).checked = false;
            //            return;
            //        }
            //        
            //        //Compare Required Till to RP End Date
            //        if(compareDates(RPEndDate,'dd/MM/yyyy',locProjectEndDate,'dd/MM/yyyy') == 1) 
            //        {
            //        alert("Please select those rows which fits in the range of required from and till.");
            //            $(chkid).checked = false;
            //            return;
            //        }


            var CheckBoxID = $(chkid).checked;

            var lblNOOfResources = $('<%=txtNoOfResources.ClientID %>').value;
            if (lblNOOfResources == "") {
                var number = 0;
            }
            else {
                var number = parseInt(lblNOOfResources);
            }
            if (CheckBoxID == true) {
                number = number + 1;
            }
            else {
                number = number - 1;
            }
            if (number == 0) {
                $('<%=txtNoOfResources.ClientID %>').value = "";
            }
            else {
                $('<%=txtNoOfResources.ClientID %>').value = number;
                $('<%=hidResourcesCount.ClientID %>').value = number;
            }
        }

        function SelectAllCheckboxes(spanChk) {
            var oItem = spanChk.children;

            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];

            var xState = theBox.checked;

            var elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
            }
        }
    </script>

</asp:Content>
