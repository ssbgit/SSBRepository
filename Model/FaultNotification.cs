using Newtonsoft.Json;
namespace emrsn.com.fun.datalake 
{
public class FaultNotification
    {
         [JsonProperty("BusinessComponentID")]
        public string BusinessComponentID { get; set; }
        [JsonProperty("ReportingDateTime")]
        public string ReportingDateTime { get; set; }
        [JsonProperty("CorrectiveAction")]
        public string CorrectiveAction { get; set; }
        [JsonProperty("FaultMessage")]
        public FaultMessage FaultMessage { get; set; }
    }
    public class FaultMessage
    {

        [JsonProperty("FaultCode")]
         public string FaultCode { get; set; }
         [JsonProperty("FaultDescription")]
        public string FaultDescription { get; set; }
          [JsonProperty("FaultSeverity")]
        public string FaultSeverity { get; set; }
        [JsonProperty("FaultTrace")]
        public string FaultTrace { get; set; }
         [JsonProperty("FaultCategory")]
        public string FaultCategory { get; set; }
    }
}