using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BusinessEntities;
using Common;

namespace Rave.HR.BusinessLayer.Projects
{
   public class RaveHRMaster
    {
        public List<BusinessEntities.Master> GetMasterData(string strCategory)
        {
            try
            {
                Rave.HR.DataAccessLayer.Projects.RaveHRMaster objDropDownDAL = new Rave.HR.DataAccessLayer.Projects.RaveHRMaster();
                DataTable dtDropDown = new DataTable();
                dtDropDown = objDropDownDAL.FillDropDownList(strCategory);


                List<BusinessEntities.Master> objListDropDown = new List<BusinessEntities.Master>();
                BusinessEntities.Master objRaveHRMaster = null;
                foreach (DataRow drProjectDetails in dtDropDown.Rows)
                {
                    objRaveHRMaster = new BusinessEntities.Master();
                    objRaveHRMaster.MasterId = int.Parse(drProjectDetails["MasterID"].ToString());
                    objRaveHRMaster.MasterName = drProjectDetails["MasterName"].ToString();
                    objListDropDown.Add(objRaveHRMaster);
                }
                return objListDropDown;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRMaster.cs", "GetMasterData", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            finally
            { 

            }
        }
   }
}
