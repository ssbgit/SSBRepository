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
  
     
  SqlConnection con;  

  
        public void OpenConection()  
        {  
            
            var _midObject=System.Environment.GetEnvironmentVariable("Managedidentity");
            string secretName = System.Environment.GetEnvironmentVariable("SECRET_NAME");
            var keyVaultName = System.Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
            var kvUri = $"https://{keyVaultName}.vault.azure.net";           

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId =_midObject}));
            var secret = client.GetSecret(secretName);

        string _secretConnString=secret.Value.Value;

            
            con = new SqlConnection(_secretConnString);
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
  
  
            public SqlDataReader DataReader(string _sqlCommand,string[] _sqlDbParams)
        {  

             string _spParams=System.Environment.GetEnvironmentVariable("SP_INPUT_PARAMS"); 

            string[] _param=_spParams.Split(",");
            SqlCommand cmd = new SqlCommand(_sqlCommand,con);  
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = _param[0].ToString();
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Value = _sqlDbParams[0];
            cmd.Parameters.Add(param1);

             SqlParameter param2 = new SqlParameter();
            param2.ParameterName =  _param[1].ToString();
            param2.SqlDbType = SqlDbType.VarChar;
            param2.Value = _sqlDbParams[1];
            
            cmd.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName =  _param[2].ToString();
            param3.SqlDbType = SqlDbType.VarChar;
            param3.Value = _sqlDbParams[2];
            
            cmd.Parameters.Add(param3);

            SqlParameter param4 = new SqlParameter();
            param4.ParameterName =  _param[3].ToString();
            param4.SqlDbType = SqlDbType.VarChar;
            param4.Value = _sqlDbParams[3];
            
            cmd.Parameters.Add(param4);

            SqlParameter param5 = new SqlParameter();
            param5.ParameterName =  _param[4].ToString();
            param5.SqlDbType = SqlDbType.VarChar;
            param5.Value = _sqlDbParams[4];
            
            cmd.Parameters.Add(param5);
			
			SqlParameter param6 = new SqlParameter();
            param6.ParameterName =  _param[5].ToString();
            param6.SqlDbType = SqlDbType.VarChar;
            param6.Value = _sqlDbParams[5];
            
            cmd.Parameters.Add(param6);
			
            SqlParameter param7 = new SqlParameter();
            param7.ParameterName =  _param[7].ToString();
            param7.SqlDbType = SqlDbType.VarChar;
            param7.Value = _sqlDbParams[7];
            
            cmd.Parameters.Add(param7);

            SqlParameter param8 = new SqlParameter();
            param8.ParameterName =  _param[8].ToString();
            param8.SqlDbType = SqlDbType.VarChar;
            param8.Value = _sqlDbParams[8];
            
            cmd.Parameters.Add(param8);
			
            SqlParameter param9 = new SqlParameter();
            param9.ParameterName =  _param[9].ToString();
            param9.SqlDbType = SqlDbType.VarChar;
            param9.Value =_sqlDbParams[9];
            cmd.Parameters.Add(param9);
			
            SqlParameter param10 = new SqlParameter();
            param10.ParameterName =  _param[10].ToString();
            param10.SqlDbType = SqlDbType.VarChar;
            param10.Value = _sqlDbParams[10];
            cmd.Parameters.Add(param10);
			
			
			SqlParameter param11 = new SqlParameter();
            param11.ParameterName =  _param[11].ToString();
            param11.SqlDbType = SqlDbType.VarChar;
            param11.Value = _sqlDbParams[11];
            cmd.Parameters.Add(param11);
			
			
			
			SqlParameter param12 = new SqlParameter();
            param12.ParameterName =  _param[12].ToString();
            param12.SqlDbType = SqlDbType.VarChar;
            param12.Value = _sqlDbParams[12];
            cmd.Parameters.Add(param12);
			
			
			
			SqlParameter param13 = new SqlParameter();
            param13.ParameterName =  _param[13].ToString();
            param13.SqlDbType = SqlDbType.VarChar;
            param13.Value = _sqlDbParams[13];
             cmd.Parameters.Add(param13);
			
			
			
			SqlParameter param14 = new SqlParameter();
            param14.ParameterName =  _param[14].ToString();
            param14.SqlDbType = SqlDbType.VarChar;
            param14.Value = _sqlDbParams[14];
            cmd.Parameters.Add(param14);

            SqlParameter param15 = new SqlParameter();
            param15.ParameterName =  _param[15].ToString();
            param15.SqlDbType = SqlDbType.VarChar;
            param15.Value = "";
            cmd.Parameters.Add(param15); 


            SqlParameter param16 = new SqlParameter();
            param16.ParameterName =  _param[6].ToString();
            param16.SqlDbType = SqlDbType.VarChar;
            param16.Value = _sqlDbParams[6];
            cmd.Parameters.Add(param16); 


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout=Int16.Parse(System.Environment.GetEnvironmentVariable("SP_TIMEOUT")); 
            SqlDataReader dr = cmd.ExecuteReader();  
            return dr;  
        } 
  
  
        
    }  
}  
