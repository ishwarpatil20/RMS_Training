using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.BusinessEntities.Common
{
    public class FileDetail
    {

        public FileDetail() { }
        public FileDetail(string filePath, string fileName) 
        {
            this.FilePath = filePath;
            this.FileName = fileName;
            this.DownloadFlag = true;
        }

        public int FileId { get; set; }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public string PhysicalFileName { get; set; }

        public bool DownloadFlag { get; set; }

        public bool DeleteFlag { get; set; }

    }
}
