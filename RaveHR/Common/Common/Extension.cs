using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Common;
/// <summary>
/// Summary description for Extension
/// </summary>
public static class Extension
{

    public static Int32 CastToInt32(this object Value)
    {
        int intValue = 0;
        if (!string.IsNullOrEmpty(Convert.ToString(Value)))
            intValue = Convert.ToInt32(Value);

        return intValue;
    }
    public static string CastToString(this object Value)
    {
        string strValue = "";
        if (!string.IsNullOrEmpty(Convert.ToString(Value)))
            strValue = Convert.ToString(Value);
        return strValue;
    }
    public static Boolean CastToBool(this object Value)
    {
        bool strValue = false;
        if (!string.IsNullOrEmpty(Convert.ToString(Value)))
            strValue = Convert.ToBoolean(Convert.ToInt32(Value));
        return strValue;
    }
    public static bool CheckIsNull(this object Value)
    {
        return string.IsNullOrEmpty(Convert.ToString(Value));
    }
    // Rakesh : Issue 57965 : 10/May/2016 : Starts 

    /// <summary>
    /// It gives Formatted/Appended Purpose Description as per Purpose
    /// </summary>
    /// <param name="strPurposeDescription"></param>
    /// <param name="strPurposeId"></param>
    /// <returns></returns>
    public static string FormattedPurpose(this string strPurposeDescription, int strPurposeId)
    {
        //Rakesh :   57942   Id's are Using 790,791,793,2097,817



        if (strPurposeId == (int)Common.MasterEnum.MRFPurpose.HiringForNewRole)
        {
            strPurposeDescription = "Position Description : " + strPurposeDescription;
        }
        else if (strPurposeId == (int)Common.MasterEnum.MRFPurpose.HiringForProject)
        {
            strPurposeDescription = "Project Name : " + strPurposeDescription;
        }

        else if (strPurposeId == (int)Common.MasterEnum.MRFPurpose.Replacement)
        {
            strPurposeDescription = "Replacement for  " + strPurposeDescription;
        }
        else if (strPurposeId == (int)Common.MasterEnum.MRFPurpose.SubstituteForMaternityLeave)
        {
            strPurposeDescription = "Substitute for  " + strPurposeDescription;
        }
        else if (strPurposeId == (int)Common.MasterEnum.MRFPurpose.Others)
        {
            strPurposeDescription = "Other Description : " + strPurposeDescription;
        }
        else if (strPurposeId == (int)Common.MasterEnum.MRFPurpose.HiringforDepartment)
        {
            strPurposeDescription = "Department Name : " + strPurposeDescription;
        }
        return strPurposeDescription;
    }
    //End


    // Rakesh : Generic Dropdown Bind Extension Method Begin

    public static void BindDropdown(this DropDownList ddl, object Data)
    {
        ddl.DataSource = Data;

        ddl.DataTextField = CommonConstants.DDL_DataTextField;
        ddl.DataValueField = CommonConstants.DDL_DataValueField;
        // Bind the data to dropdown
        ddl.DataBind();
        // Default value of dropdown is "Select"
        ddl.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
    }

    public static void BindDropdown(this DropDownList ddl, object Data,string strTextField,string strValueField)
    {
        ddl.DataSource = Data;

        ddl.DataTextField = strTextField;
        ddl.DataValueField = strValueField;
        // Bind the data to dropdown
        ddl.DataBind();
        // Default value of dropdown is "Select"
        ddl.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
    }
    //End


    
}
