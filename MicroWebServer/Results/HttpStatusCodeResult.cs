using System.IO;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Results
{
    public class HttpStatusCodeResult : IActionResult
    {
        public int StatusCode { get; private set; }
        public string StatusDescription { get; private set; }

        protected string Body { get; set; }

        public HttpStatusCodeResult(int statusCode) : this(statusCode, null) { }

        public HttpStatusCodeResult(int statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        public void ExecutResult(IHttpContext context)
        {
            var response = context.Response;

            response.StatusCode = StatusCode;
            response.StatusDescription = StatusDescription;

            using (var s = response.OutputStream)
            using (var w = new StreamWriter(s))
                w.Write(Body ?? "");
        }
    }
}
