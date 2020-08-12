using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Common.BusinessEntities.Common;

namespace Domain.Entities
{
    public class FileUploadModel
    {

        public FileUploadModel() 
        {
            this.FileDetails = new List<FileDetail>();
        }

        public FileUploadModel(List<FileDetail> fileDetails)
        {
            this.FileDetails = fileDetails;
        }

        public FileUploadModel(List<FileDetail> fileDetails, string module, string entityId, string targetID, string dir, bool showDelete = false)
        {
            this.FileDetails = fileDetails;
            this.Module = module;
            this.EntityID = entityId;
            this.TargetId = targetID;
            this.FileDirectory = dir;
        }

        public List<FileDetail> FileDetails { get; set; }

        public int FileViewMode { get; set; }

        public bool DeleteFlag { get; set; }

        public string Module { get; set; }

        public string EntityID { get; set; }

        public string TargetId {get; set;}

        public string FileDirectory { get; set; }

    }
}
