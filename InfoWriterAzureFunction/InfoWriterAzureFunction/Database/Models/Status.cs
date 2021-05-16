using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace InfoWriterAzureFunction.Database.Models
{
    public partial class Status
    {
        public Status()
        {
            StatusStorage = new HashSet<StatusStorage>();
        }

        public int Id { get; set; }
        public string Tittle { get; set; }

        public virtual ICollection<StatusStorage> StatusStorage { get; set; }
    }
}
