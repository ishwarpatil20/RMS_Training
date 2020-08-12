using RMS.Common.AuthorizationManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RMS.Reports
{
    public partial class RMSReports : System.Web.UI.Page
    {
        void Init()
        {
        ArrayList arrRolesForUser = new ArrayList();
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

            //--Add to session 
            

            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUser();
            string[] UserName;
            string UserDisplayName = null;
            string FinalUserName = null;
            char separator = Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["SplitCharacter"]);

            if (arrRolesForUser.Count == 0)
            {
                ////Response.Redirect(CommonConstants.UNAUTHORISEDUSER);
            }
            UserName = UserMailId.Split('@');
            if (UserName[0].Contains(separator.ToString()))
            {
                UserName = UserName[0].Split(separator);
                for (int i = 0; i < UserName.Length; i++)
                {
                    //UserName[i] = ConvertToUpper(UserName[i]);
                    UserDisplayName += UserName[i];

                    if (i < UserName.Length - 1)
                        UserDisplayName += ".";
                }
            }
            else
            {
                FinalUserName = UserName[0];//ConvertToUpper(UserName[0]);
                UserDisplayName = FinalUserName;
            }
            lblUser.Text = lblUser.Text + " " + UserDisplayName;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Init();
                string abc = Request.QueryString["id"];


                switch (abc)
                {
                    case "1":
                        lblReportName.Text = "Vendors List";
                        GetReport("VendorsList");
                        break;
                    case "3":
                        lblReportName.Text = "Individual Payment Sheet";
                        GetReport("IndivisualReportSheet");
                        break;
                    case "4":
                        lblReportName.Text = "Training Records Report";
                        GetReport("TrainingRecords");
                        break;
                    case "5":
                        lblReportName.Text = "NIS Training Report";
                        GetReport("NISTrainingReport");
                        break;
                    case "6":
                        lblReportName.Text = "Feedback Collation Report";
                        GetReport("FeedbackCollationReport");
                        break;
                    case "7":
                        lblReportName.Text = "Training Details Report";
                        GetReport("TrainingDetailsReport");
                        break;
                    case "8":
                        lblReportName.Text = "Training Trends Report";
                        GetReport("TrainingTrends");
                        break;
                    case "9":
                        lblReportName.Text = "Training Cost Sheet";
                        GetReport("TrainingCostSheet");
                        break;
                    case "10":
                        lblReportName.Text = "ITA Departmentwise Yearly Report";
                        GetReport("ITADepartmentWise");
                        break;
                    case "11":
                        lblReportName.Text = "ITA Department Report";
                        GetReport("ITADepartmentReport");
                        break;
                    case "12":
                        lblReportName.Text = "ITA BUwise Report";
                        GetReport("BUWiseReport");
                        break;

                    default:
                        break;
                }


                //if (abc == "1")
                //{
                //    btnTrainingList_Click(sender, e);
                //}
                //if (abc == "3")
                //{
                //    btnIndivisualPayment_Click(sender, e);
                //}
                //if (abc == "4")
                //{
                //    btnTrainingRecords_Click(sender, e);
                //}
                //if (abc == "5")
                //{
                //    btnNISTrainingList_Click(sender, e);
                //}
                //if (abc == "6")
                //{
                //    btnFeedbackCollationReport_Click(sender, e);
                //}
                //if (abc == "7")
                //{
                //    btnTrainingDetailsReport_Click(sender, e);
                //}
                //if (abc == "8")
                //{
                //    btnTrainingTrendsReport_Click(sender, e);
                //}
                //if (abc == "9")
                //{
                //    btnTrainingCostSheet_Click(sender, e);
                //}
                }
                

            }

        private void GetReport(string strReport)
        {
            try
            {

                rpvReports.ShowCredentialPrompts = false;

                //--Credentials
                //rpvReports.ServerReport.ReportServerCredentials = new ReportCredentials("username", "passwd", "domain");

                //--Processing mode
                rpvReports.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;

                //--Report Server
                rpvReports.ServerReport.ReportServerUrl = new System.Uri(ConfigurationManager.AppSettings["ReportServerURL"].ToString());
                //rpvReports.ServerReport.ReportServerUrl = new System.Uri("http://cu-489:8080/ReportServer/");

                //--Report dir
                rpvReports.ServerReport.ReportPath = "/TrainingModuleReports/" + strReport;
                //--report
                rpvReports.ServerReport.Refresh();
            }
            catch
            {
            
            }

        }

        //protected void btnTrainingList_Click(object sender, EventArgs e)
        //{
        //    GetReport("VendorsList");
        //}

        //protected void btnIndivisualPayment_Click(object sender, EventArgs e)
        //{
        //    GetReport("IndivisualReportSheet");
        //}

        //protected void btnTrainingRecords_Click(object sender, EventArgs e)
        //{
        //    GetReport("TrainingRecords");
        //}

        //protected void btnNISTrainingList_Click(object sender, EventArgs e)
        //{
        //    GetReport("NISTrainingReport");
        //}

        //protected void btnFeedbackCollationReport_Click(object sender, EventArgs e)
        //{
        //    GetReport("FeedbackCollationReport");
        //}

        //protected void btnTrainingDetailsReport_Click(object sender, EventArgs e)
        //{
        //    GetReport("TrainingDetailsReport");
        //}

        //protected void btnTrainingTrendsReport_Click(object sender, EventArgs e)
        //{
        //    GetReport("TrainingTrends");
        //}

        //protected void btnTrainingCostSheet_Click(object sender, EventArgs e)
        //{
        //    GetReport("TrainingCostSheet");
        //}

        //protected void btnITADepartmentwise_Click(object sender, EventArgs e)
        //{
        //    GetReport("ITADepartmentWise");
        //}

        //protected void btnITABUwise_Click(object sender, EventArgs e)
        //{
        //    GetReport("BUWiseReport");
        //}
    }
}