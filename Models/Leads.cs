using System;
using System.Collections.Generic;

namespace BuildingApi.Models
{
    
    public partial class Leads
    {

    
        public long id { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string company_name { get; set; }
        public string project_name { get; set; }
        public string department { get; set; }
        public string project_description { get; set; }
        public string message { get; set; }
        public byte[] file_attachment { get; set; }
        public DateTime created_at { get; set; }
        public string filename { get; set; }
        public long? customers_id { get; set; }

        public virtual Customers customers { get; set; }
    }
}
