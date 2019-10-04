using System;
using System.Threading;

namespace AsynchronouseProgramming
{/// <summary>
/// This is an example how concurrent access to same variables from different threads behaves. 
/// And the below class shows, how to use locks, to avoid race conditions
/// </summary>
    //public class StaticsInMultiThreading
    //{
    //    static bool _done;
    //    static void Main()
    //    {
    //        // Static fields are shared between all threads
    //        // in the same application domain.
    //        new Thread(Go).Start();
    //        //try commenting this out
    //        for (int i = 0; i < 1000; i++)
    //        {
    //            Console.Write(i);
    //        }

    //        Go();
    //    }
    //    static void Go()
    //    {
    //        if (!_done)
    //        {
    //            Console.WriteLine($"{Thread.GetCurrentProcessorId()} Done");
    //            _done = true;
    //        }
    //    }
    //}

    public class StaticsInMultiThreadingWithLock
    {
        static bool _done;
        static readonly object _locker = new object();

        static void Main()
        {
            // Static fields are shared between all threads
            // in the same application domain.
            var secondaryThread = new Thread(Go);
            Console.WriteLine($"secondary thread:{secondaryThread.ManagedThreadId}");
            Console.WriteLine($"main thread:{Thread.CurrentThread.ManagedThreadId}");

            secondaryThread
              .Start();

            Go();
        }
        static void Go()
        {
            lock (_locker)
            {
                if (!_done)
                {
                    Console.WriteLine($"GO executed by thread:{Thread.CurrentThread.ManagedThreadId}");

                    _done = true;
                }
            }
        }
    }
}
