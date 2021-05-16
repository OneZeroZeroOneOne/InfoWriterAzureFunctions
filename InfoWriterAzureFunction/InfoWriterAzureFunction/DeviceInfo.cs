using System;
using System.Collections.Generic;
using System.Text;

namespace InfoWriterAzureFunction
{
    public class DeviceInfo
    {
        public string osname { get; set; }
        public string timezone { get; set; }
        public string dotnetversion { get; set; }
        public string compname { get; set; }
        public int statusid { get; set; }
    }

}
