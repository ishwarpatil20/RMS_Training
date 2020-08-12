<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="RaiseTrainingRequest.aspx.cs" Inherits="RaiseTrainingRequest" Title="Training Module" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <asp:UpdatePanel ID="UPRaiseTrainingType" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <div style="border: solid 1px black">
                <table width="100%">
                    <tr>
                        <td style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                            width: 100%;">
                            <asp:Label ID="Label3" runat="server" Text="Raise Training Request" CssClass="detailsheader"
                                Style="color: White;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="padding-left: 10px; padding-bottom: 5px">
                    <tr>
                        <td style="height: 20px" colspan="2">
                            <asp:Label ID="lblConfirmMessage" CssClass="Messagetext" runat="server"></asp:Label>
                            <asp:Label ID="lblMandatory" Style="color: Red" runat="server"></asp:Label>
                            <asp:HiddenField ID="hfRaiseID" runat="server" />
                            <asp:HiddenField ID="hfID" runat="server" />
                            <asp:HiddenField ID="hfNameofParticipant" runat="server" />
                            <asp:HiddenField ID="hfPresenter" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px">
                            Training Type : <span style="color: Red">*</span>
                        </td>
                        <td align="left">
                            <span id="spanzTrainingType" runat="server" style="width: 155px">
                                <asp:DropDownList ID="ddlTrainingType" runat="server" Width="250px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlTrainingType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="padding-left: 10px; padding-bottom: 5px">
                    <tr>
                        <td style="padding-top: 5px" colspan="2">
                            <div id="divTechnicalSkills" runat="server" style="border: black 1px solid; padding: 10px">
                                <table>
                                    <tr>
                                        <td style="width: 150px">
                                            Training Name : <span style="color: Red">*</span>
                                        </td>
                                        <td style="width: 350px">
                                            <span id="spanzTrainingNameTech" runat="server" style="width: 155px">
                                                <asp:DropDownList ID="ddlTrainingNameTech" runat="server" Width="200px" onchange="Show_TrainingTypeOtherTech()">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td id="td1OtherTech" style="display: none" align="right" runat="server">
                                            <span style="color: Red">*</span>
                                        </td>
                                        <td id="td2OtherTech" style="display: none" runat="server">
                                            <asp:TextBox ID="txtTrainingTypeOtherTech" Text="Name of the Training" onblur = "WaterMark(this, event);" onfocus = "WaterMark(this, event);" ForeColor="Gray" runat="server" Width="195px"
                                            OnTextChanged="txtTrainingTypeOtherTech_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px">
                                            Quarter : <span style="color: Red">*</span>
                                        </td>
                                        <td style="width: 250px">
                                            <span id="spanzQuarter" runat="server" style="width: 155px">
                                                <asp:DropDownList ID="ddlQuarter" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td style="width: 150px">
                                            No. of participants : <span style="color: Red">*</span>
                                        </td>
                                        <td style="width: 350px">
                                            <span id="spanzNoofParticipants" runat="server" style="width: 155px">
                                                <asp:DropDownList ID="ddlNoofParticipants" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Category : <span style="color: Red;">*</span>
                                        </td>
                                        <td>
                                            <span id="spanzCategory" runat="server" style="width: 155px">
                                                <asp:DropDownList ID="ddlCategoryTech" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td>
                                            Requested By : <span style="color: Red">*</span>
                                        </td>
                                        <td>
                                            <span id="spanzRequestBy" runat="server" style="width: 155px">
                                                <asp:DropDownList ID="ddlRequestByTechnical" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            Priority : <span style="color: Red">*</span>
                                        </td>
                                        <td>
                                            <span id="spanzpriority" runat="server" style="width: 155px">
                                                <asp:DropDownList ID="ddlpriority" runat="server" Width="200px" onchange="BusinessImpactValidation(this.id)">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="High"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Medium"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Low"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td>
                                            Business Impact : <span style="color: Red" id="SpanBusinessImpact">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBusinessImpact" MaxLength="1000" runat="server" Width="320px"
                                                TextMode="MultiLine" Height="30px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Comments (if any) : <span style="color: white">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCommentsTechnical" MaxLength="1000" runat="server" Width="320px"
                                                TextMode="MultiLine" Height="30px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 40px; vertical-align: bottom" align="center">
                                            <asp:Button CssClass="button" ID="btnSave" runat="server" Text="Submit" ToolTip="Save"
                                                OnClientClick="return TechSoft_Validation()" OnClick="btnSave_Click" />
                                            <asp:Button CssClass="button" ID="btnReset" runat="server" Text="Reset" ToolTip="Reset" OnClientClick="Reset_TechnicalSoftSkillsTraining()" 
                                                OnClick="btnReset_OnClick" />
                                            <%--<asp:Button CssClass="button" ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divSeminars" runat="server" visible="false" style="border: black 1px solid;
                                padding: 10px">
                                <table>
                                    <tr>
                                        <td style="width: 150px">
                                            Name : <span style="color: Red">*</span>
                                        </td>
                                        <td style="width: 350px">
                                            <asp:TextBox ID="txtSeminarsName" runat="server" Width="320px"></asp:TextBox>
                                        </td>
                                        <td style="width: 150px">
                                            Date : <span style="color: Red">*</span>
                                        </td>
                                        <td style="width: 350px">
                                            <div id="divdate" runat="server">
                                                <uc1:DatePicker ID="ucSeminarsDate" runat="server" />
                                                </div>
                                                <span id="spanSeminars" runat="server">
                                                    <img id="img1" runat="server" src="Images/Calendar_scheduleHS.png" alt="" style="display: none;
                                                        border: none;" align="left" />
                                                </span>
                                            
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            Requested By : <span style="color: Red">*</span>
                                        </td>
                                        <td>
                                            <span id="spanzRequestedBySeminars" runat="server" style="width: 155px">
                                                <asp:DropDownList ID="ddlRequestedBySeminars" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td>
                                            Name of Participants : <span style="color: Red">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNameofParticipant" runat="server" Width="320px" Height="60px" 
                                                    ToolTip="Enter Name of Participant" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>     
                                                    <img id="imgNameofParticipant" runat="server" src="Images/find.png" alt="" />
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td >
                                            URL : <span style="color: White">*</span>
                                        </td>
                                        <td >
                                            <asp:TextBox ID="txtURL" runat="server" Width="320px"></asp:TextBox>
                                        </td>
                                        <td>
                                            Comments (if any) : <span style="color: white">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCommentSeminars" runat="server" Width="320px" TextMode="MultiLine"
                                                Height="30px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 40px; vertical-align: bottom" align="center">
                                            <asp:Button CssClass="button" ID="btnSaveSeminars" runat="server" Text="Submit" ToolTip="Save"
                                                OnClick="btnSaveSeminars_Click" OnClientClick="return Seminars_Validation()" />
                                            <asp:Button CssClass="button" ID="btnResetSeminars" runat="server" Text="Reset" ToolTip="Reset" OnClientClick="Reset_SeminarsTraining()" 
                                            OnClick="btnResetSeminars_OnClick" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divKSS" runat="server" visible="false" style="border: black 1px solid; padding: 10px">
                                <table>
                                    <tr>
                                        <td style="width: 130px">
                                            Type : <span style="color: Red">*</span>
                                        </td>
                                        <td style="width: 350px">
                                            <span id="spanzType" runat="server" style="width: 155px">
                                                <asp:DropDownList ID="ddlType" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td style="width: 130px">
                                            Topic : <span style="color: red">*</span>
                                        </td>
                                        <td style="width: 350px">
                                            <asp:TextBox ID="txtTopic" runat="server" MaxLength="200" Width="320px" TextMode="MultiLine"
                                                Height="30px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Agenda : <span style="color: red">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAgenda" MaxLength="200" Height="60px" Width="320px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            Presenter (s) : <span style="color: Red">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPresenter" runat="server" Width="320px" Height="60px" ToolTip="Enter Name of Presenter"
                                                ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                            <img id="imgPresenter" runat="server" src="Images/find.png" alt="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date : <span style="color: Red">*</span>
                                        </td>
                                        <td>
                                        <div id="divPresenterDate" runat="server">
                                            <uc1:DatePicker ID="ucDate" runat="server" />
                                            </div>
                                            <span id="SpanErrorDate" runat="server">
                                                <img id="imgErrorDate" runat="server" src="Images/Calendar_scheduleHS.png" alt=""
                                                    style="display: none; border: none;" align="left" />
                                            </span>
                                        </td>
                                        <td>
                                            Comments (if any) : <span style="color: white">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCommentsKSS" MaxLength="1000" runat="server" Width="320px" TextMode="MultiLine"
                                                Height="30px"></asp:TextBox>
                                        </td>
                                    </tr>
                            <%--        <tr>
                                        <td>Upload Document : </td>
                                        <td colspan="3">
                                        <asp:FileUpload  ID="FUFileUpload" runat="server" width="550px" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="4" style="height: 40px; vertical-align: bottom" align="center">
                                            <asp:Button CssClass="button" ID="btnSaveKSS" OnClick="btnSaveKSS_Click" runat="server"
                                                Text="Submit" ToolTip="Save" OnClientClick="return KSS_Validation()" />
                                            <asp:Button CssClass="button" ID="btnResetKSS" runat="server" Text="Reset" ToolTip="Reset" OnClientClick="Reset_KSSTraining()" 
                                            OnClick="btnResetKSS_OnClick" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script src="JavaScript/skinny.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->

    <script language="javascript" type="text/javascript">
        var retVal;
        var TechnicalOther = "ctl00_cphMainContent_txtTrainingTypeOtherTech"
        var ddlval = "Select";
        var ddlTrainingNameOtherVal = "OTHERS";
        var ddlPriorityVal = "HIGH";
        var btnValue = "Reset";
        var btnBackValue = "back";
        var defaulttxtOtherName = "Name of the Training";

        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $(window).on("message", function(e) {
                    if (e.data != undefined) {
                        retVal = e.data;
                    }
                });
            });
        })(jQuery);

        function popUpNameOfParticipantSearch() {
            if (jQuery('#<%=imgNameofParticipant.ClientID %>').attr("disabled") != "disabled") {
                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 500,
                    onclose: function(e) {
                        var txtEmpID = jQuery('#<%=hfID.ClientID %>');
                        var txtEmpName = jQuery('#<%=txtNameofParticipant.ClientID %>');
                        var NameofParticipant = jQuery('#<%=hfNameofParticipant.ClientID %>');

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
                            txtEmpID.val(EmpId.trim());
                        }
                        if (EmpName != undefined) {
                            txtEmpName.val(EmpName.trim());
                            NameofParticipant.val(EmpName.trim());
                        }
                    }
                }).open();
            }
        }

        function popUpPresenterSearch() {
            if (jQuery('#<%=imgPresenter.ClientID %>').attr("disabled") != "disabled") {
                jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 500,
                    onclose: function(e) {
                        var txtEmpID = jQuery('#<%=hfID.ClientID %>');
                        var txtEmpName = jQuery('#<%=txtPresenter.ClientID %>');
                        var PresenterName = jQuery('#<%=hfPresenter.ClientID %>');

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
                            txtEmpID.val(EmpId.trim());
                        }
                        if (EmpName != undefined) {
                            txtEmpName.val(EmpName.trim());
                            PresenterName.val(EmpName.trim());
                        }
                    }
                }).open();
            }
        }
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        function $(v)
        { return document.getElementById(v); }

        function Reset_TechnicalSoftSkillsTraining() {
            
            if ($('<%=btnReset.ClientID %>').value == btnValue) {
                $('<%=ddlTrainingNameTech.ClientID %>').selectedIndex = 0;
                $('<%=ddlQuarter.ClientID %>').selectedIndex = 0;
                $('<%=ddlNoofParticipants.ClientID %>').selectedIndex = 0;
                $('<%=ddlCategoryTech.ClientID %>').selectedIndex = 0;
                $('<%=ddlpriority.ClientID %>').selectedIndex = 0;
                $('<%=txtTrainingTypeOtherTech.ClientID %>').value = "Name of the Training";
                $('<%=txtBusinessImpact.ClientID %>').value = "";
                $('<%=txtCommentsTechnical.ClientID %>').value = "";
                $('<%=hfRaiseID.ClientID %>').value = "";
                $('<%=btnSave.ClientID %>').value = "Submit";
            }
        }

        function Reset_SeminarsTraining() {
            if ($('<%=btnResetSeminars.ClientID %>').value == btnValue) {
                $('<%=txtSeminarsName.ClientID %>').value = "";
                $('<%=ucSeminarsDate.ClientID %>').value = "";
                $('<%=txtNameofParticipant.ClientID %>').value = "";
                $('<%=hfNameofParticipant.ClientID %>').value = "";
                $('<%=txtURL.ClientID %>').value = "";
                $('<%=txtCommentSeminars.ClientID %>').value = "";
                $('<%=hfRaiseID.ClientID %>').value = "";
                $('<%=btnSaveSeminars.ClientID %>').value = "Submit";
            }
        }

        function Reset_KSSTraining() {
            if ($('<%=btnResetKSS.ClientID %>').value == btnValue) {
                $('<%=ddlType.ClientID %>').selectedIndex = 0;
                $('<%=txtTopic.ClientID %>').value = "";
                $('<%=txtAgenda.ClientID %>').value = "";
                $('<%=ucDate.ClientID %>').value = "";
                $('<%=txtCommentsKSS.ClientID %>').value = "";
                $('<%=btnSaveKSS.ClientID %>').value = "Submit";
                $('<%=hfRaiseID.ClientID %>').value = "";
                $('<%=txtPresenter.ClientID %>').value = "";
                $('<%=hfPresenter.ClientID %>').value = "";
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        
        function WaterMark(objtxt, event) {
            var defaultText = "Name of the Training";
            if (objtxt.id == TechnicalOther) {
                if (objtxt.value.length == 0 & event.type == "blur") {
                    if (objtxt.id == TechnicalOther) {
                        objtxt.style.color = "Gray";
                        objtxt.value = defaultText;
                    }
                }
            }
            if ((objtxt.value == defaultText) & event.type == "focus") {
                if (objtxt.id == TechnicalOther) {
                    objtxt.style.color = "black";
                    objtxt.value = "";
                }
            }
        }

        function Seminars_Validation() {
            //debugger;
            var spanlist = "";
            var buttonvalue = $('<%=btnSaveSeminars.ClientID %>').value;

            if (buttonvalue.toUpperCase() != btnBackValue.toUpperCase()) {
                $('<%=lblConfirmMessage.ClientID %>').innerText = "";
                var TrainingTypeVal = $('<%=ddlTrainingType.ClientID %>').value;
                var RequestBySeminarsVal = $('<%=ddlRequestedBySeminars.ClientID %>').value;
                var ParticipantsVal = $('<%=txtNameofParticipant.ClientID %>').id;

                var SeminarsNameID = $('<%=txtSeminarsName.ClientID %>').id;
                var DateID = $('<%=ucSeminarsDate.ClientID %>').id;
             
                if (TrainingTypeVal == ddlval) {
                    var TrainingTypeID = $('<%=spanzTrainingType.ClientID %>').id;
                    spanlist = TrainingTypeID;
                }
                if (RequestBySeminarsVal == ddlval) {
                    var ReqBySeminarsID = $('<%=spanzRequestedBySeminars.ClientID %>').id;
                    spanlist += ReqBySeminarsID + ",";
                }

                spanlist = spanlist + "," + SeminarsNameID + "," + DateID + "," + ParticipantsVal;

                if (spanlist != "") {
                    if (ValidateControlOnClickEvent(spanlist) == false) {
                        lblMandatory = $('<%=lblMandatory.ClientID %>')
                        lblMandatory.innerText = "Please fill all mandatory fields.";
                        lblMandatory.style.color = "Red";
                        return ValidateControlOnClickEvent(spanlist);
                    }
                }
                return ValidateControlOnClickEvent(spanlist);
            }
        }

        function KSS_Validation() {
            var spanlist = "";
            var buttonvalue = $('<%=btnSaveKSS.ClientID %>').value;

            if (buttonvalue.toUpperCase() != btnBackValue.toUpperCase()) {
                $('<%=lblConfirmMessage.ClientID %>').innerText = "";
                var TrainingTypeVal = $('<%=ddlTrainingType.ClientID %>').value;
                var TypeVal = $('<%=ddlType.ClientID %>').value;

                var PresenterVal = $('<%=txtPresenter.ClientID %>').id;
                var TopicID = $('<%=txtTopic.ClientID %>').id;
                var AgendaID = $('<%=txtAgenda.ClientID %>').id;
                var DateID = $('<%=ucDate.ClientID %>').id;

                if (TrainingTypeVal == ddlval) {
                    var TrainingTypeID = $('<%=spanzTrainingType.ClientID %>').id;
                    spanlist = TrainingTypeID;
                }
                if (TypeVal == ddlval) {
                    var TypeID = $('<%=spanzType.ClientID %>').id;
                    spanlist += TypeID + ",";
                }

                spanlist = spanlist + "," + TopicID + "," + AgendaID + "," + DateID + "," + PresenterVal;

                if (spanlist != "") {
                    if (ValidateControlOnClickEvent(spanlist) == false) {
                        lblMandatory = $('<%=lblMandatory.ClientID %>')
                        lblMandatory.innerText = "Please fill all mandatory fields.";
                        lblMandatory.style.color = "Red";
                        return ValidateControlOnClickEvent(spanlist);
                    }
                }
                return ValidateControlOnClickEvent(spanlist);
            }
        }

        function TechSoft_Validation() {
            //debugger;
            var spanlist = "";
            var buttonvalue = $('<%=btnSave.ClientID %>').value;

            if (buttonvalue.toUpperCase() != btnBackValue.toUpperCase()) {
                $('<%=lblConfirmMessage.ClientID %>').innerText = "";
                var TrainingTypeVal = $('<%=ddlTrainingType.ClientID %>').value;
                var TrainingNameVal = $('<%=ddlTrainingNameTech.ClientID %>').value;
                var QuarterVal = $('<%=ddlQuarter.ClientID %>').value;
                var NoofParticipantsVal = $('<%=ddlNoofParticipants.ClientID %>').value;
                var CategoryVal = $('<%=ddlCategoryTech.ClientID %>').value;
                var RequestByVal = $('<%=ddlRequestByTechnical.ClientID %>').value;
                var PriorityVal = $('<%=ddlpriority.ClientID %>').value;

                var TrainingNameSelect = document.getElementById('<%=ddlTrainingNameTech.ClientID %>');
                var val_TrainingName = TrainingNameSelect.options[TrainingNameSelect.selectedIndex].text;
                var PrioritySelect = document.getElementById('<%=ddlpriority.ClientID %>');
                var val_Priority = PrioritySelect.options[PrioritySelect.selectedIndex].text;

                var txtTrainingNameOtherId = $('<%=txtTrainingTypeOtherTech.ClientID %>').id;
                var txtTrainingNameOtherValue = $('ctl00_cphMainContent_txtTrainingTypeOtherTech').value;
                var txtBusinessImpactId = $('<%=txtBusinessImpact.ClientID %>').id;

                if (TrainingTypeVal == ddlval) {
                    var TrainingTypeID = $('<%=spanzTrainingType.ClientID %>').id;
                    spanlist = TrainingTypeID + ",";
                }
                if (TrainingNameVal == ddlval) {
                    var TrainingNameID = $('<%=spanzTrainingNameTech.ClientID %>').id;
                    spanlist += TrainingNameID + ",";
                }
                if (QuarterVal == "0") {
                    var QuarterID = $('<%=spanzQuarter.ClientID %>').id;
                    spanlist += QuarterID + ",";
                }
                if (NoofParticipantsVal == ddlval) {
                    var NoofParticipantsID = $('<%=spanzNoofParticipants.ClientID %>').id;
                    spanlist += NoofParticipantsID + ",";
                }
                if (CategoryVal == ddlval) {
                    var CategoryID = $('<%=spanzCategory.ClientID %>').id;
                    spanlist += CategoryID + ",";
                }
                if (RequestByVal == ddlval) {
                    var RequestByID = $('<%=spanzRequestBy.ClientID %>').id;
                    spanlist += RequestByID + ",";
                }
                if (PriorityVal == "0") {
                    var PriorityID = $('<%=spanzpriority.ClientID %>').id;
                    spanlist += PriorityID;
                }
                if (TrainingNameVal != ddlval) {
                    if ($('<%=txtTrainingTypeOtherTech.ClientID %>').value == defaulttxtOtherName) {
                        $('<%=txtTrainingTypeOtherTech.ClientID %>').value = "";
                    }
                }
                //            if (val_Priority.toUpperCase() == ddlPriorityVal) {
                //                document.getElementById('<%=txtBusinessImpact.ClientID %>').style.color = "Red";
                //            }
                //            else {
                //                document.getElementById('<%=txtBusinessImpact.ClientID %>').style.color = "Pink";
                //                document.getElementById('<%=txtBusinessImpact.ClientID %>').style.borderWidth = "1";
                //            }

                if ((val_Priority.toUpperCase() == ddlPriorityVal) && (val_TrainingName.toUpperCase() == ddlTrainingNameOtherVal)) {
                    spanlist += txtTrainingNameOtherId + "," + txtBusinessImpactId;
                }
                else {
                    if (txtTrainingNameOtherValue == "") {
                        if (val_TrainingName.toUpperCase() == ddlTrainingNameOtherVal) {
                            spanlist += "," + txtTrainingNameOtherId;
                        }
                    }
                    if (val_Priority.toUpperCase() == ddlPriorityVal) {
                        spanlist += "," + txtBusinessImpactId;
                    }
                }
                if (spanlist != "") {
                    if (ValidateControlOnClickEvent(spanlist) == false) {
                        lblMandatory = $('<%=lblMandatory.ClientID %>')
                        lblMandatory.innerText = "Please fill all mandatory fields.";
                        lblMandatory.style.color = "Red";
                        return ValidateControlOnClickEvent(spanlist);
                    }
                }
                return ValidateControlOnClickEvent(spanlist);
            }
        }
        function Show_TrainingTypeOtherTech() {
            var TrainingTypeSelect = document.getElementById('<%=ddlTrainingNameTech.ClientID %>');
            var val_TrainingType = TrainingTypeSelect.options[TrainingTypeSelect.selectedIndex].text;
            if (val_TrainingType.toUpperCase() == ddlTrainingNameOtherVal) {
                document.getElementById('<%=td1OtherTech.ClientID %>').style.display = '';
                document.getElementById('<%=td2OtherTech.ClientID %>').style.display = '';
            }
            else {
                $('ctl00_cphMainContent_txtTrainingTypeOtherTech').value = defaulttxtOtherName;
                document.getElementById('<%=td1OtherTech.ClientID %>').style.display = 'none';
                document.getElementById('<%=td2OtherTech.ClientID %>').style.display = 'none';
            }
        }
        function BusinessImpactValidation(PriorityID) {

            var val_TrainingType = document.getElementById('<%=ddlpriority.ClientID %>');
            var val_priority = val_TrainingType.options[val_TrainingType.selectedIndex].text;
            if (val_priority.toUpperCase() == ddlPriorityVal) {
                document.getElementById('SpanBusinessImpact').style.color = "Red";
            }
            else {
                document.getElementById('SpanBusinessImpact').style.color = "White";
            }
        }
    </script>

</asp:Content>
