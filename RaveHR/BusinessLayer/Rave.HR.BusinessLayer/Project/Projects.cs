//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Projects.cs       
//  Author:         vineet.kulkarni
//  Date written:   4/3/2009/ 4:37:30 PM
//  Description:    This class  provides the business layer methods for Project module.
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/3/2009 4:37:30 PM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------


using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BusinessEntities;
using Common;
using Rave.HR.BusinessLayer.Interface;
using Rave.HR.DataAccessLayer;
using Common.Constants;

namespace Rave.HR.BusinessLayer.Projects
{
    public class Projects
    {
        #region Private Members
        //Project Constants
        const string PROJECTS = "Projects.cs";
        const string SEND_MAIL_EDIT_PROJECT = "SendEmailForProjectEdited";
        const string SEND_MAIL_CLOSED_PROJECT = "SendEmailForProjectClosed";
        const string GET_HTML_PROJECT_EDITED = "GetHTMLForProjectEdited";
        const string GET_HTML_PROJECT_CLOSED = "GetHTMLForProjectClosed";

        #endregion

        public enum Mode
        {
            View,
            Update
        }
        #region Member Functions

        public DataTable GetUnFilteredApproveRejectProjectList()
        {

            Rave.HR.DataAccessLayer.Projects.Projects objApproveRejectProjectDAL;
            DataSet dsApproveRejectProject;
            DataTable dt = new DataTable();
            try
            {
                objApproveRejectProjectDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                dsApproveRejectProject = new DataSet();

                dsApproveRejectProject = objApproveRejectProjectDAL.GetPendingApprovalAndRejectedProjects();
                dt = dsApproveRejectProject.Tables[0];
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRProjects.cs", "GetUnFilteredApproveRejectProject", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            return dt;
        }

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>List</returns>
        //public List<BusinessEntities.Projects> getProjectName()
        public List<BusinessEntities.Projects> GetProjectNames()
        {
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objProjectNameDAL;
                List<BusinessEntities.Projects> lstProjectName;
                BusinessEntities.Projects objProjectName;
                DataSet dsProjectName;

                objProjectNameDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                lstProjectName = new List<BusinessEntities.Projects>();
                dsProjectName = new DataSet();
                objProjectName = null;

                //dsProjectName = objProjectNameDAL.getProjectName();
                dsProjectName = objProjectNameDAL.GetProjectNames();

                foreach (DataRow drProjectName in dsProjectName.Tables[0].Rows)
                {
                    objProjectName = new BusinessEntities.Projects();
                    objProjectName.ProjectName = drProjectName["ProjectName"].ToString();
                    objProjectName.ProjectId = int.Parse(drProjectName["ProjectID"].ToString());

                    lstProjectName.Add(objProjectName);
                }
                return lstProjectName;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRProjects.cs", "getProjectName", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Updates the existing project.
        /// </summary>
        /// <param name="objAddProject"></param>
        /// <returns></returns>
        public void UpdateProject(BusinessEntities.Projects objAddProject, string IsUpdated, string rmsURL, bool IsMailSentStatus)
        {
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objUpdateProjectDAL = new Rave.HR.DataAccessLayer.Projects.Projects();

                //return objUpdateProjectDAL.UpdateProject(objAddProject);
                objUpdateProjectDAL.UpdateProject(objAddProject, IsUpdated);

                if (objAddProject.ProjectStatus == Convert.ToInt32(MasterEnum.ProjectStatus.Closed).ToString())
                {

                    SendEmailForProjectClosed(objAddProject);
                    HttpContext.Current.Session["ConfirmationMessage"] = "Project " + objAddProject.ProjectName +
                        " is closed, email notification is sent for the same.";
                }
                else
                {
                    if (Convert.ToBoolean(IsUpdated) && Convert.ToBoolean(IsMailSentStatus)==true)
                    {  
                        SendEmailForProjectEdited(objAddProject, rmsURL);
                        HttpContext.Current.Session["ConfirmationMessage"] = "Project " + objAddProject.ProjectName 
                            + " is updated successfully, email notification is sent for the same.";
                    }
                    else
                    {
                        IsMailSentStatus = false;
                        HttpContext.Current.Session["ConfirmationMessage"] = "Project " + objAddProject.ProjectName
                            + " is updated successfully.";
                    }
                }


            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "UpdateProject", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// This Function is used to View Project Details.
        /// </summary>
        /// <param name="objViewProject"></param>
        /// <returns></returns>        
        public List<BusinessEntities.Projects> GetProjectDetails(BusinessEntities.Projects objViewProject, string SortDir, string SortExpression, string UserMailID, string PreSaleRole, string PMRole, string COORole, string RPMRole, string Action, string clientName, int projectStatusId, string projectsummaryprojectname)
        {
            try
            {
                List<BusinessEntities.Projects> objListViewProjectDetails = new List<BusinessEntities.Projects>();
                Rave.HR.DataAccessLayer.Projects.Projects objViewProjectDAL = new Rave.HR.DataAccessLayer.Projects.Projects();

                BusinessEntities.Projects objRaveHR = null;

                DataSet dsViewProjectDetails = new DataSet();

                dsViewProjectDetails = objViewProjectDAL.ViewProjectDetails(objViewProject, SortDir, SortExpression, UserMailID, PreSaleRole, PMRole, COORole, RPMRole, Action, clientName, projectStatusId, projectsummaryprojectname);

                foreach (DataRow drViewProjectDetails in dsViewProjectDetails.Tables[0].Rows)
                {
                    objRaveHR = new BusinessEntities.Projects();

                    objRaveHR.ProjectId = int.Parse(drViewProjectDetails["ProjectID"].ToString());
                    objRaveHR.ProjectCode = drViewProjectDetails["ProjectCode"].ToString();
                    objRaveHR.ClientName = drViewProjectDetails["ClientName"].ToString();
                    objRaveHR.ProjectName = drViewProjectDetails["ProjectName"].ToString();
                    objRaveHR.ProjectStatus = drViewProjectDetails["StatusID"].ToString();
                    objRaveHR.Location = drViewProjectDetails["Location"].ToString();

                    objRaveHR.Domain = drViewProjectDetails["Domain"].ToString();
                    objRaveHR.ProjectCategoryID = drViewProjectDetails["ProjectCategoryID"].ToString();

                    objRaveHR.StandardHours = drViewProjectDetails["StandardHours"].ToString();
                    objRaveHR.ProjectGroup = drViewProjectDetails["ProjectGroup"].ToString();

                    // Mohamed : Issue  : 26/09/2014 : Starts                        			  
                    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                    objRaveHR.ProjectDivision = String.IsNullOrEmpty(drViewProjectDetails["Division"].ToString()) ? 0 : Convert.ToInt32(drViewProjectDetails["Division"].ToString());
                    objRaveHR.ProjectBussinessArea = String.IsNullOrEmpty(drViewProjectDetails["BusinessArea"].ToString()) ? 0 : Convert.ToInt32(drViewProjectDetails["BusinessArea"].ToString());
                    objRaveHR.ProjectBussinessSegment = String.IsNullOrEmpty(drViewProjectDetails["BusinessSegment"].ToString()) ? 0 : Convert.ToInt32(drViewProjectDetails["BusinessSegment"].ToString());
                    //objRaveHR.ProjectAlias = drViewProjectDetails["ProjectAlias"].ToString();

                    // Mohamed : Issue  : 23/09/2014 : Ends


                    //Siddharth 13 March 2015 Start
                    objRaveHR.ProjectModel = drViewProjectDetails["ProjectModel"].ToString();
                    //Siddharth 13 March 2015 End

                    //Siddharth 3 August 2015 Start
                    objRaveHR.BusinessVertical = drViewProjectDetails["BusinessVertical"].ToString();
                    //Siddharth 3 August 2015 End

                    //Rakesh : HOD for Employees 12/July/2016 Begin
                    objRaveHR.ProjectHeadId = drViewProjectDetails["ProjectHeadId"].CastToInt32();
                    //Rakesh : HOD for Employees 12/July/2016 End

                    objRaveHR.Description = drViewProjectDetails["Description"].ToString();
                    if (!String.IsNullOrEmpty(drViewProjectDetails["StartDate"].ToString()))
                        objRaveHR.StartDate = DateTime.Parse(drViewProjectDetails["StartDate"].ToString());
                    if (!String.IsNullOrEmpty(drViewProjectDetails["EndDate"].ToString()))
                        objRaveHR.EndDate = DateTime.Parse(drViewProjectDetails["EndDate"].ToString());
                    objRaveHR.Reject = drViewProjectDetails["ReasonForRejection"].ToString();
                    objRaveHR.Delete = drViewProjectDetails["ReasonForDeletion"].ToString();
                    objRaveHR.OnGoingProjectStatusID = drViewProjectDetails["OnGoingProjectStatusID"].ToString();

                    foreach (DataRow drViewProjectDetails1 in dsViewProjectDetails.Tables[1].Rows)
                    {
                        objRaveHR.FirstName = drViewProjectDetails1["FullName"].ToString();
                        objRaveHR.EmailIdOfPM = drViewProjectDetails1["EmailId"].ToString();
                    }

                    DataRelation drelCategoryTechnology = new DataRelation("Category_Technology", dsViewProjectDetails.Tables[2].Columns["ID"], dsViewProjectDetails.Tables[3].Columns["CategoryID"]);

                    dsViewProjectDetails.Relations.Add(drelCategoryTechnology);

                    foreach (DataRow drCategory in dsViewProjectDetails.Tables[2].Rows)
                    {
                        Category category = new Category();
                        category.CategoryId = int.Parse(drCategory["ID"].ToString());
                        category.CategoryName = drCategory["Category"].ToString();
                        objRaveHR.Categories.Add(category);
                        foreach (DataRow drTechnology in drCategory.GetChildRows(drelCategoryTechnology))
                        {
                            category.Technologies.Add(new Technology(int.Parse(drTechnology["ID"].ToString()), drTechnology["TechnologyName"].ToString(), int.Parse(drTechnology["ID"].ToString())));
                        }

                    }

                    DataRelation drelDomainSubDomain = new DataRelation("Domain_SubDomain", dsViewProjectDetails.Tables[4].Columns["ID"], dsViewProjectDetails.Tables[5].Columns["DomainID"]);

                    dsViewProjectDetails.Relations.Add(drelDomainSubDomain);

                    foreach (DataRow drDomain in dsViewProjectDetails.Tables[4].Rows)
                    {
                        Domain domain = new Domain();
                        domain.DomainId = int.Parse(drDomain["ID"].ToString());
                        domain.DomainName = drDomain["Domain"].ToString();
                        objRaveHR.LstDomain.Add(domain);
                        foreach (DataRow drSubDomain in drDomain.GetChildRows(drelDomainSubDomain))
                        {
                            domain.lstSubDomain.Add(new SubDomain(int.Parse(drSubDomain["ID"].ToString()), int.Parse(drSubDomain["DomainID"].ToString()), drSubDomain["SubDomainName"].ToString()));
                        }
                    }

                    foreach (DataRow drWorkFlowStatus in dsViewProjectDetails.Tables[6].Rows)
                    {
                        objRaveHR.WorkFlowStatus = drWorkFlowStatus["Code"].ToString();
                    }

                    objListViewProjectDetails.Add(objRaveHR);
                }
                return objListViewProjectDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRProjects.cs", "GetProjectDetails", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
        }

        public List<BusinessEntities.Projects> GetProjectSummaryForPageLoad(BusinessEntities.ProjectCriteria objProjectCriteria, int pageSize, ref int pageCount, bool setPageing)
        {
            List<BusinessEntities.Projects> objProjectSummary = new List<BusinessEntities.Projects>();
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objGetProjectSummaryDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                objProjectSummary = objGetProjectSummaryDAL.GetProjectSummaryForPageLoad(objProjectCriteria, pageSize, ref pageCount, setPageing);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRProjects.cs", "GetProjectSummaryForPageLoad", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            return objProjectSummary;
        }

        public DataTable GetUnfilteredProjectSummaryList(string UserMailId,
                                                     string COORole,
                                                     string PresalesRole,
                                                     string PMRole,
                                                     string RPMRole)
        {
            Rave.HR.DataAccessLayer.Projects.Projects objGetProjectSummaryDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
            DataSet dsProjectSummary = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dsProjectSummary = objGetProjectSummaryDAL.GetProjectSummaryForPageLoad(UserMailId, COORole, PresalesRole, PMRole, RPMRole);
                dt = dsProjectSummary.Tables[0];
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "GetUnfilteredProjectSummaryList", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            return dt;
        }

        public List<BusinessEntities.Projects> GetProjectSummaryForFilter(BusinessEntities.Projects objGetProjectSummary, BusinessEntities.Master objGetProjectStatus, BusinessEntities.ProjectCriteria objProjectCriteria, int pageSize, ref int pageCount, bool setPageing)
        {
            List<BusinessEntities.Projects> objProjectSummary = new List<BusinessEntities.Projects>();
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objGetProjectSummaryDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                objProjectSummary = objGetProjectSummaryDAL.GetProjectSummaryForFilter(objGetProjectSummary, objGetProjectStatus, objProjectCriteria, pageSize, ref pageCount, setPageing);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "GetProjectSummaryForFilter", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            return objProjectSummary;
        }


        public DataTable GetFilteredProjectSummaryList(BusinessEntities.Projects objGetProjectSummary, BusinessEntities.Master objGetProjectStatus, string UserMailId, string COORole, string PresalesRole, string PMRole, string RPMRole)
        {
            Rave.HR.DataAccessLayer.Projects.Projects objGetProjectSummaryDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
            DataSet dsProjectSummary = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dsProjectSummary = objGetProjectSummaryDAL.GetProjectSummaryForFilter(objGetProjectSummary, objGetProjectStatus, UserMailId, COORole, PresalesRole, PMRole, RPMRole);
                dt = dsProjectSummary.Tables[0];
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "GetFilteredProjectSummaryList", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            return dt;
        }

        /// <summary>
        /// Gets Client Name
        /// </summary>
        /// <returns>List</returns>
        //public List<BusinessEntities.Projects> GetClientName(string UserMailId, string RPGRole, string COORole, string PresalesRole, string PMRole, string RPMRole)
        //public List<BusinessEntities.Projects> GetClientName(string UserMailId, string COORole, string PresalesRole, string PMRole, string RPMRole)
        public List<BusinessEntities.Projects> GetClientName(BusinessEntities.ProjectCriteria objProjectCriteria)
        {
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objClientNameDAL;
                List<BusinessEntities.Projects> lstClientName;

                objClientNameDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                lstClientName = new List<BusinessEntities.Projects>();

                lstClientName = objClientNameDAL.GetClientName(objProjectCriteria);
                return lstClientName;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "GetClientName", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Added by Subhra : Issue Id 27631 Start
        /// To get Client name as per selected ProjectStatus
        /// </summary>
        /// <returns></returns>
        public List<BusinessEntities.Projects> GetClientNameAsPerStatus(BusinessEntities.ProjectCriteria objProjectCriteria)
        {
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objClientNameDAL;
                List<BusinessEntities.Projects> ClientName;
                objClientNameDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                ClientName = new List<BusinessEntities.Projects>();
                ClientName = objClientNameDAL.GetClientNameAsPerStatus(objProjectCriteria);
                return ClientName;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "GetClientNameAsPerStatus", EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }

        }
        //Subhra End


        public List<Category> TechnologyCategory()
        {
            List<BusinessEntities.Category> lstTechnologyCategory = null;
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objTechnologyCategoryDAL = new Rave.HR.DataAccessLayer.Projects.Projects();

                DataTable dtTechnologyCategory = new DataTable();
                dtTechnologyCategory = objTechnologyCategoryDAL.TechnologyCategory();

                lstTechnologyCategory = new List<BusinessEntities.Category>();
                BusinessEntities.Category objTechnologyCategory = null;
                foreach (DataRow drTechnologyCategory in dtTechnologyCategory.Rows)
                {
                    objTechnologyCategory = new BusinessEntities.Category();
                    objTechnologyCategory.CategoryName = drTechnologyCategory["Category"].ToString();
                    objTechnologyCategory.CategoryId = int.Parse(drTechnologyCategory["ID"].ToString());
                    lstTechnologyCategory.Add(objTechnologyCategory);
                }

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "TechnologyCategory", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }

            return lstTechnologyCategory;
        }

        public List<Technology> Technology(int CategoryID)
        {
            List<BusinessEntities.Technology> lstTechnology = new List<BusinessEntities.Technology>();
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objTechnologyDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                DataTable dtTechnology = new DataTable();
                dtTechnology = objTechnologyDAL.Technology(CategoryID);

                BusinessEntities.Technology objTechnology = null;
                foreach (DataRow drTechnology in dtTechnology.Rows)
                {
                    objTechnology = new BusinessEntities.Technology(0, null, CategoryID);
                    objTechnology.TechnolgoyName = drTechnology["TechnologyName"].ToString();
                    objTechnology.TechnologyID = int.Parse(drTechnology["ID"].ToString());
                    lstTechnology.Add(objTechnology);
                }
                //return lstTechnology;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "Technology", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }

            return lstTechnology;
        }

        /// <summary>
        /// Gets the Domain Name
        /// </summary>
        /// <returns></returns>
        public List<Domain> GetDomainName()
        {
            List<BusinessEntities.Domain> lstDomain = null;
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objDomainDAL = new Rave.HR.DataAccessLayer.Projects.Projects();

                DataTable dtDomain = new DataTable();
                dtDomain = objDomainDAL.GetDomainName();

                lstDomain = new List<BusinessEntities.Domain>();
                BusinessEntities.Domain objDomain = null;
                foreach (DataRow drDomain in dtDomain.Rows)
                {
                    objDomain = new BusinessEntities.Domain();
                    objDomain.DomainName = drDomain["Domain"].ToString();
                    objDomain.DomainId = int.Parse(drDomain["ID"].ToString());
                    lstDomain.Add(objDomain);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "GetDomain", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            return lstDomain;
        }

        /// <summary>
        /// Gets the Sub Domain based upon selected Domain 
        /// </summary>
        /// <param name="domainId">int</param>
        /// <returns>List</returns>
        public List<SubDomain> GetSubDomain(int domainId)
        {
            List<BusinessEntities.SubDomain> lstSubDomain = null;
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objSubDomainDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                DataTable dtSubDomain = new DataTable();
                dtSubDomain = objSubDomainDAL.GetSubDomain(domainId);

                lstSubDomain = new List<BusinessEntities.SubDomain>();
                BusinessEntities.SubDomain objSubDomain = null;
                foreach (DataRow drSubDomain in dtSubDomain.Rows)
                {
                    objSubDomain = new BusinessEntities.SubDomain();
                    objSubDomain.SubDomainName = drSubDomain["SubDomainName"].ToString();
                    objSubDomain.SubDomainId = int.Parse(drSubDomain["ID"].ToString());
                    lstSubDomain.Add(objSubDomain);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "GetCheckedDomainName", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            return lstSubDomain;
        }

        /// <summary>
        /// This Function is used to retrieve Project Details after Project is added
        /// </summary>
        /// <param name="objViewProject"></param>
        /// <returns></returns>
        public BusinessEntities.Projects RetrieveProjectDetails(int ProjectID)
        {
            try
            {
                List<BusinessEntities.Projects> objListRetrieveProjectDetails = new List<BusinessEntities.Projects>();
                Rave.HR.DataAccessLayer.Projects.Projects objRetrieveProjectDAL = new Rave.HR.DataAccessLayer.Projects.Projects();

                BusinessEntities.Projects objRaveHR = null;

                DataSet dsRetrieveProjectDetails = new DataSet();
                dsRetrieveProjectDetails = objRetrieveProjectDAL.RetrieveProjectDetails(ProjectID);

                foreach (DataRow drRetrieveProjectDetails in dsRetrieveProjectDetails.Tables[0].Rows)
                {
                    objRaveHR = new BusinessEntities.Projects();

                    objRaveHR.ClientName = drRetrieveProjectDetails["ClientName"].ToString();
                    objRaveHR.ProjectName = drRetrieveProjectDetails["ProjectName"].ToString();
                    objRaveHR.ProjectStatus = drRetrieveProjectDetails["CreatedBy"].ToString();
                    objRaveHR.CreatedBy = drRetrieveProjectDetails["MailId"].ToString();
                    objRaveHR.CreatedByFullName = drRetrieveProjectDetails["CreatedByFullName"].ToString();
                    objRaveHR.ProjectCode = drRetrieveProjectDetails["ProjectCode"].ToString();
                    objRaveHR.StartDate = Convert.ToDateTime(drRetrieveProjectDetails["StartDate"].ToString());
                    objRaveHR.EndDate = Convert.ToDateTime(drRetrieveProjectDetails["EndDate"].ToString());
                }
                return objRaveHR;
                //return dsRetrieveProjectDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "RetrieveProjectDetails", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the Project Search results based on search criteria
        /// </summary>
        /// <returns>List</returns>
        public List<BusinessEntities.Projects> GetProjectSearchResult(string strKeyword)
        {
            try
            {
                List<BusinessEntities.Projects> objListRetrieveProjectDetails = new List<BusinessEntities.Projects>();
                Rave.HR.DataAccessLayer.Projects.Projects objRetrieveProjectDAL = new Rave.HR.DataAccessLayer.Projects.Projects();

                BusinessEntities.Projects objRaveHR = null;

                DataSet dsRetrieveProjectDetails = new DataSet();
                dsRetrieveProjectDetails = objRetrieveProjectDAL.GetProjectSearchResult(strKeyword);

                foreach (DataRow drRetrieveProjectDetails in dsRetrieveProjectDetails.Tables[0].Rows)
                {
                    objRaveHR = new BusinessEntities.Projects();
                    objRaveHR.ID = int.Parse(drRetrieveProjectDetails["ProjectID"].ToString());
                    objRaveHR.ClientName = drRetrieveProjectDetails["ClientName"].ToString();
                    objRaveHR.ProjectName = drRetrieveProjectDetails["ProjectName"].ToString();
                    objRaveHR.Location = drRetrieveProjectDetails["Location"].ToString();
                    objRaveHR.Category = drRetrieveProjectDetails["Category"].ToString();
                    objRaveHR.TechnologyName = drRetrieveProjectDetails["TechnologyName"].ToString();
                    objRaveHR.StartDate = DateTime.Parse(drRetrieveProjectDetails["StartDate"].ToString());
                    objRaveHR.EndDate = DateTime.Parse(drRetrieveProjectDetails["EndDate"].ToString());
                    objRaveHR.ProjectStartYear = int.Parse(drRetrieveProjectDetails["StartYear"].ToString());
                    objRaveHR.ProjectEndYear = int.Parse(drRetrieveProjectDetails["EndYear"].ToString());

                    objListRetrieveProjectDetails.Add(objRaveHR);
                }
                return objListRetrieveProjectDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRProjects.cs", "GetProjectSearchResult", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Sends Email when project Status is closed
        /// </summary>
        /// <param name="objAddProject"></param>
        /// <returns></returns>
        public void SendEmailForProjectEdited(BusinessEntities.Projects objProjectDetails, string rmsURL)
        {
            try
            {

                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                string strProjectStatus = string.Empty;
                string projectManagerEmail = string.Empty;
                string strProjectStatusFlag = objProjectDetails.ProjectStatus;
                string sComp = Utility.GetUrl();
                string strProjectSummaryLink = sComp +
                                            CommonConstants.ADDPROJECT_PAGE +
                                            "?" +
                                            rmsURL;


                DataAccessLayer.Projects.Projects objProjectsDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                BusinessEntities.Projects objProjectDetailsUpdated = new BusinessEntities.Projects();
                objProjectDetailsUpdated = objProjectsDAL.GetEditedProjectDetails(objProjectDetails);

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Projects),
                                           Convert.ToInt16(EnumsConstants.EmailFunctionality.EditedProject));

                //Get ProjectManager Name 

                raveHRCollection = DataAccessLayer.Projects.Projects.GetProjectManagerByProjectId(objProjectDetails);

                if (raveHRCollection.Count > 0)
                {
                    foreach (BusinessEntities.Projects objProj in raveHRCollection)
                    {
                        projectManagerEmail += objProj.EmailIdOfPM;
                        //projectManagerEmail += ",";
                        objProjectDetails.EmailIdOfPM = projectManagerEmail;

                        if (objProjectDetails.EmailIdOfPM.EndsWith(","))
                        {
                            objProjectDetails.EmailIdOfPM = objProjectDetails.EmailIdOfPM.Substring(0, objProjectDetails.EmailIdOfPM.Length - 1);
                        }

                    }
                 

                    obj.To.Add(objProjectDetails.EmailIdOfPM);
                }

                string tableData = GetHTMLForProjectEdited(objProjectDetailsUpdated);

                obj.Subject = string.Format(obj.Subject, objProjectDetails.ProjectCode,
                                                             objProjectDetails.ClientName,
                                                             objProjectDetails.ProjectName);

                obj.Body = string.Format(obj.Body, objProjectDetails.ProjectName,
                                                   objProjectDetails.ClientName,
                                                   tableData,
                                                   strProjectSummaryLink);

                obj.SendEmail(obj);

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, SEND_MAIL_EDIT_PROJECT, EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }

        }

        public void SendEmailForProjectClosed(BusinessEntities.Projects objProjectDetails)
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                string projectManagerEmail = string.Empty;
                string strProjectStatus = string.Empty;
                string strProjectStatusFlag = objProjectDetails.ProjectStatus;
                string sComp = Utility.GetUrl();
                string strProjectSummaryLink = sComp + CommonConstants.PROJECTSUMMARY_PAGE;

                //Get ProjectManager Name 

                raveHRCollection = DataAccessLayer.Projects.Projects.GetProjectManagerByProjectId(objProjectDetails);

                foreach (BusinessEntities.Projects objProj in raveHRCollection)
                {
                    projectManagerEmail += objProj.EmailIdOfPM;
                    //projectManagerEmail += ",";
                    objProjectDetails.EmailIdOfPM = projectManagerEmail;

                    if (objProjectDetails.EmailIdOfPM.EndsWith(","))
                    {
                        objProjectDetails.EmailIdOfPM = objProjectDetails.EmailIdOfPM.Substring(0, objProjectDetails.EmailIdOfPM.Length - 1);
                    }
                  

                }
                


                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Projects),
                                        Convert.ToInt16(EnumsConstants.EmailFunctionality.DeletedProject));

                obj.To.Add(objProjectDetails.EmailIdOfPM);

                obj.Subject = string.Format(obj.Subject, objProjectDetails.ProjectCode,
                                                         objProjectDetails.ClientName,
                                                         objProjectDetails.ProjectName);
                obj.Body = string.Format(obj.Body, objProjectDetails.ProjectName,
                                                   objProjectDetails.ClientName,
                                                   GetHTMLForProjectClosed(objProjectDetails),
                                                   strProjectSummaryLink);

                obj.SendEmail(obj);

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, SEND_MAIL_CLOSED_PROJECT, EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }

        }


        /// <summary>
        /// Get Table data in HTML format.
        /// </summary>
        /// <param name="listProjectDetails"></param>
        /// <returns></returns>
        private string GetHTMLForProjectEdited(BusinessEntities.Projects objProjectDetails)
        {
            try
            {
                string[] header = new string[8];
                string[,] arrayData = new string[1, 8];

                //Header Details
                header[0] = "Prev End Date";
                header[1] = "Project End Date";
                header[2] = "Prev ProjectGroup";
                header[3] = "Project Group";
                header[4] = "Prev Project Standard Hours";
                header[5] = "Project Standard Hours";
                header[6] = "Prev Project Status";
                header[7] = "Project Status";
                //header[8] = "Prev Project Description";
                //header[9] = "Project Description";

                //RowDetails            

                arrayData[0, 0] = objProjectDetails.PrevEndDate.ToString(CommonConstants.DATE_FORMAT);
                arrayData[0, 1] = objProjectDetails.EndDate.ToString(CommonConstants.DATE_FORMAT);
                arrayData[0, 2] = objProjectDetails.PrevProjectGroup;
                arrayData[0, 3] = objProjectDetails.ProjectGroup;
                arrayData[0, 4] = Convert.ToString(objProjectDetails.PrevStandardHours);
                arrayData[0, 5] = objProjectDetails.StandardHours;
                arrayData[0, 6] = objProjectDetails.PrevProjectStatus;
                arrayData[0, 7] = objProjectDetails.ProjectStatus;
                //arrayData[0, 8] = objProjectDetails.PrevProjectExecutiveSummary;
                //arrayData[0, 9] = objProjectDetails.ProjectExecutiveSummary;


                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.Header = header;
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = 1;

                return objEmailTableData.GetTableData(objEmailTableData);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, GET_HTML_PROJECT_EDITED, EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }

        }


        /// <summary>
        /// Displays data in Table format
        /// </summary>
        /// <param name="listRecruitmentDetails"></param>
        /// <returns></returns>
        private string GetHTMLForProjectClosed(BusinessEntities.Projects projectDetails)
        {
            string[,] arrayData = new string[4, 2];
            try
            {

                //Header Values
                arrayData[0, 0] = "Client Name";
                arrayData[1, 0] = "Project Name";
                arrayData[2, 0] = "Start Date";
                arrayData[3, 0] = "End Date";

                //Row Details
                arrayData[0, 1] = projectDetails.ClientName;
                arrayData[1, 1] = projectDetails.ProjectName;
                arrayData[2, 1] = projectDetails.StartDate.ToShortDateString();
                arrayData[3, 1] = projectDetails.EndDate.ToShortDateString();



                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = 4;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, GET_HTML_PROJECT_CLOSED, EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }


        }

        /// <summary>
        /// Add Category Technology
        /// </summary>
        public void AddCategoryTechnology(Category category, Technology technology, ref int CategoryId, ref int TechNologyId)
        {
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objTechnologyCategoryDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                objTechnologyCategoryDAL.AddCategoryTechnology(category, technology,ref  CategoryId, ref  TechNologyId);
                

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "TechnologyCategory", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Add Domain And SubDomain
        /// </summary>
        public void AddDomainAndSubDomain(Domain objDomain, SubDomain objSubDomain, ref int DomainId, ref int SubDomainId)
        {
            try
            {
                Rave.HR.DataAccessLayer.Projects.Projects objTechnologyCategoryDAL = new Rave.HR.DataAccessLayer.Projects.Projects();
                objTechnologyCategoryDAL.AddDomainAndSubDomain(objDomain, objSubDomain, ref  DomainId, ref  SubDomainId);


            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PROJECTS, "TechnologyCategory", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
        }
        #endregion Member Functions

    }
}

