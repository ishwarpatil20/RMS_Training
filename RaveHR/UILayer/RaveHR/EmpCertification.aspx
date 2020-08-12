<%@ Page Language="C#" MasterPageFile="~/MasterPage/Employee.master" AutoEventWireup="true"
    CodeFile="EmpCertification.aspx.cs" Inherits="EmpCertification" Title="Certification"
    EnableEventValidation="false" %>

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
                            <asp:Label ID="lblEmpCertification" runat="server" Text="Certification" CssClass="detailsheader"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Certificationdetails" runat="server">
                <table width="100%">
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtName" runat="server" ToolTip="Enter name" MaxLength="20"></asp:TextBox>
                            <span id="spanName" runat="server">
                                <img id="imgName" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblCertificationDate" runat="server" Text="Certification Date"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtCertificationDate" runat="server" ToolTip="Select Certification Date"></asp:TextBox>
                            <asp:ImageButton ID="imgCertificationDate" runat="server" CausesValidation="false"
                                ImageAlign="AbsMiddle" ImageUrl="Images/Calendar_scheduleHS.png" TabIndex="17" />
                            <cc1:CalendarExtender ID="CalendarExtenderCertiDate" runat="server" PopupButtonID="imgCertificationDate"
                                TargetControlID="txtCertificationDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <span id="spanCertificationDate" runat="server">
                                <img id="imgcCertificationDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <asp:Label ID="lblCertficationValidDate" runat="server" Text="Certification Valid Date"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtCertficationValidDate" runat="server" ToolTip="Select Certification Valid Date"></asp:TextBox>
                            <asp:ImageButton ID="imgCertficationValidDate" runat="server" CausesValidation="false"
                                ImageAlign="AbsMiddle" ImageUrl="Images/Calendar_scheduleHS.png" TabIndex="17"
                                Width="16px" />
                            <cc1:CalendarExtender ID="CalendarExtenderCertiValidDate" runat="server" PopupButtonID="imgCertficationValidDate"
                                TargetControlID="txtCertficationValidDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <span id="spanCertficationValidDate" runat="server">
                                <img id="imgcCertficationValidDate" runat="server" src="Images/cross.png" alt=""
                                    style="display: none; border: none;" />
                            </span>
                        </td>
                        <td style="width: 25%">
                            <asp:Label ID="lblTotalScore" runat="server" Text="Total Score"></asp:Label>
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtTotalScore" runat="server" ToolTip="Enter Total Score" MaxLength="4"></asp:TextBox>
                            <span id="spanTotalScore" runat="server">
                                <img id="imgTotalScore" runat="server" src="Images/cross.png" alt="" style="display: none;
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
                        <td style="width: 25%;" colspan="2" align="right">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="button" OnClick="btnUpdate_Click"
                                TabIndex="14" Text="Update" Visible="false" />
                            <asp:Button ID="btnCancelRow" runat="server" CssClass="button" OnClick="btnCancelRow_Click"
                                TabIndex="14" Text="Cancel" Visible="false" />
                            <asp:Button ID="btnAddRow" runat="server" CssClass="button" OnClick="btnAdd_Click"
                                TabIndex="14" Text="Add Row" />
                        </td>
                    </tr>
                </table>
                <div id="divGVCertification">
                    <asp:GridView ID="gvCertification" runat="server" Width="100%" AutoGenerateColumns="False"
                        OnRowDeleting="gvCertification_RowDeleting" OnRowEditing="gvCertification_RowEditing">
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
    setbackcolorToTab('divMenu_3');
    
    function ButtonClickValidate()
    {
        var lblMandatory;
        var spanlist = "";

        var Name = $('<%=txtName.ClientID %>').id
        var CertificationDate = $('<%=txtCertificationDate.ClientID %>').id
        var CertficationValidDate = $( '<%=txtCertficationValidDate.ClientID %>').id
        var TotalScore =$('<%=txtTotalScore.ClientID %>').id
        var OutOf = $('<%=txtOutOf.ClientID %>').id
        
         
        if(spanlist == "")
        {
            var controlList = Name +"," + CertificationDate +","+CertficationValidDate + "," + TotalScore +","+ OutOf;
        }
        else
        {
            var controlList = spanlist + Name +"," + CertificationDate +","+CertficationValidDate + "," + TotalScore +","+ OutOf;
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

        var Name = $('<%=txtName.ClientID %>').value;
        var CertificationDate = $('<%=txtCertificationDate.ClientID %>').value;
        var CertficationValidDate = $('<%=txtCertficationValidDate.ClientID %>').value;
        var TotalScore = $( '<%=txtTotalScore.ClientID %>').value;
        var OutOf = $('<%=txtOutOf.ClientID %>').value;
 
        var AllControlEmpty;
      
        if(Name == "" && CertificationDate == ""  && CertficationValidDate =="" && TotalScore == "" && OutOf == "")
        {
           AllControlEmpty = "Yes";
        }
        
        if(AllControlEmpty != "Yes")         
        {
            if($('<%=btnAddRow.ClientID %>')== null)
            {
                alert("To save the Certification details entered, kindly click on Update");
                return false;
            }
            else
            {
                alert("To save the Certification details entered, kindly click on Add row");
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
