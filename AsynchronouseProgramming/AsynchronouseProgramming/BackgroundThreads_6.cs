using System;
using System.Threading;

namespace AsynchronouseProgramming
{
    /// <summary>
    /// This sample shows that foreground threads keep the app running, however background thread lets the app terminate
    /// </summary>
    public class BackgroundThreads
    {
        static void Main(string[] args)
        {
            Thread worker = new Thread(() =>
            {
                var x = Console.ReadLine();
                Console.WriteLine(x);
                Console.ReadLine();
          });
            //change true/false
            worker.IsBackground = true;
            worker.Start();
        }
    }
}
