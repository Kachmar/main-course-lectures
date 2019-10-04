using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsynchronouseProgramming
{
    public class AsyncAwait
    {
        static async Task Main()
        {
            var content = await GetContentAsync();
            Console.WriteLine(content);
        }

        private static async Task<string> GetContentAsync()
        {
            HttpClient httpClient = new HttpClient();
            string content = await httpClient.GetStringAsync("http://google.com");
            return content;
        }

    }
}
