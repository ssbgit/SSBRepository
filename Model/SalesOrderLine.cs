    using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using emrsn.com.fun.datalake;
using System;

namespace emrsn.com.fun.datalake 
{
    public class SalesOrderSchedule
    {
        [JsonProperty("SalesOrderShipment")]
        public SalesOrderShipment SalesOrderShipment { get; set; }
            [JsonProperty("TotalAmount")]
         public string TotalAmount { get; set; }
      
         
    }

     public class SalesOrderShipment
    {
      [JsonProperty("TrackingNumber")]
        public string TrackingNumber { get; set; }
    }

    public class SalesOrderLine
    {
        [JsonProperty("SalesOrderSchedule")]
        public SalesOrderSchedule SalesOrderSchedule { get; set; }
    }
}