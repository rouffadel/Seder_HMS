using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
            try
            {
                Image1.ImageUrl = "ContactImages/" + Request.QueryString[0].ToString() + "." + Request.QueryString[1].ToString();
            }
            catch
            {
                AlertMsg.MsgBox(Page, "Unable to Print Image!");
            }
        //}

    }
}
