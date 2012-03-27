using System;
using Microsoft.SPOT;

namespace MicroWebServer
{
    public class Resource
    {
        public Resource()
        {
            LastModified = DateTime.UtcNow;
        }

        public Enum StringResource { get; set; }
        public string ContentType { get; set; }
        public DateTime LastModified { get; private set; }
    }
}
