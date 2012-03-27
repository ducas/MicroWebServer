using System;
using Microsoft.SPOT;
using System.Resources;

namespace MicroWebServer
{
    public class Resource
    {
        public Resource(ResourceManager resourceManager, Enum stringResource)
        {
            ResourceManager = resourceManager;
            StringResource = stringResource;

            LastModified = DateTime.UtcNow;
        }

        public ResourceManager ResourceManager { get; private set; }
        public Enum StringResource { get; private set; }
        public DateTime LastModified { get; private set; }

        public string ContentType { get; set; }
    }
}
