using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using Aeclogic.Common.DAL;
/// <summary>
/// Summary description for ReportsRDLC
/// </summary>
public class ReportsRDLC
{
	public ReportsRDLC()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataSet Get_PT_Report(DateTime FromDate,DateTime ToDate)
    {
        try
        {
          
            DataSet ds= SQLDBUtil.ExecuteDataset("HMS_PTreportRDLC", new SqlParameter[] { new SqlParameter("@ToDate", ToDate), new SqlParameter("@FromDate", FromDate) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static DataSet Get_ESI_PF_TDS_Report(DateTime FromDate, DateTime ToDate, int Type, int? WSID)
    {
        try
        {
          
            DataSet ds= SQLDBUtil.ExecuteDataset("HMS_ESI_PF_TDS_ReportRDLC", new SqlParameter[] { new SqlParameter("@ToDate", ToDate), new SqlParameter("@FromDate", FromDate), new SqlParameter("@Type", Type), new SqlParameter("@WSID", WSID) });
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}