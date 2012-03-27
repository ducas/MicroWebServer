using System;
using Microsoft.SPOT;
using MicroWebServer.Routing.Handlers;
using MicroWebServer.Routing;
using MicroWebServer.Tests.Mocks;
using MicroWebServer.Results;

namespace MicroWebServer.Tests.UnitTests
{
    public class ApiRouteHandlerTests
    {
        public void Handle_ShouldNotReturnResult_WhenControllerIsNull()
        {
            var handler = new ApiRouteHandler();
            var result = handler.Handle(null, new RouteData() { Controller = null });
            Assert.IsNull(result);
        }

        public void Handle_ShouldNotReturnResult_WhenControllerIsNotApiController()
        {
            var handler = new ApiRouteHandler();
            var result = handler.Handle(null, new RouteData() { Controller = new object() });
            Assert.IsNull(result);
        }

        public void Handle_ShouldReturnResult_WhenControllerIsApiController()
        {
            var handler = new ApiRouteHandler();
            var result = handler.Handle(new MockHttpContext { Request = new MockHttpRequest { HttpMethod = "GET" } }, new RouteData { Controller = new ValidController() });
            Assert.IsNotNull(result);
        }

        public void Handle_ShouldReturnJsonResult_WhenControllerIsApiController()
        {
            var handler = new ApiRouteHandler();
            var result = handler.Handle(new MockHttpContext { Request = new MockHttpRequest { HttpMethod = "GET" } }, new RouteData { Controller = new ValidController() });
            Assert.IsNotNull(result as JsonResult);
        }

        public void Handle_ShouldNotReturnResult_WhenMethodIsNotSupported()
        {
            var handler = new ApiRouteHandler();
            var result = handler.Handle(new MockHttpContext { Request = new MockHttpRequest { HttpMethod = "BLAH" } }, new RouteData { Controller = new ValidController() });
            Assert.IsNull(result);
        }
    }
}
