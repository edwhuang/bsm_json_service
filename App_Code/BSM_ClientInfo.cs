using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Jayrock.Json;
using Jayrock.JsonRpc;
using Jayrock.JsonRpc.Web;
using Jayrock.Json.Conversion;


namespace BSM
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BSM_ClientInfo
    {
#if NET20
        [JsonProperty]
        public int? region;
#else

        public int region;
#endif

        public string client_id;

        public string mac_address;

        public string client_status;

        public string owner_id;

        public string owner_email;

        public string owner_phone_no;

        public string owner_phone_status;

        public string sw_version;

        public BSM_ClientInfo()
        {
            this.region = 0;
            this.client_status = "unregister";
        }

    }

    public class BSM_Result_Base
    {
        /// <summary>
        /// result code response the process status to caller, result code format is XXX-AABCC, XXX is module name, AA for program ,B for sub program, CC is a serial number, some mesage is intergate , like BSM-00000 is process surcess.
        /// </summary>
        public string result_code;

        /// <summary>
        /// result message is message that want to response to function caller, some API (like purchase BSM-00403) will put the sub error code in message
        /// </summary>
        public string result_message;

    }

    /// <summary>
    /// the BSM_Result is a result object, result process sucess or fail or another result code, or result message
    /// </summary>
    public class BSM_Result
    {

        /// <summary>
        /// result code response the process status to caller, result code format is XXX-AABCC, XXX is module name, AA for program ,B for sub program, CC is a serial number, some mesage is intergate , like BSM-00000 is process surcess.
        /// </summary>
        public string result_code;

        /// <summary>
        /// result message is message that want to response to function caller, some API (like purchase BSM-00403) will put the sub error code in message
        /// </summary>
        public string result_message;

        public JsonObject message;

        public BSM_ClientInfo client;

        public string purchase_id;

        public BSM_Info.purchase_info_list purchase;

        public List<BSM_Info.purchase_info_list> purchase_list;

        public BSM_Result()
        {
            this.result_code = "BSM-00001";
            this.result_message = "FAILURE";
            this.purchase = new BSM_Info.purchase_info_list();
            this.purchase_list = new List<BSM_Info.purchase_info_list>(); 
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class BsmPurchaseResult
    {
        [JsonProperty]
        public string ResultCode;
        [JsonProperty]
        public string ResultMessage;
        [JsonProperty]
        public string PurchaseId;
        [JsonProperty]
        public BSM_ClientInfo client;
    }

}
