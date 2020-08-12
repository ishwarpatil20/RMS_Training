using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.ExceptionHandling
{
    public static class Sources
    {

        /// Error Source  for Exceptions from PresentationLayer.
        /// </summary>
        public const string PresentationLayer = "Rave.HR.PresentationLayer";

        /// Error Source  for Exceptions from BusinessLayer.
        /// </summary>
        public const string BusinessLayer = "Rave.HR.BusinessLayer";

        /// Error Source  for Exceptions from DataAccessLayer.
        /// </summary>
        public const string DataAccessLayer = "Rave.HR.DataAccessLayer";

        /// Error Source  for Exceptions from DataAccessLayer.
        /// </summary>
        public const string ControllerLayer = "RMS.Controller";

        /// Error Source  for Exceptions from DataAccessLayer.
        /// </summary>
        public const string UILayer = "RMS.Helpers";

        /// Error Source  for Exceptions from Common Layer.
        /// </summary>
        public const string CommonLayer = "Common";
    }
}
