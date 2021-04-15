using System;
using System.Collections.Generic;

namespace BuildingApi.Models
{
    public partial class Smart_contracts
    {
        
        public long id { get; set; }
        public string transactionHash { get; set; }
        public string projectOfficeAddress { get; set; }
        public string materialProviderAddress { get; set; }
        public string solutionManufacturinAddress { get; set; }
        public string qualitySecurityHomologationAddress { get; set; }
        public int? blockNumber { get; set; }

    }
}
