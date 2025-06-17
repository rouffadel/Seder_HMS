using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AECLOGIC.ERP.HMS
{
    public partial class ViewEmpPenalty : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            DataSet dsAU = AttendanceDAC.GetAu();
            DataRow dr = dsAU.Tables[0].NewRow();
            dr["Au_Id"] = 0;
            dr["Au_Name"] = "---Select---";
            dsAU.Tables[0].Rows.InsertAt(dr, 0);
            dsAU.AcceptChanges();
            ArrayList alUnitIndexes = new ArrayList();
            foreach (DataRow row in dsAU.Tables[0].Rows)
            {
                alUnitIndexes.Add(row["Au_Id"].ToString().Trim());
            }
            ViewState["alUnitIndexes"] = alUnitIndexes;
            ViewState["dsAU"] = dsAU;
            
            if (Request.QueryString.Count > 0)
            {
                int ERID = Convert.ToInt32(Request.QueryString["key"].ToString());
               
                DataSet ds = AttendanceDAC.HR_EmpPenality_ViewAmtApproved(ERID);
                gvShow.DataSource = ds;
                gvShow.DataBind();
                gvShow.Visible = true;

            }

        }
        public DataSet GetAUDataSet()
        {
            return (DataSet)ViewState["dsAU"];
        }
        public string DocNavigateUrl(string Proof)
        {
            string ReturnVal = "";
            string Value = Proof.Split('.')[Proof.Split('.').Length - 1];
            // MODIFY THE BY PRATAP DATE:13-04-2016
            ReturnVal = "/EmpPenaltyProof/" + Proof;
            if (ReturnVal == "/EmpPenaltyProof/")
            {
                return null;
            }
            else
            {
                return ReturnVal;
            }
          
        }

        public int GetAUIndex(string AUID)
        {
            ArrayList alUnitIndexes = (ArrayList)ViewState["alUnitIndexes"];
            return alUnitIndexes.IndexOf(AUID.Trim());
        }
    }
}