using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.BusinessEntities.Menu
{
    public class Menu
    {

        public int ResponsibilityID { get; set; }

        public string MenuName { get; set; }

        public int PageID { get; set; }

        public int ParentID { get; set; }

        public string PageName { get; set; }

        public string PageURL { get; set; }

        public int MenuOrderID { get; set; }

        public List<Menu> SubMenu { get; set; }

        public string baseUrl { get; set; }

        public string ReportName { get; set; }
    }
}
