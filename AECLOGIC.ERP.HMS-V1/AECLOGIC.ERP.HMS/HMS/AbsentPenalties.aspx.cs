using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;
namespace AECLOGIC.ERP.HMS
{
    public partial class AbsentPenalties : AECLOGIC.ERP.COMMON.WebFormMaster
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
            EmpListPaging.FirstClick += new Paging.PageFirst(EmpListPaging_FirstClick);
            EmpListPaging.PreviousClick += new Paging.PagePrevious(EmpListPaging_FirstClick);
            EmpListPaging.NextClick += new Paging.PageNext(EmpListPaging_FirstClick);
            EmpListPaging.LastClick += new Paging.PageLast(EmpListPaging_FirstClick);
            EmpListPaging.ChangeClick += new Paging.PageChange(EmpListPaging_FirstClick);
            EmpListPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpListPaging_ShowRowsClick);
            EmpListPaging.CurrentPage = 1;
        }
        void EmpListPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpListPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpListPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpListPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpListPaging.ShowRows;
            EmployeBind(objHrCommon);
        }
      protected  DataTable   addInitalyRowIngridView()
        {
               DataTable dt = new DataTable();
               dt.Columns.Add(new DataColumn("occurance", typeof(int)));//for TextBox value 
              dt.Columns.Add(new DataColumn("penality", typeof(int )));
               DataRow dr=dt.NewRow();
               dr["occurance"] = "0";
               dr["penality"] ="0";
               dt.Rows.Add(dr);
               dt.AcceptChanges();
          return dt;
        }
        void EmployeBind(HRCommon objHrCommon)
        {
            try
            {
                EmpListPaging.Visible = false;
              int Status = 0;
                if (rblDesg.SelectedValue == "1")
                {
                    Status = 1;
                }
                DataSet ds = AttendanceDAC.T_HMS_AbsentPenalties_GetList(Status, "GET_T_HMS_AbsentPenalties_ByID");
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvRMItem.DataSource = ds;
                   gvRMItem.DataBind();
                 if (gvRMItem.Rows.Count > 0)
                     GroupGridView(gvRMItem);
                }
                else
                {
                   gvRMItem.DataSource = null;
                    gvRMItem.DataBind();
                }
            }
            catch 
            {
            }
        }
        void GroupGridView( GridView grv)
        {
           if (grv.Rows.Count>1)
           {
               string val_0 = ((LinkButton)grv.Rows[0].Cells[0].FindControl("lnkEdit")).Text;
               string val_1 = ((LinkButton)grv.Rows[1].Cells[0].FindControl("lnkEdit")).Text; ;
                for (int i=0 ;i<grv.Rows.Count ;i++)
                {
                   if (i!=0)
                   {
                        ((LinkButton)grv.Rows[i].Cells[0].FindControl("lnkEdit")).Visible = true;
                   }
                   val_0 = ((LinkButton)grv.Rows[i].Cells[0].FindControl("lnkEdit")).Text;
                   if (i + 1 < grv.Rows.Count)
                       val_1 = ((LinkButton)grv.Rows[i + 1].Cells[0].FindControl("lnkEdit")).Text;
                }
           }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetParentMenuId();
                ViewState["dt"] = addInitalyRowIngridView();
                grdaddDays.DataSource = addInitalyRowIngridView();
                grdaddDays.DataBind();
                ViewState["CateId"] = "";
                if (Convert.ToInt32(Request.QueryString["key"]) == 1)
                {
                    this.Title = "Air Ticket Configuration";
                    tblNewk.Visible = true;
                    tblEdit.Visible = false;
                    gvRMItem.Visible = false;
                }
                else
                {
                    tblNewk.Visible = false;
                    tblEdit.Visible = true;
                    gvRMItem.Visible = true;
                    EmployeBind(objHrCommon);
                }
            }
        }
        #region CodeBYkAUSHAL
        private void BindLocations(  DropDownList ddl)
        {
            DataSet ds = AttendanceDAC.CMS_Get_City();
            ddl.DataSource = ds;
            ddl.DataTextField = ds.Tables[0].Columns["CItyName"].ToString();
            ddl.DataValueField = ds.Tables[0].Columns["CityID"].ToString();
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---Select---", "0"));
            int c = ds.Tables[0].Rows.Count;
        }
        #endregion
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
                gvRMItem.Columns[1].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
            }
            return MenuId;
        }
        public void BindDetails(int ID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("occurance", typeof(int)));//for TextBox value 
            dt.Columns.Add(new DataColumn("penality", typeof(int)));
         //  ------
             //  --------------
            DataSet ds = AttendanceDAC.T_HMS_AbsentPenalties_GetList(ID, "GET_T_HMS_AbsentPenalties_Bynoofdays");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in ds.Tables[0].Rows )
                {
                    DataRow dr = dt.NewRow();
                    dr["occurance"] = dr1["occurance"];
                    dr["penality"] = dr1["penality"];
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
                ViewState["dt"] = dt;
                grdaddDays.DataSource = dt;
                grdaddDays.DataBind();
                txtnoOfday.Text = ds.Tables[0].Rows[0][0].ToString();
                    tblEdit.Visible = false;
                tblNewk.Visible = true;
                 btnSubmit.Text = "Update";
            }
        }
        protected void btn_reste_Click(object sender,EventArgs e)
        {
            btnSubmit.Text = "Submit";
            Clear();
        }
        protected void gvWages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["CateId"] = ID;
            bool Status = true;
            if (rblDesg.SelectedValue == "1")
            {
                Status = false;
            }
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            else
            {
                try
                {
                    EmployeBind(objHrCommon);
                }
                catch (Exception DelDesig)
                {
                    AlertMsg.MsgBox(Page, DelDesig.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }
       protected void  addrows(object sender,EventArgs e)
      {
          TextBox txt = sender as TextBox;
          GridViewRow row = (GridViewRow) txt.NamingContainer ;
          int rowIndex = row.RowIndex;
          if (txt.Text.Trim() == "")
              txt.Text = "0";
          DataTable dt =  (DataTable ) ViewState["dt"];
          if (txt.ID == "txtoccurance")
              dt.Rows[rowIndex][0] = txt.Text.Trim ();
           else
              dt.Rows[rowIndex][1] = txt.Text.Trim(); ;
          dt.AcceptChanges();
          if (grdaddDays.Rows.Count <= rowIndex + 1)
          {
              DataRow dr = dt.NewRow();
              dr["occurance"] = "0";
              dr["penality"] = "0";
              dt.Rows.Add(dr);
              dt.AcceptChanges();
          }
          ViewState["dt"] = dt;
          grdaddDays.DataSource = dt;
          grdaddDays.DataBind();
      }
     protected void  DelRows(object sender,EventArgs e)
       {
           ImageButton txt = sender as ImageButton;
           GridViewRow row = (GridViewRow)txt.NamingContainer;
           int rowIndex = row.RowIndex;
           DataTable dt = (DataTable)ViewState["dt"];
           dt.Rows.RemoveAt (rowIndex );
           dt.AcceptChanges();
           ViewState["dt"] = dt;
           grdaddDays.DataSource = dt;
           grdaddDays.DataBind();
       }
        public string GetText()
        {
            if (rblDesg.SelectedValue == "1")
            {
                return "Deactivate";
            }
            else
            {
                return "Activate";
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int CateId = 0;
                if (ViewState["CateId"].ToString() != null && ViewState["CateId"].ToString() != string.Empty)
                {
                    CateId = Convert.ToInt32(ViewState["CateId"].ToString());
                   int i = AttendanceDAC.T_HMS_AbsentPenalties_Actions(CateId, "Delete_T_HMS_AbsentPenalties_BynoOfdays");
                }
                DataTable dt =(DataTable ) ViewState["dt"];
                int Output = 0;
                foreach( DataRow  dr in dt.Rows   )
                {
                    if (dr[0].ToString() != "0" && dr[1].ToString() != "0" && dr[0].ToString() != "" && dr[1].ToString() != "")
                 Output = AttendanceDAC.T_HMS_InsUpd_AbsentPenalties(CateId, Convert.ToInt32(txtnoOfday.Text.Trim ()), Convert.ToInt32(dr[0]), Convert.ToInt32(dr[1]),   Convert.ToInt32(Session["UserId"]), 1);
                if (Output == 2)
                    break;
                }
                if (Output == 1) 
                {
                    AlertMsg.MsgBox(Page, "Done.!!");
                }
                else if (Output == 2)
                {
                    AlertMsg.MsgBox(Page, "Already Exists.!");
                }
                else
                    AlertMsg.MsgBox(Page, "Updated.!");
                EmployeBind(objHrCommon);
                tblNewk.Visible = false;
                tblEdit.Visible = true;
                gvRMItem.Visible = true;
                Clear();
            }
            catch (Exception AddDesignation)
            {
                AlertMsg.MsgBox(Page, AddDesignation.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }
        public void Clear()
        {
            ViewState["dt"] = addInitalyRowIngridView();
            grdaddDays.DataSource = addInitalyRowIngridView();
            grdaddDays.DataBind();
            txtnoOfday.Text = "";
            ViewState["CateId"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            tblNewk.Visible = true;
            tblEdit.Visible = false;
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNewk.Visible = false;
        }
        protected void rblDesg_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeBind(objHrCommon);
        }
        protected void OnDataBound(object sender, EventArgs e)
        {
            for (int i = gvRMItem.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = gvRMItem.Rows[i];
                GridViewRow previousRow = gvRMItem.Rows[i - 1];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }
    }
}