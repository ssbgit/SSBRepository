using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using emrsn.com.fun.datalake;
using System;

namespace emrsn.com.fun.datalake 
{
 public class SalesOrderHeader
        {
			[JsonProperty("OrderHeader")]
        public OrderHeader OrderHeader { get; set; }
	//	[JsonProperty("PaymentTerm")]
      //  public PaymentTerm PaymentTerm { get; set; }

}
 public class OrderHeader
    {
		[JsonProperty("HeaderId")]
        public string HeaderId { get; set; }
		[JsonProperty("OrderGID")]
        public string OrderGID { get; set; }
		[JsonProperty("OriginatingSystem")]
        public string OriginatingSystem { get; set; }
		[JsonProperty("CurrencyCode")]
        public string CurrencyCode { get; set; }
		[JsonProperty("ShippingProfile")]
        public ShippingProfile ShippingProfile { get; set; }
		[JsonProperty("CustomerPONbr")]
        public string CustomerPONbr { get; set; }
		
        [JsonProperty("OrderSource")]
        public string OrderSource { get; set; }
        [JsonProperty("OrgId")]
        public string OrgId { get; set; }
		[JsonProperty("OrderNbr")]
        public string OrderNbr { get; set; }
		[JsonProperty("OrderDate")]
        public DateTime OrderDate { get; set; }
		[JsonProperty("EBSOU")]
        public string EBSOU { get; set; }
		[JsonProperty("BookedBy")]
        public string BookedBy { get; set; }
		[JsonProperty("CreationDate")]
        public string CreationDate { get; set; }
		[JsonProperty("AssociatedAccount")]
        public string AssociatedAccount { get; set; }
		[JsonProperty("OrderStatus")]
        public string OrderStatus { get; set; }

    }

    public class PaymentTerm
    {
		[JsonProperty("Amount")]
        public Double Amount { get; set; }
    }
	 public class SalesOrder
    {
		[JsonProperty("MessageHeader")]
        public MessageHeader MessageHeader { get; set; }
		[JsonProperty("DataArea")]
         public List<DataArea> DataArea { get; set; }
    }
	    public class ShippingProfile
    {
		[JsonProperty("ShipCode")]
        public string ShipCode { get; set; }
    }



    
}