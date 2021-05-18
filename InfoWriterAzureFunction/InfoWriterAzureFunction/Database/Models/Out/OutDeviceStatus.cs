using System;
using System.Collections.Generic;
using System.Text;

namespace InfoWriterAzureFunction.Database.Models.Out
{
    public class OutDeviceStatus
    {
        public string ComputerName { get; set; }
        public string OSName { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
