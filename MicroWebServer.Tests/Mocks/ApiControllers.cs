using System;
using Microsoft.SPOT;
using System.Collections;

namespace MicroWebServer.Tests.Mocks
{
    public class BadController : IApiController
    {
        public BadController(int id)
        {

        }

        public IEnumerable Get() { return null; }

        public void Put(object item) { }

        public void Post(object item) { }

        public void Delete(object item) { }
    }

    public class ValidController : IApiController
    {
        public IEnumerable Get() { return null; }

        public void Put(object item) { }

        public void Post(object item) { }

        public void Delete(object item) { }
    }
}
