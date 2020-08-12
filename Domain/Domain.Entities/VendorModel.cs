using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class VendorModel
    {
        public int? VendorId {get;set;}
        public string VendorName { get; set; }

        [Remote("doesEmailExists", "VendorMaster", HttpMethod = "Post", ErrorMessage = "Email already exist. Please enter valid email address.")]
        public string VendorEmailId { get; set; }
        [MaxLength(100)]
        public string ContactPersonName { get; set; }

        [RegularExpression("[0-9]+(,[0-9]+)*", ErrorMessage="Contact Person number should be number with comma seperation"),MaxLength(100)]
        public string ContactPersonNumber { get; set; }
        [MaxLength(200)]
        public string Expertise { get; set; }

    }
}
