using System;
using Microsoft.SPOT;
using System.Net;
using System.IO;
using System.Threading;

namespace MicroWebServer.Web
{
    public class HttpServer
    {
        const int defaultPoolSize = 3;
        static readonly object threadLock = new object();

        HttpListener listener;
        IRequestHandler[] handlers;
        int nextThreadId = 1;

        public HttpServer(string prefix, int port, IRequestHandler[] handlers)
        {
            this.handlers = handlers;

            listener = new HttpListener(prefix, port);
            listener.Start();

            var pool = new ThreadPool(defaultPoolSize, ProcessRequest);
        }

        public void ProcessRequest()
        {
            int threadId;
            lock(threadLock) threadId = nextThreadId++;

            while (true)
            {
                try
                {
                    var context = listener.GetContext();
                    var stopwatch = Stopwatch.StartNew();

                    foreach (var handler in handlers)
                    {
                        if (handler.TryHandle(context)) return;
                    }

                    context.Response.StatusCode = 404;
                    context.Response.StatusDescription = "Not Found";

                    stopwatch.Stop();
                    Debug.Print("Request handled on thread " + threadId.ToString() + " in " + stopwatch.Ellapsed.Ticks.ToString() + " ticks");
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
