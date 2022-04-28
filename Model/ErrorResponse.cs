using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Data.SqlClient;  

  
namespace emrsn.com.fun.datalake 
{     
    public class Response
    {
        public DateTime DateTime { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseStatus { get; set; }
        public string RequestId { get; set; }
        public string ErrorFlag { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorTrace { get; set; }
        public string ActionRecommended { get; set; }
    }
}