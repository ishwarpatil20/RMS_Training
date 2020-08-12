using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Common;
using Common.AuthorizationManager;

public partial class EmployeeMenuUC : System.Web.UI.UserControl, IPostBackEventHandler
{
    /// <summary>
    /// 
    /// </summary>
    const string INDEX = "index";

    public event EventHandler BubbleClick;

    

    public string globalEmpId;
    string UserRaveDomainId;
    string UserMailId;
    string ResumeFormat = "~/ResumeTemplate/";
    ArrayList arrRolesForUser = new ArrayList();

    #region Properties

    /// <summary>
    /// Property for EMPLOYEEPREVIOUSCOUNT
    /// </summary>
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

    /// <summary>
    ///  Property for EMPLOYEEID
    /// </summary>
    public string EMPLOYEEID
    {
        get
        {
            if (ViewState["EMPLOYEEID"] != null)
            {
                return ViewState["EMPLOYEEID"].ToString();
            }
            else
            {
                return null;
            }
        }
        set
        {
            ViewState["EMPLOYEEID"] = value;
        }
    }

    #endregion Properties

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            this.SetEmployeeIndex();

        
        //for roles and login user id

        Rave.HR.BusinessLayer.MRF.MRFRoles mrfRoles = new Rave.HR.BusinessLayer.MRF.MRFRoles();
        // Get logged-in user's email id
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
        UserMailId = UserRaveDomainId.Replace("co.in", "com");
        AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
        //ArrayList arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
        arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
        
        if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
        {
            trResignationDetails.Visible = true;
            trEmployeeResume.Visible = true;
            
        }
        // 30212,30295-Ambar-03102011-made both the statement to upper case for checking
        else if (((BusinessEntities.Employee)(Session[SessionNames.EMPLOYEEDETAILS])).EmailId.ToString().ToUpper() == UserRaveDomainId.ToString().Replace("co.in", "com").ToUpper())
        {
          // 30212-Ambar-Start-03102011
          trResignationDetails.Visible = false;
          // 30212-Ambar-End-03102011  
          trEmployeeResume.Visible = true;            
        }
        else
        {
            //Ishwar 20022015 : NIS RMS : Start
            string strUserIdentity = string.Empty;
            strUserIdentity = HttpContext.Current.ApplicationInstance.Session["WindowsUsername"].ToString().Trim();
            BusinessEntities.Employee Employee = new BusinessEntities.Employee();
            Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
            Employee = employeeBL.GetNISEmployeeList(strUserIdentity);
            if (!String.IsNullOrEmpty(Employee.WindowsUserName))
            {
                trResignationDetails.Visible = true;
            }//Ishwar 20022015 : NIS RMS : End
            else
            {
                trResignationDetails.Visible = false;
                trEmployeeResume.Visible = false;
            }
        }
    }

    /// <summary>
    /// Function will call on Privious Click
    /// </summary>
    private void PreviousClick()
    {
        EMPLOYEECURRENTCOUNT = EMPLOYEECURRENTCOUNT - 1;
        EMPLOYEEPREVIOUSCOUNT = EMPLOYEEPREVIOUSCOUNT - 1;
        EMPLOYEENEXTCOUNT = EMPLOYEENEXTCOUNT + 1;

        EnableDisableButtons(EMPLOYEECURRENTCOUNT, EMPLOYEEPREVIOUSCOUNT, EMPLOYEENEXTCOUNT);
    }

    /// <summary>
    /// Function will call on Next Click
    /// </summary>
    private void NextClick()
    {
        //Response.Redirect("Home.aspx");
        EMPLOYEECURRENTCOUNT = EMPLOYEECURRENTCOUNT + 1;
        EMPLOYEEPREVIOUSCOUNT = EMPLOYEEPREVIOUSCOUNT + 1;
        EMPLOYEENEXTCOUNT = EMPLOYEENEXTCOUNT - 1;

        EnableDisableButtons(EMPLOYEECURRENTCOUNT, EMPLOYEEPREVIOUSCOUNT, EMPLOYEENEXTCOUNT);
    }

    /// <summary>
    /// Function will use to enable or disable previous or next Buttons
    /// </summary>
    /// <param name="currentIndex"></param>
    /// <param name="PreviousIndex"></param>
    /// <param name="NextIndex"></param>
    private void EnableDisableButtons(int currentIndex, int PreviousIndex, int NextIndex)
    {
        try
        {
            Hashtable ht = new Hashtable();
            if (Session[SessionNames.EMPLOYEEVIEWINDEX] != null)
            {
                //27633-Subhra-start
                //ht = (Hashtable)Session[SessionNames.EMPLOYEEVIEWINDEX];
                ht = (Hashtable)Session[SessionNames.EMPPREVIOUSHASHTABLE];
                // 27633-Subhra-end


                if (ht.Contains(PreviousIndex) == true)
                {
                    btnPrevious.Enabled = true;
                }
                else
                {
                    btnPrevious.Enabled = false;
                }

                if (ht.Contains(NextIndex) == true)
                {
                    btnNext.Enabled = true;
                }
                else
                {
                    btnNext.Enabled = false;
                }

                if (ht.Contains(currentIndex) == true)
                {
                    string empID = Convert.ToString(ht[currentIndex]);
                    //globalEmpId = empID;
                    EMPLOYEEID = empID;
                    BusinessEntities.Employee emp = new BusinessEntities.Employee();
                    emp.EMPId = Convert.ToInt32(empID);
                    Session[SessionNames.EMPLOYEEDETAILS] = emp;
                    //this.GetEmployee(emp);
                    //this.PopulateControls();

                    //if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString())
                    //{
                    //    btnEdit.Visible = false;
                    //}

                    //CopyMRFDetail(Convert.ToInt32(ht[currentIndex]));
                }


            }
            //To solved the issue no 19221
            //Comment by Rahul P 
            //Start
            //Check the Create session variable and check 
            //if they differ than change the session variale to view
            if (Session[SessionNames.PREVIOUS_EMP] != null)
            {
                if (Session[SessionNames.PREVIOUS_EMP].ToString() != Convert.ToString(ht[currentIndex]))
                {
                    Session[SessionNames.PAGEMODE] = "View";
                }
            }
            //End
        }
        //catch (RaveHRException ex)
        //{
        //    throw ex;
        //}
        catch (Exception ex)
        {
            //RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "EnableDisableButtons", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            //LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Preview Button Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        PreviousClick();
        OnBubbleClick(e);
    }

    /// <summary>
    /// Next Button CLick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        NextClick();
        OnBubbleClick(e);
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    OnBubbleClick(e);
    //}


    /// <summary>
    /// Fucntion will Set the Index of Page
    /// </summary>
    private void SetEmployeeIndex()
    {
        Hashtable htnew = new Hashtable();

        if (Request.QueryString[INDEX] != null)
        {   
            
            string temp = URLHelper.Clarify(Request.QueryString[INDEX]);
            int countIndex = Convert.ToInt32(temp);

            if (Session[SessionNames.EMPLOYEEVIEWINDEX] != null)
            {

                //27633 Subhra Start
                //htnew = (Hashtable)Session[SessionNames.EMPLOYEEVIEWINDEX];
                htnew = (Hashtable)Session[SessionNames.EMPPREVIOUSHASHTABLE];
            }

           EMPLOYEEID = URLHelper.Clarify(Request.QueryString[CommonConstants.EMP_ID]);
           // EMPLOYEEID = Convert.ToUInt32(DecryptQueryString(CommonConstants.EMP_ID));

           EMPLOYEECURRENTCOUNT = ((Convert.ToInt16(Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString()) - 1) * 10) + countIndex;
            
            EMPLOYEEPREVIOUSCOUNT = EMPLOYEECURRENTCOUNT - 1;
            EMPLOYEENEXTCOUNT = (htnew.Keys.Count - 2) - (((Convert.ToInt16(Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString()) - 1) * 10) + countIndex);
            //if (countIndex == 0)
            //{
            //    btnNext.Enabled = false;
            //    btnPrevious.Enabled = false;
            //}
            //if (htnew.Keys.Count == 1)
            //{
            //    btnNext.Visible = false;
            //    btnPrevious.Visible = false;
            //}
            //else if (EMPLOYEEPREVIOUSCOUNT == -1)
            //{
            //    btnNext.Visible = true;
            //    btnPrevious.Visible = false;
            //}
            //else if (EMPLOYEENEXTCOUNT == -1)
            //{
            //    btnNext.Visible = false;
            //    btnPrevious.Visible = true;
            //}

            if (htnew.Keys.Count == 1)
            {
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
            }
            else if (EMPLOYEEPREVIOUSCOUNT == -1)
            {
                btnNext.Enabled = true;
                btnPrevious.Enabled = false;
            }
            else if (EMPLOYEENEXTCOUNT == -1)
            {
                btnNext.Enabled = false;
                btnPrevious.Enabled = true;
            }
        }
        else
        {
            btnNext.Visible = false;
            btnPrevious.Visible = false;
        }
    }

    protected void OnBubbleClick(EventArgs e)
    {
        if (BubbleClick != null)
        {
            BubbleClick(this, e);
        }
    }

    #region LinkButton Events

    protected void lnkEmployeeDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeDetails.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeDetails.aspx");
        }
    }

    protected void lnkContactDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeContactDetails.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeContactDetails.aspx");
        }
    }

    protected void lnkQualificationDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeQualificationDetails.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeQualificationDetails.aspx");
        }
    }

    protected void lnkCertificationDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeCertification.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeCertification.aspx");
        }
    }

    protected void lnkSkillDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeSkillsDetails.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeSkillsDetails.aspx");
        }
    }

    protected void lnkProfCourses_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeProfessionalCourses.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeProfessionalCourses.aspx");
        }
    }

    protected void lnkProjectDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeProjectDetails.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString())  + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeProjectDetails.aspx");
        }
    }

    protected void lnkExperienceSummary_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeOrganization.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeOrganization.aspx");
        }
    }

    protected void lnkPassportDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeePassportDetails.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeePassportDetails.aspx");
        }
    }

    protected void lnkOtherDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeOtherDetails.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeOtherDetails.aspx");
        }
    }

    protected void lnkResignationDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("EmployeeResignationDetails.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("EmployeeResignationDetails.aspx");
        }
    }

    protected void lnkResumeTemplate_Click(object sender, EventArgs e)
    {
        int DeptId;
        //Issue : Different departments should have different CV template 
        //Name : Poonam Starts
        DropDownList a = (DropDownList)this.Parent.FindControl("ddlDepartment");
        if (a != null)
            DeptId = Convert.ToInt32(a.SelectedValue);
        else if(Session["DeptId"] !=null)
            DeptId = Convert.ToInt32(Session["DeptId"]);
        else
            DeptId = 1;

        if (DeptId == 16)
        {
            Response.Clear();
            Response.ContentType = "Application/msword";
            Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", "Rave CV template_BA.doc"));
            Response.Charset = "";
            Response.WriteFile(Server.MapPath(ResumeFormat + "Rave CV template_BA.doc"));
            Response.Flush();
            Response.End();
        }
        else if (DeptId == 17)
        {
            Response.Clear();
            Response.ContentType = "Application/msword";
            Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", "Resume Template_Usability.doc"));
            Response.Charset = "";
            Response.WriteFile(Server.MapPath(ResumeFormat + "Resume Template_Usability.doc"));
            Response.Flush();
            Response.End();
        }
        //Rakesh : Business Vertical Wise Resume Template Download 08/07/2016 Begin 
            /*1805	Service Delivery
        else if (DeptId == 1 && Session["BusinessVertical"].ToString().Contains(" Service Delivery"))                    */
        else if (DeptId == 1 && Session["BusinessVertical"].ToString()=="1805")
        {
            //Business Vertical Wise Resume Template Download  08/07/2016 End

            /*BusinessEntities.Employee employee = new BusinessEntities.Employee();
            employee.EMPId = Convert.ToInt32(Session["EmpId"]);*/

            Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

            int count = employeeBL.CheckEmployeeIsProjectManager(Convert.ToInt32(Session["EmpId"]));

            if (count != 0)
            {
                Response.Clear();
                Response.ContentType = "Application/msword";
                Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", "Rave CV template.doc"));
                Response.Charset = "";
                Response.WriteFile(Server.MapPath(ResumeFormat + "Rave CV template.doc"));
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.Clear();
                Response.ContentType = "Application/msword";
                Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", "Rave CV template.doc"));
                Response.Charset = "";
                Response.WriteFile(Server.MapPath(ResumeFormat + "Resume template_Service Delivery.doc"));
                Response.Flush();
                Response.End();
            }
        }

        //Rakesh : Business Vertical Wise Resume Template Download 08/07/2016 Begin 
        /* Procurement- 1939 ,NIS_NPS Finance & Accounting-1940
              else if (DeptId == 1 && (Session["BusinessVertical"].ToString().Contains("NIS_NPS Mumbai Procurement") || Session["BusinessVertical"].ToString().Contains("NIS_NPS Finance & Accounting - Mumbai"))) */
        else if (DeptId == 1 && (Session["BusinessVertical"].ToString()=="1939" || Session["BusinessVertical"].ToString()=="1940"))
        {
            // Rakesh : Business Vertical Wise Resume Template Download 08/07/2016 End
 
            Response.Clear();
            Response.ContentType = "Application/msword";
            Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", "Rave CV template.doc"));
            Response.Charset = "";
            Response.WriteFile(Server.MapPath(ResumeFormat + "Resume template_ESC_SharedServices.docx"));
            Response.Flush();
            Response.End();

        }
        else
        {
            Response.Clear();
            Response.ContentType = "Application/msword";
            Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}", "Rave CV template.doc"));
            Response.Charset = "";
            Response.WriteFile(Server.MapPath(ResumeFormat + "Rave CV template.doc"));
            Response.Flush();
            Response.End();
        }
        //Ends
    }

    protected void lnkResumeDetails_Click(object sender, EventArgs e)
    {
        if (EMPLOYEEID != null)
        {
            Response.Redirect("AddEmployeeResume.aspx?" + URLHelper.SecureParameters("EmpId", EMPLOYEEID) + "&" + URLHelper.SecureParameters("index", EMPLOYEECURRENTCOUNT.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(EMPLOYEEID, EMPLOYEECURRENTCOUNT.ToString(), "EMPLOYEESUMMERY"));
        }
        else
        {
            Response.Redirect("AddEmployeeResume.aspx");
        }
    }

    #endregion LinkButton Events

    #region IPostBackEventHandler Members

    public void RaisePostBackEvent(string eventArgument)
    {

        string temp = eventArgument.GetType().Name.ToString();
        //throw new NotImplementedException();


    }

    public System.Web.UI.Control GetPostBackControl(System.Web.UI.Page page)
    {
        Control control = null;
        string ctrlname = page.Request.Params["__EVENTTARGET"];
        if (ctrlname != null && ctrlname != String.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        // if __EVENTTARGET is null, the control is a button type and we need to 
        // iterate over the form collection to find it
        else
        {
            string ctrlStr = String.Empty;
            Control c = null;
            foreach (string ctl in page.Request.Form)
            {
                // handle ImageButton controls ...
                if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                {
                    ctrlStr = ctl.Substring(0, ctl.Length - 2);
                    c = page.FindControl(ctrlStr);
                }
                else
                {
                    c = page.FindControl(ctl);
                }
                if (c is System.Web.UI.WebControls.Button ||
                         c is System.Web.UI.WebControls.ImageButton)
                {
                    control = c;
                    break;
                }
            }
        }
        return control;
    }

    #endregion
}
