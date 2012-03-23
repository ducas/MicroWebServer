using System;
using Microsoft.SPOT;
using System.Threading;

namespace MicroWebServer.Tests
{
    public class Program
    {
        public static void Main()
        {
            TestRunner.Run();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
