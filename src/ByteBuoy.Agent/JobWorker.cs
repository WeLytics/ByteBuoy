using ByteBuoy.Core.Helpers;
using ByteBuoy.Core.JobExecution;
using ByteBuoy.Infrastructure.Config;
using Microsoft.Extensions.Hosting;

namespace ByteBuoy.Agent.Services
{
	internal class JobWorker(IHostApplicationLifetime appLifetime, ICommandLineService commandLineService) : IHostedService
	{
		private readonly IHostApplicationLifetime _appLifetime = appLifetime;
		
		public async Task StartAsync(CancellationToken cancellationToken)
		{
			Console.WriteLine($"ByteBuoy Agent started ({IOHelper.GetVersion()})");
			cancellationToken.ThrowIfCancellationRequested();

			var filePath = commandLineService.FilePath;
			var isDryRun = commandLineService.DryRun;

			if (!File.Exists(filePath))
			{
				Console.WriteLine($"File not found: {filePath}");
				_appLifetime.StopApplication();
				return;
			}

			var configReader = new AgentConfigReader();
			var config = await configReader.ReadAgentConfigAsync(filePath);
			if (config?.IsValid() == true)
			{
				if (isDryRun)
					Console.WriteLine("DRY RUN MODE ENABLED");

				var executionService = new JobExecutor(config)
				{
					DryRun = isDryRun
				};

				await executionService.ExecuteTasksAsync();
			}

			_appLifetime.StopApplication();
			return;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
