using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;

namespace WebServerHttp
{
    public class Dispatcher
    {
        HttpListenerContext context;
        public HttpListenerResponse HttpResponse { get; }
        WebApp _webApp;
        string _file;

        public Dispatcher(HttpRequestListener requestListener, WebApp webApp)
        {
            context = requestListener.GetHttpListener().GetContext();
            HttpResponse = context.Response;
            _webApp = webApp;
        }
        public void Respond()
        {
            if (context.Request.Url.AbsolutePath == "/year" && context.Request.HttpMethod == "POST")
            {
                var data_text = new StreamReader(context.Request.InputStream,
                                                 context.Request.ContentEncoding)
                                                 .ReadToEnd();
                var json = System.Web.HttpUtility.UrlDecode(data_text);
                JObject requestBody = JObject.Parse(json);
                string year = (string)requestBody.SelectToken("year");

                int yearValue = Int32.Parse(year);
                if (yearValue % 400 == 0)
                    year = "yes";
                else if (yearValue % 100 == 0)
                    year = "no";
                else if (yearValue % 4 == 0)
                    year = "yes";
                else
                    year = "no";
                JObject responseJSON = JObject.Parse(@"{'IsLeap': '" + year + @"'}");
                byte[] buf = Encoding.UTF8.GetBytes(responseJSON.ToString());
                context.Response.ContentLength64 = buf.Length;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.OutputStream.Write(buf, 0, buf.Length);

            }
            else
            {
                string domainName = context.Request.Url.AbsolutePath.Split("/")[1];
                string repository = _webApp.GetRepository(domainName);
                string file = @"C:\Users\smishra\source\repos\WebServerHttp\FileSystem\" + repository + @"\" + context.Request.Url.AbsolutePath.Split("/")[2];
                if (File.Exists(file))
                    _file = file;
                else
                    _file = @"C:\Users\smishra\source\repos\WebServerHttp\FileSystem\404.html";
                var responseString = File.ReadAllText(_file, Encoding.UTF8);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                HttpResponse.ContentLength64 = buffer.Length;
                Stream output = HttpResponse.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}
