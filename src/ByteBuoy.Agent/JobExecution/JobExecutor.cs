using System.Diagnostics;
using ByteBuoy.Agent.JobExecution.JobActions;
using ByteBuoy.Agent.Services;
using ByteBuoy.Domain.Entities.Config;
using ByteBuoy.Domain.Entities.Config.Tasks;

namespace ByteBuoy.Agent.JobExecution
{
	internal class JobExecutor
	{
		private readonly AgentConfig _agentConfig;
		private readonly ApiService _apiService;

		private readonly List<JobExecutionStep> _jobExecutionSteps = [];
		private int _jobId;

		internal JobExecutor(AgentConfig agentConfig)
		{
			_agentConfig = agentConfig;
			_apiService = new ApiService(_agentConfig);

			BuildExecutionTree();
		}


		private void BuildExecutionTree()
		{
			foreach (TaskConfig taskConfig in _agentConfig.Tasks)
			{
				switch (taskConfig)
				{
					case FilesExistsConfig filesExistsConfig:
						AddTask(taskConfig, new FilesExistsAction(filesExistsConfig, _apiService));
						break;
					case FilesCopyConfig filesCopyConfig:
						AddTask(taskConfig, new FilesCopyAction(filesCopyConfig, _apiService));
						break;
					case FilesMoveJobConfig filesMoveConfig:
						AddTask(taskConfig, new FilesMoveAction(filesMoveConfig, _apiService));
						break;
					case FilesHashesConfig filesHashesConfig:
						AddTask(taskConfig, new FilesHashesAction(filesHashesConfig, _apiService));
						break;
					case SshUploadConfig sshUploadConfig:
						AddTask(taskConfig, new SshUploadAction(sshUploadConfig, _apiService));
						break;
					case CommandLineConfig commandLineConfig:
						AddTask(taskConfig, new CommandLineAction(commandLineConfig, _apiService));
						break;
					default:
						throw new NotImplementedException($"Task type {taskConfig.GetType().Name} is not implemented");
				}
			}
		}

		private void AddTask(TaskConfig jobConfig, IJobAction jobAction)
		{
			_jobExecutionSteps.Add(new JobExecutionStep
			{
				TaskId = _jobExecutionSteps.Count + 1,
				Config = jobConfig,
				jobAction = jobAction
			});
		}

		public async Task ExecuteTasksAsync()
		{
			if (!await ConnectionTestAsync())
			{
				await LogAsync("Failed to connect to the API " + _agentConfig.Host);
				return;
			}

			await CreateJobAsync();

			foreach (var executionStep in _jobExecutionSteps)
			{
				await ExecuteStep(executionStep);
			}

			await FinishJobAsync();
		}

		private async Task ExecuteStep(JobExecutionStep executionStep)
		{
			var config = executionStep.Config;
			await LogAsync($"Task {_jobExecutionSteps.IndexOf(executionStep) + 1} / {_jobExecutionSteps.Count}");
			await LogAsync($"Executing {config.Name} ({config.Action})");

			var timer = new Stopwatch();
			timer.Start();
			await ExecuteJobAsync(executionStep);
			timer.Stop();

			await LogAsync($"Finished {config.Name} ({config.Action}) in {timer.Elapsed.TotalSeconds}s");
		}

		private async Task<bool> ConnectionTestAsync()
		{
			return await _apiService.IsHealthy();
		}

		private async Task CreateJobAsync()
		{
			var response = await _apiService.CreateJobAsync(new Application.Contracts.CreateJobContract()
			{
				Description = _agentConfig.Description,
				HostName = Environment.MachineName,
				Status = Domain.Enums.JobStatus.Running
			});

			_jobId = response?.Data?.Id ?? throw new Exception("Failed to start job");
		}

		private async Task FinishJobAsync()
		{
			await _apiService.FinishJobAsync(new Application.Contracts.UpdateJobContract()
			{
				JobId = _jobId,
				FinishedDateTime = DateTime.UtcNow,
				Status = Domain.Enums.JobStatus.Success
			});
		}

		private static Task LogAsync(string message) => Console.Out.WriteLineAsync(message);

		public static async Task ExecuteJobAsync(JobExecutionStep executionStep)
		{
			try
			{
				await executionStep.jobAction.ExecuteAsync();
			}
			catch (Exception ex)
			{
				// log the error
				//await Logger.LogAsync(ex);
				if (executionStep.Config?.ContinueOnError == true)
				{
					throw;
				}
			}
		}

	}
}
