using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebServerHttp
{
    public class HttpRequestListener
    {
        HttpListener httpListener = new HttpListener();

        public HttpRequestListener()
        {
            Console.WriteLine("Listening...");
        }
        public HttpListener GetHttpListener()
        {
            return httpListener;
        }
        public void Listen()
        {
            httpListener.Prefixes.Add("http://localhost:8080/");
            httpListener.Start();
        }
        public void StopListening()
        {
            httpListener.Stop();
        }
    }
}
