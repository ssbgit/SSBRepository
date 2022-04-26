 using Newtonsoft.Json;
 using System.IO;
using System.Collections.Generic;
namespace emrsn.com.fun.datalake 
{
 public class DataArea
    {
        [JsonProperty("SalesOrderHeader")]
        public List<SalesOrderHeader> SalesOrderHeader { get; set; }
        public List<SalesOrderLine> SalesOrderLine { get; set; }

        
    }
}