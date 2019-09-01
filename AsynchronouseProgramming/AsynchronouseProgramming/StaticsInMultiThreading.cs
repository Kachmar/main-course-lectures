using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    public class StaticsInMultiThreading
    {
        static bool _done;
        static object _locker = new object();
        static void Main()
        {
            // Static fields are shared between all threads
            // in the same application domain.
            new Thread(Go).Start();
            //try commenting this out
            //for (int i = 0; i < 1000; i++)
            //{
            //    Console.Write(i);
            //}
            //
            Go();
        }
        static void Go()
        {
            lock (_locker)
            {
                if (!_done)
                {
                    Console.WriteLine($"{Thread.GetCurrentProcessorId()} Done");

                    _done = true;
                }
            }
        }
    }

    //public class StaticsInMultiThreadingWithLock
    //{
    //    static bool _done;
    //    static readonly object _locker = new object();

    //    static void Main()
    //    {
    //        // Static fields are shared between all threads
    //        // in the same application domain.
    //        new Thread(Go).Start();

    //        Go();
    //    }
    //    static void Go()
    //    {
    //        lock (_locker)
    //        {
    //            if (!_done) { Console.WriteLine("Done"); _done = true; }
    //        }
    //    }
    //}
}
