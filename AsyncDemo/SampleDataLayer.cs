using System;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public static class SampleDataLayer
    {
        private const string DelayedResponseApiUrl = "https://mfcallahan-homepage-dev.azurewebsites.net/api/Utils/DelayedResponse?seconds=";

        // Synchronous method to simulate a long running HTTP request
        public static string GetDelayedApiResponse(int seconds)
        {
            string thisMethodName = MethodBase.GetCurrentMethod().Name;
            using (WebClient client = new WebClient())
            {
                Console.WriteLine($"{thisMethodName} start.");
                var message = client.DownloadString(string.Concat(DelayedResponseApiUrl, seconds));
                Console.WriteLine($"{thisMethodName} complete.");

                return message.Trim('"');
            }
        }

        // Asynchronous method to simulate a long running HTTP request
        public async static Task<string> GetDelayedApiResponseAsync(int seconds)
        {
            string thisMethodName = MethodBase.GetCurrentMethod().Name;
            using (WebClient client = new WebClient())
            {
                Console.WriteLine($"{thisMethodName} start.");
                var message =  await client.DownloadStringTaskAsync(string.Concat(DelayedResponseApiUrl, seconds)).ConfigureAwait(false);
                Console.WriteLine($"{thisMethodName} complete.");

                return message.Trim('"');
            }
        }

        // Synchronous method to simulate long running work in application code
        public static string SimulateLongProcess(int seconds)
        {
            string thisMethodName = MethodBase.GetCurrentMethod().Name;
            Console.WriteLine($"{thisMethodName} start.");

            Thread.Sleep(seconds * 1000);

            const string data = "Hello, world!";
            Console.WriteLine($"{thisMethodName} complete.");

            return data;
        }

        // Asynchronous method to simulate long running work in application code
        public async static Task<string> SimulateLongProcessAsync(int seconds)
        {
            string thisMethodName = MethodBase.GetCurrentMethod().Name;
            Console.WriteLine($"{thisMethodName} start.");

            await Task.Delay(seconds * 1000).ConfigureAwait(false);

            const string data = "Hello, world!";
            Console.WriteLine($"{thisMethodName} complete.");

            return data;
        }

        // Synchronous method to simulate short running work in application code
        public static int Foo()
        {
            string thisMethodName = MethodBase.GetCurrentMethod().Name;
            Console.WriteLine($"{thisMethodName} start.");
            int n = Bar(10);
            Console.WriteLine($"{thisMethodName} complete.");

            return n;
        }

        private static int Bar(int n)
        {
            return 100 * n;
        }
    }
}
