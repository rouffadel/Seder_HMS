using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;

namespace AECLOGIC.ERP.HMS
{
    public partial class FinancialYear : AECLOGIC.ERP.COMMON.WebFormMaster
    {
         
      
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Convert.ToInt32(Request.QueryString["key"]) == 1)
            {
                tblNew.Visible = true;
                tblEdit.Visible = false;
                //hlnkAdd.Visible = false;
            }
           
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["FinYearId"] = "";
                if (Convert.ToInt32(Request.QueryString["key"]) == 2)
                {
                    //hlnkAdd.Visible = false;
                    tblEdit.Visible = true;
                    tblNew.Visible = false;
                    BindGrid();
                }
                if (Convert.ToInt32(Request.QueryString["key"]) == 0)
                {
                    //hlnkAdd.Visible = true;
                    tblEdit.Visible = true;
                    tblNew.Visible = false;
                    BindGrid();
                }
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID;;

             

          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
              
                btnSubmit.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                
            }
            return MenuId;
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;


         DataSet  ds = PayRollMgr.GetFinacialYearList();
            gvFinancialYear.DataSource = ds;
            gvFinancialYear.DataBind();
        }
        public void BindDetails(int FinYearId)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
          DataSet  ds = PayRollMgr.GetFinacilYearDetails(FinYearId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtFromDate.Text = ds.Tables[0].Rows[0]["FromDate"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["TODate"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            }
        }
        protected void gvFinancialYear_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int FinYearId = Convert.ToInt32(e.CommandArgument);
            ViewState["FinYearId"] = FinYearId;
            if (e.CommandName == "Edt")
            {
                BindDetails(FinYearId);
            }
            else
            {

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int FinYearId = 0;
                if (ViewState["FinYearId"].ToString() != null && ViewState["FinYearId"].ToString() != string.Empty)
                {
                    FinYearId = Convert.ToInt32(ViewState["FinYearId"].ToString());
                }

                int OutPut = PayRollMgr.InsUpdateFinacilYear(FinYearId, CODEUtility.ConvertToDate(txtFromDate.Text.Trim(), DateFormat.DayMonthYear), CODEUtility.ConvertToDate(txtToDate.Text.Trim(), DateFormat.DayMonthYear), txtName.Text);
                if (OutPut == 1)
                {
                    AlertMsg.MsgBox(Page, "Done.!!");
                }
                else if (OutPut == 2)
                {
                    AlertMsg.MsgBox(Page, "Already Exists.!");

                }
                else
                    AlertMsg.MsgBox(Page, "Updated.!");
                //if (FinYearId == 0)
                //{
                //    AlertMsg.MsgBox(Page, "Done.! ");
                //}
                //else
                //{
                //    AlertMsg.MsgBox(Page, "Updated");
                //}
                BindGrid();
                Clear();
            }
            catch (Exception FYear)
            {
                AlertMsg.MsgBox(Page, FYear.Message.ToString(),AlertMsg.MessageType.Error);
            }


        }

        public void Clear()
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            ViewState["FinYearId"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            tblNew.Visible = true;
            tblEdit.Visible = false;

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;

        }

       
    }
}
