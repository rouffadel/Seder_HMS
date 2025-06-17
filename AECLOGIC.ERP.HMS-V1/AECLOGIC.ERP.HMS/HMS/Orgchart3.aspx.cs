using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace AECLOGIC.ERP.HMS.HMS
{
    public partial class Orgchart3 :  AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC objAtt = new AttendanceDAC();
        int status;
        
        SqlDataAdapter adp;
        int id;
        int mid = 0;
        bool viewall;
        string menuname;
        string menuid;
        DataSet EMPResultSet;
        DataSet EMPResultSet1;
       
        string imgPath = "~/EmpImages/";
        static int SearchCompanyID;
        static int Empdeptid = 0;
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));           
            try
            {
                string id =  Convert.ToInt32(Session["UserId"]).ToString();
                if (!IsPostBack)
                {

                    SearchCompanyID = Convert.ToInt32(Session["CompanyID"]);
                    ClientScript.RegisterStartupScript(typeof(Page), "str", "<script type='text/javascript'>initEdit();</script>");
                }

            }
            catch
            {
                Response.Redirect("Home.aspx");
            }

        }

        protected void btnSearchToP_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(typeof(System.String), "str", "<script type='text/javascript'>initEdit();</script>");
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
       
    }
}