using System;
using AECLOGIC.ERP.COMMON;
using System.Data;
using AECLOGIC.ERP.HMS;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Drawing;
using System.IO;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class EmpVacationReturnV1 : System.Web.UI.Page   //AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                try
                {
                    int VRID = Convert.ToInt32(Request.QueryString["VRID"]);
                   
                 
                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("@VRID", VRID);
                  
                    DataSet ds = SqlHelper.ExecuteDataset("HR_getEmployeeReturnDetails", p);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["VRID"].ToString() != "")
                        {
                            lblLID.Text = ds.Tables[0].Rows[0]["VRID"].ToString();
                        }
                        else
                            lblLID.Text = "--";
                        if (ds.Tables[0].Rows[0]["WorkSite"].ToString() != "")
                        {
                            lblWSite.Text = ds.Tables[0].Rows[0]["WorkSite"].ToString();
                        }
                        else
                            lblWSite.Text = "--";
                        if (ds.Tables[0].Rows[0]["ProjectName"].ToString() != "")
                        {
                            lblProj.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                        }
                        else
                            lblProj.Text = "--";
                        if (ds.Tables[0].Rows[0]["Designation"].ToString() != "")
                        {
                            lbldesig.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                        }
                        else
                            lbldesig.Text = "--";

                        if (ds.Tables[0].Rows[0]["PHName"].ToString() != "")
                        {
                            LblReqBy.Text = ds.Tables[0].Rows[0]["PHName"].ToString();
                        }
                        else
                            LblReqBy.Text = "--";
                        if (ds.Tables[0].Rows[0]["DHName"].ToString() != "")
                        {
                            lblPHApp.Text = ds.Tables[0].Rows[0]["HRName"].ToString();
                        }
                        else
                            lblPHApp.Text = "--";

                        if (ds.Tables[0].Rows[0]["HRName"].ToString() != "")
                        {
                            lblHRApp.Text = ds.Tables[0].Rows[0]["DHName"].ToString();
                        }
                        else
                            lblHRApp.Text = "--";

                        gdvIndent.DataSource = ds.Tables[0];
                        gdvIndent.DataBind();

                        
                    }
                }
                catch { }
               

            }
        }


    }
}