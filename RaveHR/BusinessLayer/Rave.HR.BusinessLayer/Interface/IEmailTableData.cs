using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rave.HR.BusinessLayer.Interface
{
    public interface IEmailTableData
    {
        int RowCount { get; set; }
        string[] Header{get;set;}
        string[,] RowDetail { get; set; }
        string GetTableData(IEmailTableData objEmailTableData);
        string GetVerticalHeaderTableData(IEmailTableData objEmailTableData);
        string GetTableDataForDate(IEmailTableData objEmailTableData);
    }
}
