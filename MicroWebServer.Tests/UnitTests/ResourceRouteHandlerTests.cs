using System;
using Microsoft.SPOT;
using MicroWebServer.Routing.Handlers;
using MicroWebServer.Routing;
using MicroWebServer.Results;

namespace MicroWebServer.Tests.UnitTests
{
    public class ResourceRouteHandlerTests
    {
        public void Handle_ShouldNotReturnResult_WhenDataIsNull()
        {
            var handler = new ResourceRouteHandler(Resources.ResourceManager);
            var result = handler.Handle(null, new RouteData { Data = null });
            Assert.IsNull(result);
        }

        public void Handle_ShouldNotReturnResult_WhenDataIsNotResource()
        {
            var handler = new ResourceRouteHandler(Resources.ResourceManager);
            var result = handler.Handle(null, new RouteData { Data = new object() });
            Assert.IsNull(result);
        }

        public void Handle_ShouldNotReturnResult_WhenDataIsInvalidResource()
        {
            var handler = new ResourceRouteHandler(Resources.ResourceManager);
            var result = handler.Handle(null, new RouteData { Data = new Resource { StringResource = Resources.FontResources.miramob } });
            Assert.IsNull(result);
        }

        public void Handle_ShouldReturnResult_WhenDataIsResource()
        {
            var handler = new ResourceRouteHandler(Resources.ResourceManager);
            var result = handler.Handle(null, new RouteData { Data = new Resource { StringResource = Resources.StringResources.test } });
            Assert.IsNotNull(result);
        }

        public void Handle_ShouldReturnContentResult_WhenDataIsResource()
        {
            var handler = new ResourceRouteHandler(Resources.ResourceManager);
            var result = handler.Handle(null, new RouteData { Data = new Resource { StringResource = Resources.StringResources.test } });
            Assert.IsNotNull(result as ContentResult);
        }

        public void Handle_ShouldReturnContentResultWithLastModified_WhenDataIsResource()
        {
            var handler = new ResourceRouteHandler(Resources.ResourceManager);
            var resource = new Resource { StringResource = Resources.StringResources.test };
            var result = handler.Handle(null, new RouteData { Data = resource });
            Assert.AreEqual(resource.LastModified, (result as ContentResult).LastModified);
        }

        public void Handle_ShouldReturnContentResultWithContentType_WhenResourceSpecifiesType()
        {
            var handler = new ResourceRouteHandler(Resources.ResourceManager);
            var resource = new Resource { StringResource = Resources.StringResources.test, ContentType = "text/html" };
            var result = handler.Handle(null, new RouteData { Data = resource });
            Assert.AreEqual(resource.ContentType, (result as ContentResult).ContentType);
        }

        public void Handle_ShouldReturnContentResultWithContent_WhenValidResourceSpecified()
        {
            var handler = new ResourceRouteHandler(Resources.ResourceManager);
            var resource = new Resource { StringResource = Resources.StringResources.test };
            var result = handler.Handle(null, new RouteData { Data = resource });
            Assert.AreEqual(Resources.GetString(Resources.StringResources.test), (result as ContentResult).Content);
        }
    }
}
