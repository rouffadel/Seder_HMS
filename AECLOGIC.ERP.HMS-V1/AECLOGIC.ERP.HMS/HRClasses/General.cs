using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for General
/// </summary>
public static class General
{
}

[Serializable]
public class GridSort
{
    public GridSort()
    {
        //
        // TODO: Add constructor logic here
        //
        _Column = "";
        _SortingOrder = SorOrder.Ascending;
    }

    private string _Column;
    private SorOrder _SortingOrder;
    public string Column
    {
        get { return _Column; }
        set { _Column = value; }
    }

    public SorOrder SortingOrder
    {
        get { return _SortingOrder; }
        set { _SortingOrder = value; }
    }

    public string GetSortExpression(string SortColumn)
    {
        string Expression = SortColumn + " ";
        if (this.Column != SortColumn)
        {
            _Column = SortColumn;
            _SortingOrder = SorOrder.Ascending;
            Expression += "asc";
        }
        else
        {
            if (this.SortingOrder == GridSort.SorOrder.Ascending)
            {
                _SortingOrder = SorOrder.Descending;
                Expression += "desc";
            }
            else
            {
                _SortingOrder = SorOrder.Ascending;
                Expression += "asc";
            }
        }       

        return Expression;
    }

    public enum SorOrder { Ascending = 0, Descending = 1 };

}
