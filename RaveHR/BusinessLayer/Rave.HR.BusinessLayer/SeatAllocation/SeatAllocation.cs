//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SeatAllocation.cs       
//  Author:         Kanchan.Singh
//  Date written:   19/11/2009 2:00:00 PM
//  Description:    This page serves as the Business layer for the Seat allocation module.
//
//  Amendments
//  Date                   Who               Ref      Description
//  ----                   -----------       ---      -----------
//  19/11/2009 2:00:00 PM  Kanchan.Singh     n/a      Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Common;
using Common.Constants;
using Rave.HR.DataAccessLayer;
using Rave.HR.BusinessLayer.Interface;
using Common.AuthorizationManager;

namespace Rave.HR.BusinessLayer.SeatAllocation
{
   public class SeatAllocation
    {
        const string GETSEATDETAILS = "GetSeatDetails";

        const string CLASSNAME = "SeatAllocation.cs";

        const string GETEMPLOYEEDETAILSFORSEAT = "GetEmployeeDetailsForSeat";

        const string GETEMPLOYEEDETAILSBYID = "GetEmployeeDetailsByID";

       const string GETEMPLOYEENAME = "GetEmployeeName";
       //Googleconfigurable
       //const string RAVEDOMAIN = "@rave-tech.com";

        bool result = false;
        // Initialise the Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

        // Initialise the Collection class object
        BusinessEntities.SeatAllocation Seat = new BusinessEntities.SeatAllocation();

        // Initialise the DAL class object
        Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation objSeatAllocation = null;

        /// <summary>
        ///  This method will fetch records of seats from  Data acess layer and return to UI layer.
        /// </summary>       
        /// <returns>list</returns>
        public BusinessEntities.RaveHRCollection GetSeatDetails(BusinessEntities.SeatAllocation Section)
        {
            try
            {
                //instantiate SeatAllocation object of data layer
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();

                raveHRCollection = objSeatAllocation.GetSeatDetails(Section);

                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer,CLASSNAME ,GETSEATDETAILS , EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }

       /// <summary>
       /// Shift the seat from one location to another.
       /// </summary>
       /// <param name="Source"></param>
       /// <param name="Destination"></param>
       /// <returns></returns>
        public bool ShiftLocation(BusinessEntities.SeatAllocation Source, BusinessEntities.SeatAllocation Destination)
        {
            try
            {
                //instantiate SeatAllocation object of data layer.
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();

                result = objSeatAllocation.ShiftLocation(Source,Destination);

                if (result)
                {
                    SendSeatShiftingEmail(Source,Destination);
                }

                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, GETSEATDETAILS, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }

        /// <summary>
        ///  This method will fetch records of seats from  Data acess layer and return to UI layer.
        /// </summary>       
        /// <returns>list</returns>
        public BusinessEntities.SeatAllocation GetEmployeeDetailsForSeat(BusinessEntities.SeatAllocation Seat)
        {
            try
            {
                //instantiate SeatAllocation object of data layer
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();
                BusinessEntities.SeatAllocation SeatData = new BusinessEntities.SeatAllocation();

                SeatData = objSeatAllocation.GetEmployeeDetailsAtSeat(Seat);

                return SeatData;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, GETEMPLOYEEDETAILSFORSEAT, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }

        /// <summary>
        ///  This method will fetch records of seats from  Data acess layer and return to UI layer.
        /// </summary>       
        /// <returns>list</returns>
        public BusinessEntities.SeatAllocation GetEmployeeDetailsByID(BusinessEntities.SeatAllocation Seat)
        {
            try
            {
                //instantiate SeatAllocation object of data layer
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();
                BusinessEntities.SeatAllocation SeatData = new BusinessEntities.SeatAllocation();

                SeatData = objSeatAllocation.GetEmployeeDetailsByID(Seat);

                return SeatData;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, GETEMPLOYEEDETAILSBYID, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="Seat"></param>
       /// <returns></returns>
        public bool SaveEmpDetails(BusinessEntities.SeatAllocation Seat)
        {
            try
            {
                //instantiate SeatAllocation object of data layer
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();

                result = objSeatAllocation.SaveEmpDetails(Seat);

                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, "SaveEmpDetails", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }

        /// <summary>
        ///  
        /// </summary>       
        /// <returns>list</returns>
        public BusinessEntities.SeatAllocation GetEmployeeName(BusinessEntities.SeatAllocation Seat)
        {
            try
            {
                //instantiate SeatAllocation object of data layer
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();
                BusinessEntities.SeatAllocation SeatData = new BusinessEntities.SeatAllocation();

                SeatData = objSeatAllocation.GetEmployeeName(Seat);

                return SeatData;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, GETEMPLOYEENAME, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }

        public bool Allocate(BusinessEntities.SeatAllocation SeatDetails)
        {
            try
            {
                //instantiate SeatAllocation object of data layer
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();

                result = objSeatAllocation.Allocate(SeatDetails);

                if (result)
                {
                    SendAllocatationEMail(SeatDetails);
                }

                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, "Allocate", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }

       /// <summary>
       /// To check Employee Location.
       /// </summary>
       /// <param name="employeeId"></param>
       /// <returns></returns>
        public BusinessEntities.SeatAllocation CheckEmployeeLocation(int employeeId)
        {
            try
            {
                //instantiate SeatAllocation object of data layer
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();
                BusinessEntities.SeatAllocation SeatData = new BusinessEntities.SeatAllocation();

                SeatData = objSeatAllocation.CheckEmployeeLocation(employeeId);

                return SeatData;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, "CheckEmployeeLocation", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }

       /// <summary>
       ///  This method will fetch records of seats from  Data acess layer and return to UI layer.
       /// </summary>       
       /// <returns>list</returns>
        public BusinessEntities.SeatAllocation GetSeatDetailsByID(BusinessEntities.SeatAllocation Seat)
        {
            try
            {
                //instantiate SeatAllocation object of data layer
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();
                BusinessEntities.SeatAllocation SeatData = new BusinessEntities.SeatAllocation();

                SeatData = objSeatAllocation.GetSeatDeatilsByID(Seat);

                return SeatData;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, GETEMPLOYEEDETAILSBYID, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }

       /// <summary>
       /// This function returns all employee details which are not allocated to seat.
       /// </summary>
       /// <returns></returns>
        public List<BusinessEntities.SeatAllocation> GetUnAllocatedEmployee()
        {
            objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();
            List<BusinessEntities.SeatAllocation> EmployeeDetail = new List<BusinessEntities.SeatAllocation>();
            EmployeeDetail = objSeatAllocation.UnallocatedEmployee();
            return EmployeeDetail;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="branchId"></param>
       /// <returns></returns>
        public RaveHRCollection GetSectionByBranch(int branchId)
        {
            objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();

            return objSeatAllocation.GetSectionByBranch(branchId);
        }


        /// <summary>
        /// Swaping seat location.
        /// </summary>
        /// <param name="sourceSeatId"> first seat location</param>
        /// <param name="destinationSeatId"> 2nd seat location</param>
        /// <returns></returns>
        public bool SwapLocation(BusinessEntities.SeatAllocation source, 
                                 BusinessEntities.SeatAllocation destination)
        {
            try
            {
                //instantiate SeatAllocation object of data layer
                objSeatAllocation = new Rave.HR.DataAccessLayer.SeatAllocation.SeatAllocation();

                result = objSeatAllocation.SwapLocation(source, destination);

                if (result)
                {
                    //Shifting mail for first shifted employee.
                    SendSeatShiftingEmail(source, destination);
                    
                    //Shifting mail for second shifted employee.
                    SendSeatShiftingEmail(destination ,source);
                }

                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, GETSEATDETAILS, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }


        /// <summary>
        /// Send mail after allocation on seat.
        /// </summary>
        /// <param name="seatDetails"></param>
        private void SendAllocatationEMail(BusinessEntities.SeatAllocation seatDetails)
        {
            try
            {
                AuthorizationManager objAuMan = new AuthorizationManager();

                BusinessEntities.SeatAllocation EmpDetails = new BusinessEntities.SeatAllocation();

                BusinessEntities.SeatAllocation seat = new BusinessEntities.SeatAllocation();

                //get  seat details.
                seat = GetSeatDetailsByID(seatDetails);

                //Get the details of allocated employee.
                EmpDetails = GetEmployeeDetailsByID(seatDetails);

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.SeatAllocation),
                                              Convert.ToInt16(EnumsConstants.EmailFunctionality.AllocatedSeat));

                //string strFromUser = objAuMan.getLoggedInUserEmailId();
                //going google
                //string username = objAuMan.GetDomainUserName(strFromUser.Replace(CommonConstants.RAVEDOMAIN, string.Empty));
                string username = "";
                string strFromUser = objAuMan.getLoggedInUser();
                //GoogleMail
                //if (strFromUser.ToLower().Contains("@rave-tech.co.in"))
                //{
                //    username = objAuMan.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, string.Empty));
                //}
                //else
                //{
                //    username = objAuMan.GetDomainUserName(strFromUser.Replace(NORTHGATEDOMAIN, string.Empty));
                //}

                username = objAuMan.GetDomainUserName(strFromUser);

                obj.From = strFromUser;

                obj.To.Add(EmpDetails.EmployeeEmailID);

                obj.Subject = string.Format(obj.Subject, EmpDetails.EmployeeName,
                                                         seat.SeatName);
                obj.Body = string.Format(obj.Body, EmpDetails.EmployeeName,
                                                   seat.SeatName,
                                                   GetLinkForEmail(),
                                                   username);
                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, "SendAllocatationEMail", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

         }


        /// <summary>
        /// Send mail for shifting location.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        private void SendSeatShiftingEmail(BusinessEntities.SeatAllocation source,
                                           BusinessEntities.SeatAllocation destination)
        {
            try
            {
                AuthorizationManager objAuMan = new AuthorizationManager();
                BusinessEntities.SeatAllocation sourceSeatDetails = new BusinessEntities.SeatAllocation();

                BusinessEntities.SeatAllocation destSeatDetails = new BusinessEntities.SeatAllocation();

                BusinessEntities.SeatAllocation empDetails = new BusinessEntities.SeatAllocation();

                //get source seat details.
                sourceSeatDetails = GetSeatDetailsByID(source);

                //get destination seat details.
                destSeatDetails = GetSeatDetailsByID(destination);

                //Get the details of shifted employee.
                empDetails = GetEmployeeDetailsByID(destSeatDetails);

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.SeatAllocation),
                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.ShiftedSeat));

                string strFromUser = objAuMan.getLoggedInUserEmailId();
                //going google
                //string username = objAuMan.GetDomainUserName(strFromUser.Replace(CommonConstants.RAVEDOMAIN, string.Empty));
                string username = "";
                string strFrom = objAuMan.getLoggedInUser();
                //GoogleMail
                //if (strFromUser.ToLower().Contains("@rave-tech.co.in"))
                //{
                //    username = objAuMan.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, string.Empty));
                //}
                //else
                //{
                //    username = objAuMan.GetDomainUserName(strFromUser.Replace(NORTHGATEDOMAIN, string.Empty));
                //}
                username = objAuMan.GetDomainUserName(strFrom);

                //Add employee name in to list of mail.
                obj.To.Add(empDetails.EmployeeEmailID);

                obj.From = strFromUser;

                obj.Subject = string.Format(obj.Subject, empDetails.EmployeeName,
                                                         sourceSeatDetails.SeatName,
                                                         destSeatDetails.SeatName);
                obj.Body = string.Format(obj.Body, empDetails.EmployeeName,
                                                   sourceSeatDetails.SeatName,
                                                   destSeatDetails.SeatName,
                                                   GetLinkForEmail(),
                                                   username);
                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASSNAME, "SendSeatShiftingEmail", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            }

        }


        /// <summary>
        /// Get the URL of page.
        /// </summary>
        /// <returns></returns>
        private string GetLinkForEmail()
        {
            return Utility.GetUrl() + CommonConstants.SEATALLOCATION_PAGE;
        }

   }
}
