using System;

namespace MicroWebServer.Tests.Models
{
    public class TestResult
    {
        public bool Pass { get; set; }
        public string TestClass { get; set; }
        public string TestMethod { get; set; }
        public string Message { get; set; }
    }
}
