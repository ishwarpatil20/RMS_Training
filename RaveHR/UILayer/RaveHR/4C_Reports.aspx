<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="4C_Reports.aspx.cs" Inherits="FourCModule_4C_Reports" Title="4C Reports" %>

<%@ PreviousPageType VirtualPath="~/4CLogin.aspx" %>
<%@ Register Src="~/UIControls/DatePickerControl.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
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
            });
        })(jQuery);

        setbgToTab('ctl00_tab4C', 'ctl00_span4CReports');

        function popUpEmployeeSearch(strParameter, reportType) {
            jQuery.modalDialog.create({ url: 'EmployeesList.aspx?' + strParameter, maxWidth: 550,
                onclose: function(e) {
                    var EmpName;
                    var EmpId;
                    //Ishwar 09012015 Start
                    var valReturned;
                    valReturned = retVal;
                    //Ishwar 09012015 End
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

                        if (EmpName != null && EmpName != "") {

                            if (reportType == "CONSOLIDATED") {
                                jQuery('#<%=txtConsolidatedEmp.ClientID %>').val(EmpName);
                                jQuery('#<%=HfConsolidatedEmpId.ClientID %>').val(EmpId);
                                jQuery('#<%=HfConsolidatedEmpName.ClientID %>').val(EmpName);
                            }
                            else if (reportType == "ACTION") {
                                jQuery('#<%=txtActionEmp.ClientID %>').val(EmpName);
                                jQuery('#<%=HfActionEmpId.ClientID %>').val(EmpId);
                                jQuery('#<%=HfActionEmpName.ClientID %>').val(EmpName);
                            }
                            else if (reportType == "ACTION-ACTIONOWNER") {
                                jQuery('#<%=txtActionOwnerEmp.ClientID %>').val(EmpName);
                                jQuery('#<%=HfActionOwnerEmpId.ClientID %>').val(EmpId);
                                jQuery('#<%=HfActionOwnerEmpName.ClientID %>').val(EmpName);
                            }
                        }
                    }
                }
            }).open();
        }
    </script>
    <!--Umesh: Issue 'Modal Popup issue in chrome' Ends -->

    <div style="font-family: Verdana; font-size: 9pt;">
        <table width="100%">
            <tr>
                <td align="center" style="height: 25px; filter: progid:DXImageTransform.Microsoft.Gradient(endColorstr='#7590C8', startColorstr='#0C359A', gradientType='1');
                    background-color: #7590C8">
                    <span class="header">4C Reports</span>
                </td>
            </tr>
            <tr>
                <td style="height: 1pt">
                </td>
            </tr>
        </table>
        <table width="100%" runat="server" id="btlUnderConstruction" visible="false">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Under Construction..." Font-Bold="true"
                        Font-Size="XX-Large"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%" runat="server" id="tblReportMenu" visible="true">
            <tr>
                <td style="width: 20%" valign="top" id="tdmenulist" runat="server" visible="true">
                    <table width="100%" class="detailsborder">
                        <tr class="detailsbg">
                            <td align="center" style="width: 100%;" class="detailsborder">
                                <asp:Label ID="lblReportType" runat="server" Text="Report Type" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailsborder">
                                <asp:LinkButton ID="lnkConsolidated" runat="server" CssClass="employeeMenu" OnClick="lnkConsolidated_Click">Consolidated Report</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailsborder">
                                <asp:LinkButton ID="lnkAnalysis" runat="server" CssClass="employeeMenu" OnClick="lnkAnalysis_Click">Analysis Report</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailsborder">
                                <asp:LinkButton ID="lnkAction" runat="server" CssClass="employeeMenu" OnClick="lnkAction_Click">Action Report</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailsborder">
                                <asp:LinkButton ID="lnkStatus" runat="server" CssClass="employeeMenu" OnClick="lnkStatus_Click">Status Report</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailsborder">
                                <asp:LinkButton ID="lnkMovement" runat="server" CssClass="employeeMenu" OnClick="lnkMovement_Click">Movement Report</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="detailsborder">
                                <asp:LinkButton ID="lnkCountReport" runat="server" CssClass="employeeMenu" OnClick="lnkCountReport_Click">Count Report</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 80%" valign="top" class="detailsborder" visible="true">
                    <table width="100%" border="0" runat="server" id="Table1">
                        <tr>
                            <td class="detailsbg" colspan="4" align="center">
                                <asp:Label ID="lblReport" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" runat="server" id="tblMessage" visible="true">
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" runat="server" id="tblConsolidated">
                        <tr class="alternatingrowStyleRP">
                            <td>
                                <asp:Label ID="lblConsolidatedDept" runat="server" Text="Department"></asp:Label>
                            </td>
                            <td>
                                <asp:ListBox ID="lstConsolidatedDepartment" runat="server" Width="275px" SelectionMode="Multiple"
                                    AutoPostBack="true" OnSelectedIndexChanged="lstConsolidatedDepartment_SelectedIndexChanged">
                                </asp:ListBox>
                            </td>
                            <td>
                                <asp:Label ID="lblConsolidatedProject" runat="server" Text="Project"></asp:Label>
                            </td>
                            <td>
                                <asp:ListBox ID="lstConsolidatedProject" runat="server" SelectionMode="Multiple"
                                    Width="275px" Enabled="false"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <asp:Label ID="lblEmpName" runat="server" Text="Emp Name"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:TextBox ID="txtConsolidatedEmp" ReadOnly="true" Width="275px" runat="server"
                                    TextMode="MultiLine" />
                                <asp:Image ID="imgConsolidatedFind" runat="server" ImageUrl="~/Images/find.png" />
                                <asp:HiddenField ID="HfConsolidatedEmpId" runat="server" />
                                <asp:HiddenField ID="HfConsolidatedEmpName" runat="server" />
                            </td>
                            <td style="width: 15%">
                                <asp:Label ID="Label3" runat="server" Text="Employee Designation"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:ListBox ID="lstConsolidatedEmpDesignation" runat="server" Width="275px" SelectionMode="Multiple"
                                    Enabled="false"></asp:ListBox>
                            </td>
                        </tr>
                        <tr class="alternatingrowStyleRP">
                            <td style="width: 15%;">
                                <asp:Label ID="lblConsolidated" runat="server" Text="Month"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:DropDownList ID="ddlConsolidatedPeriod" runat="server">
                                    <asp:ListItem Text="1 Months" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="6 Months" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="9 Months" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="12 Months" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" runat="server" id="tblAnalysis">
                        <tr class="alternatingrowStyleRP">
                            <td>
                                <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label>
                            </td>
                            <td>
                                <asp:ListBox ID="lstAnalysisDepartment" runat="server" Width="275px" SelectionMode="Multiple"
                                    AutoPostBack="true" OnSelectedIndexChanged="lstAnalysisDepartment_SelectedIndexChanged">
                                </asp:ListBox>
                            </td>
                            <td>
                                <asp:Label ID="lblProject" runat="server" Text="Project"></asp:Label>
                            </td>
                            <td>
                                <asp:ListBox ID="lstAnalysisProject" runat="server" SelectionMode="Multiple" Width="275px"
                                    Enabled="false"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <asp:Label ID="lblPeriod" runat="server" Text="Month"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:DropDownList ID="ddlAnalysisPeriod" runat="server">
                                    <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="6 Months" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="9 Months" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="12 Months" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%">
                                <asp:Label ID="lblDesigNation" runat="server" Text="Employee Designation"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:ListBox ID="lstAnalysisEmpDesignation" runat="server" Width="275px" SelectionMode="Multiple"
                                    Enabled="false"></asp:ListBox>
                            </td>
                        </tr>
                        <tr class="alternatingrowStyleRP">
                            <td>
                                <asp:Label ID="lblFourC" runat="server" Text="Select C"></asp:Label>
                            </td>
                            <td>
                                <%--<asp:Panel ID="pnlCType" runat="server" Height="30px">--%>
                                <asp:CheckBoxList ID="chkCTypeAnalysis" runat="server">
                                </asp:CheckBoxList>
                                <%--</asp:Panel>--%>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" runat="server" id="tblAction">
                        <tr>
                            <td>
                                <asp:Label ID="lblActionDepartment" runat="server" Text="Department"></asp:Label>
                            </td>
                            <td>
                                <asp:ListBox ID="lstActionDepartment" runat="server" Width="275px" SelectionMode="Multiple"
                                    AutoPostBack="true" OnSelectedIndexChanged="lstActionDepartment_SelectedIndexChanged">
                                </asp:ListBox>
                            </td>
                            <td>
                                <asp:Label ID="lblActionProject" runat="server" Text="Project"></asp:Label>
                            </td>
                            <td>
                                <asp:ListBox ID="lstActionProject" runat="server" Width="275px" SelectionMode="Multiple"
                                    Enabled="false"></asp:ListBox>
                            </td>
                        </tr>
                        <tr class="alternatingrowStyleRP">
                            <td style="width: 15%;">
                                <asp:Label ID="lblActionEmpName" runat="server" Text="Emp Name"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:TextBox ID="txtActionEmp" ReadOnly="true" runat="server" Width="275px" TextMode="MultiLine" />
                                <asp:Image ID="imgActionFind" runat="server" ImageUrl="~/Images/find.png" />
                                <asp:HiddenField ID="HfActionEmpId" runat="server" />
                                <asp:HiddenField ID="HfActionEmpName" runat="server" />
                            </td>
                            <td style="width: 15%">
                                <asp:Label ID="lblActionOwner" runat="server" Text="Action Owner"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:TextBox ID="txtActionOwnerEmp" ReadOnly="true" Width="275px" runat="server"
                                    TextMode="MultiLine" />
                                <asp:Image ID="imgActionOwner" runat="server" ImageUrl="~/Images/find.png" />
                                <asp:HiddenField ID="HfActionOwnerEmpId" runat="server" />
                                <asp:HiddenField ID="HfActionOwnerEmpName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">
                                <asp:Label ID="lblActionPeriod" runat="server" Text="Duration"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:DropDownList ID="ddlActionPeriod" runat="server">
                                    <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="6 Months" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="9 Months" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="12 Months" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Select C"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkActionCType" runat="server">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr class="alternatingrowStyleRP">
                            <td>
                                <asp:Label ID="lblColorRating" runat="server" Text="Rating"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkActionColorRating" runat="server">
                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                    <asp:ListItem Text="Amber" Value="Amber"></asp:ListItem>
                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" runat="server" id="tblStatus">
                        <tr class="alternatingrowStyleRP">
                            <td style="width: 15%;">
                                <asp:Label ID="lblStatusMonth" runat="server" Text="Month"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:DropDownList ID="ddlStatusMonth" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%">
                                <asp:Label ID="lblStatusYear" runat="server" Text="Year"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:DropDownList ID="ddlStatusYear" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" runat="server" id="tblMovement">
                        <tr class="alternatingrowStyleRP">
                            <td style="width: 15%;">
                                <asp:Label ID="Label4" runat="server" Text="Duration"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:DropDownList ID="ddlMovementDuration" runat="server">
                                    <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="6 Months" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="9 Months" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="12 Months" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%">
                            </td>
                            <td style="width: 35%">
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" runat="server" id="tblCountReport">
                        <tr class="alternatingrowStyleRP">
                            <td style="width: 15%;">
                                <asp:Label ID="lblCount" runat="server" Text="Duration"></asp:Label>
                            </td>
                            <td style="width: 35%">
                                <asp:DropDownList ID="ddlCountReport" runat="server">
                                    <asp:ListItem Text="1 Months" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="6 Months" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="9 Months" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="12 Months" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%">
                            </td>
                            <td style="width: 35%">
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" runat="server" id="tblButton">
                        <tr>
                            <td style="width: 80%;" align="right">
                                <asp:Button ID="btnReports" runat="server" CssClass="button" Text="Generate Report"
                                    OnClick="btnReports_Click" />
                            </td>
                            <td style="width: 10%;" align="center">
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Reset" OnClick="btnClear_Click" />
                            </td>
                            <td style="width: 10%;" align="center">
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
</asp:Content>
