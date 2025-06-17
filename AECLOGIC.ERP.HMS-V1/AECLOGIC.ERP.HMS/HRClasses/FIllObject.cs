using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using Aeclogic.Common.DAL;
public static class FIllObject
{
    public static int Updatepassword(string empid, string oldpwd, string pwd)
    {
        try
        {
            SqlParameter[] objParam = new SqlParameter[4];
            objParam[0] = new SqlParameter("@EmpID", empid);
            objParam[1] = new SqlParameter("@OldPassword", oldpwd);
            objParam[2] = new SqlParameter("@NewPassword", pwd);
            objParam[3] = new SqlParameter("ReturnValue", System.Data.SqlDbType.Int);
            objParam[3].Direction = ParameterDirection.ReturnValue;
            SQLDBUtil.ExecuteNonQuery("HR_Updatepassword", objParam);
            return Convert.ToInt32(objParam[3].Value);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static void FillEmptyDropDown(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
    }
    public static void FillDropDown(ref DropDownList ddl, string spname)
    {
       
        DataSet ds= SQLDBUtil.ExecuteDataset(spname);
        ddl.Items.Clear();
        ddl.DataSource = ds;
        ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
        ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        ds.Dispose();
    }
    public static void FillDropDown(ref DropDownList ddl, string spname, string Other)
    {
       
        DataSet ds= SQLDBUtil.ExecuteDataset(spname);
        ddl.Items.Clear();
        ddl.DataSource = ds;
        ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
        ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(Other, "0"));
        ds.Dispose();
    }
    public static DataSet FillDropDown(ref DropDownList ddl, string spname, string ValueField, string TextField)
    {
        return BindListObject(ddl, spname, ValueField, TextField, "", (SqlParameter[])null);
    }

    public static void FillCheckBoxList(ref CheckBoxList ddl, string spname)
    {
       
        DataSet ds= SQLDBUtil.ExecuteDataset(spname);
        ddl.Items.Clear();
        ddl.DataSource = ds;
        ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
        ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddl.DataBind();
        ds.Dispose();
    }
    public static void FillListBox(ref ListBox ListRef, string spname, SqlParameter[] parms)
    {
       
        DataSet ds= SqlHelper.ExecuteDataset(spname, parms);
        ListRef.ClearSelection();
        ListRef.Items.Clear();
        ListRef.DataSource = ds;
        ListRef.DataTextField = ds.Tables[0].Columns[1].ToString();
        ListRef.DataValueField = ds.Tables[0].Columns[0].ToString();
        ListRef.DataBind();
        ds.Dispose();
    }
    public static void FillListBox(ref ListBox ListRef, DataView dvList, string TextMember, string ValueMember)
    {
        ListRef.DataSource = dvList;
        ListRef.DataTextField = TextMember;
        ListRef.DataValueField = ValueMember;
        ListRef.DataBind();
    }
    
    public static void FillListBox(ref ListBox ListRef, DataTable dtList, string TextMember, string ValueMember)
    {
        ListRef.DataSource = dtList;
        ListRef.DataTextField = TextMember;
        ListRef.DataValueField = ValueMember;
        ListRef.DataBind();
    }
    public static void FillDropDown1(ref DropDownList ddl, string spname)
    {
       
        DataSet ds= SQLDBUtil.ExecuteDataset(spname);
        ddl.Items.Clear();
        ddl.DataSource = ds;
        ddl.DataTextField = ds.Tables[0].Columns[3].ToString();
        ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds.Dispose();
    }
    public static DataSet FillListBox(ref ListBox ddl, string spname)
    {
        return BindListObject(ddl, spname, "", "", (SqlParameter[])null);
    }

    public static DataSet FillListBox(ref ListBox ddl, string spname, string ValueField, string TextField)
    {
        return BindListObject(ddl, spname, ValueField, TextField, (SqlParameter[])null);
    }

   

    public static DataSet FillListBox(ref ListBox ddl, string spname, string ValueField, string TextField, SqlParameter[] parms)
    {
        return BindListObject(ddl, spname, ValueField, TextField, parms);
    }
    private static DataSet BindListObject(ListControl ListObject, string spname, string ValueField, string TextField, string DefaultString, SqlParameter[] parms)
    {
        DataSet ds = new DataSet();
        if (parms == null)
             ds= SqlHelper.ExecuteDataset(spname);
        else
             ds= SqlHelper.ExecuteDataset(spname, parms);

        FillListObject(ListObject, ValueField, TextField, DefaultString, ds);
        return ds;
    }
    public static void FillListObject(ListControl ListObject, string ValueField, string TextField, string DefaultString, DataSet ds)
    {
        ListObject.Items.Clear();
        ListObject.DataSource = ds;
        if (ValueField != "" && TextField != "")
        {
            ListObject.DataTextField = ds.Tables[0].Columns[TextField].ToString();
            ListObject.DataValueField = ds.Tables[0].Columns[ValueField].ToString();
        }
        else
        {
            ListObject.DataTextField = ds.Tables[0].Columns[1].ToString();
            ListObject.DataValueField = ds.Tables[0].Columns[0].ToString();
        }
        ListObject.DataBind();
        if (DefaultString != "")
            ListObject.Items.Insert(0, new ListItem(DefaultString, "0"));
    }
    private static DataSet BindListObject(ListControl ListObject, string spname, string ValueField, string TextField, SqlParameter[] parms)
    {
        return BindListObject(ListObject, spname, ValueField, TextField, "", parms);
    }

    public static DataSet FillDropDown(ref DropDownList ddl, string spname, string DefaultString, SqlParameter[] parms)
    {
        return FillDropDown(ref ddl, spname, "", "", DefaultString, parms);
    }
    public static DataSet FillDropDown(ref DropDownList ddl, string spname, string ValueField, string TextField, string DefaultString, SqlParameter[] parms)
    {
        return BindListObject(ddl, spname, ValueField, TextField, DefaultString, parms);
    }
    public static void FillDropDown(ref DropDownList ddl, string spname, SqlParameter[] parms)
    {
       
        DataSet ds= SQLDBUtil.ExecuteDataset(spname, parms);
        ddl.ClearSelection();
        ddl.SelectedValue = null;
        ddl.Items.Clear();
        ddl.DataSource = ds;
        ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
        ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds.Dispose();
        
    }
    public static void FillDropDown3(ref DropDownList ddl, string spname, SqlParameter[] parms)
    {
       
        DataSet ds= SQLDBUtil.ExecuteDataset(spname, parms);
        ddl.ClearSelection();
        ddl.SelectedValue = null;
        ddl.Items.Clear();
        ddl.DataSource = ds;
        ddl.DataTextField = ds.Tables[0].Columns[0].ToString();
        ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        ds.Dispose();

    }




    public static DataSet FillDropDown2(ref DataSet DsReturn, ref DropDownList ddl, string spname, SqlParameter[] parms)
    {
        DsReturn = SQLDBUtil.ExecuteDataset(spname, parms);
        ddl.Items.Clear();
        ddl.DataSource = DsReturn;
        ddl.DataTextField = DsReturn.Tables[0].Columns[1].ToString();
        ddl.DataValueField = DsReturn.Tables[0].Columns[0].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("------ALL------", "0"));
        return DsReturn;
    }
    public static void FillGridview(ref GridView gv, string spname)
    {
        gv.DataSource = SQLDBUtil.ExecuteDataset(spname);
        gv.DataBind();
    }
    public static void FillGridview(ref GridView gv, string spname, SqlParameter[] parms)
    {
        gv.DataSource = SQLDBUtil.ExecuteDataset(spname, parms);
        gv.DataBind();
    }
    public static void FillGridview(ref GridView gv, string spname, SqlParameter[] parms, ref DataSet Ds)
    {
        DataSet ds= SQLDBUtil.ExecuteDataset(spname, parms);
        gv.DataSource = Ds;
        gv.DataBind();
    }
   
    public static DateTime changedateImport(string strdt)
    {
        string strdate = null;
        int strdateday = 0;
        int strdatemonth = 0;
        int strdateyear = 0;
        
        strdateyear = Convert.ToInt32(strdt.Substring(0, 4));
        strdatemonth = Convert.ToInt32(strdt.Substring(4, 2));
        strdateday = Convert.ToInt32(strdt.Substring(6, 2));

        strdate = strdatemonth + "/" + strdateday + "/" + strdateyear;
        strdate = strdate.Trim();

        return new DateTime(strdateyear, strdatemonth, strdateday);
    }
    public static DataSet FillDropDown(ref DataSet DsReturn, ref DropDownList ddl, string spname, SqlParameter[] parms)
    {
        DsReturn = SQLDBUtil.ExecuteDataset(spname, parms);
        ddl.Items.Clear();
        ddl.DataSource = DsReturn;
        ddl.DataTextField = DsReturn.Tables[0].Columns[1].ToString();
        ddl.DataValueField = DsReturn.Tables[0].Columns[0].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select", "0"));
        return DsReturn;
    }
    public static void Filter(ref DropDownList ddldropdown, ref DataSet DsLedgers, string SearchString, bool AdvancedSeach)
    {
        DataView Dv = DsLedgers.Tables[0].DefaultView;

        if (AdvancedSeach)
            Dv.RowFilter = AdvRowFilterBuilder("Ledger", SearchString);
        else
            Dv.RowFilter = RowFilterBuilder("Ledger", SearchString);
        ddldropdown.Items.Clear();
        ddldropdown.DataSource = Dv;
        ddldropdown.DataBind();
        ddldropdown.Items.Insert(0, new ListItem("Select", "0"));
    }
    public static string RowFilterBuilder(string Column, string FilterValue)
    {
        string ReturnValue = "";
        string[] strSplitArray = FilterValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (strSplitArray.Length > 0)
            ReturnValue = Column + " Like '%" + EscapeLikeValue(strSplitArray[0]) + "%'";

        for (int i = 1; i < strSplitArray.Length; i++)
        {
            ReturnValue += " AND " + Column + " Like '%" + EscapeLikeValue(strSplitArray[i]) + "%'";
        }

        return ReturnValue;
    }
    public static string AdvRowFilterBuilder(string Column, string FilterValue)
    {
        string ReturnValue = "";
        string[] strSplitArray = FilterValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        ReturnValue = Column + " Like '%" + EscapeLikeValue(strSplitArray[0]) + "%'";
        for (int i = 1; i < strSplitArray.Length; i++)
        {
            ReturnValue += " OR " + Column + " Like '%" + EscapeLikeValue(strSplitArray[i]) + "%'";
        }

        return ReturnValue;
    }
    public static string EscapeLikeValue(string valueWithoutWildcards)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < valueWithoutWildcards.Length; i++)
        {
            char c = valueWithoutWildcards[i];
            if (c == '*' || c == '%' || c == '[' || c == ']')
                sb.Append("[").Append(c).Append("]");
            else if (c == '\'')
                sb.Append("''");
            else
                sb.Append(c);
        }
        return sb.ToString();
    }
    public static void FilterGroupName(ref DropDownList ddldropdown, ref DataSet DsLedgers, string SearchString, bool AdvancedSeach)
    {
        DataView Dv = DsLedgers.Tables[0].DefaultView;

        if (AdvancedSeach)
            Dv.RowFilter = AdvRowFilterBuilder("GroupName", SearchString);
        else
            Dv.RowFilter = RowFilterBuilder("GroupName", SearchString);
        ddldropdown.Items.Clear();
        ddldropdown.DataSource = Dv;
        ddldropdown.DataBind();
        ddldropdown.Items.Insert(0, new ListItem("Select", "0"));
    }
    public static bool FilterGroupName(ref DataSet DsGroups, string SearchString)
    {
        bool va = true;
        DataView Dv = DsGroups.Tables[0].DefaultView;
        SearchString = RemoveSpecialCharacters(SearchString);

        Dv.RowFilter = "GroupName = '" + SearchString + "'";
        if (Dv.Count > 0) va = false;
        return va;
    }
    public static string RemoveSpecialCharacters(string str)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            char c = Convert.ToChar(str[i].ToString());
            if (c == '[' || c == '_' || c == ']' || c == '(' || c == ')')
            { }  

            else
                sb.Append(str[i]);
        }        
        return sb.ToString();
    }

    public static void FillDropDown_AssetType(ref DropDownList ddl, string spname)
    {
       
        DataSet ds= SQLDBUtil.ExecuteDataset(spname);
        ddl.Items.Clear();
        ddl.DataSource = ds;
        ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
        ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("------ALL------", "0"));
        ds.Dispose();
    }
    #region CheckBox
    public static void FillCheckBoxList(ref CheckBoxList objDDL, DataSet dataSet)
    {
        try
        {
            
            FillCheckBoxList(ref objDDL, dataSet.Tables[0], String.Empty, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public static void FillCheckBoxList(ref CheckBoxList objDDL, DataTable dataTable, String customText, bool insertFirst)
    {
        try
        {
            FillCheckBoxList(ref objDDL, dataTable, dataTable.Columns[0].ColumnName, dataTable.Columns[1].ColumnName, "0", customText, insertFirst);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public static void FillCheckBoxList(ref CheckBoxList objDDL, DataTable dataTable, String valueField, String displayField, String customValue, String customText, bool insertFirst)
    {
        try
        {
            objDDL.ClearSelection();
            objDDL.Items.Clear();
            objDDL.DataSource = dataTable;
            objDDL.DataTextField = displayField;
            objDDL.DataValueField = valueField;
            objDDL.DataBind();

            if (customText.Trim() != String.Empty)
            {
                if (insertFirst)
                {
                    objDDL.Items.Insert(0, new ListItem(customText, customValue));
                }
                else
                {
                    objDDL.Items.Add(new ListItem(customText, customValue));
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion CheckBox

    public static DateTime changedate(string strdt)
    {
        string strdate = null;
        int strdateday = 0;
        int strdatemonth = 0;
        int strdateyear = 0;
        int intc = 0;
        intc = strdt.IndexOf("/");
        strdateday = Convert.ToInt32(strdt.Substring(0, intc));
        strdt = strdt.Substring(intc + 2);
        intc = strdt.IndexOf("/");
        strdatemonth = Convert.ToInt32(strdt.Substring(0, intc));
        strdt = strdt.Substring(intc + 1);
        strdateyear = Convert.ToInt32(strdt);

        strdate = strdatemonth + "/" + strdateday + "/" + strdateyear;
        strdate = strdate.Trim();

        return new DateTime(strdateyear, strdatemonth, strdateday);
    }
    public static void FillEmptygridview(ref GridView gv)
    {
        gv.DataSource = null;
        gv.DataBind();
    }
}

