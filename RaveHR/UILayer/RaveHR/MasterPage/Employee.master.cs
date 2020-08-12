using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Common.AuthorizationManager;
using Common;
using System.Text;

public partial class MasterPage_Employee : System.Web.UI.MasterPage
{
    # region Private Members & Constants

    const string CLASS_NAME = "Employee.Master.cs";

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

    #endregion Private Members & Constants

    protected void Page_Load(object sender, EventArgs e)
    {
        string appPath = HttpContext.Current.Request.ApplicationPath;
        string physicalPath = HttpContext.Current.Request.MapPath(appPath);
        //string path = @"D:\RaveHR\Code\Resource Management\RaveHR\UILayer\RaveHR\menu.xml";
        string path = physicalPath + @"\menu.xml";
        DataSet ds = new DataSet();
        ds.ReadXml(path);

        for (int c = 0; c < ds.Tables["menuitem"].Rows.Count; c++)
        {
            //added div id and value for identifying individual tabs for highlighting. 
            MenuItem row = new MenuItem("<div id='divMenu_" + c.ToString() + "'>" + (string)ds.Tables["menuitem"].Rows[c][0].ToString() + "</div>", Convert.ToString(c + 1),
                                        "", (string)ds.Tables["menuitem"].Rows[c][1].ToString());
            Menu1.Items.Add(row);
        }

    }

    public static void SentMail(string emailIdTo, string updatedData)
    {
        try
        {
            AuthorizationManager authoriseduser = new AuthorizationManager();
            string LoggedInUserMailId = authoriseduser.getLoggedInUser();
            string strCurrentUser = authoriseduser.getLoggedInUserEmailId();
            string strUserEamilIdsInRole = authoriseduser.getUserEmailIdInRoles(AuthorizationManagerConstants.ROLEHR);
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SentMail", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            throw objEx;

        }
    }

    /// <summary>
    /// Get message body.
    /// </summary>
    private static string GetMessageBody(string strToUser, string strFromUser, string updatedData)
    {
        HtmlForm htmlFormBody = new HtmlForm();
        try
        {
            StringBuilder strMessageBody = new StringBuilder();
            strMessageBody.Append("Hello, " + strToUser + "</br>"
                + "This is to bring to your " + updatedData + " has been updated in the  Resource Management System " + "</br>" +
                "</br>" + "</br>" + "Regards," + "</br>" + strFromUser + "</br>");

            htmlFormBody.Style.Add(HtmlTextWriterStyle.FontFamily, "5");
            htmlFormBody.Style.Add(HtmlTextWriterStyle.FontWeight, "10");
            htmlFormBody.InnerText = strMessageBody.ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetMessageBody", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            throw objEx;

        }

        return htmlFormBody.InnerText.ToString();
    }
}
