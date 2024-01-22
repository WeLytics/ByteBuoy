using ByteBuoy.Agent.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace ByteBuoy.Agent
{
	internal class Program
    {
		public static async Task<int> Main(string[] args)
		{
			try
			{
				var host = Host.CreateDefaultBuilder()
				   .UseSerilog((hostContext, loggerConfiguration) =>
				   {
					   loggerConfiguration
					   .MinimumLevel.Debug()
					   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
					   .MinimumLevel.Override("System", LogEventLevel.Warning)
					   .WriteTo.File("bytebuoy.log", rollingInterval: RollingInterval.Day);
				   })
				   .ConfigureServices((hostContext, services) =>
				   {
					   services.AddHostedService<JobWorker>();
					   services.AddSingleton<ICommandLineService, CommandLineService>(serviceProvider =>
                       {
                           return new CommandLineService(args);
                       });

                   })
				   .Build();


                await host.RunAsync();

                if (System.Diagnostics.Debugger.IsAttached)
				{
					Console.Write("Press any key to exit (Debugging)...");
					Console.ReadKey();
				}
				return 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Console.ReadKey();
				return -1;
			}
		}
    }
}
