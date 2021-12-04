using CaterPillar.Validator.WebApp.Interfaces;
using CaterPillar.Validator.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterPillar.Validator.WebApp.Validator
{
    public class UnitsSoldShouldNotBeGreaterThan9000 : IValidator
    {
        private const string ERROR_COMMENTS = "Units Sold Should not Be Greater Than 9000";

        public bool IsInvalid(SalesRecord salesRecord)
        {
            return salesRecord.UnitSoldInt > 9000;
        }

        public string GetErrorComments()
        {
            return ERROR_COMMENTS;
        }
    }
}
