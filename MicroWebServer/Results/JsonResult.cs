using System;
using Microsoft.SPOT;
using MicroWebServer.Abstractions;
using System.IO;

namespace MicroWebServer.Results
{
    public class JsonResult : IActionResult
    {
        static readonly JsonSerializer jsonSerializer = new JsonSerializer();

        public string ContentType { get; set; }
        public object Data { get; set; }

        public JsonResult()
        {
            ContentType = "application/json";
        }

        public void ExecutResult(IHttpContext context)
        {
            var response = context.Response;

            response.StatusCode = 200;
            response.StatusDescription = "OK";
            response.ContentType = ContentType;

            using (var stream = response.OutputStream)
            using (var writer = new StreamWriter(stream))
            {
                jsonSerializer.Serialize(Data, writer);
            }
        }
    }
}
