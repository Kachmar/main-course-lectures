using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsynchronouseProgramming
{
    public class AsyncAwait
    {
        static void Main()
        {
            var content = GetContentAsync().Result;
            Console.WriteLine(content);
        }

        private static async Task<string> GetContentAsync()
        {
            HttpClient httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("http://google.com");
            return content;
        }

    }
}
