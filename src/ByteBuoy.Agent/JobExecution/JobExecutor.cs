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
			var executionContext = new JobExecutionContext(_agentConfig);	
			if (!await ConnectionTestAsync())
			{
				await LogAsync("Failed to connect to the API " + _agentConfig.Host);
				return;
			}

			await CreateJobAsync();

			foreach (var executionStep in _jobExecutionSteps)
			{
				await CreateJobHistoryAsync(executionStep);
				await ExecuteStep(executionContext, executionStep);
			}

			await FinishJobAsync();
		}

		private async Task ExecuteStep(JobExecutionContext executionContext, JobExecutionStep executionStep)
		{
			var config = executionStep.Config;
			await LogAsync($"Task {_jobExecutionSteps.IndexOf(executionStep) + 1} / {_jobExecutionSteps.Count}");
			await LogAsync($"Executing {config.Name} ({config.Action})");

			var timer = new Stopwatch();
			timer.Start();
			await ExecuteJobAsync(executionContext, executionStep);
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
				Status = Domain.Enums.JobStatus.Running,
				
			});

			_jobId = response?.Data?.Id ?? throw new Exception("Failed to start job");
		}

		private async Task CreateJobHistoryAsync(JobExecutionStep step)
		{
			if (_jobId <= 0)
				throw new Exception("JobId is not set");

			var response = await _apiService.CreateJobHistoryAsync(new Application.Contracts.CreateJobHistoryContract()
			{
				TaskName = step.Config.Name,
				TaskNumber = _jobExecutionSteps.IndexOf(step) + 1,
				JobId = _jobId,
				Description = step.Config.Description,
				Status = Domain.Enums.TaskStatus.OK,
				ErrorMessage = null
			});
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

		public static async Task ExecuteJobAsync(JobExecutionContext executionContext, JobExecutionStep executionStep)
		{
			try
			{
				await executionStep.jobAction.ExecuteAsync(executionContext);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				if (executionStep.Config?.ContinueOnError == true)
				{
					throw;
				}
			}
		}

	}
}
