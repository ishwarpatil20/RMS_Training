<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeCertification.aspx.cs" Inherits="EmployeeCertification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">


    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
         <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
             <%-- Sanju:Issue Id 50201:End--%>
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
                                        <asp:Label ID="lblEmpCertification" runat="server" Text="Certification" CssClass="detailsheader"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Certificationdetails" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="mandatoryField" ToolTip="Enter name" MaxLength="25" Width = "150"></asp:TextBox>
                                            <span id="spanName" runat="server">
                                                <img id="imgName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblCertificationDate" runat="server" Text="Certification Date"></asp:Label>
                                            <label class="mandatorymark">
                                                *</label>
                                        </td>
                                        <td style="width: 25%">
                                            <uc1:DatePicker ID = "ucDatePickerCertificationDate" runat="server" Width = "150"/>
                                            <span id="spanCertificationDate" runat="server">
                                                <img id="imgcCertificationDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblCertficationValidDate" runat="server" Text="Certification Valid Till"></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            <uc1:DatePicker ID = "ucDatePickerCertficationValidDate" runat="server" Width = "150"/>                                 
                                            <span id="spanCertficationValidDate" runat="server">
                                                <img id="imgcCertficationValidDate" runat="server" src="Images/cross.png" alt=""
                                                    style="display: none; border: none;" />
                                            </span>
                                        </td>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblTotalScore" runat="server" Text="Total Score"></asp:Label>
                                            <%--<label class="mandatorymark">
                                                *</label>--%>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtTotalScore" runat="server" CssClass="mandatoryField" ToolTip="Enter Total Score" MaxLength="4" Width = "150"></asp:TextBox>
                                            <span id="spanTotalScore" runat="server">
                                                <img id="imgTotalScore" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%" class="txtstyle">
                                            <asp:Label ID="lblOutOf" runat="server" Text="Out Of"></asp:Label>
                                            <%--<label class="mandatorymark">
                                                *</label>--%>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtOutOf" runat="server" CssClass="mandatoryField" ToolTip="Enter Out Of" MaxLength="4" Width = "150"></asp:TextBox>
                                            <span id="spanOutOf" runat="server">
                                                <img id="imgOutOf" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                    border: none;" />
                                            </span>
                                        </td>
                                        <td style="width: 25%;" colspan="2" align="right">
                                            <asp:Button ID="btnUpdate" runat="server" CssClass="button" OnClick="btnUpdate_Click"
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
                                <div id="divGVCertification">
                                    <asp:GridView ID="gvCertification" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnRowDeleting="gvCertification_RowDeleting" OnRowEditing="gvCertification_RowEditing" OnRowDataBound="gvCertification_RowDataBound">
                                        <HeaderStyle CssClass="headerStyle" />
                                        <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                        <RowStyle Height="20px" CssClass="textstyle" />
                                        <Columns>
                                            <asp:BoundField DataField="CertificationName" HeaderText="Name" />
                                            <asp:BoundField DataField="CertificateDate" HeaderText="Certification Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="CertificateValidDate" HeaderText="Certfication Valid Date"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="Score" HeaderText="Total Score" />
                                            <asp:BoundField DataField="OutOf" HeaderText="Out Of" />
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="CertificationId" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container, "DataItem.CertificationId") %>'></asp:Label>
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
                                                        ToolTip="Edit Certification Detail" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/Delete.gif"
                                                        CommandName="Delete" ToolTip="Delete Certification Detail" />
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

                <script language="javascript" type="text/javascript">
                    function $(v) { return document.getElementById(v); }

                    setbgToTab('ctl00_tabEmployee', 'ctl00_SpanEmployeeProfile');

                    function ButtonClickValidate() {
                        var lblMandatory;
                        var spanlist = "";

                        var Name = $('<%=txtName.ClientID %>').id
                        var CertificationDate = $('<%=ucDatePickerCertificationDate.ClientID %>').id
                        var TotalScore = $('<%=txtTotalScore.ClientID %>').id
                        var OutOf = $('<%=txtOutOf.ClientID %>').id


                        if (spanlist == "") {
                            // Mohamed :Issue 50440 :  10/04/2014 : Starts
                            // Desc : Remove Mandatory validation from "TotalScore" and "OutOf" Fields
                            
                            //var controlList = Name + "," + CertificationDate + "," + TotalScore + "," + OutOf;
                            var controlList = Name + "," + CertificationDate ;
                        }
                        else {
                            //var controlList = spanlist + Name + "," + CertificationDate + "," + TotalScore + "," + OutOf;
                            var controlList = spanlist + Name + "," + CertificationDate;

                            // Mohamed :Issue 50440 : 10/04/2014 : Ends
                        }

                        if (ValidateControlOnClickEvent(controlList) == false) {
                            lblError = $('<%=lblError.ClientID %>')
                            lblError.innerText = "Please fill all mandatory fields.";
                            lblError.style.color = "Red";
                        }

                        return ValidateControlOnClickEvent(controlList);
                    }

                    function SaveButtonClickValidate() {
                        var lblMandatory;
                        var spanlist = "";

                        var Name = $('<%=txtName.ClientID %>').value;
                        var CertificationDate = $('<%=ucDatePickerCertificationDate.ClientID %>').value;
                        var CertficationValidDate = $('<%=ucDatePickerCertficationValidDate.ClientID %>').value;
                        var TotalScore = $('<%=txtTotalScore.ClientID %>').value;
                        var OutOf = $('<%=txtOutOf.ClientID %>').value;

                        var AllControlEmpty;

                        if (Name == "" && CertificationDate == "" && CertficationValidDate == "" && TotalScore == "" && OutOf == "") {
                            AllControlEmpty = "Yes";
                        }

                        if (AllControlEmpty != "Yes") {
                            if ($('<%=btnAddRow.ClientID %>') == null) {
                                alert("To save the Certification details entered, kindly click on Update");
                                return false;
                            }
                            else {
                                alert("To save the Certification details entered, kindly click on Add row");
                                return false;
                            }
                        }
                        else {
                            return true;
                        }
                    }
                </script>

            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
