using System;
using Microsoft.SPOT;
using System.IO;
using System.Net;

namespace MicroWebServer.Abstractions
{
    public interface IHttpResponse : IDisposable
    {
        int StatusCode { get; set; }
        string StatusDescription { get; set; }
        string ContentType { get; set; }
        WebHeaderCollection Headers { get; }
        Stream OutputStream { get; }
        void Close();
    }

    public class HttpResponseAdapter : IHttpResponse
    {
        private HttpListenerResponse response;

        public HttpResponseAdapter(HttpListenerResponse response)
        {
            this.response = response;
        }

        public int StatusCode
        {
            get { return response.StatusCode; }
            set { response.StatusCode = value; }
        }

        public string StatusDescription
        {
            get { return response.StatusDescription; }
            set { response.StatusDescription = value; }
        }

        public string ContentType
        {
            get { return response.ContentType; }
            set { response.ContentType = value; }
        }

        public WebHeaderCollection Headers
        {
            get { return response.Headers; }
        }

        public Stream OutputStream
        {
            get { return response.OutputStream; }
        }

        public void Close()
        {
            response.Close();
        }

        public void Dispose()
        {
            response.Close();
        }
    }
}
