
//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SeatAllocation.aspx.cs       
//  Author:         Kanchan.Singh
//  Date written:   19/11/2009 2:00:00 PM
//  Description:    The Seat Allocation page allows user 
//                  to view the information of the person seatting on the location.
//
//  Amendments
//  Date                   Who               Ref      Description
//  ----                   -----------       ---      -----------
//  19/11/2009 2:00:00 PM  Kanchan.Singh     n/a      Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Text;
using Common.Constants;
using BusinessEntities;

public partial class SeatAllocation : BaseClass
{
    #region Local Memeber Declaration
  
    private string CLASS_NAME = "SeatAllocation.cs";

    private string SEATDETAILS = "getSeatDetails";

    //Define the select as string.
    private string SELECTONE = "Select";

    //Define the zero as string.
    private string ZERO = "0";

    string MasterName = "MasterName";

    string MasterID = "MasterID";

    string SectionName = "SectionName";

    string SectionId = "SectionId";

    bool result = false;

    //BusinessEntities

    BusinessEntities.SeatAllocation seatDetails = new BusinessEntities.SeatAllocation();

    // Initialise the Collection class object
    BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

    // Initialise the Bussiness layer class object
    Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation objBLSeatAllocation = null;

    string subject;

    string body;
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

   
    #endregion Local Memeber Declaration

    #region Protected method

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
                if (!IsPostBack)
                {
                    //Get the branches of rave & fill in the dropdown list.
                    GetMasterDataForBranch();
                }

                //Check whether user return after allocation on this page.
                if (Request.QueryString[QueryStringConstants.ALLOCATESEATID] != null && !IsPostBack)
                {
                    ddlBranch.SelectedValue = Request.QueryString[QueryStringConstants.SEATALNBRANCHID];

                    //Get the sections of selected branch.
                    GetSectionByBranch(Convert.ToInt32(Request.QueryString[QueryStringConstants.SEATALNBRANCHID]));
                    lblSection.Visible = true;
                    RbListSeatAllocation.SelectedValue = Request.QueryString[QueryStringConstants.SEATALNSECTIONID];

                    //Display the seat arrangement.
                    DisplaySeatArrangment();
                }

                lblMessage.Text = string.Empty;

                //If From & To location is selected the shift the location
                //Added 3rd condition to avoid f5 problem.after allocation seat should not allow to shift the location.
                if ((hdfDragFrom.Value != string.Empty) && (hdfDragTo.Value != string.Empty) && (CheckEmployeeOnSeat(hdfDragFrom.Value)) && (!CheckEmployeeOnSeat(hdfDragTo.Value)))
                {
                    callShiftLocationMethod(hdfDragFrom.Value, hdfDragTo.Value);
                    DisplaySeatArrangment();
                }
                //For swaping of seats.
                else if ((hdfDragFrom.Value != string.Empty) && (hdfDragTo.Value != string.Empty) && (CheckEmployeeOnSeat(hdfDragFrom.Value)) && (CheckEmployeeOnSeat(hdfDragTo.Value)))
                {
                    SwapLocation(Convert.ToInt32(hdfDragFrom.Value), Convert.ToInt32(hdfDragTo.Value));
                    DisplaySeatArrangment();
                }
          
         }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Display the Seating arrangment of selected Branch.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Get the sections of selected branch.
            GetSectionByBranch(Convert.ToInt32(ddlBranch.SelectedValue));

            if (RbListSeatAllocation.Items.Count > 0)
            {
                //Display the arrangement.
                DisplaySeatArrangment();
                lblSection.Visible = true;
            }
            else
            {
                divSeatDetails.InnerHtml = string.Empty;
                lblSection.Visible = false;
            }
        }

        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlBranch_SelectedIndexChanged", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
   }

    /// <summary>
    /// Display the Seat arrangement of selected section.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RbListSeatAllocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Display the arrangement.
            DisplaySeatArrangment();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "RbListSeatAllocation_OnSelectedIndexChanged", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion Protected method

    #region Public Method

    /// <summary>
    /// Get the details of a seat.
    /// </summary>
    /// <param name="SeatInformation"></param>
    /// <returns></returns>
    public List<BusinessEntities.SeatAllocation> getSeatDetails (BusinessEntities.SeatAllocation SeatInformation)
    {
        List<BusinessEntities.SeatAllocation> SeatInfo = new List<BusinessEntities.SeatAllocation>();
        try
        {
            //Invoke the object of business layer to call BusinessLayer method.
            objBLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();

            //Call the method of business layer to get seat details
            raveHRCollection = objBLSeatAllocation.GetSeatDetails(SeatInformation);

            //Convert raveHRCollection to List<BusinessEntities.SeatAllocation>.
            for (int i = 0; i < raveHRCollection.Count; i++)
            {
                foreach (List<BusinessEntities.SeatAllocation> SeatDescription in raveHRCollection)
                {
                    foreach (BusinessEntities.SeatAllocation Seat in SeatDescription)
                    {
                        seatDetails = new BusinessEntities.SeatAllocation();
                        seatDetails = (BusinessEntities.SeatAllocation)Seat;
                        SeatInfo.Add(seatDetails);
                    }
                }

            }                    
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, SEATDETAILS, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
        return SeatInfo;               
    }

    /// <summary>
    /// Make the table of ALL seats of a section and display on page.
    /// </summary>
    /// <param name="listSeatDetails"></param>
    /// <returns></returns>
    public string makeSeatDetailsTable(List<BusinessEntities.SeatAllocation> listSeatDetails)
    {
        //Define counter to count the seats.
        int counter = 0;
        //Define to keep bay id of previous seat.
        int brBayId = 0;
       
        //Create the object of string builder to make HTML code.
        StringBuilder strTableHTML = new StringBuilder("");

        strTableHTML.Append("<table  cellpadding='5' cellspacing='5' style='border-color: #000000; ' bgcolor='#CCCCCC'>");
        
        foreach (BusinessEntities.SeatAllocation seatNo in listSeatDetails)
        {
            //For first time assign the bay id.
            if (brBayId == 0)
            {
                brBayId = seatNo.BayID;
            }
            //After 10 records new row will be created.
            if (counter == 0)
            {
                strTableHTML.Append("<tr>");
                brBayId = seatNo.BayID;
            }
            //If seats of new bay started, break the current row and start with new row.
            else if (seatNo.BayID != brBayId)
            {
                strTableHTML.Append("</tr>");
                strTableHTML.Append("<tr>");
                counter = 0;
                brBayId = seatNo.BayID;
            }
            
            counter++;
            
            //Add the attributes in table's Td.
            strTableHTML.Append(" <td id='" + seatNo.SeatID + "' style='font-size:smaller; font-family:Verdana; width: 50px; height:28px;' title='" + seatNo.EmployeeName + "' bgcolor='" + getColorCodeForSeats(seatNo) + "' oncontextmenu='javascript:return CreateContextMenu(event,this);'> " + seatNo.SeatName + " </td>");
           
            //Close td after 10 seats.
            if (counter == 10 )
            {
                strTableHTML.Append("</tr>");
                counter = 0;
                brBayId = seatNo.BayID;
            }
        }
        //Close  the end table tag.
        strTableHTML.Append("</table> ");

        //Return the HTML code of table.
        return strTableHTML.ToString();

    }
  
    /// <summary>
    /// Get the color code for whether seat allocated or not.
    /// </summary>
    /// <param name="SeatNo"></param>
    /// <returns></returns>
    public string getColorCodeForSeats(BusinessEntities.SeatAllocation SeatNo)
    {
        try
        {
            string tdColor;

            //If No employee is associated with seat then display yellow color.
            if (SeatNo.EmployeeID == 0)
            {
                tdColor = "#F7FE2E";
            }
            //If  employee is associated with seat then display green color.
            else
            {
                tdColor = "#009900";
            }
            return tdColor;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "getColorCodeForSeats", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Shift the employee  seat location fron one seat to another.
    /// </summary>
    /// <param name="Source"></param>
    /// <param name="Destination"></param>
    /// <returns></returns>
    public bool ShiftLocation(BusinessEntities.SeatAllocation Source, BusinessEntities.SeatAllocation Destination)
    {
        try
        {
            //Invoke the object of business layer to call BusinessLayer method.
            objBLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();

            //Create & Invoke the object of BusinessEntities.SeatAllocation to get details of shifted employee.
            BusinessEntities.SeatAllocation EmpDetails = new BusinessEntities.SeatAllocation();

            //Invoke the object of BusinessEntities.SeatAllocation to get the seat deatils of destination.
            seatDetails = new BusinessEntities.SeatAllocation();

            //Invoke the object of BusinessEntities.SeatAllocation to get the seat deatils of source.
            BusinessEntities.SeatAllocation seat = new BusinessEntities.SeatAllocation();

            //call BusinessLayer method to shift the location.
            result = objBLSeatAllocation.ShiftLocation(Source, Destination);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShiftLocation", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
        return result;
    }

    /// <summary>
    /// Call to above method to shift the loaction.
    /// </summary>
    /// <param name="shiftFrom"></param>
    /// <param name="shiftTo"></param>
    public void callShiftLocationMethod(string shiftFrom, string shiftTo)
    {
        try
        {
            //Check the roles for shifting of loaction.(Only admin can shift the location.)
            if (Rave.HR.BusinessLayer.SeatAllocation.SeatAllocationRoles.CheckRolesSeatAllocation())
            {
                //Add the shift from location.
                BusinessEntities.SeatAllocation SeatInformationFrom = new BusinessEntities.SeatAllocation();
                SeatInformationFrom.SeatID = Convert.ToInt32(shiftFrom);

                //Add the shift To loaction.
                BusinessEntities.SeatAllocation SeatInformationTo = new BusinessEntities.SeatAllocation();
                SeatInformationTo.SeatID = Convert.ToInt32(shiftTo);

                //Display message if Seat is shifted successfully .
                if (ShiftLocation(SeatInformationFrom, SeatInformationTo))
                {
                    lblMessage.Text = "Location has been shifted successfully.";
                }
                else
                {
                    lblMessage.Text = "Location has not been shifted successfully.";
                    lblMessage.Text = "<font color=RED>" + lblMessage.Text + "</font>";
                }
            }
            else
            {
                lblMessage.Text = "You are not authorised to shift a employee's Location.";
                lblMessage.Text = "<font color=RED>" + lblMessage.Text + "</font>";
            }
            //Clear the hidden fields value.
            hdfDragFrom.Value = string.Empty;
            hdfDragTo.Value = string.Empty;
            hdfClickedTd.Value = string.Empty;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "callShiftLocationMethod", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Disaply the seat arrangment according to Branch & Section.
    /// </summary>
    public void DisplaySeatArrangment()
    {
        try
        {
            //Declare the object of  BusinessEntities.SeatAllocation class.
            BusinessEntities.SeatAllocation SeatInformation = new BusinessEntities.SeatAllocation();

            //By default page should display section A seat arrangement.
            if (String.IsNullOrEmpty(RbListSeatAllocation.SelectedValue))
            {
                RbListSeatAllocation.SelectedIndex = 0;
            }

            SeatInformation.SectionID = Convert.ToInt32(RbListSeatAllocation.SelectedValue);
            SeatInformation.RaveBranchID = Convert.ToInt32(ddlBranch.SelectedValue);

            //Assign HTML made table to Div for display.
            divSeatDetails.InnerHtml = makeSeatDetailsTable(getSeatDetails(SeatInformation));
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "DisplaySeatArrangment", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// //Check whether seat is empty or occupied.
    /// </summary>
    /// <param name="seatId"></param>
    /// <returns></returns>
    public bool CheckEmployeeOnSeat( string  seatId)
    {
        bool Flag = false;
        try
        {
            objBLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();

            //Instantiate the object of BusinessEntities.SeatAllocation to get fetched data.
            BusinessEntities.SeatAllocation fetchedSeatDetails = new BusinessEntities.SeatAllocation();

            //Instantiate the object of BusinessEntities.SeatAllocation to pass seat id as a parameter.
            seatDetails = new BusinessEntities.SeatAllocation();

            //Add the seat id to SeatAllocation BusinessEntities.
            seatDetails.SeatID = Convert.ToInt32(seatId); 

            //Call the Business layer method.
            fetchedSeatDetails = objBLSeatAllocation.GetSeatDetailsByID(seatDetails);
            
            if (fetchedSeatDetails != null)
            {   
                //If seat is not vaccant then return true.
                if (fetchedSeatDetails.EmployeeID != 0)
                {
                    Flag = true;
                }
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "CheckEmployeeOnSeat", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
        return Flag;
    }

    /// <summary>
    /// Swaping seat location.
    /// </summary>
    /// <param name="sourceSeatId"> first seat location</param>
    /// <param name="destinationSeatId"> 2nd seat location</param>
    /// <returns></returns>
    public bool SwapLocation(int sourceSeatId, int destinationSeatId)
    {
        try
        {
            //Check the roles for shifting of loaction.(Only admin can shift the location.)
            if (Rave.HR.BusinessLayer.SeatAllocation.SeatAllocationRoles.CheckRolesSeatAllocation())
            {
                //Invoke the object of business layer to call BusinessLayer method.
                objBLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();

                //Invoke the object of BusinessEntities.SeatAllocation to get the seat deatils of source.
                BusinessEntities.SeatAllocation SourceSeatDetails = new BusinessEntities.SeatAllocation();

                //Invoke the object of BusinessEntities.SeatAllocation to get the seat deatils of source.
                BusinessEntities.SeatAllocation DestinationSeatDetails = new BusinessEntities.SeatAllocation();

                //Invoke the object of BusinessEntities.SeatAllocation to get the seat deatils of source.
                BusinessEntities.SeatAllocation Source = new BusinessEntities.SeatAllocation();
                Source.SeatID = sourceSeatId;

                //Invoke the object of BusinessEntities.SeatAllocation to get the seat deatils of source.
                BusinessEntities.SeatAllocation Destination = new BusinessEntities.SeatAllocation();
                Destination.SeatID = destinationSeatId;

                //call BusinessLayer method to shift the location.
                result = objBLSeatAllocation.SwapLocation(Source, Destination);

                //Checks For the sucess of the Shifting and sends the mail.
                if (result)
                {
                  lblMessage.Text = "Seat swapping done successfully";
                }
             }
            else
            {
                lblMessage.Text = "You are not authorised to swap employee's Location.";
                lblMessage.Text = "<font color=RED>" + lblMessage.Text + "</font>";
            }
            //Clear the hidden fields value.
            hdfDragFrom.Value = string.Empty;
            hdfDragTo.Value = string.Empty;
            hdfClickedTd.Value = string.Empty;
            
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SwapLocation", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
        return result;
    }


    #endregion Public Method

    #region Private Methods

    /// <summary>
    /// Gets the master data for the Asset or employee dropdown.
    /// </summary>
    private void GetMasterDataForBranch()
    {
        try
        {
            List<BusinessEntities.Master> ddlObj = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master BLobject = new Rave.HR.BusinessLayer.Common.Master();

            //Get the Branchs of rave.
            ddlObj = BLobject.GetMasterData(Convert.ToString((int)(EnumsConstants.Category.RaveBranch)));

            //Bind the dropdown with data.
            ddlBranch.DataSource = ddlObj;
            ddlBranch.DataValueField = MasterID;
            ddlBranch.DataTextField = MasterName;
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetMasterDataForBranch", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets the master data for the Asset or employee dropdown.
    /// </summary>
    private void GetSectionByBranch(int branchId)
    {
        try
        {
            raveHRCollection = new RaveHRCollection();
            //Invoke the object of business layer to call BusinessLayer method.
            objBLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();

            //Get the Branchs of rave.
            raveHRCollection = objBLSeatAllocation.GetSectionByBranch(branchId);

            //Bind the dropdown with data.
            RbListSeatAllocation.DataSource = raveHRCollection;
            RbListSeatAllocation.DataValueField = SectionId;
            RbListSeatAllocation.DataTextField = SectionName;
            RbListSeatAllocation.DataBind();
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetMasterDataForBranch", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    #endregion Private Methods

    
}
