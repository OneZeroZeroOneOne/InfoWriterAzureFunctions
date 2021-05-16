using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace InfoWriterAzureFunction.Database.Models
{
    public partial class StatusStorage
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public DateTime ChangeStatusTime { get; set; }
        public int DeviceId { get; set; }

        public virtual Device Device { get; set; }
        public virtual Status Status { get; set; }
    }
}
