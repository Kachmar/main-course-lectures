using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsynchronouseProgramming
{
    public class TaskWithReturnValue
    {
        static void Main()
        {

            Task<string> task = Calculate();
            Console.WriteLine(task.Result);
        }

        static Task<string> Calculate()
        {
            return Task.Run<string>(() => { return GetStringresult(); });
        }

        private static string GetStringresult()
        {
            return Enumerable.Range(1, 2000)
                .Select(p => p.ToString())
                .Aggregate((aggregate, next) => { return aggregate + "," + next; });
        }
    }
}
