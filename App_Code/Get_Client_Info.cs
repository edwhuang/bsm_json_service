namespace GetClientInfo
{

    using System;
    using System.Web;
    using System.Data;
    using System.Collections.Generic;
    using Jayrock.Json;
    using Jayrock.JsonRpc;
    using Jayrock.JsonRpc.Web;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;

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

    }

    /// <summary>
    /// 購買記錄
    /// </summary>
    public class purchase_info
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

    }

    /// <summary>
    /// 方案狀態
    /// </summary
    public class package_info
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
        public string package_id;


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

        /// <summary>
        /// 
        /// </summary>
        public string cost_credits;

        public string after_credits;

        public Decimal credits_balance;

        public Decimal credits;

        public string cal_type;

        public string days;

        public string credits_description;

        public string remark;
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
        /// 下假日期
        /// </summary>
        public string off_shelf_date;

        /// <summary>
        /// 備註
        /// </summary>
        public string remark;

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

    public class credits_balance_info
    {
        public int? credits;
        public string credits_description;
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
        public OracleConnection conn;

        public BSM_Info_Service_base(string connString)
        {

            conn = new OracleConnection();
            conn.ConnectionString = connString;

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

            conn.Open();
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
        public List<catalog_info> get_catalog_info(string client_id)
        {
            List<catalog_info> v_result = new List<catalog_info>();
            string _sql;
            client_id = client_id.ToUpper();

            conn.Open();
            try
            {
                //_sql = "SELECT a.PACKAGE_CAT1,a.SUPPLY_NAME||a.PACKAGE_NAME,a.START_DATE,a.end_date,b.PACKAGE_DES_HTML,b.PRICE_DES,a.package_status,b.logo,a.package_id FROM BSM_CLIENT_DETAILS_CAT a,BSM_PACKAGE_MAS b WHERE MAC_ADDRESS=:MAC_ADDRESS AND a.SRC_PACKAGE_ID=b.PACKAGE_ID Order by b.cal_type,nvl(end_date,'9999/12/28') ";
                _sql ="SELECT a.PACKAGE_CAT1,a.SUPPLY_NAME || a.PACKAGE_NAME,a.START_DATE,a.end_date,b.PACKAGE_DES_HTML,b.PRICE_DES,a.package_status,b.logo,a.package_id "+
  " FROM BSM_CLIENT_DETAILS_CAT a, BSM_PACKAGE_MAS b "+
 " WHERE MAC_ADDRESS = :MAC_ADDRESS "+
  " AND a.SRC_PACKAGE_ID = b.PACKAGE_ID "+
 " union all "+
 " select b.PACKAGE_CAT1, "+
 "      b.DESCRIPTION, "+
 "      '', "+
 "      '無', "+
 "      b.PACKAGE_DESC_HTML, "+
 "      '免費使用', "+
 "      '已啟用', "+
 "      b.logo, "+
 "      b.package_id "+
 " from mfg_free_service b "+
 " Order by 1,4 desc";

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                // OracleParameter _p_mac_address = new OracleParameter("MAC_ADDRESS", OracleDbType.Varchar2, p_client_id, ParameterDirection.Input);
                _cmd.BindByName = true;
                _cmd.Parameters.Add("MAC_ADDRESS", client_id);
                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();
                int _i = 0;
                try
                {
                    while (v_Data_Reader.Read())
                    {
                        catalog_info v_catalog_info = new catalog_info();
                        if (!v_Data_Reader.IsDBNull(0))
                        {
                            v_catalog_info.catalog_name = v_Data_Reader.GetString(0);
                            v_catalog_info.catalog_description = v_Data_Reader.GetString(0);
                        }
                        if (!v_Data_Reader.IsDBNull(1))
                        {
                            v_catalog_info.package_name = v_Data_Reader.GetString(1);
                        }
                        if (!v_Data_Reader.IsDBNull(2))
                        {
                            v_catalog_info.start_date = v_Data_Reader.GetString(2);
                        }
                        if (!v_Data_Reader.IsDBNull(3))
                        {
                            v_catalog_info.end_date = v_Data_Reader.GetString(3);
                        }
                        if (!v_Data_Reader.IsDBNull(4))
                        {
                            v_catalog_info.package_description = v_Data_Reader.GetString(4);
                        }
                        if (!v_Data_Reader.IsDBNull(5))
                        {
                            v_catalog_info.price_description = v_Data_Reader.GetString(5);
                        }
                        if (!v_Data_Reader.IsDBNull(6))
                        {
                            v_catalog_info.status_description = v_Data_Reader.GetString(6);
                        }
                        if (!v_Data_Reader.IsDBNull(7))
                        {
                            v_catalog_info.logo = v_Data_Reader.GetString(7);
                        }
                        if (!v_Data_Reader.IsDBNull(8))
                        {
                            v_catalog_info.package_id = v_Data_Reader.GetString(8);
                        }

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
        public List<purchase_info> get_purchase_info(string client_id)
        {
            List<purchase_info> v_result = new List<purchase_info>();
            client_id = client_id.ToUpper();

            conn.Open();
            try
            {
                string _sql;
                _sql = "Select d.package_cat1 cat_description," +
 "d.description package_name," +
 "to_char(a.purchase_date, 'YYYY/MM/DD') purchase_date," +
 "d.price_des," +
 "decode(a.pay_type,'REMIT','匯款',a.pay_type) pay_type," +
 "'************' || substr(a.card_no, 13, 4) card_no," +
 "a.mas_no purchase_id," +
 "a.approval_code approval_code," +
 "decode(c.start_date,null,'未啟用', decode(sign(end_date - sysdate), 1, '已啟用', '已到期')) status_description," +
 "to_char(c.start_date,'YYYY/MM/DD') start_date," +
 "to_char(c.end_date,'YYYY/MM/DD') end_date," +
 "to_char(a.purchase_date, 'YYYY/MM/DD HH24:MI') purchase_time, " +
 "d.package_cat1 cat_name," +
 "d.package_start_date_desc,d.package_end_date_desc ,a.tax_inv_no ,a.tax_gift,cms_util.get_content_title(e.item_id) title,d.cal_type," +
 "(select to_char(F_INVO_DATE,'YYYY/MM/DD') from tax_inv_mas inv where inv.f_invo_no=a.tax_inv_no) INVO_DATE," +
 "(select IDENTIFY_ID from tax_inv_mas inv where inv.f_invo_no=a.tax_inv_no) INVO_IDENTIFY_ID," +
  "a.cost_credits,a.after_credits" +
  " from bsm_purchase_mas   a, " +
  "     bsm_purchase_item  e, " +
  "     bsm_client_details c, " +
  "     bsm_package_mas    d " +
  " where e.mas_pk_no =a.pk_no " +
  " and c.src_pk_no (+) =e.mas_pk_no " +
  " and c.src_item_pk_no (+) = e.pk_no " +
  " and e.package_id = d.package_id " +
  " and a.status_flg in ('P', 'Z') " +
  " and a.serial_id = :MAC_ADDRESS " +
  " and a.purchase_date > (sysdate - 93) " +
  "Order by to_char(a.purchase_date, 'YYYY/MM/DD') desc, a.mas_no desc";


                OracleCommand _cmd = new OracleCommand(_sql, conn);
                _cmd.BindByName = true;
                _cmd.Parameters.Add("MAC_ADDRESS", client_id);
                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();
                int _i = 0;
                try
                {
                    while (v_Data_Reader.Read())
                    {
                        purchase_info v_purchase_info = new purchase_info();

                        if (!v_Data_Reader.IsDBNull(0))
                        {
                            v_purchase_info.catalog_description = v_Data_Reader.GetString(0);
                        }

                        if (!v_Data_Reader.IsDBNull(1))
                        {
                            v_purchase_info.package_name = v_Data_Reader.GetString(1);
                        }

                        if (!v_Data_Reader.IsDBNull(2))
                        {
                            v_purchase_info.purchase_date = v_Data_Reader.GetString(2);
                        }

                        if (!v_Data_Reader.IsDBNull(3))
                        {
                            v_purchase_info.price_description = v_Data_Reader.GetString(3);
                        }

                        if (!v_Data_Reader.IsDBNull(4))
                        {
                            v_purchase_info.pay_type = v_Data_Reader.GetString(4);
                        }
                        else
                        {
                            v_purchase_info.pay_type = "信用卡";
                        }

                        if (!v_Data_Reader.IsDBNull(5))
                        {
                            v_purchase_info.card_no = v_Data_Reader.GetString(5);

                        }

                        v_purchase_info.purchase_id = v_Data_Reader.GetString(6);
                        if (!v_Data_Reader.IsDBNull(7))
                        {
                            v_purchase_info.approval_code = v_Data_Reader.GetString(7);
                        }

                        if (!v_Data_Reader.IsDBNull(8))
                        {
                            v_purchase_info.status_description = v_Data_Reader.GetString(8);
                        }
                        else
                        { v_purchase_info.status_description = "未啟用"; }


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

                        if (!v_Data_Reader.IsDBNull(12))
                        {
                            v_purchase_info.catalog_name = v_Data_Reader.GetString(12);
                        }

                        if (!v_Data_Reader.IsDBNull(15))
                        {
                            // Tax Gift
                            if (v_Data_Reader.IsDBNull(16))
                            { v_purchase_info.invoice_no = v_Data_Reader.GetString(15); }
                            else
                            {
                                if (v_Data_Reader.GetString(16) == "Y")
                                {
                                    v_purchase_info.invoice_no = "捐贈";
                                }
                                else
                                {
                                    v_purchase_info.invoice_no = v_Data_Reader.GetString(15);
                                }
                            }

                        }
                        else
                        {
                            v_purchase_info.invoice_no = "尚未開立";

                        }

                        if (!v_Data_Reader.IsDBNull(18))
                        {
                            if (v_Data_Reader.GetString(18) == "T")
                            {
                                if (!v_Data_Reader.IsDBNull(17))
                                {
                                    v_purchase_info.package_name = v_Data_Reader.GetString(17);
                                    v_purchase_info.invoice_date = v_Data_Reader.GetString(19);
                                    v_purchase_info.invoice_einv_id = v_Data_Reader.GetString(20);
                                }
                            }
                        }

                        if (!v_Data_Reader.IsDBNull(21))
                        {
                            v_purchase_info.cost_credits = v_Data_Reader.GetString(21);
                        }
                        if (!v_Data_Reader.IsDBNull(22))
                        {
                            v_purchase_info.after_credits = v_Data_Reader.GetString(22);

                        }


                        v_purchase_info.purchase_datetime = v_Data_Reader.GetString(11);
                        v_result.Add(v_purchase_info);
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
        /// 取所有方案
        /// Sample :
        ///  { "client_id" : "0080C8001002" }
        /// [{"catalog_id":"HIDO","catalog_description":"Hido\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00001","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"IFILM","catalog_description":"iFilm\u96fb\u5f71\u9928","package_name":"\u4e00\u500b\u6708","package_id":"M00002","price_description":"\u6bcf\u670899\u5143"},{"catalog_id":"KOD","catalog_description":"\u7f8e\u83ef\u5361\u62c9OK","package_name":"\u4e00\u500b\u6708","package_id":"K00001","price_description":"\u6bcf\u670869\u5143"}]
        /// </summary>
        public System.Collections.Generic.List<package_info> get_package_info(string client_id, string system_type)
        {
            List<package_info> v_result = new List<package_info>();
            string _sql;
            client_id = client_id.ToUpper();

            conn.Open();
            try
            {
                _sql = "select t2.package_id,t2.description package_name,t2.package_cat1 cat,t2.package_cat_id1,t2.price_des,t2.charge_amount price,t2.logo,t2.package_des_html,t2.package_des,t2.package_start_date_desc,t2.package_end_date_desc,t2.package_des_text,t2.credits_des,t2.credits,t2.package_cat_id1,t2.cal_type " +
                        " from bsm_package_mas t2 " +
                                    " where  t2.system_type=:SYSTEM_TYPE and t2.status_flg ='P'";

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                if (system_type == "")
                {
                    system_type = "BUY";
                }

                _cmd.Parameters.Add("SYSTEM", system_type);

                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();
                int _i = 0;
                try
                {
                    while (v_Data_Reader.Read())
                    {
                        package_info v_package_info = new package_info();
                        v_package_info.status_description = "未購買";
                        v_package_info.package_id = v_Data_Reader.GetString(0);
                        v_package_info.package_name = v_Data_Reader.GetString(1);
                        v_package_info.catalog_description = v_Data_Reader.GetString(2);
                        v_package_info.catalog_id = v_Data_Reader.GetString(3);
                        v_package_info.price_description = v_Data_Reader.GetString(4);
                        if (!v_Data_Reader.IsDBNull(5))
                        {
                            v_package_info.price = v_Data_Reader.GetDecimal(5).ToString();
                        }

                        if (!v_Data_Reader.IsDBNull(9))
                        {
                            v_package_info.start_date = v_Data_Reader.GetString(9);
                        }

                        if (!v_Data_Reader.IsDBNull(10))
                        {
                            v_package_info.end_date = v_Data_Reader.GetString(10);
                        }

                        if (!v_Data_Reader.IsDBNull(8))
                        {
                            v_package_info.package_description_dtl = v_Data_Reader.GetString(8);
                        }


                        if (!v_Data_Reader.IsDBNull(6))
                        {
                            v_package_info.logo = v_Data_Reader.GetString(6);
                        }

                        if (!v_Data_Reader.IsDBNull(7))
                        {
                            v_package_info.package_description = v_Data_Reader.GetString(7);
                        }

                        if (!v_Data_Reader.IsDBNull(11))
                        {
                            v_package_info.package_description_text = v_Data_Reader.GetString(11);
                        }

                        if (!v_Data_Reader.IsDBNull(12))
                        {
                            v_package_info.cost_credits = v_Data_Reader.GetString(12);
                            decimal _credits = v_Data_Reader.GetDecimal(13);
                            credits_balance_info _credit_b = this._get_credits_balance(client_id);
                            decimal _after_credits = (decimal)_credit_b.credits - _credits;
                            v_package_info.after_credits = _after_credits.ToString() + "點";
                            v_package_info.credits_balance = _after_credits;
                            v_package_info.credits_description = v_Data_Reader.GetString(12);
                        }

                        if (!v_Data_Reader.IsDBNull(14))
                        {
                            v_package_info.catalog_id = v_Data_Reader.GetString(14);
                        }

                        if (!v_Data_Reader.IsDBNull(15))
                        {
                            v_package_info.cal_type = v_Data_Reader.GetString(15);
                        }

                        if (system_type == "CREDITS")
                        {
                            v_package_info.remark = "購買LiTV點數之面額最低為500元,每次購買面額回饋10%點數,例如購買500元可使用550點,依此類推";
                        }
                        

                        string _sql2 = "select to_char(trunc(t.start_date),'YYYY/MM/DD')  start_date," +
        "to_char(trunc(t.end_date),'YYYY/MM/DD') end_date," +
       "decode(t.start_date,null,'未啟用',decode(sign(end_date-sysdate),1,'已啟用','已到期') ) package_status,t2.package_start_date_desc,t2.package_end_date_desc  " +
  " from bsm_client_details t,bsm_package_mas t2 " +
 " where t.status_flg = 'P' " +
 "and t.mac_address =:mac_address " +
 "and t.package_id = :package_id and t2.package_id= t.package_id";

                        OracleCommand _cmd2 = new OracleCommand(_sql2, conn);
                        _cmd2.BindByName = true;
                        _cmd2.Parameters.Add("mac_address", client_id);
                        _cmd2.Parameters.Add("package_id", v_package_info.package_id);
                        OracleDataReader v_Data_Reader2 = _cmd2.ExecuteReader();
                        while (v_Data_Reader2.Read())
                        {
                            if (v_Data_Reader2.IsDBNull(0))
                            {
                                if (!v_Data_Reader2.IsDBNull(3))
                                { v_package_info.start_date = v_Data_Reader2.GetString(3); }
                            }
                            else
                            {
                                v_package_info.start_date = v_Data_Reader2.GetString(0);
                            }

                            if (v_Data_Reader2.IsDBNull(1))
                            {
                                if (!v_Data_Reader2.IsDBNull(4))
                                { v_package_info.end_date = v_Data_Reader2.GetString(4); }
                            }
                            else
                            {
                                v_package_info.end_date = v_Data_Reader2.GetString(4);
                            }

                        }



                        v_result.Add(v_package_info);
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
        /// 取信用卡訊息
        /// </summary>
        /// <returns></returns>
        public string get_credit_message()
        {
            string _result = "";
            conn.Open();
            string _sql = "select message from BSM_CREDIT_MESSAGE t where message_type='CREDIT'";

            OracleCommand _cmd = new OracleCommand(_sql, conn);
            OracleDataReader _reader = _cmd.ExecuteReader();
            if (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                {
                    _result = _reader.GetString(0);
                }
            }
            _cmd.Dispose();
            conn.Close();

            return _result;
        }
        /// <summary>
        /// 取COUPON訊息
        /// </summary>
        /// <returns></returns>
        public string get_coupon_message()
        {
            string _result = "";
            conn.Open();
            string _sql = "select message from BSM_CREDIT_MESSAGE t where message_type='COUPON'";

            OracleCommand _cmd = new OracleCommand(_sql, conn);
            OracleDataReader _reader = _cmd.ExecuteReader();
            if (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                {
                    _result = _reader.GetString(0);
                }
            }
            _cmd.Dispose();
            conn.Close();

            return _result;
        }
        /// <summary>
        /// 取儲值卡訊息
        /// </summary>
        /// <returns></returns>
        public string get_credits_message()
        {
            string _result = "";
            conn.Open();
            string _sql = "select message from BSM_CREDIT_MESSAGE t where message_type='CREDITS'";

            OracleCommand _cmd = new OracleCommand(_sql, conn);
            OracleDataReader _reader = _cmd.ExecuteReader();
            if (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                {
                    _result = _reader.GetString(0);
                }
            }
            _cmd.Dispose();
            conn.Close();

            return _result;
        }

        /// <summary>
        /// 取儲值卡訊息
        /// </summary>
        /// <returns></returns>
        public string get_dsp_message(string client_id)
        {
            string _result = "";
            conn.Open();
            string _sql = "select message from BSM_CREDIT_MESSAGE t where message_type='DSP'";

            OracleCommand _cmd = new OracleCommand(_sql, conn);
            OracleDataReader _reader = _cmd.ExecuteReader();
            if (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                {
                    _result = _reader.GetString(0);
                }
            }
            _cmd.Dispose();
            conn.Close();

            return _result;
        }


        /// <summary>
        /// 取Content package 資訊
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <param name="content_id"></param>
        /// <returns></returns>
        public System.Collections.Generic.List<package_info> get_content_package_info(string client_id, string content_id)
        {
            List<package_info> v_result = new List<package_info>();
            string _sql;
            client_id = client_id.ToUpper();

            conn.Open();
            try
            {
                if (content_id == "KOD")
                {
                    _sql = "select t2.package_id,t2.description  package_name,t2.package_cat1 cat,t2.package_cat_id1,t2.price_des,t2.charge_amount   price, t2.logo,t2.package_des_html, null item_id,t2.package_des,t2.package_start_date_desc,t2.package_end_date_desc ,t2.package_type " +
                       " from bsm_package_mas t2  " +
                       " where t2.system_type = 'BUY' " +
                       " and t2.package_type = 'P' " +
                       " and t2.package_cat_id1='KOD'";
                }
                else
                {
                    _sql = "select t2.package_id,t2.description  package_name,t2.package_cat1 cat,t2.package_cat_id1,t2.price_des,t2.charge_amount   price, t2.logo,t2.package_des_html, b.item_id item_id,t2.package_des,t2.package_start_date_desc,t2.package_end_date_desc ,t2.package_type " +
      "from mid_cms_content c,mid_cms_item_rel a, mid_cms_item b,bsm_package_mas t2 " +
     "where t2.system_type = 'BUY' " +
       "and t2.status_flg = 'P' " +
       "and a.mas_pk_no = c.pk_no " +
       "and c.content_id = :content_id " +
       "and type = 'P' " +
       "and b.pk_no = a.detail_pk_no " +
       "and t2.package_id = b.package_id";
                }

                OracleCommand _cmd = new OracleCommand(_sql, conn);
                _cmd.Parameters.Add("content_id", content_id);
                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();
                int _i = 0;
                try
                {
                    while (v_Data_Reader.Read())
                    {
                        package_info v_package_info = new package_info();
                        v_package_info.package_id = v_Data_Reader.GetString(0);
                        v_package_info.package_name = v_Data_Reader.GetString(1);
                        v_package_info.catalog_description = v_Data_Reader.GetString(2);
                        v_package_info.catalog_id = v_Data_Reader.GetString(3);
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
                            v_package_info.package_description_dtl = v_Data_Reader.GetString(9);
                        }

                        if (!v_Data_Reader.IsDBNull(10))
                        {
                            v_package_info.start_date = v_Data_Reader.GetString(10);
                        }

                        if (!v_Data_Reader.IsDBNull(11))
                        {
                            v_package_info.end_date = v_Data_Reader.GetString(11);
                        }
                        if (!v_Data_Reader.IsDBNull(12))
                        {
                            if (v_Data_Reader.GetString(12) == "PPV")
                            {
                                v_package_info.show_detail_flg = "Y";
                            }
                            else
                            {
                                v_package_info.show_detail_flg = "N";
                            }
                        }


                        v_package_info.status_description = "尚未付費";

                        string _sql2 = "select decode(trunc(t.start_date),null,t2.package_start_date_desc,to_char(trunc(t.start_date),'YYYY/MM/DD'))  start_date," +
        "decode(trunc(t.end_date),null,'無'" +
        ",to_char(trunc(t.end_date),'YYYY/MM/DD')) end_date," +
       "decode(t.start_date,null,'已付費,未啟用',decode(sign(end_date-sysdate),1,'已啟用','已到期') ) package_status  " +
  " from bsm_client_details t,bsm_package_mas t2 " +
 " where t2.status_flg = 'P' and t2.package_id=t.package_id " +
 "and t.mac_address =:mac_address " +
 "and t.package_id = :package_id";

                        OracleCommand _cmd2 = new OracleCommand(_sql2, conn);
                        _cmd2.BindByName = true;
                        _cmd2.Parameters.Add("mac_address", client_id);
                        _cmd2.Parameters.Add("package_id", v_package_info.package_id);
                        OracleDataReader v_Data_Reader2 = _cmd2.ExecuteReader();
                        while (v_Data_Reader2.Read())
                        {
                            if (!v_Data_Reader2.IsDBNull(0)) { v_package_info.start_date = v_Data_Reader2.GetString(0); }
                            if (!v_Data_Reader2.IsDBNull(1)) { v_package_info.end_date = v_Data_Reader2.GetString(1); }
                            if (!v_Data_Reader2.IsDBNull(2)) { v_package_info.status_description = v_Data_Reader2.GetString(2); }
                        }



                        v_result.Add(v_package_info);
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
        /// 取package detail 的內容
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <param name="package_id"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
 
        /// <summary>
        /// 取單一筆購買紀錄
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client_id"></param>
        /// <returns></returns>
        public GetClientInfo.purchase_info get_purchase_info_by_id(string client_id, string purchase_id)
        {
            client_id = client_id.ToUpper();
            GetClientInfo.purchase_info v_result = new GetClientInfo.purchase_info();

            conn.Open();
            try
            {
                string _sql;
                _sql = "Select d.package_cat1 cat_description," +
 "d.description package_name," +
 "to_char(a.purchase_date, 'YYYY/MM/DD') purchase_date," +
 "d.price_des," +
 "decode(a.pay_type,'REMIT','匯款',a.pay_type) pay_type," +
 "'************' || substr(a.card_no, 13, 4) card_no," +
 "a.mas_no purchase_id," +
 "a.approval_code approval_code," +
 "decode(c.start_date,null,'未啟用', decode(sign(end_date - sysdate), 1, '已啟用', '已到期')) status_description," +
 "to_char(c.start_date,'YYYY/MM/DD') start_date," +
 "to_char(c.end_date,'YYYY/MM/DD') end_date," +
 "to_char(a.purchase_date, 'YYYY/MM/DD HH24:MI') purchase_time, " +
 "d.package_cat1 cat_name," +
 "d.package_start_date_desc,d.package_end_date_desc ,a.tax_inv_no,a.tax_gift, " +
 "(select to_char(F_INVO_DATE,'YYYY/MM/DD') from tax_inv_mas inv where inv.f_invo_no=a.tax_inv_no) INVO_DATE," +
 "(select IDENTIFY_ID from tax_inv_mas inv where inv.f_invo_no=a.tax_inv_no) INVO_IDENTIFY_ID," +
 "a.cost_credits,a.after_credits" +
 " from bsm_purchase_mas a, bsm_client_details c, bsm_package_mas d, bsm_purchase_item e " +
 " where a.pk_no = c.src_pk_no (+) " +
 "  and e.mas_pk_no= a.pk_no and e.package_id = d.package_id (+) " +
 "and a.serial_id=:MAC_ADDRESS and a.mas_no = :PURCHASE_ID Order by to_char(a.purchase_date, 'YYYY/MM/DD') desc ";

                OracleCommand _cmd = new OracleCommand(_sql, conn);

                _cmd.BindByName = true;
                _cmd.Parameters.Add("MAC_ADDRESS", client_id);
                _cmd.Parameters.Add("PURCHASE_ID", purchase_id);
                OracleDataReader v_Data_Reader = _cmd.ExecuteReader();

                try
                {
                    if (v_Data_Reader.Read())
                    {
                        purchase_info v_purchase_info = new purchase_info();

                        if (!v_Data_Reader.IsDBNull(0))
                        {
                            v_purchase_info.catalog_description = v_Data_Reader.GetString(0);
                        }

                        if (!v_Data_Reader.IsDBNull(1))
                        {
                            v_purchase_info.package_name = v_Data_Reader.GetString(1);
                        }

                        if (!v_Data_Reader.IsDBNull(2))
                        {
                            v_purchase_info.purchase_date = v_Data_Reader.GetString(2);
                        }

                        if (!v_Data_Reader.IsDBNull(3))
                        {
                            v_purchase_info.price_description = v_Data_Reader.GetString(3);
                        }

                        if (!v_Data_Reader.IsDBNull(4))
                        {
                            v_purchase_info.pay_type = v_Data_Reader.GetString(4);
                        }
                        else
                        {
                            v_purchase_info.pay_type = "信用卡";
                        }

                        v_purchase_info.card_no = v_Data_Reader.GetString(5);
                        v_purchase_info.purchase_id = v_Data_Reader.GetString(6);

                        if (!v_Data_Reader.IsDBNull(7))
                        {
                            v_purchase_info.approval_code = v_Data_Reader.GetString(7);
                        }

                        if (!v_Data_Reader.IsDBNull(8))
                        {
                            v_purchase_info.status_description = v_Data_Reader.GetString(8);
                        }
                        else
                        { v_purchase_info.status_description = "未啟用"; }


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

                        if (!v_Data_Reader.IsDBNull(12))
                        {
                            v_purchase_info.catalog_name = v_Data_Reader.GetString(12);
                        }

                        if (!v_Data_Reader.IsDBNull(15))
                        {
                            // Tax Gift
                            if (v_Data_Reader.IsDBNull(16))
                            { v_purchase_info.invoice_no = v_Data_Reader.GetString(15);
                              v_purchase_info.invoice_date = v_Data_Reader.GetString(17);
                              v_purchase_info.invoice_einv_id = v_Data_Reader.GetString(18);
                            }
                            else
                            {
                                if (v_Data_Reader.GetString(16) == "Y")
                                {
                                    v_purchase_info.invoice_no = "捐贈";
                                }
                                else
                                {
                                    v_purchase_info.invoice_no = v_Data_Reader.GetString(15);
                                    v_purchase_info.invoice_date = v_Data_Reader.GetString(17);
                                    v_purchase_info.invoice_einv_id = v_Data_Reader.GetString(18);
                                }
                            }

                        }
                        else
                        {
                            v_purchase_info.invoice_no = "N/A";

                        }

                        if (!v_Data_Reader.IsDBNull(19))
                        {
                            v_purchase_info.cost_credits = v_Data_Reader.GetString(19);
                        
                        }
                        if (!v_Data_Reader.IsDBNull(20))
                        {
                            v_purchase_info.after_credits = v_Data_Reader.GetString(20);
                        }

                        v_purchase_info.purchase_datetime = v_Data_Reader.GetString(11);
                        v_result =  v_purchase_info;
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
        /// 取得Client Activation Code
        /// </summary>
        /// <param name="client_id"></param>
        /// <returns></returns>

        public string get_activation_code(string client_id)
        {
            conn.Open();

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
            conn.Open();

            client_id = client_id.ToUpper();

            string _sql = "Select bsm_cdi_service_test.get_cdi_info(mac_address) FROM bsm_client_mas a where a.MAC_ADDRESS=:CLIENT_ID";
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

        public string check_access(string token, string client_id, string asset_id)
        {
            conn.Open();

            string sql1 = "begin :P_RESULT := check_cdi_access(:P_CLIENT_ID,:P_ASSET_ID); end; ";
            string result = "N";
            try
            {
                OracleCommand cmd = new OracleCommand(sql1, conn);
                cmd.BindByName = true;
                OracleString v_o_result;
                cmd.Parameters.Add("P_CLIENT_ID", OracleDbType.Varchar2, 32, client_id, ParameterDirection.Input);
                cmd.Parameters.Add("P_ASSET_ID", OracleDbType.Varchar2, 32, asset_id, ParameterDirection.Input);
                cmd.Parameters.Add("P_RESULT", OracleDbType.Varchar2, 32, ParameterDirection.InputOutput);
                cmd.ExecuteNonQuery();

                v_o_result = (OracleString)cmd.Parameters["P_RESULT"].Value;
                result = v_o_result.ToString();
            }
            finally
            {
                conn.Close();
            }

            return result;

        }

        private credits_balance_info _get_credits_balance(string client_id)
        {
            credits_balance_info _result = new credits_balance_info();
            OracleCommand _cmd;
            string sql1 = "SELECT nvl(SUM(500),0) FROM Bsm_Client_Credits_Mas WHERE CLIENT_ID=:P_CLIENT_ID";
            _cmd = new OracleCommand(sql1, conn);
            _cmd.Parameters.Add("P_CLIENT_ID", client_id);
            OracleDataReader _reader = _cmd.ExecuteReader();
            if (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                {
                    _result.credits = (int)_reader.GetDecimal(0);
                    _result.credits_description = _result.credits.ToString() + "點";
                }
            }
            _cmd.Dispose();
            return _result;
        }


        public credits_balance_info get_credits_balance(string client_id)
        {
            credits_balance_info _result;
            conn.Open();

            try
            {
                _result = this._get_credits_balance(client_id);
            }
            finally
            {
                conn.Close();
            };



            return _result;
        }
    }


    /// <summary>
    /// 購買記錄查詢介面
    /// </summary>
    public class CDIInfo : JsonRpcHandler
    {

        private BSM_Info_Service_base _base;

        public CDIInfo()
        {
            String ConnectString = "";

            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            System.Configuration.ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["BsmConnectionString"];
                ConnectString = connString.ConnectionString;
            }

            _base = new BSM_Info_Service_base(ConnectString);
        }

        [JsonRpcMethod("get_client_info")]
        public string check_client_access(string client_id)
        {
            string v_result = _base.get_client_info( client_id);
            return v_result;
        }

    }

}
    

