
using System;

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace winformUI.common
{
   public static  class HtmlParser
    {
        public static string GetResponse(string url, out string statusCode)
        {
            string result = string.Empty;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                statusCode = response.StatusCode.ToString();

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }



        public static string PostResponse(string url, string postData, out string statusCode)
        {
            string result = string.Empty;
            //设置Http的正文
            HttpContent httpContent = new StringContent(postData);
            //设置Http的内容标头
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //设置Http的内容标头的字符
            httpContent.Headers.ContentType.CharSet = "UTF-8";
            using (HttpClient httpClient = new HttpClient())
            {
                //异步Post
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                //输出Http响应状态码
                statusCode = response.StatusCode.ToString();
                //确保Http响应成功
                if (response.IsSuccessStatusCode)
                {
                    //异步读取json
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }


        public static string HttpGet(string url)
        {
            string responsestr = "";
            try
            {
                HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
                req.Accept = "*/*";
                req.Method = "GET";
                req.ContentType = "text/html; charset=UTF-8";
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
                using (HttpWebResponse response = req.GetResponse() as HttpWebResponse)
                {
                    Stream stream;
                    if (response.ContentEncoding.ToLower().Contains("gzip"))
                    {
                        stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    }
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))
                    {
                        stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress);
                    }
                    else
                    {
                        stream = response.GetResponseStream();
                    }
                    using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                    {
                        responsestr = reader.ReadToEnd();
                        stream.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("网络异常请稍后再试，或者检查您的网络设置。");
            }
            
            return responsestr;
        }
        public static Encoding GetEncoding(string CharacterSet)
        {
            switch (CharacterSet)
            {
                case "gb2312": return Encoding.GetEncoding("UTF-8");
                case "UTF-8": return Encoding.UTF8;
                default: return Encoding.Default;
            }
        }

        //static void Main(string[] args)

        //{

        //    ////初始化网络请求客户端

        //    //HtmlWeb webClient = new HtmlWeb();

        //    ////初始化文档



        //    //HtmlDocument doc = webClient.Load("https://detail.tmall.com/item.htm?spm=a1z10.4-b-s.w5003-21523935230.2.525d6e70tKRKuf&id=590927994427&scene=taobao_shop");

        //    ////查找节点

        //    //HtmlNodeCollection titleNodes = doc.DocumentNode.SelectNodes("//a[@class='slogo-shopname']");

        //    //if (titleNodes != null)
        //    //{
        //    //    foreach (var item in titleNodes)

        //    //    {
        //    //        Console.WriteLine("店铺名称："+item.InnerText);
        //    //    }

        //    //}

        //    //Console.Read();




        //    //String url = "https://item.taobao.com/item.htm?spm=a230r.1.14.284.2622180fbT6du1&id=584968056618&ns=1&abbucket=3#detail";
        //    String url = "https://item.taobao.com/item.htm?spm=a230r.1.14.284.2622180fbT6du1&id=584968056618&ns=1&abbucket=3#detail";
        //    Uri uri = new Uri(url);
        //    String host = uri.Host;
        //    string html = HttpGet(url);
        //    HtmlDocument doc = new HtmlDocument();
        //    doc.LoadHtml(html);
        //    //获取文章列表
        //    HtmlNodeCollection artlist = null;
        //    HtmlNodeCollection artlist2 = null;
        //    if (uri.Host.Equals("item.taobao.com")) {
        //        artlist = doc.DocumentNode.SelectNodes("//div[@class='tb-shop-name']");
        //        artlist2 = doc.DocumentNode.SelectNodes("//h3[@class='tb-main-title']");
        //    }
        //    if (uri.Host.Equals("detail.tmall.com"))
        //    {
        //        artlist = doc.DocumentNode.SelectNodes("//a[@class='slogo-shopname']");
        //        artlist2 = doc.DocumentNode.SelectNodes("//h1[@data-spm='1000983']");
        //    }

           
        //    if (artlist != null)
        //    {
        //        foreach (var item in artlist)

        //        {
        //            Console.WriteLine("店铺名称：" + item.InnerText.Trim());
        //        }

        //    }
        //    if (artlist2 != null)
        //    {
        //        foreach (var item in artlist2)

        //        {
        //            Console.WriteLine("商品名称：" + item.InnerText.Trim());
        //        }

        //    }

        //     Console.Read();

        //}

  

    }
}
