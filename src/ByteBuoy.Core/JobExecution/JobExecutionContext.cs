using ByteBuoy.Domain.Entities.Config;

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
	}
}
