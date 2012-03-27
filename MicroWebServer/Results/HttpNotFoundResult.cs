using System;
using Microsoft.SPOT;

namespace MicroWebServer.Results
{
    public class HttpNotFoundResult : HttpStatusCodeResult
    {
        public HttpNotFoundResult() : this("Not Found") { }

        public HttpNotFoundResult(string statusDescription)
            : base(404, statusDescription)
        {
            Body = Resources.GetString(Resources.StringResources.NotFoundBody);
        }
    }
}
