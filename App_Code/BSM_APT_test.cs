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
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Xml;

/// <summary>
/// BSM_APT_test 的摘要描述
/// </summary>
public class BSM_APT_test:IHttpHandler
{
	public BSM_APT_test()
	{
	}

    public void ProcessRequest(HttpContext context)
    {

        HttpRequest request = context.Request;
        HttpResponse response = context.Response;
        string method = request.QueryString["method"];

        if (method == "getMDN")
        {
            if (request.QueryString["MIN"] == "123123123" || request.QueryString["MIN"] == "0080652669")
            {
                response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?><U-MAX><UserInfo><MIN>46605123123</MIN><MDN>0900000123</MDN><OSSUserStatus>0</OSSUserStatus></UserInfo></U-MAX>");
            }
            else if (request.QueryString["MIN"] == "123123124")
            {
                response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?><U-MAX><UserInfo><MIN>46605123124</MIN><MDN>0900000124</MDN><OSSUserStatus>1</OSSUserStatus></UserInfo></U-MAX>");
            }
            else if (request.QueryString["MIN"] == "123123125")
            {
                response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?><U-MAX><UserInfo><MIN>46605123125</MIN><ErrorCode>0x01020002</ErrorCode></UserInfo></U-MAX>");
            }
            else
            {
                response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?><U-MAX><UserInfo><ErrorCode>0x0102000F</ErrorCode></UserInfo></U-MAX>");
            }
        }
        if (method == "check")
        {
            long pos = context.Request.InputStream.Position;
            var read = new StreamReader(context.Request.InputStream);
            string str_content = read.ReadToEnd();
            context.Request.InputStream.Position = pos;

            XmlDocument x_doc = new XmlDocument();
            x_doc.LoadXml(str_content);
            XmlNodeList xnList = x_doc.SelectNodes("/U-MAX/CheckSubscription/MIN");
            string MIN = xnList[0].InnerText;

            if (MIN == "123123123")
            {
                response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?><U-MAX><CheckSubscription><MIN>46605123125</MIN><ProductCode>12321</ProductCode><SpCode>0x01020002</SpCode></CheckSubscription></U-MAX>");
            }
            else if (MIN == "123123124")
            {
                response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?><U-MAX><VaildError><VaildErrorCode>17170434</VaildErrorCode></VaildError></U-MAX>");
            }
        }

        if (method == "register")
        {
            long pos = context.Request.InputStream.Position;
            var read = new StreamReader(context.Request.InputStream);
            string str_content = read.ReadToEnd();
            context.Request.InputStream.Position = pos;

            XmlDocument x_doc = new XmlDocument();
            x_doc.LoadXml(str_content);
            XmlNodeList xnList = x_doc.SelectNodes("/U-MAX/ReverseSubscription/MIN");
            string MIN = xnList[0].InnerText;

            if (MIN == "123123123"||MIN == "0080652669")
            {
                response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?><U-MAX><ReverseSubscription><MIN>46605123125</MIN><ProductCode>12321</ProductCode><SpCode>0x01020002</SpCode><TransactionID>12312312</TransactionID></ReverseSubscription></U-MAX>");
            }
            else if (MIN == "123123124")
            {
                response.Write(@"<?xml version=""1.0"" encoding=""utf-8"" ?><U-MAX><ReverseSubscription><MIN>46605123125</MIN><ProductCode>12321</ProductCode><SpCode>0x01020002</SpCode><TransactionID>12312312</TransactionID><ErrorCode>0x010A0002</ErrorCode></ReverseSubscription></U-MAX>");
            }
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
