<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="RecruitmentPipelineDetails.aspx.cs" Inherits="RecruitmentPipelineDetails"
    Title="Resource Management" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <asp:Panel ID="pnlHeaderAddPipelineDetails" runat="server" Visible="true" Width="100%">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height: 27px">
            <tr>
                <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                    <%--Sanju:Issue Id 50201:End--%>
                    <span class="header">
                        <asp:Label ID="lblHeaderPipelineDetails" runat="server"></asp:Label></span>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
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
    <table width="100%" border="0" class=" detailsbg">
        <tr>
            <td style="width: 15%">
                <%--<label class="mandatorymark">
                    *</label></td>--%>
                <asp:Label ID="lblMrfDetails" runat="server" Text="MRF Details" CssClass="detailsheader"></asp:Label>
            </td>
            <%--                    <asp:Button ID="btnDelete" runat="server" Text="Candidate Not Joined" Width="150px" CssClass="button"
                        TabIndex="4" OnClick="btnDelete_Click" Visible="true" />--%>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td align="left" style="width: 13%; height: 30px;">
                <asp:Label ID="lblMrfCode" Text="MRF Code" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 2%; height: 30px;">
                <span id="span2" runat="server">
                    <img id="img2" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="height: 30px;" colspan="4">
                <span id="spanzMRFCode" runat="server">
                    <asp:DropDownList ID="ddlMRFCode" runat="server" Width="500px" AutoPostBack="true"
                        ToolTip="Select MRF Code" TabIndex="1" OnSelectedIndexChanged="ddlMRFCode_SelectedIndexChanged"
                        CssClass="mandatoryField">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnEditMRFCode" runat="server" Text="EditMRFCode" CssClass="button"
                        Visible="false" OnClick="btnEditMRFCode_Click" />
                    <asp:HiddenField ID="hdMrfCode" runat="server" />
                </span>
            </td>
            <tr>
                <td style="width: 13%">
                </td>
                <td style="width: 2%">
                </td>
                <td style="width: 23%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 2%">
                </td>
                <td align="left" width="20%">
                    <asp:TextBox ID="txtDepartment" runat="server" ToolTip="Department Name" Width="208px"
                        Enabled="False" Visible="false" CssClass="mandatoryField"></asp:TextBox>
                </td>
            </tr>
        </tr>
        <tr id="rowProjClientDetails" runat="server">
            <td align="left" style="width: 13%">
                <asp:Label ID="lblPrjName" Text="Project Name" runat="server" CssClass="textstyle"></asp:Label>
                <%--<label class="mandatorymark" id="MandatoryProjectName" runat="server">
                    *</label>--%>
            </td>
            <td style="width: 2%">
                <span id="spanProjectName" runat="server">
                    <img id="imgNoOfResources" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 23%">
                <asp:TextBox ID="txtProjectName" runat="server" ToolTip="Project Name" Width="206px"
                    Enabled="False" CssClass="mandatoryField"></asp:TextBox>
            </td>
            <td align="left" style="width: 10%">
                <asp:Label ID="lblClientName" Text="Client Name" runat="server" CssClass="textstyle"></asp:Label>
                <%--<label class="mandatorymark" id="MandatoryClientName" runat="server">
                    *</label>--%>
            </td>
            <td style="width: 3%">
                <span id="span1" runat="server">
                    <img id="img1" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" width="20%">
                <asp:TextBox ID="txtXClientName" runat="server" ToolTip="Select Client Name" Width="206px"
                    Enabled="False" CssClass="mandatoryField"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" class=" detailsbg">
        <tr>
            <td>
                <asp:Label ID="lblResourceDetails" runat="server" Text="Resource Details" CssClass="detailsheader"
                    BorderStyle="None"></asp:Label>
            </td>
            <%--<label class="mandatorymark">
                    *</label></td>--%>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td align="left" style="width: 15%">
                <asp:Label ID="lblPrefix" Text="Prefix" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="span4" runat="server">
                    <img id="img4" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 27%">
                <span id="spanzPrefix" runat="server">
                    <asp:DropDownList ID="ddlPrefix" runat="server" Width="210px" ToolTip="Select Prefix"
                        TabIndex="3">
                    </asp:DropDownList>
                </span>
            </td>
            <td align="left" style="width: 12%">
                <asp:Label ID="lblFirstName" Text="First Name" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="spanFirtstName" runat="server">
                    <img id="imgFirstName" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" width="20%">
                <asp:TextBox ID="txtFirstName" runat="server" ToolTip="Enter First Name" TabIndex="3"
                    Width="206px" MaxLength="50" CssClass="mandatoryField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 15%">
                <asp:Label ID="lblMiddleName" Text="Middle Name" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                </label>
            </td>
            <td style="width: 3%" align="right">
                <span id="spanMiddleName" runat="server">
                    <img id="imgMiddleName" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 27%">
                <asp:TextBox ID="txtMiddleName" runat="server" ToolTip="Enter the Middle Name" Width="205px"
                    MaxLength="50" TabIndex="4"></asp:TextBox>
            </td>
            <td align="left" style="width: 12%">
                <asp:Label ID="lblLastName" Text="Last Name" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="spanLastName" runat="server">
                    <img id="imgLastName" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" width="20%">
                <asp:TextBox ID="txtLastName" runat="server" ToolTip="Enter Last Name" Width="205px"
                    MaxLength="50" TabIndex="5" CssClass="mandatoryField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 15%">
                <asp:Label ID="lblDepartment" Text="Department" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="span3" runat="server">
                    <img id="img3" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 27%">
                <span id="spanzDepartment" runat="server">
                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="210px" ToolTip="Select Department"
                        AutoPostBack="true" TabIndex="6" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                        CssClass="mandatoryField">
                    </asp:DropDownList>
                </span>
            </td>
            <td align="left" style="width: 12%">
                <asp:Label ID="lblDegignation" Text="Designation" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="span9" runat="server">
                    <img id="img8" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 23%">
                <span id="spanzDesignation" runat="server">
                    <asp:DropDownList ID="ddlDesignation" runat="server" Width="210px" ToolTip="Select Designation"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged"
                        TabIndex="7">
                        <asp:ListItem Text="SELECT" Value="0" />
                    </asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 15%">
                <asp:Label ID="lblEDtfJoining" Text="Expected Date of joining" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="spanExpectedDateOfJoining" runat="server">
                    <img id="imgExpectedDateOfJoiningError" runat="server" src="Images/cross.png" alt=""
                        style="display: none; border: none;" />
                </span>
            </td>
            <td align="left" style="width: 27%">
                <uc1:DatePicker ID="ucDatePicker" runat="server" TabIndex="8" />
            </td>
            <td align="left" style="width: 12%">
                <asp:Label ID="lblBand" Text="Band" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="span11" runat="server">
                    <img id="img9" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 23%">
                <span id="spanzBand" runat="server">
                    <asp:DropDownList ID="ddlBand" runat="server" Width="210px" ToolTip="Select Band"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlBand_SelectedIndexChanged" TabIndex="9"
                        CssClass="mandatoryField">
                    </asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 15%">
                <asp:Label ID="lblLocation" Text="Location" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *
                </label>
            </td>
            <td style="width: 3%" align="right">
                <span id="spanLocation" runat="server">
                    <img id="img5" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 27%">
                <span id="spanzLocation" runat="server">
                    <asp:DropDownList ID="ddlLocation" runat="server" Width="210px" ToolTip="Select Location"
                        TabIndex="12" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </span>
            </td>
            <td align="left" style="width: 12%" valign="top">
                <asp:Label ID="lblEmpType" Text="Employee Type" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td valign="top" align="right" style="width: 3%">
                <span id="spanEmpType" runat="server">
                    <img id="imgEmpType" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" width="20%" valign="top">
                <span id="spanzEmpType" runat="server">
                    <asp:DropDownList ID="ddlEmpType" runat="server" Width="210px" ToolTip="Select Employee Type"
                        TabIndex="11" CssClass="mandatoryField">
                    </asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 15%">
            </td>
            <td style="width: 3%">
            </td>
            <td align="left" style="width: 27%">
                <span id="span5" runat="server">
                    <asp:TextBox ID="txtLocation" Visible="false" runat="server" Width="205px" ToolTip="Enter any other location"
                        TabIndex="6"></asp:TextBox>
                </span>
            </td>
            <td align="left" style="width: 12%">
            </td>
            <td style="width: 3%">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblPhoneNo" runat="server" Text="Mobile No"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%" align="right">
                <span id="spanzPhoneNo" runat="server">
                    <img id="imgPhoneNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 27%">
                <span id="spanPhoneNo" runat="server">
                  <%--  Sanju:Issue Id 50201: Changed OnlyInt function which do validation on numbers--%>
                    <asp:TextBox ID="tbPhoneNo" runat="server" Width="205px" ToolTip="Enter Adress of Contact Phone No."
                        TabIndex="14" MaxLength="10" onkeypress="return OnlyInt(event);">
                     <%--   Sanju:Issue Id 50201:End--%>
                        </asp:TextBox>
                </span>
            </td>
            <td style="width: 12%">
                <asp:Label ID="Label6" Text="Accountable To" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="spanReportingTo" runat="server">
                    <img id="imgReportingTo" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td>
                <asp:TextBox ID="txtReportingTo" runat="server" MaxLength="100" BorderStyle="NotSet"
                    AutoComplete="off" Width="205px" ToolTip="Enter Accountable To" Height="40px"
                    Enabled="false" CssClass="mandatoryField" TabIndex="13"></asp:TextBox>
                <img id="imgResponsiblePersonSearch" runat="server" src="Images/find.png" alt=""
                    tabindex="13" />
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblExternalWorkExp" runat="server" Text="External Work Experience"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
            </td>
            <td align="left" style="width: 27%">
                <span id="span6" runat="server">
                    <asp:TextBox ID="txtReleventYears" runat="server" CssClass="mandatoryField" Width="35px"
                        TabIndex="16"></asp:TextBox>&nbsp;Years <span id="spanReleventYears" runat="server">
                            <img id="imgReleventYears" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    <asp:TextBox ID="txtReleventMonths" runat="server" CssClass="mandatoryField" Width="35px"
                        TabIndex="17"></asp:TextBox>&nbsp;Months <span id="spanReleventMonths" runat="server">
                            <img id="imgReleventMonths" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span></span>
            </td>
            <td style="width: 12%">
                <asp:Label ID="lblResourceBussinesUnit" runat="server" Text="Resource Business Unit"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="span" runat="server">
                    <img id="img6" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td>
                <span id="spanzResourceBussinesUnit" runat="server">
                    <asp:DropDownList ID="ddlResourceBussinesUnit" runat="server" TabIndex="15" Width="210px">
                    </asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                <asp:Label ID="lblLandlineNo" runat="server" Text="Landline No"></asp:Label>
            </td>
            <td style="width: 3%">
                <span id="spanzLandlineNo" runat="server">
                    <img id="imgLandlineNo" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 27%">
                <span id="spanLandlineNo" runat="server">
              <%--  Sanju:Issue Id 50201: Changed OnlyInt function which do validation on numbers--%>
                    <asp:TextBox ID="txtLandlineNo" runat="server" Width="205px" ToolTip="Enter Landline No."
                        TabIndex="19" MaxLength="10" onkeypress="return OnlyInt(event);">
                <%--        Sanju:Issue Id 50201:End--%>
                        </asp:TextBox>
                </span>
            </td>
            <td style="width: 12%">
                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
            </td>
            <td>
                <asp:TextBox ID="tbAdress" runat="server" Width="205px" MaxLength="500" TextMode="MultiLine"
                    ToolTip="Enter Adress of Contact Phone No." TabIndex="18"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 15%">
                <asp:Label ID="LblOfferAcceptedDt" Text="Offer Accepted Date" runat="server" CssClass="textstyle"></asp:Label>
                <label class="mandatorymark">
                    *</label>
            </td>
            <td style="width: 3%">
                <span id="spanOfferAcceptedDt" runat="server">
                    <img id="imgOfferAcceptedDt" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td align="left" style="width: 27%">
                <uc1:DatePicker ID="DatePickerOfferAcceptedDt" runat="server" TabIndex="21" />
            </td>
            <td style="width: 12%">
                <asp:Label ID="lblEmailID" runat="server" Text="Email ID"></asp:Label>
                <asp:Label class="mandatorymark" ID="CandidateEmailIdMandatoryMark" Visible="false"
                    runat="server">
                    *</asp:Label>
            </td>
            <td style="width: 3%">
                &nbsp;
            </td>
            <td>
                <span id="spanCandidateEmailId" runat="server">
                    <asp:TextBox ID="txtEmailId" runat="server" ToolTip="Enter Email ID" TabIndex="20"
                        Width="206px" MaxLength="100" CssClass="mandatoryField" TextMode="MultiLine"
                        onblur="return validateEmailId(this.value)">
                    </asp:TextBox>
                    <img id="imgCandidateEmailID" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 15%">
                <asp:Label ID="lblPurpose" Text="Purpose" runat="server" CssClass="textstyle"></asp:Label>
            </td>
            <td style="width: 3%">
            </td>
            <td align="left" style="width: 27%">
                <asp:TextBox ID="txtPurpose" Text="" runat="server" ReadOnly="True" ToolTip="Purpose for MRF"
                    Enabled="False" Width="205px" TabIndex="22"></asp:TextBox>
            </td>
            <%--Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
            Desc : Traninig for new joining employee. (Training Gaps).--%>
            <td>
                <asp:CheckBox ID="chkTrainingRequired" Text="Training Required" runat="server" Visible="true" />
            </td>
            <td style="width: 3%">
                &nbsp;
            </td>
            <td>
                <span id="spanSkill" runat="server">
                    <asp:TextBox ID="txtTrainingSubject" runat="server" ToolTip="Enter Training Subject" TabIndex="23"
                        Width="206px" MaxLength="100" CssClass="mandatoryField" TextMode="MultiLine" style="display:none"
                        >
                    </asp:TextBox>
                    <img id="imgTraining" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <%-- Rajan Kumar : Issue 39508: 31/01/2014 : END--%>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblContractDuration" Text="Contract Duration (months)" runat="server"
                    Visible="False"></asp:Label>
            </td>
            <td style="width: 3%">
                <span id="spanPurpose" runat="server">
                    <img id="imgPurpose" runat="server" src="Images/cross.png" alt="" style="display: none;
                        border: none;" />
                </span>
            </td>
            <td>
                <asp:TextBox ID="txtContractDuration" Text="" runat="server" ReadOnly="True" ToolTip="Contract Duration for Consultant Depts"
                    Visible="False" Width="205px" TabIndex="22"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 2%" colspan="6">
                <asp:HiddenField ID="hidResponsiblePerson" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlJoiningDetails" runat="server" Visible="false" Width="100%">
        <table width="100%" border="0">
            <tr>
                <td align="left" style="width: 12%; height: 30px;">
                    <asp:Label ID="Label3" runat="server" Text="Joining Details" CssClass="detailsheader"></asp:Label>
                </td>
                <td style="width: 12%; height: 30px;">
                </td>
                <td align="left" style="width: 25%; height: 30px;">
                </td>
                <td align="left" style="width: 7%; height: 30px;">
                </td>
                <td style="width: 1%; height: 30px;">
                </td>
                <td align="left" width="20%" style="height: 30px">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 12%; height: 30px;">
                    <asp:Label ID="lblResourceJoined" runat="server" Text="Resource Joined"></asp:Label>
                </td>
                <td style="width: 12%; height: 30px;">
                </td>
                <td align="left" style="width: 25%; height: 30px;">
                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_JoinedDate"
                        TabIndex="23" />
                </td>
                <td align="left" style="width: 20%; height: 30px;">
                </td>
                <td style="width: 10%; height: 30px;">
                </td>
                <td align="left" width="20%" style="height: 30px">
                </td>
            </tr>
            <asp:Panel ID="pnlJoiningDate" runat="server" Visible="false" Width="100%">
                <tr>
                    <td align="left" style="width: 200px; height: 30px;">
                        <asp:Label ID="Label5" runat="server" Text="Joining Date"></asp:Label>
                    </td>
                    <td style="width: 20px; height: 30px;">
                        <span id="spanzJoiningDate" runat="server">
                            <img id="imgJoiningDate" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </span>
                    </td>
                    <td align="left" style="width: 300px; height: 30px;">
                        <uc1:DatePicker ID="DatePickerCandidateJoined" runat="server" onblur="return ButtonJoiningDateValidate_Employee(this.value)" />
                    </td>
                    <td align="left" style="width: 100px; height: 30px;">
                    </td>
                    <td style="width: 100px; height: 30px;">
                    </td>
                    <td align="left" width="400px" style="height: 30px">
                    </td>
                </tr>
            </asp:Panel>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlAdd" runat="server" Visible="true" Width="100%">
        <table width="100%">
            <tr>
                <td align="left" style="width: 50%">
                </td>
                <td width="20%">
                    <%--  Issue Id : 34230 CONCURRENCY HANDLED--%>
                    <%-- Added OnClientClick="return ButtonClickValidate(this)"--%>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="120px" CssClass="button"
                        OnClientClick="return ButtonClickValidate(this)" TabIndex="25" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                    <asp:HiddenField runat="server" ID="hidToken" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="120px" CssClass="button"
                        TabIndex="26" OnClick="btnSubmitCancel_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlViewRecruitment" runat="server" Visible="false" Width="100%" Style="margin-bottom: 0px">
        <table style="width: 100%">
            <tr>
                <td style="width: 463px; margin-left: 40px">
                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="button" Width="118px"
                        Visible="true" TabIndex="27" OnClick="btnPrevious_Click" />
                    &nbsp
                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="button" Width="118px"
                        Visible="true" TabIndex="28" OnClick="btnNext_Click" />
                </td>
                <td style="width: 463px; margin-left: 130px">
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="118px" CssClass="button"
                        TabIndex="29" Visible="true" OnClick="btnEdit_Click" />
                    &nbsp
                    <%--                    <asp:Button ID="btnDelete" runat="server" Text="Candidate Not Joined" Width="150px" CssClass="button"
                        TabIndex="4" OnClick="btnDelete_Click" Visible="true" />--%>
                    &nbsp
                </td>
                <td style="width: 463px" align="right">
                    <asp:Button ID="btnCancelView" runat="server" Text="Cancel" Width="121px" CssClass="button"
                        TabIndex="30" OnClick="btnCancel_Click" />
                    <asp:HiddenField ID="hidMRFID" runat="server" />
                    <asp:HiddenField ID="hidResponsiblePersonName" runat="server" />
                    <asp:HiddenField ID="hidMrfStatus" runat="server" />
                    <%--Mohamed : Issue 48476 : 29/01/2014 : Starts                        			  
                    Desc : Assigned 2 Resource for 1 MRF _ Vinayak Narkar -- for Edit Pipeline details --%>
                    <asp:HiddenField ID="hidCandidateId" runat="server" />
                    <%--Mohamed : Issue 48476 : 29/01/2014 : Ends--%>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteReason" runat="server" Visible="false" Width="100%" Height="50px">
        <table width="100%" border="0">
            <tr>
                <td style="width: 16%">
                    <asp:Label ID="lblReson" runat="server" Text="Reason" Visible="true" CssClass="textstyle"></asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td align="right" valign="top" style="width: 5%">
                    <span id="spanReason" runat="server">
                        <img id="imgReason" runat="server" src="Images/cross.png" alt="" style="display: none;
                            border: none;" />
                    </span>
                </td>
                <td width="23%" colspan="3">
                    <asp:TextBox ID="txtReason" TextMode="MultiLine" runat="server" Height="50px" Width="208px"
                        onkeypress="return isNumberKey(event)" ToolTip="Enter reason for deletion" TabIndex="24"
                        MaxLength="500" CssClass="mandatoryField"></asp:TextBox>
                </td>
                <td width="48%" colspan="2">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDelete" runat="server" Visible="false" Width="100%">
        <table width="100%">
            <tr>
                <td align="left" style="width: 50%">
                </td>
                <td width="20%">
                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Confirm" Width="120px" CssClass="button"
                        TabIndex="31" OnClick="btnConfirmDelete_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" Width="120px" CssClass="button"
                        TabIndex="32" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlUpdate" runat="server" Visible="false" Width="100%">
        <table width="100%">
            <tr>
                <td align="left" style="width: 50%">
                </td>
                <td width="20%">
                    <asp:Button ID="btnSave" runat="server" Text="Confirm" Width="120px" CssClass="button"
                        TabIndex="33" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnSaveCancel" runat="server" Text="Cancel" Width="120px" CssClass="button"
                        TabIndex="34" OnClick="btnCancel_Click"/>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfExpectectedDOJ" runat="server" />
        <asp:HiddenField ID="hfDesignationId" runat="server" />
        <!--Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
        Desc : Expected Joinee's details edited[MRF Code: MRF_Testing_SrTstA_0387] old  Joining date is default date - Mail for de-linking MRF.-->
        <asp:HiddenField ID="hdOldMRFId" runat="server" />
        <!--Mohamed : Issue 50306 : 09/09/2014 : Ends-->
    </asp:Panel>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinny.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var retVal;
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        retVal = e.data;
                    }
                });

                $('#ctl00_cphMainContent_btnSave').click(function() {
                if (ButtonClickValidate()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                    }

                });

                $('#ctl00_cphMainContent_btnSubmit').click(function() {
                if (ButtonClickValidate()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                    }

                });

                //                $(window).on('beforeunload', function() {
                //                    var button1 = $('#ctl00_cphMainContent_btnSave');
                //                    var button2 = $('#ctl00_cphMainContent_btnSubmit');

                //                    button2.attr('disabled', 'disabled');
                //                    button2.val('Please Wait ..');
                //                    setTimeout(function() {
                //                        button2.removeAttr('disabled');
                //                    }, 20000);

                //                    button1.attr('disabled', 'disabled');
                //                    button1.val('Please Wait ..');
                //                    setTimeout(function() {
                //                        button1.removeAttr('disabled');
                //                    }, 20000);
                //                });


            });
        })(jQuery);


        function popUpEmployeeSearch() {
            jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
                onclose: function(e) {
                    var txtResponsiblePerson = jQuery('#<%=txtReportingTo.ClientID %>');
                    var hidResponsiblePerson = jQuery('<#%=hidResponsiblePerson.ClientID %>');

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
                    }
                }
            }).open();
        }
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        setbgToTab('ctl00_tabRecruitment', 'ctl00_spanPipelineDetails');

        function $(v) { return document.getElementById(v); }

        function IsNumeric(event, sText) {
            var ValidChars = "0123456789.,";
            var IsNumber = true;
            var Char;


            for (i = 0; i < sText.length && IsNumber == true; i++) {
                Char = sText.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    IsNumber = false;
                }
            }
            return IsNumber;

        }

        function ButtonDeleteClickValidate() {
            var lblMandatory = $('<%=lblMandatory.ClientID %>');
            var controlList = $('<%=txtReason.ClientID %>').id;
            var Reject = $('<%=txtReason.ClientID %>').value;
            var txtReject = $('<%=txtReason.ClientID %>');
            if (trim(Reject) == "") {
                lblMandatory.innerText = "Please enter the reason for deletion.";
                lblMandatory.style.color = "Red";

                txtReject.style.borderStyle = "Solid";
                txtReject.style.borderWidth = "2";
                txtReject.style.borderColor = "Red";
                txtReject.focus();
                return false;
            }
            var ReasonValue = $('<%=txtReason.ClientID %>').value;
            if (ReasonValue != "") {
                return fnConfirmDelete();
            }

            if (ValidateControlOnClickEvent(controlList) == false) {

                return ValidateControlOnClickEvent(controlList);
            }


        }
        // Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
        // Desc : Traninig for new joining employee. (Training Gaps).
        function TrainingRequired() {
            if (document.getElementById("<% =chkTrainingRequired.ClientID %>").checked) {
                document.getElementById("<% =txtTrainingSubject.ClientID %>").style.display = 'inline';

            }
            else {
                document.getElementById("<% =txtTrainingSubject.ClientID %>").style.display = 'none';
                document.getElementById("<% =txtTrainingSubject.ClientID %>").value = "";
            }
        }
        // Rajan Kumar : Issue 39508: 31/01/2014 : END
        // Issue Id : 34230 CONCURRENCY HANDLED

        //function ButtonClickValidate() {
        function ButtonClickValidate(obj) {

            //  debugger;
            var controlList;
            var lblMandatory;
            var HeaderText = $('<%=lblHeaderPipelineDetails.ClientID %>').innerHTML;
            var lblMessage = $('<%=lblMessage.ClientID %>')
            lblMessage.innerText = "";
            var spanlist = "";
            var MRFCode = $('<%=ddlMRFCode.ClientID %>').value;

            if (MRFCode == "" || MRFCode == "SELECT") {
                var sMRFCode = $('<%=spanzMRFCode.ClientID %>').id;
                spanlist = sMRFCode + ",";
            }
            var Prefix = $('<%=ddlPrefix.ClientID %>').value;
            if (Prefix == "" || Prefix == "SELECT") {
                var sPrefix = $('<%=spanzPrefix.ClientID %>').id;
                spanlist = spanlist + sPrefix + ",";
            }
            var EmpType = $('<%=ddlEmpType.ClientID %>').value;
            if (EmpType == "" || EmpType == "SELECT") {
                var sEmpType = $('<%=spanzEmpType.ClientID %>').id;
                spanlist = spanlist + sEmpType + ",";
            }
            var Band = $('<%=ddlBand.ClientID %>').value;
            if (Band == "" || Band == "SELECT") {
                var sBand = $('<%=spanzBand.ClientID %>').id;
                spanlist = spanlist + sBand + ",";
            }
            //Issue resolved.
            //Description :Changed name to spanzDesignation as the diffrence in name
            //and the value was not appropriate so it was not highlighting the Span.
            var Designation = $('<%=ddlDesignation.ClientID %>').value;
            //var Designation= $('<%=ddlDesignation.ClientID %>').options[$('<%=ddlLocation.ClientID %>').selectedIndex].text;
            if (Designation == "" || Designation == "SELECT") {
                var sDesignation = $('<%=spanzDesignation.ClientID %>').id;
                spanlist = spanlist + sDesignation + ",";
            }

            var Location = $('<%=ddlLocation.ClientID %>').options[$('<%=ddlLocation.ClientID %>').selectedIndex].text;
            if (Location == "" || Location == "SELECT") {
                var sLocation = $('<%=spanzLocation.ClientID %>').id;
                spanlist = spanlist + sLocation + ",";
            }
            if (Location == "Other Location") {
                var otherLocation = $('<%=txtLocation.ClientID %>').id;
            }

            var firstName = $('<%=txtFirstName.ClientID %>').id;
            var lastName = $('<%=txtLastName.ClientID %>').id;
            var reportingTo = $('<%=txtReportingTo.ClientID %>').id;
            var expectedDateOfJoining = $('<%=ucDatePicker.ClientID %>').id;
            var OfferAcceptedDate = $('<%=DatePickerOfferAcceptedDt.ClientID %>').id;
            var PhoneNo = $('<%=tbPhoneNo.ClientID %>').id;
            var Address = $('<%=tbAdress.ClientID %>').id;
            var ReleventYears = $('<%=txtReleventYears.ClientID %>').id;
            var ReleventMonths = $('<%=txtReleventMonths.ClientID %>').id;

            //Poonam : 54913 : 1/09/2015 : Starts
            //Desc : Validation to Experience in Years and Months	

            var ExperienceYears = $('<%=txtReleventYears.ClientID %>').value;
            var ExperienceMonths = $('<%=txtReleventMonths.ClientID %>').value;

            if (ExperienceYears > 100) {
                lblMessage.style.color = "RED";
                lblMessage.innerHTML = "Experience in Years should be less than 100";
                return false;
            }
            if (ExperienceMonths >= 12) {
                lblMessage.style.color = "RED";
                lblMessage.innerHTML = "Experience in Months should be less than 12";
                return false;
            }
            //Poonam : 54913 : 1/09/2015 : Ends
            
            if (Location == "Other Location") {

                controlList = $('<%=ddlPrefix.ClientID %>').id + "," + $('<%=ddlEmpType.ClientID %>').id + "," + firstName + "," + lastName + "," + expectedDateOfJoining + "," + reportingTo + "," + otherLocation + "," + PhoneNo + "," + Address + "," + OfferAcceptedDate + "," + ReleventYears + "," + ReleventMonths + "," + spanlist;
            }
            else {

                controlList = $('<%=ddlPrefix.ClientID %>').id + "," + $('<%=ddlEmpType.ClientID %>').id + "," + firstName + "," + lastName + "," + expectedDateOfJoining + "," + reportingTo + "," + PhoneNo + "," + Address + "," + OfferAcceptedDate + "," + ReleventYears + "," + ReleventMonths + "," + spanlist;

            }
            //Inserted by kanchan for highlighting the newly added two fields.
            //Issue resolved.
            //Description: Also Adding highlights to the fields which are
            //marked mandatory but are not being highlighted on button submit click 
            var ResourceUnit = $('<%=ddlResourceBussinesUnit.ClientID %>').options[$('<%=ddlResourceBussinesUnit.ClientID %>').selectedIndex].text;
            var sRUnit = $('<%=spanzResourceBussinesUnit.ClientID %>').id;
            if (ResourceUnit == "" || ResourceUnit == "SELECT") {
                controlList += "," + sRUnit;
            }
//            if ($('<%=txtXClientName.ClientID %>') != null) {
//                var ClientName = $('<%=txtXClientName.ClientID %>').id;
//                var vClientName = $('<%=txtXClientName.ClientID %>').value;
//                if (vClientName == "") {
//                    controlList += "," + ClientName;
//                }

//                if ($('<%=txtProjectName.ClientID %>') != null) {
//                    var ProjectName = $('<%=txtProjectName.ClientID %>').id;
//                    var vProjectName = $('<%=txtProjectName.ClientID %>').value;
//                    if (vProjectName == "") {
//                        controlList += "," + ProjectName;
//                    }
//                }
//            }

            var Department = $('<%=ddlDepartment.ClientID %>').options[$('<%=ddlDepartment.ClientID %>').selectedIndex].text;
            var sDepartment = $('<%=spanzDepartment.ClientID %>').id;
            if (Department == "" || Department == "SELECT") {
                controlList += "," + sDepartment;
            }



            var sCandidateEmailId = $('<%=txtEmailId.ClientID %>').id;
            var CandidateEmailValue = $('<%=txtEmailId.ClientID %>').value;
            var DepartmentName = $('<%=ddlDepartment.ClientID %>').options[$('<%=ddlDepartment.ClientID %>').selectedIndex].text;

            if (DepartmentName == "RaveConsultant-India" ||
                DepartmentName == "RaveConsultant-USA" ||
                DepartmentName == "RaveConsultant-UK") {
                controlList += "," + sCandidateEmailId;
            }
            //var ChkTraningValue = $('<%=chkTrainingRequired.ClientID %>').value;
            var ChkTraningValue = document.getElementById('<%=chkTrainingRequired.ClientID %>');
            var TraningSubjectId = $('<%=txtTrainingSubject.ClientID %>').id;
            if (ChkTraningValue.checked) {
                controlList += "," + TraningSubjectId;
            }

            if (ValidateControlOnClickEvent(controlList) == false) {
                lblMandatory = $('<%=lblMandatory.ClientID %>')
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
            }

            //Removed condition for expected closer date as suggested by niharika bcz they want to enter details of 
            // previously joined employee.
            //        if(ValidateControlOnClickEvent(controlList) == true)
            //        {
            //            if(HeaderText== "Add Pipeline Details")
            //            return ValidateExpectedDate();
            //        }
            //        else
            //        {
            // Issue Id : 34230 STRAT CONCURRENCY HANDLED Mahendra
            // return ValidateControlOnClickEvent(controlList);
            var flag = ValidateControlOnClickEvent(controlList);
            if (flag == true) {
                return f(obj);
            }
            return flag;
            // Issue Id : 34230 END CONCURRENCY HANDLED Mahendra
            //        }
        }

        // Issue Id : 34230 STRAT CONCURRENCY HANDLED Mahendra
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
        // Issue Id : 34230 END CONCURRENCY HANDLED Mahendra




        function ButtonJoiningDateValidate() {
            //            debugger;
            var lblMandatory = $('<%=lblMandatory.ClientID %>');
            var controlList = $('<%=DatePickerCandidateJoined.ClientID %>').id;
            var DatePickerCandidateJoined = $('<%=DatePickerCandidateJoined.ClientID %>');
            if (ValidateControlOnClickEvent(controlList) == false) {
                lblMandatory.innerText = "Joining date is mandatory, as resource joined is checked, kindly enter the joining date.";
                lblMandatory.style.color = "Red";
                DatePickerCandidateJoined.style.borderStyle = "Solid";
                DatePickerCandidateJoined.style.borderWidth = "2";
                DatePickerCandidateJoined.style.borderColor = "Red";
                DatePickerCandidateJoined.focus();
                return ValidateControlOnClickEvent(controlList);
            }

            //            debugger;
            //            date CurrentDate = new Date();
            //            var Curr_date = CurrentDate.getDate + "/" + CurrentDate.getMonth + "/" + CurrentDate.getFullYear;            
            //            if (DatePickerCandidateJoined.value > Curr_date ) 
            //            {
            //                alert('Invalid dates');
            //                document.getElementById('<%=DatePickerCandidateJoined.ClientID %>').focus();
            //                return false;
            //            }

        }

        function CheckResourceJoinedDetails() {

            var controlList = $('<%=chkSelect.ClientID %>').id;
            if (ValidateControlOnClickEvent(controlList) == false) {
                return true;
            }

        }

        function fnConfirmDelete() {
            return confirm('Do you want to delete the candidate’s details?');

        }

        function Decimal(text) {

            var txt = parseInt(text);

            //if(txt == 0)
            //{
            //   return true;
            //}
            var TextValue = text.trim();
            if (TextValue > 0) {
                var regex = /^\d+(?:\.\d{0,2})?$/;
                //var RegExPattern = new RegExp("^[0-9]+([.][0-9]+)*$");
                if (regex.test(TextValue)) {
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }



        //validate CTC Value
        function ValidateCTCControlValue(controlobj, imgobj, functionName) {
            //  debugger;
            var bool;
            var controlName = document.getElementById(controlobj);
            if (imgobj != "") {
                var imgName = document.getElementById(imgobj);

                if (controlName.value == "") {
                    controlName.style.borderWidth = "1";
                    controlName.style.borderColor = "#7F9DB9";
                    imgName.style.display = "none";
                    imgName.alt = "";
                    bool = true;
                }
                else {
                    if (window[functionName](controlName.value)) {
                        imgName.style.display = "inline";

                        //Highlight Control
                        controlName.style.borderStyle = "Solid";
                        controlName.style.borderWidth = "2";
                        controlName.style.borderColor = "Red";
                        controlName.value = "";
                        bool = false;
                    }
                    else {
                        controlName.style.borderWidth = "1";
                        controlName.style.borderColor = "#7F9DB9";
                        imgName.style.display = "none";
                        imgName.alt = "";
                        bool = true;
                    }
                }
            }
            else {
                if (controlName.value == "") {

                    controlName.style.borderWidth = "1";
                    controlName.style.borderColor = "#7F9DB9";
                    bool = true;
                }

            }
            return bool;
        }

        //Function to allow only integer and Decimal point
        // restricted multiple decimal point and only two digit after Decimal.
        function IntWithDecimal(event, targetControl) {
            var Point = targetControl.value.split('.');
            var regExObj = new RegExp();
            regExObj = /^([1-9]{0,1})([0-9]{0,1})([0-9]{0,1})+((\.{0,1})([0-9]{0,1}))$/;

            if (event.keyCode == 46 || (event.keyCode >= 48 && event.keyCode <= 57)) {

                if (targetControl.value != '') {
                    //Condition to check multiple Decimal point.
                    if (Point.length == 2 && event.keyCode == 46) {
                        return false;
                    }
                    if (regExObj.test(targetControl.value, '')) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
            }
            else {
                return false;
            }
        }

        //This function allows only to enter numeric values.
        //  Sanju:Issue Id 50201: Changed OnlyInt function which do validation on numbers
        //In firefox it was not working
        function OnlyInt(key) {

            //            var Point = targetControl.value;
            //            var regExObj = new RegExp();
            //            if (event.keyCode >= 48 && event.keyCode <= 57) {
            //                return true;
            //            }
            //            else {
            //                return false;
            //            }

            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57)) {
                return false;
            }
            else return true;

        }


        //debugger
        //Check Emailid validation.
        function validateEmailId(emailId) {
            //debugger
            if (trim(emailId) != "") {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                if (reg.test(emailId) == false) {
                    alert('Invalid Email Id');

                    document.getElementById('<%=txtEmailId.ClientID %>').focus();

                    return false;
                }
            }
        }



        function ButtonJoiningDateValidate_Employee() {
            //            debugger;
            //            var DatePickerCandidateJoined = $('<%=DatePickerCandidateJoined.ClientID %>');
            //            var CurrentDate = new Date();
            //            if (DatePickerCandidateJoined > CurrentDate)
            //             {
            //                 alert('Invalid dates');
            //                 document.getElementById('<%=DatePickerCandidateJoined.ClientID %>').focus();
            //                 return false;
            //            }

        }


        function ButtonJoiningDateValidate1() {
            //            var lblMandatory = $('<%=lblMandatory.ClientID %>');
            //            var controlList = $('<%=DatePickerCandidateJoined.ClientID %>').id;
            //            var DatePickerCandidateJoined = $('<%=DatePickerCandidateJoined.ClientID %>');
            //            var CurrentDate = new Date();
            //             if (DatePickerCandidateJoined > CurrentDate)
            //               
            //            {
            //                lblMandatory.innerText = "chk doj.";
            //                lblMandatory.style.color = "Red";
            //                DatePickerCandidateJoined.style.borderStyle = "Solid";
            //                DatePickerCandidateJoined.style.borderWidth = "2";
            //                DatePickerCandidateJoined.style.borderColor = "Red";
            //                DatePickerCandidateJoined.focus();
            //                return ValidateControlOnClickEvent(controlList);
            //            }

        }



        function isNumberKey(evt) {

            var keycode = (evt.which) ? evt.which : event.keyCode
            var phn = document.getElementById('<%=txtReason.ClientID %>');
            //Condition to check textbox contains ten numbers or not
            if (phn.value.length < 500) {
                return true;
            }
            else {
                alert("Only 500 chars allowed");
                return false;
            }


        }

        

       
      
    </script>

</asp:Content>
