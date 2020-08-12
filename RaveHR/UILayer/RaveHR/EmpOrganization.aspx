<%@ Page Language="C#" MasterPageFile="~/MasterPage/Employee.master" AutoEventWireup="true"
    CodeFile="EmpOrganization.aspx.cs" Inherits="EmpOrganization" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                </table>
                <table width="100%">
                    <tr id="ExperienceRow" runat="server">
                        <td style="width: 25%">
                            <asp:Label ID="lblTotalExperience" runat="server" Text="Total Experience (Months)"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtTotalExperience" runat="server" ToolTip="Enter Company Name"
                                MaxLength="3" OnTextChanged="TotalExperienceTextChangedEventHandler" AutoPostBack="True"></asp:TextBox>
                            <span id="spanTotalExperience" runat="server">
                                <img id="imgTotalExperience" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblReleventExperience" runat="server" Text="Relevant Experience (Months)"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtReleventExperience" runat="server" ToolTip="Select Working Since"
                                MaxLength="3" OnTextChanged="ReleventExperienceTextChangedEventHandler" AutoPostBack="True"></asp:TextBox>
                            <span id="spanReleventExperience" runat="server">
                                <img id="imgReleventExperience" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                        </td>
                        <td style="width: 25%">
                        </td>
                        <td style="width: 25%">
                        </td>
                        <td style="width: 25%">
                        </td>
                    </tr>
                </table>
                <div id="divReleventDetail" runat="server">
                    <table width="100%" class="detailsbg">
                        <tr>
                            <td>
                                <asp:Label ID="lblOrganizationGroup" runat="server" Text="Relevant Experience" CssClass="detailsheader"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Releventdetails" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtCompanyName" runat="server" ToolTip="Enter Company Name" MaxLength="30"></asp:TextBox>
                                    <span id="spanCompanyName" runat="server">
                                        <img id="imgCompanyName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblWorkingSince" runat="server" Text="Working Since"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <span id="spanzMonthSince" runat="server">
                                        <asp:DropDownList ID="ddlMonthSince" runat="server" Width="85px" 
                                        onselectedindexchanged="ddlMonthSince_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </span><span id="spanMonthSince" runat="server">
                                        <img id="imgMonthSince" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span><span id="spanzYearSince" runat="server">
                                        <asp:DropDownList ID="ddlYearSince" runat="server" 
                                        onselectedindexchanged="ddlYearSince_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </span><span id="spanYearSince" runat="server">
                                        <img id="imgYearSince" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblWorkingTill" runat="server" Text="Working Till"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <span id="spanzMonthTill" runat="server">
                                        <asp:DropDownList ID="ddlMonthTill" runat="server" Width="85px" 
                                        onselectedindexchanged="ddlMonthTill_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </span><span id="spanMonthTill" runat="server">
                                        <img id="imgMonthTill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span><span id="spanzYearTill" runat="server">
                                        <asp:DropDownList ID="ddlYearTill" runat="server" 
                                        onselectedindexchanged="ddlYearTill_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </span><span id="spanYearTill" runat="server">
                                        <img id="imgYearTill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblPositionHeld" runat="server" Text="Position Held"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtPositionHeld" runat="server" ToolTip="Enter Position Held" MaxLength="20"></asp:TextBox>
                                    <span id="spanPositionHeld" runat="server">
                                        <img id="imgPositionHeld" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblExperience" runat="server" Text="Experience (Months)"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtExperience" runat="server" ToolTip="Enter Experience" 
                                        MaxLength="3" ReadOnly="True"></asp:TextBox>
                                    <span id="spanExperience" runat="server">
                                        <img id="imgExperience" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnUpdate" TabIndex="14" runat="server" Text="Update" CssClass="button"
                                        OnClick="btnUpdate_Click" Visible="false"></asp:Button>
                                    <asp:Button ID="btnCancel1" TabIndex="14" runat="server" Text="Cancel" CssClass="button"
                                        OnClick="btnCancel_Click" Visible="false"></asp:Button>
                                    <asp:Button ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" TabIndex="14"
                                        Text="Add Row" />
                                </td>
                            </tr>
                        </table>
                        <div id="divGVOrganisation">
                            <asp:GridView ID="gvOrganisation" runat="server" Width="100%" AutoGenerateColumns="False"
                                OnRowDeleting="gvOrganisation_RowDeleting" OnRowEditing="gvOrganisation_RowEditing">
                                <HeaderStyle CssClass="addrowheader" />
                                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                <RowStyle Height="20px" CssClass="textstyle" />
                                <Columns>
                                    <asp:BoundField HeaderText="Company Name" DataField="CompanyName"></asp:BoundField>
                                    <asp:BoundField HeaderText="Working Since" DataField="WorkingSince"></asp:BoundField>
                                    <asp:BoundField HeaderText="Working Till" DataField="WorkingTill"></asp:BoundField>
                                    <asp:BoundField HeaderText="Position Held" DataField="Designation"></asp:BoundField>
                                    <asp:BoundField HeaderText="Experience (Months)" DataField="Experience"></asp:BoundField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="OrganisationId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.OrganisationId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Mode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="MonthSince" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.MonthSince") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="YearSince" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.YearSince") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="MonthTill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.MonthTill") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="YearTill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.YearTill") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgBtnEdit" CausesValidation="false" CommandName="Edit"
                                                ImageUrl="Images/Edit.gif" ToolTip="Edit Organisation Detail" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgBtnDelete" CausesValidation="false" CommandName="Delete"
                                                ImageUrl="Images/Delete.gif" ToolTip="Delete Organisation Detail" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="EMPId" runat="server" />
                            <asp:HiddenField ID="hfTotalExperience" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
                <div id="buttonControl">
                    <table width="100%">
                        <tr align="right">
                            <td style="width: 90%">
                            </td>
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divNonReleventDetail" runat="server" visible="false">
                    <table width="100%" class="detailsbg">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Non Relevant Experience" CssClass="detailsheader"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="NonReleventDetails" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblRCompanyName" runat="server" Text="Company Name"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtRCompanyName" runat="server" ToolTip="Enter Company Name" MaxLength="30"></asp:TextBox>
                                    <span id="spanRCompanyName" runat="server">
                                        <img id="imgRCompanyName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblRWorkingSince" runat="server" Text="Working Since"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <span id="spanzRMonthSince" runat="server">
                                        <asp:DropDownList ID="ddlRMonthSince" runat="server" Width="85px" 
                                        onselectedindexchanged="ddlRMonthSince_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </span><span id="spanRMonthSince" runat="server">
                                        <img id="imgRMonthSince" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span><span id="spanzRYearSince" runat="server">
                                        <asp:DropDownList ID="ddlRYearSince" runat="server" 
                                        onselectedindexchanged="ddlRYearSince_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </span><span id="spanRYearSince" runat="server">
                                        <img id="imgRYearSince" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblRWorkingTill" runat="server" Text="Working Till"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <span id="spanzRMonthTill" runat="server">
                                        <asp:DropDownList ID="ddlRMonthTill" runat="server" Width="85px" 
                                        onselectedindexchanged="ddlRMonthTill_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </span><span id="spanRMonthTill" runat="server">
                                        <img id="imgRMonthTill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span><span id="spanzRYearTill" runat="server">
                                        <asp:DropDownList ID="ddlRYearTill" runat="server" 
                                        onselectedindexchanged="ddlRYearTill_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </span><span id="spanRYearTill" runat="server">
                                        <img id="imgRYearTill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblRPositionHeld" runat="server" Text="Position Held"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtRPositionHeld" runat="server" ToolTip="Enter Position Held" MaxLength="20"></asp:TextBox>
                                    <span id="spanRPositionHeld" runat="server">
                                        <img id="imgRPositionHeld" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblRExperience" runat="server" Text="Experience (Months)"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtRExperience" runat="server" ToolTip="Enter Experience" 
                                        MaxLength="3" ReadOnly="True"
                                        ></asp:TextBox>
                                    <span id="spanRExperience" runat="server">
                                        <img id="imgRExperience" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnUpdateRow" TabIndex="14" runat="server" Text="Update" CssClass="button"
                                        OnClick="btnUpdateRow_Click" Visible="false"></asp:Button>
                                    <asp:Button ID="btnCancelRow" TabIndex="14" runat="server" Text="Cancel" CssClass="button"
                                        OnClick="btnCancelRow_Click" Visible="false"></asp:Button>
                                    <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAddRow_Click"
                                        TabIndex="14" Text="Add Row" />
                                </td>
                            </tr>
                        </table>
                        <div id="divNonReleventDetails">
                            <asp:GridView ID="gvOrgNonReleventDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                                OnRowDeleting="gvOrgNonReleventDetails_RowDeleting" OnRowEditing="gvOrgNonReleventDetails_RowEditing">
                                <HeaderStyle CssClass="addrowheader" />
                                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                <RowStyle Height="20px" CssClass="textstyle" />
                                <Columns>
                                    <asp:BoundField HeaderText="Company Name" DataField="CompanyName"></asp:BoundField>
                                    <asp:BoundField HeaderText="Working Since" DataField="WorkingSince">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Working Till" DataField="WorkingTill">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Position Held" DataField="Designation"></asp:BoundField>
                                    <asp:BoundField HeaderText="Experience (Months)" DataField="Experience"></asp:BoundField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="OrganisationId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.OrganisationId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Mode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="MonthSince" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.MonthSince") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="YearSince" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.YearSince") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="MonthTill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.MonthTill") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="YearTill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.YearTill") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgBtnEdit" CausesValidation="false" CommandName="Edit"
                                                ImageUrl="Images/Edit.gif" ToolTip="Edit Organisation Detail" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgBtnDelete" CausesValidation="false" CommandName="Delete"
                                                ImageUrl="Images/Delete.gif" ToolTip="Delete Organisation Detail" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hfTotalNonExperience" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
                <table width="100%">
                    <tr align="right">
                        <td style="width: 90%">
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div id="Div2">
                    <table width="100%">
                        <tr align="right">
                            <td style="width: 30%">
                            </td>
                            <td style="width: 70%" align="right">
                                <asp:Button ID="btnPrevious" runat="server" CssClass="button" Text="Previous" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" CausesValidation="true" CssClass="button"
                                    OnClick="btnSave_Click" TabIndex="18" Text="Save" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="CancelDetails_Click" />
                                <asp:Button ID="btnNext" runat="server" CssClass="button" Text="Next" OnClick="btnNext_Click" />
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
    setbackcolorToTab('divMenu_4');
    
    function ButtonClickValidate()
    {
    
        var lblMandatory;
        var spanlist = "";
        
        var ReleventExperience = $('<%=txtReleventExperience.ClientID %>').id
        var TotalExperience = $('<%=txtTotalExperience.ClientID %>').id
        var CompanyName = $('<%=txtCompanyName.ClientID %>').id
        var Experience = $('<%=txtExperience.ClientID %>').id
        var PositionHeld = $( '<%=txtPositionHeld.ClientID %>').id
        var MonthSince = $('<%=ddlMonthSince.ClientID %>').value;
        var YearSince = $('<%=ddlYearSince.ClientID %>').value;
        var MonthTill = $('<%=ddlMonthTill.ClientID %>').value;
        var YearTill = $('<%=ddlYearTill.ClientID %>').value;
        
        if(MonthSince == "" || MonthSince == "Months")
        {
            var sMonthSince = $('<%=spanzMonthSince.ClientID %>').id;
            spanlist = sMonthSince +",";
        } 
        if(YearSince == "" || YearSince == "Years")
        {
            var sYearSince = $('<%=spanzYearSince.ClientID %>').id;
            spanlist = spanlist + sYearSince +",";
        } 
        if(MonthTill == "" || MonthTill == "Months")
        {
            var sMonthTill = $('<%=spanzMonthTill.ClientID %>').id;
            spanlist = spanlist + sMonthTill +",";
        } 
        if(YearTill == "" || YearTill == "Years")
        {
            var sYearTill = $('<%=spanzYearTill.ClientID %>').id;
            spanlist = spanlist + sYearTill +",";
        }
        
        if(spanlist == "")
        {
            var controlList = ReleventExperience +"," + TotalExperience +","+ CompanyName +"," + Experience +","+PositionHeld;
        }
        else
        {
            var controlList = spanlist + ReleventExperience +"," + TotalExperience +","+ CompanyName +"," + Experience +","+PositionHeld;
        }
        
        if(ValidateControlOnClickEvent(controlList) == false)
        {
            lblError = $( '<%=lblError.ClientID %>')
            lblError.innerText = "Please fill all mandatory fields.";
            lblError.style.color = "Red";
        }
        
        return ValidateControlOnClickEvent(controlList);
    }
    
     function RowButtonClickValidate()
    {
        var lblMandatory;
        var spanlist = "";

        var ReleventExperience = $('<%=txtReleventExperience.ClientID %>').id
        var TotalExperience = $('<%=txtTotalExperience.ClientID %>').id
        var CompanyName = $('<%=txtRCompanyName.ClientID %>').id
        var Experience = $('<%=txtRExperience.ClientID %>').id
        var PositionHeld = $( '<%=txtRPositionHeld.ClientID %>').id
        var MonthSince = $('<%=ddlRMonthSince.ClientID %>').value;
        var YearSince = $('<%=ddlRYearSince.ClientID %>').value;
        var MonthTill = $('<%=ddlRMonthTill.ClientID %>').value;
        var YearTill = $('<%=ddlRYearTill.ClientID %>').value;
        
        if(MonthSince == "" || MonthSince == "Months")
        {
            var sMonthSince = $('<%=spanzRMonthSince.ClientID %>').id;
            spanlist = sMonthSince +",";
        } 
        if(YearSince == "" || YearSince == "Years")
        {
            var sYearSince = $('<%=spanzRYearSince.ClientID %>').id;
            spanlist = spanlist + sYearSince +",";
        } 
        if(MonthTill == "" || MonthTill == "Months")
        {
            var sMonthTill = $('<%=spanzRMonthTill.ClientID %>').id;
            spanlist = spanlist + sMonthTill +",";
        } 
        if(YearTill == "" || YearTill == "Years")
        {
            var sYearTill = $('<%=spanzRYearTill.ClientID %>').id;
            spanlist = spanlist + sYearTill +",";
        }
         
        if(spanlist == "")
        {
            var controlList = ReleventExperience +"," + TotalExperience +","+ CompanyName +"," + Experience +","+PositionHeld;
        }
        else
        {
            var controlList = spanlist + ReleventExperience +"," + TotalExperience +","+ CompanyName +"," + Experience +","+PositionHeld;
        }
        
        if(ValidateControlOnClickEvent(controlList) == false)
        {
            lblError = $( '<%=lblError.ClientID %>')
            lblError.innerText = "Please fill all mandatory fields.";
            lblError.style.color = "Red";
        }
        
        return ValidateControlOnClickEvent(controlList);
    }
    
    function JavScriptFn(IsFresher,PreviousPage) 
    {
      if(IsFresher == "1")
      {
        alert('Since you are fresher you cannot add Organisation Details.');
        window.location=PreviousPage;

        return false; 
      }
      else
      return true;
       
    }
    
     function SaveButtonClickValidate()
    {
     
        var lblMandatory;
        
        var ReleventExperience = $('<%=txtReleventExperience.ClientID %>').id
        var TotalExperience = $('<%=txtTotalExperience.ClientID %>').id
 
        var controlList = ReleventExperience +"," + TotalExperience;

        if(ValidateControlOnClickEvent(controlList) == false)
        {
            lblError = $( '<%=lblError.ClientID %>')
            lblError.innerText = "Please fill all mandatory fields.";
            lblError.style.color = "Red";
            
            return ValidateControlOnClickEvent(controlList);
        }
        else
        {
            var ReleventExperience = $('<%=txtReleventExperience.ClientID %>').value;
            var TotalExperience = $('<%=txtTotalExperience.ClientID %>').value;
            var CompanyName = $('<%=txtCompanyName.ClientID %>').value;
            var Experience = $('<%=txtExperience.ClientID %>').value;
            var PositionHeld = $( '<%=txtPositionHeld.ClientID %>').value;
            var MonthSince = $('<%=ddlMonthSince.ClientID %>').value;
            var YearSince = $('<%=ddlYearSince.ClientID %>').value;
            var MonthTill = $('<%=ddlMonthTill.ClientID %>').value;
            var YearTill = $('<%=ddlYearTill.ClientID %>').value;
     
            var AllControlEmpty;

            if(CompanyName =="" && Experience == "" && PositionHeld == "" && MonthSince =="Months" && YearSince == "Years" && MonthTill == "Months" && YearTill == "Years")
            {
               AllControlEmpty = "Yes";
            }
            
            if(AllControlEmpty != "Yes")         
            {
                if($('<%=btnAdd.ClientID %>')== null)
                {
                    alert("To save the Relevant Experience details entered, kindly click on Update");
                    return false;
                }
                else
                {
                    alert("To save the Relevant Experience details entered, kindly click on Add row");
                    return false;
                }   
            }  
            else
            {
                var ReleventExperience = $('<%=txtReleventExperience.ClientID %>').value;
                var TotalExperience = $('<%=txtTotalExperience.ClientID %>').value;
                 $('<%=divNonReleventDetail.ClientID %>')
                
                //if(ReleventExperience!=TotalExperience)
                if($('<%=divNonReleventDetail.ClientID %>') != null)
                {
                    var CompanyName = $('<%=txtRCompanyName.ClientID %>').value;
                    var Experience = $('<%=txtRExperience.ClientID %>').value;
                    var PositionHeld = $( '<%=txtRPositionHeld.ClientID %>').value;
                    var MonthSince = $('<%=ddlRMonthSince.ClientID %>').value;
                    var YearSince = $('<%=ddlRYearSince.ClientID %>').value;
                    var MonthTill = $('<%=ddlRMonthTill.ClientID %>').value;
                    var YearTill = $('<%=ddlRYearTill.ClientID %>').value;
             
                    var AllRelevantControlEmpty;

                    if(CompanyName =="" && Experience == "" && PositionHeld == "" && MonthSince =="Months" && YearSince == "Years" && MonthTill == "Months" && YearTill == "Years")
                    {
                       AllRelevantControlEmpty = "Yes";
                    }
                    
                    if(AllRelevantControlEmpty != "Yes")         
                    {
                        if($('<%=btnAddRow.ClientID %>')== null)
                        {
                            alert("To save the Non Relevant Experience details entered, kindly click on Update");
                            return false;
                        }
                        else
                        {
                            alert("To save the Non Relevant Experience details entered, kindly click on Add row");
                            return false;
                        }   
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                   return true;
                } 
            }
        }
        
    }
    </script>

</asp:Content>
