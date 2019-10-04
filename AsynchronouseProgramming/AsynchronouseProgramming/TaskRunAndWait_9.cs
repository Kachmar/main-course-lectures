using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronouseProgramming
{

    /// <summary>
    /// Task starts a thread using threadpool
    /// </summary>
    public class TaskRunAndWait
    {
        static void Main()
        {
            Task task = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Foo");
                throw new Exception("Shit happens");
            });

            try
            {
                task.GetAwaiter().GetResult(); // Blocks until task is complete
            }
            catch (Exception ex)
            {
Console.WriteLine(ex);
            }
        }
    }
}
