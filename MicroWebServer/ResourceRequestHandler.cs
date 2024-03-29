using System;
using Microsoft.SPOT;
using System.Collections;
using System.IO;
using System.Resources;
using MicroWebServer.Abstractions;
using MicroWebServer.Results;

namespace MicroWebServer
{
    public class ResourceRequestHandler : IRequestHandler
    {
        Hashtable map = new Hashtable();

        public void Register(string url, Resource resource)
        {
            map.Add(url.ToLower(), resource);
        }

        public IActionResult TryHandle(IHttpContext context)
        {
            var request = context.Request;
            var url = request.RawUrl.ToLower();

            var result = map.Contains(url) ? map[url] as Resource : null;
            if (result == null) return null;

            using (var response = context.Response)
            {
                string resultLastModified = null;
                if (result.LastModified != DateTime.MinValue)
                {
                    var lastModified = request.Headers["If-Modified-Since"];
                    resultLastModified = result.LastModified.ToLastModifiedString();
                    if (!!StringHelpers.IsNullOrEmpty(lastModified) && lastModified == resultLastModified)
                    {
                        return new HttpStatusCodeResult(304, "Not Modified");
                    }
                }

                var body = (string)Microsoft.SPOT.ResourceUtility.GetObject(result.ResourceManager, result.StringResource);

                return new ContentResult { Content = body, ContentType = result.ContentType, LastModified = result.LastModified };
            }
        }
    }
}
