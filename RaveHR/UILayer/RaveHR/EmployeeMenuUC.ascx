<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeMenuUC.ascx.cs"
    Inherits="EmployeeMenuUC" %>
<link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />
<asp:Table ID="tblMain" runat="server" BorderColor="AliceBlue" BorderStyle="Solid"
    BorderWidth="2">
    <asp:TableRow ID="TableRow1" runat="server" VerticalAlign="Top" Height="5%">
        <asp:TableCell ID="tblHeaderCell" runat="server" Height="5%"><br /></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trEmployeeDetails" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcEmployeeDetails" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkEmployeeDetails" Text="Employee Details" CssClass="employeeMenu"
                OnClick="lnkEmployeeDetails_Click"></asp:LinkButton><br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trContactDetails" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcContactDetails" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkContactDetails" Text="Contact Details" CssClass="employeeMenu"
                OnClick="lnkContactDetails_Click"></asp:LinkButton>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trQualificationDetails" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcQualificationDetails" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkQualificationDetails" Text="Qualification Details"
                CssClass="employeeMenu" OnClick="lnkQualificationDetails_Click"></asp:LinkButton>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trCertificationDetails" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcCertificationDetails" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkCertificationDetails" Text="Certification Details"
                CssClass="employeeMenu" OnClick="lnkCertificationDetails_Click"></asp:LinkButton>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trEmployeeSkills" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcEmployeeSkills" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkSkillDetails" Text="Employee Skills" CssClass="employeeMenu"
                OnClick="lnkSkillDetails_Click"></asp:LinkButton>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trProfessionalCourses" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcProfessionalCourses" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkProfCourses" Text="Professional Courses" CssClass="employeeMenu"
                OnClick="lnkProfCourses_Click"></asp:LinkButton>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trProjectDetails" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcProjectDetails" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkProjectDetails" Text="Project Details" CssClass="employeeMenu"
                OnClick="lnkProjectDetails_Click"></asp:LinkButton><br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trExperienceSummary" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcExperienceSummary" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkExperienceSummary" Text="Experience Summary"
                CssClass="employeeMenu" OnClick="lnkExperienceSummary_Click"></asp:LinkButton><br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trPassportDetails" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcPassportDetails" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkPassportDetails" Text="Passport Details" CssClass="employeeMenu"
                OnClick="lnkPassportDetails_Click"></asp:LinkButton><br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trOtherDetails" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcOtherDetails" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkOtherDetails" Text="Other Details" CssClass="employeeMenu"
                OnClick="lnkOtherDetails_Click"></asp:LinkButton><br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trResignationDetails" runat="server" VerticalAlign="Top" Visible="false"><%--30212-Ambar-Modify to set default as visible false--%>
        <asp:TableCell ID="tcResignationDetails" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkResignationDetails" Text="Resignation Details"
                CssClass="employeeMenu" OnClick="lnkResignationDetails_Click"></asp:LinkButton><br />
        </asp:TableCell>
    </asp:TableRow>
     <asp:TableRow ID="trResumeTemplate" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcResumeTemplate" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkResumeTemplate" Text="Resume Template"
                CssClass="employeeMenu" OnClick="lnkResumeTemplate_Click">
                <asp:HyperLink ID="ResumeTemplate" runat="server"></asp:HyperLink>
                </asp:LinkButton><br />
        </asp:TableCell>
    </asp:TableRow>
      <asp:TableRow ID="trEmployeeResume" runat="server" VerticalAlign="Top">
        <asp:TableCell ID="tcEmployeeResume" runat="server" BorderColor="Beige" BorderWidth="1">
            <asp:LinkButton runat="server" ID="lnkResumeDetails" Text="Upload Resume"
                CssClass="employeeMenu" OnClick="lnkResumeDetails_Click">
                <asp:HyperLink ID="EMPLOYEERESUME" runat="server" ></asp:HyperLink>
                </asp:LinkButton><br />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow3" runat="server" VerticalAlign="Bottom">
        <asp:TableCell ID="tcellFooterIndex" runat="server" BorderColor="Beige" BorderWidth="1"
            VerticalAlign="Bottom">
            <asp:Button ID="btnPrevious" Text="Previous" runat="server" CssClass="button" Width="80px"
                OnClick="btnPrevious_Click" UseSubmitBehavior="false"/>
            <asp:Button ID="btnNext" Text="Next" runat="server" CssClass="button" Width="80px"
                OnClick="btnNext_Click" UseSubmitBehavior="false"/>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
