using System;
using Microsoft.SPOT;
using MicroWebServer.Routing;
using System.Reflection;
using MicroWebServer.Abstractions;
using MicroWebServer.Tests.Mocks;

namespace MicroWebServer.Tests.UnitTests
{
    public class ApiRouteTests
    {
        public void Ctor_ShouldSetPrefix()
        {
            var handler = new ApiRoute("/test/", null, Assembly.GetAssembly(typeof(ApiRouteTests)));
            Assert.AreEqual("/test/", handler.UrlPrefix);
        }

        public void Ctor_ShouldPrependSlashToPrefix()
        {
            var handler = new ApiRoute("test/", null, Assembly.GetAssembly(typeof(ApiRouteTests)));
            Assert.AreEqual("/test/", handler.UrlPrefix);
        }

        public void Ctor_ShouldAppendSlashToPrefix()
        {
            var handler = new ApiRoute("/test", null, Assembly.GetAssembly(typeof(ApiRouteTests)));
            Assert.AreEqual("/test/", handler.UrlPrefix);
        }

        public void Ctor_ShouldAppendAndPrependSlashToPrefix()
        {
            var handler = new ApiRoute("test", null, Assembly.GetAssembly(typeof(ApiRouteTests)));
            Assert.AreEqual("/test/", handler.UrlPrefix);
        }

        public void GetRouteData_ShouldNotReturnData_WhenNameIsOfInvalidController()
        {
            var handler = new ApiRoute("test", null, Assembly.GetAssembly(typeof(ApiRouteTests)));

            var context = new MockHttpContext
            {
                Request = new MockHttpRequest
                {
                    RawUrl = "/test/bad"
                }
            };

            Assert.IsNull(handler.GetRouteData(context));
        }

        public void GetRouteData_ShouldReturnData_WhenNameIsOfValidController()
        {
            var handler = new ApiRoute("test", null, Assembly.GetAssembly(typeof(ApiRouteTests)));

            var context = new MockHttpContext
            {
                Request = new MockHttpRequest
                {
                    RawUrl = "/test/valid"
                }
            };

            Assert.IsNotNull(handler.GetRouteData(context));
        }

        public void GetRouteData_ShouldReturnDataWithController_WhenNameIsOfValidController()
        {
            var handler = new ApiRoute("test", null, Assembly.GetAssembly(typeof(ApiRouteTests)));

            var context = new MockHttpContext
            {
                Request = new MockHttpRequest
                {
                    RawUrl = "/test/valid"
                }
            };

            var type = typeof(ValidController);

            Assert.AreEqual(type.FullName, handler.GetRouteData(context).Controller.GetType().FullName);
        }

        public void GetRouteData_ShouldReturnDataWithController_WithTrailingSlash()
        {
            var handler = new ApiRoute("test", null, Assembly.GetAssembly(typeof(ApiRouteTests)));

            var context = new MockHttpContext
            {
                Request = new MockHttpRequest
                {
                    RawUrl = "/test/valid/"
                }
            };

            var type = typeof(ValidController);

            Assert.AreEqual(type.FullName, handler.GetRouteData(context).Controller.GetType().FullName);
        }

        public void GetRouteData_ShouldReturnDataWithController_WhenIdSpecified()
        {
            var handler = new ApiRoute("test", null, Assembly.GetAssembly(typeof(ApiRouteTests)));

            var context = new MockHttpContext
            {
                Request = new MockHttpRequest
                {
                    RawUrl = "/test/valid/1"
                }
            };

            var type = typeof(ValidController);

            Assert.AreEqual(type.FullName, handler.GetRouteData(context).Controller.GetType().FullName);
        }
    }
}
