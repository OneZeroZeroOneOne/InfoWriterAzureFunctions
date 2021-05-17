using AutoMapper;
using InfoWriterAzureFunction.Database.Context;
using InfoWriterAzureFunction.Database.Models;
using InfoWriterAzureFunction.Database.Models.Out;
using InfoWriterAzureFunction.Sevices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace InfoWriterAzureFunction
{
    public static class ShowHistoryFunc
    {
        [FunctionName("ShowHistory")]
        public static async Task<List<OutDeviceStatus>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (req.Query.ContainsKey("compname") && req.Query.ContainsKey("osname"))
            {
                var compname = req.Query["compname"];
                var osname = req.Query["osname"];
                var a = Environment.GetEnvironmentVariable("databaseconnectionstring");
                var context = new PublicContext(Environment.GetEnvironmentVariable("databaseconnectionstring"));
                var deviceService = new DeviceService(context);
                var hystory = await deviceService.GetOnlineHystoryDevice(compname, osname);
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                });
                IMapper mapper = mapperConfig.CreateMapper();
                return mapper.Map<List<OutDeviceStatus>>(hystory);
            }
            else
            {
                throw new Exception("compname or osname is absend");
            }
            
        }
    }
}
