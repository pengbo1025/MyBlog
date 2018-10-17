using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Common
{
    public class General : System.Web.UI.Page
    {
        //获取Url返回信息：GET
        public static string RequestUrl(string strUrl, Encoding encType)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Method = "GET";
                request.Timeout = 10000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = null;
                stream = response.GetResponseStream();
                string strResult = new StreamReader(stream, encType).ReadToEnd();
                response.Close();
                return strResult;
            }
            catch (Exception excResult)
            {
                return "" + excResult.Message ;
            }
        }

        //获取Url返回信息：POST
        public static string RequestUrl(string strUrl, string strPostData)
        {
            try
            {
                //构造请求
                HttpWebRequest hwrRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                hwrRequest.Method = "POST";
                hwrRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-shockwave-flash, */*";
                hwrRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
                hwrRequest.Headers.Add("Accept-Language", "zh-cn");
                hwrRequest.Headers.Add("Cache-Control", "gzip, deflate");
                hwrRequest.Headers.Add("KeepAlive", "TRUE");
                hwrRequest.Headers.Add("ContentLength", strPostData.Length.ToString());
                hwrRequest.ContentType = "application/x-www-form-urlencoded";
                hwrRequest.Referer = strUrl;
                hwrRequest.Headers.Add("UA-CPU", "x86");
                hwrRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                hwrRequest.Timeout = 10000;
                hwrRequest.ServicePoint.Expect100Continue = false;

                //发送请求
                byte[] bytPostData = Encoding.UTF8.GetBytes(strPostData);
                Stream strStream = hwrRequest.GetRequestStream();
                strStream.Write(bytPostData, 0, bytPostData.Length);
                strStream.Close();

                //就收应答
                HttpWebResponse hwrResponse = (HttpWebResponse)hwrRequest.GetResponse();
                Stream strStream1 = null;
                if (hwrResponse.ContentEncoding == "gzip")
                {
                    System.IO.Compression.GZipStream gzsStream = new System.IO.Compression.GZipStream(hwrResponse.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
                    strStream1 = gzsStream;
                }
                else
                {
                    strStream1 = hwrResponse.GetResponseStream();
                }

                string strResult = new StreamReader(strStream1, System.Text.Encoding.UTF8).ReadToEnd();
                hwrResponse.Close();

                return strResult;
            }
            catch (Exception excResult)
            {
                return "" + excResult.Message ;
            }
        }

        //获得用户IP地址
        public static string IpAddress()
        {
            string strSourceIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(strSourceIP))
            {
                strSourceIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            strSourceIP = ToString(strSourceIP).Trim().Split(';')[0].Split(',')[0].Trim();
            return strSourceIP;
        }

        //获取IP地址
        public static string ServerIpAddress()
        {
            string strIpInfo = RequestUrl("http://apps.game.qq.com/comm-htdocs/ip/get_ip.php", "");
            string strIpAddress = FindJson(strIpInfo, "ip_address");
            return strIpAddress;
        }

        /*Object转字符串*/
        public static string ToString(object objValue, string strDefaultValue)
        {
            if (objValue != null)
                return objValue.ToString();
            else
                return strDefaultValue;
        }

        public static string ToString(object objValue)
        {
            return ToString(objValue, "");
        }



        //根据IP获取登录城市
        public static string GetCity(string strIpAddress)
        {
            string strCity = "";
            string strUrl = "http://opendata.baidu.com/api.php?query=" + strIpAddress + "&co=&resource_id=6006&t=1373015938281&ie=utf8&oe=gbk&cb=bd__cbs__thtik7&format=json&tn=baidu";
            string strResult = RequestUrl(strUrl, Encoding.Default);
            if (strResult != "")
            {
                try
                {
                    strResult = strResult.Replace("/**/bd__cbs__thtik7(", "").Replace(");", "");
                    object objResult = Json2Obj.JsonDecode(strResult);
                    Hashtable hasResult = (Hashtable)objResult;
                    ArrayList arrResult = (ArrayList)hasResult["data"];
                    hasResult = (Hashtable)arrResult[0];
                    strCity = hasResult["location"].ToString();
                }
                catch (Exception excInfo)
                {
                    strCity = ""+excInfo.Message;
                }
            }
            return strCity;
        }

        //获取时间戳的毫秒数
        public static string Millisecond()
        {
            DateTime curDate = DateTime.Now.AddHours(-8);
            DateTime dtOld = new System.DateTime(1970, 1, 1);
            TimeSpan tSpan = curDate - dtOld;
            long intMillisecond = Convert.ToInt64(tSpan.TotalMilliseconds);
            return intMillisecond.ToString();
        }

        //查找Key值
        public static string FindJson(string strJson, string strKey)
        {
            string strValue = "";
            int intStartIndex = strJson.IndexOf(strKey);
            int intEndIndex = strJson.IndexOf(",", intStartIndex);
            if (intEndIndex > -1)
            {
                strValue = strJson.Substring(intStartIndex, intEndIndex - intStartIndex).Split('"')[2];
            }
            else
            {
                strValue = strJson.Substring(intStartIndex).Split('"')[2];
            }
            return strValue;
        }
    }
}
