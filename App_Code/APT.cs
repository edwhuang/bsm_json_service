using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using System.Net;
using System.Xml.Linq;
using System.Xml;

using System.Collections.Generic;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using Jayrock.JsonRpc;
using Jayrock.JsonRpc.Web;




/// <summary>
/// APT 的摘要描述
/// </summary>
public class APT
{
    const string REGISTER = "register";
    const string GETBILL = "getbill";
    const string CHECKSERVICE = "checkservice";
    const string UNREGISTER = "unregister";
    const string GETMDN = "getmdn";

    static readonly string apt_xml_path = @"C:\";
  //  static readonly string apt_url = @"http://localhost:2912/BSM_JSON_Service/BSM_APT_TEST.ashx";
   // static readonly string apt_url = @"http://s-bsm01.tw.svc.litv.tv/BSM_JSON_Service/BSM_APT_TEST.ashx";
    static readonly string apt_url = @"http://cp.qma.com.tw";

    Dictionary<string, string> apt_api_urls;
    Dictionary<string, string> apt_api_tmp;
 
    string _register_url = apt_url + @"/is/reverseSubscribe";
    string _register_temp = apt_xml_path + @"subscription.xml";
    string _subscription_content ;
    string _register_content;
    string _removce_content;
    string _path;

    public APT()
	{
        apt_api_urls = new Dictionary<string, string>();
        apt_api_tmp = new Dictionary<string, string>();

        apt_api_urls[REGISTER] = apt_url + @"/is/reverseSubscribe";
        apt_api_urls[GETBILL] = apt_url + @"";
        apt_api_urls[CHECKSERVICE] = apt_url + @"?method=check";
        apt_api_urls[UNREGISTER] = apt_url + @"/is/reverseUnsubscribe";
        apt_api_urls[GETMDN] = apt_url + @"";

        apt_api_tmp[REGISTER] = apt_xml_path + @"/subscription.xml";
        apt_api_tmp[GETBILL] = apt_xml_path + @"/getBill.xml";
        apt_api_tmp[CHECKSERVICE] = apt_xml_path + @"/check_service.xml";
        apt_api_tmp[UNREGISTER] = apt_xml_path + @"/unSubscription.xml";

        string _path = HttpContext.Current.Server.MapPath("~/subscription.xml");
	}

    public string  register(string IMSI,string ProductCode, string TransationId)
    {
        string MIN = IMSI.Substring(5);
        return apt_webrequest(REGISTER,MIN,ProductCode,TransationId);
    }

    public string getbill(string MIN, string ProductCode)
    {
        return apt_webrequest(GETBILL, MIN, ProductCode, "");
    }

    public string check_service(string MIN, string ProductCode)
    {
        return apt_webrequest(CHECKSERVICE, MIN, ProductCode, "");
    }

    public string remove(string MIN, string ProductCode, string TransationId)
    {
        return apt_webrequest(UNREGISTER, MIN, ProductCode, TransationId);
    }

 /*   public string getMDN(string IMSI)
    {
        string MIN = IMSI.Substring(5);
         return apt_webrequest(GETMDN, MIN, "", "");
    }
    */

    public string getMDN(string IMSI)
    {
        string MIN = IMSI.Substring(5);
        if (IMSI.Substring(0, 5) != "46605") { return "N"; }
        string result = this.WebRequest("https://soa.aptg.com.tw/profile-service/ProfileService",
            @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:prof=""http://www.aptg.com.tw/ws/api/core/ProfileService"">
   <soapenv:Header/>
   <soapenv:Body>
      <prof:getUserProfile>
         <!--Optional:-->
         <serviceID>LiTVmovie</serviceID>
         <!--Optional:-->
         <servicePWD>Tgctaiwan27740083</servicePWD>
         <min>"+MIN+@"</min>
      </prof:getUserProfile>
   </soapenv:Body>
</soapenv:Envelope>");
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        var doc = XElement.Parse(result);

        result = "N";
        
        var Xq = doc.Descendants("contractStatus");
        foreach(XElement a in Xq) result=a.Value;
        return result;
    }

    private string apt_webrequest(string method,string MIN,string ProductCode,string TransationId)
    {
        string apt_api_url = apt_api_urls[method];
        string apt_result;
        if (method == GETMDN)
        {
            apt_api_url = apt_api_urls[method] + "?method=getMDN&MIN=" + MIN;
            apt_result = WebRequest(apt_api_url);
        } else
        {
            string apt_content_temp = File.ReadAllText(apt_api_tmp[method]);
            string apt_content = string.Format(apt_content_temp, MIN, ProductCode, TransationId);
            apt_result = WebRequest(apt_api_url, apt_content);
        }

        XmlDocument result_xml = new XmlDocument();
        result_xml.LoadXml(apt_result);
        JsonObject result_json = new JsonObject();

        foreach (XmlNode result_no in result_xml.ChildNodes[1].ChildNodes)
        {
            if (result_no.HasChildNodes)
            {
                JsonObject child_json = new JsonObject();
                foreach (XmlNode child_node in result_no.ChildNodes)
                {
                    child_json.Add(child_node.Name, child_node.InnerText);
                }
                result_json.Add(result_no.Name, child_json);
            }
        }
        string result;
        TextWriter aa = new StringWriter();
        JsonWriter jw = new JsonTextWriter(aa);
        result_json.Export(jw);
        result = aa.ToString();
        return result;
    }

    private string WebRequest(string url, string postData)
    {
        string ret = string.Empty;

        StreamWriter requestWriter;

        var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
        if (webRequest != null)


        {
            webRequest.Method = "POST";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;
            webRequest.ContentType = "text/xml;charset=utf-8";
            using (requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(postData);
            } 
        }

        HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
        Stream resStream = resp.GetResponseStream();
        StreamReader reader = new StreamReader(resStream);
        ret = reader.ReadToEnd();

        return ret;
    }

    private string WebRequest(string url)
    {
        string ret = string.Empty;

        var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
        if (webRequest != null)
        {
            webRequest.Method = "GET";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;
            webRequest.ContentType = "text/xml;charset=utf-8";
        }

        HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
        Stream resStream = resp.GetResponseStream();
        StreamReader reader = new StreamReader(resStream);
        ret = reader.ReadToEnd();

        return ret;
    }
}
