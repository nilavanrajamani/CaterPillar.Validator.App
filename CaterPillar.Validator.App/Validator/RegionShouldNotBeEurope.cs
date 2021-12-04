using CaterPillar.Validator.WebApp.Interfaces;
using CaterPillar.Validator.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterPillar.Validator.WebApp.Validator
{
    public class RegionShouldNotBeEurope : IValidator
    {
        private const string ERROR_COMMENTS = "Region should not be Europe";
        public bool IsInvalid(SalesRecord salesRecord)
        {
            return string.Equals(salesRecord?.Region, "Europe", StringComparison.InvariantCultureIgnoreCase);
        }
        public string GetErrorComments()
        {
            return ERROR_COMMENTS;
        }
    }
}
