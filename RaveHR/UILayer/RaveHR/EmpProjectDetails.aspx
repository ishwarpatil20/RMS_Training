<%@ Page Language="C#" MasterPageFile="~/MasterPage/Employee.master" AutoEventWireup="true"
    CodeFile="EmpProjectDetails.aspx.cs" Inherits="EmpProjectDetails" Title="Project Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEmployeeContent" runat="Server">
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
                    <tr class="detailsbg">
                        <td>
                            <asp:Label ID="lblProjectDetails" runat="server" Text="Project Details" CssClass="detailsheader"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Projectdetails" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblBifurcation" runat="server" Text="Bifurcation of projects"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td>
                                <span id="spanzBifurcation" runat="server">
                                    <asp:DropDownList ID="ddlBifurcation" runat="server" ToolTip="Select Bifurcation"
                                        Width="155px" 
                                    onselectedindexchanged="ddlBifurcation_SelectedIndexChanged" AutoPostBack="true" >
                                    </asp:DropDownList>
                                </span>
                            </td>
                            <td>
                                <asp:Label ID="lblProjectName" runat="server" Text="Project Name"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProjectName" runat="server" MaxLength="20" ToolTip="Enter Project Name"></asp:TextBox>
                                <span id="spanProjectName" runat="server">
                                    <img id="imgProjectName" runat="server" alt="" src="Images/cross.png" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                                <label class="mandatorymark" id="lblmandatorymark" runat="server">
                                    *</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="20" ToolTip="Enter Organisation"
                                    Wrap="False"></asp:TextBox>
                                <span id="spanCompanyName" runat="server">
                                    <img id="imgCompanyName" runat="server" alt="" src="Images/cross.png" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                            <td>
                                <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td>
                                <span id="spanzLocation" runat="server">
                                    <asp:DropDownList ID="ddlLocation" runat="server" Width="155px" ToolTip="Select Location">
                                    </asp:DropDownList>
                                </span><span id="spanLocation" runat="server">
                                    <img id="imgLocation" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblClientName" runat="server" Text="Client Name"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtClientName" runat="server" MaxLength="20" ToolTip="Enter Client Name"></asp:TextBox>
                                <span id="spanClientName" runat="server">
                                    <img id="imgClientName" runat="server" alt="" src="Images/cross.png" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                            <td>
                                <asp:Label ID="lblProjectSize" runat="server" Text="Project Size"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProjectSiZe" runat="server" ToolTip="Enter Duration" MaxLength="4"></asp:TextBox>
                                <span id="spanProjectSize" runat="server">
                                    <img id="imgProjectSize" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDuration" runat="server" Text="Duration (Months)"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDuration" runat="server" ToolTip="Enter Duration" MaxLength="4"></asp:TextBox>
                                <span id="spanDuration" runat="server">
                                    <img id="imgDuration" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                            <td>
                                <asp:Label ID="lblRole" runat="server" Text="Role"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRole" runat="server" ToolTip="Enter Role" MaxLength="40"></asp:TextBox>
                                <span id="spanRole" runat="server">
                                    <img id="imgRole" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProjectDescription" runat="server" Text="Project Description"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProjectDescription" runat="server" TextMode="MultiLine" ToolTip="Enter Project Description"
                                    MaxLength="500"></asp:TextBox>
                                <span id="spanProjectDescription" runat="server">
                                    <img id="imgProjectDescription" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                            <td>
                                <asp:Label ID="lblResponsibility" runat="server" Text="Responsibility"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtResponsibility" runat="server" TextMode="MultiLine" ToolTip="Enter Responsibility"
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
                                    TabIndex="14" Text="Add Row" />
                            </td>
                        </tr>
                    </table>
                    <div id="divGVProjectDetails">
                        <asp:GridView ID="gvProjectDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDeleting="gvProjectDetails_RowDeleting" OnRowEditing="gvProjectDetails_RowEditing">
                            <HeaderStyle CssClass="addrowheader" />
                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                            <RowStyle Height="20px" CssClass="textstyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Bifurcation" DataField="ProjectDoneName"></asp:BoundField>
                                <asp:BoundField HeaderText="Project Name" DataField="ProjectName"></asp:BoundField>
                                <asp:BoundField HeaderText="Company Name" DataField="Organisation"></asp:BoundField>
                                <asp:BoundField HeaderText="Location" DataField="ProjectLocation"></asp:BoundField>
                                <asp:BoundField HeaderText="Client Name" DataField="ClientName"></asp:BoundField>
                                <asp:BoundField HeaderText="Project Size" DataField="ProjectSize"></asp:BoundField>
                                <asp:BoundField HeaderText="Duration" DataField="OnsiteDuration"></asp:BoundField>
                                <asp:BoundField HeaderText="Role" DataField="Role"></asp:BoundField>
                                <asp:BoundField HeaderText="Project Description" DataField="Description"></asp:BoundField>
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
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="EMPId" runat="server" />
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
                                <asp:Button ID="btnPrevious" runat="server" CssClass="button" OnClick="btnPrevious_Click"
                                    Text="Previous" />
                                <asp:Button ID="btnSave" runat="server" CausesValidation="true" CssClass="button"
                                    OnClick="btnSave_Click" TabIndex="18" Text="Save" Visible="false" Width="119px" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick="btnCancel_Click"
                                    Visible="false" Text="Cancel" />
                                <asp:Button ID="btnNext" runat="server" CssClass="button" OnClick="btnNext_Click"
                                    Text="Next" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script language="javascript" type="text/javascript">
    function $(v){return document.getElementById(v);}
    
    //Highlighting the tabs by passing individaul tab id
    setbackcolorToTab('divMenu_6');
    
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

</asp:Content>
