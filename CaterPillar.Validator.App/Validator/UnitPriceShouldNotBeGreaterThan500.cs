using CaterPillar.Validator.WebApp.Interfaces;
using CaterPillar.Validator.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterPillar.Validator.WebApp.Validator
{
    public class UnitPriceShouldNotBeGreaterThan500 : IValidator
    {
        private const string ERROR_COMMENTS = "Unit Price Should Not Be Greater Than 500";
        public bool IsInvalid(SalesRecord salesRecord)
        {
            return salesRecord.UnitPriceDecimal > 500;
        }
        public string GetErrorComments()
        {
            return ERROR_COMMENTS;
        }
    }
}
