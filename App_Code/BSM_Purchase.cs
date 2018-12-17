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
    /// �H�Υd����
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
    /// �ʶR����
    /// </summary>

    [JsonObject(MemberSerialization.OptIn)]
    public class BSM_Purchase_detail
    {
        /// <summary>
        /// item number �е��Ǹ�
        /// </summary>
        public int? item_no;

        /// <summary>
        /// ��ץN��
        /// </summary>
        public string package_id;

        /// <summary>
        /// content �����ӥN��
        /// </summary>
        public string item_id;

        /// <summary>
        /// ios_product_code
        /// </summary>

        public string ios_product_code;


        /// <summary>
        /// ����
        /// </summary>
        public string price;

        public string device_id;
    }

    /// <summary>
    /// �ʶR�ݨD�D��
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
	public class BSM_Purchase_Request 
	{

        /// <summary>
        /// session �ߤ@���
        /// </summary>
        public string session_uid;
     
        /// <summary>
        /// �I�ڤ覡:CREDIT
        /// </summary>
        public string pay_type;

        /// <summary>
        /// client id :�е�Mac Address (�j�g)
        /// </summary>
        public string client_id;
     
        /// <summary>
        /// �d��:VISA or MASTER
        /// </summary>
        public string card_type;

        /// <summary>
        /// �d��
        /// </summary>
        public string card_number;


        /// <summary>
        /// �����
        /// </summary>
        public string card_expiry;

        /// <summary>
        /// CVC2
        /// </summary>
        public string cvc2;

        /// <summary>
        /// �ʶR����
        /// </summary>
        //public System.Collections.Generic.List<BSM_Purchase_detail> details;
        public BSM_Purchase_detail[] details;

        /// <summary>
        /// �o������
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
    /// �ʶR����
    /// </summary>

    [JsonObject(MemberSerialization.OptIn)]
    public class BSM_Purchase_Info_detail
    {
        /// <summary>
        /// �Ǹ�
        /// </summary>
        public int? item_no;

        /// <summary>
        /// ��ץN��
        /// </summary>
        public string package_id;


        /// <summary>
        /// ��צW��
        /// </summary>
        public string package_name;

        /// <summary>
        /// content ���ӥN��
        /// </summary>
        public string item_id;

        /// <summary>
        /// item name
        /// </summary>
        public string item_name;

        /// <summary>
        /// ���B
        /// </summary>
        public int? price;

        /// <summary>
        /// �i����ɶ�:�p��
        /// </summary>
        public int? duration; // hour

        /// <summary>
        /// �i����ƶq
        /// </summary>
        public int? quota;

        public string device_id;
    }

    /// <summary>
    /// �ʶR��T
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BSM_Purchase_Info
    {

        /// <summary>
        /// session �ߤ@���
        /// </summary>
        public string session_uid;
        
        /// <summary>
        /// �q��N��
        /// </summary>
        public string purchase_id;

        /// <summary>
        /// �q�ʤ��
        /// </summary>
        public string purchase_date;


        /// <summary>
        /// ���A
        /// </summary>
        public string status;

        /// <summary>
        /// �o�����X
        /// </summary>
        public string invoice_no;

        /// <summary>
        /// �o�����
        /// </summary>
        public string invoice_date;


        /// <summary>
        /// �I�ڤ覡
        /// </summary>
        public string pay_type;


        /// <summary>
        /// client id 
        /// </summary>
        public string client_id;
        
        /// <summary>
        /// �d��
        /// </summary>
        public string card_type;

        /// <summary>
        /// �d��
        /// </summary>
        public string card_number;


        /// <summary>
        /// �����
        /// </summary>
        public string card_expiry;

        /// <summary>
        /// cvc2
        /// </summary>
        public string cvc2;


        /// <summary>
        /// ���v��
        /// </summary>
        public string approval_code;


        /// <summary>
        /// �o������
        /// </summary>
        public string invoice_gift_flg;

        
        /// <summary>
        /// �ʶR����
        /// </summary>
        public System.Collections.Generic.List<BSM_Purchase_Info_detail> details;

        public BSM_Purchase_Info()
        {
            // this.Details = new BSM_Purchase_Info_details();
            this.details = new List<BSM_Purchase_Info_detail>();
        }
    }
}
