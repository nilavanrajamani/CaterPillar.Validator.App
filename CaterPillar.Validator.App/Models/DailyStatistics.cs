using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterPillar.Validator.WebApp.Models
{
    public class DailyStatistics
    {
        public int CountOfRecords { get; set; }
        public int CountOfValidRecords { get; set; }
        public int CountOfInvalidRecords { get; set; }
    }
}
