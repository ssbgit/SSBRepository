using Newtonsoft.Json;
namespace emrsn.com.fun.datalake
{
public class MessageHeader
    {
        [JsonProperty("MessageID")]
        public string MessageID { get; set; }

        [JsonProperty("Sender")]
        public Sender Sender { get; set; }

        [JsonProperty("Target")]
        public Target Target { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("BusinessObjectName")]
        public string BusinessObjectName { get; set; }

        [JsonProperty("MessageDateTime")]
        public string MessageDateTime { get; set; }
        [JsonProperty("FaultNotification")]
        public FaultNotification FaultNotification { get; set; }
    }

     public class Sender
    {
        [JsonProperty("ID")]
        public string ID { get; set; }
    }

    public class Target
    {
        [JsonProperty("ID")]
        public string ID { get; set; }
    }

   
}