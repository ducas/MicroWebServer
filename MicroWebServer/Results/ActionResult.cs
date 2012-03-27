using System;
using Microsoft.SPOT;
using MicroWebServer.Abstractions;

namespace MicroWebServer.Results
{
    public interface IActionResult
    {
        void ExecutResult(IHttpContext context);
    }
}
