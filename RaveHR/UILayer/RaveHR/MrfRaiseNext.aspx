<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="MrfRaiseNext.aspx.cs" Inherits="MrfRaiseNext" Title="Resource Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height: 27px">
        <tr>
          <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
                        <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1')";>
             <%--   Sanju:Issue Id 50201:End--%>
                <span class="header">Raise MRF</span>
            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMessage" runat="server" CssClass="text"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 860px">
                <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
            </td>
        </tr>
        <tr height="10px">
            <td>
            </td>
        </tr>
        <%--Aarohi : Issue 28735(CR) : 22/12/2011 : Start--%>
           <tr> 
           <td> <table width= "100%" border = "0">
                  <tr>
                        <td align="left" style="width: 22%" valign="top" class="txtstyle">
                        <asp:Label ID="lblTitle" runat="server" Font-Names="Verdana" Font-Size="9pt"></asp:Label>  
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                        <span id="span1" runat="server">
                        </span>
                        </td>
                        <td align="left" style="width: 20%" valign="top" class="txtstyle">
                        <asp:Label ID="lblProjectName" runat="server" Font-Names="Verdana" Font-Size="9pt"></asp:Label>                     
                        </td>                      
                                             
                        <td width="20%" align="left" valign="top" class="txtstyle">
                            Role
                        </td>
                          <td valign="top" align="right" style="width: 2%">
                            <span id="span2" runat="server">
                            </span>
                        </td>
                        <td width="20%" align="left"> 
                        <asp:Label ID="lblRole" runat="server" Font-Names="Verdana" Font-Size="9pt"></asp:Label>                         
                        </td>
                       </tr>
                     
                        </table>
                        </td>
                        </tr>
    
       <%--Aarohi : Issue 28735(CR) : 22/12/2011 : End--%>
        <tr>
            <td>
                <table class="detailsbg" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="MRF Requirement" CssClass="detailsheader"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="10px">
            <td>
            </td>
        </tr>
        
        <tr>
            <td align="center">
                <table width="100%" border="0">
                    <tr>
                        <td align="left" style="width: 22%" valign="top" class="txtstyle">
                            Mandatory skills
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                            <span id="spanMusthaveskill" runat="server">
                                <img id="imgMusthaveskill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="txtMusthaveskill" runat="server" Height="40px" TextMode="MultiLine"
                                Width="190px" MaxLength="5000" ToolTip="Enter Must Have Skill" TabIndex="1" CssClass="mandatoryField"></asp:TextBox>
                        </td>
                        <td width="20%" align="left" valign="top" class="txtstyle">
                            Good to have skills<label class="mandatorymark">*</label>
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                            <span id="spanGoodtohaveskill" runat="server">
                                <img id="imgGoodtohaveskill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td width="20%" align="left">
                            <asp:TextBox ID="txtGoodtohaveskill" runat="server" Height="40px" TextMode="MultiLine"
                                Width="190px" ToolTip="Enter Good to have skill" MaxLength="5000" TabIndex="2"
                                CssClass="mandatoryField"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 22%" valign="top" class="txtstyle">
                            Tools
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                            <span id="spanTools" runat="server">
                                <img id="imgTools" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="txtTools" runat="server" Height="40px" TextMode="MultiLine" Width="190px"
                                ToolTip="Enter Tool" MaxLength="5000" TabIndex="3" CssClass="mandatoryField"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" valign="top" class="txtstyle">
                            Relevant Experience<label class="mandatorymark">*</label>
                        </td>
                        <td style="width: 2%" valign="top" align="right">
                            <span id="spanExperience" runat="server">
                                <img id="imgExperience" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <span id="spanzExperience" runat="server">
                                <asp:TextBox ID="txtExperience" runat="server" Width="50px" MaxLength="3" ToolTip="Enter Experience From"
                                    TabIndex="4" CssClass="mandatoryField"></asp:TextBox>
                                <asp:Literal ID="lit1" runat="server">--</asp:Literal>
                                <asp:TextBox ID="txtExperience1" runat="server" Width="50px" MaxLength="3" ToolTip="Enter Experience To"
                                    TabIndex="4" CssClass="mandatoryField"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr id="rowDomainAndSkill" runat="server">
                        <td align="left" style="width: 22%" valign="top" class="txtstyle">
                            Domain
                            <label class="mandatorymark" id="MandatoryDomain" runat="server">
                                *</label>
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                            <span id="spanDomain" runat="server">
                                <img id="imgDomain" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="txtDomain" runat="server" Width="190px" MaxLength="50" ToolTip="Domain"
                                TabIndex="5" CssClass="mandatoryField"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" valign="top" class="txtstyle">
                            Skills Category
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 2%" valign="top" align="right">
                            <img id="imgSkillCategory" runat="server" src="Images/cross.png" alt="" style="display: none;
                                border: none;" />
                        </td>
                        <td align="left" width="20%" valign="top">
                            <span id="spanzSkillCategory" runat="server">
                                <asp:DropDownList ID="ddlSkillCategory" runat="server" Width="196px" ToolTip="Select Skill Category"
                                    TabIndex="6" CssClass="mandatoryField">
                                </asp:DropDownList>
                            </span>
                            <!--<asp:TextBox ID="txtSkillCategory" runat="server" Height="40px" TextMode="MultiLine" Width="190px" ToolTip="Enter Skill Category"></asp:TextBox>-->
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 22%" valign="top" class="txtstyle">
                            Qualification
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                            <span id="spanHeightQualification" runat="server">
                                <img id="imgHeightQualification" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="txtHeightQualification" runat="server" Width="190px" MaxLength="30"
                                ToolTip="Enter Highest Qualification" TabIndex="7" CssClass="mandatoryField"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" valign="top" class="txtstyle">
                            Non Technical Skills<label class="mandatorymark">*</label>
                        </td>
                        <td style="width: 2%" valign="top" align="right">
                            <span id="spanSoftSkill" runat="server">
                                <img id="imgSoftSkill" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtSoftSkill" runat="server" Height="40px" TextMode="MultiLine"
                                Width="190px" ToolTip="Enter Soft Skill" MaxLength="5000" TabIndex="8"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="rowUtilizationAndBilling" runat="server">
                        <td align="left" style="width: 22%" valign="top" class="txtstyle">
                            Utilization(%)
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                            <span id="spanUtilization" runat="server">
                                <img id="imgUtilization" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="txtUtilijation" runat="server" CssClass="mandatoryField" MaxLength="3"
                                BorderStyle="NotSet" TabIndex="9" AutoComplete="off" Width="190px" ToolTip="Enter Utilization"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" valign="top" class="txtstyle">
                            Billing(%)
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 2%" valign="top" align="right">
                            <span id="spanBilling" runat="server">
                                <img id="imgBilling" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="txtBilling" runat="server" CssClass="mandatoryField" MaxLength="3"
                                TabIndex="10" AutoComplete="off" Width="190px" ToolTip="Enter Billing"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 22%" valign="top" class="txtstyle">
                            Target CTC(Lks)
                            <%-- <label class="mandatorymark">
                                *</label>--%>
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                            <span id="spanCTC" runat="server">
                                <img id="imgCTC" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <!-- Issue Id : 34809 changed CTC digit from 3 to 5  START-->
                            <asp:TextBox ID="txtCTC" runat="server" CssClass="mandatoryField" MaxLength="5" BorderStyle="NotSet"
                                TabIndex="11" AutoComplete="off" Width="50px" ToolTip="Enter Target CTC From"></asp:TextBox>
                            --
                            <asp:TextBox ID="txtCTC1" runat="server" CssClass="mandatoryField" MaxLength="5"
                                BorderStyle="NotSet" TabIndex="11" AutoComplete="off" Width="50px" ToolTip="Enter Target CTC To"></asp:TextBox>
                                <!-- Issue Id : 34809 changed CTC digit from 3 to 5  END-->
                        </td>
                        <td align="left" width="20%" valign="top" class="txtstyle">
                            Remarks
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 2%" valign="top" align="right">
                            <span id="spanRemarks" runat="server">
                                <img id="imgRemarks" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="mandatoryField" MaxLength="5000"
                                TabIndex="12" AutoComplete="off" Width="190px" Height="40px" TextMode="MultiLine"
                                ToolTip="Enter Remarks"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 22%" valign="top" class="txtstyle">
                            Accountable To
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                            <span id="spanResponsiblePerson" runat="server">
                                <img id="imgResponsiblePerson" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="txtResponsiblePerson" runat="server" CssClass="mandatoryField" MaxLength="100"
                                BorderStyle="NotSet" TabIndex="13" AutoComplete="off" Width="190px" ToolTip="Enter Accountable To"
                                Height="40px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                            <img id="imgResponsiblePersonSearch" runat="server" src="Images/find.png" alt="" />
                        </td>
                        <td align="left" width="20%" valign="top" class="txtstyle">
                            Resource Responsibility
                            <label class="mandatorymark">
                                *</label>
                        </td>
                        <td style="width: 2%" valign="top" align="right">
                            <span id="spanResourceResponsibility" runat="server">
                                <img id="imgResourceResponsibility" runat="server" src="Images/cross.png" alt=""
                                    style="display: none; border: none;" />
                            </span>
                        </td>
                        <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="txtResourceResponsibility" runat="server" CssClass="mandatoryField"
                                MaxLength="5000" TabIndex="14" AutoComplete="off" Width="190px" Height="40px"
                                TextMode="MultiLine" ToolTip="Enter Resource Responsibility"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <%--Ishwar Patil 20042015 Start --%>
                        <td align="left" style="width: 22%" valign="top" class="txtstyle">
                            Skills
                            <%--<label class="mandatorymark">
                                *</label>
                            &nbsp;--%>
                        </td>
                        <td valign="top" align="right" style="width: 2%">
                            <span id="spanSkills" runat="server">
                                <img id="imgSkills" runat="server" src="Images/cross.png" alt="" style="display: none;
                                    border: none;" />
                            </span>
                        </td>
                         <td align="left" width="20%" valign="top">
                            <asp:TextBox ID="TxtSkills" runat="server" CssClass="mandatoryField" MaxLength="2000"
                                BorderStyle="NotSet" TabIndex="13" AutoComplete="off" Width="190px" ToolTip="Select Skills"
                                Height="40px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                            <img id="imgSkillsSearch" runat="server" src="Images/find.png" alt="" />
                        </td>
                        <%--Ishwar Patil 20042015 End --%>
                        
                        <%-- <td align="left" width="20%" valign="top">
                             <span id="spanzPurpose" runat="server">
                                <asp:DropDownList ID="ddlPurpose" runat="server" Width="196px" ToolTip="Select Purpose"
                                    TabIndex="6" CssClass="mandatoryField" OnSelectedIndexChanged="ddlPurpose_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </span>
                            &nbsp;
                        </td>--%>
                        <td align="left" width="20%" valign="top" class="txtstyle">
                        </td>
                        <td style="width: 2%" valign="top" align="right">
                            &nbsp;
                        </td>
                        <td align="left" width="20%" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" valign="top" class="txtstyle">
                            <%--     <asp:Label ID="lblPurpose" Text="" runat="server"></asp:Label><label class="mandatorymark"
                                id="lblmandatorymarkPurpose" runat="server" visible="false">*</label>
            --%>
                            &nbsp;
                        </td>
                        <td style="width: 2%" valign="top" align="right">
                            &nbsp;
                        </td>
                        <td align="left" width="20%" valign="top">
                            <%--    <asp:TextBox ID="txtPurpose" runat="server" CssClass="mandatoryField" MaxLength="50"
                                AutoComplete="off" Width="190px" ToolTip="Enter Purpose" Visible="false"></asp:TextBox>
                            <img id="imgPurpose" runat="server" src="Images/find.png" alt="" visible="false" />
  --%>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 22%">
                            <asp:Button ID="btnPrevious" runat="server" Text="Previous" Width="120px" CssClass="button"
                                TabIndex="17" OnClick="btnPrevious_Click" />
                        </td>
                        <td style="width: 2%">
                        </td>
                        <td width="20%">
                            <asp:HiddenField ID="hidResponsiblePersonName" runat="server" />
                            <asp:HiddenField ID="hidSkillsName" runat="server" />
                        </td>
                        <td width="10%">
                            <asp:HiddenField ID="hidDepartmentName" runat="server" />
                        </td>
                        <td style="width: 2%">
                            <asp:HiddenField ID="hidResponsiblePerson" runat="server" />
                            <asp:HiddenField ID="hidSkills" runat="server" />
                        </td>
                        <td width="20%">
                            <asp:Button ID="btnRaiseMRF" runat="server" Text="RaiseMRF" Width="120px" CssClass="button"
                                TabIndex="15" OnClick="btnRaiseMRF_Click" />&nbsp;&nbsp;<asp:Button ID="btnCancel"
                                    runat="server" Text="Cancel" Width="120px" CssClass="button" TabIndex="16" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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

                $('#ctl00_cphMainContent_btnRaiseMRF').click(function() {
                    if (ButtonClickValidate()) {

                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                    }

                });

                //                $(window).on('beforeunload', function() {
                //                    var button2 = $('#ctl00_cphMainContent_btnRaiseMRF');
                //                    button2.attr('disabled', 'disabled');
                //                    button2.val("Please Wait..");
                //                    setTimeout(function() {
                //                        button2.removeAttr('disabled');
                //                    }, 20000);
                //                    
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
        
        function popUpEmployeeSearch() {
            jQuery.modalDialog.create({ url: "EmployeesList.aspx?PageName=MrfRaiseNextOrRaiseHeadCount", maxWidth: 550,
            onclose: function(e) {
                debugger;       
                    var txtResponsiblePerson = jQuery('#<%=txtResponsiblePerson.ClientID %>');
                    var hidResponsiblePerson = jQuery('#<%=hidResponsiblePerson.ClientID %>');
                    var hidResponsiblePersonName = jQuery('#<%=hidResponsiblePersonName.ClientID %>');

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
                        hidResponsiblePersonName.val(EmpName.trim());
                    }
                }
            }).open();
        }
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

       
        
        function $(v) { return document.getElementById(v); }

        function ButtonClickValidate() {
            
            var lblMandatory;
            var spanlist = "";

            var txtMusthaveskill = $('<%=txtMusthaveskill.ClientID %>').id
            var txtGoodtohaveskill = $('<%=txtGoodtohaveskill.ClientID %>').id
            var txtTools = $('<%=txtTools.ClientID %>').id
            var departmentName = $('<%=hidDepartmentName.ClientID %>').value
            var txtSkillCategory = $('<%=ddlSkillCategory.ClientID %>').value
            var txtDomain = $('<%=txtDomain.ClientID %>').id
            var DomainValue = $('<%=txtDomain.ClientID %>').value;
            
            if (txtSkillCategory == "" || txtSkillCategory == "SELECT" || txtSkillCategory == "0") {
                var sSkillCategory = $('<%=spanzSkillCategory.ClientID %>').id;
                spanlist = sSkillCategory + ",";
            }                      

            if (departmentName == "Projects") {
                //    if(DomainValue != "")
                //     {
                //              
                //     }
            }
            else {
                var txtUtilization = $('<%=txtUtilijation.ClientID %>').id;
                var txtBilling = $('<%=txtBilling.ClientID %>').id;
            }



            var txtExperience = $('<%=txtExperience.ClientID %>').id
            var txtExperience1 = $('<%=txtExperience1.ClientID %>').id

            var txtHeightQualification = $('<%=txtHeightQualification.ClientID %>').id
            var txtSoftSkill = $('<%=txtSoftSkill.ClientID %>').id
            var txtRemarks = $('<%=txtRemarks.ClientID %>').id
            var txtResponsiblePerson = $('<%=txtResponsiblePerson.ClientID %>').id
            var txtResourceResponsibility = $('<%=txtResourceResponsibility.ClientID %>').id
            //Ishwar Patil 28/04/2015 Start
            var TxtSkills = $('<%=TxtSkills.ClientID %>').id
            //Ishwar Patil 28/04/2015 End

            if (spanlist == "") {
                if ((DomainValue == "" || DomainValue == 'undefined') && (departmentName != "Projects")) {
                    var controlList = txtMusthaveskill + "," + txtGoodtohaveskill + "," + txtTools + "," +
                txtExperience + "," + txtExperience1 + "," + txtHeightQualification + "," + txtSoftSkill + "," +
                txtUtilization + "," + txtBilling + "," + txtRemarks + "," + txtResponsiblePerson + "," +
                txtResourceResponsibility;
                }
                else {
                    var controlList = txtMusthaveskill + "," + txtGoodtohaveskill + "," + txtTools + "," +
                txtExperience + "," + txtExperience1 + "," + txtDomain + "," + txtHeightQualification + "," +
                txtSoftSkill + "," + txtRemarks + "," + txtResponsiblePerson + "," + txtResourceResponsibility;
                }

            }
            else {
                if (departmentName != "Projects") {
                    var controlList = spanlist + txtMusthaveskill + "," + txtGoodtohaveskill + "," + txtTools + "," +
                txtExperience + "," + txtExperience1 + "," + txtHeightQualification + "," +
                txtSoftSkill + "," + txtUtilization + "," + txtBilling + "," + txtRemarks + "," +
                txtResponsiblePerson + "," + txtResourceResponsibility;
                    //+ txtDomain + "," 
                }
                else {
                    var controlList = spanlist + txtMusthaveskill + "," + txtGoodtohaveskill + "," + txtTools + "," +
                txtExperience + "," + txtExperience1 + "," + txtDomain + "," + txtHeightQualification + "," +
                txtSoftSkill + "," + txtRemarks + "," + txtResponsiblePerson + "," + txtResourceResponsibility;
                }
            }
            if (ValidateControlOnClickEvent(controlList) == false) {
                lblMandatory = $('<%=lblMandatory.ClientID %>')
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
            }
            return ValidateControlOnClickEvent(controlList);
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
            /*This if block is added to allow Billing as 0% by Gaurav Thakkar*/
            if (txtBilling == 0) {
                return true;
            }
            else {
                return ValidateControl($('<%=txtBilling.ClientID %>').id, $('<%=imgBilling.ClientID %>').id, 'IsNumeric');
            }
        }

        function checkCTC1() {

            var txtCTC1 = $('<%=txtCTC1.ClientID %>').value
            var txtCTC = $('<%=txtCTC.ClientID %>').value
            var txtCTCID = $('<%=txtCTC.ClientID %>').id
            if (txtCTC == "") {
                alert("CTC From cannot be blank");
                $('<%=txtCTC1.ClientID %>').value = "";
                $('<%=txtCTC.ClientID %>').focus();
                return false;
            }
            else if (parseInt(txtCTC) > parseInt(txtCTC1)) {
                alert("CTC From cannot be more then CTC To");
                $('<%=txtCTC1.ClientID %>').value = "";
                $('<%=txtCTC.ClientID %>').value = "";
                $('<%=txtCTC.ClientID %>').focus();
                return false;
            }
            else {
                return ValidateControl($('<%=txtCTC1.ClientID %>').id, $('<%=imgCTC.ClientID %>').id, 'Decimal');
            }
        }

        function checkExperience1() {
            
            var txtExperience1 = $('<%=txtExperience1.ClientID %>').value
            var txtExperience = $('<%=txtExperience.ClientID %>').value
            var txtExperienceID = $('<%=txtExperience.ClientID %>').id
            if (txtExperience == "") {
                alert("Experience From cannot be blank");
                $('<%=txtExperience1.ClientID %>').value = "";
                $('<%=txtExperience.ClientID %>').focus();
                return false;
            }
            else if (parseInt(txtExperience) > parseInt(txtExperience1)) {
                alert("Experience From cannot be more then Experience To");
                $('<%=txtExperience1.ClientID %>').value = "";
                $('<%=txtExperience.ClientID %>').value = "";
                $('<%=txtExperience.ClientID %>').focus();
                return false;
            }
            else {
                return ValidateControl($('<%=txtExperience1.ClientID %>').id, $('<%=imgExperience.ClientID %>').id, 'Decimal');
            }
        }

        function MultiLineTextBoxCheckMustHave(controlobj, maxlength, imgobj) {
            //debugger;
            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            //         else
            //         {
            //            return ValidateControl($( '<%=txtMusthaveskill.ClientID %>').id,$( '<%=imgMusthaveskill.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            //         }

        }
        function MultiLineTextBoxCheckGoodHave(controlobj, maxlength, imgobj) {
            //debugger;
            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            //         else
            //         {
            //            return ValidateControl($( '<%=txtGoodtohaveskill.ClientID %>').id,$( '<%=imgGoodtohaveskill.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            //         }

        }

        function MultiLineTextBoxCheckTools(controlobj, maxlength, imgobj) {
            //debugger;
            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            //         else
            //         {
            //            return ValidateControl($( '<%=txtTools.ClientID %>').id,$( '<%=imgTools.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            //         }

        }

        function MultiLineTextBoxCheckSoftSkill(controlobj, maxlength, imgobj) {
            //debugger;
            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            else {
                return ValidateControl($('<%=txtSoftSkill.ClientID %>').id, $('<%=imgSoftSkill.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            }

        }

        function MultiLineTextBoxCheckRemarks(controlobj, maxlength, imgobj) {
            //debugger;
            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
            else {
                return ValidateControl($('<%=txtRemarks.ClientID %>').id, $('<%=imgRemarks.ClientID %>').id, 'ALPHANUMERIC_WITHSPACE');
            }

        }

        function MultiLineTextBoxCheckResponsibility(controlobj, maxlength, imgobj) {
            //debugger;
            if (MultiLineTextBox(controlobj, maxlength, imgobj) == false) {
                return;
            }
        }
    
    </script>

</asp:Content>
