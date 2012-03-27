using System;
using Microsoft.SPOT;
using MicroWebServer.Mvc;
using MicroWebServer.Tests.Mocks;
using MicroWebServer.Routing;
using MicroWebServer.Results;

namespace MicroWebServer.Tests.UnitTests
{
    public class MvcRequestHandlerTests
    {
        public void TryHandle_ShouldNotReturnResult_WhenRoutesEmtpy()
        {
            var handler = new MvcRequestHandler();
            var result = handler.TryHandle(null);
            Assert.IsNull(result);
        }

        public void TryHandle_ShouldNotReturnResult_WhenRouteReturnsNull()
        {
            var handler = new MvcRequestHandler();
            handler.Routes.Add(new MockRoute(null) { GetRouteDataResult = null });
            var result = handler.TryHandle(null);
            Assert.IsNull(result);
        }

        public void TryHandle_ShouldNotReturnResult_WhenRouteHandlerReturnsNull()
        {
            var handler = new MvcRequestHandler();
            handler.Routes.Add(new MockRoute(new MockRouteHandler { HandleResult = null }) { GetRouteDataResult = new RouteData() });
            var result = handler.TryHandle(null);
            Assert.IsNull(result);
        }

        public void TryHandle_ShouldReturnResult_WhenRouteHandlerReturnsResult()
        {
            var handler = new MvcRequestHandler();
            var expected = new ContentResult();
            handler.Routes.Add(new MockRoute(new MockRouteHandler { HandleResult = expected }) { GetRouteDataResult = new RouteData() });
            var result = handler.TryHandle(null);
            Assert.AreEqual(expected, result);
        }
    }
}
