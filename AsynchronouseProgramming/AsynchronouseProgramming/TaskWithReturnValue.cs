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

            //try
            //{
            //    task.Wait();
            //}
            //catch (Exception ex)
            //{

            //}
            try
            {
                Console.WriteLine(task.Result);
            }
            catch (Exception ex)
            {

            }
        }

        static Task<string> Calculate()
        {
            return Task.Run<string>(() =>
            {
                return Enumerable.Range(1, 444).Select
(p => p.ToString()).Aggregate((aggregate, next) => { return aggregate + "," + next; });
            });
        }
    }
}
