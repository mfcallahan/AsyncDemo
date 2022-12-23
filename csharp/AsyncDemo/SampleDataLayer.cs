using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public class SampleDataLayer
    {
        private const string DelayedResponseUrl = "https://mfcallahan-homepage-dev.azurewebsites.net/api/Utils/DelayedResponse";

        // Synchronous method to simulate a long running HTTP request
        public string GetDelayedApiResponse(int seconds)
        {
            Console.WriteLine("GetDelayedApiResponse() start.");

            using WebClient client = new WebClient();
            var message = client.DownloadString($"{DelayedResponseUrl}?seconds={seconds}");

            Console.WriteLine("GetDelayedApiResponse() complete.");

            return message.Trim('"');
        }

        // Asynchronous method to simulate a long running HTTP request
        public async Task<string> GetDelayedApiResponseAsync(int seconds)
        {
            Console.WriteLine("GetDelayedApiResponseAsync() start.");

            using WebClient client = new WebClient();
            var message = await client.DownloadStringTaskAsync($"{DelayedResponseUrl}?seconds={seconds}").ConfigureAwait(false);

            Console.WriteLine("GetDelayedApiResponseAsync() complete.");

            return message.Trim('"');
        }

        // Synchronous method to simulate long running work in application code
        public string SimulateLongProcess(int seconds)
        {
            Console.WriteLine("SimulateLongProcess() start.");

            Thread.Sleep(seconds * 1000);

            const string data = "Hello, world!";
            Console.WriteLine("SimulateLongProcess() complete.");

            return data;
        }

        // Asynchronous method to simulate long running work in application code
        public async Task<string> SimulateLongProcessAsync(int seconds)
        {
            Console.WriteLine("SimulateLongProcessAsync() start.");

            await Task.Delay(seconds * 1000).ConfigureAwait(false);

            const string data = "Hello, world!";
            Console.WriteLine("SimulateLongProcessAsync() complete.");

            return data;
        }

        // Synchronous method to simulate short running work in application code
        public int ShortRunningCalculation()
        {
            Console.WriteLine("ShortRunningCalculation() start.");

            int n = Foo(10);

            Console.WriteLine("ShortRunningCalculation() complete.");

            return n;
        }

        private static int Foo(int n)
        {
            return 100 * n;
        }
    }
}
