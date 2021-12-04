using CaterPillar.Validator.WebApp.Interfaces;
using CaterPillar.Validator.WebApp.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaterPillar.Validator.WebApp.Services
{
    public class ValidationService : IValidationService
    {
        private IEnumerable<IValidator> _validators;

        public ValidationService(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }
        public IEnumerable<SalesRecord> PerformValidation(IEnumerable<SalesRecord> salesRecords)
        {
            List<SalesRecord> invalidRecords = new List<SalesRecord>();
            foreach (SalesRecord salesRecord in salesRecords)
            {
                foreach (IValidator validator in _validators)
                {
                    if (validator.IsInvalid(salesRecord))
                    {
                        salesRecord.ErrorComments = string.IsNullOrWhiteSpace(salesRecord.ErrorComments) ? "" : salesRecord.ErrorComments + " | ";
                        salesRecord.ErrorComments += validator.GetErrorComments();
                        invalidRecords.Add(salesRecord);
                    }                    
                }
            }
            return invalidRecords;
        }

        public IEnumerable<SalesRecord> PerformValidationParallel(IEnumerable<SalesRecord> salesRecords)
        {
            ConcurrentBag<SalesRecord> invalidRecords = new ConcurrentBag<SalesRecord>();
            Parallel.ForEach(salesRecords, salesRecord =>
            {
                foreach (IValidator validator in _validators)
                {
                    if (validator.IsInvalid(salesRecord))
                    {
                        salesRecord.ErrorComments = string.IsNullOrWhiteSpace(salesRecord.ErrorComments) ? "" : salesRecord.ErrorComments + " | ";
                        salesRecord.ErrorComments += validator.GetErrorComments();
                        invalidRecords.Add(salesRecord);
                    }
                }
            });
            return invalidRecords;
        }
    }
}
