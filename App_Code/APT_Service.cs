using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Data;
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
using System.Security.Cryptography;
using log4net;
using log4net.Config;



/// <summary>
/// APT_Service 的摘要描述
/// </summary>
public class APT_Service: JsonRpcHandler
{
    const string ContainerName = "LiTV BSM RAS Key";

    APT apt;
	public APT_Service()
	{
        apt = new APT();
	}

 [JsonRpcMethod("atp_public_key")]
    public RSAParameters atp_key()
    {
           
           CspParameters cp = new CspParameters();
            cp.KeyContainerName=ContainerName;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.FromXmlString(@"<RSAKeyValue><Modulus>1p7/+phN+WGYYoGiHtee2C40rAnF76tc1HZ1ePNM2H3Nz58GSyWfcoO7glsS67XhEnyrRIFZpsUvZDmhKuWguQM2YqJz3PSN0k6ny0sgmVfEjJCtQJyE5w4A7jjy2dGN+AHVKOQ3mQGihrZZP0rX8RctrvIvcG5NPymcYnSmFs8=</Modulus><Exponent>AQAB</Exponent><P>9JhiMyL3D8u8b/NR7hChpxBZQcZJ9VgSZQYpdmaigngN1ZD/e8qDDS00fBDRwGeV2X6gWG+YP9EcFTvUGXNeHw==</P><Q>4KDURV+dmVgVBiDGLC34kYTSZr6vpRBjNCK4KbMGHwrvjSM3d3Pst3M5YAVfP9Gto/A6caVVQUaLWJHMAvLRUQ==</Q><DP>v7IKDG/T19JiHg9B3+Wy+78pZQ5l+l4LFJgOuNfZd41lskKQqNFfgl0ybCW2bigA3lOKkaTsWt+lNdMM+OFdGw==</DP><DQ>xMpBjVFaGYiASrEVzIittpbdWWP/LyXvMzKjkuyjFTkYataKdl0Z6hHJFyU9sAR7Eh+YpA2LZjduRrbcO00NMQ==</DQ><InverseQ>zVuq+3e7cJbIcXiEXeSYeMhbBnJj8/AYjoY4ny5DoRdOFwg4yRsil+t1p0fqh+JdmN4rXYZoAhNAeW7FEmB7Dg==</InverseQ><D>WVcMtbNK2iHPPko6Q158h/8L/AWBeNg7p1G8auHUvlX4I5JOG2AI9LFzj7r7sFUg4QI2hSjXOf3hUXZP6bx232nsdsLq5q5F7R46taAGo53Gqi9Z/N0bG/uIqA3Ec55NI7C0FSK7hXgY9GF/Amb7kMZsUoJzLscxxPzv/6SezWE=</D></RSAKeyValue>");
           
           RSAParameters publicParamter = rsa.ExportParameters(false);

           return publicParamter;
    }

 [JsonRpcMethod("atp_prik")]
 public string atp_pri(string original, string xmlString)
 {

     CspParameters cp = new CspParameters();
     cp.KeyContainerName = ContainerName;
     RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

    string publicParamter = rsa.ToXmlString(true);

     return publicParamter;
 }

 [JsonRpcMethod("atp_encrypt")]
 public string RsaEncrypt(string data)
 {
     RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
     RSAParameters pub_key = atp_key();
     rsa.ImportParameters(pub_key);
     byte[] data_array = System.Text.Encoding.Default.GetBytes(data);
     byte[] ency_data = rsa.Encrypt(data_array, false);
     string out_data = Convert.ToBase64String(ency_data);
     return out_data;
 }

 [JsonRpcMethod("atp_decrypt")]
 public string RsaDecrypt(string data)
 {
     RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
     string xmlstr = @"<RSAKeyValue><Modulus>1p7/+phN+WGYYoGiHtee2C40rAnF76tc1HZ1ePNM2H3Nz58GSyWfcoO7glsS67XhEnyrRIFZpsUvZDmhKuWguQM2YqJz3PSN0k6ny0sgmVfEjJCtQJyE5w4A7jjy2dGN+AHVKOQ3mQGihrZZP0rX8RctrvIvcG5NPymcYnSmFs8=</Modulus><Exponent>AQAB</Exponent><P>9JhiMyL3D8u8b/NR7hChpxBZQcZJ9VgSZQYpdmaigngN1ZD/e8qDDS00fBDRwGeV2X6gWG+YP9EcFTvUGXNeHw==</P><Q>4KDURV+dmVgVBiDGLC34kYTSZr6vpRBjNCK4KbMGHwrvjSM3d3Pst3M5YAVfP9Gto/A6caVVQUaLWJHMAvLRUQ==</Q><DP>v7IKDG/T19JiHg9B3+Wy+78pZQ5l+l4LFJgOuNfZd41lskKQqNFfgl0ybCW2bigA3lOKkaTsWt+lNdMM+OFdGw==</DP><DQ>xMpBjVFaGYiASrEVzIittpbdWWP/LyXvMzKjkuyjFTkYataKdl0Z6hHJFyU9sAR7Eh+YpA2LZjduRrbcO00NMQ==</DQ><InverseQ>zVuq+3e7cJbIcXiEXeSYeMhbBnJj8/AYjoY4ny5DoRdOFwg4yRsil+t1p0fqh+JdmN4rXYZoAhNAeW7FEmB7Dg==</InverseQ><D>WVcMtbNK2iHPPko6Q158h/8L/AWBeNg7p1G8auHUvlX4I5JOG2AI9LFzj7r7sFUg4QI2hSjXOf3hUXZP6bx232nsdsLq5q5F7R46taAGo53Gqi9Z/N0bG/uIqA3Ec55NI7C0FSK7hXgY9GF/Amb7kMZsUoJzLscxxPzv/6SezWE=</D></RSAKeyValue>";
     rsa.FromXmlString(xmlstr);
     byte[] decy_data = Convert.FromBase64String(data);
     byte[] out_data = rsa.Decrypt(decy_data, false);

     string out_string = System.Text.Encoding.Default.GetString(out_data);
     return out_string;
 }

    [JsonRpcMethod("apt.register")]
   public JsonObject apt_register(string MIN, string ProductCode, string TransationId)
    {
         return stringtojson(apt.register(MIN, ProductCode, TransationId));
    }

    [JsonRpcMethod("remove")]
    public string apt_remove(string MIN, string ProductCode,string TransationId)
    {
        return apt.remove(MIN, ProductCode, TransationId);
    }

    [JsonRpcMethod("getbill")]
    public string apt_getbill(string MIN, string ProductCode)
    {
        return apt.getbill(MIN, ProductCode);
    }

    [JsonRpcMethod("apt.check_service")]
    public JsonObject apt_check_service(string MIN, string ProductCode)
    {
        string result_str = apt.check_service(MIN, ProductCode);
        JsonObject result = stringtojson(result_str);
        return result;
    }

    [JsonRpcMethod("apt.get_MDN")]
    public string apt_getMDN(string MIN)
    {
     /*   var b = new ProfileService();
        var a = new getUserProfile();
        a.min = "0080652669";
        a.serviceID = "LiTVmovie";
        a.servicePWD = "Tgctaiwan27740083";

        var c = b.getUserProfile(a); */
      //  JsonObject result = new JsonObject();
       string result_str = apt.getMDN(MIN);
      //  JsonObject result = stringtojson(result_str); 
        return result_str;
    }

    private JsonObject stringtojson(string json_str)
    {
        JsonObject result = new JsonObject();
        JsonReader rd =  new JsonTextReader(new StringReader(json_str));
        result.Import(rd);
        return result;
    }


}
