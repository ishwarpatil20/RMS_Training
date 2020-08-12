using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Domain.Entities;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using System.Configuration;
using System.IO;
using System.Data;
//using RMS.SSRSWebService;
using System.Threading.Tasks;
using RMS.Common.ExceptionHandling;
using RMS.Common.Constants;
namespace RMS.Controllers
{
    public class ReportsController : ErrorController
    {
        //
        // GET: /Reports/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GenerateReport()
        {
            ITABUWiseReportModel objITABUWise = new ITABUWiseReportModel();
            return View(objITABUWise);
        }

       
        [HttpPost]
        public ActionResult GenerateReport(ITABUWiseReportModel objITABUWiseModel)
        {
          //  ITABUWiseReportModel objITABUWise = new ITABUWiseReportModel();
            try
            {
                #region Declarations
                string encoding;
                string mimeType;
                string extension;
                string fileNamePath1="";
                string historyID = null;                
                Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution.Warning[] warnings = null;
                string[] streamIDs = null;
                string reportPath = "";
                string reportName = "ITABusinessVerticalWise";                
                string strGetCurrentDate = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.ToString("dd") + " " + DateTime.Now.Year.ToString();
                string strPath = ConfigurationManager.AppSettings["ReportsPath"].ToString() + strGetCurrentDate;

                byte[] result = null;
                string format = "EXCEL";
                ExecutionInfo execInfo = new ExecutionInfo();
                ExecutionHeader execHeader = new ExecutionHeader();
                ReportExecutionService rs = new ReportExecutionService();

                #endregion

                if (!Directory.Exists(strPath))
                {
                    //Create Reports Folder with Current Date
                    Directory.CreateDirectory(strPath);
                }
                //strPath = string.Format("{0}\\{1}_{2}", strPath, objITABUWiseModel.YearId, objITABUWiseModel.Quarter);
                
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                rs.Url = ConfigurationManager.AppSettings["ReportExecutionService2012"].ToString();
                reportPath = "/TrainingModuleReports/" + reportName;                
                RMS.Common.Master objMaster = new Common.Master();           
                System.Data.DataTable dt = objMaster.FillDropDownList("105");
                    
                rs.ExecutionHeaderValue = execHeader;
                execInfo = rs.LoadReport(reportPath, historyID);
                ParameterValue[] parametersBusinessverticals = null;             
              
                foreach (DataRow dr in dt.Rows)
                {
                    fileNamePath1 = strPath + "\\" + reportName + "_" + Convert.ToString(dr["MasterName"]).Trim() + ".xlsx";

                    if (!System.IO.File.Exists(fileNamePath1))
                    {
                        parametersBusinessverticals = new ParameterValue[3];
                        parametersBusinessverticals[0] = new ParameterValue();
                        parametersBusinessverticals[0].Name = "StartYear";
                        parametersBusinessverticals[0].Value = objITABUWiseModel.YearId;

                        parametersBusinessverticals[1] = new ParameterValue();
                        parametersBusinessverticals[1].Name = "Quarter";
                        parametersBusinessverticals[1].Value = objITABUWiseModel.Quarter;

                        parametersBusinessverticals[2] = new ParameterValue();
                        parametersBusinessverticals[2].Name = "BusinessVertical";
                        parametersBusinessverticals[2].Value = Convert.ToString(dr["MasterId"]);

                        rs.SetExecutionParameters(parametersBusinessverticals, "en-us");

                        rs.Timeout = 3600000;  //60 mins
                        result = rs.Render(format, null, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                        execInfo = rs.GetExecutionInfo();
                        using (FileStream stream = System.IO.File.Create(fileNamePath1, result.Length))
                        {
                            stream.Write(result, 0, result.Length);
                            stream.Close();
                        }
                    }
                }

                objITABUWiseModel.Message = string.Format("Reports Generated Sucessfully at Location  {0}", strPath);
                return View(objITABUWiseModel);
            }
             
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "ReportController", "GenerateReport", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }

           
        }
        public ActionResult GenerateAndDisplayReport()
        {
            //ReportViewer reportViewer = new ReportViewer();
            //reportViewer.ProcessingMode = ProcessingMode.Remote;

            //reportViewer.ServerReport.ReportPath = "/AdventureWorks 2012/Sales_by_Region";
            //reportViewer.ServerReport.ReportServerUrl = new Uri("http://localhost/ReportServer/");

            //ViewBag.ReportViewer = reportViewer;

            ReportViewer rpvReports = new Microsoft.Reporting.WebForms.ReportViewer();
            LocalReport localReport = new LocalReport();
            return View();

            System.Data.DataSet dsLocalReport = new System.Data.DataSet();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\YourLocalReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsLocalReport", dsLocalReport.Tables["SampleTable"]));

            ViewBag.ReportViewer = reportViewer;
        }

    }
}
