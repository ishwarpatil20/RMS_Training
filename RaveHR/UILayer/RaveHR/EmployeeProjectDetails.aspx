<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeProjectDetails.aspx.cs" Inherits="EmployeeProjectDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
             <%--  Sanju:Issue Id 50201:End--%>
                <asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label>
            </td>
             <%-- Sanju:Issue Id 50201: Added background color property so that all browsers display header--%>
            <td align="right" style="height: 25px; width:20%;background-color:#7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
              <%--  Sanju:Issue Id 50201:End--%>
                <asp:Label ID="lblempName" runat="server" CssClass="header"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
    </table>
    
    <asp:Table ID="tblMain" runat="server" Width="100%" BorderColor="AliceBlue" BorderStyle="Solid"
        BorderWidth="2" Height="100%">
        <asp:TableRow ID="TableRow1" runat="server" Width="100%" VerticalAlign="Top">
            <asp:TableCell ID="tcellIndex" Width="15%" Height="100%" runat="server" BorderColor="Beige"
                BorderWidth="1">
                <!-- Panel for user control -->
                <asp:Panel ID="pnlUserControl" runat="server">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell ID="tcellContent" Width="85%" Height="100%" runat="server">
                <!-- Dump aspx code here -->
                <div class="detailsborder">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblError" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
                                    </td>
                                </tr>
                                <tr class="detailsbg">
                                    <td>
                                        <asp:Label ID="lblProjectDetails" runat="server" Text="Project Details" CssClass="detailsheader"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Projectdetails" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblBifurcation" runat="server" Text="Project Location"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td>
                                            <span id="spanzBifurcation" runat="server">
                                                <asp:DropDownList ID="ddlBifurcation" runat="server" ToolTip="Select Project Location"
                                                    Width="155px" OnSelectedIndexChanged="ddlBifurcation_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblProjectName" runat="server" Text="Project Name"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="mandatoryField" MaxLength="50" ToolTip="Enter Project Name" ></asp:TextBox>
                                            <span id="spanProjectName" runat="server">
                                                <img id="imgProjectName" runat="server" alt="" src="Images/cross.png" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                                            <label class="mandatorymark" id="lblmandatorymark" runat="server">
                                                *</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompanyName" runat="server"  MaxLength="20" ToolTip="Enter Organisation" 
                                                Wrap="False" CssClass="mandatoryField"></asp:TextBox>
                                            <span id="spanCompanyName" runat="server">
                                                <img id="imgCompanyName" runat="server" alt="" src="Images/cross.png" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblLocation" runat="server" Text="Site"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td>
                                            <span id="spanzLocation" runat="server">
                                                <asp:DropDownList ID="ddlLocation" runat="server" Width="155px" CssClass="mandatoryField" ToolTip="Select Site">
                                                </asp:DropDownList>
                                            </span><span id="spanLocation" runat="server">
                                                <img id="imgLocation" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblClientName" runat="server" Text="Client Name"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtClientName" runat="server" MaxLength="20" CssClass="mandatoryField" ToolTip="Enter Client Name"></asp:TextBox>
                                            <span id="spanClientName" runat="server">
                                                <img id="imgClientName" runat="server" alt="" src="Images/cross.png" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblProjectSize" runat="server" Text="Team Size"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProjectSiZe" runat="server" CssClass="mandatoryField" ToolTip="Enter Duration" MaxLength="4"></asp:TextBox>
                                            <span id="spanProjectSize" runat="server">
                                                <img id="imgProjectSize" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblDuration" runat="server" Text="Duration(In Months)"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDuration" runat="server" CssClass="mandatoryField" ToolTip="Enter Duration" MaxLength="4"></asp:TextBox>
                                            <span id="spanDuration" runat="server">
                                                <img id="imgDuration" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblRole" runat="server" Text="Role"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRole" runat="server" CssClass="mandatoryField" ToolTip="Enter Role" MaxLength="40"></asp:TextBox>
                                            <span id="spanRole" runat="server">
                                                <img id="imgRole" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblProjectDescription" runat="server" Text="Project Description"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProjectDescription" runat="server" CssClass="mandatoryField" TextMode="MultiLine" ToolTip="Enter Project Description"
                                                MaxLength="500"></asp:TextBox>
                                            <span id="spanProjectDescription" runat="server">
                                                <img id="imgProjectDescription" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td class="txtstyle">
                                            <asp:Label ID="lblResponsibility" runat="server" Text="Responsibility"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtResponsibility" runat="server" CssClass="mandatoryField" TextMode="MultiLine" ToolTip="Enter Responsibility"
                                                MaxLength="500"></asp:TextBox>
                                            <span id="spanResponsibility" runat="server">
                                                <img id="imgResponsibility" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="btnUpdate" TabIndex="14" runat="server" Text="Update" CssClass="button"
                                                OnClick="btnUpdate_Click" Visible="false"></asp:Button>
                                            <asp:Button ID="btnCancelRow" TabIndex="14" runat="server" Text="Cancel" CssClass="button"
                                                OnClick="btnCancelRow_Click" Visible="false"></asp:Button>
                                            <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAdd_Click"
                                                TabIndex="14" Text="Save" />     
                                            <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick="btnCancel_Click"
                                                Text="Cancel" />  
                                        </td>
                                    </tr>
                                </table>
                                <div id="divGVProjectDetails">
                                    <asp:GridView ID="gvProjectDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnRowDeleting="gvProjectDetails_RowDeleting" OnRowEditing="gvProjectDetails_RowEditing" >
                                        <HeaderStyle CssClass="addrowheader" />
                                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                        <RowStyle Height="20px" CssClass="textstyle" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Project Location" DataField="ProjectDoneName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Project Name" DataField="ProjectName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Company Name" DataField="Organisation"></asp:BoundField>
                                            <asp:BoundField HeaderText="Site" DataField="ProjectLocation"></asp:BoundField>
                                            <asp:BoundField HeaderText="Client Name" DataField="ClientName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Project Size" DataField="ProjectSize"></asp:BoundField>
                                            <asp:BoundField HeaderText="Duration(In Months)" DataField="OnsiteDuration"></asp:BoundField>
                                            <asp:BoundField HeaderText="Role" DataField="Role"></asp:BoundField>
                                           
                                            <asp:TemplateField HeaderText="Project Description"> <ItemTemplate> <asp:TextBox ID="ProjectDescription" runat="server"  TextMode="MultiLine" Rows="3" Width="100" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' > </asp:TextBox></ItemTemplate> </asp:TemplateField>
                                            <asp:TemplateField> <ItemTemplate> <asp:HyperLink ID="lnkProjectDetail" runat="server"> </asp:HyperLink> </ItemTemplate></asp:TemplateField>
                                            <asp:BoundField HeaderText="Responsibility" DataField="Resposibility"></asp:BoundField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="ProjectId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ProjectId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Mode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="LocationId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.LocationId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="ProjectDoneStatus" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ProjectDone") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="imgBtnEdit" CausesValidation="false" CommandName="Edit"
                                                        ImageUrl="Images/Edit.gif" ToolTip="Edit Project Detail" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="imgBtnDelete" CausesValidation="false" CommandName="Delete"
                                                        ImageUrl="Images/Delete.gif" ToolTip="Delete Project Detail" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="imgBtnRankUp" CausesValidation="false" 
                                                        ImageUrl="Images/RankUp.jpg" ToolTip="Rank Up" OnClick ="imgBtnRankUp_Click" />
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="true">
                                                <ItemTemplate>
                                                     <asp:ImageButton runat="server" ID="imgBtnRankDown" CausesValidation="false" 
                                                        ImageUrl="Images/RankDown.jpg" ToolTip="Rank Down" OnClick ="imgBtnRankDown_Click"/>
                                               <asp:HiddenField ID="hdfRankOrder" runat="server"  Value='<%# DataBinder.Eval(Container, "DataItem.rankorder") %>'></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="EMPId" runat="server" />
                                    <asp:HiddenField ID="HfIsDataModified" runat="server" />
                                </div>
                            </asp:Panel>
                            <div id="buttonControl">
                                <table width="100%">
                                    <tr align="right">
                                        <td style="width: 30%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 70%" align="right">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td style="width: 30%">
                                        </td>
                                        <td align="right" style="width: 70%">
                                                <asp:Button ID="btnEdit" runat="server" CausesValidation="true" CssClass="button"
                                                OnClick="btnEdit_Click" TabIndex="18" Text="Edit" Visible="false" Width="119px" />
                                            <asp:Button ID="btnEditCancel" runat="server" CausesValidation="true" CssClass="button"
                                                OnClick="btnEditCancel_Click" TabIndex="18" Text="Cancel" Visible="false" Width="119px" /> 
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <script language="javascript" type="text/javascript">
                
                    function $(v){return document.getElementById(v);}
           
                    setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmployeeProfile');

                    
                             
                    function ButtonClickValidate()
                    {    
                        var lblMandatory;
                        var spanlist = "";
                        var Location = $('<%=ddlLocation.ClientID %>').value;
                        var Bifurcation = $('<%=ddlBifurcation.ClientID %>').value;
                         
                        if(Location == "" || Location == "SELECT")
                        {
                            var sLocation = $('<%=spanzLocation.ClientID %>').id;
                            spanlist = sLocation +",";
                        }
                        
                        if(Bifurcation == "" || Bifurcation == "SELECT")
                        {
                            var sBifurcation = $('<%=spanzBifurcation.ClientID %>').id;
                            spanlist = spanlist + sBifurcation + ",";
                        }
                        
                            var ProjectName = $('<%=txtProjectName.ClientID %>').id
                            var CompanyName = $('<%=txtCompanyName.ClientID %>').id
                            var ClientName = $( '<%=txtClientName.ClientID %>').id
                            var Duration =$('<%=txtDuration.ClientID %>').id
                            var ProjectSize = $('<%=txtProjectSiZe.ClientID %>').id
                            var ProjectDescription = $('<%=txtProjectDescription.ClientID %>').id
                            var Role = $('<%=txtRole.ClientID %>').id
                            var Responsibility = $('<%=txtResponsibility.ClientID %>').id
                             
                            if(spanlist == "")
                            {
                                var controlList = ProjectName +"," + CompanyName +","+ClientName + "," + Duration +","+ ProjectSize+","+ ProjectDescription+ "," + Role +","+ Responsibility;
                            }
                            else
                            {
                                var controlList = spanlist + ProjectName +"," + CompanyName +","+ClientName + "," + Duration +","+ ProjectSize+","+ ProjectDescription+ "," + Role +","+ Responsibility;
                            }

                        if(ValidateControlOnClickEvent(controlList) == false)
                        {
                            lblError = $( '<%=lblError.ClientID %>')
                            lblError.innerText = "Please fill all mandatory fields.";
                            lblError.style.color = "Red";
                        }
                        
                        return ValidateControlOnClickEvent(controlList);
                    }
                    
                     function SaveButtonClickValidate()
                    {
                        var lblMandatory;
                        var spanlist = "";

                        var Bifurcation = $('<%=ddlBifurcation.ClientID %>').value;
                        var ProjectName = $('<%=txtProjectName.ClientID %>').value
                        var CompanyName = $('<%=txtCompanyName.ClientID %>').value
                        var ClientName = $( '<%=txtClientName.ClientID %>').value
                        var Duration =$('<%=txtDuration.ClientID %>').value
                        var ProjectSize = $('<%=txtProjectSiZe.ClientID %>').value
                        var ProjectDescription = $('<%=txtProjectDescription.ClientID %>').value
                        var Role = $('<%=txtRole.ClientID %>').value
                        var Responsibility = $('<%=txtResponsibility.ClientID %>').value
                        var Location = $('<%=ddlLocation.ClientID %>').value;
                            
                            var AllControlEmpty;
                            
                            if(ProjectName == "" && CompanyName == ""  && ClientName == "" && Duration == "" && ProjectSize == "" && ProjectDescription == "" && Role=="" && Responsibility=="" && Location == "SELECT" && Bifurcation == "SELECT")
                            {
                               AllControlEmpty = "Yes";
                            }
                            
                            if(AllControlEmpty != "Yes")         
                            {
                                if($('<%=btnAddRow.ClientID %>')== null)
                                {
                                    alert("To save the Project details entered, kindly click on Update");
                                    return false;
                                }
                                else
                                {
                                    alert("To save the Project details entered, kindly click on Add row");
                                    return false;
                                }   
                            }  
                            else
                            {
                                return true;
                            }
                    }
    
                </script>

            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
