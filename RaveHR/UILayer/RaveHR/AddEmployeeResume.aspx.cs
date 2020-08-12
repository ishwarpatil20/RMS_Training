using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Common;
using Common.AuthorizationManager;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using Common.Constants;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;


public partial class AddEmployeeResume : BaseClass
{

    #region Private Viewstate

    private string EMPLOYEERESUME = "EmployeeResume";
    
    //19645-Ambar-Start
    public string str_deletedfile = null;

    #endregion

    #region Private Variable

    // 34617-Ambar-Start-20062012
    //private BusinessEntities.Employee employee;
    BusinessEntities.Employee employee = new BusinessEntities.Employee();
    // 34617-Ambar-End-20062012

    private int employeeID = 0;
    string UserRaveDomainId;
    string UserMailId;
    ArrayList arrRolesForUser = new ArrayList();
    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    string resumePhsicalPath = "~/Resumes/";
    //ConfigurationSettings.AppSettings["ResumePhsicalPath"];
    string EmailId = "";
    string ModifyDate = "";
    string ModifyBy = "";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";

    private string CLASS_NAME = "AddEmployeeResume";
    private string ResumesPath = "~/Resumes/";
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";

    #endregion

    #region  Property

    /// <summary>
    /// For saving the path of user control
    /// </summary>
    private string SavedControlVirtualPath
    {
        get
        {
            if (ViewState["saved"] == null ||
                (string)ViewState["saved"] == string.Empty)
                return null;
            return (string)ViewState["saved"];
        }
        set
        {
            ViewState["saved"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the employee resume collection.
    /// </summary>
    /// <value>The employee resume collection.</value>
    private BusinessEntities.RaveHRCollection EmployeeResumeCollection
    {
        get
        {
            if (ViewState[EMPLOYEERESUME] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[EMPLOYEERESUME]);
            else
                return null;
        }
        set
        {
            ViewState[EMPLOYEERESUME] = value;
        }
    }

    /// <summary>
    /// Gets or sets the EMPLOYEEPREVIOUSCOUNT.
    /// </summary>
    /// <value>The EMPLOYEEPREVIOUSCOUNT.</value>
    public int EMPLOYEEPREVIOUSCOUNT
    {
        get
        {
            if (ViewState["EMPLOYEEPREVIOUSCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["EMPLOYEEPREVIOUSCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["EMPLOYEEPREVIOUSCOUNT"] = value;
        }
    }

    /// <summary>
    /// Property for EMPLOYEENEXTCOUNT
    /// </summary>
    public int EMPLOYEENEXTCOUNT
    {
        get
        {
            if (ViewState["EMPLOYEENEXTCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["EMPLOYEENEXTCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["EMPLOYEENEXTCOUNT"] = value;
        }
    }

    /// <summary>
    ///  Property for EMPLOYEECURRENTCOUNT
    /// </summary>
    public int EMPLOYEECURRENTCOUNT
    {
        get
        {
            if (ViewState["EMPLOYEECURRENTCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["EMPLOYEECURRENTCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["EMPLOYEECURRENTCOUNT"] = value;
        }
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //clearing the text
        lblMessage.Text = string.Empty;

        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }

        // 34617-Ambar-Start-20062012
        // Check whether it is called for specific employee if yes then proceed with that employee profile
        if (Request.QueryString[CommonConstants.EMP_ID] != null)
        {
          Session[SessionNames.EMPLOYEEDETAILS] = null;
          employee.EMPId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.EMPID).ToString());
          employee = this.GetEmployee(employee);
          Session[SessionNames.EMPLOYEEDETAILS] = employee;
          //btnRelease.Attributes["onclick"] = "popUpEmployeeReleasePopulate('" + employee.EMPId + "')";
        }
        // 34617-Ambar-End-20062012

        if (Session[Common.SessionNames.EMPLOYEEDETAILS] != null)
        {
            employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];
        }

        if (employee != null)
        {
            employeeID = employee.EMPId;
            lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
        }

        if (!IsPostBack)
        {
            Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;
            this.PopulateGrid(employeeID);
        }

        // Get logged-in user's email id
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
        UserMailId = UserRaveDomainId.Replace("co.in", "com");
        arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

        SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
        ReloadControl();

    }

    /// <summary>
    /// Handles the Click event of the btnUpload control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        int i;
        string fileExtension = System.IO.Path.GetExtension(fileResume.FileName).ToLower();

        if (ValidateControls())
        {
            char[] SPILITER_DOT = { '.' };
            Rave.HR.BusinessLayer.Employee.EmployeeResume objEmployeeResumeBAL;
            BusinessEntities.EmployeeResume objEmployeeResume = new BusinessEntities.EmployeeResume();
            objEmployeeResumeBAL = new Rave.HR.BusinessLayer.Employee.EmployeeResume();
            //string[] EmployeeArray = Convert.ToString(employee.EmailId.Replace("@rave-tech.com", "")).Split(SPILITER_DOT);

            objEmployeeResume.EMPId = employee.EMPId;
            string docName = "";

            //Googleconfigurable
            AuthorizationManager obj = new AuthorizationManager();
            docName = obj.GetUsernameBasedOnEmail(employee.EmailId);

            //if (employee.EmailId.ToLower().Trim().Contains("@rave-tech.com"))
            //{
            //    docName = employee.EmailId.ToLower().Replace("@rave-tech.com", "");
            //}
            //else
            //{
            //    docName = employee.EmailId.ToLower().Replace("@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL, "");
            //}
            objEmployeeResume.DocumentName = docName;

            //objEmployeeResume.DocumentName = employee.EmailId.Replace("@rave-tech.com", "");
            //GoogleMail
            //Googleconfigurable
            objEmployeeResume.ModifyDate = DateTime.Now;
            objEmployeeResume.ModifyBy = obj.GetUsernameBasedOnEmail(UserRaveDomainId);
            //if (UserRaveDomainId != null && UserRaveDomainId.Contains("@rave-tech.co.in"))
            //{
            //    objEmployeeResume.ModifyBy = UserRaveDomainId.Replace("@rave-tech.co.in", "");
            //}
            //else if (UserRaveDomainId != null && UserRaveDomainId.ToLower().Contains("@" + AuthorizationManagerConstants.NORTHGATEDOMAIN))
            //{
            //    objEmployeeResume.ModifyBy = UserRaveDomainId.ToLower().Replace("@" + AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
            //}

            objEmployeeResume.FileExtension = fileExtension;

            if (EmployeeResumeCollection == null)
                EmployeeResumeCollection = new BusinessEntities.RaveHRCollection();

            EmployeeResumeCollection.Add(objEmployeeResume);

            EmployeeResumeCollection = objEmployeeResumeBAL.AddEmployeeResumeDetails(objEmployeeResume);

            i = Convert.ToInt32(((BusinessEntities.EmployeeResume)(EmployeeResumeCollection.Item(0))).ResumeCount);

            if (gvEmployeeResume.Rows.Count > 1)
            {
                string OldDocumentName = ((System.Web.UI.WebControls.LinkButton)(gvEmployeeResume.Rows[1].FindControl("_lnkResume"))).Text;
                HfOldDocumentName.Value = OldDocumentName;
            }
            this.PopulateGrid(objEmployeeResume.EMPId);

            switch (i)
            {
                case 0:
                    Upload(0, objEmployeeResume);
                    break;
                case 1:
                    Upload(1, objEmployeeResume);
                    break;
                case 2:
                    Upload(2, objEmployeeResume);
                    break;
            }

            lblMessage.Text = "Employee Resume Uploaded successfully.";

            //Aarohi : Issue 30053(CR) : 22/12/2011 : Start
            //Send mail for Employee Resume Upload.
            Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
            employeeBL.SendUploadResumeMails(employee);
            //Aarohi : Issue 30053(CR) : 22/12/2011 : End

        }

    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param 
    /// name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString[PAGETYPE] != null)
        {
            if (DecryptQueryString(PAGETYPE) == PAGETYPEEMPLOYEESUMMERY)
            {
                Response.Redirect("EmployeeDetails.aspx?" + URLHelper.SecureParameters("EmpId", employeeID.ToString()) + "&" + URLHelper.SecureParameters("index", Session["SelectedRow"].ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + "&" + URLHelper.CreateSignature(employeeID.ToString(), Session["SelectedRow"].ToString(), "EMPLOYEESUMMERY"));
            }
        }
        else
        {
            Response.Redirect(CommonConstants.PAGE_EMPLOYEEDETAILS, false);
        }
    }

    /// <summary>
    /// Handles the RowCommand event of the gvEmployeeResume control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
    protected void gvEmployeeResume_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            LinkButton lnkResume = (LinkButton)grv.FindControl("_lnkResume");
            Label FileExtension = (Label)grv.FindControl("lblExtension");

            if (lnkResume != null)
            {
                Response.Clear();
                Response.ContentType = "Application/msword";
                Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", lnkResume.Text));
                Response.Charset = "";
                Response.WriteFile(Server.MapPath(resumePhsicalPath + lnkResume.Text));
                Response.Flush();
                Response.End();

            }
        }

        //19645-Ambar-Start
        if (e.CommandName == "Delete")
        {
          GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
          LinkButton lnkResume = (LinkButton)grv.FindControl("_lnkResume");
          Label FileExtension = (Label)grv.FindControl("lblExtension");

          if (lnkResume != null)
          {
            str_deletedfile = lnkResume.Text;
            File.Delete(Server.MapPath(resumePhsicalPath + lnkResume.Text));

            if (!str_deletedfile.Contains("_old") )
            {
              string str_deletedfile_temp = str_deletedfile.Substring(0, str_deletedfile.Length - 4) + "_old" + str_deletedfile.Substring(str_deletedfile.Length - 4);

              if (File.Exists(Server.MapPath(resumePhsicalPath + str_deletedfile_temp)))
              {
                File.Copy(Server.MapPath(resumePhsicalPath + str_deletedfile_temp), Server.MapPath(resumePhsicalPath + str_deletedfile));
                File.Delete(Server.MapPath(resumePhsicalPath + str_deletedfile_temp));
              }
            }
            lblMessage.Text = "Employee Resume Deleted successfully.";

            //Aarohi : Issue 30053(CR) : 22/12/2011 : Start
            //Send mail for Employee Resume Delete.
            Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
            employeeBL.SendDeleteResumeMails(employee);
            //Aarohi : Issue 30053(CR) : 22/12/2011 : End
          }
        }
        //19645-Ambar-End
    }
   
    //19645-Ambar-Start
    protected void gvEmployeeResume_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

      Rave.HR.BusinessLayer.Employee.EmployeeResume objEmployeeDeleteResumeBAL;
      objEmployeeDeleteResumeBAL = new Rave.HR.BusinessLayer.Employee.EmployeeResume();

      objEmployeeDeleteResumeBAL.DeleteEmployeeResumeDetails(str_deletedfile, employee.EMPId);
      this.PopulateGrid(employee.EMPId);
    }
    //19645-Ambar-End


    #endregion

    #region Private Method

    /// <summary>
    /// Uploads the specified resume count.
    /// </summary>
    /// <param name="ResumeCount">The resume count.</param>
    /// <param name="ObjEmployeeResume">The obj employee resume.</param>
    private void Upload(int ResumeCount, BusinessEntities.EmployeeResume ObjEmployeeResume)
    {
        string tt = fileResume.HasFile.ToString();
        string NewDocumentName = ((System.Web.UI.WebControls.LinkButton)(gvEmployeeResume.Rows[0].FindControl("_lnkResume"))).Text;

        string Namefilename = lblempName.Text.Trim().ToString();
        string OldFileExtension = string.Empty;

        switch (ResumeCount + 1)
        {

            case 1:
                fileResume.SaveAs(Server.MapPath(resumePhsicalPath + NewDocumentName));
                break;

            case 2:
                OldFileExtension = ((System.Web.UI.WebControls.Label)(gvEmployeeResume.Rows[1].FindControl("lblExtension"))).Text;
                if (File.Exists(Server.MapPath(resumePhsicalPath + HfOldDocumentName.Value)))
                    File.Move(Server.MapPath(resumePhsicalPath + ObjEmployeeResume.DocumentName.ToString() + OldFileExtension), Server.MapPath(resumePhsicalPath + ObjEmployeeResume.DocumentName.ToString() + "_old" + OldFileExtension));
                fileResume.SaveAs(Server.MapPath(resumePhsicalPath + NewDocumentName));
                break;

            case 3:
                string OldDocumentName = ((System.Web.UI.WebControls.LinkButton)(gvEmployeeResume.Rows[1].FindControl("_lnkResume"))).Text;
                OldFileExtension = ((System.Web.UI.WebControls.Label)(gvEmployeeResume.Rows[1].FindControl("lblExtension"))).Text;
                
                if (File.Exists(Server.MapPath(resumePhsicalPath + HfOldDocumentName.Value)))
                    File.Delete(Server.MapPath(resumePhsicalPath + HfOldDocumentName.Value));
                if (File.Exists(Server.MapPath(resumePhsicalPath + ObjEmployeeResume.DocumentName.ToString() + OldFileExtension)))
                    File.Move(Server.MapPath(resumePhsicalPath + ObjEmployeeResume.DocumentName.ToString() + OldFileExtension), Server.MapPath(resumePhsicalPath + ObjEmployeeResume.DocumentName.ToString() + "_old" + OldFileExtension));
                fileResume.SaveAs(Server.MapPath(resumePhsicalPath + NewDocumentName));
                break;
        }
    }

    /// <summary>
    /// Reloads the control.
    /// </summary>
    private void ReloadControl()
    {
        PlaceHolder1.Controls.Clear();
        if (SavedControlVirtualPath != null)
        {
            Control control = this.LoadControl(SavedControlVirtualPath);
            if (control != null)
            {
                // Gives the control a unique ID. It is important to ensure
                // the page working properly. Here we use control.GetType().Name
                // as the ID.
                control.ID = control.GetType().Name;
                PlaceHolder1.Controls.Add(control);

                EmployeeMenuUC uc = control as EmployeeMenuUC;
                uc.BubbleClick += new EventHandler(BubbleControl_BubbleClick);
            }
        }
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    /// <returns></returns>
    private int PopulateGrid(int employeeID)
    {
        int i = 0;
        EmployeeResumeCollection = this.GetEmployeeResume(employeeID);
        gvEmployeeResume.DataSource = EmployeeResumeCollection;
        gvEmployeeResume.DataBind();

        EMPId.Value = employeeID.ToString().Trim();
        return i;

    }

    /// <summary>
    /// Handles the BubbleClick event of the BubbleControl control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void BubbleControl_BubbleClick(object sender, EventArgs e)
    {
        int i = 0;
        //this.PopulateControls();
        if (Session[SessionNames.EMPLOYEEDETAILS] != null)
        {
            employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
            employee = this.GetEmployee(employee);
            lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
            this.PopulateGrid(employee.EMPId);
        }

        Session[Common.SessionNames.EMPLOYEEDETAILS] = employee;


    }

    /// Gets the employee.
    /// </summary>
    /// <param name="employeeObj">The employee obj.</param>
    /// <returns></returns>
    private BusinessEntities.Employee GetEmployee(BusinessEntities.Employee employeeObj)
    {
        BusinessEntities.Employee empPar = null;
        empPar = new BusinessEntities.Employee();
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

        try
        {
            employee = employeeBL.GetEmployee(employeeObj);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return employee;
    }

    /// <summary>
    /// Gets the certification details.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetEmployeeResume(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.EmployeeResume objEmployeeResumeBAL;
        BusinessEntities.EmployeeResume objEmployeeResume;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objEmployeeResumeBAL = new Rave.HR.BusinessLayer.Employee.EmployeeResume();
            objEmployeeResume = new BusinessEntities.EmployeeResume();

            //objCertificationDetails.EMPId = 14;
            objEmployeeResume.EMPId = employeeID;

            raveHRCollection = objEmployeeResumeBAL.GetEmployeeResumeDetails(objEmployeeResume);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeResumeDetails", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }
    
    /// <summary>
    /// Enables a server control to perform final clean up before it is released from memory.
    /// </summary>
    public override void Dispose()
    {
        for (int i = 0; i < PlaceHolder1.Controls.Count; i++)
        {
            Control ctrl = PlaceHolder1.Controls[i];
            if (ctrl != null)
                ctrl.Dispose();
        }
        base.Dispose();
    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private Boolean ValidateControls()
    {
        Boolean Flag = false;
        Page page = HttpContext.Current.Handler as Page;

        if (fileResume.HasFile)
        {
            if (fileResume.FileBytes.Length > 2096927)
            {
                if (page != null)
                {
                    Flag = false;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please Upload the File whose size is Less than 2MB.');", true);
                }
            }
            else
            {
                string fileExtension = System.IO.Path.GetExtension(fileResume.FileName).ToLower();
                string[] allowedExtension = { ".doc", ".docx" };


                for (int i = 0; i < allowedExtension.Length; i++)
                {
                    if (fileExtension == allowedExtension[i])
                    {
                        Flag = true;
                    }
                }
            }
        }
        if (!Flag)
        {
            if (page != null)
            {
                Flag = false;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please Upload the valid File only in Doc and Docx format.');", true);
            }
        }
        return Flag;
    }

    #endregion


}
