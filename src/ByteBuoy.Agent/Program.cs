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
			// integrate logging
			// read config
			// execute jobs

			try
			{
				await Host.CreateDefaultBuilder()
				   // .ConfigureLogging(logging => logging.ClearProviders())
				   .UseSerilog((hostContext, loggerConfiguration) =>
				   {
					   loggerConfiguration
					   .MinimumLevel.Debug()
					   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
					   .MinimumLevel.Override("System", LogEventLevel.Warning)
					   .WriteTo.File("Setup.log", rollingInterval: RollingInterval.Day);
				   })
				   .ConfigureServices((hostContext, services) =>
				   {
					   //services.AddSingleton<CreatePackageWorker>();
					   //services.AddSingleton<SetupConfigurationWorker>();
					   //services.AddSingleton<SetupStarterWorker>();
					   //services.AddSingleton<UpdateWorker>();
					   //services.AddSingleton<SetupWorker>();
				   })
				   .Build().RunAsync();
				//.RunCommandLineApplicationAsync<Program>(args);


				if (!System.Diagnostics.Debugger.IsAttached)
				{
					Console.Write("Press any key to exit application...");
					Console.ReadKey();
				}
				//return host;
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
