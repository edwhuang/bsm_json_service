using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// bsm order register
/// </summary>
public class bsm_order
{
    public string   customer_id;
	public string   order_id;
	public string   serial_number;
	public string   mac_address;
	public string   customer_name;
	public string   sales_name;
	public int?   type;
	public int?   sex;
	public string   ssn;
	public string   birthday;
	public string   vat_number;
	public string   mobile_number;
	public string   day_contact_phone_number;
	public string   night_contact_phone_number;
	public string   fax;
	public string   email;
	public string   shipping_addr_zip_code;
	public string   shipping_address;
	public string   permanent_addr_zip_code;
	public string   permanent_address;
	public string   billing_address_zip_code;
	public string   billing_address;
	public string   invoice_type; // there are 2 different type of receipts - !TAX code!
	public string   require_invoice;
	public string   invoice_carrier_id;
	public string   invoice_love_code;
	public string   stock_brokerage_service_name;
	public string   stock_brokerage_service_branch_office_name;
	public string[]   packages_list; // this part needs to be confirmed with Edward
	public string   remark;
/*

        /// <summary>
        /// 客編 (DLC 客編)
        /// </summary>
        public string customer_id;

        /// <summary>
        ///  Name
        /// </summary>
        public string name;

    
        /// <summary>
        /// customer type : TYPE 0 :自然人 1公司 default: 0
        /// </summary>
        public int? customer_type;

        /// <summary>
        /// 性別  0: 無法分辨，可能是公司法人 1: 男性 2.女性 default:0
        /// </summary>
        public int? gender;

        /// <summary>
        /// UNIFIEDID_TW 為身份證字號 如果不是個人，不填
        /// </summary>
        public string unifiedid_tw;

        /// <summary>
        /// company_uid: 統編
        /// </summary>
        public string company_uid;

        /// <summary>
        /// 電話號碼
        /// </summary>
        public string mobile_phone_no;

        /// <summary>
        /// 負責人
        /// </summary>
        public string company_owner;

        public string dayphone;
        public string nightphone;
        public string fax;
        
        public string email;

        public string zip;
        public string address;

        public string inst_zip;
        public string inst_address;
        
        public string bill_zip;
        public string bill_address;
        public string birthday ;

        /// <summary>
        /// tax_code: 'OUTTAX0': 免開發票','OUTTAX1': 二聯式發票,'OUTTAX2':三聯式發票
        /// </summary>
        public string tax_code;

        /// <summary>
        /// 發票捐贈
        /// </summary>
        public string invoice_flg;

        /// <summary>
        /// 共通性載具ID
        /// </summary>
        public string carrierid;

        /// <summary>
        /// 共通性載具ID
        /// </summary>
        public string lovecode;



        /// <summary>
        /// 經銷商單號
        /// </summary>
        public string src_order_id;

        /// <summary>
        /// DLC 單號
        /// </summary>
        public string order_id;

        /// <summary>
        /// DLC 序號
        /// </summary>
        public string serial_id;

        /// <summary>
        /// Device id
        /// </summary>
        public string device_id;

        /// <summary>
        /// 方案代號
        /// </summary>
        public string package_id;

        /// <summary>
        /// 股市經銷商代號
        /// </summary>
        public string stock_provider;

        /// <summary>
        /// 經銷商營業所資訊
        /// </summary>
        public string stock_store;

        /// <summary>
        /// 經銷商營業員
        /// </summary>
        /// 
        public string stock_sale;

        public string content;
        public string remark; */
        public bsm_order()
	{

        
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}
}
