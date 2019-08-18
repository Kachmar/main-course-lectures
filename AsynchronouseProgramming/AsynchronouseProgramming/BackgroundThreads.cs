using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    public class BackgroundThreads
    {
        static void Main(string[] args)
        {
            Thread worker = new Thread(() => Console.ReadLine());
            //change true/false
            worker.IsBackground = true;
            worker.Start();
        }
    }
}
