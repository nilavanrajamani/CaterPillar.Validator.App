using CaterPillar.Validator.WebApp.Interfaces;
using CaterPillar.Validator.WebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CaterPillar.Validator.WebApp.Models.CONSTANTS;

namespace CaterPillar.Validator.WebApp.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private IWebHostEnvironment _hostingEnvironment;

        public AnalyticsService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public DailyStatistics GetTodayStatistics()
        {
            if (File.Exists(Path.Combine(_hostingEnvironment.ContentRootPath, "statistics.json")))
            {
                var jsonContent = File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "statistics.json"));
                if (!string.IsNullOrWhiteSpace(jsonContent))
                {
                    List<Statistics> lstStatisTics = JsonConvert.DeserializeObject<List<Statistics>>(jsonContent);
                    Statistics statistics = lstStatisTics.Where(x => x.Date == DateTime.UtcNow.Date.ToShortDateString()).FirstOrDefault();
                    if (statistics != null)
                    {
                        return statistics.DailyStatistics;
                    }

                }
            }
            return new DailyStatistics { CountOfInvalidRecords = 0, CountOfRecords = 0, CountOfValidRecords = 0 };
        }

        public List<Statistics> GetMonthlyStatistics()
        {
            if (File.Exists(Path.Combine(_hostingEnvironment.ContentRootPath, "statistics.json")))
            {
                var jsonContent = File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "statistics.json"));
                if (!string.IsNullOrWhiteSpace(jsonContent))
                {
                    List<Statistics> lstStatisTics = JsonConvert.DeserializeObject<List<Statistics>>(jsonContent);
                    return lstStatisTics.Where(x => x.DateInFormat >= DateTime.UtcNow.Date.AddMonths(-1)).ToList();
                }
            }
            return new List<Statistics>();
        }

        public DailyStatistics AddStatistics(Transaction transaction, string transactionType)
        {
            if (transaction.InvalidRecordsCount == 0 ||
                (transaction.InvalidRecordsCount > 0 && transactionType == TransactionType.NonTransaction))
            {
                List<Statistics> lstStatistics = GetCurrentStatistics();
                Statistics statistics = lstStatistics.Where(x => x.Date == DateTime.UtcNow.Date.ToShortDateString()).FirstOrDefault();
                if (statistics != null && statistics.DailyStatistics != null)
                {
                    statistics.DailyStatistics.CountOfRecords += transaction.TotalRecordsCount;
                    statistics.DailyStatistics.CountOfInvalidRecords += transaction.InvalidRecordsCount;
                    statistics.DailyStatistics.CountOfValidRecords += transaction.TotalRecordsCount - transaction.InvalidRecordsCount;

                    //Save current statistics
                    SaveCurrentStatistics(lstStatistics);
                    return statistics.DailyStatistics;
                }
            }
            return new DailyStatistics();
        }

        private void SaveCurrentStatistics(List<Statistics> lstStatistics)
        {
            if (lstStatistics != null)
            {
                File.WriteAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "statistics.json"), JsonConvert.SerializeObject(lstStatistics));
            }

        }

        private List<Statistics> GetCurrentStatistics()
        {
            if (File.Exists(Path.Combine(_hostingEnvironment.ContentRootPath, "statistics.json")))
            {
                var jsonContent = File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "statistics.json"));
                if (!string.IsNullOrWhiteSpace(jsonContent))
                {
                    return JsonConvert.DeserializeObject<List<Statistics>>(jsonContent);
                }
            }
            return new List<Statistics>() { new Statistics() { Date = DateTime.UtcNow.Date.ToShortDateString(), DailyStatistics = new DailyStatistics() } };
        }


    }
}
