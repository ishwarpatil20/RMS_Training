using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rave.HR.BusinessLayer.Interface;

namespace Rave.HR.BusinessLayer
{
    public class EmailTableData : IEmailTableData
    {
        #region Private Field Members
        private int _rowCount;
        private string[] _header;
        private string[,] _rowDetail;
        private string strHeaderStyle = "height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;horizontal-align:center;";

        private string strRowStyle = "font-family: Verdana;	font-size: 9pt;	padding-left: 5px;vertical-align:top;";
        #endregion Private Field Members

        #region IEmailTableData Members

        public int RowCount
        {
            get
            {
                return _rowCount;
            }
            set
            {
                _rowCount = value;
            }
        }

        public string[] Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
            }
        }

        public string[,] RowDetail
        {
            get
            {
                return _rowDetail;
            }
            set
            {
                _rowDetail = value;
            }
        }

        public string GetTableData(IEmailTableData objEmailTableData)
        {
            StringBuilder strTableHTML = new StringBuilder(string.Empty);

            strTableHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '80%'>");
            //--Table Header
            strTableHTML.Append("<tr>");
            for (int colNo = 0; colNo < objEmailTableData.Header.Length; colNo++)
            {
                strTableHTML.Append("<td style = '" + strHeaderStyle + "' >");
                strTableHTML.Append(objEmailTableData.Header[colNo].ToString());
                strTableHTML.Append("</td>");
            }
            strTableHTML.Append("</tr>");
            //--Row Details
            for (int rowNo = 0; rowNo < objEmailTableData.RowCount; rowNo++)
            {
                strTableHTML.Append("<tr>");

                for (int colNo = 0; colNo < objEmailTableData.Header.Length; colNo++)
                {
                    strTableHTML.Append("<td style = '" + strRowStyle + "'>");
                    
                    if (objEmailTableData.RowDetail[rowNo, colNo] != null)
                    strTableHTML.Append(objEmailTableData.RowDetail[rowNo, colNo].ToString());
                   
                    strTableHTML.Append("</td>");
                }
                strTableHTML.Append("</tr>");
            }
            strTableHTML.Append("</table><br/>");
            
            return strTableHTML.ToString();
        }

        public string GetVerticalHeaderTableData(IEmailTableData objEmailTableData)
        {
            StringBuilder strTableHTML = new StringBuilder(string.Empty);

            strTableHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '80%'>");
            
            //--Row Details
            for (int rowNo = 0; rowNo < objEmailTableData.RowCount; rowNo++)
            {
                //Handle those tr which comes null then do not create row in table.
                if (objEmailTableData.RowDetail[rowNo, 0] != null)
                {
                    strTableHTML.Append("<tr>");

                    strTableHTML.Append("<td style = '" + strHeaderStyle + "'>");
                    strTableHTML.Append(objEmailTableData.RowDetail[rowNo, 0].ToString());
                    strTableHTML.Append("</td>");

                    strTableHTML.Append("<td style = '" + strRowStyle + "'>");
                    strTableHTML.Append(objEmailTableData.RowDetail[rowNo, 1].ToString());
                    strTableHTML.Append("</td>");

                    strTableHTML.Append("</tr>");
                }
            }
            strTableHTML.Append("</table><br/>");

            return strTableHTML.ToString();
        }


        public string GetTableDataForDate(IEmailTableData objEmailTableData)
        {
            StringBuilder strTableHTML = new StringBuilder(string.Empty);

            strTableHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '60%'>");
            //--Table Header
            strTableHTML.Append("<tr>");
            for (int colNo = 0; colNo < objEmailTableData.Header.Length; colNo++)
            {
                if (objEmailTableData.Header[colNo] != null)
                {
                    strTableHTML.Append("<td style = '" + strHeaderStyle + "' >");
                    strTableHTML.Append(objEmailTableData.Header[colNo].ToString());
                    strTableHTML.Append("</td>");
                }
            }
            strTableHTML.Append("</tr>");
            //--Row Details
            for (int rowNo = 0; rowNo < objEmailTableData.RowCount; rowNo++)
            {
                strTableHTML.Append("<tr>");
                // Venkatesh : Issue 46194 : 12/Dec/2013 : Starts               
                // Desc : Show Edited joining date & designation
                //for (int colNo = 0; colNo < 3; colNo++)
                
                for (int colNo = 0; colNo < objEmailTableData.Header.Length; colNo++)
                {
                    if (objEmailTableData.RowDetail[rowNo, colNo] != null)
                    {
                        strTableHTML.Append("<td style = '" + strRowStyle + "'>");

                        strTableHTML.Append(objEmailTableData.RowDetail[rowNo, colNo].ToString());

                        strTableHTML.Append("</td>");
                    }
                }
                // Venkatesh : Issue 46194 : 12/Dec/2013 : End
                strTableHTML.Append("</tr>");
            }
            strTableHTML.Append("</table><br/>");

            return strTableHTML.ToString();
        }

        #endregion
    }
}
