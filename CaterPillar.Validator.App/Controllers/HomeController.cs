using CaterPillar.Validator.App.Models;
using CaterPillar.Validator.WebApp.Interfaces;
using CaterPillar.Validator.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace CaterPillar.Validator.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransactionService _transactionService;
        private readonly IAnalyticsService _analyticsService;

        public HomeController(ILogger<HomeController> logger, ITransactionService transactionService,
            IAnalyticsService analyticsService)
        {
            _logger = logger;
            _transactionService = transactionService;
            _analyticsService = analyticsService;
        }

        public IActionResult Index()
        {
            ViewBag.DailyStatistics = _analyticsService.GetTodayStatistics();            
            return View();
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 734003200)]
        [RequestSizeLimit(734003200)]
        public IActionResult Upload(IFormFile file, string transactionType)
        {
            Transaction currentTransaction = _transactionService.Process(file, transactionType);            
            ViewBag.CurrentTransaction = currentTransaction;
            ViewBag.DailyStatistics = _analyticsService.GetTodayStatistics();            
            return View("Index");
        }

        [HttpGet]
        public JsonResult GetMonthlyStatistics()
        {
            return Json(_analyticsService.GetMonthlyStatistics());
        }

        public IActionResult Download([FromQuery] string link)
        {
            var net = new System.Net.WebClient();
            var data = net.DownloadData(link);
            var content = new System.IO.MemoryStream(data);
            var contentType = "APPLICATION/octet-stream";
            var fileName = "errorfile.csv";
            return File(content, contentType, fileName);
        }
    }
}
