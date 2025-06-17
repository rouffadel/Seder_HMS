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
using System.Data.SqlClient;

using System.Collections.Generic;
using SMSConfig;
using System.Web.Mail;
using System.Net.Mime;
using Aeclogic.Common.DAL;
namespace AECLOGIC.ERP.COMMON
{
    public partial class CommonMaster : System.Web.UI.MasterPage
    {
        // AttendanceDAC Attendance = new AttendanceDAC();
        //DataSet ds = new DataSet();
        //int ModuleId = 1;
        //int MenuId = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private DataSet GetDataSet(string StoredProcedureName)
        {

            try
            {
                return SQLDBUtil.ExecuteDataset(StoredProcedureName);//"HR_Get_Applicantscount");
 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <param name="RoleId"></param>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        private DataSet GetStatus(int ModuleId, int RoleId, int EmpId)
        {

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@ModuleId", ModuleId);
            p[1] = new SqlParameter("@RoleId", RoleId);
            p[2] = new SqlParameter("@EmpId", EmpId);
            return SQLDBUtil.ExecuteDataset("HMS_GetStatus", p);
 
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="empID"></param>
        /// <returns></returns>
        private DataSet G_EMP_GteEmployeePic(int empID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@EmpID", empID);
                return SQLDBUtil.ExecuteDataset("G_EMP_GteEmployeePic", param);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (Session["UserId"] == null)
                {
                    Response.Redirect("../Logon.aspx?SessionExpire=" + true + 1);
                }
                DataSet ds = GetDataSet("HR_Get_Applicantscount");

                if (Session["UserId"] != null && Session["Username"].ToString() != null && Session["Username"].ToString() != "")
                {
                    lblName.Text = Session["UserId"].ToString() + " " + Session["Username"].ToString();
                }

                SqlParameter[] parms1 = new SqlParameter[2];
                parms1[0] = new SqlParameter("@empid", Session["UserID"].ToString());
                parms1[1] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms1[1].Direction = ParameterDirection.ReturnValue;
                DataSet ds1 = SqlHelper.ExecuteDataset("sg_NotificationsEmpwise", parms1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    gvnotifications.DataSource = ds1.Tables[0];
                    lblsum.Text = parms1[1].Value.ToString();
                }
                else
                    gvnotifications.DataSource = null;
                gvnotifications.DataBind();


                SqlParameter[] parms12 = new SqlParameter[2];

                parms12[0] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
                parms12[0].Direction = ParameterDirection.ReturnValue;
                DataSet ds12 = SqlHelper.ExecuteDataset("sg_noofusercount", parms12);
                if (ds12.Tables[0].Rows.Count > 0)
                {
                    grdNoOfUsers.DataSource = ds12.Tables[0];
                    lblNoofusers.Text = parms12[0].ToString();
                }
                else
                    grdNoOfUsers.DataSource = null;
                grdNoOfUsers.DataBind();



                //HypOffers.Text = "At Offer:" + ds.Tables[4].Rows[0][0];
                //HypOffers.NavigateUrl = "~/ViewOfferLetterList.aspx";
                //HypSelected.Text = "Selected:" + ds.Tables[3].Rows[0][0];
                //HypSelected.NavigateUrl = "~/ViewSelectedApplicantList.aspx";
                //lnkProcess.Text = "Applicants:" + ds.Tables[2].Rows[0][0];
                //lnkProcess.NavigateUrl = "~/ViewApplicantList.aspx";
                //DataSet ds2 = new DataSet();
                //ds2 = AttendanceDAC.HR_GetTaskUpdates();
                //lblTaskUpdaters.Text = ds2.Tables[0].Rows[0][0].ToString();
                //lblNotUpdated.Font.Bold = true;
                //lblTaskUpdated.Font.Bold = true;
                //lblTaskUpdaters.Font.Bold = true;
                //lblTaskUpdated.Text = ds2.Tables[1].Rows[0][0].ToString();
                //lblNotUpdated.Text = ds2.Tables[6].Rows[0][0].ToString();
                //lblNewJoin.Text = ds2.Tables[4].Rows[0][0].ToString();
                //lblUpdatedToday.Text = ds2.Tables[5].Rows[0][0].ToString();

                #region Old Code

                //HypOffers.Text = "At Offer:" + ds.Tables[4].Rows[0][0];
                //HypOffers.NavigateUrl = "~/ViewOfferLetterList.aspx";
                //HypSelected.Text = "Selected:" + ds.Tables[3].Rows[0][0];
                //HypSelected.NavigateUrl = "~/ViewSelectedApplicantList.aspx";
                //lnkProcess.Text = "Applicants:" + ds.Tables[2].Rows[0][0];
                //lnkProcess.NavigateUrl = "~/ViewApplicantList.aspx";
                //DataSet ds2 = new DataSet();
                DataSet ds2 = GetDataSet("HR_GetTaskUpdates");
                //lblTaskUpdaters.Text = ds2.Tables[0].Rows[0][0].ToString();
                //lblNotUpdated.Font.Bold = true;
                //lblTaskUpdated.Font.Bold = true;
                //lblTaskUpdaters.Font.Bold = true;
                //lblTaskUpdated.Text = ds2.Tables[1].Rows[0][0].ToString();
                //lblNotUpdated.Text = ds2.Tables[6].Rows[0][0].ToString();
                // lblNewJoin.Text = ds2.Tables[4].Rows[0][0].ToString();
                //lblUpdatedToday.Text = ds2.Tables[5].Rows[0][0].ToString();

                int RoleId = Convert.ToInt32(Session["RoleId"]);
                if (Session["CompanyName"] != null && Session["CompanyName"].ToString() != "")
                {
                    //  try { lblCompnayName.Text = Session["CompanyName"].ToString(); }
                    // catch { }
                }
                DataSet dsStatus = GetStatus(1, RoleId, Convert.ToInt32(Session["UserId"]));
                DataSet dsPic = G_EMP_GteEmployeePic(Convert.ToInt32(Session["UserId"]));
                if (dsPic.Tables[0].Rows.Count > 0)
                {
                    if (dsPic.Tables[0].Rows[0]["Image"].ToString() != "" && dsPic.Tables[0].Rows[0]["Image"].ToString() != "")
                    {
                        EmpPhoto.Visible = true;
                        // WRITTEN BY GANA
                        string Sr = "../HMS/EmpImages/" + dsPic.Tables[0].Rows[0]["EmpId"] + "." + dsPic.Tables[0].Rows[0]["Image"];
                        // string Sr = HttpContext.Current.Request.ApplicationPath + "/HMS/EmpImages/" + dsPic.Tables[0].Rows[0]["EmpId"] + "." + dsPic.Tables[0].Rows[0]["Image"];
                        Sr = Sr.Trim();
                        EmpPhoto.Src = Sr;
                        EmpUserImage.Src = Sr;
                    }
                }
                else
                {
                    EmpPhoto.Visible = false;
                }
                // WRITTEN BY GANA
                try
                {

                    lblPrjName.Text = Session["PrjName"].ToString();

                }
                catch
                {
                    Session["PrjName"] = "";

                }
                try
                {

                    lblCostCenter.Text = Session["CostCenterName"].ToString();

                }
                catch
                {
                    Session["CostCenterName"] = "";

                }
                // WRITTEN BY GANA
                if (dsStatus != null && dsStatus.Tables.Count != 0 && dsStatus.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsStatus.Tables[1].Rows)
                    {
                        switch (dRow["Url"].ToString().ToLower().Trim())
                        {
                            case "viewofferletterlist.aspx":
                                Label1.Visible = true;
                                HypOffers.Visible = true;
                                lblbr.Visible = true;
                                //HypOffers.Text = "At Offer:" + ds.Tables[1].Rows[0]["Nos"];
                                //HypOffers.NavigateUrl = "~/ViewOfferLetterList.aspx";
                                HypOffers.Text = "At Offer:" + ds.Tables[4].Rows[0][0];
                                HypOffers.NavigateUrl = "~/ViewOfferLetterList.aspx";
                                break;
                            case "viewselectedapplicantlist.aspx":
                                Label1.Visible = true;
                                HypSelected.Visible = true;
                                lblbr.Visible = true;
                                //HypSelected.Text = "Selected:" + ds.Tables[1].Rows[1]["Nos"];
                                //HypSelected.NavigateUrl = "~/ViewSelectedApplicantList.aspx";
                                HypSelected.Text = "Selected:" + ds.Tables[3].Rows[0][0];
                                HypSelected.NavigateUrl = "~/ViewSelectedApplicantList.aspx";
                                break;
                            case "viewapplicantlist.aspx":
                                Label1.Visible = true;
                                // lnkProcess.Text = "Applicants:" + ds.Tables[1].Rows[2]["Nos"];
                                //lnkProcess.NavigateUrl = "~/ViewApplicantList.aspx";
                                lnkProcess.Visible = true;
                                lblbr.Visible = true;
                                lnkProcess.Text = "Applicants:" + ds.Tables[2].Rows[0][0];
                                lnkProcess.NavigateUrl = "~/ViewApplicantList.aspx";

                                break;


                            //TaskUpdated 
                            case "emptaskingsystem.aspx?key=1":

                                hlnkUpdated.Visible = true;
                                lblTaskUpdated.Font.Bold = true;
                                lblTaskUpdated.Text = ds2.Tables[1].Rows[0][0].ToString();
                                lblTaskUpdated.Visible = true;
                                break;

                            //NotYpdated 
                            case "emptaskingsystem.aspx?key=2":
                                hlnkNotUpdated.Visible = true;
                                lblNotUpdated.Font.Bold = true;
                                lblNotUpdated.Visible = true;
                                lblNotUpdated.Text = ds2.Tables[6].Rows[0][0].ToString();

                                break;
                            //NewJoin 
                            case "emptaskingsystem.aspx?key=3":
                                hlnkNewJoin.Visible = true;
                                lblNewJoin.Visible = true;
                                lblNewJoin.Text = ds2.Tables[4].Rows[0][0].ToString();
                                break;

                            case "emptaskingsystem.aspx?key=4":
                                hlnkUpadatedToday.Visible = true;
                                lblUpdatedToday.Visible = true;
                                lblUpdatedToday.Text = ds2.Tables[5].Rows[0][0].ToString();
                                break;





                        }
                    }

                }
                #endregion OldCode

                //try { lblRole.Text = Session["RoleName"].ToString(); }
                //catch { Response.Redirect("Home.aspx"); }
                //lblRole.ToolTip = Session["MonitorSiteName"].ToString();

            }
            catch (Exception ex)
            {

                //throw ex;
            }

        }


        protected void lnkChangeProject_Click(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["ReleaseMode"] == "TRUE")
            {
                //ucHelp.TutorialURL = Request.ApplicationPath + "/OMS/Tutorials.aspx?menu=" + dr["MenuId"].ToString();
                Response.Redirect(Request.ApplicationPath + "../OMS/DashBoard.aspx?mode=select");
            }
            else
            {
                Response.Redirect("~/OMS/DashBoard.aspx?mode=select");

            }




        }

        //protected void Timer1_Tick(object sender, EventArgs e)
        //{

        //    int EmpID = Convert.ToInt32(Session["UserId"]);
        //    using (DataSet ds = AttendanceDAC.HR_ToDoListReminder(EmpID))
        //    {
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                //DateTime RemindTime = Convert.ToDateTime(dr["RemindTime"]);
        //                string RemindTime = dr["RemindTime"].ToString();
        //                DateTime TimeNow = Convert.ToDateTime(DateTime.Now);
        //                int  hour = TimeNow.Hour; int minute = TimeNow.Minute;
        //                string timeofnow = hour.ToString() + ":" + minute.ToString();
        //                //if (TimeNow.TimeOfDay.Ticks > RemindTime.TimeOfDay.Ticks)
        //                //Page.RegisterClientScriptBlock(typeof(Page), "<script language=\"javascript\">alert('" + msg + "');</script>");
        //                //Page.ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>alert('Task Reminder')</script>");
        //                CODEUtility.MsgBox(Page, "Task Reminder");
        //                if (timeofnow == RemindTime)
        //                {
        //                        string Task = dr["Subject"].ToString();
        //                        //AlertMsg.MsgBox(Page, "Task Reminder of" + Task);
        //                        string msg = "Task Reminder of " + Task;
        //                        Page.ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>alert('" + msg + "')</script>");

        //                }
        //            }
        //    }
        //}


        protected void lnkLogout_Click1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Application["OnlineUsers"]) > 0)
            {

                Application["OnlineUsers"] = Convert.ToInt32(Application["OnlineUsers"]) - 1;
            }
            string HostIP = "";
            HostIP = Request.UserHostAddress;
            int EmpID = Convert.ToInt32(Session["UserId"]);
            //DataSet ds = new DataSet();

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Empid", EmpID);

            SQLDBUtil.ExecuteNonQuery("scp_Logutdetails", sqlParams);

            Session.Abandon();
            FormsAuthentication.SignOut();

            //System.Web.HttpCookie C = Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName];
            //C.Domain = ConfigurationManager.AppSettings["DomainName"];
            //C.Expires = DateTime.Now.AddDays(-1);
            //Response.Cookies.Add(C);

            //Response.Redirect("logout.aspx?clearssid=" + ConfigurationManager.AppSettings["clearssid"].ToString());
            //Response.Redirect("AdminHome.aspx");
            Response.Redirect("../Logon.aspx");
            FormsAuthentication.RedirectToLoginPage();
        }
    }

}