<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">

    <script src="JavaScript/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        setbgToTab('ctl00_tabRMSReports', 'ctl00_divReports');
        
        //Ishwar 15/01/2015 Start : Removed PDF, WORD, IMAGE, CSV, XML option in Export dropdown list
//        $(DocReady);
//        function DocReady() {
//            $('option[value = PDF]').remove();
//            $('option[value = WORD]').remove();
//            $('option[value = IMAGE]').remove();
//            $('option[value = CSV]').remove();
//            $('option[value = MHTML]').remove();
//            $('option[value = XML]').remove();
//            //EXCEL is default value
//            var myText = "EXCEL";
//            $('option[value="' + myText + '"]').prop('selected', true);
        //        }
//        $(document).ready(function() {
//            var sel = $("ctl00_cphMainContent_rpvReports_ctl01_ctl05_ctl00");
//            sel.find("option[value='PDF']").remove();
//            sel.find("option[value='WORD']").remove();
//            sel.find("option[value='IMAGE']").remove();
//            sel.find("option[value='CSV']").remove();
//            sel.find("option[value='MHTML']").remove();
//            sel.find("option[value='XML']").remove();
//        });
        //Ishwar 15/01/2015 End
</script>
 
    <br />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="detailsheader">
                Resource Management System :-Reports
            </td>
        </tr>
        <tr runat="server" id="tr_QuailityTeamReport" visible="false">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnProjectsDetails" runat="server" OnClick="btnProjectsDetails_Click"
                                Text="- Projects Details" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnCurrentPlusForeCastBench" runat="server" OnClick="btnCurrentPlusForeCastBench_Click"
                                Text="- Current Plus Forecasted Bench" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnResourceDetails" runat="server" OnClick="btnResourceDetails_Click"
                                Text="- Resource Details" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="BtnQualityReport1" runat="server" Text="- Proposed Data for Quality"
                                CssClass="txtstyle" OnClick="BtnQualityReport_Click" />
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="AllProjectDetails_Quality" runat="server" Text="- All Project Details (Open and Closed)"
                                CssClass="txtstyle" OnClick="BtnAllProjectDetails_Click" />
                        </td>
                    </tr>
                    
                    <!-- Siddharth 24 March 2015 Start-->
                     <!-- Logic:- To Show "Summary of MRF" to users who are in Quality Team-->
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButtonMRFSummary2" runat="server" OnClick="btnMRFSummary_Click" 
                            Text="- Summary of MRF" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <!-- Siddharth 24 March 2015 End-->
                    
                    
                </table>
            </td>
        </tr>
        
        <tr runat="server" id="tr_RecruitmentTeamReport" visible="false">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButtonMRFSummary" runat="server" OnClick="btnMRFSummary_Click" 
                            Text="- Summary of MRF" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <!-- Siddharth Sankolli 25-02-2015 Start-->
                    <!-- Logic:- To Show "Monthly MRF Recruitment Report" to users who are in Recruitment Team-->
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton14" runat="server" 
                                 Text="- Monthly MRF Recruitment Report"
                                CssClass="txtstyle" OnClick="BtnRecruitmentMRF_Click" />
                        </td>
                    </tr>
                    <!-- Siddharth Sankolli 25-02-2015 End-->
                </table>
            </td>
        </tr>               
        
        <tr runat="server" id="tr_RPMReports" visible="false">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <%--Ishwar 26022015 : Start --Desc : Report is not required
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnCurrentPlusForeCaseBenchResourceGroupWise" runat="server"
                                OnClick="btnCurrentPlusForeCaseBenchResourceGroupWise_Click" Text="- Current Plus Forecasted Bench Resource GroupWise"
                                CssClass="txtstyle" />
                        </td>
                    </tr>
                    Ishwar 26022015 : End--%>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnAllocationBillingReport" runat="server" OnClick="btnAllocationBillingReport_Click"
                                Text="- Allocation and Billing Bench" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnMRFSummary" runat="server" OnClick="btnMRFSummary_Click" Text="- Summary of MRF"
                                CssClass="txtstyle" />
                        </td>
                    </tr>
                    <tr>
                     <td>
                            <asp:LinkButton ID="LinkButton11" runat="server" 
                            OnClick="btnCurrentPlusForeCastBench_Click"
                                Text="- Current Plus Forecasted Bench" CssClass="txtstyle" />
                        </td>
                        </tr>
                    <%--Siddharth 26th August 2015 Start--%>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnEmployeeReportingManagerReport" runat="server" OnClick="btnEmployeeReportingManagerReport_Click"
                                Text="- Employee Reporting Manager Summary Report" CssClass="txtstyle" />
                        </td>
                    </tr>
                     <%--Siddharth 26th August 2015 End--%>
                   
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnResourceAllocationAgainstMRF" runat="server" OnClick="btnResourceAllocationAgainstMRF_Click"
                                Text="- Resource allocation against MRF" CssClass="txtstyle" />
                        </td>
                    </tr>
                    
                    
                    
                    
                    
                <%--Ishwar 26022015 : Start --Desc : Report is not required
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnPractiseTeamReport" runat="server" OnClick="btnPractiseTeamReport_Click"
                                Text="- Practice Team Report" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnRecruitMentSLAReport" runat="server" OnClick="btnRecruitMentSLAReport_Click"
                                Text="- Recruitment SLA Report" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnDemandSupplyNettingReport" runat="server" Text="- Demand Supply Netting"
                                CssClass="txtstyle" OnClick="btnDemandSupplyNettingReport_Click" />
                        </td>
                    </tr>
                    Ishwar 26022015 : End--%>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnResourcesHistoryProjectDetails" runat="server" Text="- Resources History Project Details"
                                CssClass="txtstyle" OnClick="btnResourcesHistoryProjectDetails_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="BtnQualityReport" runat="server" Text="- Proposed Data for Quality"
                                CssClass="txtstyle" OnClick="BtnQualityReport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="AllProjectDetails" runat="server" Text="- All Project Details (Open and Closed)"
                                CssClass="txtstyle" OnClick="BtnAllProjectDetails_Click" />
                        </td>
                    </tr>
                   <%--Ishwar 26022015 : Start --Desc : Report is not required
                    <tr>
                        <td>
                            <asp:LinkButton ID="WeeklyBenchReport" runat="server" Text="- Weekly Bench Report"
                                CssClass="txtstyle" OnClick="BtnWeeklyBenchReport_Click" />
                        </td>
                    </tr>
                    Ishwar 26022015 : End--%>
                    <tr>
                        <td>
                            <asp:LinkButton ID="SkillsDetails" runat="server" Text="- Employee Skills Details "
                                CssClass="txtstyle" OnClick="BtnSkillsDetails_Click" />
                        </td>
                    </tr>
                           
                    <%--Ishwar 26022015 : Start --Desc : Report is not required
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButtonContractDetails" runat="server" Text="- Contract Details "
                                CssClass="txtstyle" OnClick="BtnContractDetails_Click" />
                        </td>
                    </tr>
                    Ishwar 26022015 : End--%>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton6" runat="server" 
                              OnClick="btnActiveEmployeesDetails_Click" 
                            Text="- Active Employees Details" CssClass="txtstyle" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton9" runat="server" 
                                 Text="- Resigned Employees Details "
                                CssClass="txtstyle" OnClick="BtnResignedEmpDetails_Click" />
                        </td>
                    </tr>
                    
                    <%--Mohamed :  27/03/2014 : Starts                        			  
                    Desc : Report's added--%>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton12" runat="server" 
                                 Text="- Util and Billing Day Wise "
                                CssClass="txtstyle" OnClick="BtnUtilAndBillingDayWise_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton4" runat="server" 
                                 Text="- Util and Billing Day Wise Testing Department"
                                CssClass="txtstyle" OnClick="BtnUtilAndBillingDayWiseTesting_Click" />
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton13" runat="server" 
                                 Text="- Util and Billing Greater than 100"
                                CssClass="txtstyle" OnClick="BtnUtilBillingGreaterthan100_Click" />
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton16" runat="server" 
                                 Text="- Monthly MRF Recruitment Report"
                                CssClass="txtstyle" OnClick="BtnRecruitmentMRF_Click" />
                        </td>
                    </tr>
                   
                    <%--Mohamed : 27/03/2014 : Ends--%>
                    <%--Mohamed : Issue : 51365 : 05/09/2014 : Starts                         			  
                    Desc : Average Util billing for given period Report added--%>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton15" runat="server" 
                                 Text="- Average Util & Billing Report for given Period "
                                CssClass="txtstyle" OnClick="BtnAvgUtilBilling_Click" />
                        </td>
                    </tr>
                    <%--Mohamed : 05/09/2014 : Ends--%>
                </table>
            </td>
        </tr>
        
        <tr runat="server" id="tr_HRTeamReport" visible="false">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButtonActiveEmployeesDetails" runat="server" 
                              OnClick="btnActiveEmployeesDetails_Click" 
                            Text="- Active Employees Details" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <%--Ishwar 26022015 : Start --Desc : Report is not required
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton2" runat="server" 
                            OnClick="btnCurrentPlusForeCastBench_Click"
                                Text="- Current Plus Forecasted Bench" CssClass="txtstyle" />
                        </td>
                    </tr>
                    Ishwar 26022015 : End--%>
                    
                     <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton3" runat="server" Text="- Employee Skills Details "
                                CssClass="txtstyle" OnClick="BtnSkillsDetails_Click" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButtonResignedEmpDetails" runat="server" 
                                 Text="- Resigned Employees Details "
                                CssClass="txtstyle" OnClick="BtnResignedEmpDetails_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" 
                            Text="- Resources History Project Details"
                            CssClass="txtstyle" OnClick="btnResourcesHistoryProjectDetails_Click" />
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        
        <%-- Changes made for CR 27622-Ambar- Start  --%>
        
    <%--    Ishwar 26022015 : Start --Desc : Report is not required
        <tr runat="server" id="tr_SpecialReport" visible="false">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="btnAllocationBillingReport_Click"
                                Text="- Allocation and Billing Bench" CssClass="txtstyle" />
                        </td>
                    </tr> 
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton4" runat="server" 
                              OnClick="btnActiveEmployeesDetails_Click" 
                            Text="- Active Employees Details" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton5" runat="server" 
                            OnClick="btnCurrentPlusForeCastBench_Click"
                                Text="- Current Plus Forecasted Bench" CssClass="txtstyle" />
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton8" runat="server" 
                            Text="- Resources History Project Details"
                            CssClass="txtstyle" OnClick="btnResourcesHistoryProjectDetails_Click" />
                        </td>
                    </tr>                         
                </table>
            </td>
        </tr> 
        Ishwar 26022015 : End --%>  
        
        <%-- Changes made for CR 27622-Ambar- End  --%>
        
        <%-- Changes made for CR 36581-Ambar- Start  --%>
        
        <tr runat="server" id="tr_OrgChartDetailsReport" visible="false">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton10" runat="server" OnClick="btnOrgChartDetails_Click"
                                Text="- Org Chart Details" CssClass="txtstyle" />
                        </td>
                    </tr>                                            
                </table>
            </td>
        </tr>       
        
        <%--Siddhesh Arekar for Task ID is 55884 : 06/01/2015 : Start--%> 
        
        <tr runat="server" id="tr_MRFClosureStatus" visible="false">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnMRFClosureStatus_Click"
                                Text="- MRF Closure Status Report" CssClass="txtstyle" />
                        </td>
                    </tr>                                            
                </table>
            </td>
        </tr>       
        
        <%--Siddhesh Arekar for Task ID is 55884 : 06/01/2015 : End--%> 
        
        <%--Ishwar Patil NIS RMS : 29/10/2014 : Start--%> 
       <%-- <tr runat="server" id="tr_NISRMSReport" visible="false">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">--%>
                    <%--Mohamed :  14/10/2014 : Starts                        			  
                    Desc : Head Count Report added--%>
                    <%--<tr>
                        <td>
                            <asp:LinkButton ID="LinkButton16" runat="server" 
                                 Text="- Head Count Report "
                                CssClass="txtstyle" OnClick="BtnHeadCountReport_Click" />
                        </td>
                    </tr>--%>
                    <%--Mohamed : 14/10/2014 : Ends--%>
                   <%-- <tr>
                        <td>
                            <asp:LinkButton ID="LinkButtonMRFAgingReport" runat="server" OnClick="btnMRFAging_Click" 
                            Text="- MRF Aging Report" CssClass="txtstyle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="LinkButtonMRFAgingReportByOpenPosition" runat="server" OnClick="btnMRFAgingByOpenPosition_Click" 
                            Text="- MRF Aging Report For Open Positions" CssClass="txtstyle" />
                        </td>
                    </tr>--%>
                    <%--Ishwar Patil NIS RMS : 09/10/2014 : Start--%>
                    <%--<tr>
                        <td>
                            <asp:LinkButton ID="LBSkillsReport" runat="server" 
                                 Text="- Skills Report" CssClass="txtstyle" 
                                 OnClick="LBSkillsReport_Click"></asp:LinkButton>
                        </td>
                    </tr>--%>
                    <%--Ishwar Patil NIS RMS : 09/10/2014 : End--%>
              <%--  </table>
            </td>
        </tr>--%>
        <%--Ishwar Patil NIS RMS : 29/10/2014 : End--%> 
        
        <%-- Changes made for CR 36581-Ambar- End  --%>
        
        <tr>
            <td>
                <cc1:ReportViewer ID="rpvReports" runat="server" Height="300px" Width="70%"
                    ShowZoomControl="False" ShowRefreshButton="False"
                    ShowPromptAreaButton="False" ShowPrintButton="False" ShowDocumentMapButton="False"
                    ShowFindControls="False" ShowPageNavigationControls="False">
                </cc1:ReportViewer>
            </td>
        </tr>
        <tr style="height:5%">
        <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
