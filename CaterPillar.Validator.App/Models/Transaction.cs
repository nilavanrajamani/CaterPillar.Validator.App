using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterPillar.Validator.WebApp.Models
{
    public class Transaction
    {
        public string FileName { get; set; }
        public string FolderGuid { get; set; }
        public string FileGuid { get; set; }
        public int InvalidRecordsCount { get; set; }
        public string ErrorFileName { get; set; }
        public string GetGuid()
        {
            if (string.IsNullOrWhiteSpace(this.FolderGuid))
            {
                FolderGuid = System.Guid.NewGuid().ToString();
            }
            return this.FolderGuid;
        }
        public int TotalRecordsCount { get; set; }
    }
}
