using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Data.SqlClient;

namespace AECLOGIC.ERP.HMS
{
    public partial class ITDeductions : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        
        bool Status;
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

                ViewState["DedID"] = "";
                ViewState["Proof"] = "";
                tblEdit.Visible = true;
                Employees();
                BindSections();
                BindAssessYear();
                BindGrid();

                if (Request.QueryString.Count > 0)
                {
                    ViewState["EmpID"] = Convert.ToInt32(Request.QueryString["EmpID"]);
                }
                else
                {
                    ViewState["EmpID"] = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                }
            }
        }
        public int GetParentMenuId()
        {
            string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
            int ModuleId = ModuleID; ;

            

          DataSet  ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
              Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvLeaveProfile.Columns[4].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
                btnSubmit.Enabled = Editable;
            }
            return MenuId;
        }
        public void BindSections()
        {
            DataSet ds = PayRollMgr.GetIT_SectionsList(Convert.ToInt32(Session["FinYearID"]));
            ddlSections.DataSource = ds;
            ddlSections.DataValueField = "SectionID";
            ddlSections.DataTextField = "SectionName";
            ddlSections.DataBind();
            ddlSections.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        public void BindAssessYear()
        {
            DataSet ds = PayRollMgr.GetFinacialYearList();
            ddlAssessmentYear.DataSource = ds;
            ddlAssessmentYear.DataValueField = "FinYearId";
            ddlAssessmentYear.DataTextField = "Name";
            ddlAssessmentYear.DataBind();
            ddlAssessmentYear.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlFinYear.DataSource = ds;
            ddlFinYear.DataValueField = "FinYearId";
            ddlFinYear.DataTextField = "Name";
            ddlFinYear.DataBind();

            int CurrentFinYear = ds.Tables[0].Rows.Count;
            string FromDate = CODEUtility.ConvertToDate(ds.Tables[0].Rows[0]["FromDate"].ToString(), DateFormat.DayMonthYear).ToString();
            string ToDate = CODEUtility.ConvertToDate(ds.Tables[0].Rows[0]["TODate"].ToString(), DateFormat.DayMonthYear).ToString();
            string[] str = new string[3];
            str = ToDate.Split('/');
            string YearTime = str[0];
            string[] strYr = new string[2];
            strYr = YearTime.Split(' ');
            int Year = 0;
            if (strYr.Length > 1)
                Year = Convert.ToInt32(strYr[1].Length);

            int Month = Convert.ToInt32(str[0].Length);
            int Day = Convert.ToInt32(str[0].Length);
            int CYear = Convert.ToInt32(DateTime.Now.Year);
            int CMonth = Convert.ToInt32(DateTime.Now.Month);
            int CDay = Convert.ToInt32(DateTime.Now.Day);
            int PreviousFinYear = CurrentFinYear - (Year - CYear);
            if (CYear < Year)
            {
                if (CMonth <= Month)
                {
                    if (CDay <= Day)        //Not Necessary
                    {
                        ddlFinYear.SelectedValue = PreviousFinYear.ToString();
                    }
                }
            }
            else
            {
                ddlFinYear.SelectedIndex = CurrentFinYear;
            }
        }
        public void BindGrid()
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;
            int? EmpID = null;
            if (ddlSearEmployee.SelectedIndex != 0)
            {
                EmpID = Convert.ToInt32(ddlSearEmployee.SelectedItem.Value);
            }
            DataSet ds = PayRollMgr.GetIT_SavingsList(EmpID, Convert.ToInt32(ddlFinYear.SelectedItem.Value));
            gvLeaveProfile.DataSource = ds;
            gvLeaveProfile.DataBind();
        }
        public void BindDetails(int ID)
        {
            tblEdit.Visible = false;
            tblNew.Visible = true;
            pnltblNew.Visible = true;
            DataSet ds = PayRollMgr.GetIT_SavingsDetails(ID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                ddlSections.SelectedItem.Value = ds.Tables[0].Rows[0]["SectionID"].ToString();
                ddlAssessmentYear.SelectedValue = ds.Tables[0].Rows[0]["AssYearId"].ToString();
                ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["EmpID"].ToString();
                ViewState["Proof"] = ds.Tables[0].Rows[0]["Proof"].ToString();
            }
        }
        protected void gvLeaveProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["DedID"] = ID;
            if (e.CommandName == "Edt")
            {
                BindDetails(ID);
            }
            if (e.CommandName == "Del")
            {
                try
                {
                    GridViewRow gvr = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    LinkButton lnkdel = (LinkButton)gvr.FindControl("lnkdel");

                    if (lnkdel.Text == "Active")
                    {
                        Status = true;
                    }
                    else
                    {
                        Status = false;
                    }

                    AttendanceDAC.HR_Upd_DesigStatus(ID, Status);
                    Employees();
                    //BindDetails(ID);

                    BindGrid();
                    AlertMsg.MsgBox(Page, "Done..!");
                }


                catch (Exception DeptHead)
                {
                    AlertMsg.MsgBox(Page, DeptHead.Message.ToString(),AlertMsg.MessageType.Error);
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int DedID = 0;
                string filename = "", ext = "", path = "";
                if (FUDProof.HasFile)
                {
                    filename = FUDProof.PostedFile.FileName;
                }
                if (filename != "")
                {
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                }
                else
                {
                    if (ViewState["Proof"].ToString() != "")
                    {
                        ext = ViewState["Proof"].ToString();
                    }
                    else
                    {
                        ext = "";
                    }
                }

                if (ViewState["DedID"].ToString() != null && ViewState["DedID"].ToString() != string.Empty)
                {
                    DedID = Convert.ToInt32(ViewState["DedID"].ToString());
                }
                DedID = Convert.ToInt32(PayRollMgr.InsUpdateIT_Savings(DedID,
                                                                       Convert.ToInt32(ddlEmployee.SelectedItem.Value),
                                                                       Convert.ToInt32(ddlSections.SelectedItem.Value),
                                                                       txtItemName.Text,
                                                                       Convert.ToDouble(txtAmount.Text),
                                                                       Convert.ToInt32(ddlAssessmentYear.SelectedValue),
                                                                       ext,
                                                                       Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString())));
                if (filename != "")
                {
                    if (DedID != 0)
                    {
                        path = Server.MapPath(".\\DedProofs\\" + DedID + "." + ext);
                        try
                        {
                            FUDProof.PostedFile.SaveAs(path);
                        }
                        catch { throw new Exception("FUDProof.PostedFile.SaveAs(path)"); }

                    }
                }

                AlertMsg.MsgBox(Page, "Done.! ");

                BindGrid();
                Clear();
            }
            catch (Exception ITDec)
            {
                AlertMsg.MsgBox(Page, ITDec.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();

        }
        public void Employees()
        {

            if (rdStatus.SelectedValue == "1")
            {
                Status = true;
            }
            else
            {
                Status = false;
            }
            
            string EmpName = string.Empty;
            DataSet ds = AttendanceDAC.HR_FilterSearchAll(EmpName);
            ddlEmployee.DataSource = ds.Tables[0];
            ddlEmployee.DataTextField = "name";
            ddlEmployee.DataValueField = "EmpID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("---SELECT---", "0", true));

           

        }
        public void Clear()
        {
            txtItemName.Text = "";
            txtAmount.Text = "";
            ddlSections.SelectedIndex = 0;
            ddlEmployee.SelectedIndex = 0;
            ViewState["DedID"] = "";
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            tblNew.Visible = true;
            pnltblNew.Visible = true;
            tblEdit.Visible = false;

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            tblEdit.Visible = true;
            tblNew.Visible = false;
            pnltblNew.Visible = false;

        }

        public string DocNavigateUrl(string Id, string Ex)
        {
            string ReturnVal = "";
            ReturnVal = "./DedProofs/" + Id + "." + Ex;
            return ReturnVal;
        }
        protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int finid = Convert.ToInt32(ddlFinYear.SelectedValue);
            SqlParameter[] param=new SqlParameter[1];
            param[0] = new SqlParameter("@AssYearId", finid);

            FIllObject.FillDropDown(ref ddlSearEmployee, "HR_FilterSearchAll_By_FinYear",param);

            int EmpID = Convert.ToInt32(ViewState["EmpID"]);
          DataSet   ds = PayRollMgr.GetIT_DeductionsList( Convert.ToInt32(Session["UserId"]), Convert.ToInt32(ddlFinYear.SelectedValue));
            gvLeaveProfile.DataSource = ds;
            gvLeaveProfile.DataBind();
        }

        protected void gvLeaveProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkedt = (LinkButton)e.Row.FindControl("lnkEdit");
                lnkedt.Enabled = Editable;
            }
        }

        protected void rdStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        public string GetText()
        {

            if (rdStatus.SelectedValue == "1")
            {
                return "Deactivate";
            }
            else
            {
                return "Activate";
            }
        }
       

    }
}
