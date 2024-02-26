using System.Text.Json;
using ByteBuoy.Agent.Helpers;
using ByteBuoy.Agent.Services;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Domain.Entities.Config.Tasks;


namespace ByteBuoy.Agent.JobExecution.JobActions
{
	internal class FilesExistsAction(FilesExistsConfig _config, ApiService _apiService) : IJobAction
	{
		public async Task ExecuteAsync()
		{
			foreach (var source in _config.Paths)
			{
				await CheckPath(source);
			}
		}

		private async Task CheckPath(string path)
		{
			if (path.Contains('*') || path.Contains('?'))
			{
				string directory = Path.GetDirectoryName(path) ?? throw new InvalidOperationException();
				string searchPattern = Path.GetFileName(path);

				if (Directory.Exists(directory))
				{
					string[] files = Directory.GetFiles(directory, searchPattern);
					foreach (string file in files)
					{
						await SendApiRequest(file);
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
				MetaJson = JsonSerializer.Serialize(new
				{
					path = filePath,
					labels = _config.Labels,
					hashSHA256 = FileHasher.GetFileSHA256Hash(filePath)
				})
			};

			var response = await _apiService.PostPageMetric(payload);
			if (!response.IsSuccess)
			{
				Console.WriteLine($"Error sending API request: {response.ErrorMessage}");
				Console.WriteLine($"Request: {response?.Response?.Request.Resource}");
				Console.WriteLine($"Response: {response.Response?.Content}");
			}
		}
	}
}
