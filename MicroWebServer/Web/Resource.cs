using System;
using Microsoft.SPOT;

namespace MicroWebServer.Web
{
    public class Resource
    {
        public Resource()
        {
            LastModified = DateTime.UtcNow;
        }

        public Enum StringResource { get; set; }
        public string MimeType { get; set; }
        public DateTime LastModified { get; private set; }
    }
}
