using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMS
{
    public partial class Options : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            OptionsPaging.FirstClick += new Paging.PageFirst(OptionsPaging_FirstClick);
            OptionsPaging.PreviousClick += new Paging.PagePrevious(OptionsPaging_FirstClick);
            OptionsPaging.NextClick += new Paging.PageNext(OptionsPaging_FirstClick);
            OptionsPaging.LastClick += new Paging.PageLast(OptionsPaging_FirstClick);
            OptionsPaging.ChangeClick += new Paging.PageChange(OptionsPaging_FirstClick);
            OptionsPaging.ShowRowsClick += new Paging.ShowRowsChange(OptionsPaging_ShowRowsClick);
            OptionsPaging.CurrentPage = 1;
        }
        void OptionsPaging_ShowRowsClick(object sender, EventArgs e)
        {
            OptionsPaging.CurrentPage = 1;
            BindPager();
        }
        void OptionsPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = OptionsPaging.CurrentPage;
            objHrCommon.CurrentPage = OptionsPaging.ShowRows;
            BindOptions(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                GetParentMenuId();
                if (Request.QueryString.Count > 0)
                {
                    tblEdit.Visible = true;
                    tblView.Visible = false;

                }
                else
                {
                    try
                    {
                        tblEdit.Visible = false;
                        tblView.Visible = true;
                       
                        BindPager();
                    }
                    catch { AlertMsg.MsgBox(Page, "Default options not set yet!"); }
                }
                ViewState["OID"] = 0;

            }
        }

        public void BindOptions(HRCommon objHrCommon)
        {

            try
            {
                objHrCommon.PageSize = OptionsPaging.ShowRows;
                objHrCommon.CurrentPage = OptionsPaging.CurrentPage;


                 
             DataSet  ds = AttendanceDAC.HR_GetDefaultOptionsByPaging(objHrCommon);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;
                    gvView.DataBind();
                }
                else
                {
                    gvView.DataSource = null;
                    gvView.DataBind();
                }
                OptionsPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
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
                Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
              btnSave.Enabled = Editable;
            }
            return MenuId;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOption.Text == "" || txtvalue.Text == "")
                {
                    AlertMsg.MsgBox(Page, "Check The Values u have entred");
                }
                else
                {
                    int OptionID = Convert.ToInt32(ViewState["OID"]);
                    AttendanceDAC.HR_InsUpOptions(OptionID, txtOption.Text, txtvalue.Text,  Convert.ToInt32(Session["UserId"]));
                    AlertMsg.MsgBox(Page, "Done!");
                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    BindPager();
                   
                }
            }
            catch (Exception Opt)
            {
                AlertMsg.MsgBox(Page, Opt.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        protected void btnupdt_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgbtn = (ImageButton)sender;
                GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
                int i = gvr.RowIndex;
                string str = ((HiddenField)gvr.FindControl("hdval")).Value;
                if (imgbtn.ID == "imgupdate")
                {
                    TextBox txtpurpose = (TextBox)gvr.FindControl("txtpur");
                    TextBox txtval = (TextBox)gvr.FindControl("txtval");
                    if (txtpurpose.Text.Trim() == "")
                    {
                        AlertMsg.MsgBox(Page, "Puropse name invalid!");
                        txtpurpose.Focus();
                        return;
                    }
                    if (txtval.Text.Trim() == "imgupdate")
                    {
                        AlertMsg.MsgBox(Page, "Value invalid!");
                        txtval.Focus();
                        return;
                    }
                    AttendanceDAC.HR_InsUpOptions(Convert.ToInt32(str), txtpurpose.Text.Trim(), txtval.Text.Trim(),  Convert.ToInt32(Session["UserId"]));

                }
                else
                {
                    AttendanceDAC.HR_DelOptions(Convert.ToInt32(str));
                }
                AlertMsg.MsgBox(Page, "Done!");
                BindPager();
            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry to Inconvenience! " + ex.ToString());
            }
        }

        protected void gvView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //foreach (GridViewRow gvr in gvView.Rows)
            //{
            //    TextBox txtval = (TextBox)gvr.FindControl("txtval");

            //    string str = ((HiddenField)gvr.FindControl("hdval")).Value;
            //    ImageButton imgupdate = (ImageButton)gvr.FindControl("imgupdate");
            //    int optionid = Convert.ToInt32(str);
            //    SqlParameter[] p = new SqlParameter[1];
            //    p[0] = new SqlParameter("@optionid", optionid);
            //    DataSet ds = SqlHelper.ExecuteDataset("SH_Option_value", p);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        txtval.Enabled = true;
            //        imgupdate.Visible = true;
            //    }
            //    else
            //    {
            //        txtval.Enabled = false;
            //        imgupdate.Visible = false;
            //    }
            //}

        }
      
    }
}
