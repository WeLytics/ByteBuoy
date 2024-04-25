using ByteBuoy.Application.Contracts;
using ByteBuoy.Core.Helpers;
using ByteBuoy.Core.Services;
using System.Text.Json;
using ByteBuoy.Domain.Entities.Config;
using ByteBuoy.Domain.Entities.Config.Tasks;

namespace ByteBuoy.Core.JobExecution
{
	public class LoggingConfiguration
	{
		public Action<JobExecutionStep, string>? LogAction { get; set; }
		public Action<JobExecutionStep, string>? ErrorLogAction { get; set; }
	}

	public class JobExecutionContext
	{
		private readonly LoggingConfiguration _loggingConfiguration;
		private readonly AgentConfig _agentConfig;
		public int JobId { get; internal set; }
		public bool IsDryRun { get; internal set; }
		public JobExecutionStep CurrentExecutionStep { get; set; } = null!;

		public JobExecutionContext(AgentConfig agentConfig, Action<LoggingConfiguration> configureLogging)
		{
			_loggingConfiguration = new LoggingConfiguration();
			configureLogging(_loggingConfiguration);
			_agentConfig = agentConfig;
		}

		public AgentConfig GetAgentConfig() => _agentConfig;

		public string[] GetGlobalgnoredFiles() => !string.IsNullOrEmpty(_agentConfig.IgnoreFiles) ? _agentConfig.IgnoreFiles.Split(";", StringSplitOptions.RemoveEmptyEntries) : [];

		public void AddLog(string message)
		{
			_loggingConfiguration.LogAction?.Invoke(CurrentExecutionStep, message);
		}

		public void AddErrorLog(string message)
		{
			_loggingConfiguration.ErrorLogAction?.Invoke(CurrentExecutionStep, message);
		}

		internal async Task SendApiRequestPath(TaskConfig taskConfig, ApiService apiService, string filePath)
		{
			if (IsDryRun)
			{
				AddLog($"DRY RUN: Would send API request for file {filePath}");
				return;
			}

			var payload = new CreatePageMetricContract()
			{
				JobId = JobId,
				Status = Domain.Enums.MetricStatus.Success,
				ValueString = Path.GetFileName(filePath),
				HashSHA256 = FileHasher.GetFileSHA256Hash(filePath),
				MetaJson = JsonSerializer.Serialize(new
				{
					path = filePath,
					labels = taskConfig.Labels,
				})
			};

			var response = await apiService.PostPageMetric(payload);
			if (!response.IsSuccess)
			{
				AddErrorLog($"Error sending API request: {response.ErrorMessage}");
				AddErrorLog($"Request: {response?.Response?.Request.Resource}");
				AddErrorLog($"Response: {response?.Response?.Content}");
			}
		}
	}
}
