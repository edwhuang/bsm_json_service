    using log4net;
    using log4net.Config;
    using Jayrock.Json.Conversion;

namespace BSM_Info
{

    using System;
    using System.Web;
    using System.Data;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using Jayrock.Json;
    using Jayrock.JsonRpc;
    using Jayrock.JsonRpc.Web;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;
    using System.Messaging;
    using System.Text;
    using System.Linq;

    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.GridFS;
    using MongoDB.Driver.Linq;

    using AutoMapper;
    using AutoMapper.Mappers;
    using AutoMapper.Internal;

    /// <summary>
    /// get client val message for MSMQ
    /// </summary>
    public struct get_client_val_msg
    {
        public string client;
        public string version;
        public string value_name;
        public string return_value;
    }


    /// <summary>
    /// 方案狀態
    /// </summary>
    public class catalog_info
    {
        /// <summary>
        /// catalog id:catalog identified id
        /// </summary>
        public string catalog_id;

        /// <summary>
        ///  catalog name
        /// </summary>
        public string catalog_name;

        /// <summary>
        /// catalog description
        /// </summary>
        public string catalog_description;

        /// <summary>
        ///  catalog status :catalog status 
        /// </summary>
        public string catalog_status;

        /// <summary>
        /// use status ,'Y' client can use this catalog ,'N' can't use this catalog 
        /// </summary>
        public string use_status;

        /// <summary>
        /// use status description: to client to show 
        /// </summary>
        public string status_description;

        /// <summary>
        /// start_date: string field, just for client to show start date information 
        /// </summary>
        public string start_date;

        /// <summary>
        /// end date : string field, just for client to show end information
        /// </summary>
        public string end_date;

        /// <summary>
        /// package_description:this catalog main packgage's description, html string field. 
        /// </summary>
        public string package_description;

        /// <summary>
        /// package_name :  catalog main package's name,string field.
        /// </summary>
        public string package_name;

        /// <summary>
        ///  package id :package id
        /// </summary>
        public string package_id;

        /// <summary>
        ///  price description : jsut for client to show price information
        /// </summary>
        public string price_description;

        /// <summary>
        /// logo : picture file name ,the picture will preload to client 
        /// </summary>
        public string logo;

        /// <summary>
        /// Recurrent 
        /// </summary>
        public string current_recurrent_status;

        ///
        ///
        ///
        public string next_pay_date;

        public string device_id;

        public string can_extend;

    }

    public class cup_package_id
    {
        public string package_id;
    }


    public class package_dtl
    {
        public string desc;
        public string client_id;
        public string coupon_id;
        public decimal cup_dtl_pk_no;
        public List<cup_package_id> cup_package_id;
    }


    public class purchase_detail
    {
        public string mas_pk_no;
        public string catalog_description;
        public string status_description;
        public string start_date;
        public string end_date;
        public string package_name;
        public string package_id;
        public decimal price;
        public decimal orig_price;
        public string price_description;
        public string use_status;
        public string current_recurrent_status;
        public List<package_dtl> package_dtls;
    }


    public class purchase_info
    {
        public string pk_no;
        public string purchase_date;
        public string purchase_datetime;
        public string purchase_id;
        public string card_no;
        public string pay_type;
        public decimal amount;
        public decimal orig_amount;
        public decimal pay_status;
        public string pay_due_date;
        public string invoice_no;
        public string invoice_gift_flg;
        public string bar_invo_no;
        public string bar_due_date;
        public string bar_price;
        public string bar_atm;
        public string bank_code;
        public string cost_credits;
        public string after_credits;
        public string recurrent;
        public string next_pay_date;
        public Boolean is_default;
        public string promo_desc;
        public string promo_code;
        public string promo_prog_id;
        public string promo_title;
        public credits_balance_info src_credits_details;
        public credits_balance_info after_credits_details;
        public List<purchase_detail> details;
    }


    /// <summary>
    /// 購買記錄
    /// </summary>
    public class purchase_info_list
    {
        /// <summary>
        /// catalog id:catalog identified id
        /// </summary>
        public string catalog_id;

        /// <summary>
        /// 館別名稱
        /// </summary>
        public string catalog_name;
        /// <summary>
        /// catalog name
        /// </summary>
        public string catalog_description;

        /// <summary>
        /// \u4f7f\u7528\u72c0\u614b ,'Y' client can use this catalog ,'N' can't use this catalog
        /// </summary>
        public string status_description;

        /// <summary>
        /// start_date: string field, just for client to show start date information
        /// </summary>
        public string start_date;

        /// <summary>
        /// end date : string field, just for client to show end information
        /// </summary>
        public string end_date;


        /// <summary>
        /// \u65b9\u6848\u8aaa\u660e:this catalog main packgage's description, html string field.
        /// </summary>
        public string package_description;

        /// <summary>
        /// \u65b9\u6848\u540d\u7a31 :  catalog main package's name,string field.
        /// </summary>
        public string package_name;

        /// <summary>
        /// package id :package id
        /// </summary>
        public string package_id;

        /// <summary>
        /// \u50f9\u683c\u986f\u793a
        /// </summary>
        public string price_description;

        public string amount_description;
        /// <summary>
        /// \u8cfc\u8cb7\u65e5\u671f
        /// </summary>
        public string purchase_date;
        /// <summary>
        /// 購買時間
        /// </summary>
        public string purchase_datetime;
        /// <summary>
        /// 授權碼
        /// </summary>
        public string approval_code;
        /// <summary>
        /// \u8a02\u55ae\u7de8\u865f
        /// </summary>
        public string purchase_id;
        /// <summary>
        /// \u5361\u865f\u986f\u793a
        /// </summary>
        public string card_no;
        /// <summary>
        /// \u4ed8\u6b3e\u65b9\u5f0f
        /// </summary>
        public string pay_type;

        /// <summary>
        /// 發票日期
        /// </summary>
        public string invoice_date;
        /// <summary>
        /// 發票號碼
        /// </summary>
        public string invoice_no;

        /// <summary>
        /// 發票識別碼
        /// </summary>
        public string invoice_einv_id;

        /// <summary>
        /// 發票捐贈
        /// </summary>
        public string invoice_gift_flg;

        /// <summary>
        /// 使用點數
        /// </summary>
        public string cost_credits;

        /// <summary>
        /// 剩餘點數
        /// </summary>
        public string after_credits;

        /// <summary>
        /// recurrent billing;
        /// </summary>
        public string recurrent;

        /// <summary>
        /// 下次扣款日
        /// </summary>
        public string next_pay_date;

        public string pay_type_code;

        /// <summary>
        /// 使用狀態 ,'Y' client can use this catalog ,'N' can't use this catalog 
        /// </summary>
        public string use_status;

        /// <summary>
        /// logo : picture file name ,the picture will preload to client 
        /// </summary>
        public string logo;

        public string device_id;

        public string src_no;

        public string calculation_type;




    }

    public class package_service_info
    {
        public string _id;
        public string service_id;
        public string name;
        public string description;
        public string package_id;
    }

    /// <summary>
    /// 方案狀態
    /// </summary
    public class package_info
    {
        public string _id;
        /// <summary>
        /// catalog id:catalog identified id
        /// </summary>
        /// 
        public string catalog_id;

        /// <summary>
        ///  catalog name
        /// </summary>
        public string catalog_name;

        public string catalog_ref;

        /// <summary>
        /// catalog description
        /// </summary>
        public string catalog_description;

        /// <summary>
        /// 使用狀態 ,'Y' client can use this catalog ,'N' can't use this catalog 
        /// </summary>
        public string use_status;

        /// <summary>
        /// 使用狀態說明 description: to client to show 
        /// </summary>
        public string status_description;

        /// <summary>
        /// start_date: string field, just for client to show start date information 
        /// </summary>
        public string start_date;

        /// <summary>
        /// end date : string field, just for client to show end information
        /// </summary>
        public string end_date;

        /// <summary>
        /// 方案說明(HTML):this catalog main packgage's description, html string field. 
        /// </summary>
        public string package_description;

        /// <summary>
        /// 方案說明(文字型態):this catalog main packgage's description, html string field. 
        /// </summary>
        public string package_description_text;

        /// <summary>
        /// 方案名稱 :  catalog main package's name,string field.
        /// </summary>
        public string package_name;

        /// <summary>
        ///  package id :package id
        /// </summary>
        /// 
        public string package_id;

        public string PACKAGE_ID;


        /// <summary>
        ///  catalgo status :catalog status 
        /// </summary>
        public string catalog_status;


        /// <summary>
        /// 方案說明明細
        /// </summary>

        public string package_description_dtl;

        /// <summary>
        ///  price description : jsut for client to show price information
        /// </summary>
        /// 
        public string price_description;
        public string price;

        /// <summary>
        /// logo : picture file name ,the picture will preload to client 
        /// </summary>
        public string logo;

        /// <summary>
        /// item_id : 單片此欄位回應可播放的item_id
        /// </summary>
        public string item_id;

        /// <summary>
        /// 顯示名細項附 'Y' 顯示,'N' 不顯示
        /// </summary>
        public string show_detail_flg;

        public string ref1;
        public string ref2;
        public string ref3;
        public string ref6;

        /// <summary>
        /// 點數
        /// </summary>
        public string cost_credits;
        public string after_credits;
        public Decimal credits_balance;
        public Decimal credits;
        public credits_balance_info credits_info;
        public credits_balance_info credits_balance_info;

        public string cal_type; // 計算方式P by package,T by title
        public string days; 

        public string credits_description; //點數說明
        public string credits_type; // 點數的使用方式(紅利GIFT, 購買點數BUY

        public string remark;
        public string recurrent; //是否可Recurrent
        public string next_pay_date; //下次購款日
        public string current_recurrent_status;  //目前recurrent 狀態
        public string current_recurrent_package; //目前recurrent 狀態
        
        public string credits_remind; //使用點數提醒
        public string[] pay_method_list; //付款方式
        public string system_type;
        public string apt_only;
        public string recurrent_list;

        /// <summary>
        /// package 順序
        /// </summary>
        public Decimal display_order;

        ///
        /// ios data
        ///

        public string ios_product_code;


        /// <summary>
        /// 是否可使用此方案狀態,'Y' ->可用 'N' ->不可用 'W'->警告
        /// </summary>
        public string package_status;

        public string package_status_message;

        public string qr_code_url;
        public string qr_code_desc;


        /// <summary>
        /// 
        /// </summary>

        public List<package_service_info> services;
    }

    public class package_sg_info
    {
        public string _id;
        public string package_id;
        public string software_group;
        public string version;
        public string version_end;
    }

    public class content_info
    {
        /// <summary>
        /// title
        /// </summary>
        public string title;
        /// <summary>
        /// content_id 
        /// </summary>
        public string content_id;
        /// <summary>
        /// 畫質
        /// </summary>
        public string sdhd;
        /// <summary>
        /// 級別
        /// </summary>
        /// 
        public string eng_title;

        public string release_year;

        public decimal score;

        public decimal score10;

        public string rating;
        /// <summary>
        /// 類型
        /// </summary>
        public string genre;
        /// <summary>
        /// 片長
        /// </summary>
        public string runtime;
        /// <summary>
        /// 下架日期
        /// </summary>
        public string off_shelf_date;

        /// <summary>
        /// 備註
        /// </summary>
        public string remark;

    }

    public class bsm_package_special
    {
        public string _id;
        public string package_id;
        public DateTime start_date;
        public DateTime end_date;
        public JsonObject Option;
    }

    /// <summary>
    /// content 與 package detail 資料
    /// </summary>
    public class content_package_info
    {

        /// <summary>
        /// 顯示名細項附 'Y' 顯示,'N' 不顯示
        /// </summary>
        public string show_detail_flg;
        /// <summary>
        /// Content info
        /// </summary>
        public content_info content_info;
        /// <summary>
        /// package info
        /// </summary>
        public package_info package_info;

    }

    public class client_detail
    {
        public string src_no;
        public string src_pay_type;
        public string start_date;
        public string end_date;
        public string cat_id;
        public string package_id;
        public string package_status;
        public string used;
        public string package_start_date_desc;
        public string package_end_date_desc;
        public string recurrent;
        public string device_id;
        
    }

    public class account_info
    {
        public string _id;
        public string client_id;
        public string activation_code;
        public List<client_detail> package_details;
        public List<catalog_info> services;
        public List<purchase_info> purchases;
        public credits_balance_info credits;
        public List<purchase_info_list> purchase_dtls;
        public DateTime last_refresh_time;


        public account_info()
        {
            package_details = new List<client_detail>();
            credits = new credits_balance_info();
            services = new List<catalog_info>();
         //   purchases = new List<purchase_info>();
            purchase_dtls = new List<purchase_info_list>();

        }
    }

    public class credits_detail
    {
        public string credits_type; // 點數種類
        public string credits_desc; //點數說明
        public int? credits; //剩餘點數
        public int? use_credits; //
        public string expired_date;

        public credits_detail()
        {
            credits = 0;
            use_credits=0;
        }
    }

    public class credits_balance_info
    {
        public int? credits;
        public string credits_description;
        public string creaits_remind;
        public string expired_date;
        public List<credits_detail> details;

        public credits_balance_info()
        {
            credits = 0;
            details = new List<credits_detail>();
        }
    }

    public class group
    {
        public string group_id;
        public string title;
        public string group_description;
        public List<package_info> packages;
        public group()
        {
            packages = new List<package_info>();
            group_description = "";
        }
    }

    public class software_packages
    {
        public string _id;
        public string software_group;
        public List<package_info> packages;
        public software_packages()
        {
            packages = new List<package_info>();
        }
    }

    namespace Old_version
    {
        /// <summary>
        /// 舊版package 狀態
        /// </summary>
        public class package_info
        {

            public string cat_desc;

            public string status_desc;

            public string start_date;

            public string end_date;

            public string package_description;

            public string package_name;

            public string price_desc;

            public string Logo;
        }
        /// <summary>
        /// 舊版 取package 狀態
        /// </summary>
        public class result_get_package_status
        {

            public string client_id;

            public List<BSM_Info.Old_version.package_info> package_status;
        }

    }

    public class BSM_Info_Service_base
    {
       
        OracleConnection conn;

        private string _MongoDbconnectionString;
        private string _MongoDbconnectionString_package;
        private MongoClient _Mongoclient_package;
        private MongoServer _MongoServer_package;
        private MongoClient _Mongoclient;
        private MongoServer _MongoServer;
        private MongoDatabase _MongoDB;
        private MongoDatabase _MongoDB_package;
        private String MongoDB_Database;

        private void connectDB()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
        }

        private OracleDataReader ExecuteReader(string _sql)
        {
            connectDB();
            OracleCommand _cmd = new OracleCommand(_sql, conn);
            _cmd.BindByName = true;
            OracleDataReader _Data_Reader = _cmd.ExecuteReader();
            return _Data_Reader;
        }

        private void DataReaderToObject(OracleDataReader a, object b)
        {
            Type p = b.GetType();
            System.Reflection.FieldInfo[] pf = p.GetFields();
            for (int i = 0; i < a.FieldCount; i++)
            {
                string fieldName = a.GetName(i);
                string fieldType = a[i].GetType().Name;

                object vaule = a.GetValue(i);
                if (vaule != DBNull.Value)
                {
                    foreach (var ps2 in pf)
                    {
                        Type ft = ps2.FieldType;

                        if (ps2.Name.ToUpper() == fieldName.ToUpper())
                        {
                            if (ft == typeof(Nullable<decimal>))
                                ps2.SetValue(b, Convert.ToDecimal(vaule));
                            else if (ft == typeof(string[]) && vaule.GetType() == typeof(string))
                                ps2.SetValue(b, Convert.ToString(vaule).Split(','));
                            else if (ft == typeof(string) && vaule.GetType() == typeof(string))
                                ps2.SetValue(b, vaule);
                        }
                    }
                }
            };
        }

        private IList<T> DataReaderToObjectList<T>(OracleDataReader rd) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            IList<T> result = new List<T>();
            while (rd.Read())
            {
                T item = new T();
                DataReaderToObject(rd, item);
                result.Add(item);
            }
            return result;
        }

        private IList<T> DataReaderToObjectList<T>(string sql) where T : new()
        {
            OracleDataReader rd = this.ExecuteReader(sql);
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            IList<T> result = new List<T>();
            while (rd.Read())
            {
                T item = new T();
                DataReaderToObject(rd, item);
                result.Add(item);
            }
            return result;
        }


        public BSM_Info_Service_base(string connString, string MongodbConnectString, string MongodbConnectString_package, string MongoDB_Database)
        {
            conn = new OracleConnection();
            conn.ConnectionString = connString;
            _MongoDbconnectionString = MongodbConnectString;
            _MongoDbconnectionString_package = MongodbConnectString_package;

            //
            // Mongodb setting
            //
            try
            {
                if (_MongoDbconnectionString != null && _MongoDbconnectionString !="" ){
                _Mongoclient = new MongoClient(_MongoDbconnectionString);
                _MongoServer = _Mongoclient.GetServer();
                _MongoDB = _MongoServer.GetDatabase(MongoDB_Database+"ClientInfo");
                }
                else
                {
                    _Mongoclient = null;
                    _MongoServer = null;
                    _MongoDB = null;
                }

                if (_MongoDbconnectionString_package != null && _MongoDbconnectionString_package !="")
                { 
                _Mongoclient_package = new MongoClient(_MongoDbconnectionString_package);
                _MongoServer_package = _Mongoclient_package.GetServer();
                _MongoDB_package = _MongoServer_package.GetDatabase(MongoDB_Database + "PackageInfoDB");
                }else
                {
                    _Mongoclient_package = null;
                    _MongoServer_package = null;
                    _MongoDB_package = null;
                }
                
            }
            catch(Exception e)
            {
            }
        }

        /// <summary>
        /// 取各館狀態,舊版本
        /// </summary>
        /// <param name="p_client_id"></param>
        /// <returns></returns>
        /// 
        public BSM_Info.Old_version.result_get_package_status get_package_status(string client_id)
        {
            BSM_Info.Old_version.result_get_package_status res = new BSM_Info.Old_version.result_get_package_status();
            res.client_id = client_id;
            string p_client_id = client_id;
            string _sql;

            // client 統一轉大寫

            p_client_id = p_client_id.ToUpper();

            connectDB();
            try
            {
                res.package_status = new List<BSM_Info.Old_version.package_info>();

                _sql = "SELECT a.PACKAGE_CAT1,a.SUPPLY_NAME||a.PACKAGE_NAME,a.START_DATE,a.end_date,b.PACKAGE_DES_HTML,b.PRICE_DES,a.package_status,b.logo FROM BSM_CLIENT_DETAILS_CAT a,BSM_PACKAGE_MAS b WHERE MAC_ADDRESS=:MAC_ADDRESS AND a.PACKAGE_ID=b.PACKAGE_ID";

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                // OracleParameter _p_mac_address = new OracleParameter("MAC_ADDRESS", OracleDbType.Varchar2, p_client_id, ParameterDirection.Input);
                _cmd.BindByName = true;
                _cmd.Parameters.Add("MAC_ADDRESS", p_client_id);
                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();
                int _i = 0;
                try
                {
                    while (v_Data_Reader.Read())
                    {
                        BSM_Info.Old_version.package_info v_package_info = new BSM_Info.Old_version.package_info();
                        if (!v_Data_Reader.IsDBNull(0))
                        {
                            v_package_info.cat_desc = v_Data_Reader.GetString(0);

                        }
                        if (!v_Data_Reader.IsDBNull(1))
                        {
                            v_package_info.package_name = v_Data_Reader.GetString(1);
                        }
                        if (!v_Data_Reader.IsDBNull(2))
                        {
                            v_package_info.start_date = v_Data_Reader.GetString(2);
                        }
                        if (!v_Data_Reader.IsDBNull(3))
                        {
                            v_package_info.end_date = v_Data_Reader.GetString(3);
                        }
                        if (!v_Data_Reader.IsDBNull(4))
                        {
                            v_package_info.package_description = v_Data_Reader.GetString(4);
                        }
                        if (!v_Data_Reader.IsDBNull(5))
                        {
                            v_package_info.price_desc = v_Data_Reader.GetString(5);
                        }
                        if (!v_Data_Reader.IsDBNull(6))
                        {
                            v_package_info.status_desc = v_Data_Reader.GetString(6);
                        }
                        if (!v_Data_Reader.IsDBNull(7))
                        {
                            v_package_info.Logo = v_Data_Reader.GetString(7);
                        }

                        res.package_status.Add(v_package_info);
                        _i++;

                    }
                }
                finally
                {
                    v_Data_Reader.Dispose();
                }


            }
            finally
            {
                conn.Close();

            }

            return res;
        }

        /// <summary>
        /// 取各館狀態
        /// </summary>
        /// <param name="client_id"></param>
        /// <returns></returns>
        /// 
        public List<catalog_info> get_catalog_info(string client_id, string device_id, string sw_version)
        {
            process_auto_coupon(client_id, device_id, sw_version);
            List<catalog_info> _result = new List<catalog_info>();

            account_info acc_info = get_account_info(client_id, device_id);

            if (acc_info != null && acc_info.services != null) _result = acc_info.services;
            else
                _result= get_catalog_info_oracle(client_id, device_id, sw_version);

           // _result = (from _c in _result where _c.device_id == "" || _c.device_id == device_id select _c).ToList();
            return _result;
        }

        public List<catalog_info> get_catalog_info_oracle(string client_id, string device_id,string sw_version)
        {
 
            List<catalog_info> v_result = new List<catalog_info>();
            string _sql;
            client_id = client_id.ToUpper();

            connectDB();
            try
            {

                _sql = @"with cte as (
Select case when cal_type = 'T' then
              t.package_name
            else
              t2.package_cat1
            end package_cat1,
        
         case when  min(t.start_date) is null 
              then max(t2.package_start_date_desc)
            else to_char(trunc(min(t.start_date)),'YYYY/MM/DD') end start_date,

        case when min(t.end_date) is null
            then '未啟用'
            else
                to_char(trunc(max(t.end_date)),'YYYY/MM/DD')
             end end_date,
        case when min(t.start_date) is null
          then '未啟用'
          else
            case when max(t.end_date) >= sysdate 
              then
                   '已啟用'
              else '已到期'
              end
          end package_status,
        case when min(t.start_date) is null
          then 'N'
          else
            case when max(t.end_date) >= sysdate 
              then
                   'Y'
              else 'N'
              end
          end package_status_flg,
        max(t.pk_no) detail_pk_no,
              t2.package_cat_id1
           package_cat_id1,
        t.device_id
         from bsm_client_details t,bsm_package_mas t2,bsm_purchase_mas t4
 where t.status_flg = 'P'
   and t.package_id in (Select t2.package_id from bsm_package_mas t2 where system_type <> 'CLIENT_ACTIVED')
   and t.package_id not in ('CHG003','CH4G06')  
   and t2.package_id= t.package_id
   and t2.acl_period is null
   and t.src_pk_no=t4.pk_no (+)
   and t.mac_address=:CLIENT_ID
 group by case
          when cal_type = 'T' then
              t.package_name
           else
              t2.package_cat1
            end  ,
          t2.cal_type,
          t2.package_cat1,
                
              t2.package_cat_id1
          ,
          t.item_id,
          t.device_id
          )
select cte.package_cat1,t3.supply_name||t3.package_name package_name,cte.start_date,cte.end_date,PACKAGE_DES_HTML,PRICE_DES,package_status,logo,t2.package_id,package_type,system_type,
decode(BSM_RECURRENT_UTIL.check_recurrent_2(cte.package_cat_id1, :CLIENT_ID,:DEVICE_ID),'Y','R','O') recurrent,
decode(BSM_RECURRENT_UTIL.check_recurrent_2(cte.package_cat_id1, :CLIENT_ID,:DEVICE_ID),'Y',to_char(BSM_RECURRENT_UTIL.get_service_end_date(cte.package_cat_id1, :CLIENT_ID),'YYYY/MM/DD'),'無') next_pay_date,
cte.package_cat_id1,
cte.package_status_flg,
cte.device_id
from cte,bsm_package_mas t2,bsm_client_details t3
where cte.detail_pk_no =t3.pk_no 
and t3.package_id=t2.package_id";

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                _cmd.BindByName = true;
                _cmd.Parameters.Add("CLIENT_ID", client_id);
                _cmd.Parameters.Add("DEVICE_ID", device_id);
                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();
                int _i = 0;
                try
                {
                    while (v_Data_Reader.Read())
                    {
                        catalog_info v_catalog_info = new catalog_info();

                        v_catalog_info.device_id = Convert.ToString(v_Data_Reader["DEVICE_ID"]);
                        v_catalog_info.catalog_name = Convert.ToString(v_Data_Reader["PACKAGE_CAT1"]);
                        v_catalog_info.catalog_description = Convert.ToString(v_Data_Reader["PACKAGE_CAT1"]);
                        v_catalog_info.package_name = Convert.ToString(v_Data_Reader["PACKAGE_NAME"]);
                        v_catalog_info.start_date = Convert.ToString(v_Data_Reader["START_DATE"]);
                        v_catalog_info.end_date = Convert.ToString(v_Data_Reader["END_DATE"]);
                        v_catalog_info.package_description = Convert.ToString(v_Data_Reader["PACKAGE_DES_HTML"]);
                        v_catalog_info.price_description = Convert.ToString(v_Data_Reader["PRICE_DES"]);
                        v_catalog_info.status_description = Convert.ToString(v_Data_Reader["PACKAGE_STATUS"]);
                        v_catalog_info.logo = Convert.ToString(v_Data_Reader["LOGO"]);
                        v_catalog_info.package_id = Convert.ToString(v_Data_Reader["PACKAGE_ID"]);
                        v_catalog_info.current_recurrent_status = Convert.ToString(v_Data_Reader["RECURRENT"]);
                        v_catalog_info.next_pay_date = Convert.ToString(v_Data_Reader["NEXT_PAY_DATE"]);
                        v_catalog_info.catalog_id = Convert.ToString(v_Data_Reader["PACKAGE_CAT_ID1"]);
                        v_catalog_info.use_status = Convert.ToString(v_Data_Reader["PACKAGE_STATUS_FLG"]);
  
                        v_result.Add(v_catalog_info);
                        _i++;

                    }
                }
                finally
                {
                    v_Data_Reader.Dispose();
                }
            }
            finally
            {
                conn.Close();

            }
            return v_result;
        }


        /// <summary>
        /// 取購買紀錄
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <returns></returns>
        public List<purchase_info_list> get_purchase_info(string client_id, string device_id, string src_no)
        {
            List<purchase_info_list> _result = new List<purchase_info_list>();
            account_info acc_info = get_account_info(client_id, device_id);
            if (acc_info != null && acc_info.purchase_dtls != null) _result = acc_info.purchase_dtls;
            else
            {
                _result = get_purchase_info_oracle(client_id);
                acc_info = get_account_info(client_id, device_id);

            }


            if (src_no != null) _result = (from _p in _result where _p.src_no == src_no select _p).ToList();


            _result = (from _p in _result orderby  _p.purchase_id descending,_p.amount_description descending  where _p.device_id == "" || _p.device_id == device_id select _p).ToList();
            foreach (var a in _result)
            {
                if (a.pay_type != "信用卡" && a.pay_type != "REMIT" && a.pay_type != "ATM")
                {
                    a.price_description = "";
                    a.amount_description = "";
                  
                }
            }

            return _result;
        }

        public List<purchase_info_list> get_purchase_info_oracle(string client_id)
        {

            List<purchase_info_list> v_result = new List<purchase_info_list>();
            client_id = client_id.ToUpper();

            connectDB();
            try
            {
                string _sql;
                string _dev_sql = "device_id is null"; ;

                _sql = @"Select
       d.package_cat1 cat_description,
       nvl(c.package_name, d.description)||
       (select ' '||promo_title from promotion_prog_item x where x.promo_prog_id=a.promo_prog_id and x.discount_package_id=e.package_id) package_name,
       to_char(a.purchase_date, 'YYYY/MM/DD') purchase_date,
       d.price_des,
       decode(a.pay_type,
              'REMIT',
              '便利商店',
              'ATM',
              'ATM',
              'CREDIT',
              '信用卡',
              'credit',
              '信用卡',
              '點數',
              '點數',
              a.pay_type) pay_type,
       '************' || substr(a.card_no, 13, 4) card_no,
       a.mas_no purchase_id,
       a.approval_code approval_code,
       decode(c.start_date,
              null,
              '未啟用',
              decode(sign(end_date - sysdate), 1, '已啟用', '已到期')) status_description,
       to_char(c.start_date, 'YYYY/MM/DD') start_date,
       to_char(c.end_date, 'YYYY/MM/DD') end_date,
       to_char(a.purchase_date, 'YYYY/MM/DD HH24:MI') purchase_time,
       d.package_cat1 cat_name,
       d.package_start_date_desc,
       d.package_end_date_desc,
       nvl(a.tax_inv_no,'') tax_inv_no,
       nvl(a.tax_gift,'N') tax_gift,
       '' title,
       d.cal_type,
       (select to_char(F_INVO_DATE, 'YYYY/MM/DD')
          from tax_inv_mas inv
         where inv.f_invo_no = a.tax_inv_no
           and rownum <= 1) INVO_DATE,
       (select IDENTIFY_ID
          from tax_inv_mas inv
         where inv.f_invo_no = a.tax_inv_no
           and rownum <= 1) INVO_IDENTIFY_ID,
       decode(a.cost_credits,
              '',
              '',
              replace(a.cost_credits, '點', '') || '點') cost_credits,
       decode(a.after_credits,
              '',
              '',
              replace(a.after_credits, '點', '') || '點') after_credits,
       d.system_type,
       DECODE(a.recurrent, 'Y', 'R', 'N', 'O', a.recurrent) recurrent,
       decode(a.recurrent,
              'R',
              to_char(BSM_RECURRENT_UTIL.get_service_end_date(d.package_cat_id1,
                                                              :MAC_ADDRESS),
                      'YYYY/MM/DD'),
              '無') next_pay_date,
       decode(a.pay_type,
              '其他',
              'OTHER',
              'CREDIT',
              'CREDIT',
              '贈送',
              'GIFT',
              '手動刷卡',
              'CREDIT',
              '兌換券',
              'GOUPON',
              '儲值卡',
              'CREDITS',
              '信用卡',
              'CREDIT',
              'ATM',
              'ATM',
              'REMIT',
              'REMIT',
              '便利商店',
              'REMIT',
              '匯款',
              'REMIT',
              pay_type) pay_type_code,
       d.package_cat_id1,
       d.logo,
       a.pk_no,
       c.device_id,
       a.src_no,
       e.price item_price,
       a.promo_prog_id,
       e.package_id
  from bsm_purchase_mas a,
       bsm_purchase_item e,
       bsm_client_details c,
       (select package_id,
               package_cat1,
               package_name,
               description,
               price_des,
               package_end_date_desc,
               package_start_date_desc,
               cal_type,
               system_type,
               package_cat_id1,
               logo
          from bsm_package_mas
       union all
         select package_id,package_name,'','',to_char(amount)||'元','','','P','OPTION','','' from stk_package_mas where package_type <> 'COUPON') d
 where e.mas_pk_no = a.pk_no
   and c.src_pk_no(+) = e.mas_pk_no
   and c.src_item_pk_no(+) = e.pk_no
   and e.package_id = d.package_id(+)
   and ((a.status_flg = 'P' and a.due_date >= sysdate) or
       a.status_flg = 'Z')
   and not a.show_flg = 'N'
   and a.serial_id = :MAC_ADDRESS
   and a.trans_to is null
   and a.show_flg <> 'N'
 Order by to_char(a.purchase_date, 'YYYY/MM/DD') desc, a.mas_no desc,d.cal_type
";


                OracleCommand _cmd = new OracleCommand(_sql, conn);
                _cmd.BindByName = true;
                _cmd.Parameters.Add("MAC_ADDRESS", client_id);
                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();
                int _i = 0;
                try
                {
                    while (v_Data_Reader.Read())
                    {
                        purchase_info_list v_purchase_info = new purchase_info_list();

                        v_purchase_info.catalog_description = Convert.ToString(v_Data_Reader["CAT_DESCRIPTION"]);
                        v_purchase_info.package_name = Convert.ToString(v_Data_Reader["PACKAGE_NAME"]);
                        v_purchase_info.purchase_date = Convert.ToString(v_Data_Reader["PURCHASE_DATE"]);
                        v_purchase_info.price_description = Convert.ToString(v_Data_Reader["PRICE_DES"]);
                        v_purchase_info.amount_description = Convert.ToString(v_Data_Reader["ITEM_PRICE"]) + "元";
                        v_purchase_info.device_id = Convert.ToString(v_Data_Reader["DEVICE_ID"]);
                        v_purchase_info.src_no = Convert.ToString(v_Data_Reader["SRC_NO"]); 

                        if (!v_Data_Reader.IsDBNull(4))
                        {
                            if (v_Data_Reader.GetString(4) == "儲值卡")
                            {
                                v_purchase_info.pay_type = "點數";
                                v_purchase_info.cost_credits = Convert.ToString(v_Data_Reader["COST_CREDITS"]);
                                v_purchase_info.after_credits = Convert.ToString(v_Data_Reader["AFTER_CREDITS"]);
                            }
                            else
                            {
                                v_purchase_info.pay_type = v_Data_Reader.GetString(4);
                                v_purchase_info.cost_credits = v_purchase_info.price_description;
                                v_purchase_info.after_credits = Convert.ToString(v_Data_Reader["AFTER_CREDITS"]) ?? get_credits_balance(client_id).credits_description;
                                v_purchase_info.after_credits = _get_credits_balance(client_id).credits_description;
                            }

                        }
                        else
                        {
                            v_purchase_info.pay_type = "信用卡";
                            v_purchase_info.cost_credits = v_purchase_info.price_description;
                            v_purchase_info.after_credits = Convert.ToString(v_Data_Reader["AFTER_CREDITS"]) ?? get_credits_balance(client_id).credits_description;
                            v_purchase_info.after_credits = _get_credits_balance(client_id).credits_description;
                        }

                        v_purchase_info.card_no = Convert.ToString(v_Data_Reader["CARD_NO"]);
                        v_purchase_info.purchase_id = Convert.ToString(v_Data_Reader["PURCHASE_ID"]);
                        v_purchase_info.approval_code = Convert.ToString(v_Data_Reader["APPROVAL_CODE"]);


                        if (!v_Data_Reader.IsDBNull(8))
                        {
                            if (!v_Data_Reader.IsDBNull(23))
                            {
                                if (v_Data_Reader.GetString(23) == "CREDITS" || v_Data_Reader.GetString(23) == "FREE_CREDITS")
                                {
                                    v_purchase_info.status_description = null;
                                    v_purchase_info.end_date = null;
                                    string _sql_credits = "SELECT to_char(a.expiration_date,'YYYY/MM/DD')  FROM bsm_client_credits_Mas a  WHERE mas_pk_no = " + v_Data_Reader["PK_NO"].ToString();
                                    OracleCommand _cmd_credits = new OracleCommand(_sql_credits, conn);
                                    try
                                    {
                                        OracleDataReader v_Data_Reader_2 = _cmd_credits.ExecuteReader();
                                        if (v_Data_Reader_2.Read())
                                        {
                                            v_purchase_info.status_description = "已購買";
                                            v_purchase_info.end_date = v_Data_Reader_2[0].ToString();
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        v_purchase_info.end_date = null;
                                    }
                                    _cmd_credits.Dispose();
                                }
                                else
                                {
                                    v_purchase_info.status_description = v_Data_Reader.GetString(8);
                                    if (!v_Data_Reader.IsDBNull(10))
                                    {

                                        v_purchase_info.end_date = v_Data_Reader.GetString(10);
                                    }
                                    else
                                    {
                                        if (!v_Data_Reader.IsDBNull(14)) { v_purchase_info.end_date = v_Data_Reader.GetString(14); }
                                        else
                                        {
                                            v_purchase_info.end_date = "";

                                        }
                                    }
                                }
                            }
                            else
                            {
                                v_purchase_info.status_description = v_Data_Reader.GetString(8);
                                if (!v_Data_Reader.IsDBNull(10))
                                {
                                    v_purchase_info.end_date = v_Data_Reader.GetString(10);
                                }
                                else
                                {
                                    if (!v_Data_Reader.IsDBNull(14)) { v_purchase_info.end_date = v_Data_Reader.GetString(14); }
                                    else
                                    {
                                        v_purchase_info.end_date = "";

                                    }
                                }
                            }

                        }
                        else
                        {
                            v_purchase_info.status_description = "未啟用";
                            if (!v_Data_Reader.IsDBNull(10))
                            {
                                v_purchase_info.end_date = v_Data_Reader.GetString(10);
                            }
                            else
                            {
                                if (!v_Data_Reader.IsDBNull(14)) { v_purchase_info.end_date = v_Data_Reader.GetString(14); }
                                else
                                {
                                    v_purchase_info.end_date = "";

                                }
                            }
                        }

                        if (!v_Data_Reader.IsDBNull(9))
                        {
                            v_purchase_info.start_date = v_Data_Reader.GetString(9);
                        }
                        else
                        {
                            if (!v_Data_Reader.IsDBNull(13)) { v_purchase_info.start_date = v_Data_Reader.GetString(13); }
                            else
                            {
                                v_purchase_info.start_date = "";

                            }
                        }


                        if (!v_Data_Reader.IsDBNull(12))
                        {
                            v_purchase_info.catalog_name = v_Data_Reader.GetString(12);
                        }

                        if (!v_Data_Reader.IsDBNull(15))
                        {
                            // Tax Gift

                            if (v_Data_Reader.IsDBNull(16))
                            {
                                v_purchase_info.invoice_no = v_Data_Reader.GetString(15);
                                v_purchase_info.invoice_date = v_Data_Reader.GetString(19);
                                v_purchase_info.invoice_einv_id = v_Data_Reader.GetString(20);
                                v_purchase_info.invoice_gift_flg = "N";
                            }
                            else
                            {
                                if (v_Data_Reader.GetString(16) == "Y")
                                {
                                    v_purchase_info.invoice_no = "捐贈";
                                    v_purchase_info.invoice_date = "";
                                    v_purchase_info.invoice_einv_id = "";
                                    v_purchase_info.invoice_gift_flg = v_Data_Reader.GetString(16);
                                }
                                else
                                {
                                    v_purchase_info.invoice_no = v_Data_Reader.GetString(15);
                                    v_purchase_info.invoice_date = v_Data_Reader.GetString(19);
                                    v_purchase_info.invoice_einv_id = v_Data_Reader.GetString(20);
                                    v_purchase_info.invoice_gift_flg = v_Data_Reader.GetString(16);
                                }
                            }

                        }
                        else
                        {
                            v_purchase_info.invoice_no = "無";
                            if (v_Data_Reader.IsDBNull(16))
                            {

                                v_purchase_info.invoice_gift_flg = "N";
                            }
                            else
                            {
                                v_purchase_info.invoice_gift_flg = v_Data_Reader.GetString(16);
                            }

                        }

                        v_purchase_info.calculation_type = Convert.ToString( v_Data_Reader["CAL_TYPE"]);

                        if (!v_Data_Reader.IsDBNull(18))
                        {
                            if (v_Data_Reader.GetString(18) == "T")
                            {
                                if (!v_Data_Reader.IsDBNull(17))
                                {
                                    v_purchase_info.catalog_description = v_Data_Reader.GetString(17);
                                    if (!v_Data_Reader.IsDBNull(19))
                                    {
                                        v_purchase_info.invoice_date = v_Data_Reader.GetString(19);
                                    }
                                    if (!v_Data_Reader.IsDBNull(20))
                                    {
                                        v_purchase_info.invoice_einv_id = v_Data_Reader.GetString(20);
                                    }
                                }
                            }
                        }

                        if (!v_Data_Reader.IsDBNull(24))
                        {
                            v_purchase_info.recurrent = v_Data_Reader.GetString(24);
                        }

                        if (!v_Data_Reader.IsDBNull(25))
                        {
                            v_purchase_info.next_pay_date = v_Data_Reader.GetString(25);
                        }

                        if (!v_Data_Reader.IsDBNull(26))
                        {
                            v_purchase_info.pay_type_code = v_Data_Reader.GetString(26);
                        }

                        if (!v_Data_Reader.IsDBNull(27))
                        {
                            v_purchase_info.catalog_id = v_Data_Reader.GetString(27);
                        }


                        v_purchase_info.logo = Convert.ToString(v_Data_Reader["LOGO"]);


                        string _sql2 = "select to_char(trunc(t.start_date),'YYYY/MM/DD')  start_date," +
"to_char(trunc(t.end_date),'YYYY/MM/DD') end_date," +
"decode(t.start_date,null,'未啟用',decode(sign(end_date-sysdate),1,'已啟用','已到期') ) package_status,t2.package_start_date_desc,t2.package_end_date_desc ,decode(t.start_date,null,'N',decode(sign(end_date-sysdate),1,'Y','N') ) status " +
" from bsm_client_details t,bsm_package_mas t2 " +
" where t.status_flg = 'P' " +
"and t.mac_address =:mac_address " +
"and t.package_id = :package_id and t2.package_id= t.package_id";

                        OracleCommand _cmd2 = new OracleCommand(_sql2, conn);
                        _cmd2.BindByName = true;
                        _cmd2.Parameters.Add("mac_address", client_id);
                        _cmd2.Parameters.Add("package_id", v_purchase_info.package_id);
                        
                        OracleDataReader v_Data_Reader2 = _cmd2.ExecuteReader();
                        try
                        {

                            v_purchase_info.use_status = "N";
                            while (v_Data_Reader2.Read()) v_purchase_info.use_status = Convert.ToString(v_Data_Reader2["PACKAGE_STATUS"]);
                            v_purchase_info.purchase_datetime = v_Data_Reader.GetString(11);


                            v_result.Add(v_purchase_info);
                            _i++;
                        }
                        finally
                        {
                            v_Data_Reader2.Dispose();
                        }

                    }
                }
                finally
                {
                    v_Data_Reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
   
            }
            return v_result;
        }


        /// <summary>
        /// 取各Group 方案
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <param name="device_id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public List<group> get_group_package_info(string token, string client_id, string device_id,string imsi,string sw_version)
        {
            process_auto_coupon(client_id, device_id, sw_version);

            List<group> _result = new List<group>();
            group _g;
            string _sw_group = "";

           

            if (sw_version != null && sw_version != "") { _sw_group = sw_version.Substring(1, 7); }
            else
            {
                try
                {
                    connectDB();
                    string _sql = "SELECT GET_SOFTWARE_GROUP(:CLIENT_ID,:DEVICE_ID) FROM DUAL";
                    OracleCommand _cmd = new OracleCommand(_sql, conn);
                    _cmd.BindByName = true;
                    _cmd.Parameters.Add("CLIENT_ID", client_id);
                    _cmd.Parameters.Add("DEVICE_ID", device_id);
                    OracleDataReader _rd = _cmd.ExecuteReader();
                    if (_rd.Read())
                    {
                        if (!_rd.IsDBNull(0))
                        {
                            _sw_group = _rd.GetString(0);
                        }
                    }
                    _cmd.Dispose();
                }
                finally
                {
   
                }
            }
            List<package_info> _package_info_list = this.get_package_info(client_id, "BUY", 0, device_id,null,imsi,sw_version,"N","P");


            var _package_group_list = from _package_info in _package_info_list
                                      orderby _package_info.display_order, _package_info.catalog_id
                                      group _package_info by new { _package_info.catalog_id, _package_info.catalog_description, _package_info.catalog_ref } into g
                                      select new { group_id = g.Key.catalog_id, title = g.Key.catalog_description, group_description = g.Key.catalog_ref };

            foreach ( var _pg in _package_group_list)
            {
                 _g = new group();
                 _g.group_id = _pg.group_id;

                 _g.title = _pg.title;
                 _g.group_description = _pg.group_description;
                 _g.packages = (from _package_info in _package_info_list where _package_info.catalog_id == _g.group_id select _package_info).ToList<package_info>();
                _result.Add(_g);
            };
       
            return _result;

        }

        /// <summary>
        /// 取所有方案
        /// Sample :
        ///  { "client_id" : "0080C8001002" }
        /// [{"catalog_id":"HIDO","catalog_description":"Hido\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00001","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"IFILM","catalog_description":"iFilm\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00002","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"KOD","catalog_description":"\u7f8e\u83ef\u5361\u62c9OK","package_name":"\u4e00\u500b\u6708","package_id":"K00001","price_description":"\u6bcf\u670869\u5143"}]
        /// </summary>
        /// 

        public List<package_info> get_package_info(string client_id, string system_type, string device_id,string imsi,string sw_version,string cal_type)
        {
            return get_package_info(client_id, system_type, 0, device_id, null,imsi,sw_version,"N",cal_type);
        }

        public List<package_sg_info> get_sg_package(string sw_group,string sw_version)
        {
            var _collection = _MongoDB_package.GetCollection<package_sg_info>("sg_package");
            List<package_sg_info> d_list;
            try
            {
                d_list = _collection.AsQueryable().ToList();
            }
            catch (Exception e)
            {
                d_list = new  List<package_sg_info>();
            }
            if (d_list.Count() == 0)
            {
                OracleCommand _cmd = new OracleCommand(@"Select software_group||'+'||package_id ""_ID"",package_id,software_group,version,version_end from bsm_package_sg where status_flg='P'", conn);

                OracleDataReader rd = _cmd.ExecuteReader();
                List<package_sg_info> all_package_sg_info = DataReaderToObjectList<package_sg_info>(rd).ToList();
                try{
                _collection.InsertBatch(all_package_sg_info);
                }catch(Exception e)
                {
                }

                List<package_sg_info> _result = (from a in all_package_sg_info where a.software_group == sw_group && (a.version == null || String.Compare(a.version, sw_version) >= 0) && (a.version_end == null || String.Compare(a.version_end, sw_version) <= 0) select a).ToList();
                return _result;
            }
            else
            {
                List<package_sg_info> _result = (from a in d_list where a.software_group == sw_group && (a.version == null || String.Compare(a.version, sw_version) <= 0) && (a.version_end == null || String.Compare(a.version_end, sw_version) >= 0) select a).ToList();
                return _result;
            }
        }
   
        private List<package_service_info> get_all_package_service()
        {
            List<package_service_info> _result = new List<package_service_info>() ;

            var _collection = _MongoDB_package.GetCollection<package_service_info>("package_services"); 
            List<package_service_info> d_list;

            try
            {
              
                d_list = _collection.AsQueryable().ToList();
            }
            catch (Exception e)
            {
                d_list = new List<package_service_info>();
            }
            if (d_list.Count() == 0)
            {
                connectDB();
                try
                {
                    string _sql = @"Select package_id,a.cat_id,name,description
from service_cat_mas a,bsm_package_service b
where a.cat_id=b.cat_id and b.status_flg='P'";

                    OracleCommand _cmd = new OracleCommand(_sql, conn);
                    _cmd.BindByName = true;
                    OracleDataReader _Data_Reader = _cmd.ExecuteReader();
                    while (_Data_Reader.Read())
                    {
                        package_service_info _p = new package_service_info();

                        _p.service_id = Convert.ToString(_Data_Reader["CAT_ID"]);
                        _p.package_id = Convert.ToString(_Data_Reader["PACKAGE_ID"]);
                        _p._id = _p.service_id + "+" + _p.package_id;
                        _p.description = Convert.ToString(_Data_Reader["DESCRIPTION"]);
                        _p.name = Convert.ToString(_Data_Reader["NAME"]);
                        _result.Add(_p);
                    }
                }
                finally
                {
                    conn.Close();
                }
                  

                if (_result.Count() >0)

                _collection.InsertBatch(_result);
            }
            else
            {
                _result = d_list;
            }
               return _result;
        }
   
        private List<package_info> get_all_package()
        {
            List<package_info> v_result = new List<package_info>();
            var package_collection = _MongoDB_package.GetCollection<package_info>("packages");
            List<package_info> d_list;
            try
            {
                d_list = package_collection.AsQueryable().ToList();
            }
            catch (Exception e)
            {
                d_list = new List<package_info>();
            }
            if (d_list.Count == 0)
            {

                //20150104
                List<package_service_info> _services = get_all_package_service();

                if (conn.State == ConnectionState.Closed) conn.Open();

                string _sql = @"Select 
       t2.system_type,
       t2.package_id,
       t2.description package_name,
       nvl(t2.package_cat3,t2.package_cat1) cat,
       t2.package_cat_id1,
       t2.price_des,
       t2.charge_amount   price,
       t2.logo,
       t2.package_des_html,
       t2.package_des,
       t2.package_start_date_desc,
       t2.package_end_date_desc,
       t2.package_des_text,
       t2.credits_des,
       nvl(t2.credits,0) credits,
       t2.package_cat_id1,
       t2.cal_type,
       t2.recurrent recurrent,
       t2.ref2,
        t2.ref3,
        t2.ref6,
        APT_ProductCode,
        APT_Only ,
        Pay_credits_type,
        nvl(t2.display_order,0) display_order,
        t2.ref5 category_ref,
        ios_product_code,
        qr_code_url,
        qr_code_desc
        FROM  bsm_package_mas t2
        WHERE  t2.acl_period_duration is null
        and status_flg='P'
        ORDER BY  t2.package_cat_id1,t2.package_id";

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                _cmd.BindByName = true;
                OracleDataReader _Data_Reader = _cmd.ExecuteReader();
                while (_Data_Reader.Read())
                {
                    package_info v_package_info = new package_info();
                    v_package_info._id = Convert.ToString(_Data_Reader["PACKAGE_ID"]);
                    v_package_info.status_description = "未購買";
                    v_package_info.system_type = Convert.ToString(_Data_Reader["SYSTEM_TYPE"]);
                    v_package_info.package_id = Convert.ToString(_Data_Reader["PACKAGE_ID"]);
                    v_package_info.ref2 = Convert.ToString(_Data_Reader["REF2"]);
                    v_package_info.ref3 = Convert.ToString(_Data_Reader["REF3"]);
                    v_package_info.ref6 = Convert.ToString(_Data_Reader["REF6"]);
                    v_package_info.package_name = Convert.ToString(_Data_Reader["PACKAGE_NAME"]);
                    v_package_info.catalog_description = Convert.ToString(_Data_Reader["CAT"]);
                    v_package_info.catalog_ref = Convert.ToString(_Data_Reader["CATEGORY_REF"]);
                    v_package_info.catalog_id = Convert.ToString(_Data_Reader["PACKAGE_CAT_ID1"]);
                    v_package_info.price_description = Convert.ToString(_Data_Reader["PRICE_DES"]);
                    v_package_info.price = Convert.ToString(_Data_Reader["PRICE"]);
                    v_package_info.start_date = Convert.ToString(_Data_Reader["PACKAGE_START_DATE_DESC"]);
                    v_package_info.end_date = Convert.ToString(_Data_Reader["PACKAGE_END_DATE_DESC"]);
                    v_package_info.package_description_dtl = Convert.ToString(_Data_Reader["PACKAGE_DES"]);
                    v_package_info.logo = Convert.ToString(_Data_Reader["LOGO"]);
                    v_package_info.package_description = Convert.ToString(_Data_Reader["PACKAGE_DES_HTML"]);
                    v_package_info.package_description_text = Convert.ToString(_Data_Reader["PACKAGE_DES_TEXT"]);
                    v_package_info.credits = Convert.ToDecimal(_Data_Reader["CREDITS"]);
                    v_package_info.cal_type = Convert.ToString(_Data_Reader["CAL_TYPE"]);
                    v_package_info.recurrent = Convert.ToString(_Data_Reader["RECURRENT"]);
                    v_package_info.recurrent_list = (v_package_info.catalog_id == "KOD" || v_package_info.catalog_id == "HIKIDS") ? (v_package_info.recurrent == "R") ? "RO" : "O" : (v_package_info.recurrent == "R") ? "R" : "O";
                    v_package_info.apt_only = Convert.ToString(_Data_Reader["APT_ONLY"]);
                    v_package_info.cost_credits = Convert.ToString(_Data_Reader["CREDITS_DES"]);
                    v_package_info.credits_description = Convert.ToString(_Data_Reader["CREDITS_DES"]);
                    v_package_info.credits_type = Convert.ToString(_Data_Reader["PAY_CREDITS_TYPE"]);
                    v_package_info.display_order = Convert.ToDecimal(_Data_Reader["DISPLAY_ORDER"]);
                    v_package_info.ios_product_code = Convert.ToString(_Data_Reader["IOS_PRODUCT_CODE"]);
                    v_package_info.qr_code_desc = Convert.ToString(_Data_Reader["QR_CODE_URL"]);
                    v_package_info.qr_code_url = Convert.ToString(_Data_Reader["QR_CODE_DESC"]);

                    if (v_package_info.system_type == "CREDITS")
                    {
                        v_package_info.remark = "購買LiTV點數之面額最低為500元,每次購買面額回饋10%點數,例如購買500元可使用550點,依此類推";
                    }

                    //2015 01 14
                    v_package_info.services = (from _a in _services where _a.package_id == v_package_info.package_id select _a).ToList();

                    v_result.Add(v_package_info);

                }

                try
                {

                    package_collection.InsertBatch(v_result);
                }
                catch (Exception e)
                {
                }
            }
            else
            {
                v_result = d_list;
            }

            return v_result;
        }

        public account_info get_account_info(string client_id,string device_id)
        {
            account_info acc_info;
            if (_MongoDB == null) { return null; }
            var account_collection = _MongoDB.GetCollection<account_info>("account_info");
            
            try
            {
                acc_info= (account_info)account_collection.FindOne(Query.EQ("_id", new BsonString(client_id)));
            }
            catch(Exception e)
            {
                return null;
            }
            if (acc_info != null)
            {
                return acc_info;
            }
            else
            {

            account_info _acc_info = new account_info();
            _acc_info._id = client_id;
            _acc_info.client_id = client_id;
          
            _acc_info.package_details = get_client_detail_oracle(client_id);
            _acc_info.credits = get_credits_balance(client_id);
            _acc_info.purchase_dtls = get_purchase_info_oracle(client_id);
            _acc_info.services = get_catalog_info_oracle(client_id, device_id, null);
            _acc_info.activation_code = get_activation_code_oracle(client_id);
            return _acc_info;
            }

       //     account_collection.Save(_acc_info);

            


        }

        public string refresh_client(string client_id)
        {
            var account_collection = _MongoDB.GetCollection<account_info>("account_info");
            account_collection.Remove(Query.EQ("_id", new BsonString(client_id)));
       //     get_account_info(client_id,null);
            return "Sucess";
           
        }

        public string catch_client(string client_id,string device_id)
        {
           account_info  acc_info =get_account_info(client_id,device_id);

            return "Sucess";

        }

        public string catch_client_all()
        {
      /*      OracleConnection conn2 = new OracleConnection();
            conn2.ConnectionString = conn.ConnectionString;
            conn2.Open();
            string _sql = "SELECT serial_id from bsm_client_mas where status_flg = 'A' and serial_id like '2A%'";
            OracleCommand cmd = new OracleCommand(_sql, conn2);
            cmd.BindByName = true;

            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                catch_client(Convert.ToString(rd["SERIAL_ID"]), null);
            } */
            return "Surcess";
        }



        private List<client_detail> get_client_detail(string client_id, string device_id)
        {
            List<client_detail> _result = new List<client_detail>();

            account_info acc_info = get_account_info(client_id, device_id);

            if (acc_info != null) _result =  acc_info.package_details;
            else
                _result = get_client_detail_oracle(client_id);

            if (device_id == null) device_id = "";

            _result = (from dtl in _result where dtl.device_id == "" || dtl.device_id == null || dtl.device_id==device_id select dtl).ToList();

            return _result;
        }


        private List<client_detail> get_client_detail_oracle(string client_id)
        {
            List<client_detail> _result = new List<client_detail>();

            connectDB();
            try
            {


                string _sql = @"select t3.pay_type,t.src_no,to_char(trunc(t.start_date),'yyyy/mm/dd')  start_date,
        to_char(trunc(t.end_date),'yyyy/mm/dd') end_date,
        decode(t.start_date,null,'未啟用',decode(sign(end_date-sysdate),1,'已啟用','已到期') ) package_status,
        t2.package_start_date_desc,
        t2.package_end_date_desc ,
        decode(t.start_date,null,'N',decode(sign(end_date-sysdate),1,'Y','N') ) used,
        t2.package_id,
        t2.package_cat_id1,
        t3.recurrent,
        t.device_id
        from bsm_client_details t,bsm_package_mas t2 ,bsm_purchase_mas t3
        where t.status_flg = 'P'
        and t2.package_id=t.package_id
        and t3.mas_no(+) = t.src_no
        and t2.acl_period_duration is null 
        and t.mac_address =:client_id
        and t.end_date >= sysdate 
        order by end_date";

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                _cmd.BindByName = true;
                _cmd.Parameters.Add("CLIENT_ID", client_id);
                OracleDataReader _rd = _cmd.ExecuteReader();

                while (_rd.Read())
                {
                    client_detail _detail = new client_detail();
                    _detail.src_no = Convert.ToString(_rd["SRC_NO"]);
                    _detail.src_pay_type = Convert.ToString(_rd["PAY_TYPE"]);
                    _detail.cat_id = Convert.ToString(_rd["PACKAGE_CAT_ID1"]);
                    _detail.package_id = Convert.ToString(_rd["PACKAGE_ID"]);
                    _detail.start_date = Convert.ToString(_rd["START_DATE"]);
                    _detail.end_date = Convert.ToString(_rd["END_DATE"]);
                    _detail.package_start_date_desc = Convert.ToString(_rd["PACKAGE_START_DATE_DESC"]);
                    _detail.package_end_date_desc = Convert.ToString(_rd["PACKAGE_END_DATE_DESC"]);
                    _detail.package_status = Convert.ToString(_rd["PACKAGE_STATUS"]);
                    _detail.used = Convert.ToString(_rd["USED"]);
                    _detail.recurrent = Convert.ToString(_rd["RECURRENT"]);
                    _detail.device_id = Convert.ToString(_rd["DEVICE_ID"]);
                    _result.Add(_detail);
                }
            }
            finally
            {
                conn.Close();
            }

            
            return _result;
         }

        public List<package_info> get_package_info(string client_id, string system_type, int? min_credits, string device_id, string group_id, string imsi, string sw_version,string from_credits,string cal_type)
        {
            if (group_id == "CREDITS") system_type = "CREDITS";
            List<package_info> v_result = new List<package_info>();
            List<package_info> all_packages = new List<package_info>();

            try
            {
                string sw_group = "";
                if (sw_version == null)
                {
                    connectDB();
                    try
                    {
                        if (device_id == null && sw_version == null)
                        {

                            OracleCommand _cmd = new OracleCommand(@"Select software_group from bsm_client_device_list where client_id=:MAC_ADDRESS and software_group <> 'LTWEB00' and rownum <=1", conn);
                            _cmd.BindByName = true;
                            _cmd.Parameters.Add("MAC_ADDRESS", client_id);
                            OracleDataReader rd_sw = _cmd.ExecuteReader();
                            if (rd_sw.Read())
                            {
                                sw_group = Convert.ToString(rd_sw["SOFTWARE_GROUP"]);
                            }
                        }
                        else
                        {
                            OracleCommand _cmd = new OracleCommand(@"Select software_group from bsm_client_device_list where client_id=:MAC_ADDRESS and device_id=:DEVICE_ID and rownum <=1", conn);
                            _cmd.BindByName = true;
                            _cmd.Parameters.Add("MAC_ADDRESS", client_id);
                            _cmd.Parameters.Add("DEVICE_ID", device_id);
                            OracleDataReader rd_sw = _cmd.ExecuteReader();
                            if (rd_sw.Read())
                            {
                                sw_group = Convert.ToString(rd_sw["SOFTWARE_GROUP"]);
                            }
                            else
                            {
                                OracleCommand _cmd_2 = new OracleCommand(@"select  nvl(get_device_current_swver(:MAC_ADDRESS,:DEVICE_ID),t7.software_ver)  CURR_VERSION
                   from bsm_client_mas t7
                  where t7.mac_address = :MAC_ADDRESS", conn);
                                _cmd.BindByName = true;
                                _cmd.Parameters.Add("MAC_ADDRESS", client_id);
                                _cmd.Parameters.Add("DEVICE_ID", device_id);
                                OracleDataReader rd_sw_2 = _cmd_2.ExecuteReader();
                                if (rd_sw.Read())
                                {
                                    sw_version = Convert.ToString(rd_sw_2["CURR_VERSION"]);
                                    if (sw_version != null && sw_version != "") sw_group = sw_version.Substring(0, 7);
                                }
                            }
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
                else
                {
                     sw_group = sw_version.Substring(0, 7);
                }

                software_packages sg_p=null;
                
                if (_MongoDB_package != null)
                {
                    var sg_package_collection = _MongoDB_package.GetCollection<software_packages>("software_packages");
                   

                    try
                    {
                        sg_p = (software_packages)sg_package_collection.FindOne(Query.EQ("_id", new BsonString(system_type + "+" + sw_group + "+" + group_id)));
                    }
                    catch (Exception e)
                    {
                        sg_p = null;
                    }
                }

                if (sg_p != null)
                {
                    all_packages = sg_p.packages;
                }
                else
                {
                    all_packages = get_all_package();
                    List<package_sg_info> sg_packages = get_sg_package(sw_group, sw_version);

                    //filter SYSTYEM_TYPE
                    all_packages = (from a in all_packages where a.system_type == system_type select a).ToList();
                    foreach (var a in all_packages)
                    {
                        a.system_type = null;


                        if (a.ref6 != null && a.ref6 != "")
                        {
                            a.package_description = a.ref6;
                            a.package_name = a.package_description;
                            a.ref6 = null;
   
                        }
                        else if (a.ref2 != null && a.ref2 != "")
                        {
                            a.package_description = a.ref2 + " " + a.ref3;
                            a.package_name = a.package_description;
                            a.ref2 = null;
                            a.ref3 = null;
                        }
                    }


                    if (!(system_type == "CREDITS" && from_credits == "Y")) all_packages = (from x in all_packages orderby x.display_order, x.package_id where (sg_packages.Exists(y => (y.package_id == x.package_id || y.package_id == x.catalog_id))) select x).ToList();

                    //filter GROUP_ID
                    if (group_id != "" && group_id != null) all_packages = (from a in all_packages where a.catalog_id == group_id select a).ToList();
                    sg_p = new software_packages();
                    sg_p._id = system_type + "+" + sw_group + "+" + group_id;
                    sg_p.packages = all_packages;
                    var sg_package_collection = _MongoDB_package.GetCollection<software_packages>("software_packages");
                    sg_package_collection.Save(sg_p);
                }

                if (cal_type == "T")
                {
                    all_packages = (from _c in all_packages where (_c.cal_type == "T") select _c).ToList();
                }
                else
                {
                    all_packages = (from _c in all_packages where (_c.cal_type == "P") select _c).ToList();
                }




                if (client_id != "" && client_id != null)
                {
                    account_info acc_info = get_account_info(client_id, device_id);
                    List<client_detail> _client_details = get_client_detail(client_id, device_id);
                        
                    foreach (var _a in all_packages)
                    {
                        List<client_detail> _detail_packages = (from _b in _client_details where _b.cat_id == _a.catalog_id select _b).ToList();
                        string start_date = (from _b in _detail_packages select _b.start_date).Min();
                        string end_date =  (from _b in _detail_packages select _b.end_date).Max();
                        string start_date_desc = (from _b in _detail_packages select _b.package_start_date_desc).Max();
                        string end_date_desc = (from _b in _detail_packages select _b.package_end_date_desc).Max();
                        string used = (from _b in _detail_packages select _b.used).Max();
                        string package_status = (from _b in _detail_packages select _b.package_status).Max();
                        _a.start_date = (start_date == "" || start_date == null) ? start_date_desc : start_date;
                        _a.end_date = (end_date == "" || end_date == null) ? end_date_desc : end_date;
                        _a.use_status = used ?? "N";
                        _a.status_description = package_status??"未購買";
                        _a.current_recurrent_status = ((from a in _detail_packages where a.recurrent == "R" select a).Count() > 0) ? "R" : "O";
                        _a.current_recurrent_package = ((from a in _detail_packages where a.recurrent == "R" && a.package_id==_a.package_id select a).Count() > 0) ? "Y" : "N";

                        _a.next_pay_date = (_a.current_recurrent_status == "R") ? end_date : null;
                        _a.package_status = (_a.current_recurrent_status == "R") ? "N" : "Y";
                        _a.package_status_message = (_a.current_recurrent_status == "R") ? "已使用自動扣款" : null; 

                        List<string> _pay_method = new List<string>();
                        _pay_method.Add("CREDIT");

                        if (_a.apt_only == "Y" && this.check_apt_user(imsi) == "Y")   _pay_method.Add("APT");

                        string v_credits_type = _a.credits_type;
                        List<string> v_credits_type_list = new List<string>(v_credits_type.Split(','));
                        {
                            credits_balance_info _credit_b;
                            credits_balance_info _credit_c;

                            if (acc_info != null)
                            {
                                _credit_b = acc_info.credits;
                                _credit_c = acc_info.credits;
                            }
                            else
                            {
                                _credit_b = get_credits_balance(client_id);
                                _credit_c = get_credits_balance(client_id);
                            }
                            decimal _credits = _a.credits;
                            decimal _after_credits = (decimal)_credit_b.credits - _credits;

                            _a.after_credits = _after_credits.ToString() + "點";
                            _a.credits_balance = _after_credits;
                            _a.credits_info = _credit_b;

                            decimal _b = _credits;
                            foreach (var _c_value in _credit_c.details)
                            {
                                _c_value.use_credits = 0;
                                if (v_credits_type_list.Contains(_c_value.credits_type))
                                {
                                    if (_c_value.credits >= _b && _c_value.credits > 0)
                                    {
                                        _c_value.credits -= (int?)_b;
                                        _credit_c.credits -= (int?)_b;
                                        _c_value.use_credits = (int?)_b;
                                        _b = 0;
                                    }
                                    else if (_c_value.credits < _b && _c_value.credits > 0)
                                    {
                                        int? _c_credits = _c_value.credits;
                                        _c_value.credits -= _c_credits;
                                        _credit_c.credits -= _c_credits;
                                        _c_value.use_credits = (int?)_c_credits;
                                        _b -= (decimal)_c_credits;
                                    }
                                }
                            }

                            if (v_credits_type_list.Count > 0)
                            {
                                if (_b <= 0) _pay_method.Add("CREDITS");
                            }

                            _a.credits_balance_info = _credit_c;
                        };

                        _a.pay_method_list = _pay_method.ToArray();
                    }
                }
            }
            finally
            {
                conn.Close();

            }
            v_result = all_packages;
            
            return v_result;
        }

        public JsonArray get_package_info_a(string client_id, string system_type, int? min_credits, string device_id, string group_id, string imsi, string sw_version, string from_credits, string cal_type)
        {
            List<package_info> all_packages = get_package_info(client_id,system_type,min_credits, device_id,group_id,imsi, sw_version,from_credits, cal_type);
            JsonObject package = new JsonObject();
            JsonArray _result_a = new JsonArray();
            List<bsm_package_special> _all_special = get_package_special();
           
                foreach (var item in all_packages)
                {
                    package = (JsonObject)JsonConvert.Import(JsonConvert.ExportToString(item));

                    List<bsm_package_special> specials = (from c in _all_special where c.package_id == item.package_id && (DateTime.Compare(c.start_date, DateTime.Now) <= 0 && DateTime.Compare(DateTime.Now, c.end_date) <= 0) select c).ToList();
                    foreach (var special in specials)
                    {
                        JsonObject _option = special.Option;
                        foreach (string name in _option.Names)
                        {
                            if (package.Contains(name))
                            {
                                package[name] = _option[name];
                            }
                            else
                            {
                                package.Add(name, _option[name]);
                            }
                        }
                    }
                    _result_a.Add(package);
                }
            return _result_a;
        }


        public List<bsm_package_special> get_package_special()
        {
            var _collection = _MongoDB_package.GetCollection<bsm_package_special>("bsm_package_special");
            List<bsm_package_special> _result = _collection.AsQueryable().ToList();
            return _result;
        }
        public void post_package_special()
        {
            var _collection = _MongoDB_package.GetCollection<bsm_package_special>("bsm_package_special");
            bsm_package_special _data = new bsm_package_special();
            connectDB();

            string sql = @"select pk_no id,x.src_id,(start_date+8/24) start_date,(end_date+8/24) end_date,x.pc_option from bsm_package_special_setting x where type = 'PACKAGE' and status_flg in ('P','Z')";
            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                _data._id = Convert.ToString(rd["ID"]);
                _data.package_id = Convert.ToString(rd["SRC_ID"]);
                _data.start_date = Convert.ToDateTime(rd["START_DATE"]);
                _data.end_date = Convert.ToDateTime(rd["END_DATE"]);
                _data.Option = (JsonObject)JsonConvert.Import(Convert.ToString(rd["PC_OPTION"]));
                _collection.Save(_data);
            };
        }
        public class bsm_message
        {
            public string _id;
            public string message;
            public string message_type;
        }

        public string get_message(string message_type, string sw_version)
        {
            string _result = "";
            sw_version = null;
            string software_group = (sw_version != null )?sw_version.Substring(0, 7):"";
    
            var bsm_message_collection = _MongoDB_package.GetCollection<bsm_message>("bsm_messages");
            var query = Query.EQ("_id",message_type);
            bsm_message re= bsm_message_collection.FindOne(query);
            if (re != null)
            {
                _result = re.message;
            }
            else
            {
                connectDB();
                try
                {
                    string _sql = (software_group == null) ? "select message,message_type from BSM_MESSAGE_MAS t where message_type =:MESSAGE_TYPE and software_group is null" : "select message,message_type from BSM_MESSAGE_MAS t where message_type =:MESSAGE_TYPE and software_group ='" + software_group + "'";
                    OracleCommand _cmd = new OracleCommand(_sql, conn);
                    _cmd.BindByName = true;
                    _cmd.Parameters.Add("MESSAGE_TYPE", message_type);
                    OracleDataReader _reader = _cmd.ExecuteReader();
                    _result = _reader.Read() ? Convert.ToString(_reader["MESSAGE"]) : null;
                    if (_result == null)
                    {
                        _sql = "select message,message_type from BSM_CREDIT_MESSAGE t where message_type =:MESSAGE_TYPE";
                        _cmd = new OracleCommand(_sql, conn);
                        _cmd.BindByName = true;
                        _cmd.Parameters.Add("MESSAGE_TYPE", message_type);
                        _reader = _cmd.ExecuteReader();
                        _result = _reader.Read() ? Convert.ToString(_reader["MESSAGE"]) : null;
                    };
                    _cmd.Dispose();

                    bsm_message msg = new bsm_message();
                    msg.message = _result;
                    msg.message_type = message_type;
                    msg._id = message_type;
                    bsm_message_collection.Save(msg);
                }
                finally
                {
                    conn.Close();
                }
            }

            return _result;
        }


        /// <summary>
        /// 取信用卡訊息
        /// </summary>
        /// <returns></returns>
        public string get_credit_message()
        {
            return get_message("CREDIT", null);
        }

        /// <summary>
        /// 取COUPON訊息
        /// </summary>
        /// <returns></returns>
        public string get_coupon_message()
        {
            return get_message("COUPON", null);
        }

        /// <summary>
        /// 取儲值卡訊息
        /// </summary>
        /// <returns></returns>
        public string get_credits_message()
        {
            return get_message("CREDITS", null);
        }

        /// <summary>
        /// 取儲值卡訊息
        /// </summary>
        /// <returns></returns>
        public string get_dsp_message(string client_id)
        {
            return get_message("DSP", null);
        }

        /// <summary>
        /// 取Recurrent Contract
        /// </summary>
        /// <returns></returns>
        public string get_recurrent_contract()
        {
            return get_message("RECURRENT_CNT", null);
        }
        /// <summary>
        /// 取Already Recurrent Contract
        /// </summary>
        /// <returns></returns>
        public string get_already_recurrent_message()
        {
            return get_message("RECURRENT_ALREADY", null);
        }

        public string get_onetime_message()
        {
            return get_message("ONETIME_ALREADY", null);
        }
        /// <summary>
        /// 取Content package 資訊
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <param name="content_id"></param>
        /// <returns></returns>
        public List<package_info> get_content_package_info(string client_id, string content_id, string device_id,string imsi,string sw_version)
        {
            List<package_info> v_result = new List<package_info>();
            List<package_info> _package_info_list;

            _package_info_list = this.get_package_info(client_id, "BUY", 0, device_id, null, imsi,sw_version,"N","P");
            string _sql;
            client_id = client_id.ToUpper();

            connectDB();
            try
            {
                /* select content package */
                if (content_id == "KOD")
                {
                    _sql = @"Select package_id from bsm_package_mas t2 where  system_type = 'BUY'  and status_flg='P' and t2.package_cat_id1=:content_id ";
                }
                else
                {
                    _sql = @"select c.package_id
  from mid_cms_content  a,
       mid_cms_item_rel b,
       mid_cms_item     c,
       bsm_package_mas  d
 where b.mas_pk_no = a.pk_no
   and b.type = 'P'
   and c.pk_no = b.detail_pk_no
   and a.content_id = :CONTENT_ID
   and d.package_id = c.package_id";
                }

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                _cmd.BindByName = true;
                _cmd.Parameters.Add("CONTENT_ID", content_id);
                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();
                
                while (v_Data_Reader.Read())
                {
                    string _package_id = Convert.ToString(v_Data_Reader["PACKAGE_ID"]);
                    List<package_info> _package_list = (from p in _package_info_list where p.package_id == _package_id select p).ToList();
                    v_result.AddRange(_package_list);
                }

                foreach(var _package_info in _package_info_list)
                {
                    int _cnt = (from p in v_result where p.package_id == _package_info.package_id select p).Count();
                    if (_cnt == 0)
                    {
                        v_result.Add(_package_info);
                    }
                }

            }
            finally
            {
                conn.Close();

            }
            return v_result;
        }

        /// <summary>
        /// 取package detail 的內容
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <param name="package_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public BSM_Info.content_package_info get_package_detail(string client_id, string package_id, string item_id, string p_content_id,string imsi,string sw_version)
        {

            if (package_id != null && package_id != "" && p_content_id == null)
            {
                string[] _str = package_id.Split(',');
                package_id = _str[0];
                if (_str.Length > 1)
                {
                    p_content_id = _str[1];
                }
            }

            if (p_content_id != "" && p_content_id != null)
            {
                List<package_info> v_package_list = get_content_package_info(client_id, p_content_id, null,imsi,sw_version);
                if (v_package_list.Count > 0)
                {
                    package_id = v_package_list[0].package_id;
                    item_id = v_package_list[0].item_id;
                }
            }

            content_package_info v_result = new content_package_info();
            package_info v_package_info = new package_info();
            content_info v_content_info = new content_info();

            client_id = client_id.ToUpper();

            string _sql;

            connectDB();

            try
            {
                if ((item_id != "" && item_id != null) || p_content_id != null)
                {
                    //
                    // get content info
                    //

                    if (item_id == "" || item_id == null)
                    { item_id = p_content_id; }
                    _sql = "select a.title,a.content_id,decode(a.sdhd,'SD','SD 畫質','HD','HD 高畫質','一般畫質') sdhd,a.rating,a.genre,a.runtime,a.off_shelf_date ,a.eng_title,to_number(nvl(a.score,0))/2,release_year||'出品',to_number(nvl(a.score,0)) from mid_cms_content a " +
                    "where pk_no in (select mas_pk_no from mid_cms_item_rel a " +
                    "where a.detail_pk_no in (select pk_no from mid_cms_item where item_id=:item_id)) or content_id=:content_id";

                    OracleCommand _cmd_content = new OracleCommand(_sql, conn);
                    _cmd_content.Parameters.Add("item_id", item_id);
                    _cmd_content.Parameters.Add("content_id", item_id);
                    OracleDataReader v_dr_content = _cmd_content.ExecuteReader();

                    try
                    {
                        if (v_dr_content.Read())
                        {
                            if (!v_dr_content.IsDBNull(0))
                            {
                                v_content_info.title = v_dr_content.GetString(0);
                            }
                            if (!v_dr_content.IsDBNull(1))
                            {
                                v_content_info.content_id = v_dr_content.GetString(1);
                            }

                            if (!v_dr_content.IsDBNull(2))
                            {
                                v_content_info.sdhd = v_dr_content.GetString(2);
                            }

                            if (!v_dr_content.IsDBNull(3))
                            {
                                v_content_info.rating = v_dr_content.GetString(3);
                            }
                            if (!v_dr_content.IsDBNull(4))
                            {
                                v_content_info.genre = v_dr_content.GetString(4);
                            }
                            if (!v_dr_content.IsDBNull(5))
                            {
                                v_content_info.runtime = v_dr_content.GetString(5);
                            }
                            if (!v_dr_content.IsDBNull(6))
                            {
                                v_content_info.off_shelf_date = v_dr_content.GetString(6);
                            }

                            if (!v_dr_content.IsDBNull(7))
                            {
                                v_content_info.eng_title = v_dr_content.GetString(7);
                            }

                            if (!v_dr_content.IsDBNull(8))
                            {
                                v_content_info.score = v_dr_content.GetDecimal(8);
                            }


                            if (!v_dr_content.IsDBNull(9))
                            {
                                v_content_info.release_year = v_dr_content.GetString(9);
                            }


                            if (!v_dr_content.IsDBNull(10))
                            {
                                v_content_info.score10 = v_dr_content.GetDecimal(10);
                            }


                            v_content_info.remark = " ";

                        }
                    }
                    finally
                    {
                        v_dr_content.Dispose();
                        _cmd_content.Dispose();
                    }
                }

                //
                // get package info 
                //

                if (v_content_info.content_id != null)
                {
                    _sql = @"select t2.package_id,t2.description  package_name,t2.package_cat1 cat,t2.package_cat_id1,t2.price_des,t2.charge_amount   price, 'http://streaming01.tw.svc.litv.tv/'||replace(c.main_picture,'http://streaming01.tw.svc.litv.tv/','') logo,t2.package_des_html, c.content_id item_id,t2.package_type,t2.package_start_date_desc,t2.package_end_date_desc,t2.credits_des,t2.credits,to_char(t2.duration_by_day)||'天' ,
                             t2.recurrent,
                             decode(t2.recurrent,'R',decode(BSM_RECURRENT_UTIL.check_recurrent(t2.package_cat_id1, :MAC_ADDRESS),'N','O','R'),'O') current_recurrent_status,
                             decode(BSM_RECURRENT_UTIL.check_recurrent(t2.package_cat_id1, :MAC_ADDRESS),'Y',to_char(BSM_RECURRENT_UTIL.get_service_end_date(t2.package_cat_id1, :MAC_ADDRESS),'YYYY/MM/DD'),'無') next_pay_date
   from mid_cms_content c,mid_cms_item_rel a, mid_cms_item b,bsm_package_mas t2 
 where t2.system_type in ('BUY','CREDITS') 
   and t2.status_flg = 'P' 
   and a.mas_pk_no = c.pk_no 
   and type = 'P' 
   and b.pk_no = a.detail_pk_no 
   and t2.package_id = b.package_id ";

                }
                else
                {
                    _sql = @"select t2.package_id, package_name,cat,t2.package_cat_id1,t2.price_des, price, t2.logo,t2.package_des_html, '' item_id,t2.package_type,t2.package_start_date_desc,t2.package_end_date_desc ,t2.credits_des,t2.credits, day ,
                                                     'O' ,
                             'O' current_recurrent_status,
                             '無' next_pay_date
                        from  bsm_package_mas_free t2 
                        where 1=1 ";
                };

                if (v_content_info.content_id != null)
                {
                    _sql = _sql + "and c.content_id = '" + v_content_info.content_id + "' " +
                    "and t2.package_id = '" + package_id + "' ";
                }
                else
                {
                    _sql = _sql + "and t2.package_id = '" + package_id + "' ";

                };

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                _cmd.Parameters.Add("MAC_ADDRESS", client_id);

                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();
                try
                {
                    if (v_Data_Reader.Read())
                    {
                        v_package_info.package_id = v_Data_Reader.GetString(0);
                        if (!v_Data_Reader.IsDBNull(1))
                        {
                            v_package_info.package_name = v_Data_Reader.GetString(1);
                        }
                        if (!v_Data_Reader.IsDBNull(2))
                        {
                            v_package_info.catalog_description = v_Data_Reader.GetString(2);
                        }
                        if (!v_Data_Reader.IsDBNull(3))
                        {
                            v_package_info.catalog_id = v_Data_Reader.GetString(3);
                        }
                        if (!v_Data_Reader.IsDBNull(4))
                        {
                            v_package_info.price_description = v_Data_Reader.GetString(4);
                        }
                        if (!v_Data_Reader.IsDBNull(5))
                        {
                            v_package_info.price = v_Data_Reader.GetDecimal(5).ToString();
                        }
                        if (!v_Data_Reader.IsDBNull(6))
                        {
                            v_package_info.logo = v_Data_Reader.GetString(6);
                        }
                        if (!v_Data_Reader.IsDBNull(7))
                        {
                            v_package_info.package_description = v_Data_Reader.GetString(7);
                        }

                        if (!v_Data_Reader.IsDBNull(8))
                        {
                            v_package_info.item_id = v_Data_Reader.GetString(8);
                        }

                        if (!v_Data_Reader.IsDBNull(9))
                        {
                            if (v_Data_Reader.GetString(9) == "PPV")
                            {
                                v_result.show_detail_flg = "Y";
                            }
                            else
                            {
                                v_result.show_detail_flg = "N";
                            }
                        }
                        else { v_result.show_detail_flg = "N"; }

                        if (!v_Data_Reader.IsDBNull(10))
                        { v_package_info.start_date = v_Data_Reader.GetString(10); }

                        if (!v_Data_Reader.IsDBNull(11))
                        { v_package_info.end_date = v_Data_Reader.GetString(11); }

                        if (!v_Data_Reader.IsDBNull(12))
                        {
                            v_package_info.cost_credits = v_Data_Reader.GetString(12);
                        }
                        if (!v_Data_Reader.IsDBNull(13))
                        {
                            v_package_info.credits = v_Data_Reader.GetDecimal(13);

                        }
                        if (!v_Data_Reader.IsDBNull(14))
                        {
                            v_package_info.days = v_Data_Reader.GetString(14);
                        }

                        if (!v_Data_Reader.IsDBNull(15))
                        {
                            v_package_info.recurrent = v_Data_Reader.GetString(15);
                        }


                        if (!v_Data_Reader.IsDBNull(16))
                        {
                            v_package_info.current_recurrent_status = v_Data_Reader.GetString(16);
                        }

                        if (!v_Data_Reader.IsDBNull(17))
                        {
                            v_package_info.next_pay_date = v_Data_Reader.GetString(17);
                        }

                        v_package_info.credits_balance = (int)_get_credits_balance(client_id).credits - v_package_info.credits;
                        if (v_package_info.credits_balance >= 0)
                        {
                            v_package_info.after_credits = v_package_info.credits_balance.ToString() + "點";
                        }
                        else
                        {
                            v_package_info.after_credits = "0點";
                        }


                        v_package_info.status_description = "尚未付費";

                    }
                    if (p_content_id != "" && p_content_id != null)
                    {
                        string _sql2 = "select decode(trunc(t.start_date),null,t2.package_start_date_desc,to_char(trunc(t.start_date),'YYYY/MM/DD'))  start_date," +
          "decode(trunc(t.end_date),null,null" +
          ",to_char(trunc(t.end_date),'YYYY/MM/DD')) end_date," +
         "decode(t.start_date,null,'可使用,未啟用',decode(sign(end_date-sysdate),1,'已啟用','已到期') ) package_status,decode(t.start_date,null,'N',decode(sign(end_date-sysdate),1,'Y','N') ) status  " +
    " from bsm_client_details t,bsm_package_mas t2 " +
   " where t2.status_flg = 'P' and t2.package_id=t.package_id " +
   "and t.mac_address =:mac_address " +
   "and t.status_flg ='P' " +
  "and (t.package_id, decode(t2.cal_type, 'T',nvl(t.item_id, ' '),' ')) in " +
     "  (select t2.package_id, decode(t2.cal_type, 'T', b.item_id, ' ') " +
  "from mid_cms_content c,mid_cms_item_rel a, mid_cms_item b,bsm_package_mas t2 " +
  "where t2.status_flg = 'P' " +
  "and a.mas_pk_no = c.pk_no " +
  "and c.content_id = :content_id " +
  "and type = 'P' " +
  "and b.pk_no = a.detail_pk_no " +
  "and t2.package_id = b.package_id) ";


                        OracleCommand _cmd2 = new OracleCommand(_sql2, conn);
                        _cmd2.BindByName = true;
                        _cmd2.Parameters.Add("mac_address", client_id);
                        _cmd2.Parameters.Add("content_id", p_content_id);
                        OracleDataReader v_Data_Reader2 = _cmd2.ExecuteReader();
                        v_package_info.use_status = "N";
                        while (v_Data_Reader2.Read())
                        {
                            if (!v_Data_Reader2.IsDBNull(0)) { v_package_info.start_date = v_Data_Reader2.GetString(0); }
                            if (!v_Data_Reader2.IsDBNull(1)) { v_package_info.end_date = v_Data_Reader2.GetString(1); }
                            if (!v_Data_Reader2.IsDBNull(2)) { v_package_info.status_description = v_Data_Reader2.GetString(2); }
                            if (!v_Data_Reader2.IsDBNull(3)) { v_package_info.use_status = v_Data_Reader2.GetString(3); }
                        }
                    }
                    else
                    {
                        string _sql3 = "select '' start_date,'無' end_date,'已啟用' from mfg_free_service where package_id=:P_PACKAGE_ID ";
                        OracleCommand _cmd3 = new OracleCommand(_sql3, conn);
                        _cmd3.Parameters.Add("P_PACKAGE_ID", package_id);
                        OracleDataReader _Data_Reader3 = _cmd3.ExecuteReader();
                        while (_Data_Reader3.Read())
                        {
                            if (!_Data_Reader3.IsDBNull(0)) { v_package_info.start_date = _Data_Reader3.GetString(0); }
                            if (!_Data_Reader3.IsDBNull(1)) { v_package_info.end_date = _Data_Reader3.GetString(1); }
                            if (!_Data_Reader3.IsDBNull(2)) { v_package_info.status_description = _Data_Reader3.GetString(2); }
                        }


                        string _sql2 = "select decode(trunc(t.start_date),null,t2.package_start_date_desc,to_char(trunc(t.start_date),'YYYY/MM/DD'))  start_date," +
                            "decode(trunc(t.end_date),null,null" +
                            ",to_char(trunc(t.end_date),'YYYY/MM/DD')) end_date," +
                            "decode(t.start_date,null,'可使用,未啟用',decode(sign(end_date-sysdate),1,'已啟用','已到期') ) package_status ,decode(t.start_date,null,'N',decode(sign(end_date-sysdate),1,'Y','N') ) status " +
                            " from bsm_client_details t,bsm_package_mas t2 " +
                            " where t2.status_flg = 'P' and t2.package_id=t.package_id " +
                            "and t.mac_address =:mac_address " +
                            "and t.package_id = :package_id and t.status_flg ='P'";
                        OracleCommand _cmd2 = new OracleCommand(_sql2, conn);
                        _cmd2.BindByName = true;
                        _cmd2.Parameters.Add("mac_address", client_id);
                        _cmd2.Parameters.Add("package_id", package_id);
                        OracleDataReader v_Data_Reader2 = _cmd2.ExecuteReader();
                        v_package_info.use_status = "N";
                        while (v_Data_Reader2.Read())
                        {
                            if (!v_Data_Reader2.IsDBNull(0)) { v_package_info.start_date = v_Data_Reader2.GetString(0); }
                            if (!v_Data_Reader2.IsDBNull(1)) { v_package_info.end_date = v_Data_Reader2.GetString(1); }
                            if (!v_Data_Reader2.IsDBNull(2)) { v_package_info.status_description = v_Data_Reader2.GetString(2); }
                            if (!v_Data_Reader2.IsDBNull(3)) { v_package_info.status_description = v_Data_Reader2.GetString(3); }
                        }
                    }
                }
                finally
                {
                    v_Data_Reader.Dispose();
                }
            }
            finally
            {
                conn.Close();

            }

            v_result.package_info = v_package_info;
            v_result.content_info = v_content_info;
            return v_result;
        }

        /// <summary>
        /// 取單一筆購買紀錄
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <returns></returns>
        public BSM_Info.purchase_info_list get_purchase_info_by_id(string client_id, string purchase_id)
        {
            client_id = client_id.ToUpper();
            BSM_Info.purchase_info_list v_result = new BSM_Info.purchase_info_list();
            v_result.purchase_id = purchase_id;
           
            List<purchase_info_list> _reault_list = this.get_purchase_info_oracle(client_id);
            if (_reault_list.Count > 0)
            {
                try
                {
                    v_result = (from a in _reault_list where a.purchase_id == purchase_id select a).First();
                }
                catch (InvalidOperationException e)
                {
                    v_result.purchase_id = purchase_id;
                }
            }

            return v_result;
        }

        /// <summary>
        /// 取得Client Activation Code
        /// </summary>
        /// <param name="client_id"></param>
        /// <returns></returns>
        public string get_activation_code(string client_id)
        {
            string _result ="";
            account_info acc_info = get_account_info(client_id,null);
            if (acc_info != null) _result= acc_info.activation_code;
            return _result;
            
            

        }


        public string get_activation_code_oracle(string client_id)
        {

            connectDB();

            client_id = client_id.ToUpper();

            string _sql = "Select activation_code FROM bsm_client_mas a where a.MAC_ADDRESS=:CLIENT_ID";
            string _result = "";
            OracleCommand _cmd = new OracleCommand(_sql, conn);
            _cmd.Parameters.Add("CLIENT_ID", client_id);
            OracleDataReader _Data_Reader;

            try
            {
                _Data_Reader = _cmd.ExecuteReader();
                if (_Data_Reader.Read())
                {
                    if (!_Data_Reader.IsDBNull(0))
                    {
                        _result = _Data_Reader.GetString(0);
                    }
                }
            }
            finally
            {
                _cmd.Dispose();
                conn.Close();
            } 
            return _result;
        }


        public string get_client_info(string client_id)
        {
       /*     connectDB();

            client_id = client_id.ToUpper();

            string _sql = "Select bsm_cdi_service.get_cdi_info(mac_address) FROM bsm_client_mas a where a.MAC_ADDRESS=:CLIENT_ID";
            string _result = "";
            OracleCommand _cmd = new OracleCommand(_sql, conn);
            _cmd.Parameters.Add("CLIENT_ID", client_id);
            OracleDataReader _Data_Reader;

            try
            {
                _Data_Reader = _cmd.ExecuteReader();
                if (_Data_Reader.Read())
                {
                    if (!_Data_Reader.IsDBNull(0))
                    {
                        _result = _Data_Reader.GetString(0);
                    }
                }
            }
            finally
            {
                _cmd.Dispose();
                conn.Close();

            } */
            return null;
        }

        public string check_access(string token, string client_id, string asset_id, string device_id)
        {
            return "Y";
        }

        public string get_stock_broker(string client_id)
        {
            return null;
        }

        public string get_service_end_date(string token, string client_id, string device_id, string asset_id,string sw_version)
        {
            process_auto_coupon(client_id, device_id,sw_version);

            connectDB();

            string sql1 = "begin :P_RESULT := check_cdi_access(:P_CLIENT_ID,:P_ASSET_ID,:P_DEVICE_ID); end; ";
            string v_can_access = "N";
            string result = "";
            try
            {
                OracleCommand cmd = new OracleCommand(sql1, conn);
                cmd.BindByName = true;
                OracleString v_o_result;
                cmd.Parameters.Add("P_RESULT", OracleDbType.Varchar2, 32, ParameterDirection.InputOutput);
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2, 32, client_id, ParameterDirection.Input);
                cmd.Parameters.Add("P_ASSET_ID", OracleDbType.Varchar2, 32, asset_id, ParameterDirection.Input);
                cmd.Parameters.Add("P_DEVICE_ID", OracleDbType.Varchar2, 32, device_id, ParameterDirection.Input);

                cmd.ExecuteNonQuery();

                v_o_result = (OracleString)cmd.Parameters["P_RESULT"].Value;
                v_can_access = v_o_result.ToString();

                sql1 = "Select get_end_date(:P_CLIENT_ID,:P_ASSET_ID,:P_DEVICE_ID) FROM dual";

                if (v_can_access == "Y")
                {
                    cmd = new OracleCommand(sql1, conn);
                    cmd.BindByName = true;
                    cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2, 32, client_id, ParameterDirection.Input);
                    cmd.Parameters.Add("P_ASSET_ID", OracleDbType.Varchar2, 32, asset_id, ParameterDirection.Input);
                    cmd.Parameters.Add("P_DEVICE_ID", OracleDbType.Varchar2, 32, device_id, ParameterDirection.Input);
                    OracleDataReader _reader = cmd.ExecuteReader();
                    if (_reader.Read())
                    {
                        if (!_reader.IsDBNull(0))
                        {
                            result = (_reader.GetDateTime(0).ToString("yyyy/MM/dd") != "2999/12/31")?_reader.GetDateTime(0).ToString("yyyy/MM/dd"):"";
                        }
                        else
                        {
                            result = "";
                        }
                    }
                }
                else
                {
                    result = "未定";
                }

            }
            finally
            {

                conn.Close();
            }


            return result;
        }

        private void process_auto_coupon(string client_id,string device_id,string sw_version)
        {
            //測試暫時註銷
            /*
            connectDB();
            try{
                string sql = "begin  p_gift_coupon(:p_client_id,:p_device_id,:p_sw_version); end;";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.BindByName = true;
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2, 32, client_id, ParameterDirection.Input);
                cmd.Parameters.Add("P_DEVICE_ID", OracleDbType.Varchar2, 32, device_id, ParameterDirection.Input);
                cmd.Parameters.Add("P_SW_VERSION", OracleDbType.Varchar2, 32, sw_version, ParameterDirection.Input);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
          */
        }


        public credits_balance_info _get_credits_balance(string client_id)
        {
            credits_balance_info _result = new credits_balance_info();
            

            try
            {
                _result.credits = 0;
                credits_detail _buy_credits = new credits_detail();
                _buy_credits.credits_type = "BUY";
                _buy_credits.credits_desc = "儲值點數";
                _buy_credits.credits = 0;
                credits_detail _gift_credits = new credits_detail();
                _gift_credits.credits_type = "GIFT";
                _gift_credits.credits_desc = "紅利點數";
                _gift_credits.credits = 0;

                _result.credits_description = "0點";

                _result.details = new List<credits_detail>();
                _result.details.Add(_gift_credits);
                _result.details.Add(_buy_credits);
            }
            finally
            {

            } 
             
           
            return _result;
        }

        public credits_balance_info get_credits_balance(string client_id)
        {
            credits_balance_info _result;
            _result = this._get_credits_balance(client_id);
            _result.details = (from _x in _result.details orderby _x.credits_type select _x).ToList();
            return _result;
        }

        private string check_apt_user(string imsi)
        {
            return "";
        }

        private JsonObject stringtojson(string json_str)
        {
            JsonObject result = new JsonObject();
            JsonReader rd = new JsonTextReader(new StringReader(json_str));
            result.Import(rd);
            return result;
        }

        public void refresh()
        {
            if (_MongoDB_package != null) _MongoDB_package.Drop();
            get_message("COUPON",null);
            get_message("CREDIT",null);
            get_message("NULL",null);
            get_message("CREDITS",null);
            get_message("ONETIME_ALREADY",null);
            get_message("RECURRENT_ALREADY",null);
            get_message("RECURRENT_CNT",null);
            get_message("DSP",null);
            get_message("SERVICE_ALREADY",null);
            get_message("USE_CREDITS",null);
            get_message("COUPON_TEXT",null);
            get_message("CREDIT_TEXT",null);
            get_message("NULL_TEXT",null);
            get_message("CREDITS_TEXT",null);
            get_message("ONETIME_ALREADY_TEXT",null);
            get_message("RECURRENT_ALREADY_TEXT",null);
            get_message("RECURRENT_CNT_TEXT",null);
            get_message("DSP_TEXT",null);
            get_message("SERVICE_ALREADY_TEXT",null);
            get_message("USE_CREDITS_TEXT",null);
            post_package_special();

          //  get_all_package();

        }
    }

    /// <summary>
    /// BSM_Client set class
    /// </summary>
    /// 
    class client_value
    {
        public string _id;
        public string value_name;
        public string software_group;
        public string value;
    }

    public class BSM_CLIENT_SET
    {
        private MongoClient _Mongoclient;
        private MongoServer _MongoServer;
        private MongoDatabase _MongoDB;



        public BSM_CLIENT_SET(string _connectString, string MongoDbconnectionString, string MongodbConnectString_package, string MongoDB_Database)
        {
          //  conn = new OracleConnection();
          //  conn.ConnectionString = _connectString;
          //  MsgQ = new MessageQueue(".\\Private$\\bsm_client_val");
            if (MongoDbconnectionString != null && MongoDbconnectionString != "")
            {
                _Mongoclient = new MongoClient(MongoDbconnectionString);
                _MongoServer = _Mongoclient.GetServer();
                _MongoDB = _MongoServer.GetDatabase(MongoDB_Database + "ClientInfo");
            }
            else
            {
                _Mongoclient = null;
                _MongoServer = null;
                _MongoDB = null;
            }
            
        }



        public string get_client_vaule(string client_id, string device_id, string value_name, string default_vaule, string software_ver)
        {
            string _result="";
            string _software_goroup = software_ver.Substring(0, 7);
            MongoCollection _Mogodb_client_list = _MongoDB.GetCollection("client_values");

            var q = Query.And(Query.EQ("value_name", value_name), Query.EQ("software_group", _software_goroup));
            client_value _result_v = _Mogodb_client_list.FindOneAs<client_value>(q);
            if (_result_v != null)
            {
                _result = _result_v.value;
            }
            
            return _result;
        }

     /*   public string set_client_vaule(string client_id, string value_name, string default_vaule)
        {
            string _result;
            string _sql = @"begin :result := set_client_val(:client_id,:p_name,:p_default_val); end;";

            OracleCommand _cmd;
            conn.Open();

            _cmd = new OracleCommand(_sql, conn);
            _cmd.BindByName = true;
            _cmd.Parameters.Add("CLIENT_ID", client_id);
            _cmd.Parameters.Add("P_NAME", value_name);

            char[] _bf;
            _bf = default_vaule.ToCharArray();

            OracleClob _clob = new OracleClob(conn);
            //  _clob.
            _clob.Write(_bf, 0, default_vaule.Length);

            OracleParameter _param_def_val = new OracleParameter();
            _param_def_val.ParameterName = "P_DEFAULT_VAL";
            _param_def_val.OracleDbType = OracleDbType.Clob;
            _param_def_val.Value = _clob;
            _cmd.Parameters.Add(_param_def_val);

            OracleParameter _p_result = new OracleParameter();
            _p_result.ParameterName = "RESULT";
            _p_result.OracleDbType = OracleDbType.Clob;
            _p_result.Value = "";
            _p_result.Direction = ParameterDirection.ReturnValue;
            _cmd.Parameters.Add(_p_result);

            try
            {
                _cmd.ExecuteNonQuery();
                OracleClob _res = (OracleClob)_p_result.Value;
                _result = _res.Value;
            }
            finally
            {
                conn.Close();

            }
            return _result;
        } 
      */
    }

    /// <summary>
    /// 購買記錄查詢介面
    /// </summary>
    public class BSM_Info_Service : JsonRpcHandler
    {
        //     OracleConnection conn;

        static ILog logger;
        private BSM_Info_Service_base _base;
        private BSM_CLIENT_SET _base_client_set;
        private string MongoDBConnectString;
        private string MongoDBConnectString_package;
        private string MongoDB_Database; 

        public BSM_Info_Service()
        {
            logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            String ConnectString = "";

            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            System.Configuration.ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["BsmConnectionString"];
                MongoDBConnectString =rootWebConfig.ConnectionStrings.ConnectionStrings["MongoDb"].ToString();
                MongoDBConnectString_package = rootWebConfig.ConnectionStrings.ConnectionStrings["MongoDb_Package"].ToString();
                MongoDB_Database = rootWebConfig.ConnectionStrings.ConnectionStrings["MongoDb_Database"].ToString();
                ConnectString = connString.ConnectionString;
            }

            _base = new BSM_Info_Service_base(ConnectString, MongoDBConnectString, MongoDBConnectString_package, MongoDB_Database);
            _base_client_set = new BSM_CLIENT_SET(ConnectString, MongoDBConnectString, MongoDBConnectString_package, MongoDB_Database);

        }

        /// <summary>
        /// 取各館狀態,舊版本
        /// </summary>
        /// <param name="p_client_id"></param>
        /// <returns></returns>
        /// 
        [JsonRpcMethod("get_package_status")]
        [JsonRpcHelp("舊版,取各館狀態")]
        public BSM_Info.Old_version.result_get_package_status get_package_status(string client_id)
        {
            BSM_Info.Old_version.result_get_package_status res = _base.get_package_status(client_id);
            return res;
        }


        /// <summary>
        /// 取各館狀態
        /// </summary>
        /// <param name="client_id"></param>
        /// <returns></returns>
        [JsonRpcMethod("get_catalog_info")]
        [JsonRpcHelp("取各館狀態")]
        public List<catalog_info> get_catalog_info(string token, string client_id, string device_id,string sw_version)
        {
            List<catalog_info> v_result = _base.get_catalog_info(client_id, device_id, sw_version);
            return v_result;
        }


        /// <summary>
        /// 取購買紀錄
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <returns></returns>

        [JsonRpcMethod("get_purchase_info")]
        [JsonRpcHelp("取購買紀錄 : 傳入 Client Id, device id")]
        public List<purchase_info_list> get_purchase_info(string token, string client_id, string device_id)
        {
            List<purchase_info_list> v_result = _base.get_purchase_info(client_id, device_id, null);
            return v_result;
        }

        /// <summary>
        /// 取所有方案
        /// Sample :
        ///  { "client_id" : "0080C8001002" }ca
        /// [{"catalog_id":"HIDO","catalog_description":"Hido\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00001","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"IFILM","catalog_description":"iFilm\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00002","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"KOD","catalog_description":"\u7f8e\u83ef\u5361\u62c9OK","package_name":"\u4e00\u500b\u6708","package_id":"K00001","price_description":"\u6bcf\u670869\u5143"}]
        /// </summary>
        [JsonRpcMethod("get_package_info")]
        [JsonRpcHelp("取得Package 資訊,(cal_type = 'T' 為單片,'P' 方案")]
        public JsonArray get_package_info(string token,string group_id, string client_id, string device_id,string imsi,string sw_version,string cal_type)
        {
            JsonArray v_result;
            if (group_id == null)
            {
                v_result = _base.get_package_info_a(client_id, "BUY", 0, device_id, null, imsi, sw_version, "N", cal_type);
            }
            else
            {
                v_result = _base.get_package_info_a(client_id, "BUY", 0, device_id, group_id, imsi, sw_version, "N",cal_type);
            }
            logger.Info("Sccess");

            return v_result;
        }

        /// <summary>
        /// 取所有方案
        /// Sample :
        ///  { "client_id" : "0080C8001002" }
        /// [{"catalog_id":"HIDO","catalog_description":"Hido\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00001","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"IFILM","catalog_description":"iFilm\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00002","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"KOD","catalog_description":"\u7f8e\u83ef\u5361\u62c9OK","package_name":"\u4e00\u500b\u6708","package_id":"K00001","price_description":"\u6bcf\u670869\u5143"}]
        /// </summary>
        [JsonRpcMethod("get_group_package_info")]
        [JsonRpcHelp("取得Package 資訊, by Group")]
        public System.Collections.Generic.List<group> get_group_package_info(string token, string client_id, string device_id,string imsi,string ency_imsi,string sw_version)
        {
            List<group> v_result = new List<group>();
            v_result = _base.get_group_package_info(token, client_id, device_id,imsi,sw_version);
            logger.Info(JsonConvert.ExportToString(v_result));

            return v_result;
        }


        /// <summary>
        /// 取信用卡訊息
        /// </summary>
        /// <returns></returns>
        [JsonRpcMethod("get_credit_message")]
        [JsonRpcHelp("取信用卡付款顯示資訊")]
        public string get_credit_message()
        {
            return _base.get_credit_message();
        }

        /// <summary>
        /// 取信用卡訊息
        /// </summary>
        /// <returns></returns>
        [JsonRpcMethod("get_credits_message")]
        [JsonRpcHelp("取儲值卡付款顯示資訊")]
        public string get_credits_message()
        {
            return _base.get_credits_message();
        }

        [JsonRpcMethod("get_coupon_message")]
        [JsonRpcHelp("取儲值卡付款顯示資訊")]
        public string get_coupon_message()
        {
            return _base.get_coupon_message();
        }

        [JsonRpcMethod("get_dsp_message")]
        [JsonRpcHelp("取DSP顯示資訊")]
        public string get_dsp_message(string client_id)
        {
            return _base.get_dsp_message(client_id);
        }

        [JsonRpcMethod("get_recurrent_contract")]
        [JsonRpcHelp("取連續扣款合約")]
        public string get_recurrent_contract()
        {
            return _base.get_recurrent_contract();
        }

        [JsonRpcMethod("get_recurrent_message")]
        [JsonRpcHelp("取連續扣款訊息")]
        public string get_recurrent_message()
        {
            return _base.get_already_recurrent_message();
        }

        [JsonRpcMethod("get_onetime_message")]
        [JsonRpcHelp("取連續扣款訊息")]
        public string get_onetime_message()
        {
            return _base.get_onetime_message();
        }

        /// <summary>
        /// 取Content package 資訊
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <param name="content_id"></param>
        /// <returns></returns>
        [JsonRpcMethod("get_content_package_info")]
        [JsonRpcHelp("取得Content 相關的方案列表: 傳入Client Id ,Content_id")]
        public System.Collections.Generic.List<package_info> get_content_package_info(string token, string client_id, string content_id, string device_id,string imsi,string sw_version)
        {
            List<package_info> v_result = _base.get_content_package_info(client_id, content_id, device_id,imsi,sw_version);
            return v_result;
        }

        /// <summary>
        /// 取package detail 的內容
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <param name="package_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        [JsonRpcMethod("get_package_detail")]
        [JsonRpcHelp("取得方案的詳細資料:傳入 client id ,package id, 播放影片的item_id,cotent_id 如果有直時,系統自動取此Content 的第一個Package")]
        public BSM_Info.content_package_info get_package_detail(string token, string client_id, string package_id, string item_id, string content_id,string imsi,string sw_version)
        {
            BSM_Info.content_package_info v_result = _base.get_package_detail(client_id, package_id, item_id, content_id,imsi,sw_version);
            return v_result;
        }

        /// <summary>
        /// 取得單一訂單資訊:傳入Clinet id ,Purchase Id 單據編號
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <param name="purchase_id"></param>
        /// <returns></returns>
        [JsonRpcMethod("get_package_info_by_id")]
        public package_info get_package_info_by_id(string client_id, string system_type, int? min_credits, string device_id, string package_id, string sw_version)
        {
            
            system_type = system_type ?? "BUY";
            List<package_info> v_result = _base.get_package_info(client_id, system_type, min_credits, device_id, null, null, sw_version,"N","P");
            package_info result = ((from a in v_result where a.package_id == package_id select a).Count() > 0) ? (from a in v_result where a.package_id == package_id select a).First() : null;
            return result;
        }
        /// <summary>
        /// 取得單一訂單資訊:傳入Clinet id ,Purchase Id 單據編號
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <param name="purchase_id"></param>
        /// <returns></returns>
        [JsonRpcMethod("get_purchase_info_by_id")]
        [JsonRpcHelp("取得單一訂單資訊:傳入Clinet id ,Purchase Id 單據編號")]
        public BSM_Info.purchase_info_list get_purchase_info_by_id(string token, string client_id, string purchase_id)
        {
            BSM_Info.purchase_info_list v_result = new purchase_info_list();
            v_result = _base.get_purchase_info_by_id(client_id, purchase_id);
            return v_result;
        }

//        [JsonRpcMethod("get_activation_code")]
//        [JsonRpcHelp("取得Client 的Activation Code : 傳入 Client id")]
        public string get_activation_code(string token, string client_id)
        {
            string v_result = _base.get_activation_code(client_id);
            return v_result;
        }

        [JsonRpcMethod("check_client_access")]
        public string check_client_access(string token, string client_id, string device_id, string asset_id)
        {
            string v_result = _base.check_access(token, client_id, asset_id, device_id);
            return v_result;
        }

        [JsonRpcMethod("get_service_end_date")]
        [JsonRpcHelp("取得服務到期日")]
        public string get_service_end_date(string token, string client_id, string device_id, string asset_id,string sw_version)
        {
            string v_result = _base.get_service_end_date(token, client_id, device_id, asset_id,sw_version);
            return v_result;
        }

        [JsonRpcMethod("check_client_info")]
        public string check_client_access(string client_id)
        {
            string v_result = _base.get_client_info(client_id);
            return v_result;
        }


        /// <summary>
        /// 取儲值卡訊息
        /// </summary>
        /// <returns></returns>
        [JsonRpcMethod("get_credits_balance")]
        [JsonRpcHelp("取得剩餘點數")]
        public credits_balance_info get_credits_balance(string token, string client_id)
        {
            return _base.get_credits_balance(client_id);
        }


        /// <summary>
        /// 取所有方案
        /// Sample :
        ///  { "client_id" : "0080C8001002" }
        /// [{"catalog_id":"HIDO","catalog_description":"Hido\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00001","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"IFILM","catalog_description":"iFilm\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00002","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"KOD","catalog_description":"\u7f8e\u83ef\u5361\u62c9OK","package_name":"\u4e00\u500b\u6708","package_id":"K00001","price_description":"\u6bcf\u670869\u5143"}]
        /// </summary>

        [JsonRpcMethod("get_credits_package")]
        [JsonRpcHelp("取得儲值方案列表 資訊")]
        public System.Collections.Generic.List<package_info> get_credits_package(string token, string client_id, string device_id, int? min_credits)
        {
            List<package_info> v_result = _base.get_package_info(client_id, "CREDITS", min_credits, device_id, null,null,null,"Y","P");
            return v_result;
        }

        [JsonRpcMethod("get_stock_broker")]
        [JsonRpcHelp("取股市相關設定 client_id")]
        public string get_stock_broker(string token, string client_id)
        {
            string v_result = _base.get_stock_broker(client_id);
            return v_result;
        }


        [JsonRpcMethod("get_client_value")]
        [JsonRpcHelp("取得Client 端相關設定 client_id, value name 變數名稱, 變數default 值")]
        public string get_client_vaule(string token, string client_id, string device_id, string value_name, string software_ver)
        {
            string default_vaule = "";
            string v_result = _base_client_set.get_client_vaule(client_id, device_id, value_name, default_vaule, software_ver);
            return v_result;
        }

        [JsonRpcMethod("set_client_value")]
        [JsonRpcHelp("取得Client 端相關設定 client_id, value name 變數名稱, 變數default 值")]
        public string set_client_vaule(string token, string client_id, string value_name, string default_vaule)
        {
         //   string v_result = _base_client_set.set_client_vaule(client_id, value_name, default_vaule);
            return null;
        }

        [JsonRpcMethod("get_message")]
        [JsonRpcHelp("取得get_message")]
        public string get_message(string message_type, string sw_version)
        {
            return _base.get_message(message_type, sw_version);
        }

        [JsonRpcMethod("refresh")]
        [JsonRpcHelp("refresh")]
        public string refrash()
        {
            _base.refresh();
            return "Sucess";
        }


        [JsonRpcMethod("refresh_client")]
        [JsonRpcHelp("取得Client 端相關設定 client_id, value name 變數名稱, 變數default 值")]
        public string refresh_client(string token, string client_id)
        {
            string v_result = _base.refresh_client(client_id);
            return v_result;
        }

        [JsonRpcMethod("catch_account")]
        [JsonRpcHelp("取得Client 端相關設定 client_id, value name 變數名稱, 變數default 值")]
        public string catch_account(string token, string client_id, string device_id)
        {
            string v_result = _base.catch_client(client_id,device_id);
            return v_result;
        }

        [JsonRpcMethod("catch_account_all")]
        [JsonRpcHelp("取得Client 端相關設定 client_id, value name 變數名稱, 變數default 值")]
        public string catch_account_all(string token, string client_id, string device_id)
        {
            string v_result = _base.catch_client_all();
            return v_result;
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
        [JsonRpcMethod("version")]
        public string version()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }

}



