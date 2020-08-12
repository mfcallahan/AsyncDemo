using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using static AsyncDemo.SampleDataLayer;

namespace AsyncDemo
{
    public static class Program
    {
        // Wait time in seconds for SampleDataLayer methods
        private const int ApiWaitTimeSeconds = 4;
        private const int MethodWaitTimeSeconds = 3;

        public static async Task Main()
        {
            Stopwatch s = new Stopwatch();

            // RunDemoSynchronous() will call three methods in SampleDataLayer synchronously
            s.Start();
            RunDemoSynchronous();
            s.Stop();

            Console.WriteLine($"RunDemoSynchronous() complete. Elapsed seconds: {s.Elapsed.TotalSeconds}" + Environment.NewLine);

            // RunDemoAsync() will call three methods in SampleDataLayer asynchronously
            s.Restart();
            await RunDemoAsync().ConfigureAwait(false);
            s.Stop();
            Console.WriteLine($"RunDemoAsync() complete. Elapsed seconds: {s.Elapsed.TotalSeconds}" + s.Elapsed.TotalSeconds + Environment.NewLine);

            Console.WriteLine("Demo complete.");
            Console.ReadLine();
        }

        private static void RunDemoSynchronous()
        {
            string thisMethodName = MethodBase.GetCurrentMethod().Name;
            Console.WriteLine($"{thisMethodName} start.");

            // Execute long and short running methods synchronously
            string dataA = GetDelayedApiResponse(ApiWaitTimeSeconds);
            string dataB = SimulateLongProcess(MethodWaitTimeSeconds);
            int dataC = Foo();

            SampleData sample = new SampleData(dataA, dataB, dataC);

            Console.WriteLine("SampleData object 'sample' created:");
            Console.WriteLine("sample.A = {0}", sample.A);
            Console.WriteLine("sample.B = {0}", sample.B);
            Console.WriteLine("sample.C = {0}", sample.C);
        }

        private static async Task RunDemoAsync()
        {
            string thisMethodName = MethodBase.GetCurrentMethod().Name;
            Console.WriteLine($"{thisMethodName} start.");

            Stopwatch s = new Stopwatch();
            s.Start();

            // Create tasks for long running asynchronous methods
            Task<string> dataA = GetDelayedApiResponseAsync(ApiWaitTimeSeconds);
            Task<string> dataB = SimulateLongProcessAsync(MethodWaitTimeSeconds);

            // Execute short running synchronous method
            int dataC = Foo();

            // Await the tasks
            Console.WriteLine("Awaiting...");
            SampleData sample = new SampleData(await dataA.ConfigureAwait(false), await dataB.ConfigureAwait(false), dataC);

            Console.WriteLine($"SampleData object '{nameof(sample)}' created:");
            Console.WriteLine($"sample.A = {sample.A}");
            Console.WriteLine($"sample.B = {sample.B}");
            Console.WriteLine($"sample.C = {sample.C}");
        }
    }
}