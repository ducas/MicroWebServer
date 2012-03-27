using System;
using Microsoft.SPOT;

namespace MicroWebServer.Results
{
    public class HttpNotFoundResult : HttpStatusCodeResult
    {
        public HttpNotFoundResult() : base(404, "Not Found") { }

        public HttpNotFoundResult(string statusDescription) : base(404, statusDescription) { }
    }
}
