<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmpContactDetails.aspx.cs" Inherits="EmpContactDetails" Title="Contact Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script type="text/javascript">
    
    function $(v){return document.getElementById(v);}
        
    function ButtonClickValidate()
    {
        
        var lblMandatory;
        var spanlist = "";
        
        var id = "<%=rblSamePerAddress.ClientID%>"; 
        
        //if current & perment address is different
        if(document.getElementById(id+"_"+'1'.toString()).checked==true)  
        {
            //GET current address details
            var HouseNo = $('<%=txtCHouseNo.ClientID %>').id
            var Apartment = $('<%=txtCApartment.ClientID %>').id
            var StreetName = $('<%=txtCStreetName.ClientID %>').id
            var Landmark = $('<%=txtCLandmark.ClientID %>').id
            var City = $('<%=txtCCity.ClientID %>').id
            var State = $('<%=txtCState.ClientID %>').id
            var Country = $('<%=txtCCountry.ClientID %>').id
            var PinCode = $('<%=txtCPinCode.ClientID %>').id 
            
            //GET perment address details
            var PHouseNo = $('<%=txtPHouseNo.ClientID %>').id
            var PApartment = $('<%=txtPApartment.ClientID %>').id
            var PStreetName = $('<%=txtPStreetName.ClientID %>').id
            var PLandmark = $('<%=txtPLandmark.ClientID %>').id
            var PCity = $('<%=txtPCity.ClientID %>').id
            var PState = $('<%=txtPState.ClientID %>').id
            var PCountry = $('<%=txtPCountry.ClientID %>').id
            var PPinCode = $('<%=txtPPincode.ClientID %>').id 
            
            if(spanlist == "")
            {
               var controlList =HouseNo +"," + Apartment +"," + StreetName +"," + Landmark +"," + City +"," + State +"," + Country +"," + PinCode +"," +PHouseNo +"," + PApartment +"," + PStreetName +"," + PLandmark +"," + PCity +"," + PState +"," + PCountry +"," + PPinCode;
            }
            else
            {
               var controlList = spanlist + HouseNo +"," + Apartment +"," + StreetName +"," + Landmark +"," + City +"," + State +"," + Country +"," + PinCode +"," +PHouseNo +"," + PApartment +"," + PStreetName +"," + PLandmark +"," + PCity +"," + PState +"," + PCountry +"," + PPinCode;
            }             
        }
        else
        {
            //GET current address details
            var HouseNo = $('<%=txtCHouseNo.ClientID %>').id
            var Apartment = $('<%=txtCApartment.ClientID %>').id
            var StreetName = $('<%=txtCStreetName.ClientID %>').id
            var Landmark = $('<%=txtCLandmark.ClientID %>').id
            var City = $('<%=txtCCity.ClientID %>').id
            var State = $('<%=txtCState.ClientID %>').id
            var Country = $('<%=txtCCountry.ClientID %>').id
            var PinCode = $('<%=txtCPinCode.ClientID %>').id 
            
            if(spanlist == "")
            {
               var controlList =HouseNo +"," + Apartment +"," + StreetName +"," + Landmark +"," + City +"," + State +"," + Country +"," + PinCode;
            }
            else
            {
               var controlList = spanlist + HouseNo +"," + Apartment +"," + StreetName +"," + Landmark +"," + City +"," + State +"," + Country +"," + PinCode;
            }  
            
        } 
                 
       if(ValidateControlOnClickEvent(controlList) == false)
        {
            lblMessage = $( '<%=lblMessage.ClientID %>')
            lblMessage.innerText = "Please fill all mandatory fields.";
            lblMessage.style.color = "Red"; 
        }
        
        return ValidateControlOnClickEvent(controlList);
    }
 
    </script>
    <table width="100%">
        <tr>
            <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <asp:Label ID="lblContactdetails" runat="server" Text="Contact Details" CssClass="header"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 1pt">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblConfirmMsg" runat="server" CssClass="Messagetext"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="text"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
            </td>
        </tr>
    </table>
    <div id="divEmpContactDetails" class="detailsborder">
        <table width="100%" class="detailsbg">
            <tr>
                <td>
                    <asp:Label ID="lblEmpCurrentAddress" runat="server" Text="Current Address" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlEmpCurrentAddress" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 25%">
                        <asp:Label ID="lblCHouseNo" runat="server" Text="House No"></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtCHouseNo" runat="server" MaxLength="10"></asp:TextBox>
                        <span id="spanCHouseNo" runat="server">
                            <img id="imgCHouseNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                    <td style="width: 25%">
                        <asp:Label ID="lblCApartment" runat="server" Text="Apartment/Building"></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtCApartment" runat="server" MaxLength="25"></asp:TextBox>
                        <span id="spanCApartment" runat="server">
                            <img id="imgCApartment" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <asp:Label ID="lblCStreetName" runat="server" Text="Street Name/Sector No."></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtCStreetName" runat="server" MaxLength="25"></asp:TextBox>
                        <span id="spanCStreetName" runat="server">
                            <img id="imgCStreetName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                    <td style="width: 25%">
                        <asp:Label ID="lblLandmark" runat="server" Text="Landmark"></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtCLandmark" runat="server" MaxLength="25"></asp:TextBox>
                        <span id="spanCLandmark" runat="server">
                            <img id="imgCLandmark" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <asp:Label ID="lblCCity" runat="server" Text="City"></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtCCity" runat="server" MaxLength="10"></asp:TextBox>
                        <span id="spanCCity" runat="server">
                            <img id="imgCCity" runat="server" alt="" src="Images/cross.png" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                    <td style="width: 25%">
                        <asp:Label ID="lblCState" runat="server" Text="State"></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtCState" runat="server" MaxLength="20"></asp:TextBox>
                        <span id="spanCState" runat="server">
                            <img id="imgCState" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <asp:Label ID="lblCCountry" runat="server" Text="Country"></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txtCCountry" runat="server" MaxLength="20"></asp:TextBox>
                        <span id="spanCCountry" runat="server">
                            <img id="imgCCountry" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                    <td style="width: 25%">
                        <asp:Label ID="lblPinCode" runat="server" Text="Pincode/Zipcode"></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td style="width: 25%;">
                        <asp:TextBox ID="txtCPinCode" runat="server" MaxLength="6"></asp:TextBox>
                        <span id="spanCPinCode" runat="server">
                            <img id="imgCPinCode" runat="server" alt="" src="Images/cross.png" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <asp:Label ID="lblSamePerAddress" runat="server" Text="Is Permanet Address same as above"></asp:Label>
                    </td>
                    <td style="width: 25%">
                        <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>--%>
                        <asp:RadioButtonList ID="rblSamePerAddress" runat="server" RepeatDirection="Horizontal"
                            Width="75%" AutoPostBack="True" OnSelectedIndexChanged="rblSamePerAddress_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="True">Yes</asp:ListItem>
                            <asp:ListItem Value="False">No</asp:ListItem>
                        </asp:RadioButtonList>
                        <%--   </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </td>
                    
                    <td style="width: 25%" colspan = "2" align="right">
                        <asp:Button ID="btnChangeAddress" runat="server" CssClass="button" 
                            OnClick="btnChangeAddress_Click" Text="Change Address" />
                        <asp:Button ID="btnCanelChange" runat="server" CssClass="button" 
                            OnClick="btnCanelChange_Click" Text="Cancel"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server">
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <asp:Panel ID="pnlPermanenetAddr" runat="server">
                        <table width="100%" class="detailsbg">
                            <tr>
                                <td>
                                    <asp:Label ID="lblPermanentAddress" runat="server" Text="Permanent Address" CssClass="detailsheader"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblPHouseNo" runat="server" Text="House No"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtPHouseNo" runat="server" MaxLength="10"></asp:TextBox>
                                    <span id="spanPHouseNo" runat="server">
                                        <img id="imgPHouseNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblPApartment" runat="server" Text="Apartment/Building"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtPApartment" runat="server" MaxLength="25"></asp:TextBox>
                                    <span id="spanPApartment" runat="server">
                                        <img id="imgPApartment" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblPStreetName" runat="server" Text="Street Name/Sector No."></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtPStreetName" runat="server" MaxLength="25"></asp:TextBox>
                                    <span id="spanPStreetName" runat="server">
                                        <img id="imgPStreetName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblPLandmark" runat="server" Text="Landmark"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtPLandmark" runat="server" MaxLength="25"></asp:TextBox>
                                    <span id="spanPLandmark" runat="server">
                                        <img id="imgPLandmark" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblPCity" runat="server" Text="City"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtPCity" runat="server" MaxLength="10"></asp:TextBox>
                                    <span id="spanPCity" runat="server">
                                        <img id="imgPCity" runat="server" alt="" src="Images/cross.png" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblPState" runat="server" Text="State"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtPState" runat="server" MaxLength="20"></asp:TextBox>
                                    <span id="spanPState" runat="server">
                                        <img id="imgPState" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Label ID="lblPCountry" runat="server" Text="Country"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtPCountry" runat="server" MaxLength="20"></asp:TextBox>
                                    <span id="spanPCountry" runat="server">
                                        <img id="imgPCountry" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%">
                                    <asp:Label ID="lblPPincode" runat="server" Text="Pincode/Zipcode"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtPPincode" runat="server" MaxLength="6"></asp:TextBox>
                                    <span id="spanPPincode" runat="server">
                                        <img id="imgPPincode" runat="server" alt="" src="Images/cross.png" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
            <%--    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblSamePerAddress" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </asp:Panel>
        <br />
        <table width="100%" class="detailsbg">
            <tr>
                <td>
                    <asp:Label ID="lblContactNumbers" runat="server" Text="Contact Numbers" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblContactError" runat="server"  CssClass="text"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="divContactDetails" runat="server">
            <asp:GridView ID="gvContactDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                OnRowDataBound="gvContactDetails_RowDataBound" OnRowCommand="gvContactDetails_RowCommand"
                ShowFooter="True" OnRowDeleting="gvContactDetails_RowDeleting" OnRowEditing="gvContactDetails_RowEditing"
                OnRowCancelingEdit="gvContactDetails_RowCancelingEdit" OnRowUpdating="gvContactDetails_RowUpdating">
                <HeaderStyle CssClass="headerStyle" />
                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                <RowStyle Height="20px" CssClass="textstyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Contact Type">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlContactType" runat="server" Width="155px">
                            </asp:DropDownList>
                            <asp:Label ID="lblContactType" runat="server" Visible="false" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ContactTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAddContactType" runat="server" Width="155px">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="City Code">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCityCode" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCityCode" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddCityCode" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Country Code">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCountryCode" Text='<%# DataBinder.Eval(Container, "DataItem.CountryCode") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCountryCode" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CountryCode") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddCountryCode" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CountryCode") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact No.">
                        <ItemTemplate>
                            <asp:Label ID="lblContactNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNo") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtContactNo" runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNo") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddContactNo" runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNo") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Extension">
                        <ItemTemplate>
                            <asp:Label ID="lblExtension" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Extension") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtExtension" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.Extension") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddExtension" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.Extension") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Availability Time">
                        <ItemTemplate>
                            <asp:Label ID="lblAvailabilityTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvalibilityTime") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAvailabilityTime" runat="server" MaxLength="30" Text='<%# DataBinder.Eval(Container, "DataItem.AvalibilityTime") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddAvailabilityTime" runat="server" MaxLength="30" Text='<%# DataBinder.Eval(Container, "DataItem.AvalibilityTime") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Commands">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/Edit.gif" CommandName="Edit"
                                ToolTip="Edit Contact Numbers" />&nbsp;&nbsp;
                            <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                CommandName="Delete" ToolTip="Delete Contact Numbers" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgBtnUpdate" runat="server" ImageUrl="~/Images/update.bmp"
                                CommandName="Update" ToolTip="Update Contact Numbers" />&nbsp;&nbsp;
                            <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/cancel.gif" CommandName="Cancel"
                                ToolTip="Cancel Contact Numbers" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgBtnAdd" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="Add"
                                ToolTip="Add Contact Numbers" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="HfContactType" runat="server" Value='<%# Eval("ContactType") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="HfContactType" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ContactType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="ContactId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.EmployeeContactId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="Mode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 25%">
                                Contact Type<label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:DropDownList ID="ddlEmptyContactType" runat="server" Width="155px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 25%">
                                City Code<label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtEmptyCityCode" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode") %>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                Country Code<label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtEmptyCountryCode" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.CountryCode") %>'></asp:TextBox>
                            </td>
                            <td style="width: 25%">
                                Contact No<label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtEmptyContactNo" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNo") %>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                Extension<label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtEmptyExtension" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.Extension") %>'></asp:TextBox>
                            </td>
                            <td style="width: 25%">
                                Time of Availability<label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtEmptyAvailabilityTime" runat="server" MaxLength="30" Text='<%# DataBinder.Eval(Container, "DataItem.AvalibilityTime") %>'></asp:TextBox>
                                &nbsp;&nbsp;<asp:ImageButton ID="imgBtnEmptyAdd" runat="server" ImageUrl="~/Images/plus.JPG"
                                    CommandName="EmptyAdd" ToolTip="Add Contact Numbers" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:HiddenField ID="EMPId" runat="server" />
            <asp:HiddenField ID="EMPAddressId" runat="server" />
        </div>
        <br />
        <table width="100%" class="detailsbg">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Emergency Details" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblEmrgencyError" runat="server"  CssClass="text"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="divEmergencyDetails" class="detailsborder" runat="server">
            <asp:GridView ID="gvEmergencyDetails" runat="server" AutoGenerateColumns="False"
                Width="100%" OnSelectedIndexChanged="gvEmergencyDetails_SelectedIndexChanged"
                ShowFooter="True" OnRowCancelingEdit="gvEmergencyDetails_RowCancelingEdit" OnRowCommand="gvEmergencyDetails_RowCommand"
                OnRowDataBound="gvEmergencyDetails_RowDataBound" OnRowDeleting="gvEmergencyDetails_RowDeleting"
                OnRowEditing="gvEmergencyDetails_RowEditing" OnRowUpdating="gvEmergencyDetails_RowUpdating"
                PageSize="5">
                <HeaderStyle CssClass="addrowheader" />
                <AlternatingRowStyle CssClass="alternatingrowStyle" />
                <RowStyle Height="20px" CssClass="textstyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Person Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblName" Text='<%# DataBinder.Eval(Container, "DataItem.ContactName") %>'></asp:Label>
                          </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtName" runat="server" MaxLength="30" Text='<%# DataBinder.Eval(Container, "DataItem.ContactName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddName" runat="server" MaxLength="30" Text='<%# DataBinder.Eval(Container, "DataItem.ContactName") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Relation">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblRelation" Text='<%# DataBinder.Eval(Container, "DataItem.Relation") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlRelation" runat="server" Width="155px">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAddRelation" runat="server" Width="155px">
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact No.">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblContactNo" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNumber") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtContactNo" runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNumber") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddContactNo" runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNumber") %>'></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Commands">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/Edit.gif" CommandName="Edit"
                                ToolTip="Edit Emergency Details" />&nbsp;&nbsp;
                            <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                CommandName="Delete" ToolTip="Delete Emergency Details" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgBtnUpdate" runat="server" ImageUrl="~/Images/update.bmp"
                                CommandName="Update" ToolTip="Update Emergency Details" />&nbsp;&nbsp;
                            <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/cancel.gif" CommandName="Cancel"
                                ToolTip="Cancel Emergency Details" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgBtnAdd" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="Add"
                                ToolTip="Add Emergency Details" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="EmrContactId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.EmergencyContactId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="Mode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Mode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="HfRelationType" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.RelationType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 25%">
                                Person Name<label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtEmptyName" runat="server" MaxLength="30" Text='<%# DataBinder.Eval(Container, "DataItem.ContactName") %>'></asp:TextBox>
                            </td>
                            <td style="width: 25%">
                                Relation<label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <%--<asp:TextBox ID="txtEmptyRelation" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.Relation") %>'></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlEmptyRelation" runat="server" Width="155px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%">
                                Contact No<label class="mandatorymark">
                                    *</label>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtEmptyContactNo" runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNumber") %>'></asp:TextBox>
                            </td>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 25%">
                                <asp:ImageButton ID="imgBtnEmptyAdd" runat="server" ImageUrl="~/Images/plus.JPG"
                                    CommandName="EmptyAdd" ToolTip="Add Emergency Details" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
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
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="button" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update" Visible="false"
                            OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" 
                             onclick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
