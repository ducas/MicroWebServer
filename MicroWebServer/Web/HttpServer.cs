using System;
using Microsoft.SPOT;
using System.Net;
using System.IO;

namespace MicroWebServer.Web
{
    public class HttpServer
    {
        HttpListener listener;
        RequestHandler handler;
        int nextId = 1;

        public HttpServer(string prefix, int port, RequestHandler handler)
        {
            this.handler = handler;

            listener = new HttpListener("http", 9997);
            listener.Start();
            
            var pool = new ThreadPool(10, ProcessRequest);
        }

        public void ProcessRequest()
        {
            var id = nextId++;
            while (true)
            {
                try
                {
                    var context = listener.GetContext();
                    var stopwatch = Stopwatch.StartNew();
                    handler.Invoke(context);
                    stopwatch.Stop();
                    Debug.Print("Request handled on thread " + id.ToString() + " in " + stopwatch.Ellapsed.Ticks.ToString() + " ticks");
                    
                }
                catch (Exception ex)
                {
                    Debug.Print("An error occurred:\n" + ex.ToString());
                }
            }
        }
    }

    public delegate void RequestHandler(HttpListenerContext context);

    public class HttpRequestEventArgs
    {
        public HttpRequestEventArgs(HttpListenerContext context)
        {
            Context = context;
        }

        public HttpListenerContext Context { get; private set; }
    }
}
