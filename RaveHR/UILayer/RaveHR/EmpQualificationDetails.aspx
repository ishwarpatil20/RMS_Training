<%@ Page Language="C#" MasterPageFile="~/MasterPage/Employee.master" AutoEventWireup="true"
    CodeFile="EmpQualificationDetails.aspx.cs" Inherits="EmpQualificationDetails"
    Title="Qualification" EnableEventValidation="false" %>

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
                    <tr class="detailsbg">
                        <td>
                            <asp:Label ID="lblQualificationHeader" runat="server" Text="Qualification" CssClass="detailsheader"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Qualificationdetails" runat="server">
                <table width="100%">
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblQualification" runat="server" Text="Qualification"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <span id="spanzQualification" runat="server">
                                <asp:DropDownList ID="ddlQualification" runat="server" Width="155px" ToolTip="Select Qualification">
                                </asp:DropDownList>
                            </span><span id="spanQualification" runat="server">
                                <img id="imgQualification" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblUniversityName" runat="server" Text="University/Board Name"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtUniversityName" runat="server" MaxLength="30" ToolTip="Enter University Name"></asp:TextBox>
                            <span id="spanUniversityName" runat="server">
                                <img id="imgUniversityName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblInstituteName" runat="server" Text="Institute Name"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtInstituteName" runat="server" MaxLength="30" ToolTip="Enter Institute Name"></asp:TextBox>
                            <span id="spanInstituteName" runat="server">
                                <img id="imgInstituteName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblYearOfPassing" runat="server" Text="Year of Passing"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtYearOfPassing" runat="server" MaxLength="4" ToolTip="Enter Year Of Passing"></asp:TextBox>
                            <span id="spanYearOfPassing" runat="server">
                                <img id="imgYearOfPassing" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblGPA" runat="server" Text="GPA"></asp:Label>
                            
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtGPA" runat="server" MaxLength="6" ToolTip="Enter GPA"></asp:TextBox>
                            <span id="spanGPA" runat="server">
                                <img id="imgGPA" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblOutOf" runat="server" Text="Out Of"></asp:Label>
                            
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtOutOf" runat="server" MaxLength="6" ToolTip="Enter OutOf"></asp:TextBox>
                            <span id="spanOutOf" runat="server">
                                <img id="imgOutOf" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblPercentage" runat="server" Text="Percentage"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txtPercentage" runat="server" MaxLength="30" ToolTip="Enter Percentage"></asp:TextBox>
                            <span id="spanPercentage" runat="server">
                                <img id="imgPercentage" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnUpdateRow" runat="server" CssClass="button" OnClick="btnUpdateRow_Click"
                                TabIndex="14" Text="Update" Visible="false" />
                            <asp:Button ID="btnCancelRow" runat="server" CssClass="button" OnClick="btnCancelRow_Click"
                                TabIndex="14" Text="Cancel" Visible="false" />
                            <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAdd_Click"
                                TabIndex="14" Text="Add Row" />
                        </td>
                    </tr>
                </table>
                <div id="divGVQualification" runat="server">
                    <asp:GridView ID="gvQualification" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowDeleting="gvQualification_RowDeleting" OnRowEditing="gvQualification_RowEditing">
                        <HeaderStyle CssClass="headerStyle" />
                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                        <RowStyle Height="20px" CssClass="textstyle" />
                        <Columns>
                            <asp:BoundField DataField="QualificationName" HeaderText="Qualification" />
                            <asp:BoundField DataField="UniversityName" HeaderText="University Name" />
                            <asp:BoundField DataField="InstituteName" HeaderText="Institute Name" />
                            <asp:BoundField DataField="PassingYear" HeaderText="Passing Year" />
                            <asp:BoundField DataField="GPA" HeaderText="GPA" />
                            <asp:BoundField DataField="OutOf" HeaderText="OutOf" />
                            <asp:BoundField DataField="Percentage" HeaderText="Percentage" />
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="QualificationId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.QualificationId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Qualification" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Qualification") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Mode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/Edit.gif" CommandName="Edit"
                                        ToolTip="Edit Qualification Detail" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                        CommandName="Delete" ToolTip="Delete Qualification Detail" />
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
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" Visible="false"/>
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
    setbackcolorToTab('divMenu_1');
    
    
    function ButtonClickValidate()
    {
        
        var lblMandatory;
        var spanlist = "";
        var Qualification = $('<%=ddlQualification.ClientID %>').value;
         
        if(Qualification == "" || Qualification == "SELECT")
        {
            var sQualification = $('<%=spanzQualification.ClientID %>').id;
            spanlist = sQualification +",";
        }
         
        var UniversityName = $('<%=txtUniversityName.ClientID %>').id
        var InstituteName = $('<%=txtInstituteName.ClientID %>').id
        var YearOfPassing = $( '<%=txtYearOfPassing.ClientID %>').id
//        var GPA =$('<%=txtGPA.ClientID %>').id
//        var OutOf = $('<%=txtOutOf.ClientID %>').id
        var Percentage = $('<%=txtPercentage.ClientID %>').id
         
        if(spanlist == "")
        {
//            var controlList = UniversityName +"," + InstituteName +","+YearOfPassing + "," + GPA +","+ OutOf+","+ Percentage;
              var controlList = UniversityName +"," + InstituteName +","+YearOfPassing + "," + Percentage;
        }
        else
        {
            var controlList = spanlist + UniversityName +"," + InstituteName +","+YearOfPassing + "," + Percentage;
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

        var Qualification = $('<%=ddlQualification.ClientID %>').value;
        var UniversityName = $('<%=txtUniversityName.ClientID %>').value;
        var InstituteName = $('<%=txtInstituteName.ClientID %>').value;
        var YearOfPassing = $( '<%=txtYearOfPassing.ClientID %>').value;
        var Percentage = $('<%=txtPercentage.ClientID %>').value;
 
        var AllControlEmpty;
             
        //if(Qualification == "0" && UniversityName == ""  && InstituteName =="" && YearOfPassing == "" && GPA == "" && OutOf == "" && Percentage == "")
        if(Qualification == "SELECT" && UniversityName == ""  && InstituteName =="" && YearOfPassing == "" && Percentage == "")
        {
           AllControlEmpty = "Yes";
        }
        
        if(AllControlEmpty != "Yes")         
        {
            if($('<%=btnAddRow.ClientID %>')== null)
            {
                alert("To save the Qualification details entered, kindly click on Update");
                return false;
            }
            else
            {
                alert("To save the Qualification details entered, kindly click on Add row");
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
