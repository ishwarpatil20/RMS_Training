<%@ Page Language="C#" MasterPageFile="~/MasterPage/Employee.master" AutoEventWireup="true"
    CodeFile="EmpProfessionalCourses.aspx.cs" Inherits="EmpProfessionalCourses" Title="Professional Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphEmployeeContent" runat="Server">

    <script type="text/javascript" src="JavaScript/CommonValidations.js"></script>

    <div style="border: solid 1px black" id="divTechnologyDetails" runat="server">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
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
                            <asp:Label ID="lblTechnologyDetails" runat="server" Text="Professional Courses" CssClass="detailsheader"></asp:Label><label
                                style="display: none;" id="lblTechDetails" class="mandatorymark">*</label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Professionaldetails" runat="server">
                    <table width="100%">
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblCourseName" runat="server" Text="Course Name"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtCourseName" runat="server" ToolTip="Enter Course Name" MaxLength="30"></asp:TextBox>
                                <span id="spanCourseName" runat="server">
                                    <img id="imgCourseName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                            <td style="width: 25%">
                                <asp:Label ID="lblInstituteName" runat="server" Text="Institute Name"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtInstituteName" runat="server" ToolTip="Enter Institute Name"
                                    MaxLength="30"></asp:TextBox>
                                <span id="spanInstituteName" runat="server">
                                    <img id="imgInstituteName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblYearofPassing" runat="server" Text="Year of Passing"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtYearofPassing" runat="server" ToolTip="Enter Year Of Passing"
                                    MaxLength="4"></asp:TextBox>
                                <span id="spanYearofPassing" runat="server">
                                    <img id="imgYearofPassing" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                            <td style="width: 25%">
                                <asp:Label ID="lblScore" runat="server" Text="Score"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtScore" runat="server" ToolTip="Enter Score" MaxLength="4"></asp:TextBox>
                                <span id="spanScore" runat="server">
                                    <img id="imgScore" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblOutOf" runat="server" Text="Out Of"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtOutOf" runat="server" ToolTip="Enter Out Of" MaxLength="4"></asp:TextBox>
                                <span id="spanOutOf" runat="server">
                                    <img id="imgOutOf" runat="server" src="Images/cross.png" alt="" style="display: none;
                                        border: none;" />
                                </span>
                            </td>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnUpdateRow" TabIndex="14" runat="server" Text="Update" CssClass="button"
                                    OnClick="btnUpdate_Click" Visible="false"></asp:Button>
                                <asp:Button ID="btnCancelRow" TabIndex="14" runat="server" Text="Cancel" CssClass="button"
                                    OnClick="btnCancelRow_Click" Visible="false"></asp:Button>
                                <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAdd_Click"
                                    TabIndex="14" Text="Add Row" />
                            </td>
                        </tr>
                    </table>
                    <div id="divGVProfessionalCourses">
                        <asp:GridView ID="gvProfessionalCourses" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDeleting="gvProfessionalCourses_RowDeleting" OnRowEditing="gvProfessionalCourses_RowEditing">
                            <HeaderStyle CssClass="addrowheader" />
                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                            <RowStyle Height="20px" CssClass="textstyle" />
                            <Columns>
                                <%--<asp:BoundField DataField="ProfessionalId" HeaderText="ProfessionalId" />
                            <asp:BoundField DataField="EMPId" HeaderText="EMPId" />--%>
                                <asp:BoundField HeaderText="Course Name" DataField="CourseName"></asp:BoundField>
                                <asp:BoundField HeaderText="Institute Name" DataField="InstitutionName"></asp:BoundField>
                                <asp:BoundField HeaderText="Year of Passing" DataField="PassingYear"></asp:BoundField>
                                <asp:BoundField HeaderText="Score" DataField="Score"></asp:BoundField>
                                <asp:BoundField HeaderText="Out Of" DataField="Outof"></asp:BoundField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="ProfessionalId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ProfessionalId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Mode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgBtnEdit" CausesValidation="false" CommandName="Edit"
                                            ImageUrl="Images/Edit.gif" ToolTip="Edit ProfessionalCourses Detail" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgBtnDelete" CausesValidation="false" CommandName="Delete"
                                            ImageUrl="Images/Delete.gif" ToolTip="Delete ProfessionalCourses Detail" />
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
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"
                                    Visible="false" />
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
    setbackcolorToTab('divMenu_2');
    
    function ButtonClickValidate()
    {
      
        var lblMandatory;
        var spanlist = "";
         
        var CourseName = $('<%=txtCourseName.ClientID %>').id
        var InstituteName = $('<%=txtInstituteName.ClientID %>').id
        var YearOfPassing = $( '<%=txtYearofPassing.ClientID %>').id
        var Score =$('<%=txtScore.ClientID %>').id
        var OutOf = $('<%=txtOutOf.ClientID %>').id

        if(spanlist == "")
        {
            var controlList = CourseName +"," + InstituteName +","+YearOfPassing + "," + Score +","+ OutOf;
        }
        else
        {
            var controlList = spanlist + CourseName +"," + InstituteName +","+YearOfPassing + "," + Score +","+ OutOf;
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

        var CourseName = $('<%=txtCourseName.ClientID %>').value;
        var InstituteName = $('<%=txtInstituteName.ClientID %>').value;
        var YearOfPassing = $('<%=txtYearofPassing.ClientID %>').value;
        var Score = $( '<%=txtScore.ClientID %>').value;
        var OutOf = $('<%=txtOutOf.ClientID %>').value;
 
        var AllControlEmpty;

        if(CourseName == "" && InstituteName == ""  && YearOfPassing =="" && Score == "" && OutOf == "")
        {
           AllControlEmpty = "Yes";
        }
        
        if(AllControlEmpty != "Yes")         
        {
            if($('<%=btnAddRow.ClientID %>')== null)
            {
                alert("To save the Professional details entered, kindly click on Update");
                return false;
            }
            else
            {
                alert("To save the Professional details entered, kindly click on Add row");
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
