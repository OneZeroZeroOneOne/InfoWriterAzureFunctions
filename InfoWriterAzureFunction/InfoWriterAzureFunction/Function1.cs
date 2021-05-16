using InfoWriterAzureFunction.Database.Context;
using InfoWriterAzureFunction.Sevices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InfoWriterAzureFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var deviceInfo = JsonConvert.DeserializeObject<DeviceInfo>(requestBody);
            var context = new PublicContext(Environment.GetEnvironmentVariable("databaseconnectionstring"));
            var deviceService = new DeviceService(context);
            var device = await deviceService.GetDevice(deviceInfo.compname, deviceInfo.osname);
            if(device != null)
            {
                await deviceService.UpdateDevice(device.Id, deviceInfo);
            }
            else
            {
                await deviceService.AddDevice(deviceInfo);
            }
            return new OkObjectResult("{\"status\": \"Ok\"}");
        }
    }
}
