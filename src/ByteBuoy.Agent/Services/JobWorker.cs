using ByteBuoy.Agent.JobExecution;
using ByteBuoy.Infrastructure.Config;
using Microsoft.Extensions.Hosting;

namespace ByteBuoy.Agent.Services
{
	internal class JobWorker(IHostApplicationLifetime appLifetime, ICommandLineService commandLineService) : IHostedService
	{
		private readonly IHostApplicationLifetime _appLifetime = appLifetime;

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			Console.WriteLine("ByteBuoy Agent started (v0.1.6)");
			cancellationToken.ThrowIfCancellationRequested();

			var filePath = commandLineService.FilePath;

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
				var executionService = new JobExecutor(config);
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
