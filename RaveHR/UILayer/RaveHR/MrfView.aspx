<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="MrfView.aspx.cs" Inherits="MrfView" Title="Resource Management System" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <asp:Panel ID="pnlHeaderViewMRF" runat="server" Visible="true" Width="100%">
        <table width="100%">
            <tr>
                <%--Sanju:Issue Id 50201: Added new class header_employee_profile so that the header color is same for all browsers--%>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <%-- Sanju:Issue Id 50201: End--%>
                    <span class="header">
                        <asp:Label ID="lblHeaderViewEdit" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlHeaderAllocateResource" runat="server" Visible="false" Width="100%">
        <table width="100%">
            <tr>
                <%--Sanju:Issue Id 50201: Added new class header_employee_profile so that the header color is same for all browsers--%>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <span class="header">Allocate Resource</span>
                </td>
                <%-- Sanju:Issue Id 50201: End--%>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                ValidationGroup="Save" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                ValidationGroup="MoveMrf" />
            <td style="width: 860px">
                <asp:Label ID="lblMessage" runat="server" CssClass="Messagetext"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
            </td>
        </tr>
    </table>
    <div id="divMRFDetails" class="detailsborder">
        <table width="100%" class="detailsbg">
            <tr>
                <td>
                    <asp:Label ID="lblMRFDetails" runat="server" Text="MRF Details" CssClass="detailsheader"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="lblMRFCode" runat="server" Text="MRF Code" CssClass="txtstyle"></asp:Label>
                </td>
                <td style="width: 18px">
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtMRFCode" runat="server" Width="500px" CssClass="mandatoryField"></asp:TextBox>
                    <asp:Label ID="lblNewMRFCode" runat="server" Text="New MRF Code" Visible="true" CssClass="txtstyle">
                        <asp:TextBox ID="txtNewMRFCode" runat="server" Width="0px" Visible="true" CssClass="mandatoryField">
                        </asp:TextBox>
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="MRF Status" CssClass="txtstyle"></asp:Label>
                </td>
                <td style="width: 18px">
                </td>
                <td style="width: 277px">
                    <%--<asp:TextBox ID="txtMRFStatus" runat="server" Width="210px" CssClass="mandatoryField"></asp:TextBox>--%>
                    <span id="spanzMRFStatus" runat="server">
                        <asp:DropDownList ID="ddlMRFStatus" runat="server" Width="215px" CssClass="mandatoryField"
                            OnSelectedIndexChanged="ddlMRFStatus_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </span>
                </td>
                <td style="width: 165px">
                    <asp:Label ID="lblMRFType" runat="server" Text="MRF Type" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td>
                </td>
                <td>
                    <span id="spanzMRFType" runat="server">
                        <asp:DropDownList ID="ddlMRFType" runat="server" Width="215px" CssClass="mandatoryField">
                        </asp:DropDownList>
                    </span>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDepartment" runat="server" Text="Department" CssClass="txtstyle"></asp:Label>
                </td>
                <td style="width: 18px">
                </td>
                <td style="width: 277px">
                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="215px" CssClass="mandatoryField"
                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td style="width: 165px">
                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" CssClass="txtstyle"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProjectName" runat="server" Width="215px" CssClass="mandatoryField"
                        OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblResourcePlanCode" runat="server" Text="Resource Plan Code" CssClass="txtstyle"></asp:Label>
                </td>
                <td style="width: 18px">
                </td>
                <td style="width: 277px">
                    <asp:DropDownList ID="ddlResourcePlanCode" runat="server" Width="215px" CssClass="mandatoryField"
                        OnSelectedIndexChanged="ddlResourcePlanCode_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td style="width: 165px">
                    <asp:Label ID="lblRole" runat="server" Text="Role" CssClass="txtstyle"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRole" runat="server" Width="215px" CssClass="mandatoryField"
                        OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:GridView ID="grdresource" runat="server" Width="70%" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnRowDataBound="grdresource_RowDataBound"
                        OnRowCommand="grdresource_RowCommand">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" AutoPostBack="false" Checked='<%# DataBinder.Eval(Container.DataItem,"CheckGridValue")  %>' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                        type="checkbox" disabled />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblRPDuId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ResourcePlanDurationId") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ResourcePlanStartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="ResourcePlanEndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="ResourceLocation" HeaderText="Location" />
                            <asp:TemplateField Visible="true" HeaderText="Utilization">
                                <ItemTemplate>
                                    <asp:Label ID="lblUtilization" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Utilization") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="true" HeaderText="Billing">
                                <ItemTemplate>
                                    <asp:Label ID="lblBilling" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Billing") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Billing Date
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <uc1:DatePicker ID="billingDatePicker" runat="server" style="width: 40px;" />
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%--   <asp:ImageButton runat="server" ID="imgView" ImageUrl="~/Images/view.png" ToolTip="View"
                                        Width="20px" Height="20px" CommandName="View" CommandArgument='<%# Container.DisplayIndex %>' />
                          --%>
                                </ItemTemplate>
                                <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#7590C8" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#2461BF" />
                        <RowStyle Height="20px" CssClass="txtstyle" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 24%">
                    <asp:Label ID="lblDateOfRequisition" runat="server" Text="Date of Requisition" CssClass="txtstyle"></asp:Label>
                </td>
                <td style="width: 18px">
                </td>
                <td style="width: 277px">
                    <span id="spanStartDateTooltip3">
                        <asp:TextBox ID="txtDateOfRequisition" runat="server" MaxLength="30" AutoComplete="off"
                            Width="210px" CssClass="mandatoryField"></asp:TextBox>
                        <asp:ImageButton ID="imgDateOfRequisition" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                            CausesValidation="false" ImageAlign="AbsMiddle" Visible="false" />
                        <cc1:CalendarExtender ID="calendarDateOfRequisition" runat="server" PopupButtonID="imgDateOfRequisition"
                            TargetControlID="txtDateOfRequisition" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </span>
                </td>
                <td style="width: 165px">
                    <asp:Label ID="lblRequiredFrom" runat="server" Text="Required From" CssClass="txtstyle"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <span id="spanREquiredFrom">
                        <asp:TextBox ID="txtRequiredFrom" runat="server" MaxLength="30" AutoComplete="off"
                            Width="210px" CssClass="mandatoryField"></asp:TextBox>
                    </span>
                </td>
                <td>
                    <asp:ImageButton ID="imgRequiredFrom" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                        CausesValidation="false" ImageAlign="AbsMiddle" Visible="false" />
                    <cc1:CalendarExtender ID="CalendarRequiredFrom" runat="server" PopupButtonID="imgRequiredFrom"
                        TargetControlID="txtRequiredFrom" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRequiredTill" runat="server" Text="Required Till" CssClass="txtstyle"></asp:Label>
                </td>
                <td style="width: 18px">
                </td>
                <td style="width: 277px">
                    <span id="spanRequiredTill">
                        <asp:TextBox ID="txtRequiredTill" runat="server" MaxLength="30" AutoComplete="off"
                            Width="210px" CssClass="mandatoryField"></asp:TextBox>
                        <asp:ImageButton ID="imgRequiredTill" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                            CausesValidation="false" ImageAlign="AbsMiddle" Visible="false" />
                        <cc1:CalendarExtender ID="CalendarRequiredTill" runat="server" PopupButtonID="imgRequiredTill"
                            TargetControlID="txtRequiredTill" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </span>
                </td>
                <td style="width: 165px">
                    <asp:Label ID="lblProjectDescription" runat="server" Text="Project Description" CssClass="txtstyle"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtProjectDescription" runat="server" Width="210px" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblMustHaveSkills" runat="server" Text="Mandatory Skills" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right" width="5%">
                    <span id="spanMustHaveSkills" runat="server">
                        <img id="imgMustHaveSkills" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtMustHaveSkills" TextMode="MultiLine" runat="server" Height="50px"
                        Width="210px" MaxLength="5000" ToolTip="Enter Must To Have Skill" TabIndex="1"
                        CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblGoodSkills" runat="server" Text="Good To Have Skills" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanGoodSkills" runat="server">
                        <img id="imgGoodSkills" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtGoodSkills" TextMode="MultiLine" runat="server" Height="50px"
                        Width="210px" MaxLength="5000" ToolTip="Enter Good To Have Skill" TabIndex="2"
                        CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblTools" runat="server" TextMode="MultiLine" Text="Tools" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanTools" runat="server">
                        <img id="imgTools" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtTools" runat="server" Width="210px" Height="50px" MaxLength="5000"
                        TextMode="MultiLine" ToolTip="Enter Tool" TabIndex="3" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtExperince" runat="server" Width="50px" MaxLength="3" ToolTip="Enter Experience From"
                        TabIndex="5"></asp:TextBox>--
                    <asp:TextBox ID="txtExperince1" runat="server" Width="50px" MaxLength="3" ToolTip="Enter Experience To"
                        TabIndex="6"></asp:TextBox>--%>
                    <asp:Label ID="lblExperince" runat="server" Text="Experience in yrs" CssClass="txtstyle">
                    </asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td>
                </td>
                <td>
                    <%--<asp:TextBox ID="txtExperince" runat="server" Width="50px" MaxLength="3" ToolTip="Enter Experience From"
                        TabIndex="5"></asp:TextBox>--
                    <asp:TextBox ID="txtExperince1" runat="server" Width="50px" MaxLength="3" ToolTip="Enter Experience To"
                        TabIndex="6"></asp:TextBox>--%>
                    <asp:TextBox ID="txtExperince" runat="server" Width="50px" MaxLength="3" ToolTip="Enter Experience From"
                        TabIndex="4" CssClass="mandatoryField"></asp:TextBox>--
                    <asp:TextBox ID="txtExperince1" runat="server" Width="50px" MaxLength="3" ToolTip="Enter Experience To"
                        TabIndex="5" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblDomain" runat="server" Text="Domain" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanDomain" runat="server">
                        <img id="imgDomain" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtDomain" runat="server" Width="210px" MaxLength="50" ToolTip="Enter Domain"
                        TabIndex="5" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                    <%--AutoPostBack="true"--%>
                    <asp:Label ID="lblSkillCategory" runat="server" Text="Skills Category" CssClass="txtstyle">
                    </asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanExperience" runat="server">
                        <img id="imgExperience" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <%--AutoPostBack="true"--%>
                    <span id="spanzSkillCategory" runat="server">
                        <asp:DropDownList ID="ddlSkillsCategory" runat="server" Width="215px" ToolTip="Enter Skill Category"
                            TabIndex="6" CssClass="mandatoryField">
                        </asp:DropDownList>
                    </span>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblHeighestQualification" runat="server" Text="Highest Qualification"
                        CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td align="right">
                    <span id="spanHeightQualification" runat="server">
                        <img id="imgHeightQualification" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtHeighestQualification" runat="server" Width="210px" MaxLength="30"
                        ToolTip="Enter Highest Qualification" TabIndex="7" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSoftSkills" runat="server" Text="Soft Skills" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanSoftSkill" runat="server">
                        <img id="imgSoftSkill" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtSoftSkills" TextMode="MultiLine" runat="server" Height="50px"
                        Width="210px" MaxLength="5000" ToolTip="Enter Soft Skill" TabIndex="8" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblUtilization" runat="server" Text="Utilization(%)" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanUtilization" runat="server">
                        <img id="imgUtilization" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtUtilijation" runat="server" Width="210px" MaxLength="3" ToolTip="Enter Utilization"
                        TabIndex="9" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblBilling" runat="server" Text="Billing(%)" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanBilling" runat="server">
                        <img id="imgBilling" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <%--35093-Ambar-03072012-Added OnTextChanged and AutoPostBack Properties--%>
                    <asp:TextBox ID="txtBilling" runat="server" Width="210px" MaxLength="3" ToolTip="Enter Billing"
                        TabIndex="10" CssClass="mandatoryField" OnTextChanged="TxtBilling_TextChanged"
                        AutoPostBack="true"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblTargetCTC" runat="server" Text="Target CTC (Lks)" CssClass="txtstyle"></asp:Label>
                </td>
                <td align="right">
                    <span id="spanCTC" runat="server">
                        <img id="imgCTC" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <%--32182-Ambar-19122011-Changed MaxLength property--%>
                    <asp:TextBox ID="txtTargetCTC" runat="server" CssClass="mandatoryField" MaxLength="5"
                        BorderStyle="NotSet" TabIndex="11" AutoComplete="off" Width="50px" ToolTip="Enter Target CTC From"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtTargetCTC1" runat="server" CssClass="mandatoryField" MaxLength="5"
                        BorderStyle="NotSet" TabIndex="12" AutoComplete="off" Width="50px" ToolTip="Enter Target CTC To"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanRemarks" runat="server">
                        <img id="imgRemarks" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Height="50px" Width="210px"
                        MaxLength="5000" ToolTip="Enter Remarks" TabIndex="13" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblResponsiblePerson" runat="server" Text="Accountable To" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanResponsiblePerson" runat="server">
                        <img id="imgResponsiblePerson" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td width="27%">
                    <asp:TextBox ID="txtResponsiblePerson" runat="server" Width="210px" Height="50px"
                        TextMode="MultiLine" MaxLength="100" ToolTip="Enter Accountable To" TabIndex="14"
                        CssClass="mandatoryField"></asp:TextBox>
                    <%--  Sanju:Issue Id 50201:Changed cursor to pointer--%>
                    <img id="imgResponsiblePersonSearch" runat="server" src="Images/find.png" alt=""
                        class="cursor_pointer" />
                    <%--Sanju:Issue Id 50201:End--%>
                </td>
                <td width="20%">
                    <asp:Label ID="lblResourceResponsibility" runat="server" Text="Resource Responsibility"
                        CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanResourceResponsibility" runat="server">
                        <img id="imgResourceResponsibility" runat="server" src="Images/cross.png" alt=""
                            style="display: none; border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtResourceResponsibility" TextMode="MultiLine" runat="server" Height="50px"
                        Width="210px" ToolTip="Enter Resource Responsibility" MaxLength="5000" TabIndex="15"
                        CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblColorCode" runat="server" Text="MRF Color Code" CssClass="txtstyle"></asp:Label>
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td width="27%">
                    <asp:DropDownList ID="ddlColorCode" runat="server" Width="215px" ToolTip="Enter MRF Color Code"
                        TabIndex="16" CssClass="mandatoryField">
                        <asp:ListItem Text="Amber" Value="Amber" />
                        <asp:ListItem Text="Red" Value="Red" />
                        <asp:ListItem Text="Green" Value="Green" />
                        <asp:ListItem Text="White" Value="White" />
                    </asp:DropDownList>
                </td>
                <td style="width: 220px">
                    <asp:Label ID="lblResourceName" runat="server" Visible="false" Text="Resource Name"
                        CssClass="txtstyle">
                    </asp:Label>
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtResourceJoined" runat="server" Visible="false" Width="210px"
                        MaxLength="30" TabIndex="17" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr id="Tr1" runat="server">
                <td style="width: 220px">
                    <asp:Label ID="lblRecruiterName" runat="server" Text="Recruiter Name" CssClass="txtstyle">
                    </asp:Label>
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="ddlRecruiterName" runat="server" Width="215px" CssClass="mandatoryField">
                    </asp:DropDownList>
                </td>
                <td style="width: 220px">
                    <asp:Label ID="lblClientName" runat="server" Text="Client Name" CssClass="txtstyle">
                    </asp:Label>
                    <label class="mandatorymark" id="mandatorymarkClientName" runat="server">
                        *</label>
                </td>
                <td valign="top" align="right">
                    <span id="spanClientName" runat="server">
                        <img id="imgClientName" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtClientName" runat="server" Width="210px" MaxLength="30" ToolTip="Enter Client Name"
                        TabIndex="17" CssClass="mandatoryField"></asp:TextBox>
                </td>
            </tr>
            <tr id="Tr2" runat="server">
                <td style="width: 220px; height: 31px;">
                    <asp:Label ID="lblPurpose1" runat="server" Text="Purpose" CssClass="txtstyle">
                    </asp:Label>
                    <label class="mandatorymark" runat="server" id="lblPurposemandatory">
                        *</label>
                </td>
                <td valign="top" align="right" style="height: 31px">
                    &nbsp;
                </td>
                <td style="height: 31px">
                    <span id="spanzPurpose" runat="server">
                        <asp:DropDownList ID="ddlPurpose" runat="server" Width="215px" CssClass="mandatoryField"
                            OnSelectedIndexChanged="ddlPurpose_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <span id="spanzddlDepartment1" runat="server">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPurpose"
                                Display="None" ErrorMessage="Please Select Purpose" ValidationGroup="MoveMrf"
                                InitialValue="SELECT"></asp:RequiredFieldValidator>
                        </span></span>
                </td>
                <td style="width: 220px; height: 31px;">
                    <asp:Label ID="lblRForExtendingEClDates" runat="server" Text="Reason For Extending Expected Closure Dates"
                        CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark" id="lblRForExtendingEClDatesmandatory" runat="server">
                        *</label>
                </td>
                <td valign="top" align="right" style="height: 31px">
                    &nbsp;
                </td>
                <td style="height: 31px">
                    <asp:TextBox ID="txtReasonExpDate" runat="server" Width="210px" MaxLength="50" CssClass="mandatoryField"></asp:TextBox>
                </td>
            </tr>
            <tr id="Tr3" runat="server">
                <td style="width: 220px">
                    <asp:Label ID="lblPurposedescription" runat="server" Text="" CssClass="txtstyle">
                    </asp:Label>
                    <label class="mandatorymark" id="lblmandatorymarkPurpose" runat="server" visible="false">
                        *</label>
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtPurposeDescription" runat="server" Width="210px" MaxLength="50"
                        TabIndex="17" CssClass="mandatoryField" Visible="false"></asp:TextBox>
                    <img id="imgPurpose" runat="server" src="Images/find.png" alt="" visible="false"
                        class="cursor_pointer" /><span id="spanzddlDepartment0" runat="server"><asp:DropDownList
                            ID="ddlPuposeDepartment" Visible="false" runat="server" Width="155px" CssClass="mandatoryField"
                            ToolTip="Select Department">
                        </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="MoveMrf"
                                ErrorMessage="Please Select Department" ControlToValidate="ddlPuposeDepartment"
                                InitialValue="SELECT" Display="None"></asp:RequiredFieldValidator>
                            <span id="spanzddlDepartment" runat="server">
                                <asp:RequiredFieldValidator ID="valRequired" runat="server" ErrorMessage="" ControlToValidate="txtPurposeDescription"
                                    Display="dynamic" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPurposeDescription"
                                    ErrorMessage="Please Enter Mandatory Fields" ValidationGroup="MoveMrf" Display="None"></asp:RequiredFieldValidator>
                            </span></span>
                </td>
                <td style="width: 220px">
                    &nbsp;
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblExpectedClosureDate" runat="server" Text="Expected Closure Date"
                        CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark" id="lblExpectedClosureDatemandatory" runat="server">
                        *</label>
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td>
                    <uc1:DatePicker ID="uclExpectedClosureDate" runat="server" style="width: 210px;" />
                </td>
                <td style="width: 220px">
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblBillingDate" runat="server" Text="Billing Date" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark" id="lblBillingDatemandatory" runat="server">
                        *</label>
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td>
                    <uc1:DatePicker ID="txtBillingDate" runat="server" style="width: 210px;" />
                </td>
                <td style="width: 220px">
                    <asp:Label ID="lblReasonMoveMRF" runat="server" Text="Reason For Moving MRF" CssClass="txtstyle"
                        Visible="false"></asp:Label>
                    <label class="mandatorymark" id="lblReasonMoveMRFMandatory" runat="server" visible="false">
                        *</label>
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtReasonMoveMRF" runat="server" Width="210px" MaxLength="50" CssClass="mandatoryField"
                        Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 220px" rowspan="3">
                    <asp:Label ID="lblSOWNo" runat="server" Text="SOW No" CssClass="txtstyle"></asp:Label>
                </td>
                <td align="right" rowspan="3">
                    <span id="spanSOWNo" runat="server">
                        <img id="imgSOWNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td rowspan="3">
                    <asp:TextBox ID="txtSOWNo" runat="server" Width="210px" MaxLength="30" AutoPostBack="True"
                        OnTextChanged="txtSOWNo_TextChanged" ToolTip="Enter SOWNo" TabIndex="7" CssClass="mandatoryField">
                    </asp:TextBox>
                </td>
                <td style="width: 165px">
                    <asp:Label ID="lblSowStartDt" runat="server" Text="SOW Start Date" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark" id="mandmarkSowStartDt" visible="false" runat="server">
                        *</label>
                </td>
                <td>
                </td>
                <td>
                    <uc1:DatePicker ID="DtPckerSOWStartDt" runat="server" style="width: 210px;" />
                </td>
            </tr>
            <tr runat="server" id="trCostCode">
                <td style="width: 165px; height: 28px;" class="txtstyle">
                    CostCode
               <label class="mandatorymark" id="lblCostCodeRequired" visible="true" runat="server">
                        *</label>
                    <asp:RequiredFieldValidator ID="rfCostCodeValidator" runat="server" ControlToValidate="ddlCostCode"
                        ValidationGroup="Save" ErrorMessage="Please Select Cost Code" Display="None"
                        InitialValue="SELECT" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                <td style="height: 28px">
                    &nbsp;
                </td>
                <td style="height: 28px">
                    <span id="spnCostCode" runat="server">
                        <asp:DropDownList ID="ddlCostCode" runat="server" Width="200px" CssClass="mandatoryField"
                            ValidationGroup="Save" ToolTip="Select Cost Code" 
                        OnSelectedIndexChanged="ddlCostCode_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr >
                <td style="height: 14px;" class="txtstyle" colspan="3">
                   <div ID="divOverride" runat="server" visible="false">
                   <asp:CheckBox ID="chkOverride" runat="server" Text="Override" />
                        </div>
                            
                </td>
            </tr>
            <tr>
                <td style="width: 165px">
                    <asp:Label ID="lblSOWEndDt" runat="server" Text="SOW End Date" CssClass="txtstyle" />
                    <label class="mandatorymark" id="mandmrkSOWEndDt" visible="false" runat="server">
                        *</label>
                </td>
                <td>
                </td>
                <td>
                    <uc1:DatePicker ID="DtPckSOWEndDt" runat="server" style="width: 210px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblClosedDate" runat="server" Text="" CssClass="txtstyle"></asp:Label>
                </td>
                <td valign="top" align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtClosedDate" runat="server" Width="210px" MaxLength="50" CssClass="mandatoryField"
                        disabled></asp:TextBox>
                </td>
            </tr>
            <%--Ishwar Patil 20/04/2015 Start--%>
            <tr>
                <td style="width: 220px">
                    <asp:Label ID="lblSkills" runat="server" Text="Skills" CssClass="txtstyle"></asp:Label>
                </td>
                <td valign="top" align="right">
                    <span id="spanSkills" runat="server">
                        <img id="imgSkills" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td width="27%">
                    <asp:TextBox ID="TxtSkills" runat="server" Width="210px" Height="50px" TextMode="MultiLine"
                        MaxLength="2000" ToolTip="Select Skills" TabIndex="14"></asp:TextBox>
                    <img id="imgSkillsSearch" runat="server" src="Images/find.png" alt="" class="cursor_pointer" />
                </td>
            </tr>
            <%--Ishwar Patil 20/04/2015 End--%>
            <%--Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
                Desc : IN Mrf Details ,GroupId need to implement--%>
            <tr>
                <td colspan="3">
                    <asp:CheckBox runat="server" ID="chkGroupId" Text=" Separate from current GroupId"
                        Visible="false" CssClass="txtstyle" />
                </td>
            </tr>
            <%--Mohamed : Issue 50791 : 12/05/2014 : Ends--%>
        </table>
        <asp:Panel ID="pnlDelete" runat="server" Visible="false" Width="100%" Height="50px">
            <table width="100%" border="0">
                <tr>
                    <td width="25%">
                        <asp:Label ID="lblReson" runat="server" Text="" Visible="true" CssClass="txtstyle"></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td align="right" valign="top" width="2%">
                        <span id="spanReason" runat="server">
                            <img id="imgReason" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                    <td width="23%" colspan="3">
                        <asp:TextBox ID="txtReason" TextMode="MultiLine" runat="server" Height="50px" Width="210px"
                            MaxLength="100" CssClass="mandatoryField">
                        </asp:TextBox>
                    </td>
                    <td colspan="2" style="width: 34%">
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlInternalResourceDetails" runat="server" Visible="false" Width="100%">
            <table width="100%" class="detailsbg">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Internal Resource Details" CssClass="detailsheader"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ID="DeleteImage" ImageUrl="Images/Delete.gif" ToolTip="Delete Resource"
                            CommandName="ViewImageBtn" alt="Delete" Style="border: none; cursor: hand;" OnClick="DeleteResource_Click" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td style="width: 162px">
                        <asp:Label ID="Label4" runat="server" Text="Resource Name" CssClass="txtstyle"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td style="width: 27%">
                        <asp:TextBox ID="txtResourceName" runat="server" Enabled="false" CssClass="mandatoryField"
                            Width="210px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Utilization(%)" Visible="False" CssClass="txtstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUtilization_ISR" runat="server" Enabled="false" Visible="false"
                            CssClass="mandatoryField"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Billing" Visible="False" CssClass="txtstyle"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBilling_ISR" runat="server" Enabled="false" Visible="false" CssClass="mandatoryField"></asp:TextBox>
                    </td>
                    <td style="width: 162px">
                        <asp:Label ID="lblAllocationDate" runat="server" Text="Allocation Date" CssClass="txtstyle">
                        </asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td style="width: 5%" valign="top" align="right">
                        <span id="spanzAllocationDate" runat="server">
                            <img id="imgAllocationDateError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                    <td style="width: 27%">
                        <span id="spanzlbAllocationDate" runat="server">
                            <asp:TextBox ID="txtAllocationDate" runat="server" CssClass="mandatoryField" Width="130px"
                                onblur="RestrictDateTypingAndPaste(this)" onpaste="return RestrictDateTypingAndPaste(this)"></asp:TextBox>
                            <asp:ImageButton ID="imgAllocationDate" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                                CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="6" />
                            <cc1:CalendarExtender ID="calendardate" runat="server" PopupButtonID="imgAllocationDate"
                                TargetControlID="txtAllocationDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTypeOfClosure" Text="Type Of Closure" runat="server" CssClass="txtstyle"></asp:Label>
                        <label class="mandatorymark">
                            *</label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <span id="spanzClosureType" runat="server">
                            <asp:DropDownList ID="ddlClosureType" runat="server" CssClass="mandatoryField" Width="132px">
                            </asp:DropDownList>
                        </span>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="4">
                     
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="hidMRFID" runat="server" />
                    <asp:HiddenField ID="hidprojectName" runat="server" />
                    <asp:HiddenField ID="hidrole" runat="server" />
                    <asp:HiddenField ID="hidExp" runat="server" />
                    <asp:HiddenField ID="hidCTC" runat="server" />
                    <asp:HiddenField ID="hidDepartment" runat="server" />
                    <asp:HiddenField ID="hidResponsiblePersonName" runat="server" />
                    <asp:HiddenField ID="hidInternalResourceId" runat="server" />
                    <asp:HiddenField ID="hidOldRequiredFromDate" runat="server" />
                    <asp:HiddenField ID="hidProjectStartDate" runat="server" />
                    <asp:HiddenField ID="hidResourcePlanDurationId" runat="server" />
                    <asp:HiddenField ID="hidOldRequiredTillDate" runat="server" />
                    <asp:HiddenField ID="hidProjectEndDate" runat="server" />
                    <asp:HiddenField ID="hidContractTypeID" runat="server" />
                    <asp:HiddenField ID="hidSLADays" runat="server" />
                    <asp:HiddenField ID="hidEncryptedQueryString" runat="server" />
                    <asp:HiddenField ID="hidRecruiterId" runat="server" />
                    <asp:HiddenField ID="hidFutureEmpID" runat="server" />
                    <asp:HiddenField ID="hidTypeOfAllocation" runat="server" />
                    <asp:HiddenField ID="hidEmployeeName" runat="server" />
                    <asp:HiddenField ID="hidReportingToName" runat="server" />
                    <asp:HiddenField ID="hidExpectedClosedDate" runat="server" />
                    <asp:HiddenField ID="hidReasonExpectedCloserDate" runat="server" />
                    <asp:HiddenField ID="hidRequestForRecruitment" runat="server" />
                    <asp:HiddenField ID="hidOldMRFCode" runat="server" />
                    <asp:HiddenField ID="hidRPId" runat="server" />
                    <asp:HiddenField ID="hidNoOfResources" runat="server" />
                    <asp:HiddenField ID="hidGridEnabled" runat="server" />
                    <asp:HiddenField ID="hidResponsiblePerson" runat="server" />
                    <asp:HiddenField ID="hidMoveMrfRoleID" runat="server" />
                    <asp:HiddenField ID="hidMoveMrfRoleName" runat="server" />
                    <asp:HiddenField ID="txtMRFStatus" runat="server" />
                    <asp:HiddenField ID="previousMRFStatus" runat="server" />
                    <%-- Ishwar Patil 22/04/2015 Start --%>
                    <asp:HiddenField ID="hidSkillsName" runat="server" />
                    <asp:HiddenField ID="hidSkills" runat="server" />
                    <%-- Ishwar Patil 22/04/2015 End --%>
                    <%-- 57877-Venkatesh-  29042016 : Start 
                     Add sai email if while raising headcount for nis projects--%>
                    <asp:HiddenField ID="hidprojectId" runat="server" />
                    <%--57877-Venkatesh-  29042016 : End--%>
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="pnlPendingAllocation" runat="server" Visible="false" Width="100%">
            <table style="width: 91%">
                <tr>
                    <td align="Right">
                        <%-- Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra --%>
                        <%--Added OnClientClick ="return checkResourceSelected(this);"--%>
                        <asp:Button ID="btnAllocate" runat="server" CssClass="button" OnClick="btnAllocate_Click" ValidationGroup="Save" OnClientClick="return Allocation_Validate();"
                            Text="Allocate" Visible="true" Width="118px" />
                        <%-- Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra --%>
                        <asp:HiddenField runat="server" ID="hidToken" />
                        <%-- Issue Id : 34229 END CONCURRENCY HANDLED Mahendra --%>
                        &nbsp
                        <%--Madhura:Issue Id 50201:Changed width--%>
                        <asp:Button ID="btnRaiseHeadCount" runat="server" Text="Raise Head Count" Width="130px"
                            CssClass="button" TabIndex="24" Visible="true" />
                        <%--Madhura:Issue Id 50201:End--%>
                        &nbsp
                        <%--Madhura:Issue Id 50201:Changed width--%>
                        <asp:Button ID="btnSelectInternalResource" runat="server" Text="Select Internal Resource"
                            Width="172px" CssClass="button" TabIndex="21" Visible="true" />
                        <%--Madhura:Issue Id 50201:End--%>
                        &nbsp
                        <asp:Button ID="btnCancelAllocateResource" runat="server" Text="Cancel" Width="121px"
                            CssClass="button" TabIndex="22" OnClick="btnCancelAllocateResource_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlViewMRF" runat="server" Visible="false" Width="100%">
            <table style="width: 100%">
                <tr>
                    <td style="width: 463px; margin-left: 40px">
                        <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="button" Width="118px"
                            Visible="true" OnClick="btnPrevious_Click" />
                        &nbsp
                        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="button" Width="118px"
                            Visible="true" TabIndex="23" OnClick="btnNext_Click" />
                        &nbsp
                        <asp:Button ID="btnMoveMRF" runat="server" Text="Move MRF" Width="118px" CssClass="button"
                            TabIndex="24" Visible="true" OnClick="btnMoveMRF_Click" />
                    </td>
                    <td align="Right">
                        <asp:Button ID="btnReplaceMRF" runat="server" Text="Replace MRF" Width="118px" CssClass="button"
                            TabIndex="21" OnClick="btnReplaceMRF_Click" Visible="false" />
                        &nbsp
                        
                        
                        
                        
                        <%--Siddhesh : Issue 56245 : 28/07/2015 : Start
                        Description: Added OnClientClick() on client side and removed from server side.--%>
                        <asp:Button ID="btnSavebtn" runat="server" Text="Confirm" Width="118px" OnClientClick="return ButtonClickValidate();"
                            CssClass="button" ValidationGroup="Save" TabIndex="24" Visible="False" OnClick="btnSavebtn_Click" />
                        <%--Siddhesh : Issue 56245 : 28/07/2015 : End--%>
                        <asp:Button ID="btnRaiseMRF" runat="server" Text="Raise MRF" Width="118px" CssClass="button"
                            TabIndex="24" Visible="False" OnClick="btnRaiseMRF_Click" />
                        &nbsp
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="118px" CssClass="button"
                            TabIndex="24" Visible="true" OnClick="btnEdit_Click" />
                        &nbsp
                        <%--Aarohi : Issue : 29878 : 03/01/2012--%>
                        <%--Changed the button Name from Delete to Abort--%>
                        <asp:Button ID="btnDelete" runat="server" Text="Abort" Width="118px" CssClass="button"
                            TabIndex="21" OnClick="btnDelete_Click" Visible="False" />
                        &nbsp
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="121px" CssClass="button"
                            TabIndex="22" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var retVal;
        (function($) {
            $(document).ready(function() {
                //    Page_ClientValidate("Save,MoveMrf");
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        retVal = e.data;
                        if ($.modalDialog.getCurrent())
                            $.modalDialog.getCurrent().postMessage(e.data);
                    }
                });


                function Allocation_Validate() {
                    debugger;
                 
                    if (checkResourceSelected()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                    }
                }
                $('#ctl00_cphMainContent_btnAllocate').click(function(e) {
                    debugger;
//                    if (!Page_ClientValidate("Save")) {
//                        Page_BlockSubmit = false;
//                        e.preventDefault();
//                        return;
//                    }
                    if (checkResourceSelected()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                    }

                });

                //                $(window).on('beforeunload', function() {
                //                    var button1 = $('#ctl00_cphMainContent_btnAllocate');
                //                    button1.attr('disabled', 'disabled');
                //                    button1.val('Please Wait..');
                //                    setTimeout(function() {
                //                        button1.removeAttr('disabled');
                //                    },20000);
                //                });
            });
        })(jQuery);

        //Ishwar Patil 20042015 Start
        function popUpSkillSearch() {
            jQuery.modalDialog.create({ url: "SkillsList.aspx", maxWidth: 550,
                onclose: function(e) {
                    var txtskills = jQuery('#<%=TxtSkills.ClientID %>');
                    var hidskills = jQuery('#<%=hidSkills.ClientID %>');
                    var hidskillsName = jQuery('#<%=hidSkillsName.ClientID %>');

                    var EmpName;
                    var EmpId;
                    var employee = new Array();
                    if (retVal != undefined)
                        employee = retVal.split(",");
                    for (var i = 0; i < employee.length - 1; i++) {
                        var emp = employee[i];
                        var emp1 = new Array();
                        var emp1 = emp.split("_");
                        if (EmpId == undefined) {
                            EmpId = emp1[0];
                        }
                        else {
                            EmpId = EmpId + "," + emp1[0];
                        }
                        if (EmpName == undefined) {
                            EmpName = emp1[1];
                        }
                        else {
                            EmpName = EmpName + "," + emp1[1];
                        }
                    }
                    if (EmpId != undefined) {
                        hidskills.val(EmpId.trim());
                    }
                    if (EmpName != undefined) {
                        txtskills.val(EmpName.trim());
                        hidskillsName.val(EmpName.trim());
                    }
                }
            }).open();
        }
        //Ishwar Patil 20042015 End

        function fnRaiseHeadCountPopup(e) {
            debugger;
            var IsResourceSelected = $('<%=hidInternalResourceId.ClientID %>').value
            var lblMessage = $('<%=lblMessage.ClientID %>');
            lblMessage.innerHTML = "";
            //object to validate resurce is already selected or not
            var IsResourceExist = $('<%=txtResourceName.ClientID%>');

            if (IsResourceSelected == "yes" && IsResourceExist == null) {

                var popupWindow = null;
                var EncryptedQueryString = $('<%=hidEncryptedQueryString.ClientID %>').value;

                //Sayali:Issue Id 50201: Added dimensions for modalPopup
                jQuery.modalDialog.create({ url: EncryptedQueryString, maxWidth: 700,
                    onclose: function(e) {
                        location.href = "MrfPendingAllocation.aspx";
                    }
                }).open();
                //Sayali:Issue Id 50201:End

                //return false;
            }
            else {
                if (IsResourceExist != null) {
                    lblMessage.innerHTML = "Delete the selected internal resource in order to raise head count.";
                    lblMessage.style.color = "RED";
                    return false;
                }
            }
            return false;
        }

        function popUpEmployeeSearch() {
            jQuery.modalDialog.create({ url: "EmployeesList.aspx?PageName=MrfRaiseNextOrRaiseHeadCount", maxWidth: 600,
                onclose: function(e) {
                    var txtResponsiblePerson = jQuery('#<%=txtResponsiblePerson.ClientID %>');
                    var hidResponsiblePerson = jQuery('#<%=hidResponsiblePersonName.ClientID %>');
                    var hidReportingToName = jQuery('#<%=hidReportingToName.ClientID %>');

                    var EmpName;
                    var EmpId;
                    var employee = new Array();
                    if (retVal != undefined)
                        employee = retVal.split(",");
                    for (var i = 0; i < employee.length - 1; i++) {
                        var emp = employee[i];
                        var emp1 = new Array();
                        var emp1 = emp.split("_");
                        if (EmpId == undefined) {
                            EmpId = emp1[0];
                        }
                        else {
                            EmpId = EmpId + "," + emp1[0];
                        }
                        if (EmpName == undefined) {
                            EmpName = emp1[1];
                        }
                        else {
                            EmpName = EmpName + "," + emp1[1];
                        }
                    }
                    if (EmpId != undefined) {
                        hidResponsiblePerson.val(EmpId.trim());
                    }
                    if (EmpName != undefined) {
                        txtResponsiblePerson.val(EmpName.trim());
                        hidReportingToName.val(EmpName.trim());
                    }
                }
            }).open();
        }

        function fnInternalResourcePopup() {
            jQuery.modalDialog.create({ url: "MRFInternalResource.aspx", maxWidth: 900,
                onclose: function(e) {
                    location.reload(true);
                }
            }).open();
            return false;
        }

        function popUpEmployeeName() {
            jQuery.modalDialog.create({ url: "EmployeesList.aspx?PageName=MrfRaiseNextOrRaiseHeadCount", maxWidth: 550,
                onclose: function(e) {
                    var txtPurpose = jQuery('#<%=txtPurposeDescription.ClientID %>');
                    var EmployeeName = jQuery('#<%=hidEmployeeName.ClientID %>');
                    var EmpName;
                    var EmpId;
                    var employee = new Array();
                    if (retVal != undefined)
                        employee = retVal.split(",");
                    for (var i = 0; i < employee.length - 1; i++) {
                        var emp = employee[i];
                        var emp1 = new Array();
                        var emp1 = emp.split("_");

                        if (EmpName == undefined) {
                            EmpName = emp1[1];
                        }
                        else {
                            EmpName = EmpName + "," + emp1[1];
                        }
                    }
                    if (EmpName != undefined) {
                        txtPurpose.val(EmpName.trim());
                        EmployeeName.val(EmpName.trim());
                    }
                }
            }).open();
        }
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        function $(v) { return document.getElementById(v); }

        function fnConfirmDelete() {
            return confirm('Are you sure want to Delete?');
        }

        // Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra
        var last = null;
        function f(obj) {
            // Note: Disabling it here produced strange results. More investigation required.
            last = obj;
            setTimeout("reset()", 1 * 1000);
            return true;
        }

        function reset() {
            last.disabled = "false";
        }
        // Issue Id : 34229 END CONCURRENCY HANDLED Mahendra



        function checkResourceSelected(obj) {
            //debugger;
            var IsResourceSelected = $('<%=hidInternalResourceId.ClientID %>').value;
            var IsAllocationDate = $('<%=txtAllocationDate.ClientID %>');
            var lblMessage = $('<%=lblMessage.ClientID %>');
            lblMessage.value = "";
            var spanlist = "";
            
            if (!Page_ClientValidate("Save")) {
                Page_BlockSubmit = false;
                return false;
            }

            //object to validate resurce is already selected or not
            var IsResourceExist = $('<%=txtResourceName.ClientID%>');

            if (IsResourceSelected == "yes" && IsResourceExist == null) {
                lblMessage.innerHTML = "Please select internal resource to perform allocate operation.";
                lblMessage.style.color = "RED";
                //alert("Kindly, click on 'Select Internal Resource' to perform allocate operation.");
                return false;
            }

            if (IsAllocationDate != null) {
                var AllocationDate = $('<%=txtAllocationDate.ClientID %>').value;
            }
            if (AllocationDate == "" && IsResourceSelected == "yes") {
                spanlist = $('<%=txtAllocationDate.ClientID %>').id;

            }
            var TypeOfClosure = $('<%=ddlClosureType.ClientID %>').value;
            if (TypeOfClosure == "" || TypeOfClosure == "SELECT") {
                var sTypeOfClosure = $('<%=spanzClosureType.ClientID %>').id;
                spanlist = spanlist + "," + sTypeOfClosure;
            }
            var controlList;
            controlList = $('<%=ddlClosureType.ClientID %>').id + "," + $('<%=txtAllocationDate.ClientID %>').id + "," + spanlist;
            if (ValidateControlOnClickEvent(controlList) == false) {
                lblMessage.innerHTML = "Please fill in all mandatory fields";
                lblMessage.style.color = "RED";
                return false;
            }
            else {
                // Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra
                // commented by mahendra
                //return true;
                return f(obj);
                // Issue Id : 34229 END CONCURRENCY HANDLED Mahendra

            }



            //return true;
        }


        function ConfirmForDeletingMRF() {
            //Aarohi : Issue : 29878(CR) : 30/12/2011 : Start
            //commenetd the below line to call the new function for confirm
            //var cnfrm = fnConfirm();
            var cnfrm = confirm('Are you sure this MRF has to be aborted?');
            //Aarohi : Issue : 29878(CR) : 30/12/2011 : End            

            if (cnfrm == true) {
                alert("MRF is successfully deleted");
                return true;
            }
            else
            { return false; }

        }

        function fnConfirm() {
            return confirm('Do you really want to delete MRF permanently,its corresponding role will also be deleted?');
        }

        //Aarohi : Issue : 29878(CR) : 30/12/2011 : Start
        //commented the below function to replace the Ok/Cancel button on the confirm box to Yes/No.
        //Sanju:Issue Id 50201 
        //commented the below code since there was break in chrome, FF and IE latest browser. Now in confirm box it will display OK and cancel instead of Yes/No.
        //        function window.confirm(str) {
        //            execScript('n = msgbox("' + str + '", "4132", "Windows Internet Explorer")', "vbscript");
        //            return (n == 6);
        //        }
        //Aarohi : Issue : 29878(CR) : 30/12/2011 : End

        function ButtonClickValidate() {
            debugger

            if (!Page_ClientValidate("Save")) {
                Page_BlockSubmit = false;
                return false;
            }

            var HeaderText = $('<%=lblHeaderViewEdit.ClientID %>').innerHTML;


            //HeaderText == ' Move MRF ' &&
            if (HeaderText == 'Move MRF' && (!Page_ClientValidate("MoveMrf"))) {
                Page_BlockSubmit = false;
                return false;
            }

            var lblMandatory;
            var spanlist = "";


            var MRFType = $('<%=ddlMRFType.ClientID %>').value;

            if (MRFType == "" || MRFType == "SELECT") {
                var sMRFType = $('<%=spanzMRFType.ClientID %>').id;
                spanlist = sMRFType + ",";
            }
            var MRFStatus = $('<%=txtMRFStatus.ClientID %>').value;

            if (HeaderText != "Move MRF" && MRFStatus != "Pending Allocation" && MRFStatus != "PendingAllocation") {
                var MRFPurpose = $('<%=ddlPurpose.ClientID %>').value;

                if (MRFPurpose == "" || MRFPurpose == "SELECT") {
                    var spanPurpose = $('<%=spanzPurpose.ClientID %>').id;
                    spanlist = spanPurpose + ",";
                }
            }

            var txtMusthaveskill = $('<%=txtMustHaveSkills.ClientID %>').id
            var txtGoodtohaveskill = $('<%=txtGoodSkills.ClientID %>').id
            var txtTools = $('<%=txtTools.ClientID %>').id
            var txtSkillCategory = $('<%=ddlSkillsCategory.ClientID %>').value
            var Department = $('<%=ddlDepartment.ClientID %>').options[$('<%=ddlDepartment.ClientID%>').selectedIndex].text;
            if (Department == "Projects") {
                if (txtSkillCategory == "" || txtSkillCategory == "SELECT" || txtSkillCategory == "0") {
                    var sSkillCategory = $('<%=spanzSkillCategory.ClientID %>').id;
                    spanlist = sSkillCategory + ",";
                }
            }
            var txtExperience = $('<%=txtExperince.ClientID %>').id
            var txtExperience1 = $('<%=txtExperince1.ClientID %>').id
            var txtHeightQualification = $('<%=txtHeighestQualification.ClientID %>').id
            var txtSoftSkill = $('<%=txtSoftSkills.ClientID %>').id
            var txtUtilization = $('<%=txtUtilijation.ClientID %>').id
            var txtBilling = $('<%=txtBilling.ClientID %>').id
            var txtRemarks = $('<%=txtRemarks.ClientID %>').id
            var txtResponsiblePerson = $('<%=txtResponsiblePerson.ClientID %>').id
            var txtResourceResponsibility = $('<%=txtResourceResponsibility.ClientID %>').id
            //Ishwar Patil 28/04/2015 Start
            var TxtSkills = $('<%=TxtSkills.ClientID %>').id
            //Ishwar Patil 28/04/2015 End
            //Siddhesh - Make Skills non mandatory
            var controlList = txtMusthaveskill + "," + txtGoodtohaveskill + "," + txtTools + "," + txtExperience + "," + txtHeightQualification + "," + txtSoftSkill + "," + txtUtilization + "," + txtBilling + "," + txtRemarks + "," + txtResponsiblePerson + "," + txtResourceResponsibility + "," + txtExperience1;
            //Siddhesh - Make Skills non mandatory
            if (Department == "PreSales - UK" || Department == "PreSales - USA") {
                var txtClientName = $('<%=txtClientName.ClientID %>').id;
                controlList = controlList + "," + txtClientName;
            }



            if ((Department == "RaveConsultant-India" || Department == "RaveConsultant-USA" ||
                  Department == "RaveConsultant-UK") && SOwNo != "") {
                var SOwNo = $('<%=txtSOWNo.ClientID %>').value;
                var DtPckrSOWStartDate = $('<%=DtPckerSOWStartDt.ClientID %>').id;
                var DatePickerSOWEndDate = $('<%=DtPckSOWEndDt.ClientID %>').id;

                controlList = controlList + "," + DtPckrSOWStartDate + "," + DatePickerSOWEndDate;
            }
            else
            { controlList = spanlist + controlList; }


            if (HeaderText != "Move MRF") {
                var Purpose = $('<%=ddlPurpose.ClientID %>').options[$('<%=ddlPurpose.ClientID%>').selectedIndex].text;
                if (Purpose == "Hiring for project" ||
                    Purpose == "Hiring for new role" ||
                    Purpose == "Replacement") {
                    var txtPurposeDescription = $('<%=txtPurposeDescription.ClientID %>').id;
                    controlList = controlList + "," + txtPurposeDescription;
                }
            }

            if (spanlist == "") {
                if (HeaderText == "Abort MRF" || HeaderText == "Replace MRF") {

                    var reason = $('<%=txtReason.ClientID %>').id
                    controlList = controlList + "," + reason;
                    //var controlList = txtMusthaveskill +"," + txtGoodtohaveskill +","+txtTools + ","+ txtExperience+","+txtExperience1+"," + txtHeightQualification+","+ txtSoftSkill+","+ txtUtilization+","+ txtBilling+","+ txtCTC+","+ txtRemarks+","+ txtResponsiblePerson+","+ txtResourceResponsibility + "," + reason + "," + txtCTC1;
                }
                else {
                    controlList;
                    //var controlList = txtMusthaveskill +"," + txtGoodtohaveskill +","+txtTools + ","+ txtExperience+","+ txtHeightQualification+","+ txtSoftSkill+","+ txtUtilization+","+ txtBilling+","+ txtCTC+","+ txtRemarks+","+ txtResponsiblePerson+","+ txtResourceResponsibility + "," + txtCTC1 + "," +txtExperience1;
                }
            }
            else {
                if (HeaderText == "Abort MRF") {
                    fnConfirm();
                    var reason = $('<%=txtReason.ClientID %>').id
                    //var controlList = spanlist + txtMusthaveskill +"," + txtGoodtohaveskill +","+txtTools + ","+ txtExperience+","+ txtHeightQualification+","+ txtSoftSkill+","+ txtUtilization+","+ txtBilling+","+ txtCTC+","+ txtRemarks+","+ txtResponsiblePerson+","+ txtResourceResponsibility+ "," + reason + "," +txtCTC1 + "," +txtExperience1;
                    controlList = spanlist + controlList + "," + reason;
                }
                else if (HeaderText == "Replace MRF") {
                    var reason = $('<%=txtReason.ClientID %>').id
                    controlList = spanlist + controlList + "," + reason;
                }
                else {
                    controlList = spanlist + controlList;
                    //var controlList = spanlist + txtMusthaveskill +"," + txtGoodtohaveskill +","+txtTools + ","+ txtExperience+","+ txtHeightQualification+","+ txtSoftSkill+","+ txtUtilization+","+ txtBilling+","+ txtCTC+","+ txtRemarks+","+ txtResponsiblePerson+","+ txtResourceResponsibility +"," + txtCTC1 + "," +txtExperience1;;
                }

            }

            if (ValidateControlOnClickEvent(controlList) == false) {
                lblMandatory = $('<%=lblMandatory.ClientID %>')
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
            }
            if (HeaderText == "Abort MRF") {
                var ReasonValue = $('<%=txtReason.ClientID %>').value;
                if (ReasonValue != "") {
                    return fnConfirm();
                }
            }
            if (HeaderText == "Replace MRF") {
                if (ValidateControlOnClickEvent(controlList) == true) {
                    return ValidateDateForRaiseMRF();
                }
                else {
                    return ValidateControlOnClickEvent(controlList);
                }
            }
            else {
                return ValidateControlOnClickEvent(controlList);
            }
        }

        function RestrictDateTypingAndPaste(targetControl) {
            if (targetControl.value != "") {
                targetControl.value = "";
            }
        }


        function checkUtilization() {

            var txtUtilization = $('<%=txtUtilijation.ClientID %>').value
            if (parseInt(txtUtilization) > 100) {
                alert("Utilization cannot be more then 100%.");
                $('<%=txtUtilijation.ClientID %>').value = "";
                $('<%=txtUtilijation.ClientID %>').focus();
                return false;
            }
            else {
                return ValidateControl($('<%=txtUtilijation.ClientID %>').id, $('<%=imgUtilization.ClientID %>').id, 'IsNumeric');
            }
        }

        function checkBilling() {

            var txtBilling = $('<%=txtBilling.ClientID %>').value
            if (parseInt(txtBilling) > 100) {
                alert("Billing cannot be more then 100%");
                $('<%=txtBilling.ClientID %>').value = "";
                $('<%=txtBilling.ClientID %>').focus();
                return false;
            }
            else {
                return ValidateBillingControl($('<%=txtBilling.ClientID %>').id, $('<%=imgBilling.ClientID %>').id, 'IsNumeric');
            }
        }

        // 32182-Ambar-30122011-Start
        var ctcflag = true;
        // 32182-Ambar-30122011-End

        function checkCTC1() {
            //          debugger;
            var txtCTC1 = $('<%=txtTargetCTC1.ClientID %>').value
            var txtCTC = $('<%=txtTargetCTC.ClientID %>').value
            var txtCTCID = $('<%=txtTargetCTC.ClientID %>').id
            // 32182-Ambar-30122011
            // Added condition for validation
            if (ctcflag == true) {
                if (txtCTC == "") {
                    alert("CTC From cannot be blank");
                    $('<%=txtTargetCTC1.ClientID %>').value = "";
                    $('<%=txtTargetCTC.ClientID %>').focus();
                    return false;
                }
                else if (parseInt(txtCTC) > parseInt(txtCTC1)) {
                    alert("CTC From cannot be more then CTC To");
                    $('<%=txtTargetCTC1.ClientID %>').value = "";
                    $('<%=txtTargetCTC.ClientID %>').value = "";
                    $('<%=txtTargetCTC.ClientID %>').focus();
                    return false;
                }
                else {
                    return ValidateControl($('<%=txtTargetCTC.ClientID %>').id, $('<%=imgCTC.ClientID %>').id, 'Decimal');
                }
            }
            //32182-Ambar-30122011            
            else {
                ctcflag = true;
            }
        }


        //32182-Ambar-Start-30122011
        function checkCTC2() {
            //              debugger;
            var txtCTC1 = $('<%=txtTargetCTC1.ClientID %>').value
            var txtCTC = $('<%=txtTargetCTC.ClientID %>').value
            var txtCTCID = $('<%=txtTargetCTC.ClientID %>').id
            if (txtCTC1 > 0 && txtCTC > 0) {
                if (parseInt(txtCTC) > parseInt(txtCTC1)) {
                    alert("CTC From cannot be more then CTC To");
                    $('<%=txtTargetCTC1.ClientID %>').value = "";
                    $('<%=txtTargetCTC.ClientID %>').value = "";
                    $('<%=txtTargetCTC.ClientID %>').focus();
                    ctcflag = false;
                    return false;
                }
            }
            else {
                return ValidateControl($('<%=txtTargetCTC.ClientID %>').id, $('<%=imgCTC.ClientID %>').id, 'Decimal');
            }
        }
        //32182-Ambar-End-30122011

        function checkExperience1() {

            var txtExperience1 = $('<%=txtExperince1.ClientID %>').value
            var txtExperience = $('<%=txtExperince.ClientID %>').value
            var txtExperienceID = $('<%=txtExperince.ClientID %>').id
            if (txtExperience == "") {
                alert("Experience From cannot be blank");
                $('<%=txtExperince1.ClientID %>').value = "";
                $('<%=txtExperince.ClientID %>').focus();
                return false;
            }
            else if (parseInt(txtExperience) > parseInt(txtExperience1)) {
                alert("Experience From cannot be more then Experience To");
                $('<%=txtExperince1.ClientID %>').value = "";
                $('<%=txtExperince.ClientID %>').value = "";
                $('<%=txtExperince.ClientID %>').focus();
                return false;
            }
            else {
                return ValidateControl($('<%=txtExperince1.ClientID %>').id, $('<%=imgExperience.ClientID %>').id, 'Decimal');
            }
        }

        function MultiLineTextBoxCheckReason(controlobj, maxlength, imgobj) {

            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            else {
                return ValidateControl($('<%=txtReason.ClientID %>').id, $('<%=imgReason.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            }

        }

        function MultiLineTextBoxCheckMustHave(controlobj, maxlength, imgobj) {

            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            //         else
            //         {
            //            return ValidateControl($( '<%=txtMustHaveSkills.ClientID %>').id,$( '<%=imgMustHaveSkills.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            //         }

        }
        function MultiLineTextBoxCheckGoodHave(controlobj, maxlength, imgobj) {

            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            //         else
            //         {
            //            return ValidateControl($( '<%=txtGoodSkills.ClientID %>').id,$( '<%=imgGoodSkills.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            //         }

        }

        function MultiLineTextBoxCheckTools(controlobj, maxlength, imgobj) {

            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            //         else
            //         {
            //            return ValidateControl($( '<%=txtTools.ClientID %>').id,$( '<%=imgTools.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            //         }

        }

        function MultiLineTextBoxCheckSoftSkill(controlobj, maxlength, imgobj) {

            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            else {
                return ValidateControl($('<%=txtSoftSkills.ClientID %>').id, $('<%=imgSoftSkill.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            }

        }

        function MultiLineTextBoxCheckRemarks(controlobj, maxlength, imgobj) {

            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            else {
                return ValidateControl($('<%=txtRemarks.ClientID %>').id, $('<%=imgRemarks.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            }

        }

        function MultiLineTextBoxCheckResponsibility(controlobj, maxlength, imgobj) {

            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            //         else
            //         {
            //            return ValidateControl($( '<%=txtResourceResponsibility.ClientID %>').id,$( '<%=imgResourceResponsibility.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            //         }

        }

        function ValidateDateForRaiseMRF() {

            var locRequiredFromDate = $('<%=txtRequiredFrom.ClientID %>').value;
            var locRequiredTillDate = $('<%=txtRequiredTill.ClientID %>').value;
            var locActualProjectStartDate = $('<%=hidProjectStartDate.ClientID %>').value;
            var locActualProjectEndDate = $('<%=hidProjectEndDate.ClientID %>').value;
            var oldRequiredFromDate = $('<%=hidOldRequiredFromDate.ClientID %>').value;
            var oldRequiredTillDate = $('<%=hidOldRequiredTillDate.ClientID %>').value;
            var Department = $('<%=ddlDepartment.ClientID %>').value;
            //Compare Required From greater then Required Till Date 
            if (compareDates(locRequiredFromDate, 'dd/MM/yyyy', locRequiredTillDate, 'dd/MM/yyyy') == 1) {

                lblMandatory = $('<%=lblMandatory.ClientID %>')
                lblMandatory.innerText = "Please select the Required From Date lesser then Required Till Date.";
                lblMandatory.style.color = "Red";
                return false;
            }
            if (compareDates(oldRequiredFromDate, 'dd/MM/yyyy', locRequiredFromDate, 'dd/MM/yyyy') == 1) {

                lblMandatory = $('<%=lblMandatory.ClientID %>')
                lblMandatory.innerText = "Replaced MRF's Required from Date will be greater than or equal to Required from Date of Old MRF " + oldRequiredFromDate;
                lblMandatory.style.color = "Red";
                return false;
            }
            if (Department == 1) {
                if (compareDates(locRequiredTillDate, 'dd/MM/yyyy', locActualProjectEndDate, 'dd/MM/yyyy') == 1) {
                    lblMandatory = $('<%=lblMandatory.ClientID %>')
                    lblMandatory.innerText = "Please select the Required Till date lesser or equal to then project end date " + locActualProjectEndDate;
                    lblMandatory.style.color = "Red";
                    return false;
                }
            }

            return true;
        }
        function ValidateCheckBox(chkid) {
            //debugger;            
            var CheckBoxID = $(chkid).checked;
            var lblNOOfResources = $('<%=hidNoOfResources.ClientID %>').value;
            if (lblNOOfResources == "") {
                var number = 0;
            }
            else {
                var number = parseInt(lblNOOfResources);
            }

            if (CheckBoxID == true) {
                number = number + 1;
            }
            else {
                number = number - 1;
            }
            if (number == 0) {
                $('<%=hidNoOfResources.ClientID %>').value = "";
            }
            else {
                if (number == 1) {
                    $('<%=hidNoOfResources.ClientID %>').value = number;
                }
                else if (number > 1) {
                    alert("Only one role can be selected.");
                    $(chkid).checked = false;
                }
            }
        }
    </script>

</asp:Content>
