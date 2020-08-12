<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeContactDetails.aspx.cs" Inherits="EmployeeContactDetails" %>

<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
          <%--  Sanju:Issue Id 50201: End--%>
                <asp:Label ID="lblEmployeeDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label>
            </td>
             <%-- Sanju:Issue Id 50201: Added background color property so that all browsers display header--%>
            <td align="right" style="height: 25px; width: 20%;background-color:#7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
               <%-- Sanju:Issue Id 50201:End--%>
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

                <script type="text/javascript">
                  setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmployeeProfile');

//                  function ValidateSpecialCharacters() {
//                      if (event.keyCode == 46 || event.keyCode == 32 || (event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) {
//                          event.returnValue = true;

//                      }
//                      else {
//                          event.returnValue = false;
//                          alert("Please input alphanumeric value only");
//                      }

//                  }

                  function ValidateCityStateCountry() {
                      if (event.keyCode == 32 || (event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122)) {
                          event.returnValue = true;

                      }
                      else {
                          event.returnValue = false;
                          alert("Please input valid characters only");
                      }

                  }

                  //function to check whether pincode has 6 characters or not
                  function Max_Length() {
                      
                     
                      if (document.getElementById('<%=txtCPinCode.ClientID %>').value.length<6) {
                          alert("Please enter atleast 6 characters");
                          // 26878-Ambar-Start
                          // Commented following line
                          // document.getElementById('<%=txtCPinCode.ClientID %>').focus();
                          // 26878-Ambar-End
                          return (false);
                      }
                          if(document.getElementById('<%=txtCPinCode.ClientID %>').value<100000)
                          {alert("Please enter a valid pincode");
                          
                         // document.getElementById('<%=txtCPinCode.ClientID %>').value = "";
                          document.getElementById('<%=txtCPinCode.ClientID %>').focus();
                          return (false);
                      }
}
                  

                              
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
                            var HouseNo = $('<%=txtCFlatNo.ClientID %>').id
                            var Apartment = $('<%=txtCApartment.ClientID %>').id
                            var StreetName = $('<%=txtCStreetName.ClientID %>').id
                            var Landmark = $('<%=txtCLandmark.ClientID %>').id
                            var City = $('<%=txtCCity.ClientID %>').id
                            var State = $('<%=txtCState.ClientID %>').id
                            var Country = $('<%=txtCCountry.ClientID %>').id
                            var PinCode = $('<%=txtCPinCode.ClientID %>').id 
                            
                            //GET perment address details
                            var PHouseNo = $('<%=txtPFlatNo.ClientID %>').id
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
                            var HouseNo = $('<%=txtCFlatNo.ClientID %>').id
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
 
                </script> <table width="100%">
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
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="9pt" class="mandatorymark">Please click on the <span style="font-weight:bold">Save All</span> button to save contact details.</asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="divEmpContactDetails" class="detailsborder">
                    <table width="100%" class="detailsbg">
                        <tr>
                            <td>
                                <asp:Label ID="lblEmpCurrentAddress" runat="server" Text="Current Address" CssClass="detailsheader"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlEmpCurrentAddress" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblCFlatNo" runat="server" Text="Flat No & Floor"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtCFlatNo" runat="server" MaxLength="40" CssClass="mandatoryField" ></asp:TextBox>
                                    <span id="spanCFlatNo" runat="server">
                                        <img id="imgCFlatNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblCApartment" runat="server" Text="Apartment/Building"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtCApartment" runat="server" MaxLength="40" CssClass="mandatoryField" ></asp:TextBox>
                                    <span id="spanCApartment" runat="server">
                                        <img id="imgCApartment" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblCStreetName" runat="server" Text="Street Name/Sector No."></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtCStreetName" runat="server" MaxLength="50" CssClass="mandatoryField" ></asp:TextBox>
                                    <span id="spanCStreetName" runat="server">
                                        <img id="imgCStreetName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblLandmark" runat="server" Text="Landmark"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtCLandmark" runat="server" MaxLength="50" CssClass="mandatoryField"  ></asp:TextBox>
                                    <span id="spanCLandmark" runat="server">
                                        <img id="imgCLandmark" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblCCity" runat="server" Text="City"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtCCity" runat="server" MaxLength="40" CssClass="mandatoryField" OnKeyPress="ValidateCityStateCountry()"></asp:TextBox>
                                    <span id="spanCCity" runat="server">
                                        <img id="imgCCity" runat="server" alt="" src="Images/cross.png" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblCState" runat="server" Text="State"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtCState" runat="server" MaxLength="40" CssClass="mandatoryField" OnKeyPress="ValidateCityStateCountry()"></asp:TextBox>
                                    <span id="spanCState" runat="server">
                                        <img id="imgCState" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblCCountry" runat="server" Text="Country"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtCCountry" runat="server" MaxLength="40" CssClass="mandatoryField" OnKeyPress="ValidateCityStateCountry()"></asp:TextBox>
                                    <span id="spanCCountry" runat="server">
                                        <img id="imgCCountry" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblPinCode" runat="server" Text="Pincode"></asp:Label>
                                    <label class="mandatorymark">
                                        *</label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtCPinCode" runat="server" MaxLength="6" CssClass="mandatoryField"  ></asp:TextBox>
                                    <span id="spanCPinCode" runat="server">
                                        <img id="imgCPinCode" runat="server" alt="" src="Images/cross.png" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblSamePerAddress" runat="server" Text="Is Permanent Address same as above"></asp:Label>
                                </td>
                                <td style="width: 25%">
                                    <asp:RadioButtonList ID="rblSamePerAddress" runat="server" RepeatDirection="Horizontal"
                                        Width="75%" AutoPostBack="True" OnSelectedIndexChanged="rblSamePerAddress_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="False">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnChangeAddress" runat="server" CssClass="button" OnClick="btnChangeAddress_Click"
                                        Text="Change Address" Visible="false" />
                                    <asp:Button ID="btnCanelChange" runat="server" CssClass="button" OnClick="btnCanelChange_Click"
                                        Text="Cancel" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:Panel ID="pnlPermanenetAddr" runat="server">
                            <table width="100%" class="detailsbg">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPermanentAddress" runat="server" Text="Permanent Address" CssClass="detailsheader"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblPFlatNo" runat="server" Text="Flat No & Floor"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPFlatNo" CssClass="mandatoryField" runat="server" MaxLength="40" ></asp:TextBox>
                                        <span id="spanPFlatNo" runat="server">
                                            <img id="imgPFlatNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblPApartment" runat="server" Text="Apartment/Building"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtPApartment" runat="server" CssClass="mandatoryField" MaxLength="40" ></asp:TextBox>
                                        <span id="spanPApartment" runat="server">
                                            <img id="imgPApartment" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblPStreetName" runat="server" Text="Street Name/Sector No."></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPStreetName" CssClass="mandatoryField" runat="server" MaxLength="50" ></asp:TextBox>
                                        <span id="spanPStreetName" runat="server">
                                            <img id="imgPStreetName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblPLandmark" runat="server" Text="Landmark"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtPLandmark" runat="server" CssClass="mandatoryField" MaxLength="50" ></asp:TextBox>
                                        <span id="spanPLandmark" runat="server">
                                            <img id="imgPLandmark" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblPCity" runat="server" Text="City"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPCity" runat="server" CssClass="mandatoryField" MaxLength="40" OnKeyPress="ValidateCityStateCountry()"></asp:TextBox>
                                        <span id="spanPCity" runat="server">
                                            <img id="imgPCity" runat="server" alt="" src="Images/cross.png" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblPState" runat="server" Text="State"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtPState" runat="server" CssClass="mandatoryField" MaxLength="40" OnKeyPress="ValidateCityStateCountry()"></asp:TextBox>
                                        <span id="spanPState" runat="server">
                                            <img id="imgPState" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblPCountry" runat="server" Text="Country"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtPCountry" runat="server" CssClass="mandatoryField" MaxLength="40" OnKeyPress="ValidateCityStateCountry()"></asp:TextBox>
                                        <span id="spanPCountry" runat="server">
                                            <img id="imgPCountry" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblPPincode" runat="server" Text="Pincode"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtPPincode" runat="server" CssClass="mandatoryField" MaxLength="6"></asp:TextBox>
                                        <span id="spanPPincode" runat="server">
                                            <img id="imgPPincode" runat="server" alt="" src="Images/cross.png" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                    <br />
                    <table width="100%" class="detailsbg">
                        <tr>
                            <td>
                                <asp:Label ID="lblContactNumbers" runat="server" Text="Contact Numbers" CssClass="detailsheader"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblContactError" runat="server" CssClass="text"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMsg" runat="server" CssClass="Messagetext" Text="Please specify 2 Nos,1 of which is a Residence number."></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlContactDetails" runat="server">
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
                                        <asp:DropDownList ID="ddlContactType" runat="server">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblContactType" runat="server" Visible="false" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.ContactTypeName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlAddContactType" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCountryCode" Text='<%# DataBinder.Eval(Container, "DataItem.CountryCode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCountryCode" runat="server" MaxLength="6" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.CountryCode") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddCountryCode" runat="server" MaxLength="6" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.CountryCode") %>'></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCityCode" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCityCode" runat="server" MaxLength="6" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddCityCode" runat="server" MaxLength="6" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode") %>'></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtContactNo" runat="server" MaxLength="10" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNo") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddContactNo" runat="server" MaxLength="10" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNo") %>'></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Extension">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExtension" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Extension") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtExtension" runat="server" MaxLength="6" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.Extension") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddExtension" runat="server" MaxLength="6" Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.Extension") %>'></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Availability Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAvailabilityTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvalibilityTime") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAvailabilityTime" runat="server" MaxLength="30" Width="100px"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.AvalibilityTime") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddAvailabilityTime" runat="server" MaxLength="30" Width="100px"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.AvalibilityTime") %>'></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/Edit.gif" CommandName="Edit"
                                            ToolTip="Edit Contact Numbers" />&nbsp;&nbsp;
                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                            CommandName="Delete" ToolTip="Delete Contact Numbers" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgBtnUpdate" runat="server" ImageUrl="~/Images/update.bmp"
                                            CommandName="Update" ToolTip="Update Contact Numbers" />&nbsp;&nbsp;
                                        <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/cancel.gif"
                                            CommandName="Cancel" ToolTip="Cancel Contact Numbers" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="imgBtnAdd" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="Add"
                                            ToolTip="Add Contact Numbers" Text="Add Contact" CssClass="button" />
                                    </FooterTemplate>
                                </asp:TemplateField>
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
                                        <td style="width: 25%" class="txtstyle">
                                            Contact Type<label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlEmptyContactType" CssClass="mandatoryField" runat="server"
                                                Width="155px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            Country Code
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtEmptyCountryCode" CssClass="mandatoryField" runat="server" MaxLength="6"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.CountryCode") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            City Code
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtEmptyCityCode" runat="server" MaxLength="6" Text='<%# DataBinder.Eval(Container, "DataItem.CityCode") %>'></asp:TextBox>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            Contact No<label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtEmptyContactNo" runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.ContactNo") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            Extension
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtEmptyExtension" CssClass="mandatoryField" runat="server" MaxLength="6"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.Extension") %>'></asp:TextBox>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            Time of Availability
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtEmptyAvailabilityTime" CssClass="mandatoryField" runat="server"
                                                MaxLength="30" Text='<%# DataBinder.Eval(Container, "DataItem.AvalibilityTime") %>'></asp:TextBox>
                                            &nbsp;&nbsp;
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
                                            <asp:Button ID="imgBtnEmptyAdd" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="EmptyAdd"
                                                ToolTip="Add Contact Numbers" Text="Add Contact" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:HiddenField ID="EMPId" runat="server" />
                        <asp:HiddenField ID="EMPAddressId" runat="server" />
                        <asp:HiddenField ID="EMPPerAddressId" runat="server" />
                        <asp:HiddenField ID="HfIsDataModified" runat="server" />
                    </div>
                    </asp:Panel>
                    <br />
                    <table width="100%" class="detailsbg">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Emergency Details" CssClass="detailsheader"></asp:Label>
                                <label class="mandatorymark">
                                    *</label>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblEmrgencyError" runat="server" CssClass="text"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlEmergencyDetails" runat="server">
                        <asp:GridView ID="gvEmergencyDetails" runat="server" AutoGenerateColumns="False"
                            Width="100%" 
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
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Images/Edit.gif" CommandName="Edit"
                                            ToolTip="Edit Emergency Details" />&nbsp;&nbsp;
                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                            CommandName="Delete" ToolTip="Delete Emergency Details" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgBtnUpdate" runat="server" ImageUrl="~/Images/update.bmp"
                                            CommandName="Update" ToolTip="Update Emergency Details" />&nbsp;&nbsp;
                                        <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/cancel.gif"
                                            CommandName="Cancel" ToolTip="Cancel Emergency Details" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="imgBtnAdd" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="Add"
                                            ToolTip="Add Emergency Details" Text="Add Emergency" CssClass="button" />
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
                                        <td style="width: 25%" class="txtstyle">
                                            Person Name<label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtEmptyName" CssClass="mandatoryField" runat="server" MaxLength="30"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.ContactName") %>'></asp:TextBox>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            Relation<label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlEmptyRelation" CssClass="mandatoryField" runat="server"
                                                Width="155px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            Contact No<label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtEmptyContactNo" CssClass="mandatoryField" runat="server" MaxLength="10"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.ContactNumber") %>'></asp:TextBox>
                                        </td>
                                        <td style="width: 25%">
                                        </td>
                                        <td style="width: 25%">
                                            <asp:Button ID="imgBtnEmptyAdd" runat="server" ImageUrl="~/Images/plus.JPG" CommandName="EmptyAdd"
                                                ToolTip="Add Emergency Details" Text="Add Emergency" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnlSeatDetails" runat="server">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%" class="txtstyle">
                                    <asp:Label ID="lblSeatNumber" runat="server" Text="Seat Number"></asp:Label>
                                </td>
                                <td rowspan="3" align="left">
                                    <asp:TextBox ID="txtSeatNumber" runat="server" CssClass="mandatoryField" MaxLength="10"></asp:TextBox>
                                    <asp:Label ID="Label2" runat="server" Text="eg. 2C-50,E-C1." CssClass="Messagetext"></asp:Label>
                                    <span id="spanSeatNumber" runat="server">
                                        <img id="imgSeatNumber" runat="server" src="Images/cross.png" alt="" style="display: none;
                                            border: none;" />
                                    </span>
                                </td>
                            </tr>
                        </table>
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
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="button" OnClick="btnEdit_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Save All" Visible="false"
                                        OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
