using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    /// <summary>
    /// This is a demo, how the child thread is waiting for a signal from main thread
    /// </summary>
    public class ManualResetEventClass
    {
        static ManualResetEvent signal = new ManualResetEvent(false);
        static void Main()
        {
            var t = new Thread(Go);

            t.Start();
            Thread.Sleep(2000);
            signal.Set(); // "Open" the signal
        }

        static void Go()
        {
            Console.WriteLine("Waiting for signal...");
            signal.WaitOne();
            signal.Dispose();
            Console.WriteLine("Got signal!");
        }
    }
}
