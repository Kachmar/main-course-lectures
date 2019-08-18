using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronouseProgramming
{
   public class TaskCancellation
    {
        static void Main()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task cancellableTask = Task.Run(Go, cancellationTokenSource.Token);
            Thread.Sleep(1000);
            //  cancellationTokenSource.Cancel();
            //Without the Console Read, the app would terminate,
            //cause the task is running the thread on background thread, that is retrieved from a threadpool
            Console.Read();
        }

        static void Go()
        {
            Console.WriteLine("Go started");
            Thread.Sleep(2000);

            Console.WriteLine("Go finished");
        }
    }
}
