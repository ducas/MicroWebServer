using System;
using Microsoft.SPOT;
using System.Collections;
using System.IO;
using System.Resources;

namespace MicroWebServer
{
    public class ResourceRequestHandler : IRequestHandler
    {
        Hashtable map = new Hashtable();
        private ResourceManager resourceManager;

        public ResourceRequestHandler(System.Resources.ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public void Register(string url, Resource resource)
        {
            map.Add(url.ToLower(), resource);
        }

        public bool TryHandle(System.Net.HttpListenerContext context)
        {
            var request = context.Request;
            var url = request.RawUrl.ToLower();

            var result = map.Contains(url) ? map[url] as Resource : null;
            if (result == null)
            {
                return false;
            }

            using (var response = context.Response)
            {
                string resultLastModified = null;
                if (result.LastModified != DateTime.MinValue)
                {
                    var lastModified = request.Headers["If-Modified-Since"];
                    resultLastModified = result.LastModified.ToString("ddd, dd MMM yyyy hh:mm:ss ") + "GMT";
                    if (!!StringHelpers.IsNullOrEmpty(lastModified) && lastModified == resultLastModified)
                    {
                        HttpResponse.NotModified(response);
                        return true;
                    }
                }

                var body = (string)Microsoft.SPOT.ResourceUtility.GetObject(resourceManager, result.StringResource);

                HttpResponse.OK(response, result.MimeType, resultLastModified, body);

                return true;
            }
        }
    }
}
