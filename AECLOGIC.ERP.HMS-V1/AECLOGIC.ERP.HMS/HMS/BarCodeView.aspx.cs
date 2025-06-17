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
using AECLOGIC.ERP.HMS;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;

namespace AECLOGIC.ERP.HMSV1
{
    public partial class BarCodeViewV1 : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        HRCommon objcommon = new HRCommon();

        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBarCode.Text.Trim() != "")
                {
                    string strMod = string.Empty;
                    string strPrCode = string.Empty;
                    string ID = string.Empty;
                    string url = string.Empty;
                    int UserId = Convert.ToInt32(Session["UserId"]);
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string strBarcode = txtBarCode.Text.Trim();
                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("@BarCode", strBarcode);
                    p[1] = new SqlParameter("@UserID", UserId);
                    p[2] = new SqlParameter("@RoleId", RoleId);

                    DataSet ds = SqlHelper.ExecuteDataset("HR_BarCodeView", p);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        strMod = ds.Tables[0].Rows[0][0].ToString();
                        strPrCode = ds.Tables[0].Rows[0][1].ToString();
                        ID = ds.Tables[0].Rows[0][2].ToString();
                        if (ds.Tables[0].Rows[0][3].ToString() == "1")
                        {
                            if (strMod == "HMS")
                            {
                                if (strPrCode == "LV")
                                {
                                    url = "ViewEmpLeaveDetails.aspx?LID=" + ID;
                                }
                                else if (strPrCode == "CA")
                                {
                                    url = "ViewEmpLoanDetails.aspx?LoanId=" + ID;
                                }
                                else if (strPrCode == "FE")
                                {
                                    url = "ViewEmpExitDetails.aspx?FEID=" + ID;
                                }
                                else if (strPrCode == "BT")
                                {
                                    url = "ViewBusinessTrip.aspx?BID=" + ID;
                                }
                            }
                            string fullURL = "window.open('" + url + "', '_blank' );";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage1", "alert('UnAuthorised Access..! ')", true);
                        }
                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid BarCode.')", true);
                    }
                    //Page.ClientScript.RegisterStartupScript(
                    // this.GetType(), "OpenWindow", "window.open('YourURL','_newtab');", true);
                   // string url = "ViewBusinessTrip.aspx?BID=" + ID;
                    

                }
            }
            catch (Exception ex)
            {

                AlertMsg.MsgBox(Page, ex.Message.ToString(), AlertMsg.MessageType.Error);
            }
        }
        }
    }
