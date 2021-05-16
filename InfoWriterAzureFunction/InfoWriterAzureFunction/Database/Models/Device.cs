using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace InfoWriterAzureFunction.Database.Models
{
    public partial class Device
    {
        public Device()
        {
            StatusStorage = new HashSet<StatusStorage>();
        }

        public string ComputerName { get; set; }
        public string Osname { get; set; }
        public string DotNewVersion { get; set; }
        public string TimeZone { get; set; }
        public int Id { get; set; }

        public virtual ICollection<StatusStorage> StatusStorage { get; set; }
    }
}
