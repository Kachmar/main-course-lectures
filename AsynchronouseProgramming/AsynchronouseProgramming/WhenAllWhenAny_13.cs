using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsynchronouseProgramming
{

    /// <summary>
    /// This sample shows how to scale out the processes. So there might be case, that there are different algorithm for particular task, and we do not know which one is optimal in this case. So we start all the algorithms and when any on those algorithms finishes first - we use its result.
    /// Second thing is how to run parallel tasks to scale the processing.
    /// </summary>
    public class WhenAllWhenAny
    {
        static void Main()
        {
            Demo().GetAwaiter().GetResult();
        }
        static async Task Demo()
        {
            //var stopWatch = new Stopwatch();
            //stopWatch.Start();
            //Task<int> winningTask = await Task.WhenAny(Alg1(), Alg2(), Alg3());
            //Console.WriteLine($"Done in {stopWatch.ElapsedMilliseconds}");
            //Console.WriteLine(winningTask.Result);

            //var stopWatch = new Stopwatch();
            //stopWatch.Start();
            //int[] res = await Task.WhenAll(Alg1(), Alg2(), Alg3());
            //Console.WriteLine($"Done in {stopWatch.ElapsedMilliseconds}");
            //var x = 5;

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 20; i++)
            {
                await GetContentAsync();
            }
            Console.WriteLine($"Done in sequence {stopWatch.ElapsedMilliseconds}");

            stopWatch = new Stopwatch();
            stopWatch.Start();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 20; i++)
            {
                tasks.Add(GetContentAsync());
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"Done in parallel {stopWatch.ElapsedMilliseconds}");
        }

        static async Task<string> GetContentAsync()
        {
            HttpClient httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("http://google.com");
            return content;
        }

        static async Task<int> Alg1()
        {
            await Task.Delay(3000);
            return 1;
        }
        static async Task<int> Alg2()
        {
            await Task.Delay(2000);
            return 2;
        }
        static async Task<int> Alg3()
        {
            await Task.Delay(1000);
            return 3;
        }
    }
}
