using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using RMS.Common;
using RMS.Common.AuthorizationManager;
using RMS.Common.Constants;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class CommonModel
    {
        public CommonModel() { }

        public string EmpID { get; set; }

        public string EMPCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Designation { get; set; }

        public bool Checked { get; set; }
    }
}
