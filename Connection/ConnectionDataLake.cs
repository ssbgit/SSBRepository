using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Data.SqlClient;  
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;


  
  
namespace emrsn.com.fun.datalake 
{  
    public class ConnectionDataLake  
    {  
  
      string ConnectionString = System.Environment.GetEnvironmentVariable("SQL_POOL_CONN_PROD_BETSY"); 
  SqlConnection con;  
      // string ConnectionString = "Server=tcp:ws-eus-synapse-001.sql.azuresynapse.net ,1433;Initial Catalog=SQL_FLMC;Persist Security Info=False;User ID=FLMC_SQL_POOL_Reader;Password=Ncf9V2deEKrc0yzEGshn21dc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"; 

        public void OpenConection()  
        {  
            /*
            var _midObject=System.Environment.GetEnvironmentVariable("Managedidentity");
             string secretName = "SQL-POOL-CONN-PROD-BETSY";
            var keyVaultName = System.Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
            var kvUri = $"https://{keyVaultName}.vault.azure.net";           

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId =_midObject}));
            var secret = client.GetSecret(secretName);
            

        string _secretConnString=secret.Value.Value; 
*/
            
            con = new SqlConnection(ConnectionString);
            con.Open();  
        }  
  
  
        public void CloseConnection()  
        {  
            con.Close();  
        }  
  
  
        public void ExecuteQueries(string _sqlCommand)  
        {  
            SqlCommand cmd = new SqlCommand(_sqlCommand,con);  
            cmd.ExecuteNonQuery();  
        }  
  
  
        public SqlDataReader DataReader(string _sqlCommand,string InstanceID,string CustomerAccId,string FromDate,string ToDate,string OrderNumber,string CustomerPoNumber,string OrderStatusCode ,string OrderStatus,string OrderedFrom,string SerialNumber,string GSOrderNumber,string OrderedBy,string ActionType,string RecipientEmailId,string LanguageCode )  
        {  

             string _spParams=System.Environment.GetEnvironmentVariable("SP_INPUT_PARAMS"); 

            string[] _param=_spParams.Split(",");
            SqlCommand cmd = new SqlCommand(_sqlCommand,con);  
            SqlParameter param1 = new SqlParameter();
          //  param1.ParameterName = "@InstanceID ";
            param1.ParameterName = _param[0].ToString();
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Value = InstanceID;
            cmd.Parameters.Add(param1);

             SqlParameter param2 = new SqlParameter();
            param2.ParameterName =  _param[1].ToString();
            param2.SqlDbType = SqlDbType.VarChar;
            param2.Value = CustomerAccId;
            
            cmd.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName =  _param[2].ToString();
            param3.SqlDbType = SqlDbType.VarChar;
            param3.Value = FromDate;
            
            cmd.Parameters.Add(param3);

            SqlParameter param4 = new SqlParameter();
            param4.ParameterName =  _param[3].ToString();
            param4.SqlDbType = SqlDbType.VarChar;
            param4.Value = ToDate;
            
            cmd.Parameters.Add(param4);

            SqlParameter param5 = new SqlParameter();
            param5.ParameterName =  _param[4].ToString();
            param5.SqlDbType = SqlDbType.VarChar;
            param5.Value = OrderNumber;
            
            cmd.Parameters.Add(param5);
			
			SqlParameter param6 = new SqlParameter();
            param6.ParameterName =  _param[5].ToString();
            param6.SqlDbType = SqlDbType.VarChar;
            param6.Value = CustomerPoNumber;
            
            cmd.Parameters.Add(param6);
			
            SqlParameter param7 = new SqlParameter();
            param7.ParameterName =  _param[7].ToString();
            param7.SqlDbType = SqlDbType.VarChar;
            param7.Value = OrderStatus;
            
            cmd.Parameters.Add(param7);

            SqlParameter param8 = new SqlParameter();
            param8.ParameterName =  _param[8].ToString();
            param8.SqlDbType = SqlDbType.VarChar;
            param8.Value = OrderedFrom;
            
            cmd.Parameters.Add(param8);
			
            SqlParameter param9 = new SqlParameter();
            param9.ParameterName =  _param[9].ToString();
            param9.SqlDbType = SqlDbType.VarChar;
            param9.Value =SerialNumber;
            cmd.Parameters.Add(param9);
			
            SqlParameter param10 = new SqlParameter();
            param10.ParameterName =  _param[10].ToString();
            param10.SqlDbType = SqlDbType.VarChar;
            param10.Value = GSOrderNumber;
            cmd.Parameters.Add(param10);
			
			
			SqlParameter param11 = new SqlParameter();
            param11.ParameterName =  _param[11].ToString();
            param11.SqlDbType = SqlDbType.VarChar;
            param11.Value = OrderedBy;
            cmd.Parameters.Add(param11);
			
			
			
			SqlParameter param12 = new SqlParameter();
            param12.ParameterName =  _param[12].ToString();
            param12.SqlDbType = SqlDbType.VarChar;
            param12.Value = ActionType;
            cmd.Parameters.Add(param12);
			
			
			
			SqlParameter param13 = new SqlParameter();
            param13.ParameterName =  _param[13].ToString();
            param13.SqlDbType = SqlDbType.VarChar;
            param13.Value = RecipientEmailId;
             cmd.Parameters.Add(param13);
			
			
			
			SqlParameter param14 = new SqlParameter();
            param14.ParameterName =  _param[14].ToString();
            param14.SqlDbType = SqlDbType.VarChar;
            param14.Value = LanguageCode;
            cmd.Parameters.Add(param14);

            SqlParameter param15 = new SqlParameter();
            param15.ParameterName =  _param[15].ToString();
            param15.SqlDbType = SqlDbType.VarChar;
            param15.Value = "";
            cmd.Parameters.Add(param15); 


            SqlParameter param16 = new SqlParameter();
            param16.ParameterName =  _param[6].ToString();
            param16.SqlDbType = SqlDbType.VarChar;
            param16.Value = OrderStatusCode;
            cmd.Parameters.Add(param16); 


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout=3000;
            SqlDataReader dr = cmd.ExecuteReader();  
            return dr;  
        } 

         
  
        
    }  
}  