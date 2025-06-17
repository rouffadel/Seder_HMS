using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.IO;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class EmpPrevTDS : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objEmployee = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        bool Editable;

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            EmpPreTDSPaging.FirstClick += new Paging.PageFirst(EmpPreTDSPaging_FirstClick);
            EmpPreTDSPaging.PreviousClick += new Paging.PagePrevious(EmpPreTDSPaging_FirstClick);
            EmpPreTDSPaging.NextClick += new Paging.PageNext(EmpPreTDSPaging_FirstClick);
            EmpPreTDSPaging.LastClick += new Paging.PageLast(EmpPreTDSPaging_FirstClick);
            EmpPreTDSPaging.ChangeClick += new Paging.PageChange(EmpPreTDSPaging_FirstClick);
            EmpPreTDSPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpPreTDSPaging_ShowRowsClick);
            EmpPreTDSPaging.CurrentPage = 1;
        }
        void EmpPreTDSPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpPreTDSPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpPreTDSPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpPreTDSPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpPreTDSPaging.ShowRows;
            BindEnpTDS(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindWorkSites();
                BindPager();
            }
        }
        public void BindWorkSites()
        {

            try
            {
                FIllObject.FillDropDown(ref ddlworksites, "HR_GetWorkSite_By_EmpListTDS");
                    if (Convert.ToInt32(Session["MonitorSite"]) != 0)
                    {
                        ddlworksites.Items.FindByValue(Session["MonitorSite"].ToString()).Selected = true;
                        ddlworksites.Enabled = false;
                    }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void BindDeparmetBySite(int Site)
        {
            DataSet ds = AttendanceDAC.BindDeparmetBySite(Site, Convert.ToInt32(Session["CompanyID"]));
            ddldepartments.DataSource = ds;
            ddldepartments.DataTextField = "DeptName";
            ddldepartments.DataValueField = "DepartmentUId";
            ddldepartments.DataBind();
            ddldepartments.Items.Insert(0, new ListItem("---ALL---", "0", true));
        }


    
        public void BindEnpTDS(HRCommon objHrCommon)
        {
            try
            {
                objHrCommon.PageSize = EmpPreTDSPaging.ShowRows;
                objHrCommon.CurrentPage = EmpPreTDSPaging.CurrentPage;
                int? DeptID = null;
                int? SiteID = null;
                if (ddlworksites.SelectedItem.Value != "0")
                    SiteID = Convert.ToInt32(ddlworksites.SelectedItem.Value);
                if (ddldepartments.SelectedValue != "0")
                    DeptID = Convert.ToInt32(ddldepartments.SelectedValue);

                objHrCommon.OldEmpID = null;
                if (txtOldEmpID.Text != "")
                    objHrCommon.OldEmpID = txtOldEmpID.Text;

                int? EmpID = null;
                if (txtEmpID.Text != "")
                    EmpID = Convert.ToInt32(txtEmpID.Text);

                objHrCommon.FName = txtusername.Text;
                grdPrvTDS.DataSource = null;
                grdPrvTDS.DataBind();
                DataSet ds = AttendanceDAC.GetEmpPreTDS(objHrCommon, SiteID, DeptID, EmpID, Convert.ToInt32(Session["CompanyID"]));

                ViewState["BingEmpTDS"] = ds;
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdPrvTDS.DataSource = ds;
                    grdPrvTDS.DataBind();

                }
                EmpPreTDSPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

            }
            catch (Exception e)
            {
                throw e;
            }
        }


       


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();
        }
        protected void grdPrvTDS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string path;
            if (e.CommandName == "Upd")
            {
                GridViewRow grd = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;

                Label lblEmpID = (Label)grdPrvTDS.Rows[grd.RowIndex].FindControl("lblEmpId");
                int EmpID = int.Parse(lblEmpID.Text);
                Label Ext = (Label)grdPrvTDS.Rows[grd.RowIndex].FindControl("lblExt");
                string Exstn = Ext.Text;
                TextBox txtTDSAmt = (TextBox)grdPrvTDS.Rows[grd.RowIndex].FindControl("TDSAmount");
                double TDSAmount = double.Parse(txtTDSAmt.Text);

                int FinYearID = Convert.ToInt32(Session["FinYearID"]);

                FileUpload UploadProof1 = (FileUpload)grd.Cells[8].FindControl("UploadProof");
                String MyString = string.Empty;
                string extension = string.Empty;
                if (UploadProof1.HasFile)
                {
                    MyString = EmpID.ToString();
                    string Filename = UploadProof1.PostedFile.FileName.ToLower();
                    extension = Filename.Split('.')[Filename.Split('.').Length - 1];
                    if (Exstn != "")
                        path = Server.MapPath(".\\EmpPreviousTDS\\" + EmpID + "." + Exstn);
                    else
                        path = "";
                    if (path != "")
                    {
                        string Vf = Path.GetFileName(path);
                        System.IO.File.Delete(path);
                    }
                    string storePath = Server.MapPath("~") + "/" + "EmpPreviousTDS/";
                    if (!Directory.Exists(storePath))
                        Directory.CreateDirectory(storePath);
                    UploadProof1.PostedFile.SaveAs(storePath + "/" + MyString + "." + extension);
                }

                string ext;
                if (UploadProof1.HasFile)
                {
                    ext = extension;
                }
                else
                {
                    DataSet ds = (DataSet)ViewState["BingEmpTDS"];
                    GridViewRow grv = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                    int Index = grv.RowIndex;
                    if (ds.Tables[0].Rows[Index]["Ext"].ToString() != "")
                        ext = ds.Tables[0].Rows[Index]["Ext"].ToString();
                    else
                        ext = null;
                }
                int UserID =  Convert.ToInt32(Session["UserId"]);

                AttendanceDAC.InsUpdateEmpPreTDS(EmpID, TDSAmount, ext, UserID, FinYearID);
                BindPager();
            }
        }

        protected void grdPrvTDS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string ext = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkUpd = (LinkButton)e.Row.FindControl("btnUpdate");
                lnkUpd.Enabled = Editable;
                LinkButton lnkProof = (LinkButton)e.Row.FindControl("lnkProof");
                Label lblEmpID = (Label)e.Row.FindControl("lblEmpId");
                Label lblExt = (Label)e.Row.FindControl("lblExt");
                string empID = lblEmpID.Text;
                ext = lblExt.Text;
                if (ext != "")
                {
                    lnkProof.Attributes.Add("onclick", "javascript:return ShowProof('" + empID + "','" + ext + "')");
                }
                else
                {
                    lnkProof.Text = "No Proof";
                    lnkProof.Enabled = false;
                }
            }
        }

        protected void ddlworksites_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeparmetBySite(Convert.ToInt32(ddlworksites.SelectedValue));
        }
    }
}