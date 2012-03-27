using System;
using System.IO;
using System.Text;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Results
{
    public class ContentResult : IActionResult
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
        public DateTime LastModified { get; set; }
        public Encoding ContentEncoding { get; set; }

        public ContentResult()
        {
            ContentType = "text/plain";
            ContentEncoding = Encoding.UTF8;
        }

        public void ExecutResult(IHttpContext context)
        {
            var response = context.Response;

            response.StatusCode = 200;
            response.StatusDescription = "OK";

            if (!StringHelpers.IsNullOrEmpty(ContentType)) response.ContentType = ContentType;
            if (LastModified != DateTime.MinValue) response.Headers.Add("Last-Modified", LastModified.ToLastModifiedString());

            if (Content == null) Content = string.Empty;

            using (var s = response.OutputStream)
            using (var w = new StreamWriter(s))
                w.Write(Content);
        }
    }
}
