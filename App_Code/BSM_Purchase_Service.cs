using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Threading;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
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
using System.Web.Security;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;


namespace BSM
{

    public class ios_receipt_info
    {
        public string _id;
        public string ticket_uid;
        public string password;
        public string ios_product_code;
        public string client_id;
        public string package_id;
        public string receipt;
    }


    public class IOS_ReceiptInfo
    {
        public string _id;
        public string original_purchase_date_pst { get; set; }
        public string quantity { get; set; }
        public string unique_vendor_identifier { get; set; }
        public string bvrs { get; set; }
        public string expires_date_formatted { get; set; }
        public string is_in_intro_offer_period { get; set; }
        public string purchase_date_ms { get; set; }
        public string expires_date_formatted_pst { get; set; }
        public string is_trial_period { get; set; }
        public string item_id { get; set; }
        public string unique_identifier { get; set; }
        public string original_transaction_id { get; set; }
        public string expires_date { get; set; }
        public string app_item_id { get; set; }
        public string transaction_id { get; set; }
        public string web_order_line_item_id { get; set; }
        public string version_external_identifier { get; set; }
        public string product_id { get; set; }
        public string purchase_date { get; set; }
        public string original_purchase_date { get; set; }
        public string purchase_date_pst { get; set; }
        public string bid { get; set; }
        public string original_purchase_date_ms { get; set; }
    }

    public class IOS_Result
    {
        public int? auto_renew_status { get; set; }
        public int? status { get; set; }
        public string auto_renew_product_id { get; set; }
        public IOS_ReceiptInfo receipt { get; set; }
        public IOS_ReceiptInfo latest_receipt_info { get; set; }
        public object latest_receipt { get; set; }
    }

    public class BSM_Purchase_Service : JsonRpcHandler
    {
        OracleConnection conn;
        MessageQueue MsgQ_Active;
        MessageQueue MsgQ_Purchase;
        public string PayGatewayInfo;
        static ILog logger;
        private string MongoDBConnectString;
        private string MongoDBConnectString_package;
        private string IOS_URL;
        private string MongoDB_Database;
        private string MongoDB;

        public BSM_Purchase_Service()
        {
            logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            conn = new OracleConnection();

            System.Configuration.Configuration rootWebConfig =
            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            System.Configuration.ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["BsmConnectionString"];
                conn.ConnectionString = connString.ConnectionString;
                PayGatewayInfo = rootWebConfig.AppSettings.Settings["PayGatewayInfo"].Value;
                IOS_URL = rootWebConfig.ConnectionStrings.ConnectionStrings["IOSUrl"].ToString();

                MongoDBConnectString = rootWebConfig.ConnectionStrings.ConnectionStrings["MongoDb"].ToString();
                MongoDBConnectString_package = rootWebConfig.ConnectionStrings.ConnectionStrings["MongoDb_package"].ToString();

                MongoDB_Database = rootWebConfig.ConnectionStrings.ConnectionStrings["MongoDb_Database"].ToString();
                MongoDB = MongoDB_Database + "ClientInfo";
            }

            MsgQ_Active = new MessageQueue(".\\Private$\\bsm_client_activate");
            MsgQ_Purchase = new MessageQueue(".\\Private$\\bsm_client_purchase");
        }



        /// <summary>
        /// 登錄
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_info"></param>
        /// <returns></returns>
        [JsonRpcMethod("register")]
        [JsonRpcHelp("Client 登錄")]
        public BSM_Result register(string token, BSM_ClientInfo client_info)
        {
            BSM_Result _result;
            _result = new BSM_Result();
            BSM_ClientInfo _client_info = new BSM_ClientInfo();

            _result.result_code = "BSM-00000";
            _result.result_message = "OK";

            //Check MAC can't null
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
            //檢查電話號碼是否為行動電話
            else if (client_info.owner_phone_no.Substring(0, 2) != "09")
            {

                _result.result_code = "BSM-00102";
                _result.result_message = "電話號碼不是行動電話號碼";
                _result.client = client_info;
                return _result;
            }

            // 更新資料庫項目

            conn.Open();
            string sql1 = "begin :M_RESULT := BSM_CLIENT_SERVICE.Check_And_Register_Client(:M_client_info); end; ";

            TBSM_CLIENT_INFO t_client_info = new TBSM_CLIENT_INFO();
            TBSM_RESULT bsm_result = new TBSM_RESULT();

            t_client_info.SERIAL_ID = client_info.client_id;
            t_client_info.MAC_ADDRESS = client_info.mac_address;
            t_client_info.OWNER_PHONE = client_info.owner_phone_no;


            OracleCommand cmd = new OracleCommand(sql1, conn);
            try
            {
                cmd.BindByName = true;

                OracleParameter param1 = new OracleParameter();
                param1.ParameterName = "M_client_info";
                param1.OracleDbType = OracleDbType.Object;
                param1.Direction = ParameterDirection.InputOutput;
                param1.UdtTypeName = "TBSM_CLIENT_INFO";
                param1.Value = t_client_info;
                cmd.Parameters.Add(param1);

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
                    _result.client = client_info;
                    return _result;
                }
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }


            return _result;
        }


        /// <summary>
        /// 啟用
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Client_Info"></param>
        /// <param name="Activation_code"></param>
        /// <returns></returns>
        /// 
        public struct activate_msg
        {
            public string client_id;
            public string device_id;
            public string phone_no;
            public string activate_code;
            public string result_code;
        }

        public struct purchase_ms
        {
            public string client_id;
            public string device_id;
            public string method;
            public string p_params;
        }

        [JsonRpcMethod("activate")]
        [JsonRpcHelp("Client 啟用")]
        public BSM_Result activate(string token, BSM_ClientInfo client_info, string activation_code)
        {
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


            // 檢查啟用碼是否為空值
            if (activation_code == "")
            {
                _result.result_code = "BSM-00201";
                _result.result_message = "未輸入Activation Code null";
                _result.client = client_info;
                return _result;

            }

            conn.Open();
            string sql1 = "begin :M_RESULT := BSM_CLIENT_SERVICE.Activate_Client(:M_CLIENT_INFO); end; ";

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
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }

            _result.result_code = bsm_result.result_code;
            _result.result_message = bsm_result.result_message;
            _result.client = _client_info;

            // send mesage to MSMQ

            activate_msg _ms = new activate_msg();
            _ms.client_id = client_info.client_id;
            _ms.device_id = client_info.mac_address;
            _ms.phone_no = client_info.owner_phone_no;
            _ms.result_code = _result.result_code;
            System.Messaging.Message _msg = new System.Messaging.Message();
            _msg.Body = _ms;
            this.MsgQ_Active.Send(_msg);

            return _result;
        }

        [JsonRpcMethod("purchase_t")]
        [JsonRpcHelp("Client 購買")]
        public BSM_Result purchase_test(string token, string device_id, string sw_version, BSM_Purchase_Request purchase_info)
        {
            BSM_Result result;
            result = new BSM_Result();
            return result;

        }

        /// <summary>
        /// 購買
        /// </summary>
        /// <param name="token"></param>
        /// <param name="purchase_info"></param>
        /// <returns></returns>
        [JsonRpcMethod("purchase")]
        [JsonRpcHelp("Client 購買")]
        public BSM_Result purchase(string token, string device_id, string sw_version, BSM_Purchase_Request purchase_info, string vendor_id)
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
            _option.Add("order", purchase_info.order);
            if (purchase_info.promote_code != "" && !(purchase_info.promote_code == null))
            { _option.Add("promo_code", purchase_info.promote_code); }
            else
            {
                _option.Add("promo_code", purchase_info.promo_code);
            }

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

            if (purchase_info.pay_type == "IOS")
            {


                string ticket = JsonConvert.ExportToString(purchase_info.ios_receipt_info);
                string ticket_uid = FormsAuthentication.HashPasswordForStoringInConfigFile(ticket, "MD5");
                long _purchase_pk_no = 0;

                JsonObject ios_result = _ios_verifyReceipt(null, purchase_info.ios_receipt_info, "c7cdbd0220b54ab99af16548b0f27733");
                JsonObject _receipt = new JsonObject();
                IOS_ReceiptInfo _rec = new IOS_ReceiptInfo();
                List<IOS_ReceiptInfo> _rec_l = new List<IOS_ReceiptInfo>();

                if (ios_result["status"].ToString() == "21007")
                {
                    ios_result = _ios_verifyReceipt_sandbox(null, purchase_info.ios_receipt_info, "c7cdbd0220b54ab99af16548b0f27733");
                    _receipt = (JsonObject)ios_result["receipt"];
                }

                if (ios_result["status"].ToString() == "0")
                {
                    if (ios_result["latest_receipt_info"] != null)
                    {
                        try
                        {
                            JsonArray _ja = (JsonArray)ios_result["latest_receipt_info"];
                            foreach (JsonObject _j in _ja)
                            {
                                _rec = new IOS_ReceiptInfo();
                                _rec._id = _j["transaction_id"].ToString();
                                _rec.transaction_id = _j["transaction_id"].ToString();
                                _rec.is_in_intro_offer_period = _j["is_in_intro_offer_period"].ToString();
                                _rec.original_transaction_id = _j["original_transaction_id"].ToString();
                                _rec.product_id = _j["product_id"].ToString();
                                _rec.purchase_date = _j["purchase_date"].ToString();
                                _rec.expires_date = _j["expires_date"].ToString();
                                _rec.expires_date_formatted = _j["expires_date"].ToString();
                                _rec.is_trial_period = _j["is_trial_period"].ToString();
                                _rec.original_purchase_date = _j["original_purchase_date"].ToString();
                                _rec_l.Add(_rec);
                            }
                        }catch(Exception e)
                        {
                        }

                    }
                   
                    if (ios_result["receipt"] != null)
                    {
                        _receipt = (JsonObject) ios_result["receipt"];
                        if (_receipt["in_app"] != null)
                        {
                            JsonArray _ja = (JsonArray) _receipt["in_app"];
                            foreach (JsonObject _j in _ja)
                            {
                                _rec = new IOS_ReceiptInfo();
                                _rec._id = _j["transaction_id"].ToString();
                                _rec.transaction_id = _j["transaction_id"].ToString();
                                _rec.is_in_intro_offer_period = _j["is_in_intro_offer_period"].ToString();
                                _rec.original_transaction_id = _j["original_transaction_id"].ToString();
                                _rec.product_id = _j["product_id"].ToString();
                                _rec.purchase_date = _j["purchase_date"].ToString();
                                _rec.expires_date = _j["expires_date"].ToString();
                                _rec.expires_date_formatted = _j["expires_date"].ToString();
                                _rec.is_trial_period = _j["is_trial_period"].ToString();
                                _rec.original_purchase_date = _j["original_purchase_date"].ToString();
                                _rec_l.Add(_rec);
                            }
                        }
                        else
                        {
                            if (ios_result["latest_receipt_info"] != null)
                            {
                                JsonArray _ja;
                                try
                                {
                                    _receipt = (JsonObject)ios_result["latest_receipt_info"];
                                }
                                catch (Exception e)
                                {
                                    _receipt = null;
                                }

                                try
                                {
                                    _ja = (JsonArray)_receipt["latest_receipt_info"];
                                    if (_ja != null)
                                    {
                                        foreach (JsonObject _j in _ja)
                                        {
                                            _rec = new IOS_ReceiptInfo();
                                            _rec._id = _j["transaction_id"].ToString();
                                            _rec.transaction_id = _j["transaction_id"].ToString();
                                            _rec.is_in_intro_offer_period = _j["is_in_intro_offer_period"].ToString();
                                            _rec.original_transaction_id = _j["original_transaction_id"].ToString();
                                            _rec.product_id = _j["product_id"].ToString();
                                            _rec.purchase_date = _j["purchase_date"].ToString();
                                            _rec.expires_date = _j["expires_date"].ToString();
                                            _rec.expires_date_formatted = _j["expires_date"].ToString();
                                            _rec.is_trial_period = _j["is_trial_period"].ToString();
                                            _rec.original_purchase_date = _j["original_purchase_date"].ToString();
                                            _rec_l.Add(_rec);
                                        }
                                    }

                                }
                                catch (Exception e)
                                {
                                }


                                
                            }
                            _rec = new IOS_ReceiptInfo();
                            _rec._id = _receipt["transaction_id"].ToString();
                            _rec.transaction_id = _receipt["transaction_id"].ToString();
                            _rec.original_transaction_id = _receipt["original_transaction_id"].ToString();
                            _rec.product_id = _receipt["product_id"].ToString();
                            _rec.purchase_date = _receipt["purchase_date"].ToString();
                            _rec.expires_date = _receipt["expires_date"].ToString();
                            _rec.expires_date_formatted = _receipt["expires_date_formatted"].ToString();
                            _rec.is_trial_period = _receipt["is_trial_period"].ToString();
                            _rec.is_in_intro_offer_period = _receipt["is_in_intro_offer_period"].ToString(); 
                            _rec.original_purchase_date = _receipt["original_purchase_date"].ToString();
                            _rec_l.Add(_rec);
                        }
                    }

                    conn.Open();
                    try
                    {
                        string _sql_pk_tick = "SELECT 'x' FROM bsm_ios_receipt_mas WHERE ticket_uid = :ticket_uid and created >= ( sysdate)";
                        OracleCommand _cmd_ticket = new OracleCommand(_sql_pk_tick, conn);
                        _cmd_ticket.BindByName = true;
                        _cmd_ticket.Parameters.Add("TICKET_UID", ticket_uid);
                        OracleDataReader _rd_ticket = _cmd_ticket.ExecuteReader();
                        /* 必須只驗證成功,或是還未送的 */
                        if (_rd_ticket.Read())
                        {
                            result.result_code = "BSM-00404";
                            result.result_message = "重複傳送";
                            return result;
                        }

                        // Order by Purchase Date

                        _rec_l = (from v in _rec_l orderby v.purchase_date select v).ToList();

                        // get bsm pk_no
                        foreach (IOS_ReceiptInfo _r in _rec_l)
                        {

                            if (mongo_save_ios_receiptinfo(_r))
                            {
                                string _sql_pk_no = "Select Seq_Bsm_Purchase_Pk_No.Nextval PK_NO From Dual";
                                OracleCommand _cmd = new OracleCommand(_sql_pk_no, conn);
                                OracleDataReader _rd = _cmd.ExecuteReader();
                                try
                                {
                                    if (_rd.Read()) _purchase_pk_no = Convert.ToInt32(_rd[0]);
                                }
                                finally
                                {
                                    _rd.Dispose();
                                }

                                string _sql_insert_receipt = @"insert into bsm_ios_receipt_mas(mas_pk_no,receipt,password,ios_product_code,ticket_uid,created,CLIENT_ID,PACKAGE_ID)  values(:P_ORDER_NO,:P_RECEIPT,:P_PASSWORD,:P_IOS_PRODUCT_CODE,:TICKET_UID,sysdate,:CLIENT_ID,:PACKAGE_ID)";
                                OracleCommand _cmd_rep = new OracleCommand(_sql_insert_receipt, conn);
                                try
                                {
                                    _cmd_rep.BindByName = true;
                                    _cmd_rep.Parameters.Add("P_ORDER_NO", _purchase_pk_no);
                                    _cmd_rep.Parameters.Add("P_RECEIPT", OracleDbType.Clob, ticket, ParameterDirection.Input);
                                    _cmd_rep.Parameters.Add("P_PASSWORD", "c7cdbd0220b54ab99af16548b0f27733");
                                    _cmd_rep.Parameters.Add("P_IOS_PRODUCT_CODE", purchase_info.details[0].ios_product_code);
                                    _cmd_rep.Parameters.Add("TICKET_UID", ticket_uid);
                                    _cmd_rep.Parameters.Add("CLIENT_ID", purchase_info.client_id);
                                    _cmd_rep.Parameters.Add("PACKAGE_ID", purchase_info.details[0].package_id);
                                    _cmd_rep.ExecuteNonQuery();
                                }
                                finally
                                {
                                    _cmd_rep.Dispose();
                                }

                                try
                                {

                                    ios_receipt_info _ios_receipt_info;
                                    _ios_receipt_info = new ios_receipt_info();
                                    _ios_receipt_info._id = _purchase_pk_no.ToString();
                                    _ios_receipt_info.client_id = Convert.ToString(_rd["CLIENT_ID"]);
                                    _ios_receipt_info.password = "c7cdbd0220b54ab99af16548b0f27733";
                                    _ios_receipt_info.package_id = purchase_info.details[0].package_id;
                                    _ios_receipt_info.receipt = ticket;
                                    _ios_receipt_info.ticket_uid = ticket_uid;

                                    mongo_save_ios_receipt(_ios_receipt_info);
                                }
                                catch
                                { };


                                string src_no = purchase_info.session_uid;

                                JsonObject json_purchase_info = new JsonObject();
                                json_purchase_info.Add("purchase_info", purchase_info);
                                json_purchase_info.Add("verify_receipt", _receipt);
                                json_purchase_info.Add("receipt", purchase_info.ios_receipt_info);


                                string _sql_purchae = @"begin
    crt_purchase_ios(p_paytype => :p_paytype,
                   p_client_id => :p_client_id,
                   p_device_id => :p_device_id,
                   p_package_id => :p_package_id,
                   p_ios_org_trans_id => :p_ios_org_trans_id,
                   p_ios_trans_id => :p_ios_trans_id,
                   p_pk_no => :p_pk_no,
                   p_mas_no => :p_mas_no,
                   p_purchase_date => :p_purchase_date,
                   p_expires_date => :p_expires_date,
                   is_intro_offer => :p_is_intro_offer,
                    sw_version => :sw_version,
                    from_client => 'Y');
end;";
                                OracleCommand _cmd_p = new OracleCommand(_sql_purchae, conn);
                                try
                                {
                                    _cmd_p.BindByName = true;
                                    _cmd_p.Parameters.Add("P_PAYTYPE", "IOS");
                                    _cmd_p.Parameters.Add("P_CLIENT_ID", purchase_info.client_id);
                                    _cmd_p.Parameters.Add("P_DEVICE_ID", purchase_info.device_id);
                                    _cmd_p.Parameters.Add("P_PACKAGE_ID", _r.product_id);
                                    _cmd_p.Parameters.Add("P_IOS_ORG_TRANS_ID", _r.original_transaction_id);
                                    _cmd_p.Parameters.Add("P_IOS_TRANS_ID", _r.transaction_id);
                                    _cmd_p.Parameters.Add("P_PURCHASE_DATE", _r.purchase_date);
                                    _cmd_p.Parameters.Add("P_EXPIRES_DATE", _r.expires_date_formatted);
                                    _cmd_p.Parameters.Add("P_PK_NO", _purchase_pk_no);
                                    _cmd_p.Parameters.Add("P_IS_INTRO_OFFER", _r.is_in_intro_offer_period);
                                    string option = JsonConvert.ExportToString(_option);
                                    //    _cmd_p.Parameters.Add("OPTION", option);
                                    _cmd_p.Parameters.Add("SW_VERSION", sw_version);



                                    OracleParameter _mas_no = new OracleParameter();
                                    String _s_mas_no = "";
                                    _mas_no.ParameterName = "P_MAS_NO";
                                    _mas_no.OracleDbType = OracleDbType.Varchar2;
                                    _mas_no.Direction = ParameterDirection.InputOutput;
                                    _mas_no.Size = 64;
                                    _mas_no.Value = _s_mas_no;
                                    _cmd_p.Parameters.Add(_mas_no);
                                    _cmd_p.ExecuteNonQuery();

                                    if (_mas_no.Value != null && Convert.ToString(_mas_no.Value) != "null")
                                    {
                                        result.purchase_id = Convert.ToString(_mas_no.Value);
                                    }
                                }
                                finally
                                {
                                    _cmd_p.Dispose();
                                }


                            };
                        };
                    }
                    finally
                    {
                        conn.Close();
                    }
                     
                    result.result_code = "BSM-00000";
                    result.result_message = "Success";


                    if (!(result.purchase_id == null))
                    {
                        result.purchase = BSM_Info_base.get_purchase_info_by_id(purchase_info.client_id, result.purchase_id);
                    }



                    return result;
                }
                else
                {
                    /* result error */

                    Dictionary<string, string> ios_error_code = new Dictionary<string, string>();
                    ios_error_code.Add("21000", "The App Store could not read the JSON object you provided.");
                    ios_error_code.Add("21002", "The data in the receipt-data property was malformed or missing.");
                    ios_error_code.Add("21003", "The receipt could not be authenticated.");
                    ios_error_code.Add("21004", "The shared secret you provided does not match the shared secret on file for your account.");
                    ios_error_code.Add("21005", "The receipt server is not currently available.");
                    ios_error_code.Add("21006", "This receipt is valid but the subscription has expired. When this status code is returned to your server, the receipt data is also decoded and returned as part of the response.Only returned for iOS 6 style transaction receipts for auto-renewable subscriptions.");
                    ios_error_code.Add("21007", "This receipt is from the test environment, but it was sent to the production environment for verification. Send it to the test environment instead.");
                    ios_error_code.Add("21008", "This receipt is from the production environment, but it was sent to the test environment for verification. Send it to the production environment instead.");

                    result.result_code = "BSM-00309";
                    result.result_message = "IOS 認證失敗-" + ios_result["status"].ToString() + ios_error_code[ios_result["status"].ToString()];

                    return result;
                }

            }

            conn.Open();
            string sql1 = "begin :M_RESULT := BSM_CLIENT_SERVICE.CRT_PURCHASE(:M_PURCHASE_INFO,:P_RECURRENT,:P_DEVICE_ID,:OPTION,:SW_VERSION); end; ";

            TBSM_PURCHASE bsm_purchase = new TBSM_PURCHASE();
            TBSM_PURCHASE_DTLS bsm_purchase_dtls = new TBSM_PURCHASE_DTLS();
            TBSM_RESULT bsm_result = new TBSM_RESULT();

            OracleCommand cmd = new OracleCommand(sql1, conn);

            bsm_purchase.SRC_NO = purchase_info.session_uid.Length > 32 ? purchase_info.session_uid.Substring(0, 32) : purchase_info.session_uid;
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

                BSM_Info_base.refresh_client(purchase_info.client_id);

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

        /// <summary>
        /// 設定發票捐贈
        /// </summary>
        /// <param name="gift_flg"></param>
        /// <param name="purchase_id"></param>
        /// <returns></returns>
        /// 
        [JsonRpcMethod("set_invoice_gift")]
        [JsonRpcHelp("發票贈送 gift_flg :Y 贈送 N不贈送")]
        public BSM_Info.purchase_info_list set_invoice_gift(string gift_flg, string purchase_id)
        {
            BSM_Purchase_Info v_purchase = new BSM.BSM_Purchase_Info();
            BSM_Info.purchase_info_list v_result = new BSM_Info.purchase_info_list();
            BSM_Info.BSM_Info_Service_base BSM_Info_base = new BSM_Info.BSM_Info_Service_base(conn.ConnectionString, MongoDBConnectString, MongoDBConnectString_package, MongoDB_Database);
            string client_id = "";
            conn.Open();
            try
            {
                string sql1 = "update bsm_purchase_mas a set a.tax_gift = :p_gift where a.mas_no = :p_purchase_id";
                OracleCommand cmd = new OracleCommand(sql1, conn);
                cmd.BindByName = true;
                cmd.Parameters.Add("P_GIFT", gift_flg);
                cmd.Parameters.Add("P_PURCHASE_ID", purchase_id);
                cmd.ExecuteNonQuery();

                v_purchase = GetPurchaseInfoPriv(purchase_id);

                if (v_purchase.client_id != null)
                {
                    client_id = v_purchase.client_id;
                }
                else
                { client_id = "X"; }

                v_result = BSM_Info_base.get_purchase_info_by_id(client_id, purchase_id);
            }
            finally
            {
                conn.Close();
            }

            logger.Info(JsonConvert.ExportToString(v_result));
            return v_result;

        }


        [JsonRpcMethod("register_coupon")]
        [JsonRpcHelp("兌換券輸入,請輸入ClientInfo 已即Coupon No ,回傳BSM-00000代表成功")]
        /// <summary>
        /// 登錄coupon 號碼
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Client_Info"></param>
        /// <param name="Coupon_NO"></param>
        /// <returns></returns>
        public BSM_Result register_coupon(string token, BSM_ClientInfo client_info, string coupon_no, string device_id, string sw_version)
        {
            BSM_Result _result;
            BSM_Info.BSM_Info_Service_base BSM_Info_base = new BSM_Info.BSM_Info_Service_base(conn.ConnectionString, MongoDBConnectString, MongoDBConnectString_package, MongoDB_Database);
            _result = new BSM_Result();
            BSM_ClientInfo v_Client_Info = new BSM_ClientInfo();
            string coupon_mas_no;

            conn.Open();
            string sql1 = "begin :M_RESULT := BSM_CLIENT_SERVICE.register_coupon(:M_CLIENT_INFO,:P_COUPON_NO,:P_SRC_NO,:P_SW_VERSION); end; ";

            TBSM_CLIENT_INFO t_client_info = new TBSM_CLIENT_INFO();
            TBSM_RESULT bsm_result = new TBSM_RESULT();

            t_client_info.SERIAL_ID = client_info.client_id;

            t_client_info.OWNER_PHONE = client_info.owner_phone_no;
            if (device_id != null)
            {
                t_client_info.MAC_ADDRESS = device_id;
            }
            else
            {
                t_client_info.MAC_ADDRESS = client_info.mac_address;
            }

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

                OracleParameter param2 = new OracleParameter();
                param2.ParameterName = "P_COUPON_NO";
                param2.OracleDbType = OracleDbType.Varchar2;
                param2.Direction = ParameterDirection.InputOutput;
                param2.Value = coupon_no;
                cmd.Parameters.Add(param2);

                OracleParameter param4 = new OracleParameter();
                param4.ParameterName = "P_SRC_NO";
                param4.OracleDbType = OracleDbType.Varchar2;
                param4.Direction = ParameterDirection.InputOutput;
                param4.Size = 64;
                param4.Value = "12345678";
                cmd.Parameters.Add(param4);

                OracleParameter param3 = new OracleParameter();
                param3.ParameterName = "M_RESULT";
                param3.OracleDbType = OracleDbType.Object;
                param3.Direction = ParameterDirection.InputOutput;
                param3.UdtTypeName = "TBSM_RESULT";
                cmd.Parameters.Add(param3);

                OracleParameter param5 = new OracleParameter();
                param5.ParameterName = "P_SW_VERSION";
                param5.OracleDbType = OracleDbType.Varchar2;
                param5.Direction = ParameterDirection.InputOutput;
                param5.Size = 64;
                param5.Value = sw_version ?? client_info.sw_version;
                cmd.Parameters.Add(param5);

                cmd.ExecuteNonQuery();

                bsm_result = (TBSM_RESULT)param3.Value;
                coupon_mas_no = param4.Value.ToString();
                if (bsm_result.result_code == "BSM-00000")
                {
                    BSM_Info_base.refresh_client(client_info.client_id);
                    _result.purchase_list = BSM_Info_base.get_purchase_info(client_info.client_id, client_info.mac_address, coupon_mas_no);
                }

                if (bsm_result.result_code != "BSM-00000")
                {
                    _result.result_code = bsm_result.result_code;
                    _result.result_message = bsm_result.result_message;
                    _result.client = v_Client_Info;
                    return _result;
                }
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }

            _result.result_code = bsm_result.result_code;
            _result.result_message = bsm_result.result_message;
            _result.client = v_Client_Info;
            logger.Info(JsonConvert.ExportToString(_result));
            return _result;
        }


        public BSM_Purchase_Info GetPurchaseInfoPriv(string purchase_id)
        {
            BSM_Purchase_Info v_result = new BSM.BSM_Purchase_Info();
            TBSM_PURCHASE res = new TBSM_PURCHASE();
            string sql1 = "begin :P_PURCHASE_INFO := BSM_CLIENT_SERVICE.Get_Purchase(:P_PURCHASE_ID); end; ";

            OracleCommand cmd = new OracleCommand(sql1, conn);
            cmd.BindByName = true;

            OracleParameter param1 = new OracleParameter();
            param1.ParameterName = "P_PURCHASE_ID";
            param1.OracleDbType = OracleDbType.Varchar2;
            param1.Direction = ParameterDirection.Input;
            param1.Value = purchase_id;
            cmd.Parameters.Add(param1);

            OracleParameter param2 = new OracleParameter();
            param2.ParameterName = "P_PURCHASE_INFO";
            param2.OracleDbType = OracleDbType.Object;
            param2.Direction = ParameterDirection.Output;
            param2.UdtTypeName = "TBSM_PURCHASE";
            cmd.Parameters.Add(param2);

            cmd.ExecuteNonQuery();

            res = (TBSM_PURCHASE)param2.Value;

            v_result.session_uid = res.SRC_NO;
            v_result.purchase_id = res.MAS_NO;
            v_result.client_id = res.SERIAL_ID;
            v_result.pay_type = res.PAY_TYPE;
            v_result.purchase_date = res.PURCHASE_DATE.ToString("yyyy/M/dd HH:mm:ss");
            v_result.card_number = res.CARD_NO;
            v_result.card_type = res.CARD_TYPE;
            v_result.card_expiry = res.CARD_EXPIRY;
            v_result.cvc2 = res.CVC2;
            v_result.approval_code = res.APPROVAL_CODE;

            foreach (TBSM_PURCHASE_DTL n in res.DETAILS.Value)
            {
                BSM.BSM_Purchase_Info_detail m = new BSM_Purchase_Info_detail();
                m.item_id = n.ITEM_ID;
                m.item_name = n.ITEM_NAME;

                m.package_id = n.PACKAGE_ID;
                m.package_name = n.PACKAGE_NAME;
                m.price = (int)n.AMOUNT;
                m.quota = (int)n.QUOTA;
                m.duration = (int)n.DURATION;

                v_result.details.Add(m);
            }

            return v_result;
        }

        public BSM_Purchase_Info GetPurchaseInfo(string purchase_id)
        {
            conn.Open();
            BSM_Purchase_Info v_result = new BSM.BSM_Purchase_Info();
            TBSM_PURCHASE res = new TBSM_PURCHASE();
            string sql1 = "begin :P_PURCHASE_INFO := BSM_CLIENT_SERVICE.Get_Purchase(:P_PURCHASE_ID); end; ";
            OracleCommand cmd = new OracleCommand(sql1, conn);

            try
            {
                cmd.BindByName = true;

                OracleParameter param1 = new OracleParameter();
                param1.ParameterName = "P_PURCHASE_ID";
                param1.OracleDbType = OracleDbType.Varchar2;
                param1.Direction = ParameterDirection.Input;
                param1.Value = purchase_id;
                cmd.Parameters.Add(param1);

                OracleParameter param2 = new OracleParameter();
                param2.ParameterName = "P_PURCHASE_INFO";
                param2.OracleDbType = OracleDbType.Object;
                param2.Direction = ParameterDirection.Output;
                param2.UdtTypeName = "TBSM_PURCHASE";
                cmd.Parameters.Add(param2);

                cmd.ExecuteNonQuery();
                res = (TBSM_PURCHASE)param2.Value;

            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }

            v_result.session_uid = res.SRC_NO;
            v_result.purchase_id = res.MAS_NO;
            v_result.client_id = res.SERIAL_ID;
            v_result.pay_type = res.PAY_TYPE;
            v_result.purchase_date = res.PURCHASE_DATE.ToString("yyyy/M/dd HH:mm:ss");
            v_result.card_number = res.CARD_NO;
            v_result.card_type = res.CARD_TYPE;
            v_result.card_expiry = res.CARD_EXPIRY;
            v_result.cvc2 = res.CVC2;
            v_result.approval_code = res.APPROVAL_CODE;
            foreach (TBSM_PURCHASE_DTL n in res.DETAILS.Value)
            {
                BSM.BSM_Purchase_Info_detail m = new BSM_Purchase_Info_detail();
                m.item_id = n.ITEM_ID;
                m.item_name = n.ITEM_NAME;
                m.package_id = n.PACKAGE_ID;
                m.package_name = n.PACKAGE_NAME;
                m.price = (int)n.AMOUNT;
                m.quota = (int)n.QUOTA;
                m.duration = (int)n.DURATION;
                v_result.details.Add(m);
            }

            return v_result;
        }

        [JsonRpcMethod("test_out")]
        public BSM_Purchase_Request test_out()
        {
            BSM_Purchase_Request result = new BSM_Purchase_Request();
            result.session_uid = "1234";
            result.pay_type = "rewrere";
            result.details[0].item_id = "1234";
            return result;
        }

        [JsonRpcMethod("test_inout")]
        public BSM_Purchase_Request test_out(BSM_Purchase_Request result)
        {
            return result;
        }

        public override void ProcessRequest(HttpContext context)
        {
            long pos = context.Request.InputStream.Position;
            var read = new StreamReader(context.Request.InputStream);
            string jsontstr = read.ReadToEnd();
            context.Request.InputStream.Position = pos;
            int card_pos = jsontstr.ToUpper().IndexOf("CARD_NUMBER");
            if (card_pos > 0)
            {
                jsontstr = jsontstr.Substring(0, card_pos) + jsontstr.Substring(card_pos + 47);
            }
            logger.Info(jsontstr);
            base.ProcessRequest(context);
        }

        [JsonRpcMethod("get_ios_receipt")]
        public string get_ios_receipt(int order_pk_no, string p_receipt, string p_password)
        {
            JsonObject _json_result = new JsonObject();
            conn.Open();
            try
            {
                string _sql = "SELECT RECEIPT,PASSWORD from BSM_IOS_RECEIPT_MAS WHERE MAS_PK_NO=:P_PK_NO AND ROWNUM <=1";
                OracleCommand cmd = new OracleCommand(_sql, conn);
                cmd.BindByName = true;
                cmd.Parameters.Add("P_PK_NO", order_pk_no);
                OracleDataReader _rd = cmd.ExecuteReader();
                if (_rd.Read())
                {
                    JsonObject jo = (JsonObject)JsonConvert.Import(typeof(JsonObject), Convert.ToString(_rd["RECEIPT"]));
                    p_receipt = jo["ios_receipt_info"].ToString();

                    string a = "p_receipt";
                    byte[] data = Convert.FromBase64String(p_receipt);
                    string decodedString = Encoding.ASCII.GetString(data);
                    //  return md5(decodedString;
                }

            }
            finally
            { conn.Close(); }
            return null;
        }

        [JsonRpcMethod("ios_verifyReceipt")]
        public JsonObject ios_verifyReceipt(int order_pk_no, string p_receipt, string p_password)
        {
            JsonObject _json_result = new JsonObject();
            ios_receipt_info _ios_receipt_info;

            _ios_receipt_info = mongo_load_ios_receipt(order_pk_no);

            if (_ios_receipt_info == null)
            {
                conn.Open();
                try
                {
                    string _sql = "SELECT RECEIPT,PASSWORD,TICKET_UID,CLIENT_ID,PACKAGE_ID from BSM_IOS_RECEIPT_MAS WHERE MAS_PK_NO=:P_PK_NO AND ROWNUM <=1";
                    OracleCommand cmd = new OracleCommand(_sql, conn);
                    cmd.BindByName = true;
                    cmd.Parameters.Add("P_PK_NO", order_pk_no);
                    OracleDataReader _rd = cmd.ExecuteReader();
                    if (_rd.Read())
                    {
                        try
                        {
                            JsonObject jo = (JsonObject)JsonConvert.Import(typeof(JsonObject), Convert.ToString(_rd["RECEIPT"]));
                            p_receipt = jo["ios_receipt_info"].ToString();
                        }
                        catch (Jayrock.Json.JsonException e)
                        {
                            JsonArray ja = (JsonArray)JsonConvert.Import(typeof(JsonArray), Convert.ToString(_rd["RECEIPT"]));

                            p_receipt = ja[0].ToString();

                        }
                        p_password = Convert.ToString(_rd["PASSWORD"]);

                        _ios_receipt_info = new ios_receipt_info();
                        _ios_receipt_info._id = order_pk_no.ToString();
                        _ios_receipt_info.client_id = Convert.ToString(_rd["CLIENT_ID"]);
                        _ios_receipt_info.password = Convert.ToString(_rd["PASSWORD"]);
                        _ios_receipt_info.receipt = Convert.ToString(_rd["RECEIPT"]);
                        _ios_receipt_info.ticket_uid = Convert.ToString(_rd["TICKET_UID"]);

                        mongo_save_ios_receipt(_ios_receipt_info);


                    }

                }
                finally
                { conn.Close(); }
                _json_result = _ios_verifyReceipt("", p_receipt, p_password);
                _json_result["latest_receipt"] = null;
            }
            else
            {
                try
                {
                    JsonObject jo = (JsonObject)JsonConvert.Import(typeof(JsonObject), _ios_receipt_info.receipt);
                    p_receipt = jo["ios_receipt_info"].ToString();
                }
                catch (Jayrock.Json.JsonException e)
                {
                    JsonArray ja = (JsonArray)JsonConvert.Import(typeof(JsonArray), _ios_receipt_info.receipt);

                    p_receipt = ja[0].ToString();

                }
                _json_result = _ios_verifyReceipt("", p_receipt, _ios_receipt_info.password);
                _json_result["latest_receipt"] = null;
            }

            return _json_result;
        }

        private JsonObject _ios_verifyReceipt_dev(string src_pk_no, string p_receipt, string p_password)
        {
            JsonObject _json_result = new JsonObject();
            try
            {
                string _result = "";
                StreamReader a = new StreamReader(@"C:\sample_json3.txt");
                _result = a.ReadToEnd();
                a.Dispose();

                _json_result = (JsonObject)JsonConvert.Import(typeof(JsonObject), _result);
            }
            finally
            { }
            logger.Info(_json_result);
            return _json_result;
        }

        private JsonObject _ios_verifyReceipt(string src_pk_no, string p_receipt, string p_password)
        {
            JsonObject _json_result = new JsonObject();
            try
            {
                string _result = "";
                JsonObject _jsonObject = new JsonObject();
                _jsonObject.Add("receipt-data", p_receipt);
                _jsonObject.Add("password", p_password);
                string _post_data = JsonConvert.ExportToString(_jsonObject);
                string url = IOS_URL;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                _result = WebRequest(url, _post_data);
                _json_result = (JsonObject)JsonConvert.Import(typeof(JsonObject), _result);
            }
            finally
            { }
            logger.Info(_json_result);
            return _json_result;
        }

        private JsonObject _ios_verifyReceipt_sandbox(string src_pk_no, string p_receipt, string p_password)
        {
            JsonObject _json_result = new JsonObject();
            conn.Open();
            try
            {
                string _result = "";
                JsonObject _jsonObject = new JsonObject();
                _jsonObject.Add("receipt-data", p_receipt);
                _jsonObject.Add("password", p_password);
                string _post_data = JsonConvert.ExportToString(_jsonObject);
                // string url = IOS_URL;
                //   string url = @"https://buy.itunes.apple.com/verifyReceipt";
                string url = @"https://sandbox.itunes.apple.com/verifyReceipt";
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                _result = WebRequest(url, _post_data);
                _json_result = (JsonObject)JsonConvert.Import(typeof(JsonObject), _result);
            }
            finally
            { conn.Close(); }
            return _json_result;
        }

        public void mongo_save_ios_receipt(ios_receipt_info _ios_receipt_info)
        {
            MongoClient _MongoClient = new MongoClient(MongoDBConnectString);
            MongoServer _MongoServer = _MongoClient.GetServer();
            MongoDatabase _MongoDB_ClientInfo = _MongoServer.GetDatabase(MongoDB);
            MongoCollection<ios_receipt_info> _c_ios_receipts = _MongoDB_ClientInfo.GetCollection<ios_receipt_info>("ios_receipts");

            _c_ios_receipts.Save(_ios_receipt_info);

        }

        public void ios_new_receiptinfo(IOS_ReceiptInfo p_receiptinfo)
        {
            mongo_save_ios_receiptinfo(p_receiptinfo);
            conn.Open();
            try
            {
            }
            finally
            {
                conn.Close();
            }


        }

        public Boolean mongo_save_ios_receiptinfo(IOS_ReceiptInfo p_receiptinfo)
        {
            IOS_ReceiptInfo _receiptinfo;
            MongoClient _MongoClient = new MongoClient(MongoDBConnectString);
            MongoServer _MongoServer = _MongoClient.GetServer();
            MongoDatabase _MongoDB_ClientInfo = _MongoServer.GetDatabase(MongoDB);
            MongoCollection<IOS_ReceiptInfo> _c_ios_receiptinfos = _MongoDB_ClientInfo.GetCollection<IOS_ReceiptInfo>("ios_receiptinfos");
            try
            {
                _receiptinfo = (IOS_ReceiptInfo)_c_ios_receiptinfos.FindOne(Query.EQ("_id", p_receiptinfo._id));
                if (_receiptinfo == null) { _c_ios_receiptinfos.Save(p_receiptinfo); return true; }
                else
                {
                    return true;
                }
            }
            catch
            { return false; }

            _c_ios_receiptinfos.Save(p_receiptinfo);
        }

        public ios_receipt_info mongo_load_ios_receipt(int order_pk_no)
        {
            ios_receipt_info _ios_receipt_info;
            MongoClient _MongoClient = new MongoClient(MongoDBConnectString);
            MongoServer _MongoServer = _MongoClient.GetServer();
            MongoDatabase _MongoDB_ClientInfo = _MongoServer.GetDatabase(MongoDB);
            MongoCollection<ios_receipt_info> _c_ios_receipts = _MongoDB_ClientInfo.GetCollection<ios_receipt_info>("ios_receipts");
            try
            {
                _ios_receipt_info = (ios_receipt_info)_c_ios_receipts.FindOne(Query.EQ("_id", new BsonString(order_pk_no.ToString())));
                return _ios_receipt_info;
            }
            catch
            { return null; }
        }

        [JsonRpcMethod("httpRequest")]
        public string HttpRequest(string url, JsonObject postData)
        {
            string _result;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                string _post_data = JsonConvert.ExportToString(postData);
                //  _result = WebRequest(url, _post_data);

                var jsonBytes = Encoding.ASCII.GetBytes(_post_data);
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;//http1.0
                //httpWebRequest.Connection = "Close";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = jsonBytes.Length;
                var streamWriter = httpWebRequest.GetRequestStream();
                //   StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                string json = _post_data;

                streamWriter.Write(jsonBytes, 0, jsonBytes.Length);
                streamWriter.Flush();
                //   streamWriter.Close();
                try
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
                    String result = streamReader.ReadToEnd();
                    _result = result;
                }
                catch (WebException ex)
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        String result = reader.ReadToEnd();
                        return result;
                    }
                }


                logger.Info(_result);
                return _result;
            }
            catch (Exception e)
            {
                logger.Info(e.Message);
                return e.Message;
            }

        }

        public string WebRequest(string url, string postData)
        {
            string ret = string.Empty;
            int retry = 3;

            //  StreamWriter requestWriter;
            ASCIIEncoding ascii = new ASCIIEncoding();

            byte[] postBytes = Encoding.UTF8.GetBytes(postData);

            while (retry > 0)
            {

                try
                {

                    var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
                    if (webRequest != null)
                    {
                        webRequest.Method = "POST";
                        webRequest.ServicePoint.Expect100Continue = false;
                        webRequest.Timeout = 20000;
                        webRequest.ContentType = "application/json";

                        webRequest.ContentLength = postBytes.Length;
                        using (var requestWriter = webRequest.GetRequestStream())
                        {
                            requestWriter.Write(postBytes, 0, postBytes.Length);
                            requestWriter.Flush();

                        }

                    }

                    HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
                    Stream resStream = resp.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream);
                    ret = reader.ReadToEnd();
                    retry = 0;
                }
                catch (Exception e)
                {
                    retry--;
                    Thread.Sleep(5000);
                }
            }

            return ret;
        }

    }



}