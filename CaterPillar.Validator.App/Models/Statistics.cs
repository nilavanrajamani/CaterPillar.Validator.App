using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterPillar.Validator.WebApp.Models
{
    public class Statistics
    {
        public string Date { get; set; }
        public DailyStatistics DailyStatistics { get; set; }
        public DateTime DateInFormat
        {
            get
            {
                return Convert.ToDateTime(Date);
            }
        }
    }
}
