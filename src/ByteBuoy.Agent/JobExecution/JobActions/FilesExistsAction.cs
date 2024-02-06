using System.Text.Json;
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
			return;
		}

		private async Task CheckPath(string path)
		{
			if (path.Contains('*') || path.Contains('?'))
			{
				string directory = Path.GetDirectoryName(path);
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
				Status = Domain.Enums.MetricStatus.OK,
				ValueString = Path.GetFileName(filePath),
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
			}
		}
	}
}
