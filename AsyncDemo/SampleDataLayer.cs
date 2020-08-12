using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public static class SampleDataLayer
    {
        const string _apiUrl = "http://mfcallahan-dev.com/api/GetDelayedResponse?waitSeconds=";

        // synchronous method to simulate a long running HTTP request
        public static string GetDelayedApiResponse(int seconds)
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine("GetDelayedApiResponse() start.");
                var message = client.DownloadString(string.Concat(_apiUrl, seconds));
                Console.WriteLine("GetDelayedApiResponse() complete.");

                return message.Trim('"');
            }
        }

        // async method to simulate a long running HTTP request
        public async static Task<string> GetDelayedApiResponseAsync(int seconds)
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine("GetDelayedApiResponseAsync() start.");
                var message =  await client.DownloadStringTaskAsync(string.Concat(_apiUrl, seconds));
                Console.WriteLine("GetDelayedApiResponseAsync() complete.");

                return message.Trim('"');
            }            
        }

        // synchronous method to simulate long running work in application code
        public static string SimulateLongProcess(int seconds)
        {
            Console.WriteLine("SimulateLongProcess() start.");
            Thread.Sleep(seconds * 1000);
            string data = "Hello, world!";
            Console.WriteLine("SimulateLongProcess() complete.");

            return data;
        }

        // async method to simulate long running work in application code
        public async static Task<string> SimulateLongProcessAsync(int seconds)
        {
            Console.WriteLine("SimulateLongProcessAsync() start.");
            await Task.Delay(seconds * 1000);
            string data = "Hello, world!";
            Console.WriteLine("SimulateLongProcessAsync() complete.");

            return data;
        }

        // synchronous methods to simulate short running work in application code
        public static int Foo()
        {
            Console.WriteLine("Foo() start.");
            int n = Bar(10);
            Console.WriteLine("Foo() complete.");

            return n;
        }

        static int Bar(int n)
        {
            return 100 * n;
        }
    }

    public class SampleData
    {
        public SampleData(string dataA, string dataB, int dataC)
        {
            A = dataA;
            B = dataB;
            C = dataC;
        }

        public string A { get; set; }
        public string B { get; set; }
        public int C { get; set; }
    }
}
