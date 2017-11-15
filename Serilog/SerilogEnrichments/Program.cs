using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

// https://serilog.net/
// https://github.com/serilog/serilog
// https://github.com/nblumhardt/serilog-enrichers-demystify
// https://github.com/serilog/serilog/wiki/Provided-Sinks
// https://github.com/serilog/serilog-sinks-literate

namespace SerilogEnrichments
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                //.Enrich.WithDemystifiedStackTraces() // <- Add this line
               //.WriteTo.RollingFile("log-{Date}.txt")
               .WriteTo.ColoredConsole()
                .CreateLogger();

            //var log = new LoggerConfiguration()
            //    .WriteTo.LiterateConsole()
            //    .WriteTo.RollingFile("log-{Date}.txt")
            //    .CreateLogger();

            Task t = A(10);
            t.Wait();
            Log.CloseAndFlush();
            Console.ReadKey();
        }

        private static async Task A(int i)
        {
            await Task.Yield();
            await B(i * 2).ConfigureAwait(false);
        }
        private static async Task B(int i)
        {
            try
            {
                await Task.Delay(10).ConfigureAwait(false);
                var position = new { Latitude = 25, Longitude = 134 };
                var elapsedMs = 34;

                Log.Logger.Information("Processed {@Position} in {Elapsed} ms.", position, elapsedMs);

                await C().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "PID {PID}, TID {TID} ms.", 121, Thread.CurrentThread.ManagedThreadId);
            }
        }

        private static async Task C()
        {
            await Task.Delay(10).ConfigureAwait(false);
            throw new InvalidCastException("a to b");
        }

    }
}
