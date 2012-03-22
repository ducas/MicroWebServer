using System;

namespace MicroWebServer
{
    public class Stopwatch
    {
        public static Stopwatch StartNew()
        {
            var watch = new Stopwatch();
            watch.Start();
            return watch;
        }

        public TimeSpan Ellapsed { get; set; }

        DateTime start;
        public void Start()
        {
            start = DateTime.Now;
        }

        public void Stop()
        {
            Ellapsed = DateTime.Now - this.start;
        }
    }
}
