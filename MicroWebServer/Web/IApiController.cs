using System;
using System.Collections;

namespace MicroWebServer.Web
{
    public interface IApiController
    {
        IEnumerable Get();
        void Put(object item);
        void Post(object item);
        void Delete(object item);
    }
}
