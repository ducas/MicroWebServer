using System;
using System.Threading;

namespace MicroWebServer
{
    public class ThreadPool
    {
        readonly Thread[] threads;

        public ThreadPool(int count, ThreadStart threadStart)
        {
            threads = new Thread[count];
            for (int i = 0; i < count; i++)
            {
                threads[i] = new Thread(threadStart);
                threads[i].Start();
            }
        }
    }
}
