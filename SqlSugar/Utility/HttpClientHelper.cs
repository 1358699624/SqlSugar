using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySqlSugar.Utility.ApiResult;

namespace MySqlSugar.Utility
{
    public  class HttpClientHelper
    {

            private static readonly HttpClient HttpClient;

            static HttpClientHelper()
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };
                HttpClient = new HttpClient(handler);
            }


            #region GET

            /// <summary>
            /// get请求，可以对请求头进行多项设置
            /// </summary>
            /// <param name="url"></param>
            /// <returns></returns>
            public static string GetResponseByGet(string url)
            {
                return GetResponseByGet(url, null);
            }
            /// <summary>
            /// get请求，可以对请求头进行多项设置
            /// </summary>
            /// <param name="url"></param>
            /// <param name="paramArray"></param>
            /// <returns></returns>
            public static string GetResponseByGet(string url, List<KeyValuePair<string, string>> paramArray)
            {
                string result = "";

                if (paramArray != null)
                    url = url + "?" + BuildParam(paramArray);

                var httpclient = HttpClientHelper.HttpClient;
                var response = httpclient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    Stream myResponseStream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                }

                return result;
            }


            public static string GetResponseBySimpleGet(string Url)
            {
                return GetResponseBySimpleGet(Url, null);
            }
            public static string GetResponseBySimpleGet(string Url, List<KeyValuePair<string, string>> paramArray)
            {
                string result = string.Empty;

                var httpclient = HttpClientHelper.HttpClient;

                if (paramArray != null)
                    Url = Url + "?" + BuildParam(paramArray);

                //var result = httpclient.GetStringAsync(Url).Result;

                try
                {
                    using (HttpClient http = new HttpClient())
                    {

                        HttpResponseMessage message = null;

                        http.DefaultRequestHeaders.Add("Bearer", "");

                        var task = http.GetAsync(Url);
                        message = task.Result;

                        if (message != null && message.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (message)
                            {
                                result = message.Content.ReadAsStringAsync().Result;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return result;
            }


            #endregion

            #region POST

            public static string HttpPostRequestAsync(string Url, List<KeyValuePair<string, string>> paramArray, string ContentType = "application/json")
            {
                return HttpPostRequestAsync(Url, paramArray, null, ContentType);
            }

            public static string HttpPostRequestAsync(string Url, object objBody, string ContentType = "application/json")
            {
                return HttpPostRequestAsync(Url, null, objBody, ContentType);
            }

            public static string HttpPostRequestAsync(string Url, List<KeyValuePair<string, string>> paramArray, object objBody, string ContentType = "application/x-www-form-urlencoded")
            {
                string result = "";

                var postData = BuildParam(paramArray);
                string strBody = string.Empty;

                if (paramArray != null)
                    Url = string.Concat(new string[] { Url, "?", postData });

                if (objBody != null)
                   
                    strBody = objBody as string;

            try
                {
                    using (HttpClient http = new HttpClient())
                    {
 
                        HttpResponseMessage message = null;

                        using (StringContent content = new StringContent(strBody, Encoding.UTF8, ContentType))
                        {
                            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

                            var task = http.PostAsync(Url, content);
                            message = task.Result;
                        }

                        if (message != null && message.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (message)
                            {
                                result = message.Content.ReadAsStringAsync().Result;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return result;
            }



            #endregion

            private static string Encode(string content, Encoding encode = null)
            {
                if (encode == null) return content;

                return System.Web.HttpUtility.UrlEncode(content, Encoding.UTF8);

            }

            private static string BuildParam(List<KeyValuePair<string, string>> paramArray, Encoding encode = null)
            {
                string url = "";

                if (encode == null) encode = Encoding.UTF8;

                if (paramArray != null && paramArray.Count > 0)
                {
                    var parms = "";
                    foreach (var item in paramArray)
                    {
                        parms += string.Format("{0}={1}&", Encode(item.Key, encode), Encode(item.Value, encode));
                    }
                    if (parms != "")
                    {
                        parms = parms.TrimEnd('&');
                    }
                    url += parms;

                }
                return url;
            }

        

    }
}
