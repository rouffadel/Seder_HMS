using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using System.Data.SqlClient;
using System.Collections.Generic;
using SMSConfig;
using System.Web.Mail;
using System.Net.Mime;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class ViewAcceptedOfferList : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        int mid = 0;
        bool viewall, Editable;
        string menuname;
        string menuid;

        AttendanceDAC objApp = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);

            ViewAccOfferlilstPaging.FirstClick += new Paging.PageFirst(ViewAccOfferlilstPaging_FirstClick);
            ViewAccOfferlilstPaging.PreviousClick += new Paging.PagePrevious(ViewAccOfferlilstPaging_FirstClick);
            ViewAccOfferlilstPaging.NextClick += new Paging.PageNext(ViewAccOfferlilstPaging_FirstClick);
            ViewAccOfferlilstPaging.LastClick += new Paging.PageLast(ViewAccOfferlilstPaging_FirstClick);
            ViewAccOfferlilstPaging.ChangeClick += new Paging.PageChange(ViewAccOfferlilstPaging_FirstClick);
            ViewAccOfferlilstPaging.ShowRowsClick += new Paging.ShowRowsChange(ViewAccOfferlilstPaging_ShowRowsClick);
            ViewAccOfferlilstPaging.CurrentPage = 1;
        }
        void ViewAccOfferlilstPaging_ShowRowsClick(object sender, EventArgs e)
        {
            ViewAccOfferlilstPaging.CurrentPage = 1;
            BindPager();
        }
        void ViewAccOfferlilstPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = ViewAccOfferlilstPaging.CurrentPage;
            objHrCommon.CurrentPage = ViewAccOfferlilstPaging.ShowRows;
            AcceptedOfferList(objHrCommon);
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
                GetParentMenuId();
                BindPositions();
                BindPager();
          
            }
        }
      
        public void BindPositions()
        {
            try
            {

                DataSet ds = (DataSet)objApp.GetPositionList();
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlPosition.DataValueField = "PosID";
                    ddlPosition.DataTextField = "Position";
                    ddlPosition.DataSource = ds;
                    ddlPosition.DataBind();
                    ddlPosition.Items.Insert(0, new ListItem("--ALL--", "0"));

                }

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
            int ModuleId = ModuleID; ;

          

            DataSet ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
            int MenuId = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
                ViewState["Editable"] = Editable = (bool)ds.Tables[0].Rows[0]["Editable"];
                ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
                viewall = (bool)ViewState["ViewAll"];
                menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
                menuid = MenuId.ToString();
                mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
                gvAcceptedOfferList.Columns[4].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvAcceptedOfferList.Columns[6].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
                gvAcceptedOfferList.Columns[7].Visible = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }


        public void AcceptedOfferList(HRCommon objHrCommon)
        {

            objHrCommon.PageSize = ViewAccOfferlilstPaging.ShowRows;
            objHrCommon.CurrentPage = ViewAccOfferlilstPaging.CurrentPage;
            int? WSID = null;
           
            if (Convert.ToInt32(ddlWs_hid.Value == "" ? "0" :ddlWs_hid.Value) != 0)
                WSID = Convert.ToInt32(Convert.ToInt32(ddlWs_hid.Value == "" ? "0" : ddlWs_hid.Value));

            int? POSID = null;
            if (ddlPosition.SelectedValue != "0" && ddlPosition.SelectedValue != "")
                POSID = Convert.ToInt32(ddlPosition.SelectedValue);
            int Status = 5;
            if (chkJoinEmpLst.Checked)
                Status = 6;
           
            DataSet ds = AttendanceDAC.AcceptedOfferListByPaging(objHrCommon, WSID, POSID, Status, Convert.ToInt32(Session["CompanyID"]));
            if (!chkJoinEmpLst.Checked)
            {
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvAcceptedOfferList.DataSource = ds;
                    gvAcceptedOfferList.DataBind();
                    ViewAccOfferlilstPaging.Visible = true;

                }
                else
                {
                    ViewAccOfferlilstPaging.Visible = false;
                    gvAcceptedOfferList.DataSource = null;
                    gvAcceptedOfferList.DataBind();
                }
                gvAcceptedOfferList.Visible = true;
                grdJoinedEmp.Visible = false;
            }
            else
            {
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    grdJoinedEmp.DataSource = ds;
                    grdJoinedEmp.DataBind();
                    ViewAccOfferlilstPaging.Visible = true;
                }
                else
                {
                    ViewAccOfferlilstPaging.Visible = false;
                    grdJoinedEmp.DataSource = null;
                    grdJoinedEmp.DataBind();
                }
                gvAcceptedOfferList.Visible = false;
                grdJoinedEmp.Visible = true;
            }
            ViewAccOfferlilstPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

        }
        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewAccOfferlilstPaging.CurrentPage = 1;
            try
            {
                BindPager();
            }
            catch (Exception)
            {
            }
        }

        protected string FormatInput(object EntryType)
        {
            string retValue = "";
            string input = EntryType.ToString();
            if (input == "1")
            {
                retValue = "Globally";
            }
            if (input == "2")
            {
                retValue = "Locally";
            }
            return retValue;
        }
        protected void gvAcceptedOfferList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int AppID = Convert.ToInt32(e.CommandArgument);
                objHrCommon.AppID = AppID;
                objApp.DeleteApplicant(objHrCommon);
                BindPager();
             
            }
            if (e.CommandName == "Join")
            {
                int AppID = Convert.ToInt32(e.CommandArgument);

                int Status = 6; //need to change
                objApp.UpdateAppIDStaus(AppID, Status);
             
                BindPager();
               
                InsertEmpDetails(AppID);
            }
            if (e.CommandName == "Accept")
            {
                int AppID = Convert.ToInt32(e.CommandArgument);

                int Status = 6; //need to change
                objApp.UpdateAppIDStaus(AppID, Status);
                BindPager();
              
            }
            if (e.CommandName == "Reject")
            {
                int AppID = Convert.ToInt32(e.CommandArgument);


                int Status = 0; //need to change
                objApp.UpdateAppIDStaus(AppID, Status);
                BindPager();
               

            }
        }

        public void InsertEmpDetails(int AppID)
        {
         
            DataSet ds = objApp.GetAppOfferDetails(AppID);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                objHrCommon.AppID = AppID;
                objHrCommon.DeptID = Convert.ToDouble(ds.Tables[0].Rows[0]["DeptID"].ToString());
                objHrCommon.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                objHrCommon.MName = ds.Tables[0].Rows[0]["MName"].ToString();
                objHrCommon.LName = ds.Tables[0].Rows[0]["LName"].ToString();
                objHrCommon.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                objHrCommon.Qualification = ds.Tables[0].Rows[0]["Qua"].ToString();
                objHrCommon.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                objHrCommon.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                objHrCommon.SiteID = Convert.ToInt32(ds.Tables[0].Rows[0]["SiteID"].ToString());
                objHrCommon.ImageType = ds.Tables[0].Rows[0]["ImageType"].ToString();
                objHrCommon.DOB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"]);
                objHrCommon.OfferLetter = ds.Tables[0].Rows[0]["OfferLetter"].ToString();
                objHrCommon.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                objHrCommon.City = ds.Tables[0].Rows[0]["City"].ToString();
                objHrCommon.State = ds.Tables[0].Rows[0]["State"].ToString();
                objHrCommon.Country = ds.Tables[0].Rows[0]["Country"].ToString();
                objHrCommon.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                objHrCommon.Pin = Convert.ToInt32(ds.Tables[0].Rows[0]["pin"].ToString());
                objHrCommon.SameAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                objHrCommon.Salary = Convert.ToDouble(ds.Tables[0].Rows[0]["Salary"].ToString());
                objHrCommon.DesigID = Convert.ToInt32(ds.Tables[0].Rows[0]["DesigID"].ToString());
                objHrCommon.TradeID = Convert.ToInt32(ds.Tables[0].Rows[0]["TradeID"].ToString());
                objHrCommon.ReqDate = CODEUtility.ConvertToDate(ds.Tables[0].Rows[0]["ReqDOJ"].ToString(), DateFormat.DayMonthYear);//Convert.ToDateTime(ds.Tables[0].Rows[0]["ReqDOJ"]);
                int EmpID = Convert.ToInt32(objApp.InsertEmpDetails(objHrCommon));
                objApp.Insert_Emp_Edu_Exp_Details(EmpID, AppID);
                try
                {
                    string filename = "", ext = "", path = "", ThumbPath = "";
                    filename = Server.MapPath("./ApplicantImages/" + AppID + "." + ds.Tables[0].Rows[0]["ImageType"].ToString());
                    if (filename != "")
                        ext = filename.Split('/')[filename.Split('/').Length - 1];
                    ext = filename.Split('.')[filename.Split('.').Length - 1];
                    if (filename != "")
                    {

                        path = Server.MapPath(".\\EmpImages\\" + EmpID + "." + ext);
                        ThumbPath = Server.MapPath(".\\EmpImages\\" + EmpID + "_thumb." + ext);

                        PicUtil.ConvertPic(filename, 128, 160, path);
                        PicUtil.ConvertPic(filename, 19, 24, ThumbPath);

                    }
                }
                catch
                {
                    //Response.Redirect("CreateEmployee.aspx?Id=" + EmpID);
                }
                Response.Redirect("CreateEmployee.aspx?Id=" + EmpID + "&Join=" + 1);
            }

        }
        protected void Showall_Click(object sender, EventArgs e)
        {
            ViewAccOfferlilstPaging.CurrentPage = 1;
            BindPager();
        }
        protected void gvAcceptedOfferList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gvAcceptedOfferList.Rows.Count > 0)
                {
                    LinkButton lnkJoin = (LinkButton)e.Row.FindControl("lnkJoin");
                    LinkButton lnkRej = (LinkButton)e.Row.FindControl("lnkReject");
                    lnkJoin.Enabled = lnkRej.Enabled = true;
                }
            }
        }
        protected void chkJoinEmpLst_CheckedChanged(object sender, EventArgs e)
        {
            BindPager();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
           
            DataSet ds = AttendanceDAC.HR_GetWorkSite_By_AcceptedOfferList_googlesearch(prefixText.Trim());
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray(); ;// txtItems.ToArray();

        }
    }
}