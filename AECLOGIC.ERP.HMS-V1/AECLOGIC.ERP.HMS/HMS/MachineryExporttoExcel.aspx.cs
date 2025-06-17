using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL; 

namespace AECLOGIC.ERP.HMS
{
    public partial class Reports_ExporttoExcel123 : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }
            if (!IsPostBack)
            {

            }
            try
            {
                if (Session["RepCols"] != null)
                {
                    List<int> IndexList = (List<int>)Session["RepCols"];

                    foreach (int idx in IndexList)
                        gvDetails.Columns[idx].Visible = false;

                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["strConn"]);
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("EMS_MachineryCostDetails", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cn.Close();



                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();

                    //' Set the content type to Excel.
                    Response.ContentType = "application/vnd.ms-excel";
                    //' Remove the charset from the Content-Type header.
                    Response.Charset = "";
                    //' Turn off the view state.
                    this.EnableViewState = false;

                    System.IO.StringWriter tw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                    // ' Get the HTML for the control.
                    gvDetails.RenderControl(hw);
                    // ' Write the HTML back to the browser.
                    Response.Write(tw.ToString());
                    // ' End the response.
                    Response.End();
                }
            }
            catch
            {
            }

        }

    }
}
