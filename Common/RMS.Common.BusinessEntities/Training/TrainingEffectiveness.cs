using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.BusinessEntities
{
    public class Effectiveness
    {
        public int EffectivenessID { get; set; }

        public string EffectivenessName { get; set; }

        public bool IsSelected { get; set; }
    }

    public class PostedEffectiveness
    {
        //this array will be used to POST values from the form to the controller
        public string[] EffectivenessIds { get; set; }
    }
}
