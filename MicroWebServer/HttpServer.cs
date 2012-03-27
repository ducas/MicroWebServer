using System;
using Microsoft.SPOT;
using System.Net;
using System.IO;
using System.Threading;
using MicroWebServer.Abstractions;
using MicroWebServer.Results;
using MicroWebServer.Routing;

namespace MicroWebServer
{
    public class HttpServer
    {
        const int defaultPoolSize = 3;
        static readonly object threadLock = new object();

        public RouteCollection Routes { get; private set; }

        HttpListener listener;
        IRequestHandler[] handlers;
        int nextThreadId = 1;

        public HttpServer(string prefix, int port, IRequestHandler[] handlers)
        {
            this.handlers = handlers;
            this.Routes = new RouteCollection();

            listener = new HttpListener(prefix, port);
            listener.Start();

            var pool = new ThreadPool(defaultPoolSize, ProcessRequest);
        }

        public void ProcessRequest()
        {
            int threadId;
            lock (threadLock) threadId = nextThreadId++;

            string url = string.Empty;
            HttpListenerContext context = null;
            HttpContextAdapter contextAdapter = null;
            Stopwatch stopwatch = null;

            while (true)
            {
                try
                {
                    context = listener.GetContext();
                    contextAdapter = new HttpContextAdapter(context);

                    url = context.Request.RawUrl;
                    Debug.Print("Request for \"" + url + "\" received on thread " + threadId.ToString());
                    stopwatch = Stopwatch.StartNew();

                    HandleRequest(contextAdapter);

                    stopwatch.Stop();
                    Debug.Print("Request for \"" + url + "\" handled on thread " + threadId.ToString() + " in " + stopwatch.Ellapsed.Ticks.ToString() + " ticks");

                    url = string.Empty;
                }
                catch (Exception ex)
                {
                    Debug.Print("An error occurred handling request for \"" + url + "\" on thread " + threadId.ToString() + ":\n" + ex.ToString());
                }
                finally
                {
                    contextAdapter = null;
                    if (context != null)
                    {
                        context.Close();
                        context = null;
                    }
                    url = string.Empty;
                    if (stopwatch != null) stopwatch = null;
                }
            }
        }

        private void HandleRequest(HttpContextAdapter contextAdapter)
        {
            IActionResult result = null;
            foreach (var handler in handlers)
            {
                if ((result = handler.TryHandle(contextAdapter)) == null) break;
            }

            if (result == null) result = new HttpNotFoundResult();

            result.ExecutResult(contextAdapter);
        }
    }
}
