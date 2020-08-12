using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FileDetails
    {
        public int FileId { get; set; }

        public string FileName { get; set; }

        public string FileGuid { get; set; }

        public string Category { get; set; }
    }
}
