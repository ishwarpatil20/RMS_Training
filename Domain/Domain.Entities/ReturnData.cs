using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public  class ResultData
    {
        public bool IsSucess { get; set; }
        public object JsonData { get; set; }
        public string ErrorMessage { get; set; }
    }
}
