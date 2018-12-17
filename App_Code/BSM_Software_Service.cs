using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using Jayrock.Json;
using Jayrock.JsonRpc;
using Jayrock.JsonRpc.Web;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using BsmDatabaseObjects;



namespace BSM.SoftwareControl
{


    public class BSM_Software_Service : JsonRpcHandler
    {
        OracleConnection conn;


        public BSM_Software_Service()
        {
            conn = new OracleConnection();

            System.Configuration.Configuration rootWebConfig =
            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            System.Configuration.ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["BsmConnectionString"];
                conn.ConnectionString = connString.ConnectionString;
            }
        }


        [JsonRpcMethod("add_swver")]
        [JsonRpcHelp("新增版號")]
        public string add_swver(string name, string crypto_type, string swkey,string url)
        {
            conn.Open();
            string _result;
            string default_api_ver = "4";
            string _sql = "INSERT INTO MFG_SOFTWARE_FILES(FILE_NAME,SW_VER,SW_API_VER,SW_KEY,STATUS_FLG,CREATE_DATE,CRYPTO_TYPE,URL) " +
                "VALUES (:FILE_NAME,:SW_VER,:SW_API_VER,:SW_KEY,'R',sysdate,:CRYPTO_TYPE,:URL)";

            try
            {
                OracleCommand _cmd = new OracleCommand(_sql, conn);
                _cmd.Parameters.Add("FILE_NAME", name);
                try
                {

                    if (name.Substring(0, 2) == "LT")
                    {
                        default_api_ver = "5";
                    }
                    else
                    {
                        default_api_ver = "4";
                    }
                    _cmd.Parameters.Add("SW_VER", name);
                    _cmd.Parameters.Add("SW_API_VER", default_api_ver);
                    _cmd.Parameters.Add("SW_KEY", swkey);
                    _cmd.Parameters.Add("CRYPTO_TYPE", crypto_type);
                    _cmd.Parameters.Add("URL", url);
                    _cmd.ExecuteNonQuery();
                    _cmd.Dispose();
                }
                catch
            {
                _cmd.Dispose();
                }

                if (name.Substring(0, 3) != "tsm")
                {

                    string _sql2 = "begin :result:= bsm_cdi_service.add_swver(:p_name,:p_crypto_type,:p_swkey,:p_apiversion,:url); end;";
                    OracleCommand _cmd2 = new OracleCommand(_sql2, conn);

                    try
                    {

                        OracleParameter _param1 = new OracleParameter("p_name", OracleDbType.Varchar2, 256);
                        _param1.Direction = ParameterDirection.Input;
                        _param1.Value = name;
                        _cmd2.Parameters.Add(_param1);

                        OracleParameter _param2 = new OracleParameter("p_crypto_type", OracleDbType.Varchar2, 256);
                        _param2.Direction = ParameterDirection.Input;
                        _param2.Value = crypto_type;
                        _cmd2.Parameters.Add(_param2);

                        OracleParameter _param3 = new OracleParameter("p_swkey", OracleDbType.Varchar2, 256);
                        _param3.Direction = ParameterDirection.Input;
                        _param3.Value = swkey;
                        _cmd2.Parameters.Add(_param3);

                        OracleParameter _param6 = new OracleParameter("url", OracleDbType.Varchar2, 256);
                        _param6.Direction = ParameterDirection.Input;
                        _param6.Value = swkey;
                        _cmd2.Parameters.Add(_param6);

                        OracleParameter _param4 = new OracleParameter("p_apiversion", OracleDbType.Varchar2, 32);
                        _param4.Direction = ParameterDirection.Input;

                        if (name.Substring(1, 2) == "LT")
                        {
                            _param4.Value = "5";
                        }
                        else
                        {
                            _param4.Value = "4";
                        };
                        _cmd2.Parameters.Add(_param4);

                        _result = "";
                        OracleParameter _param5 = new OracleParameter("result", OracleDbType.Varchar2, 256);
                        _param5.Direction = ParameterDirection.ReturnValue;
                        _param5.Value = _result;
                        _cmd2.Parameters.Add(_param5);


                        _cmd2.ExecuteNonQuery();
                        _result = "0";
                    }
                    finally
                    {
                        _cmd2.Dispose();
                    }
                }
                else
                {
                    _result = "0";
                }

            }
            finally
            {
                conn.Close();
            }

            return _result;
        }

    }


        

 

}