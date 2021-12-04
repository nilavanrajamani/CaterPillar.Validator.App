using CaterPillar.Validator.WebApp.Helpers;
using CaterPillar.Validator.WebApp.Interfaces;
using CaterPillar.Validator.WebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CaterPillar.Validator.WebApp.Services
{
    public class FileOperation : IFileOperation
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FileOperation(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IEnumerable<SalesRecord> GetDataCollection(string filePath)
        {
            List<SalesRecord> salesRecords = new List<SalesRecord>();
            using (var reader = new StreamReader(filePath))
            {
                var headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    salesRecords.Add(SalesRecord.Create(values[0], values[1], values[2], values[3], values[4],
                        values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13]));
                }
            }
            return salesRecords.AsEnumerable();
        }
        public Transaction SaveUploadedFile(IFormFile file, Transaction currentTransaction)
        {
            if (currentTransaction == null)
            {
                throw new Exception("Current Transaction is not valid");
            }

            if (!Directory.Exists(Path.Combine(_hostingEnvironment.ContentRootPath, DateTime.UtcNow.Date.ToShortDateString(), currentTransaction.GetGuid())))
            {
                Directory.CreateDirectory(Path.Combine(_hostingEnvironment.ContentRootPath, DateTime.UtcNow.Date.ToShortDateString(), currentTransaction.GetGuid()));
            }

            currentTransaction.FileGuid = Guid.NewGuid().ToString();
            currentTransaction.FileName = Path.Combine(_hostingEnvironment.ContentRootPath, DateTime.UtcNow.Date.ToShortDateString(), currentTransaction.GetGuid(), string.Concat(currentTransaction.FileGuid, ".csv"));

            using (FileStream fs = File.Create(currentTransaction.FileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return currentTransaction;
        }

        public Transaction WriteInvalidRecords(IEnumerable<SalesRecord> invalidRecords, Transaction transaction)
        {
            if (invalidRecords.Any())
            {
                CsvExport<SalesRecord> invalidRecordsCsvExport = new CsvExport<SalesRecord>(invalidRecords.ToList());
                string errorFilePath = Path.Combine(Path.GetDirectoryName(transaction.FileName), String.Concat(transaction.FileGuid, "_Invalid", ".csv"));
                invalidRecordsCsvExport.ExportToFile(errorFilePath);
                transaction.ErrorFileName = errorFilePath;
            }
            return transaction;
        }
    }
}
