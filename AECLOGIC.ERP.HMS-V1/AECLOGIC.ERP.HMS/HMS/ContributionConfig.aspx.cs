using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AECLOGIC.HMS.BLL;
namespace AECLOGIC.ERP.HMS
{
    public partial class ContributionConfig : AECLOGIC.ERP.COMMON.WebFormMaster
    {
        AttendanceDAC obj = new AttendanceDAC();
        AjaxDAL Aj = new AjaxDAL();

        int mid = 0;
       
        protected override void OnInit(EventArgs e)
        {
            ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                ContributionsBind();
            }
        }

        public void ContributionsBind()
        {
            ListItem listItem = null;
            //cblContributions
            using (DataSet ds = PayRollMgr.GetEmpCoyContributionItemsList(0, 0))
            {
                cblContributions.Items.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    listItem = new ListItem(dr["Name"].ToString(), dr["ItemId"].ToString());
                    rblstContribution.Items.Add(listItem);
                }
            }

        }
    
        void Bind()
        {
            ListItem listItem = null;
            //cblWages
            using (DataSet ds = PayRollMgr.GetEmpWages(0, 0))
            {
                cblWages.Items.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    listItem = new ListItem(dr["Name"].ToString(), dr["WagesId"].ToString());
                    cblWages.Items.Add(listItem);

                }
            }
            //cblAllowences
            using (DataSet ds = PayRollMgr.GetEmpAllowancesList(0, 0))
            {
                cblAllowences.Items.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    listItem = new ListItem(dr["Name"].ToString(), dr["AllowId"].ToString());
                    cblAllowences.Items.Add(listItem);
                }
            }
            //cblContributions
            using (DataSet ds = PayRollMgr.GetEmpCoyContributionItemsList(0, 0))
            {
                cblContributions.Items.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    listItem = new ListItem(dr["Name"].ToString(), dr["ItemId"].ToString());
                    if (dr["ItemId"].ToString() != rblstContribution.SelectedItem.Value)
                    {
                        cblContributions.Items.Add(listItem);
                    }
                }
            }
            //cblDeductions
            using (DataSet ds = PayRollMgr.GetEmpDeductStatutoryList(0, 0))
            {
                cblDeductions.Items.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    listItem = new ListItem(dr["Name"].ToString(), dr["ItemId"].ToString());
                    cblDeductions.Items.Add(listItem);
                }
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Save();
        }
        protected void rblstContribution_SelectedIndexChanged(object sender, EventArgs e)
        {
            tdPayrole.Visible = true;
            Bind();
        }

        public void Save()
        {
            string ContributionID = rblstContribution.SelectedItem.Value;

            //wages
            foreach (ListItem lstItm in cblWages.Items)
            {
                string wageid = lstItm.Value;
                bool Access = false;
                if (lstItm.Selected == true)
                {
                    Access = true;
                }
                Aj.ConfigWagesContributiion(ContributionID, wageid, Access);
            }
            //Allowances
            foreach (ListItem lstItm in cblAllowences.Items)
            {
                string allowid = lstItm.Value;
                bool Access = false;
                if (lstItm.Selected == true)
                {
                    Access = true;
                }
                Aj.ConfigAllowancesContributiion(ContributionID, allowid, Access);
            }
            //EmpContribution
            foreach (ListItem lstItm in cblContributions.Items)
            {
                string Contributionid = lstItm.Value;
                bool Access = false;
                if (lstItm.Selected == true)
                {
                    Access = true;
                }

                Aj.ConfigContribution(ContributionID, Contributionid, Access);
            }
            //EmpDeductions
            foreach (ListItem lstItm in cblDeductions.Items)
            {
                string Deductionid = lstItm.Value;
                bool Access = false;
                if (lstItm.Selected == true)
                {
                    Access = true;
                }
                Aj.ConfigDeductionsContributiion(ContributionID, Deductionid, Access);
            }
        }
    }
}