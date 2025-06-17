using Aeclogic.Common.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AECLOGIC.ERP.HMS.HRClasses
{
    public class clViewCPRoles
    {
        public static DataSet HR_DailyAttStatus(int EMPID)
        {
            DataSet ds = new DataSet();
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@EMPID", EMPID);//HR_GetWSID(@EMPID int)
            ds = SQLDBUtil.ExecuteDataset("HR_GetWSID", parm);
            return ds;
        }
    }
}