using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Configuration;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpWork : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
          
        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;
        HRCommon objHrCommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpWorkPaging.FirstClick += new Paging.PageFirst(EmpWorkPaging_FirstClick);
            EmpWorkPaging.PreviousClick += new Paging.PagePrevious(EmpWorkPaging_FirstClick);
            EmpWorkPaging.NextClick += new Paging.PageNext(EmpWorkPaging_FirstClick);
            EmpWorkPaging.LastClick += new Paging.PageLast(EmpWorkPaging_FirstClick);
            EmpWorkPaging.ChangeClick += new Paging.PageChange(EmpWorkPaging_FirstClick);
            EmpWorkPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpWorkPaging_ShowRowsClick);
            EmpWorkPaging.CurrentPage = 1;
        }
        void EmpWorkPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpWorkPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpWorkPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpWorkPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpWorkPaging.ShowRows;
            BindGrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblDate.Text = "Date: " + DateTime.Now.ToString(ConfigurationManager.AppSettings["DateDisplayFormat"]);
            Label lbl = new Label();
            lbl.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateDisplayFormat"]);
            if (!IsPostBack)
            {
                ViewState["EmpworkID"] = 0;
                ViewState["Date"] = "";
                int WorkID = Convert.ToInt32(ViewState["EmpworkID"]);
                int EmpID =  Convert.ToInt32(Session["UserId"]);
                ViewState["EmpID"] = EmpID;
                BindPager();
                
                DataSet  dscout = AttendanceDAC.HR_WorkStatusCount(EmpID);
                int taskcount = Convert.ToInt32(dscout.Tables[0].Rows[0][0]);
                if (taskcount > 0)
                {
                    TextBoxWatermarkExtender1.WatermarkText = "You can enter task only once in a day! If you want to enter some more please click on Edit and add your task!. Max 700 Characters only!";
                    btnSave.Enabled = false;
                }
                mainview.ActiveViewIndex = 1;
                if (Request.QueryString.Count > 0)
                {
                    int id = Convert.ToInt32(Request.QueryString["key"].ToString());

                    if (id == 1)
                    {
                        mainview.ActiveViewIndex = 0;
                    }

                }
            }
        }
    
        public void BindGrid(HRCommon objHrCommon)//int EmpID)
        {
            try
            {
                int EmpID = Convert.ToInt32(ViewState["EmpID"]);
                objHrCommon.PageSize = EmpWorkPaging.ShowRows;
                objHrCommon.CurrentPage = EmpWorkPaging.CurrentPage;
                DataSet dswork = AttendanceDAC.GetWorkStatus(objHrCommon, EmpID, Convert.ToInt32(Session["CompanyID"]));
                
                    gvWork.DataSource = dswork;
                    gvWork.DataBind();
                
                EmpWorkPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void BindDetails(int EmpworkID)
        {

            DataSet dswork = AttendanceDAC.GetWorkStatusByID(EmpworkID);
            if (dswork != null && dswork.Tables.Count > 0 && dswork.Tables[0].Rows.Count > 0)
            {

                txtWork.Text = dswork.Tables[0].Rows[0]["Task"].ToString();

            }
        }
        protected void gvWork_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int EmpID = Convert.ToInt32(ViewState["EmpID"]);
                ViewState["EmpworkID"] = Convert.ToInt32(e.CommandArgument);
                Label lbl = new Label();
                lbl.Text = DateTime.Now.ToString(ConfigurationManager.AppSettings["DateDisplayFormat"]);
                if (e.CommandName == "Edt")
                {
                    int EmpworkID = Convert.ToInt32(e.CommandArgument);
                    ViewState["EmpworkID"] = EmpworkID;

                    DataSet ds = AttendanceDAC.GetWorkStatusByID(EmpworkID);

                    string Date = ds.Tables[0].Rows[0]["Date"].ToString();
                    ViewState["Date"] = Date;
                    BindDetails(EmpworkID);
                    
                    BindPager();
                    btnSave.Enabled = true;
                    mainview.ActiveViewIndex = 0;
                }

                if (e.CommandName == "Dlt")
                {
                    int EmpworkID = Convert.ToInt32(e.CommandArgument);
                    AttendanceDAC.DeleteWorkTask(EmpworkID);
                    EmpID = Convert.ToInt32(ViewState["EmpID"]);
                    
                    
                    DataSet dscout = AttendanceDAC.HR_WorkStatusCount(EmpID);
                    int taskcount = Convert.ToInt32(dscout.Tables[0].Rows[0][0]);
                    if (taskcount == 0)
                    {
                        TextBoxWatermarkExtender1.WatermarkText = "Enter Your Task Here!. Max 700 Characters only!";
                        btnSave.Enabled = true;
                    }
                    BindPager();
                    AlertMsg.MsgBox(Page, "Done!");
                }
            }
            catch (Exception EmpWorkDel)
            {
                AlertMsg.MsgBox(Page, EmpWorkDel.Message.ToString(),AlertMsg.MessageType.Error);
            }


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtWork.Text.Trim()== "")
                {
                    AlertMsg.MsgBox(Page, "Please enter your work.!");
                    return;
                }
                int EmpID = Convert.ToInt32(ViewState["EmpID"]);
                string Task = txtWork.Text;
                int EmpworkID = Convert.ToInt32(ViewState["EmpworkID"].ToString());
                string WorkDate = ViewState["Date"].ToString();
                DateTime Date;
                if (WorkDate == "" || WorkDate == string.Empty)
                {
                    Date = DateTime.Now;
                }
                else
                {
                    Date = CODEUtility.ConvertToDate(WorkDate, DateFormat.DayMonthYear);
                }
                AttendanceDAC.InsertWorkTask(EmpworkID, EmpID, Date, Task);
                BindPager();
                //BindGrid(EmpID);
                AlertMsg.MsgBox(Page, "Done!");
                txtWork.Text = "";
                Response.Redirect("EmpWork.aspx");
            }
            catch (Exception EmpWork)
            {
                AlertMsg.MsgBox(Page, EmpWork.Message.ToString(),AlertMsg.MessageType.Error);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtWork.Text = "";
        }
       

    }
}

