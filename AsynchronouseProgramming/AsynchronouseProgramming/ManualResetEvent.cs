using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    public class ManualResetEventClass
    {
        static void Main()
        {
            var signal = new ManualResetEvent(false);
            new Thread(() =>
        {
            Console.WriteLine("Waiting for signal...");
            signal.WaitOne();
            signal.Dispose();
            Console.WriteLine("Got signal!");
        }).Start();
            Thread.Sleep(2000);
            signal.Set(); // "Open" the signal
        }
    }
}
