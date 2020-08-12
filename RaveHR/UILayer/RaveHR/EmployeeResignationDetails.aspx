<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeResignationDetails.aspx.cs" Inherits="EmployeeResignationDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ksa" TagName="BubbleControl" Src="~/EmployeeMenuUC.ascx" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        (function($) {
            $(document).ready(function() {
                window._jqueryPostMessagePolyfillPath = "/postmessage.htm";
                $("#<%=btnRMFMHTML.ClientID %>").on("click", function(e) {
                    $.modalDialog.create({ url: "ReportingOrFunctionalManager.aspx", maxWidth: 1000,
                        onclose: function(e) {
                        }
                    }).open();
                });

                $('#ctl00_cphMainContent_btnSave').click(function() {
                    if (ButtonClickValidate()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                        //$(this).parents('#form').submit();
                    }

                });

                $('#ctl00_cphMainContent_btnRollBack').click(function() {
//                    if (ConfirmForRollBackResign()) {
                        $(this).val("Please Wait..");
                        $(this).attr('disabled', 'disabled');
                        //$(this).parents('#form').submit();
//                    }

                });
                
                

//                $(window).on('beforeunload', function() {
//                    var button1 = $('#ctl00_cphMainContent_btnSave');
//                    var button2 = $('#ctl00_cphMainContent_btnRollBack');

//                    button1.attr('disabled', 'disabled');
//                    button1.val('Please Wait..');
//                    setTimeout(function() {
//                        button1.removeAttr('disabled')
//                    }, 20000);

//                    button2.attr('disabled', 'disabled');
//                    button2.val('Please Wait..');
//                    setTimeout(function() {
//                        button2.removeAttr('disabled');
//                    }, 20000)
//                });

            });
        })(jQuery);
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        //function added to check data has been modified or not on Page.        
        function myfn() {
            var IsDataModified = $('<%=HfIsDataModified.ClientID %>').value;

            if (IsDataModified == "Yes")
                javascript: __doPostBack('ctl00$cphMainContent$lnkSaveBtn', '')
        }

        // 26137-Ambar-Start
        function ConfirmForRollBackResign() {
            var cnfrm = fnConfirm();
            if (cnfrm == true)
            { return true; }
            else
            { return false; }
        }

        function fnConfirm() {
            return confirm('Do you want to cancel the Resignation?');
        }
        // 26137-Ambar-End
        
    </script>

    <asp:LinkButton runat="server" ID="lnkSaveBtn" OnClick="lnkSaveBtn_Click" Visible="false" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <%-- Sanju:Issue Id 50201:Added new class so that header display in other browsers also--%>
            <td align="center" class="header_employee_profile" style="filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');">
                <%--  Sanju:Issue Id 50201:End--%>
                <asp:Label ID="lblResignationDetails" runat="server" Text="Employee Profile" CssClass="header"></asp:Label>
            </td>
            <%-- Sanju:Issue Id 50201: Added background color property so that all browsers display header--%>
            <td align="right" style="height: 25px; width: 20%; background-color: #7590C8; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#7590C8', gradientType='0');">
                <%--  Sanju:Issue Id 50201:End--%>
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

                    function $(v) { return document.getElementById(v); }

                    function ButtonClickValidate() {
                        var lblMandatory;
                        var lblMessage = $('<%=lblMessage.ClientID %>');
                        lblMessage.innerHTML = "";
                        var spanlist = "";

                        // Resignation details controls 
                        var Status = $('<%=ddlStatus.ClientID %>').value;
                        var ResignationDate = $('<%=ucDatePickerResignationDate.ClientID %>').id;
                        var ResignationReason = $('<%=txtResignationReason.ClientID %>').id;
                        //Poonam : Issue : 56652 : Starts
                        var TempResignationDateval = $('<%=ucDatePickerResignationDate.ClientID %>').value.split("/");
                        var ResignationDateVal = Date.UTC(TempResignationDateval[2], TempResignationDateval[1] - 1, TempResignationDateval[0]);
                        
                        var currentTime = new Date();
                        var month = currentTime.getMonth();
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var TodaysDate = Date.UTC(year, month, day);
                        //Poonam : Issue : 56652 : Ends

                        if (Status == "" || Status == "SELECT") {
                            var sStatus = $('<%=spanzStatus.ClientID %>').id;

                            spanlist = sStatus + ",";
                        }

                        if (spanlist == "") {
                            if (Status == "142")
                                var controlList = ResignationDate + "," + ResignationReason;
                            else {
                                var LastWorkDay = $('<%=ucDatePickerLastWorkDay.ClientID %>').id
                                var controlList = LastWorkDay + "," + ResignationDate + "," + ResignationReason;
                            }
                        }
                        else {
                            if (Status == "142")
                                var controlList = spanlist + ResignationDate + "," + ResignationReason;
                            else {
                                var LastWorkDay = $('<%=ucDatePickerLastWorkDay.ClientID %>').id
                                var controlList = spanlist + LastWorkDay + "," + ResignationDate + "," + ResignationReason;
                            }
                        }

                        //Poonam : Issue : 56652 : Starts

                        if (ResignationDateVal > TodaysDate) {
                            lblMessage.innerHTML = "Resignation Date Should not be greater than Todays Date";
                            lblMessage.style.color = "RED";

                            lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                            lblConfirmMsg.innerHTML = "";
                            
                            ValidateNotNullControls(ResignationDate);
                            return false;

                        }
                        //Poonam : Issue : 56652 : Ends
                        
                        if (ValidateControlOnClickEvent(controlList) == false) {
                            //lblMessage = $('<%=lblMessage.ClientID %>')
                            lblMessage.innerHTML = "Please fill all mandatory fields.";
                            lblMessage.style.color = "Red";

                            lblConfirmMsg = $('<%=lblConfirmMsg.ClientID %>')
                            lblConfirmMsg.innerText = "";
                        }

                        return ValidateControlOnClickEvent(controlList);
                    }
                    
                    //Poonam : Issue : 56652 : Starts
                    function ValidateNotNullControls(controlobject) {
                        var arraycontrolobj = controlobject.split(',');
                        for (var i = 0; i < arraycontrolobj.length; i++) {
                            var controlName = document.getElementById(arraycontrolobj[i]);
                            if (controlName != null) {
                                controlName.style.borderStyle = "Solid";
                                controlName.style.borderWidth = "1px";
                                controlName.style.borderColor = "Red";
                            }
                        }
                    }
                    //Poonam : Issue : 56652 : Ends
                    
 
                </script>

                <div class="detailsborder">
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
                    <div id="divRegDetails" class="detailsborder">
                        <table width="100%" class="detailsbg">
                            <tr>
                                <td class="txtstyle">
                                    <asp:Label ID="lblResignationDetailsGroup" runat="server" Text="Resignation Details"
                                        CssClass="detailsheader"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlResignationDetails" runat="server">
                            <table width="100%">
                                <tr style="width: 100%">
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <span id="spanzStatus" runat="server">
                                            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Select Status" CssClass="mandatoryField"
                                                TabIndex="11" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true"
                                                Width="150">
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lbLstWrkDay" runat="server" Text="Last Working Day" CssClass="textstyle"></asp:Label>
                                        <label id="lblmandatoryLstWrkDay" class="mandatorymark" runat="server">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <uc1:DatePicker ID="ucDatePickerLastWorkDay" runat="server" Width="150" />
                                        <span id="spanLastWorkDay" runat="server">
                                            <img id="imgLastWorkDayError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblResignationDate" runat="server" Text="Resignation Date" CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <uc1:DatePicker ID="ucDatePickerResignationDate" runat="server" Width="150" />
                                        <span id="spanResignationDate" runat="server">
                                            <img id="imgResignationDateError" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                    <td style="width: 25%" class="txtstyle">
                                        <asp:Label ID="lblResignationReason" runat="server" Text="Reason For Resignation"
                                            CssClass="textstyle"></asp:Label>
                                        <label class="mandatorymark">
                                            *</label>
                                    </td>
                                    <td style="width: 25%">
                                        <asp:TextBox ID="txtResignationReason" runat="server" CssClass="mandatoryField" MaxLength="100"
                                            ToolTip="Enter Reason For Resignation" BorderStyle="Solid" TabIndex="1" Width="150"
                                            TextMode="MultiLine"></asp:TextBox>
                                        <span id="spanResignationReason" runat="server">
                                            <img id="imgResignationReason" runat="server" src="Images/cross.png" alt="" style="display: none;
                                                border: none;" />
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <%--Aarohi : Issue 28572(CR) : 05/01/2012 : Start--%>
                                    <%--<asp:Button ID="btnRMFM" runat="server" Text="Update Manager" CssClass="button" OnClick="btnRMFM_Click"/>--%>
                                    <input id="btnRMFMHTML" runat="server" type="button" value="Update Manager" onclick="UpdateManagerClick()"
                                        class="button" visible = "false"  />
                                    <%--Aarohi : Issue 28572(CR) : 05/01/2012 : End--%>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnRollBack" runat="server" Text="Roll Back" CssClass="button" OnClick="btnRollBack_Click"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:HiddenField ID="HfIsDataModified" runat="server" />
                </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
