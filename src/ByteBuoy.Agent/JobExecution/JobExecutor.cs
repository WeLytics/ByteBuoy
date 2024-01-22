using System.Diagnostics;
using ByteBuoy.Agent.JobExecution.JobActions;
using ByteBuoy.Agent.Services;
using ByteBuoy.Domain.Entities.Config;
using ByteBuoy.Domain.Entities.Config.Jobs;

namespace ByteBuoy.Agent.JobExecution
{
	internal class JobExecutor
	{
		private readonly AgentConfig _agentConfig;
		private readonly ApiService _apiService;

		private List<JobExecutionStep> _jobExecutionSteps = [];

		internal JobExecutor(AgentConfig agentConfig)
		{
			_agentConfig = agentConfig;
			_apiService = new ApiService(_agentConfig.Host, _agentConfig.ApiKey);

			BuildExecutionTree();
		}


		private void BuildExecutionTree()
		{
			foreach (JobConfig jobConfig in _agentConfig.Jobs)
			{
				switch (jobConfig)
				{
					case FilesExistsConfig filesExistsConfig:
						AddExecutionStep(jobConfig, new FilesExistsAction(filesExistsConfig, _apiService));
						break;
					case FilesCopyConfig filesCopyConfig:
						AddExecutionStep(jobConfig, new FilesCopyAction(filesCopyConfig, _apiService));
						break;
					case FilesMoveJobConfig filesMoveConfig:
						AddExecutionStep(jobConfig, new FilesMoveAction(filesMoveConfig, _apiService));
						break;
					case FilesHashesConfig filesHashesConfig:
						AddExecutionStep(jobConfig, new FilesHashesAction(filesHashesConfig, _apiService));
						break;
					case SshUploadConfig sshUploadConfig:
						AddExecutionStep(jobConfig, new SshUploadAction(sshUploadConfig, _apiService));
						break;
					case CommandLineConfig commandLineConfig:
						AddExecutionStep(jobConfig, new CommandLineAction(commandLineConfig, _apiService));
						break;
					default:
						throw new NotImplementedException($"Job type {jobConfig.GetType().Name} is not implemented");
				}
			}
		}

		private void AddExecutionStep(JobConfig jobConfig, IJobAction jobAction)
		{
			_jobExecutionSteps.Add(new JobExecutionStep
			{
				JobId = _jobExecutionSteps.Count + 1,
				Config = jobConfig,
				jobAction = jobAction
			});
		}

		public async Task ExecuteJobsAsync()
		{
			foreach (var executionStep in _jobExecutionSteps)
			{
				var config = executionStep.Config;
				await Console.Out.WriteLineAsync($"Executing {config.Name} ({config.Action})");

				var timer = new Stopwatch();
				timer.Start();
				await ExecuteJobAsync(executionStep);
				timer.Stop();

				await Console.Out.WriteLineAsync($"Finished {config.Name} ({config.Action}) in {timer.Elapsed.TotalSeconds}s");
			}
		}

		public static async Task ExecuteJobAsync(JobExecutionStep executionStep)
		{
			// execute the job
			try
			{
				await executionStep.jobAction.ExecuteAsync();
			}
			catch (Exception ex)
			{
				// log the error
				//await Logger.LogAsync(ex);
				// if the job is configured to continue on error, then continue to the next job, otherwise throw the exception
				if (executionStep.Config?.ContinueOnError == true)
				{
					throw;
				}
			}
		}

	}
}
