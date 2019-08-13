using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsynchronouseProgramming
{
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
            //Task<int> winningTask = Task.WhenAny(Delay1(), Delay2(), Delay3()).Result;
            //Console.WriteLine($"Done in {stopWatch.ElapsedMilliseconds}");
            //Console.WriteLine(winningTask.Result);

            //var stopWatch = new Stopwatch();
            //stopWatch.Start();
            //var res = Task.WhenAll(Delay1(), Delay2(), Delay3()).Result;
            //Console.WriteLine($"Done in {stopWatch.ElapsedMilliseconds}");


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

        static async Task<int> Delay1()
        {
            await Task.Delay(3000); return 1;
        }
        static async Task<int> Delay2()
        {
            await Task.Delay(2000); return 2;
        }
        static async Task<int> Delay3()
        {
            await Task.Delay(1000); return 3;
        }
    }
}
