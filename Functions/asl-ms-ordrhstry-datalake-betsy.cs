using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;



namespace emrsn.com.fun.datalake
{
    internal class GetorderHistory
    {


        [FunctionName("asl-ms-ordrhstry-datalake-betsy")]
        
       [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header )]
        [OpenApiOperation(operationId: "GetOrderHistoryBetsy", tags: "List SalesOrders" )]
        [OpenApiParameter(name: "CustomerAccId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Customer Account Id Number")]
        [OpenApiParameter(name: "FromDate", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "From Date")]
        [OpenApiParameter(name: "ToDate", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "To Date")]
        [OpenApiParameter(name: "OrderNumber", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "")]
        [OpenApiParameter(name: "CustomerPoNumber", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Customer PO Number")]
        [OpenApiParameter(name: "OrderStatus", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "{ In Process, Cancelled,  Closed, Received, Booked,In Active, Approved } ")]
        [OpenApiParameter(name: "OrderedFrom", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Ordered From" )]
        [OpenApiParameter(name: "SerialNumber", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Serial Number ")]
        [OpenApiParameter(name: "GSOrderNumber", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "GS Order Number")]
        [OpenApiParameter(name: "OrderedBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Ordered By")]
        [OpenApiParameter(name: "ActionType", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Action Type Email ")]
        [OpenApiParameter(name: "RecipientEmailId", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Recipient Email Id")]
        [OpenApiParameter(name: "LanguageCode", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Language Code")]
        [OpenApiParameter(name: "SourceSystem", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Source System of Request")]
        
      
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrder), Description = "Successfully Completed")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(Response), Description = "Server Error")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response), Description = "Bad Request")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.MethodNotAllowed, contentType: "application/json", bodyType: typeof(Response), Description = "Operation is not allowed")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(Response), Description = "Not Found")]
         [OpenApiResponseWithBody(statusCode: HttpStatusCode.ServiceUnavailable, contentType: "application/json", bodyType: typeof(Response), Description = "Service Unavailable")]
        public async Task<IActionResult> GetOrderHistoryBetsy(
            [HttpTrigger(AuthorizationLevel.Function, "get",Route = "asl-ms-ordrhstry-datalake-betsy/GetOrderHistoryBetsy")] HttpRequest req, ILogger log)
        {
             ConnectionDataLake _obCOnnection=new ConnectionDataLake();
             GetOrderHistoryResponse _response = new GetOrderHistoryResponse();
             Guid _transactionId =Guid.NewGuid();
            /*
            Initialising business fault variables
            */

           
                    string _InstanceID= System.Environment.GetEnvironmentVariable("INSTANCE_ID_BETSY"); 
                    string _CustomerAccId= req.Query["CustomerAccId"];
                    string _FromDate= req.Query["FromDate"];
                    string _ToDate= req.Query["ToDate"];
                    string _OrderNumber= req.Query["OrderNumber"];
                    string _CustomerPoNumber= req.Query["CustomerPoNumber"];
                    string _OrderStatus= req.Query["OrderStatusCode"];   
                    string _OrderStatusCode=  req.Query["OrderStatus"];               
                    string _OrderedFrom= req.Query["OrderedFrom"];
                    string _SerialNumber= req.Query["SerialNumber"];
                    string _GSOrderNumber= req.Query["GSOrderNumber"];
                    string _OrderedBy= req.Query["OrderedBy"];
                    string _ActionType= req.Query["ActionType"];
                    string _RecipientEmailId= req.Query["RecipientEmailId"];
                    string _LanguageCode= req.Query["LanguageCode"];
                    string _SourceSystem= req.Query["SourceSystem"];

string inputParams=_InstanceID + ","+ _CustomerAccId + "," +_FromDate + "," +_ToDate + "," +_OrderNumber + "," +_CustomerPoNumber + "," +_OrderStatusCode + "," + _OrderStatus+ "," + _OrderedFrom+ "," + _SerialNumber+ "," +_GSOrderNumber + "," +_OrderedBy + "," +_ActionType + "," +_RecipientEmailId + "," +_LanguageCode ;

              string[] _sqlDbParams=inputParams.Split(",");
            SalesOrder _obSalesOrder=new SalesOrder();
           
              List<DataArea> _lstDataArea =new List<DataArea>();
              FaultNotification _obMsFaultNotification = new FaultNotification();
              FaultMessage _obFaultMessage=new FaultMessage();            

            log.LogInformation($"********Started Order History Microservice with  Trnsaction Id = {_transactionId} , ****************");   
            log.LogInformation($"******** Trnsaction Id = {_transactionId} , Input Query Params : #InstanceID, #CustomerAccId, #FromDate, #ToDate, #OrderNumber, #CustomerPoNumber, # OrderStatusCode, #OrderStatus, #OrderedFrom, #SerialNumber, #GSOrderNumber, #OrderedBy, #ActionType, #RecipientEmailId, #LanguageCode, #SourceSystem  ****");   
            log.LogInformation($"******** Trnsaction Id = {_transactionId} , Values : # { _InstanceID } ,# { _CustomerAccId } ,# { _FromDate } ,# { _ToDate } ,# { _OrderNumber } ,# { _CustomerPoNumber } ,# { _OrderStatus } ,# { _OrderStatusCode } ,# { _OrderedFrom } ,# { _SerialNumber } ,# { _GSOrderNumber } ,# { _OrderedBy } ,# { _ActionType } ,# { _RecipientEmailId } ,# { _LanguageCode } ,# { _SourceSystem }  ****");               
              try{         
            _obCOnnection.OpenConection();   

             string _microserviceName=System.Environment.GetEnvironmentVariable("MS_INTERFACE_BETSY"); 
            string _msBusinessFaultCode=System.Environment.GetEnvironmentVariable("BUSINESS_FAULT_CODE"); 
            string _msBusinessFault ="BUSINESS_FAULT"; 

              
           string _statement =System.Environment.GetEnvironmentVariable("SQL_CMD_ORERHISTORY_BETSY"); 
          
           
 SqlDataReader _reader =_obCOnnection.DataReader(_statement,_sqlDbParams);

                while (_reader.Read())
                {
                    if(_reader["ErrorFlag"].ToString()!="Y")
                    {                                 
           
             List<OrderHeader> _lsOrderHeader =new List<OrderHeader>();
             List<PaymentTerm> _lstPaymentTerm=new List<PaymentTerm>();
             List<SalesOrderHeader> _lstSalesOrderHeader=new List<SalesOrderHeader>();
             List<SalesOrderLine> _lstSalesOrderLine=new List<SalesOrderLine>();
             
   OrderHeader _obOrderHeader=new OrderHeader();
              
               _obOrderHeader.OriginatingSystem= _reader["SourceSystem"].ToString();		
               _obOrderHeader.OrderGID=_reader["GOSNumber"].Equals(System.DBNull.Value)? null : _reader["GOSNumber"].ToString();		
               _obOrderHeader.OrgId= _reader["OrganizationID"].ToString();		
               _obOrderHeader.CustomerPONbr= _reader["CustomerPONumber"].ToString();
               _obOrderHeader.OrderNbr= _reader["OrderNumber"].ToString();
               _obOrderHeader.HeaderId= _reader["HeaderID"].ToString();
               _obOrderHeader.OrderDate=Convert.ToDateTime(_reader["OrderDate"]);
               _obOrderHeader.CurrencyCode=_reader["OrderCurrencyCode"].ToString();
               _obOrderHeader.OrderStatus= _reader["GroupedOrderStatus"].ToString();
               _obOrderHeader.EBSOU= _reader["OperatingUnit"].ToString();
               _obOrderHeader.BookedBy=_reader["SoldToContactID"].Equals(System.DBNull.Value)? null : _reader["SoldToContactID"].ToString();  
               _obOrderHeader.OrderSource=_reader["OrganizationID"].ToString();		
              _obOrderHeader.AssociatedAccount=_CustomerAccId;

         ShippingProfile _obSHippingProfile=new ShippingProfile();
                         _obSHippingProfile.ShipCode= _reader["ShippingMethodDescription"].Equals(System.DBNull.Value)?  null : _reader["ShippingMethodDescription"].ToString();
              
               _obOrderHeader.ShippingProfile=_obSHippingProfile;


                            SalesOrderSchedule _obSalesOrderSchedule=new SalesOrderSchedule();
                                               _obSalesOrderSchedule.TotalAmount=_reader["ExtendedPriceDC"].Equals(System.DBNull.Value)?  "0" : _reader["ExtendedPriceDC"].ToString();

                                               SalesOrderShipment _obSalesOrderShipment=new SalesOrderShipment();
                                                                  _obSalesOrderShipment.TrackingNumber=_reader["TrackingNumber"].Equals(System.DBNull.Value)?  null : _reader["TrackingNumber"].ToString();

                                              _obSalesOrderSchedule.SalesOrderShipment=_obSalesOrderShipment;
                          SalesOrderLine _obSalesOrderLine=new SalesOrderLine();
                                        _obSalesOrderLine.SalesOrderSchedule=_obSalesOrderSchedule;


                SalesOrderHeader _obSalesOrderHeader = new SalesOrderHeader();                   
                                _obSalesOrderHeader.OrderHeader=_obOrderHeader;
                           

                _lstSalesOrderHeader.Add(_obSalesOrderHeader);
                _lstSalesOrderLine.Add(_obSalesOrderLine);
                          
                          
                            DataArea _obDataArea=new DataArea();
                           _obDataArea.SalesOrderHeader=_lstSalesOrderHeader;
                           _obDataArea.SalesOrderLine=_lstSalesOrderLine;
                          _lstDataArea.Add(_obDataArea);

                }
                else 
                {
                        _obMsFaultNotification.BusinessComponentID=_microserviceName; 
                        _obMsFaultNotification.ReportingDateTime=DateTime.Now.ToString();
                                _obFaultMessage.FaultCode=_msBusinessFaultCode; 
                                _obFaultMessage.FaultCategory=_msBusinessFault; 
                                _obFaultMessage.FaultDescription=_reader["ErrorMessage"].ToString();
                        _obMsFaultNotification.FaultMessage=_obFaultMessage;
                        _obMsFaultNotification.CorrectiveAction="Y";

                }
             }
 
                       _obCOnnection.CloseConnection();  
                      }
                catch (Exception ex)
                {
                        _obCOnnection.CloseConnection();

                         log.LogInformation($"********  Transaction Id = {_transactionId} ,  Error  Details  = {ex.Message.ToString()}  ****************");   
                          log.LogInformation($"********End of the Microservice with Error Transaction Id = {_transactionId}  ****************");   
                           Response _obResponse=new Response();
                                  _obResponse.ErrorCode=System.Environment.GetEnvironmentVariable("TECHNICAL_FAULT_CODE"); 
                                  _obResponse.ErrorTrace=ex.StackTrace.ToString();
                                  _obResponse.DateTime=DateTime.Now;
                                  _obResponse.ErrorMessage="TECHNICAL_FAULT"; 
                                  _obResponse.ErrorTrace=ex.Message.ToString();
                                  _obResponse.ResponseCode="500";
                                  _obResponse.ResponseStatus="TECHNICAL_FAULT"; 
                                  _obResponse.RequestId=_transactionId.ToString();
                                  _obResponse.ActionRecommended=System.Environment.GetEnvironmentVariable("TECHNICAL_FAULT_ACTION"); 

                           return new OkObjectResult(JsonConvert.SerializeObject(_obResponse, Formatting.Indented));   
                  }

                  Sender _obSender=new Sender();
                         _obSender.ID=System.Environment.GetEnvironmentVariable("SQL_POOL_DB_SOURCE_System");
                  Target _obTarget=new Target();
                         _obTarget.ID= (_SourceSystem == null)  ? System.Environment.GetEnvironmentVariable("TARGET_ID") : _SourceSystem ;

                  MessageHeader _obMessageHeader=new MessageHeader();
                                _obMessageHeader.MessageID=_transactionId.ToString();
                                _obMessageHeader.Description=System.Environment.GetEnvironmentVariable("HEADER_BUSINESS_DESCRIPTION"); 
                                _obMessageHeader.BusinessObjectName=System.Environment.GetEnvironmentVariable("BUSINESS_OBJECT_NAME"); 
                                _obMessageHeader.Sender=_obSender;
                                _obMessageHeader.Target=_obTarget;
                                _obMessageHeader.MessageDateTime=DateTime.Now.ToString();
                                _obMessageHeader.FaultNotification=_obMsFaultNotification;

                           _obSalesOrder.DataArea=_lstDataArea;
                           _obSalesOrder.MessageHeader=_obMessageHeader;


                _response.SalesOrder=_obSalesOrder;
          

          
            log.LogInformation($"********End of the Microservice Response Transaction Id = {_transactionId}  ****************");   
            return new OkObjectResult(JsonConvert.SerializeObject(_response, Formatting.Indented));
        }

         public class GetOrderHistoryResponse
    {
        [JsonProperty("SalesOrder")]
        public SalesOrder SalesOrder { get; set; }
    }
    }
}

 