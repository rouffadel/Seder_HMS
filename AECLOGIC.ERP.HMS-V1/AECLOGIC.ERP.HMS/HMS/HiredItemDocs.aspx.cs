using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace AECLOGIC.ERP.HMS
{
    public partial class Default4 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int WOID = 0; string path;  
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int WOID = Convert.ToInt32(Session["WOID"]);
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    WOID = Convert.ToInt32(Request.QueryString["id"]);
                    Session["WOID"] = WOID;
                    if (WOID == 0)
                    {
                        WOID = Convert.ToInt32(Session["WOID"]);
                    }
                    path = Server.MapPath(".\\Lands-Buildings\\" + WOID);
                    DataSet ds = null;

                    if (Directory.Exists(path))
                    {
                        string[] files = Directory.GetFileSystemEntries(path);
                        int c = 0;
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("Id", typeof(System.Int32)));
                        dt.Columns.Add(new DataColumn("FileName", typeof(System.String)));
                        DataRow dr;
                        foreach (string file in files)
                        {
                            string ext = file.Split('.')[file.Split('.').Length - 1];
                            string names = file.Split('.')[file.Split('.').Length - 2];
                            string name = names.Split('\\')[names.Split('\\').Length - 1];
                            string fname = name + "." + ext;

                            dr = dt.NewRow();
                            dr["Id"] = c; dr["FileName"] = fname;
                            dt.Rows.Add(dr);
                            dr.AcceptChanges();
                            c++;

                        }

                        dt.AcceptChanges();
                        ds.Tables.Add(dt);
                        ds.AcceptChanges();
                    }
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dlQuotations.DataSource = ds;
                        dlQuotations.DataBind();
                    }
                    else
                    {
                        lblEmptyData.Visible = true;
                        lblEmptyData.Text = "No Records Found!!";
                    }
                }
            }
        }

        public string ViewQuoation(string FileName)
        {
            string returnfile = "";
            path = Server.MapPath("./Lands-Buildings/" + Convert.ToInt32(Session["WOID"]) + "/" + FileName);

            if (FileName != null)
            {
                returnfile = "./Lands-Buildings/" + Convert.ToInt32(Session["WOID"]) + "/" + FileName;
            }
            return returnfile;
        }
        public string ShowQuotations(string filename)
        {
            string returnfile = "";
            path = Server.MapPath("./Lands-Buildings/" + Convert.ToInt32(Session["WOID"]) + "/" + filename);

            if (filename != null)
            {
                returnfile = "./Lands-Buildings/" + Convert.ToInt32(Session["WOID"]) + "/" + filename;
            }
            return returnfile;
        }
        protected void dlQuotations_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string filename = e.CommandArgument.ToString();
            if (e.CommandName == "View")
            {
                if (filename != null)
                {
                    // ShowQuotations(filename);
                    string strScript = "<script> ";
                    strScript += "var newWindow = window.open('HiredItemDocsPreview.aspx?id=" + WOID + "&type=" + filename + "' , '_blank');";
                    strScript += "</script>";
                    ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "strScript", strScript);
                }
            }
        }
    }
}