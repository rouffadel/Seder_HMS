using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AECLOGIC.HMS.BLL;
using System.Web.SessionState;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{

    public partial class MyRentalDocs : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon objHrCommon = new HRCommon();
        int mid = 0; bool viewall; string menuname; string menuid;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        
            EmpRentalDocsPaging.FirstClick += new Paging.PageFirst(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.PreviousClick += new Paging.PagePrevious(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.NextClick += new Paging.PageNext(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.LastClick += new Paging.PageLast(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.ChangeClick += new Paging.PageChange(EmpRentalDocsPaging_FirstClick);
            EmpRentalDocsPaging.ShowRowsClick += new Paging.ShowRowsChange(EmpRentalDocsPaging_ShowRowsClick);
            EmpRentalDocsPaging.CurrentPage = 1;
        }
        void EmpRentalDocsPaging_ShowRowsClick(object sender, EventArgs e)
        {
            EmpRentalDocsPaging.CurrentPage = 1;
            BindPager();
        }
        void EmpRentalDocsPaging_FirstClick(object sender, EventArgs e)
        {
            BindPager();
        }
        void BindPager()
        {
            objHrCommon.PageSize = EmpRentalDocsPaging.CurrentPage;
            objHrCommon.CurrentPage = EmpRentalDocsPaging.ShowRows;
            BindGrid(objHrCommon);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                GetParentMenuId();
                Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));
                ViewState["EmpID"] = 0; ViewState["HRDocID"] = 0;
                if (Request.QueryString.Count > 0)
                {
                   //--Add
                    DataSet ds1 = objAtt.GetEmployeeDetails( Convert.ToInt32(Session["UserId"])); 
                    lblemp.Text = "(" +  Convert.ToInt32(Session["UserId"]).ToString() + ") " + ds1.Tables[0].Rows[0]["FName"].ToString() + " " + ds1.Tables[0].Rows[0]["MName"].ToString()+" "+ ds1.Tables[0].Rows[0]["LName"].ToString();
                    tblView.Visible = false;
                    tblEdit.Visible = true;
                    pnltblEdit.Visible = true;
                }
                else
                {

                    tblView.Visible = true;
                    tblEdit.Visible = false;
                    pnltblEdit.Visible = false;
                }
                BindPager();
                ViewState["Proof"] = "";
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

                gvView.Columns[9].Visible = Convert.ToBoolean(ds.Tables[0].Rows[0]["Editable"].ToString());
               
                btnSave.Enabled = (bool)ds.Tables[0].Rows[0]["Editable"];
                
            }
            return MenuId;
        }

        void BindGrid(HRCommon objHrCommon)
        {

            try
            {

                objHrCommon.PageSize = EmpRentalDocsPaging.ShowRows;
                objHrCommon.CurrentPage = EmpRentalDocsPaging.CurrentPage;

                int Emp =  Convert.ToInt32(Session["UserId"]);
                int WS = 0;
                int Dept = 0;
                string EmpName = "";
                  
               DataSet ds = AttendanceDAC.HR_GetRentalDocsByPaging(objHrCommon, WS, Dept, Emp, EmpName);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvView.DataSource = ds;
                    gvView.DataBind();
                    EmpRentalDocsPaging.Visible = true;
                    EmpRentalDocsPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);
                }
                else
                {
                    EmpRentalDocsPaging.Visible = false;
                    gvView.EmptyDataText = "No Records Found";
                    gvView.DataSource = null;
                    gvView.DataBind();
                }




            }
            catch (Exception e)
            {
                throw e;
            }
        }


        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            ViewState["HRDocID"] = ID;
            tblView.Visible = false;
            tblEdit.Visible = true;
            pnltblEdit.Visible = true;
            if (e.CommandName == "Edt")
            {
                DataSet ds = AttendanceDAC.HR_GetRentalDocsByID(ID);

                txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                txtFrom.Text = ds.Tables[0].Rows[0]["FromDate1"].ToString();
                txtUpto.Text = ds.Tables[0].Rows[0]["ToDate1"].ToString();
                ViewState["Proof"] = ds.Tables[0].Rows[0]["Proof"].ToString();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtAmount.Text = txtFrom.Text = txtUpto.Text = "";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                int EmpID = Convert.ToInt32( Convert.ToInt32(Session["UserId"]).ToString());
                int HRDocID = Convert.ToInt32(ViewState["HRDocID"]);


                string filename = "", ext = "", path = "";
                filename = FileProof.PostedFile.FileName;
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
                try
                {
                    DateTime From = CODEUtility.ConvertToDate(txtFrom.Text, DateFormat.DayMonthYear);

                    DateTime? Upto = null;
                    try
                    {

                        if (txtUpto.Text != "")
                        {
                            Upto = CODEUtility.ConvertToDate(txtUpto.Text, DateFormat.DayMonthYear);
                        }
                        double Amount = 0.0;
                        if (txtAmount.Text != "")
                            Amount = Convert.ToDouble(txtAmount.Text);
                      int k=  AttendanceDAC.HR_InsUpRentalDocs(HRDocID, EmpID, Amount, ext, From, Upto,  Convert.ToInt32(Session["UserId"]));

                      if (k == 1)
                          AlertMsg.MsgBox(Page, "Inseted successfully!");
                      else if (k == 3)
                          AlertMsg.MsgBox(Page, "Updated successfully!");
                      else if (k == 2)
                      {
                          AlertMsg.MsgBox(Page, "Already exited!");
                          return;
                      }
                      else
                      {
                          AlertMsg.MsgBox(Page, "From date should not be greater than To date!");
                          return;
                      }

                    }
                    catch (Exception)
                    {
                        AlertMsg.MsgBox(Page, "Please select proper TO date.!");
                        return;

                    }
                }
                catch (Exception)
                {
                    AlertMsg.MsgBox(Page, "Please select proper from date.!");
                    return;
                }
                if (filename != "")
                {
                    if (EmpID != 0)
                    {
                        path = Server.MapPath(".\\RentalDocs\\" + EmpID + "." + ext);
                        try
                        {
                            FileProof.PostedFile.SaveAs(path);
                        }
                        catch { throw new Exception("FileProof.PostedFile.SaveAs(path)"); }
                    }
                }
                tblView.Visible = true;
                tblEdit.Visible = false;
                pnltblEdit.Visible = false;
               
                BindPager();
            }
            catch (Exception EmpRental)
            {
                AlertMsg.MsgBox(Page, EmpRental.Message.ToString(),AlertMsg.MessageType.Error);
            }

        }
        public string ProofView(string EmpID, string ext)
        {
            if (ext.Trim ()=="")
                return "javascript:return alert('No proof has been uploaded!')";
            else
            return "javascript:return window.open('./RentalDocs/" + EmpID + "." + ext + "', '_blank')";
        }
        public bool Visible(string ext)
        {

            if (ext != "")
            {
                return true;
            }
            else
                return false;
        }
    }
}