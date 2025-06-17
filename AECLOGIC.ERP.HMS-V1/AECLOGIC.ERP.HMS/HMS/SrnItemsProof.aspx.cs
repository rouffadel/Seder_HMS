using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccessLayer;
//using BussinessLayer;
using AECLOGIC.HMS.BLL;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class SrnItemsProof : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int SRNID;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                GetParentMenuId();
                if (Request.QueryString["SRNID"] != null && Request.QueryString["SRNID"] != string.Empty)
                {
                    SRNID = Convert.ToInt32(Request.QueryString["SRNID"].ToString());
                    if (Convert.ToInt32(Request.QueryString["ID"].ToString()) == 0)
                        BindGridgvItemDetails(SRNID);
                    else
                        BindGridgvItemDetails(SRNID.ToString());
                    ViewState["SRNID"] = SRNID;

                }
            }
        }
        protected void BindGridgvItemDetails(int SRNID)
        {

            DataSet ds = dlSRN.MMS_gvSrnItemDetails(SRNID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvItemDetails.DataSource = ds;
                gvItemDetails.DataBind();
                dvItems.Visible = true;
            }
        }
        protected void BindGridgvItemDetails(string SRNID)
        {
            DataSet ds = dlSRN.MMS_SrnGridItemDetailsFupload(int.Parse(SRNID));
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvItemDetails.DataSource = ds;
                gvItemDetails.DataBind();
                dvItems.Visible = true;
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;
            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
               
                btnUpload.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }

            return MenuId;
        }
        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            try
            {
                
                string filename = "", ext = string.Empty, path = "", ThumbPath = "";
                foreach (GridViewRow gvRow in gvItemDetails.Rows)
                {
                    FileUpload fuc = (FileUpload)gvRow.Cells[5].Controls[1];
                    filename = fuc.PostedFile.FileName;
                    if (filename != "")
                    {
                        ext = filename.Split('.')[filename.Split('.').Length - 1];
                    }
                    else
                    {
                        ext = "";
                    }
                    int SRNPreID = int.Parse(gvItemDetails.DataKeys[gvRow.RowIndex][0].ToString());
                    SqlParameter[] p = new SqlParameter[2];
                    p[0] = new SqlParameter("@SRNItemID", SRNPreID);
                    p[1] = new SqlParameter("@ext", ext);
                    SQLDBUtil.ExecuteNonQuery("MMS_InsertSRNInvoice", p);
                   
                    if (filename != "")
                    {
                        if (SRNPreID != 0)
                        {
                            path = Server.MapPath(".\\SDNItemsImages\\" + SRNPreID + "." + ext);
                            ThumbPath = Server.MapPath(".\\SDNItemsImages\\" + SRNPreID + "_thumb." + ext);
                            fuc.PostedFile.SaveAs(path);
                        }
                    }
                }
                SRNID = Convert.ToInt32(ViewState["SRNID"]);
                DataSet ds = SQLDBUtil.ExecuteDataset("MMS_GetSRNStatus", new SqlParameter[] { new SqlParameter("@SRNID", SRNID) });
                int status = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                if (status != 3)
                    Response.Redirect("~/hms/SRNPrereq.aspx?SRNID=" + SRNID, false);
                else
                    Response.Redirect("SRNStatus.aspx?ID=3");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}