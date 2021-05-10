﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using SNIClassLibrary;
using System.Collections.Generic;
using SuplaNotificationIntegration.Interfaces;

namespace SuplaNotificationIntegration
{
    public class Function
    {
        private readonly IReportsManager _reportsManager;
        private readonly IReportsArchivizer _reportsArchivizer;
        public Function(IReportsManager reportsManager, IReportsArchivizer reportsArchivizer)
        {
            _reportsManager = reportsManager;
            _reportsArchivizer = reportsArchivizer;
        }
        //public static async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer,
        //    [Blob("sni-contaier/sensors.json", System.IO.FileAccess.Read)] Stream myBlob)
        ////public static async Task<IActionResult> Run(
        [FunctionName("Executor")]
        public async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
              ILogger log)
        {
            List<QuarterlyReport> reports = _reportsManager.GenerateQuarterlyReports();
            await _reportsArchivizer.ArchivizeReports(reports);
            await _reportsManager.MailTheReports(reports);
            return new OkObjectResult("ok");
        }
    }
}


