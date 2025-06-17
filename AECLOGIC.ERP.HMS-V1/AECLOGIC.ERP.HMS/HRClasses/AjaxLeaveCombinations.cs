using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using System.Data.SqlClient;

/// <summary>
/// Summary description for AjaxLeaveCombinations
/// </summary>
public class AjaxLeaveCombinations
{

	public AjaxLeaveCombinations()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    [Ajax.AjaxMethod()]
    public bool UpdateLeaveCombination(string Leave1,string Leave2,string Status)
    {
        bool returnValue = false;
        int bStatus=Status=="true"?1:0;
        int Leavetype1 = Convert.ToInt32(Leave1);
        int Leavetype2 = Convert.ToInt32(Leave2); 
        try
        {
            Leaves.InsUpdateLeaveCombination(Leavetype1, Leavetype2, bStatus);
            returnValue = true;
        }
        catch 
        {         
        }
        return returnValue;
    }

}
