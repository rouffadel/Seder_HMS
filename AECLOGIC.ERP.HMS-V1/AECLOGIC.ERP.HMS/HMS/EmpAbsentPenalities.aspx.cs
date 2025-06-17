using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AECLOGIC.HMS.BLL;
using System.Collections.Generic;
using System.Linq;


namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class EmpAbsentPenalities : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        static int Empdeptid = 0;
        static int SearchCompanyID;
        static int EmpSalID;
        static int BaseSalary;
        TableRow tblRow;
        TableCell tcName, tcDesig, tcDate, tcStatus, tcShift, tcEmpID, tcLnkbtn, tcScope, tcAbsent, tcOD, tcCL, tcEL, tcSL, tcLeavesApp,
            tcOBCL, tcOBEL, tcOBSL, tcCurCL, tcCurEL, tcCurSL, tcAdjLeaves, tcLOP;


        AttendanceDAC objAtt = new AttendanceDAC();
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);



        }
        protected void Page_Load(object sender, EventArgs e)
        {

            DataSet dstemp = new DataSet();
            if (!IsPostBack)
            {
                try
                {
                    SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                    //dstemp = BindWorkSite(dstemp);
                    //dstemp = BindDepartments(dstemp);
                    BindYears();
                }
                catch
                {

                }
            }


            GetMonthlyReport();
        }
        protected void GetMonthlyReport()
        {
            DataTable dtFinalDataFinal = new DataTable();
            dtFinalDataFinal.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dtFinalDataFinal.Columns.Add(new DataColumn("EmpName", typeof(System.String)));
            dtFinalDataFinal.Columns.Add(new DataColumn("TotalPnalities", typeof(System.String)));
                 dtFinalDataFinal.Columns.Add(new DataColumn("Basesalary", typeof(System.Int32)));
                 dtFinalDataFinal.Columns.Add(new DataColumn("PenalityAmount", typeof(System.Int32)));
            ViewState["AtandencePending"] = dtFinalDataFinal;
            dtFinalDataFinal = (DataTable)ViewState["AtandencePending"];
            DataSet ds = new DataSet();
            int empid = txtEmpID.Text == "" ?0 : Convert.ToInt32(txtEmpID.Text);
            
            DataRow drt;

            if (empid == 0)
            {
                ds = AttendanceDAC.T_HMS_Get_EmptListofAbsen(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlDept_hid.Value == "" ? "0" : ddlDept_hid.Value), Convert.ToInt32(ddlWS_hid.Value == "" ? "0" : ddlWS_hid.Value), "HR_GetEmpListAttendanceByMonth");
                DataTable dt1 = ds.Tables[0];
              int rows = ds.Tables[0].Select("EmpID=" + empid).Count();
              if (rows>0 ||empid==0)
               {
                   foreach (DataRow item in dt1.Rows)
                   {
                      
                       drt = GetPanalities(Convert.ToInt32(item["EmpID"]), item["EmpName"].ToString());
                       dtFinalDataFinal.Rows.Add(drt.ItemArray);
                   }
               }
              else
              {
                  
                  AlertMsg.MsgBox(Page, "This employess Did have absents Plz check the EmpID.!"); 
              }

                
            }
            else
            {
                drt = GetPanalities(empid,"");
                ds = AttendanceDAC.GetEmpSalList(empid);
                dtFinalDataFinal.Rows.Add(drt.ItemArray);
            }
                    

            gvHolidays.DataSource = dtFinalDataFinal;         
            gvHolidays.DataBind();
        }


        protected DataRow GetPanalities(int EmpID, string EmpName)
        {
            DataTable dtFinalData = new DataTable();         
            bool absentContinue = false;
            string str = "";
            int j = 0; 
            int k = 0;
            int m = 0;
            AttendanceDAC objAtt = new AttendanceDAC();
            int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));
            //int empid = txtEmpID.Text == "" ? 0 : Convert.ToInt32(txtEmpID.Text);
            DataSet dsEmpSalryIDget = AttendanceDAC.GetEmpSalList(EmpID);
            EmpSalID = Convert.ToInt32(dsEmpSalryIDget.Tables[0].Rows[dsEmpSalryIDget.Tables[0].Rows.Count - 1]["EmpSalID"]);
            DataSet dsBaseSalget = PayRollMgr.GetEmpWages(EmpID, EmpSalID);
            BaseSalary = Convert.ToInt32(dsBaseSalget.Tables[0].Rows[0]["Value"]);
            DataSet ds = objAtt.GetAttendanceByMonth_Cursor(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlWS_hid.Value == "" ? "0" : ddlWS_hid.Value), Convert.ToInt32(ddlWS_hid.Value == "" ? "0" : ddlWS_hid.Value), EmpID, null, null);
            DataSet ds1 = AttendanceDAC.T_HMS_AbsentPenalties_GetList(1, "GET_T_HMS_AbsentPenalties_RT");
            DataSet dsconfig = AttendanceDAC.T_HMS_AbsentPenalties_GetList(1, "GET_T_HMS_AbsentPenaltiesConfig_ByID");
            string strTemp = "";
            string str1 = "";            
            int nArraySize = 0;
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds1.Tables[0];
            DataTable dt3 = dsconfig.Tables[0];
            var dictFinal = new Dictionary<int, int>();
            int nCount = 0;
            foreach (DataRow ad in dt3.Rows)
            {
                dictFinal[Convert.ToInt32(ad["noOfdays"])] = 0;
                nCount = Convert.ToInt32(ad["noOfdays"]);
            }

            foreach (DataRow row in dt1.Rows) // Loop over the rows.
            {
                str1 = row["Status"].ToString();
                if ((str1 == "A" && str1 != strTemp) || (str1 == "A" && strTemp == ""))
                {
                    nArraySize = nArraySize + 1;
                    EmpName = EmpName != "" ? EmpName : row["name"].ToString();
                }
                strTemp = str1;
            }


            #region gWT ABSENTIESlILST CONTIOUS COUNT
            int[,] aData = new int[2, nArraySize];
            //int val = aData[2, 3];
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows) // Loop over the rows.
                {
                    str = row["Status"].ToString();
                    if (str != "A")
                    {
                        if (absentContinue == true)
                        {
                            k = k + 1; //increase occurance
                        }
                        absentContinue = false;
                        j = 0;

                    }
                    else if (str == "A") // here str=="A"
                    {
                        if (absentContinue == false)
                        {
                            m = m + 1;
                        }
                        absentContinue = true;
                        j = j + 1;

                        aData[0, m - 1] = k + 1; //occurance
                        aData[1, m - 1] = j;   //length
                    }
                }
            }
            #endregion

            #region HOW MANY DAYs OCCURED BASED ON maSTER CONFIG 
            j = 0; k = 0; m = 0;
            //int[,] aData2 = new int[1, nArraySize];          
            //var dict = new Dictionary<int, int>();

            for (var t = 0; t <= nArraySize - 1; t++)
            {
                foreach (DataRow item1 in dt3.Rows)
                {
                    if (aData[1, t] <= Convert.ToInt32(item1["noOfdays"]))
                    {

                        if (dictFinal.ContainsKey(Convert.ToInt32(item1["noOfdays"])))
                        {
                            dictFinal[Convert.ToInt32(item1["noOfdays"])]++;
                            break;
                        }
                        else
                        {
                            dictFinal[Convert.ToInt32(item1["noOfdays"])]++;
                        }

                    }
                    else if (nCount + 1 <= aData[1, t])
                    {
                        if (!dictFinal.ContainsKey(aData[1, t]))
                        {
                            dictFinal[aData[1, t]] = 1;
                            break;
                        }
                        else
                        {
                            dictFinal[aData[1, t]]++;
                        }
                    }
                }
            }
            #endregion

            #region ASSIGN  PENALITIES BASED ON MASTER CONFIG
            int mcday;
            int mcOCC;
            var dict2 = new Dictionary<int, int>();
            foreach (var item in dictFinal)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    mcday = Convert.ToInt32(dr["noOfdays"]);
                    mcOCC = Convert.ToInt32(dr["occurance"]);

                    if (item.Key <= mcday && item.Value == mcOCC)
                    {
                        if (!dict2.ContainsKey(item.Key))
                        {
                            dict2.Add(item.Key, Convert.ToInt32(dr["penality"]));
                        }
                        break;
                    }
                    else if (item.Key <= mcday && item.Value > mcOCC &&  Convert.ToInt32(dr["MAXoccurance"]) < item.Value)   
                    {
                        if (!dict2.ContainsKey(item.Key))
                        {
                            dict2.Add(item.Key,-1);
                        }                             
                    }
                    else if (nCount + 1 <= item.Key)
                    {
                        if (!dict2.ContainsKey(item.Key))
                        {
                            dict2[item.Key] = 0;
                            break;
                        }
                        else
                        {
                            dict2[item.Key] = 0;
                        }
                    }
                }
            }
            #endregion
            int TotalPnalities = 0;
            foreach (var dictItem in dict2)
            {
                if (dictItem.Value!=-1)
                {
                TotalPnalities = TotalPnalities + dictItem.Value;
               }
                else if (dictItem.Value == -1)
                {
                    TotalPnalities = 0;
                }
            }
            dtFinalData.Columns.Add(new DataColumn("EmpID", typeof(System.Int32)));
            dtFinalData.Columns.Add(new DataColumn("EmpName", typeof(System.String)));
            dtFinalData.Columns.Add(new DataColumn("TotalPnalities", typeof(System.String)));
            dtFinalData.Columns.Add(new DataColumn("Basesalary", typeof(System.Int32)));
            dtFinalData.Columns.Add(new DataColumn("PenalityAmount", typeof(System.Int32)));
            ViewState["AtandencePending"] = dtFinalData;
            dtFinalData = (DataTable)ViewState["AtandencePending"];
            DataRow drt = dtFinalData.NewRow();
            drt["EmpID"] = EmpID;
            drt["EmpName"] = EmpName;
            drt["TotalPnalities"] = TotalPnalities == 0 ? "0" :TotalPnalities.ToString();
            drt["Basesalary"] = BaseSalary;
            drt["PenalityAmount"] = TotalPnalities == 0 ? PenalityAmount(BaseSalary, 5, days) : PenalityAmount(BaseSalary, 5, TotalPnalities);
            dtFinalData.Rows.Add(drt);
            ViewState["AtandencePending"] = dtFinalData;
           
            return drt;
        }

        protected void GvHolidays_OnRowDataBound(object sender,GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlAction");
                string amt = e.Row.Cells[2].Text;
                if (amt == "Cross the Limit")
                {

                    //enable ddl 
                    
                    ddl.Enabled = true;
                    ddl.SelectedIndex = 0;
                }
                else
                {
                    //diable
                    ddl.Enabled = false;
                    ddl.SelectedIndex = 1;
                }
                   

            }
      
        }


        public int PenalityAmount(int BaseSalary, int TotalPnalities, int days)
        {
            int PenalityAmount;
            PenalityAmount = (TotalPnalities * (BaseSalary / 30));
            return PenalityAmount;
        }

        protected void GetWork(object sender, EventArgs e)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Search", txtSearchWorksite.Text);
            param[1] = new SqlParameter("@CompanyID", Convert.ToInt32(Session["CompanyID"]));
            // FIllObject.FillDropDown(ref ddlWorksite, "G_GET_WorkSitebyFilter", param);
            FIllObject.FillDropDown(ref ddlWS, "G_GET_WorkSitebyFilter", param);
            ListItem itmSelected = ddlWS.Items.FindByText(txtSearchWorksite.Text);
            if (itmSelected != null)
            {
                ddlWS.SelectedItem.Selected = false;
                itmSelected.Selected = true;
            }
        }
       
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetGoogleABCSearchWorkSite(prefixText, SearchCompanyID);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }                
          
           return items.ToArray(); // txtItems.ToArray();
        }
       
        public void BindYears()
        {
            DataSet ds = AttendanceDAC.GetCalenderYear();

            int i = 0;
            int Maxyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxYear"].ToString());
            for (int Minyear = Convert.ToInt32(ds.Tables[0].Rows[0]["MinYear"].ToString()); Minyear <= Maxyear; Minyear++)
            {
                ddlYear.Items.Insert(0, new ListItem(Convert.ToString(Minyear), Convert.ToString(Minyear)));
                i = i + 1;
            }
            ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["CurrentMonth"].ToString();
            ddlYear.SelectedValue = ds.Tables[0].Rows[0]["CurrentYear"].ToString();

        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMonthlyReport();
        }

        protected void ddlWorksite_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMonthlyReport();   
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            GetMonthlyReport();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            GetMonthlyReport();
        }
        protected void GetDept(object sender, EventArgs e)
        {
           
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]

        public static string[] GetCompletionListDep(string prefixText, int count, string contextKey)
        {
            DataSet ds = AttendanceDAC.GetSearchDesiginationFilter(prefixText, SearchCompanyID, Empdeptid);
            DataTable dt = ds.Tables[0];
            List<string> items = new List<string>(count);
            var rtval = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                string str = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row["Name"].ToString(), row["ID"].ToString());
                items.Add(str);
            }

            return items.ToArray();
        }
       
       
        public string GetCompliance(string ID)
        {
            string strReturn;

            strReturn = String.Format("window.showModalDialog('ViewAttendance.aspx?ID={0}&ddlYear={0}&ddlMonth={0}' ,'');", ID, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));

            return strReturn;
        }
        protected void btnMonthReport_Click(object sender, EventArgs e)
        {
           
            GetMonthlyReport();
        }

        protected void gvHolidays_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHolidays.PageIndex = e.NewPageIndex;
            GetMonthlyReport();
        }

        protected void gvHolidays_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int EmpID = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                DropDownList ddl = (DropDownList)row.FindControl("ddlAction");
                if (ddl.SelectedIndex == 0)
                {
                    Response.Redirect("EmpTermination.aspx?EmpID=" + EmpID);
                }
                else
                {
                    AlertMsg.MsgBox(Page, "Penality for Emp");
                }
                   

               
               
            }

        }

    }
}
