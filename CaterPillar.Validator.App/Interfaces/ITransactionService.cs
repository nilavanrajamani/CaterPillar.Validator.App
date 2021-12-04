using CaterPillar.Validator.WebApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterPillar.Validator.WebApp.Interfaces
{
    public interface ITransactionService
    {
        Transaction Process(IFormFile file, string transactionType);
    }
}
