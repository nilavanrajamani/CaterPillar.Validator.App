using CaterPillar.Validator.WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CaterPillar.Validator.WebApp.Interfaces
{
    public interface IFileOperation
    {
        Transaction SaveUploadedFile(IFormFile file, Transaction currentTransaction);
        IEnumerable<SalesRecord> GetDataCollection(string filePath);
        Transaction WriteInvalidRecords(IEnumerable<SalesRecord> invalidRecords, Transaction transaction);
    }
}
