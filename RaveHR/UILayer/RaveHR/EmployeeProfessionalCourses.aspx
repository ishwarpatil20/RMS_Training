<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeProfessionalCourses.aspx.cs" Inherits="EmployeeProfessionalCourses" %>

<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript" src="JavaScript/CommonValidations.js"></script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
         <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
          <%--  Sanju:Issue Id 50201:End--%>
                <asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label>
            </td>
             <%-- Sanju:Issue Id 50201: Added background color property so that all browsers display header--%>
            <td align="right" style="height: 25px; width: 20%;background-color:#7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
               <%--   Sanju:Issue Id 50201:end--%>
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
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <table width="100%">
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
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblCourseName" runat="server" Text="Course Name"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtCourseName" runat="server" CssClass="mandatoryField" ToolTip="Enter Course Name"
                                                    MaxLength="20"></asp:TextBox>
                                                <span id="spanCourseName" runat="server">
                                                    <img id="imgCourseName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblInstituteName" runat="server" Text="Institute Name"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtInstituteName" CssClass="mandatoryField" runat="server" ToolTip="Enter Institute Name"
                                                    MaxLength="50"></asp:TextBox>
                                                <span id="spanInstituteName" runat="server">
                                                    <img id="imgInstituteName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblYearofPassing" runat="server" Text="Year of Passing"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtYearofPassing" CssClass="mandatoryField" runat="server" ToolTip="Enter Year Of Passing"
                                                    MaxLength="4"></asp:TextBox>
                                                <span id="spanYearofPassing" runat="server">
                                                    <img id="imgYearofPassing" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblScore" runat="server" Text="Percentage/Grade"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtScore" runat="server" CssClass="mandatoryField" ToolTip="Enter Score"
                                                    MaxLength="5" onkeypress="return AlphanumericWithPlusMinus(event,this)"></asp:TextBox>
                                                <span id="spanScore" runat="server">
                                                    <img id="imgScore" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                            </td>
                                            <td style="width: 25%">
                                            </td>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="btnUpdateRow" runat="server" Text="Update" CssClass="button"
                                                    OnClick="btnUpdate_Click" Visible="false"></asp:Button>
                                                <asp:Button ID="btnCancelRow" runat="server" Text="Cancel" CssClass="button"
                                                    OnClick="btnCancelRow_Click" Visible="false"></asp:Button>
                                                <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAdd_Click"
                                                    Text="Save" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"
                                                     />
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
                                            <asp:BoundField HeaderText="Course Name" DataField="CourseName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Institute Name" DataField="InstitutionName"></asp:BoundField>
                                            <asp:BoundField HeaderText="Year of Passing" DataField="PassingYear"></asp:BoundField>
                                            <asp:BoundField HeaderText="Percentage/Grade" DataField="Score"></asp:BoundField>
                                            <%--<asp:BoundField HeaderText="Out Of" DataField="Outof"></asp:BoundField>--%>
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
                                                    OnClick="btnEdit_Click" TabIndex="18" Text="Edit" Visible="false"/>
                                                <asp:Button ID="btnEditCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnEditCancel_Click" Visible="false"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
                         
                        var CourseName = $('<%=txtCourseName.ClientID %>').id
                        var InstituteName = $('<%=txtInstituteName.ClientID %>').id
                        var YearOfPassing = $( '<%=txtYearofPassing.ClientID %>').id
                        var Score =$('<%=txtScore.ClientID %>').id

                        if(spanlist == "")
                        {
                            var controlList = CourseName +"," + InstituteName +","+YearOfPassing + "," + Score;
                        }
                        else
                        {
                            var controlList = spanlist + CourseName +"," + InstituteName +","+YearOfPassing + "," + Score;
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
                 
                        var AllControlEmpty;

                        if(CourseName == "" && InstituteName == ""  && YearOfPassing =="" && Score == "")
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
                    
                     //This function allows only to enter alphanumeric values with + - sign allowed. 
                    function AlphanumericWithPlusMinus(event, targetControl)
                     {
                        var Point = targetControl.value;
                        var regExObj = new RegExp();
                        //Sanju:Issue Id 50201:Added condition for firefox events.which event(backspace and delete were not working)
                        var code = (event.keyCode) ? event.keyCode : event.which;
                        //Sanju:Issue Id 50201:End
                        if ((code >= 65 && code <= 90) || (code >= 48 && code <= 57) || (code >= 97 && code <= 122) || code == 43 || code == 45 || code == 46 || code == 8)
                         {
                            return true;
                        }
                        else
                         {
                            return false;
                        }
                    }
                </script>

            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
