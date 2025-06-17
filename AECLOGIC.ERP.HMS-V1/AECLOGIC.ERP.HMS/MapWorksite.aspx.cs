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
using HumanResource;


public partial class MapWorksite : WebFormMaster
{
    int mid = 0;
    bool viewall,Editable;
    string menuname;
    string menuid;
    AttendanceDAC objRights = new AttendanceDAC();
    HRCommon objHrCommon = new HRCommon();
    protected override void OnInit(EventArgs e)
    {
        MAPaging.FirstClick += new Paging.PageFirst(MAPaging_FirstClick);
        MAPaging.PreviousClick += new Paging.PagePrevious(MAPaging_FirstClick);
        MAPaging.NextClick += new Paging.PageNext(MAPaging_FirstClick);
        MAPaging.LastClick += new Paging.PageLast(MAPaging_FirstClick);
        MAPaging.ChangeClick += new Paging.PageChange(MAPaging_FirstClick);
        MAPaging.ShowRowsClick += new Paging.ShowRowsChange(MAPaging_ShowRowsClick);
        MAPaging.CurrentPage = 1;
    }
    void MAPaging_ShowRowsClick(object sender, EventArgs e)
    {
        MAPaging.CurrentPage = 1;
        BindPager();
    }
    void MAPaging_FirstClick(object sender, EventArgs e)
    {
        if (hdnSearchChange.Value == "1")
            MAPaging.CurrentPage = 1;
        BindPager();
        hdnSearchChange.Value = "0";
    }
    void BindPager()
    {

        objHrCommon.PageSize = MAPaging.CurrentPage;
        objHrCommon.CurrentPage = MAPaging.ShowRows;
        BindGrid(objHrCommon);

    }
    public int GetParentMenuId()
    {
        string URL = Request.Url.Segments[Request.Url.Segments.Length - 1] + Request.Url.Query;
        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
        int ModuleId = Convert.ToInt32(Application["ModuleId"].ToString());

        DataSet ds = new DataSet();

        ds = AttendanceDAC.GetAllowed(RoleId, ModuleId, URL);
        int MenuId = 0;
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            MenuId = Convert.ToInt32(ds.Tables[0].Rows[0]["Under"]);
            ViewState["Editable"] =Editable= (bool)ds.Tables[0].Rows[0]["Editable"];
            ViewState["ViewAll"] = (bool)ds.Tables[0].Rows[0]["ViewAll"];
            viewall = (bool)ViewState["ViewAll"];
            menuname = ds.Tables[0].Rows[0]["menuname"].ToString();
            menuid = MenuId.ToString();
            mid = Convert.ToInt32(ds.Tables[0].Rows[0]["MenuId"].ToString());
            BtnSave.Enabled = Editable;
        }
        return MenuId;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(AjaxDAL));

        topmenu.MenuId = GetParentMenuId();
        topmenu.ModuleId = Convert.ToInt32(Application["ModuleId"].ToString());
        topmenu.RoleID = Convert.ToInt32(Session["RoleId"].ToString());
        topmenu.SelectedMenu = Convert.ToInt32(mid.ToString());
        topmenu.DataBind();
        Session["menuname"] = menuname;
        Session["menuid"] = menuid;
        Session["MId"] = mid;
        if (!IsPostBack)
        {
            int EmpId = Convert.ToInt32(Session["UserId"]);
            GetWS();
            GetDept();
            ChkFillWS();
            //BtnSearch_Click(sender, e);
            BindPager();
        }
    }

    public void GetWS()
    {
        DataSet ds = new DataSet();
        ds = objRights.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlDropWorkSite.DataSource = ds.Tables[0];
            ddlDropWorkSite.DataTextField = "Site_Name";
            ddlDropWorkSite.DataValueField = "Site_ID";
            ddlDropWorkSite.DataBind();
        }
        ddlDropWorkSite.Items.Insert(0, new ListItem("---Select---", "0"));

    }

    public void GetDept()
    {
        DataSet ds = new DataSet();
        ds = objRights.GetDepartments(0);
        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlDept.DataSource = ds.Tables[0];
            ddlDept.DataTextField = "Deptname";
            //ddlDept.DataTextField = "DepartmentName";
            ddlDept.DataValueField = "DepartmentUId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }

    public void ChkFillWS()
    {
        DataSet ds = new DataSet();
        ds = objRights.GetWorkSite(0, '1', Convert.ToInt32(Session["CompanyID"]));
        FIllObject.FillCheckBoxList(ref ChkWS, ds);
    }

    public void BindGrid(HRCommon objHrCommon)
    {

        ChkWS.Visible = false;
        int? WorksiteID = null;
        string EmpName = "";
        int? DeptID = null;

        if (ddlDropWorkSite.SelectedIndex != 0)
            WorksiteID = Convert.ToInt32(ddlDropWorkSite.SelectedValue);

        if (ddlDept.SelectedIndex != 0)
            DeptID = Convert.ToInt32(ddlDept.SelectedValue);

        EmpName = txtname.Text.Trim();
        objHrCommon.PageSize = MAPaging.ShowRows;
        objHrCommon.CurrentPage = MAPaging.CurrentPage;

        DataSet Ds = new DataSet();
        Ds = AttendanceDAC.GetEmployeeRolesByPaging(objHrCommon, WorksiteID, EmpName, DeptID,Convert.ToInt32(Session["CompanyID"]));
        if (Ds != null && Ds.Tables[0].Rows.Count > 0 && Ds.Tables.Count > 0)
        {
            tbl1Grid.Visible = true;
            BtnSave.Visible = true;
            GVAtten.DataSource = Ds;
            MAPaging.Bind(objHrCommon.CurrentPage, objHrCommon.TotalPages, objHrCommon.NoofRecords, objHrCommon.PageSize);

        }
        else
        {
            GVAtten.EmptyDataText = "No Records Found";
            MAPaging.Visible = false;
        }
        GVAtten.DataBind();
        if (GVAtten.Rows.Count > 0)
            ChkWS.Visible = true;


    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        tbl1Grid.Visible = true;
        MAPaging.CurrentPage = 1;
        BindPager();
    }
    protected void GVAtten_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        foreach (GridViewRow row in GVAtten.Rows)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)row.FindControl("Chk");
            chk.Checked = false;
        }
        if (e.CommandName == "Ed")
        {
            LinkButton lnk = new LinkButton();
            lnk = (LinkButton)e.CommandSource;
            GridViewRow selectedRow = (GridViewRow)lnk.Parent.Parent;
            CheckBox chk = new CheckBox();
            chk = (CheckBox)selectedRow.FindControl("Chk");
            chk.Checked = true;

            foreach (ListItem Item in ChkWS.Items)
            {
                Item.Selected = false;
            }

            DataSet DS = new DataSet();
            int EMPID = Convert.ToInt32(e.CommandArgument);
            DS = AttendanceDAC.GETEMPWorksite(EMPID);

            foreach (DataRow ROW in DS.Tables[0].Rows)
            {
                foreach (ListItem Item in ChkWS.Items)
                {
                    if (ROW[0].ToString() == Item.Value)
                    {
                        Item.Selected = true;
                    }
                }
            }
        }
    }
    protected void GVAtten_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkEdt = (LinkButton)e.Row.FindControl("EditE");
            lnkEdt.Enabled =  Editable;
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GVAtten.Rows)
        {
            CheckBox Chk = new CheckBox();
            Label lblEmpid = new Label();

            Chk = (CheckBox)row.FindControl("Chk");
            lblEmpid = (Label)row.FindControl("lblempid");

            if (Chk.Checked)
            {
                foreach (ListItem item in ChkWS.Items)
                {

                    int Status = 0;
                    if (item.Selected)
                        Status = 1;
                    int EmpID = Convert.ToInt32(lblEmpid.Text);
                    int WSID = Convert.ToInt32(item.Value);
                    AttendanceDAC.MAPWorksite(EmpID, Status, WSID);

                }
            }
        }

        AlertMsg.MsgBox(Page, "Inserted Sucessfully!");
    }
}