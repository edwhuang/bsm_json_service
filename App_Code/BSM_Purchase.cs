using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Newtonsoft.Json.Linq;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Jayrock.Json;
using Jayrock.JsonRpc;
using Jayrock.JsonRpc.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace BSM
{
    /// <summary>
    /// 信用卡物件
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Credit
    {
        public string card_type;
        public string card_number;
        public string card_expiry;
        public string cvc2;
    }

    /// <summary>
    /// 購買明細
    /// </summary>

    [JsonObject(MemberSerialization.OptIn)]
    public class BSM_Purchase_detail
    {
        /// <summary>
        /// item number 請給序號
        /// </summary>
        public int? item_no;

        /// <summary>
        /// 方案代號
        /// </summary>
        public string package_id;

        /// <summary>
        /// content 的明細代號
        /// </summary>
        public string item_id;

        /// <summary>
        /// ios_product_code
        /// </summary>

        public string ios_product_code;


        /// <summary>
        /// 價格
        /// </summary>
        public string price;

        public string device_id;
    }

    /// <summary>
    /// 購買需求主檔
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
	public class BSM_Purchase_Request 
	{

        /// <summary>
        /// session 唯一鍵值
        /// </summary>
        public string session_uid;
     
        /// <summary>
        /// 付款方式:CREDIT
        /// </summary>
        public string pay_type;

        /// <summary>
        /// client id :請給Mac Address (大寫)
        /// </summary>
        public string client_id;
     
        /// <summary>
        /// 卡種:VISA or MASTER
        /// </summary>
        public string card_type;

        /// <summary>
        /// 卡號
        /// </summary>
        public string card_number;


        /// <summary>
        /// 到期日
        /// </summary>
        public string card_expiry;

        /// <summary>
        /// CVC2
        /// </summary>
        public string cvc2;

        /// <summary>
        /// 購買明細
        /// </summary>
        //public System.Collections.Generic.List<BSM_Purchase_detail> details;
        public BSM_Purchase_detail[] details;

        /// <summary>
        /// 發票捐贈
        /// </summary>
        public string invoice_gift_flg;

        /// <summary>
        /// Recurrent billing
        /// </summary>
        public string recurrent;

        public string imsi;

        public string ency_imsi;

        public string device_id;

        /// <summary>
        /// ios_receipt_info
        /// </summary>
        public string ios_receipt_info;
        public string promo_code;
        public string promote_code;
        public string order;

        public BSM_Purchase_Request()
        {
            this.invoice_gift_flg = "Y";
            this.recurrent = "O";
        }

	}

    /// <summary>
    /// 購買明細
    /// </summary>

    [JsonObject(MemberSerialization.OptIn)]
    public class BSM_Purchase_Info_detail
    {
        /// <summary>
        /// 序號
        /// </summary>
        public int? item_no;

        /// <summary>
        /// 方案代號
        /// </summary>
        public string package_id;


        /// <summary>
        /// 方案名稱
        /// </summary>
        public string package_name;

        /// <summary>
        /// content 明細代號
        /// </summary>
        public string item_id;

        /// <summary>
        /// item name
        /// </summary>
        public string item_name;

        /// <summary>
        /// 金額
        /// </summary>
        public int? price;

        /// <summary>
        /// 可播放時間:小時
        /// </summary>
        public int? duration; // hour

        /// <summary>
        /// 可播放數量
        /// </summary>
        public int? quota;

        public string device_id;
    }

    /// <summary>
    /// 購買資訊
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BSM_Purchase_Info
    {

        /// <summary>
        /// session 唯一鍵值
        /// </summary>
        public string session_uid;
        
        /// <summary>
        /// 訂單代號
        /// </summary>
        public string purchase_id;

        /// <summary>
        /// 訂購日期
        /// </summary>
        public string purchase_date;


        /// <summary>
        /// 狀態
        /// </summary>
        public string status;

        /// <summary>
        /// 發票號碼
        /// </summary>
        public string invoice_no;

        /// <summary>
        /// 發票日期
        /// </summary>
        public string invoice_date;


        /// <summary>
        /// 付款方式
        /// </summary>
        public string pay_type;


        /// <summary>
        /// client id 
        /// </summary>
        public string client_id;
        
        /// <summary>
        /// 卡種
        /// </summary>
        public string card_type;

        /// <summary>
        /// 卡號
        /// </summary>
        public string card_number;


        /// <summary>
        /// 到期日
        /// </summary>
        public string card_expiry;

        /// <summary>
        /// cvc2
        /// </summary>
        public string cvc2;


        /// <summary>
        /// 授權號
        /// </summary>
        public string approval_code;


        /// <summary>
        /// 發票捐贈
        /// </summary>
        public string invoice_gift_flg;

        
        /// <summary>
        /// 購買明細
        /// </summary>
        public System.Collections.Generic.List<BSM_Purchase_Info_detail> details;

        public BSM_Purchase_Info()
        {
            // this.Details = new BSM_Purchase_Info_details();
            this.details = new List<BSM_Purchase_Info_detail>();
        }
    }
}
