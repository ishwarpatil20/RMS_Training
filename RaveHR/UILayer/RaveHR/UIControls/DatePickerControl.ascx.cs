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

public partial class UIControls_DatePickerControl : System.Web.UI.UserControl
{
    #region Properties

    public string Text
    {
        get
        {
            return txtDate.Text;
        }
        set
        {
            txtDate.Text = value;
        }
    }

    public override string ClientID
    {
        get
        {
            return txtDate.ClientID;
        }
        
    }


    public bool IsEnable
    {
        set 
        {
            txtDate.Enabled = value;
            imgDate.Enabled = value;
        }
    }


    public  TextBox TextBox
    {
        get
        {
            return txtDate;
        }

    }

    public int Width
    {
        set
        {
            txtDate.Width=value;
        }

    }


    #endregion Properties

    #region Protected Method

    protected void Page_Load(object sender, EventArgs e)
    {
        txtDate.Attributes.Add("onKeyUp", "return GetTrueKeyCode(event);");
    }

    #endregion Protected Method
}
