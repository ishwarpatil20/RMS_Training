<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="4CEmpAction.aspx.cs" Inherits="_4CEmpAction" Title="4C Action" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!--Umesh: Issue 'Modal Popup issue in chrome' Starts -->
    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/skinny.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.modalDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
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
                    $(this).val("Please Wait..");
                    $(this).attr('disabled', 'disabled');
                    //$(this).parents('#form').submit();
                });
            });
        })(jQuery);

        function func_AskUser() {
            // debugger;
            return confirm("Are you sure, you want to delete the record ?");
        }


        function Open4CDetailPopUp(strEmpId, departmentId, projectId, month, year, fbId, Mode, loginEmailId, empName) {
            jQuery.modalDialog.create({ url: "4CEmpDetailsPopup.aspx?EmpId=" + strEmpId + "&departmentId=" + departmentId + "&projectId=" + projectId + "&month=" + month + "&year=" + year + "&FBID=" + fbId + "&Mode=" + Mode + "&LoginEmailId= " + loginEmailId + "&EmpName=" + empName, maxWidth: 1270 }).open();
        }

        function Open4CActionPopUp(parameters) {
            //alert(parameters);
            jQuery.modalDialog.create({ url: '4CEmpDetailsPopup.aspx?' + parameters, maxWidth: 1400 }).open();
        }
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        //function readonlyTextBox(strSelectedRow) {
        function readonlyTextBox(e) {
            //            var row = parseInt(strSelectedRow) + 2;
            //            if (row <= 9) {
            //                //document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl0" + row + "_txtActionOwner").readonly = true;

            //                var otxt = document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl0" + row + "_txtActionOwner");
            //                var val = otxt.value;
            //                for (i = 0; i < val.length; i++) {
            //                    var code = val.charCodeAt(i);
            //                    if (!(code >= 65 && code <= 91) && !(code >= 97 && code <= 121) && !(code >= 48 && code <= 57)) {
            //                        otxt.value = ""; return;
            //                    } 
            //                } 
            //                
            //            }
            //            else {
            //                document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl" + row + "_txtActionOwner").readonly = true;
            //            }
            //            alert(strSelectedRow);

            //alert('enter');
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if ((unicode == 8) || (unicode == 9) || (unicode > 47 && unicode < 58)) {
                return true;
            }
            else {

                //window.alert("This field accepts only Numbers");
                return false;
            }
        }

        function check(elem) {

            var row = parseInt(elem) + 2;
            if (row <= 9) {
                document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl0" + row + "_ucDatePickerActualClosureDate_txtDate").value = "";
            }
            else {
                document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl" + row + "_ucDatePickerActualClosureDate_txtDate").value = "";
            }
        }


        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        function popUpEmployeeSearch(strSelectedRow) {
            var valReturned;
            jQuery.modalDialog.create({ url: "EmployeesList.aspx", maxWidth: 550,
            onclose: function(e) {
            valReturned = retVal;
                    var row = parseInt(strSelectedRow) + 2;
                    var EmpName;
                    var EmpId;
                    var employee = new Array();
                    if (retVal != undefined)
                        employee = valReturned.split(",");

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
                    if (EmpName == undefined) {
                    }
                    else {
                        if (row <= 9) {
                            if (EmpName != null && EmpName != "") {
                                document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl0" + row + "_txtActionOwner").value = EmpName;
                                document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl0" + row + "_HfActionOwner").value = EmpId;
                                document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl0" + row + "_HfActionOwnerName").value = EmpName;

                            }
                        }
                        else {
                            if (EmpName != null && EmpName != "") {
                                document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl" + row + "_txtActionOwner").value = EmpName;
                                document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl" + row + "_HfActionOwner").value = EmpId;
                                document.getElementById("ctl00_cphMainContent_grdEmpActionDetails_ctl" + row + "_HfActionOwnerName").value = EmpName;
                            }
                        }
                    }
                }
            }).open();
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }

    </script>

    <style type="text/css">
        .style1
        {
            height: 15.0pt;
            width: 117pt;
            color: white;
            font-size: 11.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #60497B;
        }
        .style2
        {
            height: 15.0pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #E5E0EC;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <div style="font-family: Verdana; font-size: 9pt;">
        <table width="100%">
            <tr>
                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <%--<span style="color: White; vertical-align: middle; font-size: 9pt; font-weight: bold">
                        Project Summary </span>--%>
                    <span class="header"> 
                        <asp:Label ID="lblEmpName" runat="server" ></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                    <asp:HiddenField ID="hdEmpId" runat="server" />
                    <asp:HiddenField ID="hdProjectId" runat="server" />
                    <asp:HiddenField ID="hdMonth" runat="server" />
                    <asp:HiddenField ID="hdYear" runat="server" />
                    <asp:HiddenField ID="hdRole" runat="server" />
                    <asp:HiddenField ID="hdFBId" runat="server" />
                    <asp:HiddenField ID="hdCurrentFBId" runat="server" />
                </td>
            </tr>
        </table>
        <table width="100%" class="detailsborder">
            <tr class="detailsbg">
                <td style="width: 22%">
                    <asp:Label ID="lblDept" runat="server" Text="Department -" Font-Bold="true"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlDepartment" runat="server" ToolTip="Select Department" Width="150px"
                        CssClass="mandatoryField">
                    </asp:DropDownList>
                </td>
                <td style="width: 30%">
                    &nbsp;&nbsp;
                    <asp:Label ID="lblProj" runat="server" Text="Project Name" Font-Bold="true"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlProjectList" runat="server" Width="250px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 15%">
                    <asp:Label ID="lblMan" runat="server" Text="Creator - " Font-Bold="true"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblManager" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    <asp:Label ID="lblReviewer" runat="server" Text="Reviewer -" Font-Bold="true"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblReviewerName" runat="server"></asp:Label>
                </td>
                <td colspan="1" align="center" style="width: 18%">
                    <table>
                        <tr>
                            <td style="width: 30%" align="center">
                                <asp:ImageButton ID="imgPrevious" Visible="false" runat="server" OnClick="imgPrevious_Click"
                                    CausesValidation="false" ImageUrl="~/Images/LeftArrow.jpg" Width="30px" Height="30px" />
                                <asp:ImageButton ID="imgNext" runat="server" Visible="false" OnClick="imgNext_Click"
                                    ImageUrl="~/Images/RightArrow.jpg" CausesValidation="false" Width="30px" Height="30px" />
                            </td>
                            <td style="width: 70%" align="center">
                                <asp:Label ID="lblDate" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <div id="divFinalRating" runat="server">
            <table width="100%">
                <tr>
                    <td align="left">
                        <div style="width: 100%;">
                            <asp:Label ID="Label1" runat="server" Text="Amber, Red - Please add remarks " Font-Size="Small"
                                Font-Names="Verdana"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" Text="Green - Please add remarks if required "
                                Font-Size="Small" Font-Names="Verdana"></asp:Label>
                            <table id="tblFinalRating" width="100%" class="detailsborder">
                                <tr>
                                    <td style="width: 80%">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="4" align="center" class="detailsbg">
                                                    <span style="font-weight: bold"> Final Rating </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="detailsbg">
                                                    <div id="divComp" runat="server" style="width: 125px;">
                                                        <b>Competency </b>
                                                    </div>
                                                </td>
                                                <td align="center" class="detailsbg">
                                                    <div id="divCommunication" runat="server" style="width: 125px;">
                                                        <b>Communication </b>
                                                    </div>
                                                </td>
                                                <td align="center" class="detailsbg">
                                                    <div id="divCommitment" runat="server" style="width: 125px;">
                                                        <b>Commitment </b>
                                                    </div>
                                                </td>
                                                <td align="center" class="detailsbg">
                                                    <div id="divCollaboration" runat="server" style="width: 125px;">
                                                        <b>Collaboration </b>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%-- Sanju:Issue Id 50201:Added onChange event--%>
                                                <td align="center" style="width: 20%">
                                                    <asp:DropDownList ID="ddlRAGCompetency" onchange="this.style.backgroundColor = this.options[this.selectedIndex].style.backgroundColor;"
                                                        runat="server">
                                                        <asp:ListItem Text="--Select--" Value="Select" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                        <asp:ListItem Text="Amber" Value="Amber"></asp:ListItem>
                                                        <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:DropDownList ID="ddlRAGCommunication" onchange="this.style.backgroundColor = this.options[this.selectedIndex].style.backgroundColor;"
                                                        runat="server">
                                                        <asp:ListItem Text="--Select--" Value="Select" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                        <asp:ListItem Text="Amber" Value="Amber"></asp:ListItem>
                                                        <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:DropDownList ID="ddlRAGCommitment" onchange="this.style.backgroundColor = this.options[this.selectedIndex].style.backgroundColor;"
                                                        runat="server">
                                                        <asp:ListItem Text="--Select--" Value="Select" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                        <asp:ListItem Text="Amber" Value="Amber"></asp:ListItem>
                                                        <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" style="width: 20%">
                                                    <asp:DropDownList ID="ddlRAGCollaboration" onchange="this.style.backgroundColor = this.options[this.selectedIndex].style.backgroundColor;"
                                                        runat="server">
                                                        <asp:ListItem Text="--Select--" Value="Select" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                        <asp:ListItem Text="Amber" Value="Amber"></asp:ListItem>
                                                        <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <%-- Sanju:Issue Id 50201:End    --%>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 20%" runat="server" id="tblReviewerRemarks">
                                        <table width="100%" border="0">
                                            <tr>
                                                <td align="center" id="tdReviewerRemarks" class="detailsbg" colspan="2">
                                                    <div id="divReviewerRemarks">
                                                        <b>Reviewer Remarks For Change of Rating </b>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" runat="server" id="tdReviewerRemarks">
                                                    <asp:TextBox ID="txtRemarksReviewer" runat="server" TextMode="MultiLine" MaxLength="50"
                                                        Width="280px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <asp:UpdatePanel ID="upDetails" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="divVal" runat="server">
                    <table width="100%">
                        <tr>
                            <td style="width: 80%;">
                                <asp:Label ID="lblMessage" runat="server" CssClass="text" Visible="false"></asp:Label>
                                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%--<div style="text-align:right">
            
            </div>
            <div class="detailsborder">--%>
                <div id="divActionDetails" runat="server">
                    <table width="100%">
                        <tr>
                            <td class="detailsborder">
                                <asp:GridView ID="grdEmpActionDetails" runat="server" GridLines="Both" Width="100%"
                                    AutoGenerateColumns="false" DataKeyNames="FBAID" OnRowDataBound="grdEmpActionDetails_RowDataBound"
                                    OnSelectedIndexChanged="grdEmpActionDetails_SelectedIndexChanged">
                                    <HeaderStyle CssClass="headerStyle" />
                                    <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Serial No" HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text="<%# ((GridViewRow)Container).RowIndex %>"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Four C's" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCType" runat="server" AutoPostBack="true" Width="100px"
                                                    OnSelectedIndexChanged="ddlCType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdActionId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"FBAID") %>' />
                                                <asp:HiddenField ID="hdActionFrom" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ActionComeFrom") %>' />
                                                <asp:HiddenField ID="hdParentFBAID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ParentFBAID") %>' />
                                                <%--<asp:RequiredFieldValidator ID="valActionC" runat="server" ErrorMessage="Please Select C." Text="*"  ControlToValidate="ddlCType" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Parameters" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCParameter" Width="200px" runat="server">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="valActionCParameter" runat="server" ErrorMessage="Please Select Parameter." Text="*"  ControlToValidate="ddlCParameter" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkDesc" runat="server" ImageUrl="~/Images/Edit.gif" CommandName="Desc"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FBAID") %>' OnCommand="lnk_Click">
                                                </asp:ImageButton>
                                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" MaxLength="50"
                                                    Text='<%#DataBinder.Eval(Container.DataItem,"Description") %>' Width="175px"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="valActionDesc" runat="server" ErrorMessage="Please enter decsription." Text="*"   ControlToValidate="txtDescription" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkAction" runat="server" ImageUrl="~/Images/Edit.gif" CommandName="Action"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FBAID") %>' OnCommand="lnk_Click">
                                                </asp:ImageButton>
                                                <asp:TextBox ID="txtAction" runat="server" TextMode="MultiLine" MaxLength="50" Width="175px"
                                                    CssClass="mandatoryField" Text='<%#DataBinder.Eval(Container.DataItem,"Action") %>'></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="valAction" runat="server" ErrorMessage="Please enter Action." Text="*"   ControlToValidate="txtAction" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Action Owner" HeaderStyle-Width="20%">
                                     <ItemTemplate>
                                        <asp:DropDownList ID="ddlActionOwner" runat="server">
                                        </asp:DropDownList>
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="left" />
                                  </asp:TemplateField> onclick="readonlyTextBox(this);"  onkeypress="retrun readonlyTextBox(this);"
                                   onkeyup="readonlyTextBox(this);"
                                  --%>
                                        <asp:TemplateField HeaderText="Action Owner" HeaderStyle-Width="17%" ItemStyle-Width="17%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtActionOwner" ReadOnly="true" runat="server" TextMode="MultiLine"
                                                    Text='<%#DataBinder.Eval(Container.DataItem,"ActionOwner") %>' />
                                                <asp:Image ID="imgActionOwner" runat="server" ImageUrl="~/Images/find.png" />
                                                <asp:HiddenField ID="HfActionOwner" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ActionOwnerId") %>' />
                                                <asp:HiddenField ID="HfActionOwnerName" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ActionOwner") %>' />
                                                <%--<asp:RequiredFieldValidator ID="valActionOwner" runat="server" ErrorMessage="Please enter Action Owner." Text="*"  ControlToValidate="txtActionOwner" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action Created Date" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActionCreatedDate" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Target Closure Date" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <uc1:DatePicker ID="ucDatePickerTragetClosureDate" runat="server" Width="80" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Closure Date" HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <uc1:DatePicker ID="ucDatePickerActualClosureDate" runat="server" Width="80" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemarks" runat="server" ImageUrl="~/Images/Edit.gif" CommandName="Remarks"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FBAID") %>' OnCommand="lnk_Click">
                                                </asp:ImageButton>
                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" MaxLength="50" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks") %>'
                                                    Width="175px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action Status" HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlActionStatus" runat="server" AutoPostBack="true" Width="100px"
                                                    OnSelectedIndexChanged="ddlActionStatus_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="HfActionStatus" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"ActionStatus")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDelete" ImageUrl="~/Images/Delete.gif" runat="server" OnClick="imgDelete_Click"
                                                    CausesValidation="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                </div>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup"
            PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="pnlPopup" runat="server" Width="400px" Style="display: none">
            <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="75%" border="3">
                        <tr>
                            <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                                background-color: #7590C8">
                                <span class="header"> 
                                           <asp:Label ID="lblModelPopupHeader" runat="server"></asp:Label>  </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Height="250px" Width="400px"></asp:TextBox>
                                <asp:HiddenField ID="hdRowId" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table width="75%" border="0">
                <tr>
                    <td style="width: 70%;" align="right">
                        <asp:Button ID="btnDescSave" runat="server" Text="Save" CssClass="button" OnClick="btnDescSave_Click"
                            Width="50px" />
                    </td>
                    <td style="width: 30%" align="left">
                        <asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" Width="50px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div id="divParameters" runat="server">
            <asp:HoverMenuExtender ID="hmeCompetency" runat="Server" TargetControlID="divComp"
                PopupControlID="PopupMenu" PopupPosition="Right" OffsetX="0" OffsetY="20" PopDelay="50" />
            <asp:Panel ID="PopupMenu" Width="150px" Height="100px" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                    width: 117pt" width="156">
                    <tr height="20">
                        <td class="style1" height="20" width="156">
                            Competency
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Dependable
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Skills Set
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Customer Facing
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Business Sense
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Maturity
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Problem Solving
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Task / Service Quality
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Timely Decision Making
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Out of Box Thinking
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Process Compliance
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HoverMenuExtender ID="hmeCommunication" runat="Server" TargetControlID="divCommunication"
                PopupControlID="pnlCommunication" PopupPosition="Right" OffsetX="0" OffsetY="20"
                PopDelay="50" />
            <asp:Panel ID="pnlCommunication" Width="150px" Height="100px" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                    width: 117pt" width="156">
                    <tr height="20">
                        <td class="style1" height="20" width="156">
                            Communication
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Verbal Fluency
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Clarity of Thought
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Grammatical Errors
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Written Fluency
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Listening and Connection
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Level of Details
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Tone
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Pro-activeness
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HoverMenuExtender ID="hmeCommitment" runat="Server" TargetControlID="divCommitment"
                PopupControlID="pnlCommitment" PopupPosition="Right" OffsetX="0" OffsetY="20"
                PopDelay="50" />
            <asp:Panel ID="pnlCommitment" Width="150px" Height="100px" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                    width: 117pt" width="156">
                    <tr height="20">
                        <td class="style1" height="20" width="156">
                            Commitment
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Alignment to office hours
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Timesheet completion
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Punctuality
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Alignment to Priorities
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Sense of Ownership
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Goes above and beyond
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Predictable
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Assured performance
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HoverMenuExtender ID="hmeCollaboration" runat="Server" TargetControlID="divCollaboration"
                PopupControlID="pnlCollaboration" PopupPosition="Right" OffsetX="-150" OffsetY="20"
                PopDelay="50" />
            <asp:Panel ID="pnlCollaboration" Width="150px" Height="100px" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                    width: 117pt" width="156">
                    <tr height="20">
                        <td class="style1" height="20" width="156">
                            Collaboration
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Team Player
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Role Model
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Supportive and Helping
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Motivating
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Situation Handling
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Conflict Resolution
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Approachable / Open to new ideas
                        </td>
                    </tr>
                    <tr height="20">
                        <td class="style2" height="20">
                            Accepts constructive feedback
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div id="divNoDataFound" runat="server" visible="false">
            <table width="100%" style="height: 100px">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblNoDataFoundMsg" runat="server" Text="No Data Found.!!!" Font-Bold="true"
                            ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <table id="Table1" border="0" width="100%">
            <tr>
                <td style="width: 70%">
                </td>
                <td style="width: 10%" align="right">
                    <%--<div id="divAddNewRow" runat="server">
              
                                       <table width="100%">
                                <tr>
                                    <td style="width:80%;">
                                        <%--<asp:Label ID="lblMessage" runat="server" CssClass="text" Visible="false"></asp:Label>
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                    </td>
                                    <td align="right" style="width:20%;">--%>
                    <asp:Button ID="btnAddNewRow" runat="server" Text="Add New Row" CssClass="button"
                        OnClick="btnAddNewRow_Click" CausesValidation="false" />
                    <asp:HiddenField ID="hdSendForReviewEnable" runat="server" />
                    <asp:HiddenField ID="hdSubmitratingEnable" runat="server" />
                    <%--</td>
                                </tr>
                            </table>
                                        
                                      </div>  --%>
                </td>
                <td style="width: 5%" align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />
                </td>
                <td style="width: 10%" align="left">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="false"
                        OnClientClick="return confirm('Do you wish to exit without saving?')" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" class="detailsborder">
            <tr>
                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <asp:Label ID="Label3" runat="server" Text="Dashboard" CssClass="header"></asp:Label></span>
                </td>
            </tr>
        </table>
        <table width="100%" class="detailsborder">
            <tr>
                <td>
                    <asp:GridView ID="grdEmpDashboard" runat="server" Width="100%" AllowPaging="False"
                        EmptyDataText="No Record Found." AutoGenerateColumns="false" DataKeyNames="EMPId"
                        OnRowDataBound="grdEmpDashboard_DataBound">
                        <HeaderStyle CssClass="headerStyle" />
                        <AlternatingRowStyle CssClass="alternatingrowStyleRP" />
                        <Columns>
                            <asp:TemplateField HeaderText="Serial No" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rating Month" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypRatingMonth" runat="server" Text='<%# string.Format("{0}-{1}", DataBinder.Eval(Container.DataItem,"RatingMonth"), DataBinder.Eval(Container.DataItem,"Year")) %>'
                                        CommandName="select"></asp:HyperLink>
                                    <%--<asp:LinkButton ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmployeeName") %>'  CommandName="select" ></asp:LinkButton>--%>
                                    <asp:HiddenField ID="hdEmpId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"EMPId") %>' />
                                    <asp:HiddenField ID="hdMonthDashboard" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Month") %>' />
                                    <asp:HiddenField ID="hdYearDashboard" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Year") %>' />
                                    <asp:HiddenField ID="hdProjectId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ProjectId") %>' />
                                    <asp:HiddenField ID="hdDepartmentId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"DepartmentId") %>' />
                                    <asp:HiddenField ID="hdFBID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"FBID") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Department" DataField="DEPTName" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Project Name" DataField="ProjectName" ItemStyle-Width="20%"
                                HeaderStyle-Width="10%">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Creator" DataField="Creator" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Reviewer" DataField="Reviewer" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Competency" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrdCompetency" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Competency") %>'></asp:Label>
                                    <asp:HiddenField ID="hdCompetency" Value='<%# DataBinder.Eval(Container.DataItem,"Competency") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Communication" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrdCommunication" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Communication") %>'></asp:Label>
                                    <asp:HiddenField ID="hdCommunication" Value='<%# DataBinder.Eval(Container.DataItem,"Communication") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commitment" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrdCommitment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Commitment") %>'></asp:Label>
                                    <asp:HiddenField ID="hdCommitment" Value='<%# DataBinder.Eval(Container.DataItem,"Commitment") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Collaboration" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrdCollaboration" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Collaboration") %>'></asp:Label>
                                    <asp:HiddenField ID="hdCollaboration" Value='<%# DataBinder.Eval(Container.DataItem,"Collaboration") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnDashboardHistory" runat="server" Text="More History.." OnClick="btnDashboardHistory_OnClick"
                        CssClass="button" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
