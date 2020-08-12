<%@ Page Language="C#" MasterPageFile="~/MasterPage/Employee.master" AutoEventWireup="true"
    CodeFile="EmpSkillsDetails.aspx.cs" Inherits="EmpSkillsDetails" Title="Skills" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEmployeeContent" runat="Server">
    <div class="detailsborder">
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                    <tr id="SkillTypeRow" runat="server">
                        <td style="width: 16.66%">
                            <asp:Label ID="lblSkillType" runat="server" Text="Skills Type"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 16.66%">
                            <span id="spanzSkillsType" runat="server">
                                <asp:DropDownList ID="ddlSkillsType" runat="server" Width="135px" OnSelectedIndexChanged="ddlSkillsType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </span><span id="span2" runat="server">
                                <img id="img1" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 16.66%">
                        </td>
                        <td style="width: 16.66%">
                            <span id="span3" runat="server">
                                <img id="img2" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 16.66%">
                        </td>
                        <td style="width: 16.66%">
                            <span id="span5" runat="server">
                                <img id="img3" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                </table>
                <table width="100%" class="detailsbg">
                    <tr>
                        <td>
                            <asp:Label ID="lblMustSkills" runat="server" Text="Skills" CssClass="detailsheader"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Skillsdetails" runat="server">
                <table width="100%">
                    <tr>
                        <td style="width: 16.66%">
                            <asp:Label ID="lblSkills" runat="server" Text="Skills"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 16.66%">
                            <span id="spanzSkills" runat="server">
                                <asp:DropDownList ID="ddlSkills" runat="server" Width="135px">
                                </asp:DropDownList>
                            </span><span id="spanSkills" runat="server">
                                <img id="imgSkills" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 16.66%">
                            <asp:Label ID="lblYearsOfExperience" runat="server" Text="Years of Experience"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 16.66%">
                            <asp:TextBox ID="txtYearsOfExperience" runat="server" ToolTip="Enter Years of Experience"
                                MaxLength="5" Width="70%"></asp:TextBox>
                            <span id="spanYearsOfExperience" runat="server">
                                <img id="imgYearsOfExperience" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 16.66%">
                            <asp:Label ID="lblProficiencyLevel" runat="server" Text="Proficiency Level"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 16.66%">
                            <span id="spanzProficiencyLevel" runat="server">
                                <asp:DropDownList ID="ddlProficiencyLevel" runat="server" Width="135px">
                                </asp:DropDownList>
                            </span><span id="spanProficiencyLevel" runat="server">
                                <img id="imgProficiencyLevel" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16.66%">
                            <asp:Label ID="lblLastUsedDate" runat="server" Text="Last Used Date"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 16.66%">
                            <asp:TextBox ID="txtLastUsedDate" runat="server" ToolTip="Select Last Used date" Width="70%"></asp:TextBox>
                            <asp:ImageButton ID="imgLastUsedDate" runat="server" CausesValidation="false" ImageAlign="AbsMiddle"
                                ImageUrl="Images/Calendar_scheduleHS.png" TabIndex="17" />
                            <cc1:CalendarExtender ID="CalendarExtenderLastUsedDate" runat="server" PopupButtonID="imgLastUsedDate"
                                TargetControlID="txtLastUsedDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <span id="spanLastUsedDate" runat="server">
                                <img id="imgLastUsedDateError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 16.66%">
                        </td>
                        <td colspan="3" align = "right">
                            
                            <asp:Button ID="btnUpdate" runat="server" CssClass="button" 
                                OnClick="btnUpdate_Click" TabIndex="14" Text="Update" Visible="false" />
                            <asp:Button ID="btnCancelRow" runat="server" CssClass="button" 
                                OnClick="btnCancelRow_Click" TabIndex="14" Text="Cancel" Visible="false" />
                            <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAdd_Click" 
                                TabIndex="14" Text="Add Row" />
                        </td>
                    </tr>
                </table>
                <div id="divGVSkills">
                    <asp:GridView ID="gvSkills" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowDeleting="gvSkills_RowDeleting" OnRowEditing="gvSkills_RowEditing">
                        <HeaderStyle CssClass="headerStyle" />
                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                        <RowStyle Height="20px" CssClass="textstyle" />
                        <Columns>
                            <asp:BoundField DataField="SkillName" HeaderText="Skill" />
                            <asp:BoundField DataField="Experience" HeaderText="Years Of Experience" />
                            <asp:BoundField DataField="ProficiencyLevel" HeaderText="Proficiency Level" />
                            <asp:BoundField DataField="LastUsedDate" HeaderText="Last Used Date" DataFormatString="{0:dd/MM/yyyy}"/>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="SkillsId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.SkillsId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Mode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Skill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Skill") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Proficiency" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Proficiency") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="SkillType" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.SkillType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="true">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/Edit.gif" CommandName="Edit"
                                        ToolTip="Edit Skill Detail" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="true">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                        CommandName="Delete" ToolTip="Delete Skill Detail" />
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
                                &nbsp;</td>
                            <td style="width: 70%" align="right">
                                &nbsp;</td>
                        </tr>
                        <tr align="right">
                            <td style="width: 30%">
                            </td>
                            <td align="right" style="width: 70%">
                                <asp:Button ID="btnPrevious" runat="server" CssClass="button" 
                                    onclick="btnPrevious_Click" Text="Previous" />
                                <asp:Button ID="btnSave" runat="server" CausesValidation="true" 
                                    CssClass="button" OnClick="btnSave_Click" TabIndex="18" Text="Save" 
                                    Visible="false" Width="119px" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" 
                                    onclick="btnCancel_Click" Text="Cancel" Visible="false"/>
                                <asp:Button ID="btnNext" runat="server" CssClass="button" 
                                    onclick="btnNext_Click" Text="Next" />
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
    setbackcolorToTab('divMenu_5');
    
    function ButtonClickValidate()
    {
        
        var lblMandatory;
        var spanlist = "";

        var Name = $('<%=txtYearsOfExperience.ClientID %>').id
        var LastUsedDate = $('<%=txtLastUsedDate.ClientID %>').id
        var Skills = $('<%=ddlSkills.ClientID %>').value;
        var ProficiencyLevel = $('<%=ddlProficiencyLevel.ClientID %>').value;
        var SkillsType = $('<%=ddlSkillsType.ClientID %>').value;
         
        if(Skills == "" || Skills == "SELECT")
        {
            var sSkills = $('<%=spanzSkills.ClientID %>').id;
            spanlist = sSkills +",";
        }
        
        if(ProficiencyLevel == "" || ProficiencyLevel == "SELECT")
        {
            var sProficiencyLevel = $('<%=spanzProficiencyLevel.ClientID %>').id;
            spanlist = spanlist + sProficiencyLevel +",";
        }
        
        if(SkillsType == "" || SkillsType == "SELECT")
        {
            var sSkillsType = $('<%=spanzSkillsType.ClientID %>').id;
            spanlist = spanlist + sSkillsType +",";
        }
        
        if(spanlist == "")
        {
            var controlList = Name +"," + LastUsedDate;
        }
        else
        {
            var controlList = spanlist + Name +"," + LastUsedDate;
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

        var YearsOfExperience = $('<%=txtYearsOfExperience.ClientID %>').value;
        var LastUsedDate = $('<%=txtLastUsedDate.ClientID %>').value;
        var Skills = $('<%=ddlSkills.ClientID %>').value;
        var ProficiencyLevel = $( '<%=ddlProficiencyLevel.ClientID %>').value;

        var AllControlEmpty;
        
        if(YearsOfExperience == "" && LastUsedDate == ""  && Skills =="SELECT" && ProficiencyLevel == "SELECT")
        {
           AllControlEmpty = "Yes";
        }
        
        if(AllControlEmpty != "Yes")         
        {
            if($('<%=btnAddRow.ClientID %>')== null)
            {
                alert("To save the Skills details entered, kindly click on Update");
                return false;
            }
            else
            {
                alert("To save the Skills details entered, kindly click on Add row");
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
