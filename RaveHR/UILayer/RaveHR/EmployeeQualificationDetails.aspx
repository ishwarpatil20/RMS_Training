<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeQualificationDetails.aspx.cs" Inherits="EmployeeQualificationDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript">
        //function added to check data has been modified or not on Page.        
        function myfn()
        {
            var IsDataModified = $('<%=HfIsDataModified.ClientID %>').value;
               
//             if(IsDataModified== "Yes") 
//            javascript:__doPostBack('ctl00$cphMainContent$lnkSaveBtn','')
        }
//function added to check whether year of passing is greater than 1950 or not
        function ValidateYearOfPassing() {
            var yop = document.getElementById('<%=txtYearOfPassing.ClientID %>').value;
            if (yop != '' && yop < 1950) {
                alert("year of passing can't be less than 1950");
                document.getElementById('<%=txtYearOfPassing.ClientID %>').value = "";
                // 26866-Ambar-Start
                // Commented following line
                // document.getElementById('<%=txtYearOfPassing.ClientID %>').focus();
                // 26866-Ambar-End
            }
        }
        
    </script>

    <asp:LinkButton runat="server" ID="lnkSaveBtn" OnClick="lnkSaveBtn_Click" Visible="false" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
           <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label>
            </td>
            <%--  Sanju:Issue Id 50201:End--%>
              <%-- Sanju:Issue Id 50201: Added background color property so that all browsers display header--%>
            <td align="right" style="height: 25px; width: 20%;background-color:#7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
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
                                        <asp:Label ID="lblQualificationHeader" runat="server" Text="Qualification" CssClass="detailsheader"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Qualificationdetails" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblQualification" runat="server" Text="Qualification"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <span id="spanzQualification" runat="server">
                                                <asp:DropDownList ID="ddlQualification" runat="server" CssClass="mandatoryField"
                                                    Width="155px" ToolTip="Select Qualification" AutoPostBack="true" OnSelectedIndexChanged="ddlQualification_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </span><span id="spanQualification" runat="server">
                                                <img id="imgQualification" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblStream" runat="server" Text="Stream"></asp:Label>
                                            <label class="mandatorymark" id="mandatorymarkStream" runat="server">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtStream" runat="server" CssClass="mandatoryField" MaxLength="30"
                                                ToolTip="Enter Stream" Text=""></asp:TextBox>
                                            <span id="spanStream" runat="server">
                                                <img id="imgStream" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblUniversityName" runat="server" Text="University/Board Name"></asp:Label>
                                            <%--27947-Ambar-Start : Added following line to show this as manadatory field--%>
                                            <label class="mandatorymark" id="Label1" runat="server">*</label>
                                            <%--27947-Ambar-End--%>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtUniversityName" runat="server" CssClass="mandatoryField" MaxLength="30"
                                                ToolTip="Enter University Name"></asp:TextBox>
                                            <span id="spanUniversityName" runat="server">
                                                <img id="imgUniversityName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblInstituteName" runat="server" Text="Institute Name"></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtInstituteName" runat="server" CssClass="mandatoryField" MaxLength="30"
                                                ToolTip="Enter Institute Name"></asp:TextBox>
                                            <span id="spanInstituteName" runat="server">
                                                <img id="imgInstituteName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblYearOfPassing" runat="server" Text="Year of Passing"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtYearOfPassing" runat="server" CssClass="mandatoryField" MaxLength="4"
                                                 ToolTip="Enter Year Of Passing"></asp:TextBox>
                                            <span id="spanYearOfPassing" runat="server">
                                                <img id="imgYearOfPassing" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblPercentage" runat="server" Text="Percentage"></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtPercentage" runat="server" CssClass="mandatoryField" MaxLength="5"
                                                ToolTip="Enter Percentage" OnTextChanged="txtPercentage_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                            <span id="spanPercentage" runat="server">
                                                <img id="imgPercentage" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblGPA" runat="server" Text="GPA"></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtGPA" runat="server" MaxLength="6" CssClass="mandatoryField" ToolTip="Enter GPA"
                                                OnTextChanged="txtGPA_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                            <span id="spanGPA" runat="server">
                                                <img id="imgGPA" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblOutOf" runat="server" Text="Out Of"></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtOutOf" runat="server" MaxLength="6" CssClass="mandatoryField"
                                                ToolTip="Enter OutOf" Enabled="false"></asp:TextBox>
                                            <span id="spanOutOf" runat="server">
                                                <img id="imgOutOf" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%">
                                        </td>
                                        <td style="width: 20%">
                                        </td>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="btnUpdateRow" runat="server" CssClass="button" OnClick="btnUpdateRow_Click"
                                                TabIndex="14" Text="Update" Visible="false" />
                                            <asp:Button ID="btnCancelRow" runat="server" CssClass="button" OnClick="btnCancelRow_Click"
                                                TabIndex="14" Text="Cancel" Visible="false" />
                                            <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAdd_Click"
                                                TabIndex="14" Text="Save" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"
                                                />
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
                                            <asp:BoundField DataField="Stream" HeaderText="Stream" />
                                            <asp:BoundField DataField="UniversityName" HeaderText="University Name" />
                                            <asp:BoundField DataField="InstituteName" HeaderText="Institute Name" />
                                            <asp:BoundField DataField="PassingYear" HeaderText="Year of Passing" />
                                            <asp:BoundField DataField="Percentage" HeaderText="Percentage" />
                                            <asp:BoundField DataField="GPA" HeaderText="GPA" />
                                            <asp:BoundField DataField="OutOf" HeaderText="OutOf" />
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
                                            <asp:Button ID="btnSave" runat="server" CausesValidation="true" CssClass="button"
                                                OnClick="btnSave_Click" TabIndex="18" Text="Submit" Visible="false" Width="119px" />
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

                <script type="text/javascript">
                
                setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmployeeProfile');
                
                    function $(v){return document.getElementById(v);}
                         
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
                        
//                        var UniversityName = $('<%=txtUniversityName.ClientID %>').id
//                        var InstituteName = $('<%=txtInstituteName.ClientID %>').id
                        var YearOfPassing = $( '<%=txtYearOfPassing.ClientID %>').id
                        var Stream = $( '<%=txtStream.ClientID %>').id
                        
                        if(spanlist == "")
                        {
                            var controlList = YearOfPassing +"," + Stream;
                        }
                        else
                        {
                            var controlList = spanlist + YearOfPassing +"," + Stream;
                        }
                        
//                        if($('<%=ddlQualification.ClientID %>').options[$('<%=ddlQualification.ClientID %>').selectedIndex].text == "Diploma" ||
//                            $('<%=ddlQualification.ClientID %>').options[$('<%=ddlQualification.ClientID %>').selectedIndex].text == "ICWA" ||
//                            $('<%=ddlQualification.ClientID %>').options[$('<%=ddlQualification.ClientID %>').selectedIndex].text == "CA" ||
//                            $('<%=ddlQualification.ClientID %>').options[$('<%=ddlQualification.ClientID %>').selectedIndex].text == "CS" ||
//                            $('<%=ddlQualification.ClientID %>').options[$('<%=ddlQualification.ClientID %>').selectedIndex].text == "PG Diploma" )
//                            {
//                                controlList=controlList.replace("," + UniversityName,"");
//                            } 
                           
                         if( $('<%=ddlQualification.ClientID %>').options[$('<%=ddlQualification.ClientID %>').selectedIndex].text == "CA" ||
                            $('<%=ddlQualification.ClientID %>').options[$('<%=ddlQualification.ClientID %>').selectedIndex].text == "CS" )
                            {
                                controlList=controlList.replace("," + Stream,"");
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
                        var GPA =$('<%=txtGPA.ClientID %>').value
                        var OutOf = $('<%=txtOutOf.ClientID %>').value
                        
                        var AllControlEmpty;
                             
                        if(Qualification == "SELECT" && UniversityName == ""  && InstituteName =="" && YearOfPassing == "" && GPA == "" && OutOf == "" && Percentage == "")
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

            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
