using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static AsyncDemo.SampleDataLayer;

namespace AsyncDemo
{
    class Program
    {
        // Wait time in seconds for SampleDataLayer methods
        const int _apiWaitTime = 4;
        const int _methodWaitTime = 3;

        static async Task Main()
        {
            Stopwatch s = new Stopwatch();

            // RunDemoSynchronous() will call three methods in SampleDataLayer synchronously
            s.Start();
            RunDemoSynchronous();
            s.Stop();

            Console.WriteLine("RunDemoSynchronous() complete. Elapsed seconds: " + s.Elapsed.TotalSeconds + Environment.NewLine);

            // RunDemoAsync() will call three methods in SampleDataLayer asynchronously
            s.Restart();
            await RunDemoAsync();
            s.Stop();
            Console.WriteLine("RunDemoAsync() complete. Elapsed seconds: " + s.Elapsed.TotalSeconds + Environment.NewLine);

            Console.WriteLine("Demo complete.");
            Console.ReadLine();
        }

        static void RunDemoSynchronous()
        {
            Console.WriteLine("RunDemoSynchronous() start.");

            // execute long and short running methods synchronously
            string dataA = GetDelayedApiResponse(_apiWaitTime);
            string dataB = SimulateLongProcess(_methodWaitTime);
            int dataC = Foo();

            SampleData sample = new SampleData(dataA, dataB, dataC);

            Console.WriteLine("SampleData object 'sample' created:");
            Console.WriteLine("sample.A = {0}", sample.A);
            Console.WriteLine("sample.B = {0}", sample.B);
            Console.WriteLine("sample.C = {0}", sample.C);
        }

        static async Task RunDemoAsync()
        {
            Console.WriteLine("RunDemoAsync() start.");

            Stopwatch s = new Stopwatch();
            s.Start();

            // start long running asynchronous methods
            Task<string> dataA = GetDelayedApiResponseAsync(_apiWaitTime);
            Task<string> dataB = SimulateLongProcessAsync(_methodWaitTime);

            // start short running synchronous method which doesn't need to wait for dataA or dataB
            int dataC = Foo();

            // await the tasks
            Console.WriteLine("Awaiting...");
            SampleData sample = new SampleData(await dataA, await dataB, dataC);

            Console.WriteLine("SampleData object 'sample' created:");
            Console.WriteLine("sample.A = {0}", sample.A);
            Console.WriteLine("sample.B = {0}", sample.B);
            Console.WriteLine("sample.C = {0}", sample.C);
        }
    }
}