using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public static class Program
    {
        // Wait time in seconds for SampleDataLayer methods
        private const int ApiWaitTimeSeconds = 4;
        private const int MethodWaitTimeSeconds = 3;

        static async Task Main()
        {
            Console.WriteLine("Synchronous demo start.");

            Stopwatch s = new Stopwatch();
            s.Start();
            // RunDemoSynchronous() will call methods in SampleDataLayer synchronously
            RunDemoSynchronous(new SampleDataLayer());
            s.Stop();

            Console.WriteLine($"Synchronous demo complete. Elapsed seconds: {s.Elapsed.TotalSeconds}" + Environment.NewLine);

            Console.WriteLine("Asynchronous demo start.");

            s.Restart();
            // RunDemoAsync() will call methods in SampleDataLayer asynchronously
            await RunDemoAsync(new SampleDataLayer()).ConfigureAwait(false);
            s.Stop();

            Console.WriteLine($"Asynchronous demo complete. Elapsed seconds: {s.Elapsed.TotalSeconds}" + Environment.NewLine);

            Console.WriteLine("Demo complete.");
            Console.ReadLine();

            Environment.Exit(1);
        }

        private static void RunDemoSynchronous(SampleDataLayer sampleDataLayer)
        {
            // Execute long and short running methods synchronously
            string dataA = sampleDataLayer.GetDelayedApiResponse(ApiWaitTimeSeconds);
            string dataB = sampleDataLayer.SimulateLongProcess(MethodWaitTimeSeconds);
            int dataC = sampleDataLayer.ShortRunningCalculation();

            SampleData sample = new SampleData(dataA, dataB, dataC);

            Console.WriteLine();
            Console.WriteLine("SampleData object 'sample' created:");
            Console.WriteLine("sample.A = {0}", sample.A);
            Console.WriteLine("sample.B = {0}", sample.B);
            Console.WriteLine("sample.C = {0}", sample.C);
        }

        private static async Task RunDemoAsync(SampleDataLayer sampleDataLayer)
        {
            // Create tasks for long running asynchronous methods
            Task<string> taskDataA = sampleDataLayer.GetDelayedApiResponseAsync(ApiWaitTimeSeconds);
            Task<string> taskDataB = sampleDataLayer.SimulateLongProcessAsync(MethodWaitTimeSeconds);

            // Execute short running synchronous method
            int dataC = sampleDataLayer.ShortRunningCalculation();

            // Await the completeion of the tasks
            Console.WriteLine("Awaiting tasks...");
            await Task.WhenAll(taskDataA, taskDataB).ConfigureAwait(false);

            SampleData sample = new SampleData(taskDataA.Result, taskDataB.Result, dataC);

            Console.WriteLine();
            Console.WriteLine($"SampleData object '{nameof(sample)}' created:");
            Console.WriteLine($"sample.A = {sample.A}");
            Console.WriteLine($"sample.B = {sample.B}");
            Console.WriteLine($"sample.C = {sample.C}");
        }
    }
}
