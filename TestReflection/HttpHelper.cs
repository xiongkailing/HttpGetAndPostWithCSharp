using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace HttpFaker
{
    public delegate void ReturnHandler(string response);
    public class HttpHelper
    {
        private static CookieContainer cookie = new CookieContainer();
        public static void HttpGet(string url, object data, ReturnHandler handler)
        {
            if (data != null)
            {
                Type type = data.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                string urlParams = "?";
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    urlParams += propertyInfos[i].Name;
                    urlParams += "=";
                    urlParams += propertyInfos[i].GetValue(data);
                    if (i != propertyInfos.Length - 1)
                        urlParams += "&";
                }
                url += urlParams;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            handler(retString);
        }
        public static void HttpPost(string url, object data, ReturnHandler handler)
        {
            string urlParams = string.Empty;
            if (data != null)
            {
                Type type = data.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();

                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    urlParams += propertyInfos[i].Name;
                    urlParams += "=";
                    urlParams += propertyInfos[i].GetValue(data);
                    if (i != propertyInfos.Length - 1)
                        urlParams += "&";
                }
                url += "?" + urlParams;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";        
            //request.ContentLength = Encoding.UTF8.GetByteCount(urlParams);
            request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(urlParams);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            handler(retString);
        }
    }
}