<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeePassportDetails.aspx.cs" Inherits="EmployeePassportDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
        <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
         <%--   Sanju:Issue Id 50201:end--%>
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
                                        <%--<asp:Label ID="lblError" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="9pt" class="mandatorymark">Please click on the <span style="font-weight:bold">Save All</span> button to save passport details.</asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblValidPassport" runat="server" Text="Do you hold valid passport"></asp:Label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:RadioButtonList ID="rbtnValidPassport" runat="server" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rbtnValidPassport_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                    <td style="width: 25%">
                                    </td>
                                </tr>
                            </table>
                            <div id="divPassportDetails" runat="server">
                                <table width="100%" class="detailsbg">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPassportDetails" runat="server" Text="Passport Details" CssClass="detailsheader"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="passportdetails" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblPassportNo" runat="server" Text="Passport Number"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtPassportNo" runat="server" ToolTip="Enter Passport Number" MaxLength="10" 
                                                    Width="150" CssClass="mandatoryField"></asp:TextBox>
                                                <span id="spanPassportNo" runat="server">
                                                    <img id="imgPassportNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblIssuePlace" runat="server" Text="Place Of Issue"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtIssuePlace" runat="server" ToolTip="Enter Place Of Isssue" MaxLength="15"
                                                    Width="150" CssClass="mandatoryField"></asp:TextBox>
                                                <span id="spanIssuePlace" runat="server">
                                                    <img id="imgIssuePlace" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblIssueDate" runat="server" Text="Issue Date"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <uc1:DatePicker ID="ucDatePickerIssueDate" runat="server" Width="150" />
                                                <span id="spanIssueDate" runat="server">
                                                    <img id="imgcIssueDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblExpiryDate" runat="server" Text="Expiry Date"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <uc1:DatePicker ID="ucDatePickerExpiryDate" runat="server" Width="150" />
                                                <span id="spanExpiryDate" runat="server">
                                                    <img id="imgcExpiryDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <%--CR - 28321- Passport Application Number New Field Added Sachin- Start--%>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblPassportAppNo" runat="server" Text="Passport Application No."></asp:Label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtPassportAppNo" runat="server" ToolTip="Enter Passport Application No."
                                                    MaxLength="15" Width="150"></asp:TextBox>
                                            </td>
                                            <%--CR - 28321- Passport Application Number New Field Added Sachin- END--%>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblvalidVisa" runat="server" Text="Valid Visa Of Any Country"></asp:Label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:RadioButtonList ID="rbtnValidVisa" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rbtnValidVisa_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <%--CR - 28321- Passport Application Number New Field Added Sachin- Start--%>
                                            <%--Commented following for consistrancy--%>
                                            <%--<td colspan="2">
                                                &nbsp;
                                            </td>--%>
                                            <%--CR - 28321- Passport Application Number New Field Added Sachin- End--%>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td style="width: 100%">
                                    </td>
                                </tr>
                            </table>
                            <div id="divVisaDetails" runat="server" visible="false">
                                <table width="100%" class="detailsbg">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblVisaDetails" runat="server" Text="Visa Details" CssClass="detailsheader"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="visaDetails" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblCountryName" runat="server" Text="Country Name"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtCountryName" runat="server" Width="150" CssClass="mandatoryField"></asp:TextBox>
                                                <span id="spanCountryName" runat="server">
                                                    <img id="imgCountryName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblVisaType" runat="server" Text="Visa Type"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtVisaType" runat="server" ToolTip="Enter Visa type" MaxLength="10"
                                                    Width="150" CssClass="mandatoryField"></asp:TextBox>
                                                <span id="spanVisaType" runat="server">
                                                    <img id="imgVisaType" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                        border: none;" />
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%" class="txtstyle">
                                                <asp:Label ID="lblVisaExpiryDate" runat="server" Text="Visa Expiry Date"></asp:Label>
                                                <label class="mandatorymark">
                                                    *</label>
                                            </td>
                                            <td style="width: 25%">
                                                <uc1:DatePicker ID="ucDatePickerVisaExpiryDate" runat="server" Width="150" />
                                            </td>
                                            <td colspan="2" align="right">
                                                <asp:Button ID="btnUpdateRow" runat="server" CssClass="button" OnClick="btnUpdateRow_Click"
                                                    TabIndex="14" Text="Update" Visible="false" />
                                                <asp:Button ID="btnCancelRow" runat="server" CssClass="button" OnClick="btnCancelRow_Click"
                                                    TabIndex="14" Text="Cancel" Visible="false" />
                                                <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAdd_Click"
                                                    TabIndex="14" Text="Add" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:GridView ID="gvVisaDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    OnRowDeleting="gvVisaDetails_RowDeleting" OnRowEditing="gvVisaDetails_RowEditing">
                                                    <HeaderStyle CssClass="addrowheader" />
                                                    <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                                    <RowStyle Height="20px" CssClass="textstyle" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Country Name" DataField="CountryName"></asp:BoundField>
                                                        <asp:BoundField HeaderText="Visa Type" DataField="VisaType"></asp:BoundField>
                                                        <asp:BoundField HeaderText="Visa Expiry Date" DataField="ExpiryDate" DataFormatString="{0:dd/MM/yyyy}">
                                                        </asp:BoundField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="VisaId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.VisaId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Mode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="imgBtnEdit" CommandName="Edit" ImageUrl="Images/Edit.gif"
                                                                    ToolTip="Edit Visa Detail" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="imgBtnDelete" CommandName="Delete" ImageUrl="Images/Delete.gif"
                                                                    ToolTip="Delete Visa Detail" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField ID="EMPId" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td style="width: 100%">
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr style="width: 100%">
                                    <td style="width: 40%">
                                    </td>
                                    <td colspan="3" align="right">
                                        <asp:Button ID="btnSave" runat="server" Text="Save All" CssClass="button" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnEdit" runat="server" CausesValidation="true" CssClass="button"
                                                OnClick="btnEdit_Click" TabIndex="18" Text="Edit" Visible="false" Width="119px" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <asp:HiddenField ID="HfIsDataModified" runat="server" />
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <script language="javascript" type="text/javascript">
                    setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmployeeProfile');

                    function $(v) { return document.getElementById(v); }
                    function Max_Length1() {
                        

                        if (document.getElementById('<%=txtPassportNo.ClientID %>').value.length < 10) {
                            alert("Please enter atleast 10 characters");
               
                            document.getElementById('<%=txtPassportNo.ClientID %>').focus();
                            return (false);
                        }
                                                    
                    }

                    function ButtonClickValidate()
                    {
                        var lblMandatory;
                        var spanlist = "";

                        var CountryName = $('<%=txtCountryName.ClientID %>').id
                        var VisaType = $('<%=txtVisaType.ClientID %>').id
                        var VisaExpiryDate = $( '<%=ucDatePickerVisaExpiryDate.ClientID %>').id
                         
                        if(spanlist == "")
                        {
                            var controlList = CountryName +"," + VisaType +","+VisaExpiryDate;
                        }
                        else
                        {
                            var controlList = spanlist + CountryName +"," + VisaType +","+VisaExpiryDate;
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
                        
                        //Get passport radio button list value.
                        var ValidPassport =$('<%=rbtnValidPassport.ClientID %>').getElementsByTagName("input")
                        
                        //Check for Yes selection
                        if(ValidPassport[0].checked)
                        {
                            var PassportNo = $('<%=txtPassportNo.ClientID %>').id
                            var IssuePlace = $('<%=txtIssuePlace.ClientID %>').id
                            var IssueDate = $( '<%=ucDatePickerIssueDate.ClientID %>').id
                            var ExpiryDate = $( '<%=ucDatePickerExpiryDate.ClientID %>').id
                            
                            //Gets visa radio button list value.
                            var Inputs =$('<%=rbtnValidVisa.ClientID %>').getElementsByTagName("input")
                            var AllControlEmpty;
                            
                            //Check for Yes selected
                            if (Inputs[0].checked)
                            {
                                 var CountryName = $('<%=txtCountryName.ClientID %>').value
                                 var VisaType = $('<%=txtVisaType.ClientID %>').value
                                 var VisaExpiryDate = $( '<%=ucDatePickerVisaExpiryDate.ClientID %>').value

                                if(CountryName == ""  && VisaType =="" && VisaExpiryDate == "")
                                {
                                   AllControlEmpty = "Yes";
                                }
                                
                                if(AllControlEmpty == "Yes")         
                                {
                                    if(spanlist == "")
                                    {
                                        var controlList = PassportNo +"," + IssuePlace +","+IssueDate+","+ExpiryDate;
                                    }
                                    else
                                    {
                                        var controlList = spanlist + PassportNo +"," + IssuePlace +","+IssueDate+","+ExpiryDate;
                                    }
                            
                                    if(ValidateControlOnClickEvent(controlList) == false)
                                    {
                                        lblError = $( '<%=lblError.ClientID %>')
                                        lblError.innerText = "Please fill all mandatory fields.";
                                        lblError.style.color = "Red";
                                    }
                                    
                                    return ValidateControlOnClickEvent(controlList);
                                }  
                                else
                                {   
                                    if($('<%=btnAddRow.ClientID %>')== null)
                                    {
                                        alert("To save the Visa details entered, kindly click on Update");
                                        return false;
                                    }
                                    else
                                    {
                                        alert("To save the Visa details entered, kindly click on Add row");
                                        return false;
                                    }   
                                } 
                            }
                            else
                            {
                                var controlList = PassportNo +"," + IssuePlace +","+IssueDate+","+ExpiryDate;
                                
                                if(ValidateControlOnClickEvent(controlList) == false)
                                    {
                                        lblError = $( '<%=lblError.ClientID %>')
                                        lblError.innerText = "Please fill all mandatory fields.";
                                        lblError.style.color = "Red";
                                    }
                                    
                                return ValidateControlOnClickEvent(controlList);
                            }
                        }
                    }
                    
                </script>

            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
