using System;
using Microsoft.SPOT;
using MicroWebServer.Routing;

namespace MicroWebServer.Tests.UnitTests
{
    public class RouteCollectionTests
    {
        public void ResolveFor_ShouldReturnNull_WhenNoRoutesExist()
        {
            var collection = new RouteCollection();
            Assert.IsNull(collection.ResolveFor(null));
        }

        private class FalseRoute : RouteBase
        {
            public override RouteData GetRouteData(Abstractions.IHttpContext context)
            {
                return null;
            }
        }

        public void ResolveFor_ShouldReturnNull_WhenNoRouteMatches()
        {
            var collection = new RouteCollection();
            collection.Add(new FalseRoute());
            Assert.IsNull(collection.ResolveFor(null));
        }

        private class TrueRoute : RouteBase
        {
            public string Name { get; set; }

            public override RouteData GetRouteData(Abstractions.IHttpContext context)
            {
                return new RouteData() { Data = Name };
            }
        }

        public void ResolveFor_ShouldReturnRoute_WhenRouteMatches()
        {
            var collection = new RouteCollection();
            collection.Add(new TrueRoute());
            Assert.IsNotNull(collection.ResolveFor(null));
        }

        public void ResolveFor_ShouldReturnFirstRoute_WhenMultipleRouteMatches()
        {
            var collection = new RouteCollection();
            var first = new TrueRoute { Name = "first route" };
            var second = new TrueRoute { Name = "second route" };
            collection.Add(first);
            collection.Add(second);
            var actual = (string)collection.ResolveFor(null).Data;
            Assert.AreEqual(first.Name, actual);
        }
    }
}
