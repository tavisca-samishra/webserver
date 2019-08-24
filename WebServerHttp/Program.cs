using System;
using System.Net;

namespace WebServerHttp
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpRequestListener httpRequestListener = new HttpRequestListener();
            while (true)
            {
                httpRequestListener.Listen();
                Dispatcher dispatcher = new Dispatcher(httpRequestListener,new WebApp());
                dispatcher.Respond();
                httpRequestListener.StopListening();
            }
        }
    }
}
