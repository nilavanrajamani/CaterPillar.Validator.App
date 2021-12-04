using CaterPillar.Validator.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterPillar.Validator.WebApp.Interfaces
{
    public interface IValidator
    {
        bool IsInvalid(SalesRecord salesRecord);
        string GetErrorComments();
    }
}
