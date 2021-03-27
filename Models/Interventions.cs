using System;
using System.Collections.Generic;

namespace BuildingApi.Models
{
    public partial class Interventions
    {
        public long id { get; set; }
        public long author { get; set; }
        public long customers_id { get; set; }
        public long building_id { get; set; }
        public long? batteries_id { get; set; }
        public long? columns_id { get; set; }
        public long? elevators_id { get; set; }
        public long? employees_id { get; set; }
        public DateTime? End_of_the_intervention { get; set; }
        public DateTime? Start_of_the_intervention { get; set; }
        public string Result { get; set; }
        public string Report { get; set; }
        public string Status { get; set; }

    }
}
