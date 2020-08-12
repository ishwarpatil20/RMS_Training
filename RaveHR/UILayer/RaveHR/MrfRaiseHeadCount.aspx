<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MrfRaiseHeadCount.aspx.cs"
    Inherits="MrfRaiseHeadCount" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Raise Head Count</title>
    <%--<base target="_self" />--%>
    <link href="CSS/MRFCommon.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="~/../JavaScript/CommonValidations.js"></script>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinnyContent.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialogContent.js" type="text/javascript"></script>

    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->
    <style type="text/css">
        .style1
        {
            width: 40%;
        }
        .style2
        {
            width: 60%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <%--Sanju:Issue Id 50201: Added new class header_employee_profile so that the header color is same for all browsers--%>
                <td align="center" class="header_employee_profile" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <%-- Sanju:Issue Id 50201: End--%>
                    <span class="header">Raise Head Count</span>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 200px;">
                    <asp:Label ID="lblMandatory" runat="server" Font-Names="Verdana" Font-Size="9pt">Fields marked with <span class="mandatorymark">*</span> are mandatory.</asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td class="style1">
                    <asp:Label ID="lblRecruitmentManager" runat="server" Text="Recruiter Name" CssClass="txtstyle">
                    </asp:Label>
                    <label class="mandatorymark">
                        *</label>
                </td>
                <td class="style2">
                    <span id="spanzddlRecruitmentManager" runat="server">
                        <asp:DropDownList ID="ddlRecruitmentManager" runat="server" Width="155px" CssClass="mandatoryField"
                            ToolTip="Select Recruitment Manager">
                        </asp:DropDownList>
                        &nbsp; </span>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" CssClass="txtstyle">
                    </asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtProjectName" runat="server" CssClass="mandatoryField" Width="280px"
                        ReadOnly="true">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblRole" runat="server" Text="Role" CssClass="txtstyle">
                    </asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtRole" runat="server" CssClass="mandatoryField" Width="280px"
                        ReadOnly="true">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblExperience" runat="server" Text="Experience(yrs)" CssClass="txtstyle">
                    </asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtExperience" runat="server" CssClass="mandatoryField" Width="150px"
                        ReadOnly="true">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblTargetCTC" runat="server" Text="Target CTC(lks)" CssClass="txtstyle">
                    </asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtTargetCTC" runat="server" CssClass="mandatoryField" Width="150px"
                        ReadOnly="true">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="padding-left: 5px;">
                    <asp:Label ID="lblTargetDate" runat="server" Text="Target Closure Date to Recruiter">
                    </asp:Label>
                    <label class="mandatorymark" visible="false">
                        *</label>
                </td>
                <td class="style2">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <asp:TextBox ID="txtTargetDate" runat="server" CssClass="mandatoryField" Width="130px"
                        onblur="RestrictDateTypingAndPaste(this)" onpaste="return RestrictDateTypingAndPaste(this)">
                    </asp:TextBox>
                    <asp:ImageButton ID="imgTargetDate" runat="server" ImageUrl="Images/Calendar_scheduleHS.png"
                        CausesValidation="false" ImageAlign="AbsMiddle" TabIndex="6" />
                    <cc1:CalendarExtender ID="calendardate" runat="server" PopupButtonID="imgTargetDate"
                        TargetControlID="txtTargetDate" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="Label1" runat="server" Text="Purpose" CssClass="txtstyle">
                    </asp:Label>
                    <label class="mandatorymark" visible="false">
                        *</label>
                </td>
                <td class="style2">
                    <span id="spanzPurpose" runat="server">
                        <asp:DropDownList ID="ddlPurpose" runat="server" Width="196px" ToolTip="Select Purpose"
                            TabIndex="6" CssClass="mandatoryField" OnSelectedIndexChanged="ddlPurpose_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="style1" style="padding-left: 3px;">
                    <asp:Label ID="lblPurpose" Text="" runat="server" CssClass="txtstyle"></asp:Label>
                    <label class="mandatorymark" id="lblmandatorymarkPurpose" runat="server" visible="false">
                        *</label>
                </td>
                <td class="style2" style="width: 210px;">
                    <asp:TextBox ID="txtPurpose" runat="server" CssClass="mandatoryField" MaxLength="50"
                        AutoComplete="off" Width="170px" ToolTip="Enter Purpose" Visible="false"></asp:TextBox>
                    <img id="imgPurpose" runat="server" src="Images/find.png" alt="" visible="false" />
                    <asp:RequiredFieldValidator ID="valRequired" runat="server" ErrorMessage="" ControlToValidate="txtPurpose"
                        Display="dynamic" />
                    <span id="spanzddlDepartment" runat="server">
                        <asp:DropDownList ID="ddlDepartment" Visible="false" runat="server" Width="155px"
                            CssClass="mandatoryField" ToolTip="Select Department">
                        </asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr runat="server" id="trCostCode">
                <td class="style1" style="padding-left: 3px;">
                    Cost Code<label class="mandatorymark" id="lblCostCodeValidation" runat="server"
                        visible="true">*</label>
                </td>
                <td class="style2" style="width: 210px;">
                    <span id="spanzddlCostCode" runat="server">
                        <asp:DropDownList ID="ddlCostCode" runat="server" Width="200px" CssClass="mandatoryField"
                            ToolTip="Select Cost Code">
                        </asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:HiddenField ID="hidMrfId" runat="server" />
                    <asp:HiddenField ID="hidDepartment" runat="server" />
                    <asp:HiddenField ID="hidMrfCode" runat="server" />
                    <%--57877-Venkatesh-  29042016 : Start --%>
                    <asp:HiddenField ID="HfldProjectId" runat="server" />
                    <%-- 57877-Venkatesh-  29042016 : End--%>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnOK" runat="server" Text="OK" Width="119px" CssClass="button" OnClick="btnOK_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="119px" CssClass="button"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script language="javascript" type="text/javascript">
        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        var retVal;
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $("#<%=imgPurpose.ClientID %>").on("click", function(e) {
                    $.modalDialog.create({ url: "EmployeesList.aspx?PageName=MrfRaiseNextOrRaiseHeadCount", maxWidth: 550,
                        onclose: function(e) {
                            var txtPurpose = $('#<%=txtPurpose.ClientID %>');
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
                            }
                        }
                    }).open();
                });

                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        retVal = e.data;
                    }
                });

                $('#btnOK').click(function() {
                    if (ButtonClickValidate()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                    }
                });
            });
        })(jQuery);
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        function $(v) {
            return document.getElementById(v);
        }

        //        window.onbeforeunload = function() {
        //            var btn = document.getElementById("btnOK");
        //            btn.disabled = true;
        //            btn.value = "Proecesing";
        //            
        //        };


        function ButtonClickValidate() {
           // debugger;
            var lblMandatory;
            var spanlist = "";
            var RecruitmentManager = $('<%=ddlRecruitmentManager.ClientID %>').value;

            if ($('<%=ddlDepartment.ClientID %>') != undefined) {
                var ddlDepartment = $('<%=ddlDepartment.ClientID %>').value;
                if (ddlDepartment == "" || ddlDepartment == "SELECT") {
                    var sddlDepartment = $('<%=spanzddlDepartment.ClientID %>').id;
                    spanlist = sddlDepartment + ",";
                }

            }
            debugger;
        //    alert($('<%=IsCostCodeValidation%>'));
            var flg = '<%=IsCostCodeValidation%>' ;
            if (flg == "True") {

                if ($('<%=ddlCostCode.ClientID %>') != undefined) {
                    var ddlCostCode = $('<%=ddlCostCode.ClientID %>').value;
                    if (ddlCostCode == "" || ddlCostCode == "SELECT") {
                        var sddlCostCode = $('<%=spanzddlCostCode.ClientID %>').id;
                        spanlist = spanlist + sddlCostCode + ",";
                    }

                }
            }
            
            var TargetDate = $('<%=txtTargetDate.ClientID %>').id;


            if (RecruitmentManager == "" || RecruitmentManager == "SELECT") {
                var sRecruitmentManager = $('<%=spanzddlRecruitmentManager.ClientID %>').id;
                spanlist = spanlist + sRecruitmentManager + ",";
            }






            var MRFPurpose = $('<%=ddlPurpose.ClientID %>').value;

            if (MRFPurpose == "" || MRFPurpose == "SELECT") {
                var spanPurpose = $('<%=spanzPurpose.ClientID %>').id;
                spanlist = spanlist + spanPurpose + ",";
            }
            //Venkatesh : Issue 35089 : 07/11/2013 : Start
            //Desc : Remove validation for Project, except Raveforecasted
            var txtProjectName = $('<%=txtProjectName.ClientID %>').id
            //if (document.getElementById(txtProjectName).value.trim() == "") {
            var Purpose = $('<%=ddlPurpose.ClientID %>').options[$('<%=ddlPurpose.ClientID%>').selectedIndex].text;
            if (Purpose == "Hiring for new role" || Purpose == "Hiring for project" || Purpose == "Replacement" || Purpose == "Internal Projects/Future Business") {
                var txtPurposeDescription = $('<%=txtPurpose.ClientID %>').id;
                spanlist = spanlist + txtPurposeDescription + ",";
            }
            //}
            //            else {
            //                var Purpose = $('<%=ddlPurpose.ClientID %>').options[$('<%=ddlPurpose.ClientID%>').selectedIndex].text;
            //                if (Purpose == "Hiring for new role" || Purpose == "Replacement" || Purpose == "Internal Projects/Future Business") {
            //                    var txtPurposeDescription = $('<%=txtPurpose.ClientID %>').id;
            //                    spanlist = txtPurposeDescription + ",";
            //                }
            //            }
            //Venkatesh : Issue 35089 : 07/11/2013 : End

            if (spanlist == "") {
                controlList = TargetDate;
            }
            else {
                controlList = TargetDate + "," + $('<%=ddlRecruitmentManager.ClientID %>').id + "," + spanlist;
            }

            if (ValidateControlOnClickEvent(controlList) == false) {
                lblMandatory = $('<%=lblMandatory.ClientID %>')
                lblMandatory.innerText = "Please fill all mandatory fields.";
                lblMandatory.style.color = "Red";
            }
            return ValidateControlOnClickEvent(controlList);
        }

        //        function ValidateProject() {
        //            var txtProjectName = $('<%=txtProjectName.ClientID %>').id
        //            var txtPurpose = $('<%=txtPurpose.ClientID %>').id
        //            var ddlPurpose = $('<%=ddlPurpose.ClientID %>').options[$('<%=ddlPurpose.ClientID%>').selectedIndex].text;

        //            if (document.getElementById(txtProjectName).value.trim() != "" && ddlPurpose == "Hiring for project") {
        //                //var txtPurpose = $('<%=txtPurpose.ClientID %>').id
        //                document.getElementById(txtPurpose).value = document.getElementById(txtProjectName).value.trim();
        //            }
        //        }


        function RestrictDateTypingAndPaste(targetControl) {

            if (targetControl.value != "") {
                targetControl.value = "";
            }
        }
    </script>

</body>
</html>
