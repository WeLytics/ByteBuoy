using ByteBuoy.Domain.Entities.Config;

namespace ByteBuoy.Agent.JobExecution
{
	internal class LoggingConfiguration
		{
			public Action<JobExecutionStep, string>? LogAction { get; set; }
			public Action<JobExecutionStep, string>? ErrorLogAction { get; set; }
		}

	internal class JobExecutionContext
	{
		private readonly LoggingConfiguration _loggingConfiguration;
		private readonly AgentConfig _agentConfig;
		public JobExecutionStep CurrentExecutionStep { get; set; } = null!;

		public JobExecutionContext(AgentConfig agentConfig, Action<LoggingConfiguration> configureLogging)
		{
			_loggingConfiguration = new LoggingConfiguration();
			configureLogging(_loggingConfiguration);
			_agentConfig = agentConfig;
		}

		public AgentConfig GetAgentConfig() => _agentConfig;

		public string[] GetGlobalgnoredFiles() => !string.IsNullOrEmpty(_agentConfig.IgnoreFiles) ?  _agentConfig.IgnoreFiles.Split(";", StringSplitOptions.RemoveEmptyEntries) : [];

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
