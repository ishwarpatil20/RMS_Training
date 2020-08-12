<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeSkillsDetails.aspx.cs" Inherits="EmployeeSkillsDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript">
        //function added to check data has been modified or not on Page.        
        function myfn() {
            var IsDataModified = $('<%=HfIsDataModified.ClientID %>').value;

            if (IsDataModified == "Yes")
                javascript: __doPostBack('ctl00$cphMainContent$lnkSaveBtn', '')
        }

        //function To Validate Save PrimaySkills Button
        function SavePrimaySkillsButtonClickValidate() {
            if ($('<%=lbxPrimaryskills.ClientID %>') != null) {
                var Primaryskills = $('<%=lbxPrimaryskills.ClientID %>').value;

                if (Primaryskills == "" || Primaryskills == "SELECT") {
                    //Siddhesh Arekar : Issue : Validation message change  : 28/09/2015 : Starts
                    alert("Please select at least one Primary Skill.");
                    //Siddhesh Arekar : Issue : Validation message change  : 28/09/2015 : End
                    return false;
                }
            }
        }

        function ValidateSpecialCharacters() {
            if (event.keyCode == 46 || event.keyCode == 32 || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) {
                event.returnValue = true;

            }
            else {
                event.returnValue = false;
                alert("Please input alphanumeric value only");
            }

        }
        function $(v) { return document.getElementById(v); }
    </script>

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <%-- Sanju:Issue Id 50201:End--%>
                <asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label>
            </td>
            <%-- Sanju:Issue Id 50201: Added background color property so that all browsers display header--%>
            <td align="right" style="height: 25px; width: 20%; background-color: #7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
                <asp:Label ID="lblempName" runat="server" CssClass="header"> </asp:Label>
            </td>
            <%--  Sanju:Issue Id 50201:End--%>
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
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields
                                            marked with <span class="mandatorymark" id="mark" runat="server">*</span> are mandatory.</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="9pt" class="mandatorymark">Please click on the <span style="font-weight:bold">Save All</span> button to save skills details.</asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlPrimarysSkills" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblPrimaryskills" runat="server" Text="Primary Skills" class="txtstyle"></asp:Label>
                                            <label class="mandatorymark" id="mandPriSkills" runat="server">
                                                *</label>
                                        </td>
                                        <td>
                                            <span id="spanzPrimaryskills" runat="server">
                                                <asp:ListBox ID="lbxPrimaryskills" runat="server" Width="200px" SelectionMode="Multiple"
                                                    OnSelectedIndexChanged="lbxPrimarySkills_ClickEvtHandler" CssClass="mandatoryField"
                                                    AutoPostBack="true"></asp:ListBox>
                                            </span>
                                            <asp:TextBox ID="txtPrimarySkills" runat="server" Width="142px" Visible="false" ReadOnly="true"
                                                TextMode="MultiLine" />
                                            <asp:Label ID="lblMsg" runat="server" Font-Names="Verdana" Font-Size="9pt">Press <span style="font-style:inherit">Ctrl Key</span> to select multiple skills.</asp:Label>
                                        </td>
                                        <td align="right">
                                            <br />
                                            <br />
                                            <br />
                                            <asp:Button ID="btnSavePrimarySkills" runat="server" Text="Save Primary Skills" CssClass="button"
                                                Width="135px" OnClick="btnSavePrimarySkills_ClickEvtHandler" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblText" runat="server" Text="Primary Skills selected :" class="txtstyle"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSkills" runat="server" class="txtstyle"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table width="100%" class="detailsbg">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMustSkills" runat="server" Text="Skills" CssClass="detailsheader"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Skillsdetails" runat="server" HorizontalAlign="Left">
                                <div id="divSkillsdetails" runat="server" style="text-align: center">
                                    <asp:GridView ID="gvSkillsdetails" runat="server" AutoGenerateColumns="False"
                                        OnRowDataBound="gvSkillsdetails_RowDataBound" OnRowCommand="gvSkillsdetails_RowCommand"
                                        ShowFooter="True" OnRowDeleting="gvSkillsdetails_RowDeleting">
                                        <HeaderStyle CssClass="headerStyle" />
                                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                        <RowStyle Height="20px" CssClass="textstyle" HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Skill" FooterStyle-HorizontalAlign="Center"  ItemStyle-Width="500px" HeaderStyle-Width="500px">
                                                <ItemTemplate>
                                                    <%--Siddhesh Arekar : Issue : UI changes  : 28/09/2015 : Starts--%>
                                                    <table>
                                                    <tr><td>
                                                        <cc1:ComboBox ID="ddlSkill" runat="server" Width="200px" DropDownStyle="DropDownList"
                                                            AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ddlSkill_ClickEvtHandler">
                                                        </cc1:ComboBox>
                                                    </td><td>
                                                        <asp:TextBox ID="txtSkill" runat="server" Width="200px" MaxLength="50"
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.SkillName") %>' style="display:none;" ></asp:TextBox>
                                                    </td></tr>
                                                    </table>
                                                </ItemTemplate>
                                                <%--  Sanju:Issue Id 50201:Removed nbsp; since the dropdown was not properly aligned--%>
                                                <FooterTemplate>
                                                    <%--   Sanju:Issue Id 50201:End--%>
                                                    <table>
                                                    <tr><td>
                                                        <cc1:ComboBox ID="ddlAddSkill" runat="server" Width="200px" DropDownStyle="DropDownList"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlAddSkill_ClickEvtHandler" AutoCompleteMode="SuggestAppend">
                                                        </cc1:ComboBox>
                                                    </td><td>
                                                        <asp:TextBox ID="txtAddSkill" runat="server" Width="200px" MaxLength="50"
                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.SkillName") %>' style="display:none;" ></asp:TextBox>
                                                    </td></tr>
                                                    </table>          
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Version"  ItemStyle-Width="100px" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtVersion" runat="server" MaxLength="30" Width="95px" Text='<%# DataBinder.Eval(Container, "DataItem.SkillVersion") %>'
                                                        OnKeyPress="ValidateSpecialCharacters()"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAddVersion" runat="server" MaxLength="30" Width="95px" Text='<%# DataBinder.Eval(Container, "DataItem.SkillVersion") %>'></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Experience" ItemStyle-Width="150px" HeaderStyle-Width="150px" FooterStyle-Width="150px" >
                                                <ItemTemplate>
                                                    <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlYear" runat="server" Width="60px">
                                                            </asp:DropDownList>
                                                        </td><td>
                                                            <asp:DropDownList ID="ddlMonth" runat="server" Width="70px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table>
                                                    <tr>
                                                        <td>                                                
                                                            <asp:DropDownList ID="ddlAddYear" runat="server" Width="60px">
                                                            </asp:DropDownList>
                                                        </td><td>
                                                            <asp:DropDownList ID="ddlAddMonth" runat="server" Width="70px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                    <%--Siddhesh Arekar : Issue : UI changes  : 28/09/2015 : End--%>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proficiency level">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlProficiency" runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlAddProficiency" runat="server">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Used" ControlStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlLastUsed" runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlAddLastUsed" runat="server">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderStyle-Width="100px" FooterStyle-Width="100px" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                                        CommandName="Delete" ToolTip="Delete Skill Details" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="imgBtnAdd" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="Add"
                                                        ToolTip="Add Skill Details" Text="Add Skill" CssClass="button" Width="90px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMonth" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblYear" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Year") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProficiency" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Proficiency") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLastUsed" runat="server" Width="100px" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.LastUsed") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSkill" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Skill") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="SkillId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.SkillsId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <%--<EmptyDataTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 17%" class="txtstyle">
                                                        Skill<label class="mandatorymark">
                                                            *</label>
                                                    </td>
                                                    <td style="width: 33%">
                                                        <cc1:ComboBox ID="ddlEmptySkill" runat="server" Width="200px" DropDownStyle="DropDownList"
                                                            AutoCompleteMode="SuggestAppend" CssClass="mandatoryField" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlEmptySkill_ClickEvtHandler">
                                                        </cc1:ComboBox>
                                                        <asp:TextBox ID="txtEmptySkill" runat="server" CssClass="mandatoryField" Width="200px"
                                                            MaxLength="50" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.SkillName") %>'></asp:TextBox>
                                                    </td>
                                                    <td style="width: 17%" class="txtstyle">
                                                        Version
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:TextBox ID="txtEmptyVersion" runat="server" CssClass="mandatoryField" Width="155px"
                                                            MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.SkillVersion") %>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 17%" class="txtstyle">
                                                        Experience<label class="mandatorymark">
                                                            *</label>
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:DropDownList ID="ddlEmptyYear" runat="server" Width="78px" CssClass="mandatoryField">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlEmptyMonth" runat="server" Width="77px" CssClass="mandatoryField">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 17%" class="txtstyle">
                                                        Proficiency Level<label class="mandatorymark">
                                                            *</label>
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:DropDownList ID="ddlEmptyProficiency" runat="server" Width="160px" CssClass="mandatoryField">
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 17%" class="txtstyle">
                                                        Last Used<label class="mandatorymark">
                                                            *</label>
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:DropDownList ID="ddlEmptyLastUsed" runat="server" Width="160px" CssClass="mandatoryField">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 17%">
                                                    </td>
                                                    <td style="width: 33%">
                                                        <asp:Button ID="imgBtnEmptyAdd" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="EmptyAdd"
                                                            ToolTip="Add Contact Numbers" Text="Add" CssClass="button" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>--%>
                                    </asp:GridView>
                                                                   
                                    <asp:HiddenField ID="EMPId" runat="server" />
                                    <asp:HiddenField ID="HfIsDataModified" runat="server" />
                                    <asp:HiddenField ID="HfOtherskill" runat="server" />
                                    <asp:HiddenField ID="HfOtherEmpDomain" runat="server" />
                                </div>
                            </asp:Panel>
                            <%--//Start--%>                            
                            <table width="100%" class="detailsbg" style="margin-top:20px">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDomainDetails" runat="server" Text="Domain Details" CssClass="detailsheader"></asp:Label>
                                    </td>
                                </tr>
                            </table>          
                            <%--Siddhesh Arekar Domain Details 09032015 Start--%>
                            <asp:Panel ID="pnlDomainDetails" runat="server">
                                    <asp:GridView ID="gvEmployeeDomain" runat="server" AutoGenerateColumns="False"
                                        OnRowDataBound="gvEmployeeDomain_RowDataBound" OnRowCommand="gvEmployeeDomain_RowCommand"
                                        ShowFooter="True" OnRowDeleting="gvEmployeeDomain_RowDeleting">
                                        <HeaderStyle CssClass="headerStyle" />
                                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                        <RowStyle Height="20px" CssClass="textstyle" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Employee Domain" FooterStyle-HorizontalAlign="Center" ItemStyle-Width="500px" HeaderStyle-Width="500px">
                                                <ItemTemplate>
                                                    <%--Siddhesh Arekar : Issue : UI changes  : 28/09/2015 : Starts--%>
                                                    <table>
                                                    <tr><td>
                                                    <cc1:ComboBox ID="ddlEmployeeDomain" runat="server" Width="200px" DropDownStyle="DropDownList"
                                                        AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeDomain_ClickEvtHandler">
                                                    </cc1:ComboBox>
                                                    </td><td>
                                                    <asp:TextBox ID="txtEmployeeDomain" runat="server" Width="200px" MaxLength="50"
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.EmployeeDomainName") %>' style="display:none;" ></asp:TextBox>
                                                    </td></tr>
                                                    </table>
                                                </ItemTemplate>                                                
                                                <FooterTemplate>
                                                    <table>
                                                    <tr><td>                                                     
                                                    <cc1:ComboBox ID="ddlAddEmployeeDomain" runat="server" Width="200px" DropDownStyle="DropDownList"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlAddEmployeeDomain_ClickEvtHandler" AutoCompleteMode="SuggestAppend" >
                                                    </cc1:ComboBox>
                                                    </td><td>
                                                    <asp:TextBox ID="txtAddEmployeeDomain" runat="server" Width="200px" MaxLength="50"
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.EmployeeDomainName") %>' style="display:none;"></asp:TextBox>
                                                    </td></tr>
                                                    </table>
                                                    <%--Siddhesh Arekar : Issue : UI changes  : 28/09/2015 : End--%>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeDomain" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.EmployeeDomain") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="100px" HeaderStyle-Width="100px" FooterStyle-Width="100px" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnDeleteDomain" runat="server" ImageUrl="~/Images/Delete.gif"
                                                        CommandName="Delete" ToolTip="Delete Employee Domain" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="imgBtnAddDomain" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="Add"
                                                        ToolTip="Add Employee Domain" Text="Add Domain" CssClass="button" Width="90px" />
                                                </FooterTemplate>
                                            </asp:TemplateField>                                            
                                        </Columns>
                                    </asp:GridView>
                            </asp:Panel>
                            <%--Siddhesh Arekar Domain Details 09032015 End--%>
                            <%--//End--%>
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
                                                OnClick="btnSave_Click" TabIndex="18" Text="Save All" Width="119px" />
                                            <asp:Button ID="btnEdit" runat="server" CausesValidation="true" CssClass="button"
                                                OnClick="btnEdit_Click" TabIndex="18" Text="Edit" Width="119px" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick="btnCancel_Click"
                                                Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
