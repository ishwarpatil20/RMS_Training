<%@ Page Language="C#" MasterPageFile="~/MasterPage/Employee.master" AutoEventWireup="true"
    CodeFile="EmpPassportDetails.aspx.cs" Inherits="EmpPassportDetails" Title="Untitled Page" %>

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
                                <%--<asp:Label ID="lblError" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>--%>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td style="width: 25%">
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
                                <td style="width: 25%">
                                    <asp:Label ID="lblPassportNo" runat="server" Text="Passport Number"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtPassportNo" runat="server" ToolTip="Enter Passport Number" MaxLength="10"></asp:TextBox>
                                    <span id="spanPassportNo" runat="server">
                                        <img id="imgPassportNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblIssuePlace" runat="server" Text="Place Of Issue"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtIssuePlace" runat="server" ToolTip="Enter Place Of Isssue" MaxLength="15"></asp:TextBox>
                                    <span id="spanIssuePlace" runat="server">
                                        <img id="imgIssuePlace" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblIssueDate" runat="server" Text="Issue Date"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtIssueDate" runat="server" ToolTip="Enter Issue Date"></asp:TextBox>
                                    <asp:ImageButton ID="imgIssueDate" runat="server" CausesValidation="false" ImageAlign="AbsMiddle"
                                        ImageUrl="Images/Calendar_scheduleHS.png" TabIndex="17" Width="16px" />
                                    <cc1:CalendarExtender ID="CalendarExtenderIssueDate" runat="server" PopupButtonID="imgIssueDate"
                                        TargetControlID="txtIssueDate" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    <span id="spanIssueDate" runat="server">
                                        <img id="imgcIssueDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblExpiryDate" runat="server" Text="Expiry Date"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtExpiryDate" runat="server" ToolTip="Enter Expiry Date"></asp:TextBox>
                                    <asp:ImageButton ID="imgExpiryDate" runat="server" CausesValidation="false" ImageAlign="AbsMiddle"
                                        ImageUrl="Images/Calendar_scheduleHS.png" TabIndex="17" Width="16px" />
                                    <cc1:CalendarExtender ID="CalendarExtenderExpiryDate" runat="server" PopupButtonID="imgExpiryDate"
                                        TargetControlID="txtExpiryDate" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    <span id="spanExpiryDate" runat="server">
                                        <img id="imgcExpiryDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblvalidVisa" runat="server" Text="Valid Visa Of Any Country"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                    <asp:RadioButtonList ID="rbtnValidVisa" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rbtnValidVisa_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td colspan="2">
                                &nbsp;
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
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
                                <td style="width: 25%">
                                    <asp:Label ID="lblCountryName" runat="server" Text="Country Name"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtCountryName" runat="server"></asp:TextBox>
                                    <span id="spanCountryName" runat="server">
                                        <img id="imgCountryName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblVisaType" runat="server" Text="Visa Type"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtVisaType" runat="server" ToolTip="Enter Visa type" MaxLength="10"></asp:TextBox>
                                    <span id="spanVisaType" runat="server">
                                        <img id="imgVisaType" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblVisaExpiryDate" runat="server" Text="Visa Expiry Date"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtVisaExpiryDate" runat="server" ToolTip="Enter Visa Expiry Date"></asp:TextBox>
                                    <asp:ImageButton ID="imgVisaExpiryDate" runat="server" CausesValidation="false" ImageAlign="AbsMiddle"
                                        ImageUrl="Images/Calendar_scheduleHS.png" TabIndex="17" Width="16px" />
                                    <cc1:CalendarExtender ID="CalendarExtenderVisaExpiryDate" runat="server" PopupButtonID="imgVisaExpiryDate"
                                        TargetControlID="txtVisaExpiryDate" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    <span id="spanVisaExpiryDate" runat="server">
                                        <img id="imgcVisaExpiryDate" runat="server" src="Images/cross.png" alt="" style="display: none;
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
                                                    <asp:ImageButton runat="server" ID="imgBtnEdit" CommandName="Edit"
                                                        ImageUrl="Images/Edit.gif" ToolTip="Edit Visa Detail" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="imgBtnDelete" CommandName="Delete"
                                                        ImageUrl="Images/Delete.gif" ToolTip="Delete Visa Detail" />
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
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click"
                                Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click"
                                Visible="false" />
                            <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="button" OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script language="javascript" type="text/javascript">
    function $(v){return document.getElementById(v);}
    
    //Highlighting the tabs by passing individaul tab id
    setbackcolorToTab('divMenu_0');
    
    function ButtonClickValidate()
    {
        var lblMandatory;
        var spanlist = "";

        var CountryName = $('<%=txtCountryName.ClientID %>').id
        var VisaType = $('<%=txtVisaType.ClientID %>').id
        var VisaExpiryDate = $( '<%=txtVisaExpiryDate.ClientID %>').id
         
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
            var IssueDate = $( '<%=txtIssueDate.ClientID %>').id
            var ExpiryDate = $( '<%=txtExpiryDate.ClientID %>').id
            
            //Gets visa radio button list value.
            var Inputs =$('<%=rbtnValidVisa.ClientID %>').getElementsByTagName("input")
            var AllControlEmpty;
            
            //Check for Yes selected
            if (Inputs[0].checked)
            {
                 var CountryName = $('ctl00_cphEmployeeContent_txtCountryName').value
                 var VisaType = $('ctl00_cphEmployeeContent_txtVisaType').value
                 var VisaExpiryDate = $( 'ctl00_cphEmployeeContent_txtVisaExpiryDate').value

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
                        alert("To save the Passport details entered, kindly click on Update");
                        return false;
                    }
                    else
                    {
                        alert("To save the Passport details entered, kindly click on Add row");
                        return false;
                    }   
                } 
            }
            else
            {
                return true;
            }
        }
    }
    </script>

</asp:Content>
