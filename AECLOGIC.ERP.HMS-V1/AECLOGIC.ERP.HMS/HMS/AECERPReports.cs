using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Aeclogic.Common.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for AECERPReports
/// </summary>
[WebService(Namespace = "http://microsoft.com/webservices/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AECERPReports : System.Web.Services.WebService {

    public AECERPReports () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string TestString()
    {
        return "Hello World123";
    }
    [WebMethod]
    public int Testint()
    {
        return 1;
    }
   
    [WebMethod]
    public  string OMS_RPT_Costsummary()
    {
        DataSet ds = new DataSet();
        SqlConnection objconnection = new SqlConnection("Password=SrvKphov;Persist Security Info=True;User ID=sa;Initial catalog= TTD; Data Source=192.168.1.28; Connection Timeout=120");
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDataAdapter = new SqlDataAdapter();
        objCommand.Connection = objconnection;
        objCommand.CommandType = CommandType.Text;
        objCommand.CommandText = "SELECT * FROM T_OMS_Tasks";
        objCommand.CommandTimeout = 100;
        
        try
        {
            objCommand.Connection.Open();
            objDataAdapter.SelectCommand = objCommand;
            objDataAdapter.Fill(ds);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            objCommand.Connection.Close();
        }
        string JSONresult = DataTableToJsonObj(ds.Tables[0]);
        return JSONresult;

    }

    public string DataTableToJsonObj(DataTable dt)
    {
        DataSet ds = new DataSet();
        ds.Merge(dt);
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            JsonString.Append("[");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }  
   
    
}
