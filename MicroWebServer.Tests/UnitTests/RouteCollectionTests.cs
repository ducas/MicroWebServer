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
            public override bool CanHandle(Abstractions.IHttpContext context)
            {
                return false;
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

            public override bool CanHandle(Abstractions.IHttpContext context)
            {
                return true;
            }

            public override string ToString()
            {
                return Name ?? base.ToString();
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
            Assert.AreEqual(first, collection.ResolveFor(null));
        }
    }
}
