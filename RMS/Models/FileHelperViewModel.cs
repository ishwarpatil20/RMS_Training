using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Common.BusinessEntities.Common;

namespace RMS.Models
{
    public class FileHelperViewModel
    {
        public FileHelperViewModel() 
        {
            this.FileDetails = new List<FileDetail>();
        }

        public FileHelperViewModel(List<FileDetail> fileDetails)
        {
            this.FileDetails = fileDetails;
        }

        public List<FileDetail> FileDetails { get; set; }
    }
}