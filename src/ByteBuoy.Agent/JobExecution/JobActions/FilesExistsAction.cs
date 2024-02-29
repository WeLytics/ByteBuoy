using System.Text.Json;
using ByteBuoy.Agent.Helpers;
using ByteBuoy.Agent.Services;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Entities.Config.Tasks;


namespace ByteBuoy.Agent.JobExecution.JobActions
{
	internal class FilesExistsAction(FilesExistsConfig _config, ApiService _apiService) : IJobAction
	{
		private JobExecutionContext _jobExecutionContext = null!;

		public async Task ExecuteAsync(JobExecutionContext jobExecutionContext)
		{
			_jobExecutionContext = jobExecutionContext ?? throw new ArgumentNullException(nameof(jobExecutionContext));

			foreach (var source in _config.Paths)
			{
				await CheckPath(source);
			}
		}

		private async Task CheckPath(string path)
		{
			path = IOHelper.ResolvePathWithDynamicPlaceholders(path);

			if (path.Contains('*') || path.Contains('?'))
			{
				var directory = Path.GetDirectoryName(path) ?? throw new InvalidOperationException();

				var searchPattern = Path.GetFileName(path);

				if (Directory.Exists(directory))
				{
					string[] files = Directory.GetFiles(directory, searchPattern);
					foreach (string file in files)
					{
						if (!IOHelper.IsFileIgnored(file, _jobExecutionContext.GetGlobalgnoredFiles()))
						{
							await SendApiRequest(file);
						}
						else
						{
							Console.WriteLine($"File {file} is ignored.");
						} 
					}
				}
				else
				{
					Console.WriteLine("Directory does not exist.");
				}
			}
			else if (File.Exists(path))
			{
				await SendApiRequest(path);
			}
		}

		private async Task SendApiRequest(string filePath)
		{
			var payload = new CreatePageMetricContract()
			{
				Status = Domain.Enums.MetricStatus.Success,
				ValueString = Path.GetFileName(filePath),
				HashSHA256 = FileHasher.GetFileSHA256Hash(filePath),
				MetaJson = JsonSerializer.Serialize(new
				{
					path = filePath,
					labels = _config.Labels,
				})
			};

			var response = await _apiService.PostPageMetric(payload);
			if (!response.IsSuccess)
			{
				Console.WriteLine($"Error sending API request: {response.ErrorMessage}");
				Console.WriteLine($"Request: {response?.Response?.Request.Resource}");
				Console.WriteLine($"Response: {response?.Response?.Content}");
			}
		}
	}
}
