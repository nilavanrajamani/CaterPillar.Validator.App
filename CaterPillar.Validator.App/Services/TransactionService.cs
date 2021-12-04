using CaterPillar.Validator.WebApp.Interfaces;
using CaterPillar.Validator.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Transaction = CaterPillar.Validator.WebApp.Models.Transaction;

namespace CaterPillar.Validator.WebApp.Services
{
    public class TransactionService : ITransactionService
    {
        private ILogger<TransactionService> _logger;
        private IFileOperation _fileOperation;
        private IValidationService _validationService;
        private IAnalyticsService _analyticsService;

        public TransactionService(ILogger<TransactionService> logger, IFileOperation fileOperation,
            IValidationService validationService, IAnalyticsService analyticsService)
        {
            _logger = logger;
            _fileOperation = fileOperation;
            _validationService = validationService;
            _analyticsService = analyticsService;
        }
        public Transaction Process(IFormFile file, string transactionType)
        {
            _logger.LogInformation("Saving uploaded file");
            Transaction currentTransaction = _fileOperation.SaveUploadedFile(file, new Transaction());

            _logger.LogInformation("Get data Collection");
            IEnumerable<SalesRecord> salesRecords =
                _fileOperation.GetDataCollection(currentTransaction.FileName);

            _logger.LogInformation("Perform Validation and return invalid records");
            IEnumerable<SalesRecord> invalidRecords = _validationService.PerformValidation(salesRecords);

            _logger.LogInformation("Create file with invalid records");
            _fileOperation.WriteInvalidRecords(invalidRecords, currentTransaction);

            currentTransaction.TotalRecordsCount = salesRecords.Count();
            currentTransaction.InvalidRecordsCount = invalidRecords.Count();

            _analyticsService.AddStatistics(currentTransaction, transactionType);
            return currentTransaction;
        }
    }
}
