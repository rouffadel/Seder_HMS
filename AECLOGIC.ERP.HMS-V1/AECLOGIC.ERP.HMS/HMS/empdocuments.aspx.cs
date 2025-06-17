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
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class empdocuments : AECLOGIC.ERP.COMMON.WebFormMaster
    {

        AttendanceDAC objApp = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();

        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static string strurl = string.Empty;


        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
            // btnSave.Attributes.Add("onclick", "javascript:return ValidateSave('" + txtCategoryName.ClientID + "');");
            EmployeeChangesPaging.FirstClick += new Paging.PageFirst(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.PreviousClick += new Paging.PagePrevious(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.NextClick += new Paging.PageNext(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.LastClick += new Paging.PageLast(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.ChangeClick += new Paging.PageChange(EmployeeChangesPaging_FirstClick);
            EmployeeChangesPaging.ShowRowsClick += new Paging.ShowRowsChange(EmployeeChangesPaging_ShowRowsClick);
            EmployeeChangesPaging.CurrentPage = 1;
        }
        void EmployeeChangesPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmployeeChangesPaging.CurrentPage = 1;
            BindPager();
        }
        void EmployeeChangesPaging_FirstClick(object sender, EventArgs e)
        {
            EmployeeChangesPaging.CurrentPage = 1;
            BindPager();
        }
        void BindPager()
        {

            objHrCommon.PageSize = EmployeeChangesPaging.CurrentPage;
            objHrCommon.CurrentPage = EmployeeChangesPaging.ShowRows;
            BindEmployeeDocDetails(objHrCommon);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                RblChage();



                if (!IsPostBack)
                {
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    GetParentMenuId();
                    strurl = Request.UrlReferrer.ToString();
                    if (Request.QueryString.Count > 0)
                    {
                        int EmpId = Convert.ToInt32(Request.QueryString["eid"]);
                        DataSet ds = AttendanceDAC.GetEmpDetailsForDocsDetails(EmpId);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblName.Text = ds.Tables[0].Rows[0]["name"].ToString();
                            lblDept.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                            lbldesig.Text = ds.Tables[0].Rows[0]["Design"].ToString();
                            lblDOJ.Text = ds.Tables[0].Rows[0]["Dateofjoin"].ToString();
                            lblTrade.Text = ds.Tables[0].Rows[0]["Category"].ToString();
                            lblcntryname1.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();
                            lblpan1.Text = ds.Tables[0].Rows[0]["PANNumber"].ToString();
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            lblIqama.Text = ds.Tables[1].Rows[0]["Numeber"].ToString();
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            lblIqmaExpDate.Text = ds.Tables[1].Rows[0]["IqmaExpDate"].ToString();
                        }
                        BindDocuments();
                        BindPager();

                    }
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.HMSEventLog(ex, "EmpDocuments", "Page Load", "001");
            }

        }
        public void BindEmployeeDocDetails(HRCommon objHrCommon)
        {
            objHrCommon.PageSize = EmployeeChangesPaging.ShowRows;
            objHrCommon.CurrentPage = EmployeeChangesPaging.CurrentPage;

            int EmpId = Convert.ToInt32(Request.QueryString["eid"]);
            string DocName = txtSDocName.Text;
            DataSet ds = objApp.GetEmpDocsByPaging(objHrCommon, EmpId, DocName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvDocs.DataSource = ds.Tables[0];
            }
            else
            {
                gvDocs.EmptyDataText = "No Records Found";
            }
            gvDocs.DataBind();
            EmployeeChangesPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);



        }
        void BindDocuments()
        {
            DataSet ds = objApp.GetMasterDocs(0, Convert.ToInt32(Application["ModuleId"].ToString()));
            ddlDocuments.DataSource = ds.Tables[0];
            ddlDocuments.DataTextField = "DocName";
            ddlDocuments.DataValueField = "DocID";
            ddlDocuments.DataBind();

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
                gvDocs.Columns[2].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
                btnSubmint.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
            }
            return MenuId;
        }
        public void RblChage()
        {
            if (rblstStatus.SelectedValue == "1")
            {
                tblDoc.Visible = false;
                btnNext.Visible = true;
                trDocType.Visible = true;
                //trDocName.Visible = true;
                lblNewDoc.Visible = true;

            }
            else
            {
                tblDoc.Visible = true;
                btnNext.Visible = false;
                trDocType.Visible = false;
                //trDocName.Visible = false;
                lblNewDoc.Visible = false;

            }


        }
        protected void btnNext_Click(object sender, EventArgs e)
        {

            try
            {
                if (ddlDocuments.Items.Count > 0)
                {
                    int EmpId = Convert.ToInt32(Request.QueryString["eid"]);
                    int DocID = Convert.ToInt32(ddlDocuments.SelectedItem.Value);


                    string DocName = ddlDocuments.SelectedItem.Text;
                    if (rblstStatus.SelectedValue == "1")
                    {
                        switch (ddlDocuments.SelectedItem.Value)
                        {
                            case "1":
                                Response.Redirect("Appointment.aspx?id=" + EmpId + "&DocID=" + DocID + "&DocName=" + DocName);
                                break;

                            default:
                                Response.Redirect("CreateGenEmpdocuments.aspx?EmpId=" + EmpId + "&DocID=" + DocID + "&DocName=" + DocName);
                                break;
                        }
                    }
                    else
                    {


                    }
                }

            }
            catch (Exception ex)
            {
                AlertMsg.MsgBox(Page, "Sorry for the inconvinience. Try again.\nError:" + ex.Message.ToString(), AlertMsg.MessageType.Error);
            }

        }
        protected void btnSubmint_Click(object sender, EventArgs e)
        {
            HttpFileCollection uploadedFiles = Request.Files;
            for (int i = 0; i < uploadedFiles.Count; i++)
            {
                HttpPostedFile userPostedFile = uploadedFiles[i];
                try
                {
                    if (userPostedFile.ContentLength > 0)
                    {

                        int EmpId = Convert.ToInt32(Request.QueryString["eid"]);
                        int EmpDocID1 = 0;
                        String fname = "";
                        if (userPostedFile.FileName != null && userPostedFile.ContentLength > 0)
                        {
                            fname = userPostedFile.FileName.Substring(userPostedFile.FileName.LastIndexOf('.'), userPostedFile.FileName.Length - userPostedFile.FileName.LastIndexOf('.'));
                        }
                        TextBox txtBox = new TextBox();
                        if (i == 0) { txtBox.Text = txtDocName.Text; }
                        if (i == 1) { txtBox.Text = txtDocName2.Text; }
                        if (i == 2) { txtBox.Text = txtDocName3.Text; }
                        if (i == 3) { txtBox.Text = txtDocName4.Text; }
                        if (i == 4) { txtBox.Text = txtDocName5.Text; }
                        int EmpDocID = AttendanceDAC.AddUpdateEmpDocsGeneral(EmpId, 0, 0, fname, DateTime.Now, txtBox.Text.Trim(), EmpDocID1);
                        String SaveLocation = Server.MapPath(".\\ScanedDocuments\\" + EmpDocID + fname);
                        userPostedFile.SaveAs(SaveLocation);

                        DataSet ds = objApp.GetEmpDocs(EmpId);
                        gvDocs.DataSource = ds.Tables[0];
                        gvDocs.DataBind();
                    }
                }
                catch (Exception Ex)
                {
                }
            }
        }
        public bool IsEditable(string DocType)
        {

            if (DocType.Trim() == "Digital")
                return true;
            else
                return false;

        }
        public string DocNavigateUrl(string DocType, string EmpId, string DocID, string EmpDocID, string Value)
        {


            string ReturnVal = "";
            if (DocType.Trim() == "Digital")
            {

                ReturnVal = String.Format("AppointmentPreview.aspx?id={0}&DocID={1}&EmpDocID={2}", EmpId, DocID, EmpDocID);

            }
            else
                ReturnVal = "./scaneddocuments/" + EmpDocID + Value;
            return ReturnVal;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect(strurl);
        }
        protected void rblstStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RblChage();
        }

        protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int EmpDocID = Convert.ToInt32(e.CommandArgument);
                AttendanceDAC.UpdateEmpDocIDStatus(EmpDocID);
                BindPager();

            }
        }
        protected void lnkDoc_Click(object sender, EventArgs e)
        {
            Response.Redirect("Documents.aspx");

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPager();

        }

    }
}