using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using Aeclogic.Common.DAL;
using System.Web.UI.WebControls;
using AECLOGIC.HMS.BLL;
using AECLOGIC.ERP.COMMON;

namespace AECLOGIC.ERP.HMS
{
    public partial class NewState :WebFormMaster
    {
        AttendanceDAC objAtt;
        protected override void OnInit(EventArgs e)
        {
           // ModuleID = 1;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Text = String.Empty;
            if (Session["RoleName"].ToString() == "Guest")
            {
                btnSave.Enabled = false;
            }

            if (!IsPostBack)
            {

                FIllObject.FillDropDown(ref ddlCountry, "PM_Country");
                btnSave.Attributes.Add("onclick", "javascript:reaturn validate();");
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int CountryId = 0;
                CountryId = Convert.ToInt32(ddlCountry.SelectedItem.Value);
                string State = txtNewState.Text;
                int OutPut = AttendanceDAC.InsNewState(State, CountryId);
                if (OutPut == 0)
                {
                   // AlertMsg.MsgBox(Page, "Inserted Sucessfully");

                    lblStatus.Text = "Inserted Sucessfully !";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                  //  AlertMsg.MsgBox(Page, "Already Exists");

                    lblStatus.Text = "Already Exists !";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch  
            {
               // AlertMsg.MsgBox(Page, "Already Exists");

                lblStatus.Text = "Already Exists ";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}