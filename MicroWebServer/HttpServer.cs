using System;
using Microsoft.SPOT;
using System.Net;
using System.IO;
using System.Threading;
using MicroWebServer.Abstractions;

namespace MicroWebServer
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
            lock (threadLock) threadId = nextThreadId++;

            string url = string.Empty;
            HttpListenerContext context = null;
            HttpContextAdapter contextAdapter = null;
            bool handled = false;
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

                    handled = false;
                    foreach (var handler in handlers)
                    {
                        if (handled = handler.TryHandle(contextAdapter)) break;
                    }

                    if (!handled) HttpResponse.NotFound(contextAdapter.Response, Resources.GetString(Resources.StringResources.NotFoundBody));

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
    }
}
