using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.IO;
using Jayrock.Json;
using Jayrock.JsonRpc;
using Jayrock.JsonRpc.Web;
using Jayrock.Json.Conversion;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using BsmDatabaseObjects;
using System.Messaging;
using System.Text;
using log4net;
using log4net.Config;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System.Web.Security;

namespace BSM
{

    public class cht_coupon
    {
        public string coupon_id;
        public string serial_num;
        public string session_id;
        public int? status;
    }

    public class cht_coupon_db : cht_coupon
    {
        public string Id;
        public string mac_address;
        public string serial_id;

        public void dump(cht_coupon _t)
        {
            _t.coupon_id = this.coupon_id;
            _t.serial_num = serial_id;
            _t.session_id = session_id;
            _t.status = this.status;
        }
    }

    /// <summary>
    /// BSM_CDI_Service 的摘要描述
    /// </summary>
    /// 

    public class BSM_CDI_Service : JsonRpcHandler
    {
        OracleConnection conn;
        MessageQueue MsgQ_register;
        MessageQueue MsgQ_activate;
        MessageQueue MsgQ_tstar;
        static ILog logger;

        private string _MongoDbconnectionString;
        private MongoClient _Mongoclient;
        private MongoServer _MongoServer;
        private MongoDatabase _MongoDB;

        private string MongoDBConnectString;
        private string MongoDBConnectString_package;
        private string MongoDB_Database;
        private string IOS_URL;


        public BSM_CDI_Service()
        {
            logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            conn = new OracleConnection();

            System.Configuration.Configuration rootWebConfig =
            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            System.Configuration.ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["BsmConnectionString"];
                MongoDBConnectString = rootWebConfig.ConnectionStrings.ConnectionStrings["MongoDb"].ToString();
                MongoDBConnectString_package = rootWebConfig.ConnectionStrings.ConnectionStrings["MongoDb_Package"].ToString();
                MongoDB_Database = rootWebConfig.ConnectionStrings.ConnectionStrings["MongoDb_Database"].ToString();
                conn.ConnectionString = connString.ConnectionString;
            }

            MsgQ_register = new MessageQueue(".\\Private$\\bsm_cdi_register");
            MsgQ_tstar = new MessageQueue(".\\Private$\\tstar_order");

            //
            // Mongodb setting
            //
           // _MongoDbconnectionString = "mongodb://localhost";
            //_Mongoclient = new MongoClient(_MongoDbconnectionString);
           // _MongoServer = _Mongoclient.GetServer();
            //_MongoDB = _MongoServer.GetDatabase("CdiService");

        }

        public struct activate_msg
        {
            public string client_id;
            public string device_id;
            public string phone_no;
            public string activate_code;
            public string result_code;
            public string method;
            public Boolean send_passcode;
            public string notice;
            public string model_info;
        }

        /// <summary>
        /// 登錄
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_info"></param>
        /// <returns></returns>
        [JsonRpcMethod("register")]
        [JsonRpcHelp("Client 登錄")]
        public BSM_Result register(string client_id, string mobile_number, string device_id, string passcode,Boolean? send_passcode )
        {

            BSM_ClientInfo client_info = new BSM_ClientInfo();
            string activation_code = passcode;
            client_info.client_id = client_id;
            client_info.owner_phone_no = mobile_number;
            client_info.mac_address = device_id;

            BSM_Result _result;
            _result = new BSM_Result();
            BSM_ClientInfo _client_info = new BSM_ClientInfo();


            _result.result_code = "BSM-00000";
            _result.result_message = "OK";
            _result.message = (JsonObject)JsonConvert.Import("{\"subject\":\"\",\"body\":\"\"}");
            if (client_info.mac_address == null)
            {
                _result.result_code = "BSM-00103";
                _result.result_message = "未輸入MAC_ADDRESS";
                _result.client = client_info;
                return _result;
            }
            else
            {
                client_info.mac_address = client_info.mac_address.Replace(":", "");
                client_info.mac_address = client_info.mac_address.ToUpper();
            }

            // 檢查是否輸入電話號碼
            if (client_info.owner_phone_no == null)
            {
                _result.result_code = "BSM-00101";
                _result.result_message = "未輸入電話號碼";
                _result.client = client_info;
                return _result;
            }
            //檢查電話號碼是否為空號
            else if (client_info.owner_phone_no == "")
            {
                _result.result_code = "BSM-00101";
                _result.result_message = "電話號碼為空字串";
                _result.client = client_info;
                return _result;
            }
            activate_msg _ms = new activate_msg();

            _ms.method = "REGISTER";
            _ms.client_id = client_id;
            _ms.device_id = device_id;
            _ms.phone_no = mobile_number;
            _ms.activate_code = passcode;
            _ms.send_passcode =  send_passcode ??  true;

            System.Messaging.Message _msg = new System.Messaging.Message();
            _msg.Body = _ms;
            this.MsgQ_register.Send(_msg);

            logger.Info(JsonConvert.ExportToString(_result));
            return _result;
        }

        /// <summary>
        /// 登錄
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_info"></param>
        /// <returns></returns>
        [JsonRpcMethod("cdi_notice")]
        [JsonRpcHelp("cdi_notice")]
        public JsonObject notice(string client_id, string device_id, string mobile_number, string software_version,string user_agent, JsonObject notice)
        {

            JsonObject _result;
            _result = new JsonObject();
            try
            {
                activate_msg _ms = new activate_msg();

                _ms.method = "NOTICE";
                _ms.client_id = client_id;
                _ms.device_id = device_id;
                _ms.phone_no = mobile_number;
                _ms.notice = JsonConvert.ExportToString(notice);

                System.Messaging.Message _msg = new System.Messaging.Message();
                _msg.Body = _ms;
                this.MsgQ_register.Send(_msg);
                logger.Info(JsonConvert.ExportToString(_result));
            }
            finally
            {
                _result.Add("result_code", "BSM-00000");
                _result.Add("result_message","OK");
            }
            return _result;
        }


        [JsonRpcMethod("activate")]
        [JsonRpcHelp("Client 啟用")]

        public BSM_Result activate_q(string client_id, string device_id, string mobile_number, string model_info)
        {
            BSM_ClientInfo client_info = new BSM_ClientInfo();
           
            client_info.client_id = client_id;
            client_info.owner_phone_no = mobile_number;
            client_info.mac_address = device_id;

            BSM_Result _result;
            _result = new BSM_Result();
            BSM_ClientInfo _client_info = new BSM_ClientInfo();


            _result.result_code = "BSM-00000";
            _result.result_message = "OK";
            _result.message = (JsonObject)JsonConvert.Import("{\"subject\":\"\",\"body\":\"\"}");

           
            activate_msg _ms = new activate_msg();

            _ms.method = "ACTIVATE";
            _ms.client_id = client_id;
            _ms.device_id = device_id;
            _ms.phone_no = mobile_number;
            _ms.model_info = model_info;
           
            

            System.Messaging.Message _msg = new System.Messaging.Message();
            _msg.Body = _ms;
            this.MsgQ_register.Send(_msg);

            logger.Info(JsonConvert.ExportToString(_result));
            return _result;
        }


        /// <summary>
        /// 啟用
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Client_Info"></param>
        /// <param name="Activation_code"></param>
        /// <returns></returns>

        [JsonRpcMethod("activate_o")]
        [JsonRpcHelp("Client 啟用")]
        public BSM_Result activate(string client_id, string device_id, string mobile_number,string model_info)
        {

            BSM_ClientInfo client_info = new BSM_ClientInfo();
            string activation_code = "";
            string client_status = "";
            string device_status = "";
            string v_model_info;
            v_model_info = @"{""model_info"":"""+model_info+@"""}";

            client_info.client_id = client_id;
            client_info.owner_phone_no = mobile_number;
            client_info.mac_address = device_id;

            BSM_Result _result;
            _result = new BSM_Result();
            BSM_ClientInfo _client_info = new BSM_ClientInfo();
            if (client_info.mac_address == null)
            {
                _result.result_code = "BSM-00103";
                _result.result_message = "未輸入MAC_ADDRESS";
                _result.client = client_info;
                return _result;
            }
            else
            {
                client_info.mac_address = client_info.mac_address.Replace(":", "");
                client_info.mac_address = client_info.mac_address.ToUpper();

            }


            conn.Open();
            try
            {
                string _sql_acode = @"SELECT activation_code,A.status_flg,b.device_id,nvl(b.status_flg,'N') device_flg,b.device_id FROM BSM_CLIENT_MAS a,BSM_CLIENT_DEVICE_LIST b WHERE MAC_ADDRESS= :MAC_ADDRESS and b.client_id(+)=a.mac_address and b.device_id(+)=:DEVICE_ID";

                OracleCommand _cmd_acode = new OracleCommand(_sql_acode, conn);
                try
                {
                    _cmd_acode.Parameters.Add("MAC_ADDRESS", client_id);
                    _cmd_acode.Parameters.Add("DEVICE_ID", client_id);
                    OracleDataReader _reader = _cmd_acode.ExecuteReader();
                    if (_reader.Read())
                    {
                        activation_code = _reader.GetString(0);
                        client_status = _reader.GetString(1);
                        device_status = _reader.GetString(3);
                    }
                    else
                    {
                        activation_code = "";
                    }

                    _reader.Dispose();
                }
                finally
                {
                    _cmd_acode.Dispose();
                }

                    string sql1 = "begin :M_RESULT := BSM_CLIENT_SERVICE.Activate_Client(:M_CLIENT_INFO,:P_OPTIONS); end; ";

                    TBSM_CLIENT_INFO t_client_info = new TBSM_CLIENT_INFO();
                    TBSM_RESULT bsm_result = new TBSM_RESULT();

                    t_client_info.SERIAL_ID = client_info.client_id;
                    t_client_info.MAC_ADDRESS = client_info.mac_address;
                    t_client_info.OWNER_PHONE = client_info.owner_phone_no;
                    t_client_info.ACTIVATION_CODE = activation_code;

                    OracleCommand cmd = new OracleCommand(sql1, conn);
                    try
                    {
                        cmd.BindByName = true;

                        OracleParameter param1 = new OracleParameter();
                        param1.ParameterName = "M_CLIENT_INFO";
                        param1.OracleDbType = OracleDbType.Object;
                        param1.Direction = ParameterDirection.InputOutput;
                        param1.UdtTypeName = "TBSM_CLIENT_INFO";

                        param1.Value = t_client_info;

                        cmd.Parameters.Add(param1);

                        OracleParameter param3 = new OracleParameter();
                        param3.ParameterName = "P_OPTIONS";
                        param3.OracleDbType = OracleDbType.Varchar2;
                        param3.Direction = ParameterDirection.Input;
                        param3.Value = v_model_info;
                        cmd.Parameters.Add(param3);

                        OracleParameter param2 = new OracleParameter();
                        param2.ParameterName = "M_RESULT";
                        param2.OracleDbType = OracleDbType.Object;
                        param2.Direction = ParameterDirection.InputOutput;
                        param2.UdtTypeName = "TBSM_RESULT";

                        cmd.Parameters.Add(param2);
                        cmd.ExecuteNonQuery();

                        bsm_result = (TBSM_RESULT)param2.Value;
                        t_client_info = (TBSM_CLIENT_INFO)param1.Value;

                        _client_info.client_id = t_client_info.SERIAL_ID;
                        _client_info.region = (int)t_client_info.REGION;
                        _client_info.owner_phone_no = t_client_info.OWNER_PHONE;
                        _client_info.owner_phone_status = t_client_info.OWNER_PHONE_STATUS;
                        _client_info.mac_address = t_client_info.MAC_ADDRESS;
                        _client_info.client_status = t_client_info.STATUS_FLG;

                        if (bsm_result.result_code != "BSM-00000")
                        {
                            _result.result_code = bsm_result.result_code;
                            _result.result_message = bsm_result.result_message;
                            _result.client = _client_info;
                            return _result;
                        }
                        else
                        {
                            _result.result_code = bsm_result.result_code;
                            _result.result_message = bsm_result.result_message;
                            _result.client = _client_info;
                            if(bsm_result.result_message!= null)
                            if (bsm_result.result_message.IndexOf("message:") >= 0)
                            {
                                string p_msg_str = bsm_result.result_message.Replace("message:", "");
                                JsonObject p_message = (JsonObject)JsonConvert.Import(p_msg_str);
                                _result.message =  p_message;

                            }
                            _result.result_message = "OK";
                        }
                    }
                    finally
                    {
                        cmd.Dispose();
                    }


                    logger.Info(JsonConvert.ExportToString(_result));
            }
            finally
            {
                logger.Info(JsonConvert.ExportToString(_result));
                conn.Close();
                
            }
            return _result;
        }




        [JsonRpcMethod("promo_activate")]
        [JsonRpcHelp("啟用 Promo")]
        public string promo_activate(string promo_code, string client_id, string device_id) 
        {
            Dictionary<string, string> _res = new Dictionary<string, string>();
            _res["Failure"] = "F";

            if (client_id == null)
            {

                logger.Info(_res["Failure"]);
                return _res["Failure"];
            }

            if (promo_code == null)
            {
                logger.Info(_res["Failure"]);
                return _res["Failure"];
            }

            conn.Open();
            try
            {
                string sql1 = "begin :result := promo_activate(:promo_code, :client_id,:device_id); end; ";

                    OracleCommand cmd = new OracleCommand(sql1, conn);
                    try
                    {
                        string result;
                        cmd.BindByName = true;
                        cmd.Parameters.Add("PROMO_CODE", promo_code);
                        cmd.Parameters.Add("CLIENT_ID", client_id);
                        cmd.Parameters.Add("DEVICE_ID", device_id);

                        OracleParameter param_result = new OracleParameter();
                        param_result.ParameterName = "RESULT";
                        param_result.OracleDbType = OracleDbType.Varchar2;
                        param_result.Size = 32;
                        param_result.Direction = ParameterDirection.Output;
                       
                        cmd.Parameters.Add(param_result);
                        cmd.ExecuteNonQuery();
                        result = param_result.Value.ToString();
                        logger.Info(result);
                        return result;
                        
                    }
                    finally
                    {
                        cmd.Dispose();
                    }
            }
            finally
            {
                conn.Close();
            }
        }


        [JsonRpcMethod("get_chimei_ids")]
        [JsonRpcHelp("取奇美client id:start_date ,end_date,allow_type ('A' allow, 'D' deny) , range ('F' full, 'R' range) ")]
        public List<string> get_chimei_ids(DateTime start_date, DateTime end_date, string allow_type, string range)
        {
            conn.Open();
            List<string> result = new List<string>();
            string _sql = @"select '0000'||client_id client_id,allow_type from temp_chimei_ids where 1=1 ";

            if (allow_type == "A")
            {
                _sql = _sql + @" AND allow_type='A'";
            }
            else
            {
                _sql = _sql + @" AND allow_type='D'";
            }

            if (range != "F")
            {
                _sql = _sql + @" and to_char(create_date,'YYYY-MM-DD') >= '" + start_date.ToString("yyyy-MM-dd") + "' and  to_char(create_date,'YYYY-MM-DD') <= '" + end_date.ToString("yyyy-MM-dd") + "'";

            }
            OracleCommand cmd = new OracleCommand(_sql, conn);
            OracleDataReader _dr = cmd.ExecuteReader();

            while (_dr.Read())
            {
                result.Add(_dr.GetString(0));
            }
            conn.Close();

            logger.Info(JsonConvert.ExportToString(result));
            return result;
        }

  
        [JsonRpcHelp("Client 登錄")]

        public BSM_Result register_test(string client_id, string mobile_number, string device_id, string passcode)
        {
            BSM_Result _result = new BSM_Result();

            JsonRpcException jf;
            jf = new JsonRpcException("TEST Messgae");
            throw jf;
            return _result;
        }

        [JsonRpcMethod("get_account_info")]
        public BSM_Info.account_info get_account_info(string client_id)
        {
            BSM_Info.account_info _result;
            BSM_Info.BSM_Info_Service_base _base = new BSM_Info.BSM_Info_Service_base(conn.ConnectionString, MongoDBConnectString, MongoDBConnectString_package, MongoDB_Database);
            _result = _base.get_account_info(client_id,null);
            return _result;
        }


        [JsonRpcMethod("tstar_order")]
        [JsonRpcHelp("台灣之星訂單")]
        public JsonObject tstar_order(JsonObject p_tstar_order)
        {
            JsonObject _result = new JsonObject();
            string p_params;
            p_params = JsonConvert.ExportToString(p_tstar_order);

            conn.Open();
            try
            {
                string sql1 = "begin  :result := tstar_order_service.tstar_order(p_order => :p_order); end;";

                OracleCommand cmd = new OracleCommand(sql1, conn);
                try
                {
                    string result;
                    cmd.BindByName = true;
                    cmd.Parameters.Add("P_ORDER", p_params);


                    OracleParameter param_result = new OracleParameter();
                    param_result.ParameterName = "RESULT";
                    param_result.OracleDbType = OracleDbType.Varchar2;
                    param_result.Size = 1024;
                    param_result.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(param_result);
                    cmd.ExecuteNonQuery();
                    result = param_result.Value.ToString();
                    logger.Info(result);
                    _result = JsonConvert.Import<JsonObject>(new StringReader(result));

                }
                finally
                {
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
            }
            finally
            {
                conn.Close();
            }

            logger.Info(JsonConvert.ExportToString(_result));

            return _result;
        }


        [JsonRpcMethod("partner_service")]
        [JsonRpcHelp("合作廠商會員會員付款")]
        /*    "vendor":"bandott"
     "action": "create",
     "mobile": "0923123123",
     "client_id": "2A00417AE7225B4A",
     "name":"Star Tsai",
     "email":"star.tsai@tgc-taiwan.com.tw"
     "order_id": "BAN111111111111",
     "order_date":"2017/10/24 10:15:20"
     "software_group":"LTBND00",
     "package_id": "XD0001",
     "price":349,
     "pay_amount":299,
     "device_id":"001122334455",
     "action_date": "2017/10/24 10:15:20"
         */
        public JsonObject partner_service(
            string action,
            string mobile,
            string vendor,
            string client_id,
            string name,
            string email,
            string order_id,
            string order_date,
            string software_group,
            string package_id,
            int? price,
            int? pay_amount,
            string device_id,
            string action_date,
            string cancel_date,
            string expire_date,
            string orig_order_id,
            int? offset,
            string remark,
            Boolean? purge_flg)
        {
            JsonObject _result = new JsonObject();
            JsonObject _partner_order = new JsonObject();
            _partner_order.Add("action", action);
            _partner_order.Add("mobile", mobile);
            _partner_order.Add("vendor", vendor);
            _partner_order.Add("client_id", client_id);
            _partner_order.Add("name", name);
            _partner_order.Add("email", email);
            _partner_order.Add("order_id", order_id);
            _partner_order.Add("order_date", order_date);
            _partner_order.Add("software_group", software_group);
            _partner_order.Add("package_id", package_id);
            _partner_order.Add("price", price);
            _partner_order.Add("pay_amount", pay_amount);
            _partner_order.Add("device_id", device_id);
            _partner_order.Add("action_date", action_date);
            _partner_order.Add("cancel_date", cancel_date);
            _partner_order.Add("orig_order_id", orig_order_id);
            _partner_order.Add("expire_date", expire_date);
            _partner_order.Add("offset", offset);
            _partner_order.Add("remrk", remark);
            _partner_order.Add("purge_flg", purge_flg);

            string p_params;
            p_params = JsonConvert.ExportToString(_partner_order);

            conn.Open();
            try
            {
                string sql1 = "begin :result := partner_service.partner_order_service(p_order => :p_order); end;";

                OracleCommand cmd = new OracleCommand(sql1, conn);
                try
                {
                    string result;
                    cmd.BindByName = true;
                    cmd.Parameters.Add("P_ORDER", p_params);


                    OracleParameter param_result = new OracleParameter();
                    param_result.ParameterName = "RESULT";
                    param_result.OracleDbType = OracleDbType.Varchar2;
                    param_result.Size = 1024;
                    param_result.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(param_result);
                    cmd.ExecuteNonQuery();
                    result = param_result.Value.ToString();
                    logger.Info(result);
                    try
                    {
                        _result = JsonConvert.Import<JsonObject>(new StringReader(result));
                    }
                    catch (Exception e)
                    {
                        _result = new JsonObject();
                        _result.Add("result_code", "BSM-00803");
                        _result.Add("result_message", result);
                    }

                }
                finally
                {
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                _result = new JsonObject();
                _result.Add("result_code", "BSM-00803");
                _result.Add("result_message", e.Message);
            }
            finally
            {
                conn.Close();
            }

            logger.Info(JsonConvert.ExportToString(_result));

            return _result;
        }



        public JsonObject tstar_order_q(JsonObject p_tstar_order)
        {
            JsonObject _result = new JsonObject();
            string p_params;
            p_params = JsonConvert.ExportToString(p_tstar_order);


            activate_msg _ms = new activate_msg();

            System.Messaging.Message _msg = new System.Messaging.Message();
            _msg.Body = p_params;
            this.MsgQ_tstar.Send(_msg);

            logger.Info(JsonConvert.ExportToString(_result));

            _result = JsonConvert.Import<JsonObject>(new StringReader(@"{""result_code"":""BSM-00000"",""result_message"":""Success"", ""purchase_no"":"""", ""client_id"": """"}"));
           
            logger.Info(JsonConvert.ExportToString(_result));

            return _result;
        }

        [JsonRpcMethod("purchase")]
        [JsonRpcHelp("Client 購買")]
        public BSM_Result purchase(string token, string device_id, string sw_version, BSM_Purchase_Request purchase_info,string vendor_id)
        {
            BSM_Result result;
            BSM_Info.BSM_Info_Service_base BSM_Info_base = new BSM_Info.BSM_Info_Service_base(conn.ConnectionString, MongoDBConnectString, MongoDBConnectString_package, MongoDB_Database);

            string imsi = purchase_info.imsi;
            if (purchase_info.device_id != null && purchase_info.device_id != "")
            {
                device_id = purchase_info.device_id;
            }

            result = new BSM_Result();

            purchase_info.client_id = purchase_info.client_id.ToUpper();
            JsonObject _option = new JsonObject();

            if ((purchase_info.client_id == null) || (purchase_info.client_id == ""))
            {
                result.result_code = "BSM-00301";
                result.result_message = "未輸入ClientID";
                return result;
            }

            if ((purchase_info.pay_type == null) || (purchase_info.pay_type == ""))
            {
                result.result_code = "BSM-00304";
                result.result_message = "未輸入付款方式認證碼";
                return result;
            }


            if (purchase_info.pay_type == "APT" && (imsi == null || imsi == ""))
            {
                result.result_code = "BSM-00305";
                result.result_message = "沒有亞太帳號資料";
                return result;
            };


            if (purchase_info.pay_type == "IOS" && (purchase_info.ios_receipt_info == null || purchase_info.ios_receipt_info == ""))
            {
                result.result_code = "BSM-00306";
                result.result_message = "IOS沒有ios_receipt_info";

                return result;
            };

            _option.Add("ios_receipt_info", purchase_info.ios_receipt_info);

            if ((purchase_info.pay_type != "贈送") && (purchase_info.pay_type != "儲值卡") && (purchase_info.pay_type != "CREDITS") && (purchase_info.pay_type != "APT") && (purchase_info.pay_type != "IOS"))
            {
                if ((purchase_info.card_number == null) || (purchase_info.card_number == ""))
                {
                    result.result_code = "BSM-00302";
                    result.result_message = "未輸入卡號";
                    return result;
                }

                if ((purchase_info.card_type == null) || (purchase_info.card_type == ""))
                {
                    result.result_code = "BSM-00303";
                    result.result_message = "未輸入信用卡種類";
                    return result;
                }


                if ((purchase_info.card_expiry == null) || (purchase_info.card_expiry == ""))
                {
                    result.result_code = "BSM-00304";
                    result.result_message = "未輸入信用卡期限";
                    return result;
                }


                if ((purchase_info.cvc2 == null) || (purchase_info.cvc2 == ""))
                {
                    result.result_code = "BSM-00304";
                    result.result_message = "未輸入信用卡認證碼";
                    return result;
                }

                if (purchase_info.card_number.Substring(0, 1) != "4" && purchase_info.card_number.Substring(0, 1) != "5")
                {
                    result.result_code = "BSM-00307";
                    result.result_message = "信用卡卡號錯誤";
                    return result;
                }

                if ((purchase_info.card_type == "VISA" && purchase_info.card_number.Substring(0, 1) != "4") || (purchase_info.card_type == "MASTER" && purchase_info.card_number.Substring(0, 1) != "5"))
                {
                    result.result_code = "BSM-00306";
                    result.result_message = "信用卡種類錯誤";
                    return result;
                }
            }

            /* IOS 先確認是否購買成功  */

            

            conn.Open();
            string sql1 = "begin :M_RESULT := BSM_CLIENT_SERVICE.CRT_PURCHASE(:M_PURCHASE_INFO,:P_RECURRENT,:P_DEVICE_ID,:OPTION,:SW_VERSION); end; ";

            TBSM_PURCHASE bsm_purchase = new TBSM_PURCHASE();
            TBSM_PURCHASE_DTLS bsm_purchase_dtls = new TBSM_PURCHASE_DTLS();
            TBSM_RESULT bsm_result = new TBSM_RESULT();

            OracleCommand cmd = new OracleCommand(sql1, conn);
            bsm_purchase.SRC_NO = purchase_info.session_uid;
            bsm_purchase.SERIAL_ID = purchase_info.client_id;
            bsm_purchase.CVC2 = purchase_info.cvc2;
            bsm_purchase.CARD_EXPIRY = purchase_info.card_expiry;
            bsm_purchase.CARD_TYPE = purchase_info.card_type;
            bsm_purchase.CARD_NO = purchase_info.card_number;
            bsm_purchase.PAY_TYPE = purchase_info.pay_type;



            bsm_purchase_dtls.Value = new TBSM_PURCHASE_DTL[purchase_info.details.Length];
            for (int i = 0; i < purchase_info.details.Length; i++)
            {
                bsm_purchase_dtls.Value[i] = new TBSM_PURCHASE_DTL();
                bsm_purchase_dtls.Value[i].ASSET_ID = purchase_info.details[i].item_id;
                bsm_purchase_dtls.Value[i].OFFER_ID = purchase_info.details[i].package_id;
            }

            bsm_purchase.DETAILS = bsm_purchase_dtls;

            try
            {
                cmd.BindByName = true;

                OracleParameter param1 = new OracleParameter();
                param1.ParameterName = "M_PURCHASE_INFO";
                param1.OracleDbType = OracleDbType.Object;
                param1.Direction = ParameterDirection.InputOutput;
                param1.UdtTypeName = "TBSM_PURCHASE";
                param1.Value = bsm_purchase;
                cmd.Parameters.Add(param1);

                OracleParameter param2 = new OracleParameter();
                param2.ParameterName = "M_RESULT";
                param2.OracleDbType = OracleDbType.Object;
                param2.Direction = ParameterDirection.InputOutput;
                param2.UdtTypeName = "TBSM_RESULT";
                cmd.Parameters.Add(param2);

                OracleParameter param3 = new OracleParameter();
                param3.ParameterName = "P_RECURRENT";
                param3.Direction = ParameterDirection.Input;
                if (purchase_info.recurrent != null && purchase_info.recurrent != "")
                {
                    param3.Value = purchase_info.recurrent;
                }
                else
                {
                    param3.Value = "";
                }

                cmd.Parameters.Add(param3);

                OracleParameter param4 = new OracleParameter();
                param4.ParameterName = "P_DEVICE_ID";
                param4.Direction = ParameterDirection.Input;
                param4.Value = device_id;
                cmd.Parameters.Add(param4);

                if (imsi != null && imsi != "")
                {
                    _option.Add("min", imsi);
                }
                _option.Add("vendor_id", vendor_id);

                string option = JsonConvert.ExportToString(_option);

                OracleParameter param5 = new OracleParameter();
                param5.ParameterName = "OPTION";
                param5.Direction = ParameterDirection.Input;
                param5.Value = option;

                cmd.Parameters.Add(param5);

                OracleParameter param6 = new OracleParameter();
                param6.ParameterName = "SW_VERSION";
                param6.Direction = ParameterDirection.Input;
                param6.Value = sw_version;
                cmd.Parameters.Add(param6);

                cmd.ExecuteNonQuery();

                bsm_result = (TBSM_RESULT)param2.Value;
                bsm_purchase = (TBSM_PURCHASE)param1.Value;

                if (bsm_result.result_code != "BSM-00000")
                {
                    result.result_code = bsm_result.result_code;
                    result.result_message = bsm_result.result_message;
                    result.purchase_id = bsm_purchase.MAS_NO;
                }
                else
                {
                    result.result_code = bsm_result.result_code;
                    result.result_message = bsm_result.result_message;
                    result.purchase_id = bsm_purchase.MAS_NO;
                }

                if (!(result.purchase_id == null))
                {
                    result.purchase = BSM_Info_base.get_purchase_info_by_id(purchase_info.client_id, result.purchase_id);
                }
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }

            logger.Info(JsonConvert.ExportToString(result));

            return result;
            
        }

        public BSM_Result_Base dlc_order(bsm_order order, string action)
        {
            // Checks
            BSM_Result_Base _result = new BSM_Result_Base();

            if ((action == null) || (action == ""))
            {
                _result.result_code = "BSM-00704";
                _result.result_message = "action ERROR";
                return _result;
            }


            if ((order.mobile_number == null) || (order.mobile_number == ""))
            {
                _result.result_code = "BSM-00701";
                _result.result_message = "mobile_number";
                return _result;
            }

            if ((order.mac_address == null) || (order.mac_address == ""))
            {
                _result.result_code = "BSM-00702";
                _result.result_message = "error mac address";
                return _result;
            }

            if (order.packages_list == null)
            {
                _result.result_code = "BSM-00703";
                _result.result_message = "error packages list";
                return _result;
            }

            if (order.packages_list.Length == 0)
            {
                _result.result_code = "BSM-00703";
                _result.result_message = "error package list";
                return _result;
            }
            else
            {
                foreach (string package_id in order.packages_list)
                {
                    if (package_id == "")
                    {
                        _result.result_code = "BSM-00703";
                        _result.result_message = "error package";
                        return _result;

                    }
                }
            }


            conn.Open();
            string _sql = @"begin dlc_order_process(:p_order); end;";
            string _p_order_serial;

            _p_order_serial = JsonConvert.ExportToString(order);
            OracleCommand _cmd = new OracleCommand(_sql, conn);
            _cmd.BindByName = true;
            _cmd.Parameters.Add("P_ORDER", OracleDbType.Clob, _p_order_serial, ParameterDirection.Input);
            _cmd.ExecuteNonQuery();

            conn.Close();
            _result.result_code = "BSM-00000";
            _result.result_message = "";
            return _result;
        }

        [JsonRpcMethod("get_hichannel_coupon")]
        public cht_coupon get_hichannel_coupon(string device_id, string serial_num, string session_id,string platform_name, string sw_ver)
        {
            // “platform_name”:”InFocus|LIA21X-1"
            cht_coupon _result = new cht_coupon();

            if (platform_name != "InFocus|LIA21X-1")
            {
                JsonRpcException a = new JsonRpcException("error plat form");
                throw a;
            }
            

            cht_coupon_db _cache;

            var _collection_c = _MongoDB.GetCollection<cht_coupon_db>("catche_cht_coupon");
            var _qc = Query<cht_coupon_db>.EQ(e => e.Id, device_id);
            _cache = _collection_c.FindOne(_qc);


            conn.Open();
            try
            {
            if (device_id == null)
            {
                JsonRpcException a = new JsonRpcException("null device_id or serial_num");
                throw a;
            }

                string _sql_mac = @"SELECT COUPON_ID,SERIAL_ID,MAC_ADDRESS,STATUS_FLG,SESSION_ID FROM CHT_COUPON_MAS WHERE MAC_ADDRESS=:MAC_ADDRESS AND STATUS_FLG in (0,1,2,3,4)";

                OracleCommand _cmd_mac = new OracleCommand(_sql_mac, conn);
                _cmd_mac.BindByName = true;
                _cmd_mac.Parameters.Add("MAC_ADDRESS", device_id);
                OracleDataReader _rd = _cmd_mac.ExecuteReader();
                if (_rd.Read())
                {
                    _cache = new cht_coupon_db();
                    _cache.Id = device_id;
                    _cache.coupon_id = _rd.GetString(0);

                    if (!_rd.IsDBNull(4))
                    {
                        _cache.session_id = _rd.GetString(4);
                    }

                    if (!_rd.IsDBNull(1))
                    {
                        _cache.serial_id = _rd.GetString(1);
                    }
                        if (_cache.serial_id == serial_num)
                        {
                            if (session_id != null && session_id != _cache.session_id)
                            {
                                string _u_serial_num = @"UPDATE CHT_COUPON_MAS SET SESSION_ID=:SESSION_ID WHERE MAC_ADDRESS=:MAC_ADDRESS";
                                OracleCommand _cmd_book = new OracleCommand(_u_serial_num, conn);
                                _cmd_book.BindByName = true;
                                _cmd_book.Parameters.Add("MAC_ADDRESS", device_id);
                                _cmd_book.Parameters.Add("SESSION_ID", session_id);
                                _cmd_book.ExecuteNonQuery();
                                _cache.session_id = session_id;
                            }

                        }
                        else
                        {
                            if (serial_num != null && serial_num != "" && _cache.serial_id == null)
                            {

                                string _u_serial_num = @"UPDATE CHT_COUPON_MAS SET SERIAL_ID = NVL(SERIAL_ID,:SERIAL_ID) WHERE MAC_ADDRESS=:MAC_ADDRESS";
                                OracleCommand _cmd_book = new OracleCommand(_u_serial_num, conn);
                                _cmd_book.BindByName = true;
                                _cmd_book.Parameters.Add("SERIAL_ID", serial_num);
                                _cmd_book.Parameters.Add("MAC_ADDRESS", device_id);
                                _cmd_book.ExecuteNonQuery();
                                _cache.serial_id = serial_num;

                                if (session_id != null && session_id != _cache.session_id)
                                {
                                    _u_serial_num = @"UPDATE CHT_COUPON_MAS SET SESSION_ID=:SESSION_ID WHERE MAC_ADDRESS=:MAC_ADDRESS";
                                    _cmd_book = new OracleCommand(_u_serial_num, conn);
                                    _cmd_book.BindByName = true;
                                    _cmd_book.Parameters.Add("MAC_ADDRESS", device_id);
                                    _cmd_book.Parameters.Add("SESSION_ID", session_id);
                                    _cmd_book.ExecuteNonQuery();
                                    _cache.session_id = session_id;
                                }

                            }
                        }
                 

                     _cache.mac_address = _rd.GetString(2);
                     if (!_rd.IsDBNull(3))
                     {
                          _cache.status = (int)_rd.GetInt64(3);
                     }
                      _rd.Dispose();
                    }
                    else
                    {
                        
                        string _book_coupon_id = @"UPDATE CHT_COUPON_MAS SET MAC_ADDRESS=:MAC_ADDRESS,SERIAL_ID = NVL(SERIAL_ID,:SERIAL_ID) ,REGISTER_DATE=SYSDATE,STATUS_FLG=1 WHERE  MAC_ADDRESS is null and status_flg = 4 and rownum <= 1 ";
                        OracleCommand _cmd_book = new OracleCommand(_book_coupon_id, conn);
                        _cmd_book.BindByName = true;
                        _cmd_book.Parameters.Add("MAC_ADDRESS", device_id);
                        _cmd_book.Parameters.Add("SERIAL_ID", serial_num);
                        _cmd_book.ExecuteNonQuery();
                        if (serial_num != null && serial_num != "")
                        {

                            string _u_serial_num = @"UPDATE CHT_COUPON_MAS SET SERIAL_ID = NVL(SERIAL_ID,:SERIAL_ID) WHERE MAC_ADDRESS=:MAC_ADDRESS";
                            _cmd_book = new OracleCommand(_u_serial_num, conn);
                            _cmd_book.BindByName = true;
                            _cmd_book.Parameters.Add("SERIAL_ID", serial_num);
                            _cmd_book.Parameters.Add("MAC_ADDRESS", device_id);
                            _cmd_book.ExecuteNonQuery();

                            if (session_id != null )
                            {
                                _u_serial_num = @"UPDATE CHT_COUPON_MAS SET SESSION_ID=:SESSION_ID WHERE MAC_ADDRESS=:MAC_ADDRESS";
                                _cmd_book = new OracleCommand(_u_serial_num, conn);
                                _cmd_book.BindByName = true;
                                _cmd_book.Parameters.Add("MAC_ADDRESS", device_id);
                                _cmd_book.Parameters.Add("SESSION_ID", session_id);
                                _cmd_book.ExecuteNonQuery();
                            }

                        }

                        OracleDataReader _rd_2 = _cmd_mac.ExecuteReader();
                        if (_rd_2.Read())
                        {
                            _cache = new cht_coupon_db();
                            _cache.Id = device_id;
                            _cache.coupon_id = _rd_2.GetString(0);
                            if (!_rd_2.IsDBNull(1))
                            {
                                _cache.serial_id = _rd_2.GetString(1);
                            }

                            _cache.mac_address = _rd_2.GetString(2);
                            if (!_rd_2.IsDBNull(3))
                            {
                                _cache.status = (int)_rd_2.GetInt64(3);
                            }

                            if (!_rd_2.IsDBNull(4))
                            {
                                _cache.session_id = _rd_2.GetString(4);
                            }
                            _rd_2.Dispose();
                            _collection_c.Save(_cache);
                            _cache.status = 0;
                        }
                    }
                }

            
            catch(Exception e)
            {
              logger.Info(JsonConvert.ExportToString(e));
              throw e;
            }
            finally
            {
                conn.Close();
            }

            if (_cache != null)
            {
                _cache.dump(_result);
            } 

            logger.Info(JsonConvert.ExportToString(_result));

            return _result;
        }

        [JsonRpcMethod("set_hichannel_activation_status")]
        public int? set_hichannel_activation_status(string device_id, string serial_num, string sw_ver, string coupon,string session_id,string platform_name, int? status)
        {
            if (device_id == null|| coupon == null)
            {
                JsonRpcException a = new JsonRpcException("null device_id or serial_num");
                throw a;
            }

            if (status == null)
            {
                JsonRpcException a = new JsonRpcException("null status");
                throw a;
            }

            cht_coupon _result = new cht_coupon();

            cht_coupon_db _cache;
            var _collection_c = _MongoDB.GetCollection<cht_coupon_db>("catche_cht_coupon");

            conn.Open();
            try
            {

                string _book_coupon_id = @"UPDATE CHT_COUPON_MAS
   SET STATUS_FLG    = :STATUS
   ,SERIAL_ID     = NVL(SERIAL_ID,:SERIAL_ID) 
   ,SESSION_ID = NVL(:SESSION_ID,SESSION_ID)
 WHERE MAC_ADDRESS   = :MAC_ADDRESS
   AND COUPON_ID = :COUPON_ID";

                OracleCommand _cmd_book = new OracleCommand(_book_coupon_id, conn);
                _cmd_book.BindByName = true;
                _cmd_book.Parameters.Add("MAC_ADDRESS", device_id);
                _cmd_book.Parameters.Add("SERIAL_ID", serial_num);
                _cmd_book.Parameters.Add("COUPON_ID", coupon);
                _cmd_book.Parameters.Add("SESSION_ID", session_id);
                _cmd_book.Parameters.Add("STATUS", status);

                _cmd_book.ExecuteNonQuery();

                string _sql_mac = @"SELECT COUPON_ID,SERIAL_ID,MAC_ADDRESS,STATUS_FLG,SESSION_ID FROM CHT_COUPON_MAS WHERE MAC_ADDRESS=:MAC_ADDRESS AND STATUS_FLG in (0,1,2,3,4)";

                OracleCommand _cmd_mac = new OracleCommand(_sql_mac, conn);
                _cmd_mac.BindByName = true;
                _cmd_mac.Parameters.Add("MAC_ADDRESS", device_id);

                OracleDataReader _rd_2 = _cmd_mac.ExecuteReader();
                if (_rd_2.Read())
                {
                    _cache = new cht_coupon_db();
                    _cache.Id = device_id;
                    _cache.coupon_id = _rd_2.GetString(0);
                    _cache.serial_id = _rd_2.GetString(1);
                    _cache.mac_address = _rd_2.GetString(2);
                  
                    if (!_rd_2.IsDBNull(3))
                    {
                        _cache.status = (int)_rd_2.GetInt64(3);
                    }


                    if (!_rd_2.IsDBNull(4))
                    {
                        _cache.session_id = _rd_2.GetString(4);
                    }
                    _rd_2.Dispose();
                    _collection_c.Save(_cache);
                }

            }
            finally
            {
                conn.Close();
            }

            return 0;
        }

        public override void ProcessRequest(HttpContext context)
        {

            long pos = context.Request.InputStream.Position;

            var read = new StreamReader(context.Request.InputStream);
            string jsontstr = read.ReadToEnd();
            context.Request.InputStream.Position = pos;
            logger.Info(jsontstr);

            base.ProcessRequest(context);
        }
    }
}