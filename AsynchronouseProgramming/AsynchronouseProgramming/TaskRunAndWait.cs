using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronouseProgramming
{
    public class TaskRunAndWait
    {

        static void Main()
        {
            Task task = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Foo");
            });
            Console.WriteLine(task.IsCompleted);  // False
            task.GetAwaiter().GetResult();  // Blocks until task is complete
        }
    }
}
