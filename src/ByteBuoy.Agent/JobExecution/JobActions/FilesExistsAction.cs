using System.Text.Json;
using ByteBuoy.Agent.Services;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Domain.Entities.Config;
using ByteBuoy.Domain.Entities.Config.Jobs;


namespace ByteBuoy.Agent.JobExecution.JobActions
{
	internal class FilesExistsAction(FilesExistsConfig config, ApiService apiService) : IJobAction
	{
		public async Task ExecuteAsync()
		{
			foreach (var source in config.Paths)
			{
				await CheckPath(source);
			}
			return;
		}

		private async Task CheckPath(string path)
		{
			try
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
							await SendApiRequest(path);
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
			catch (IOException ioException)
			{
				throw;
			}
		}

		private async Task SendApiRequest(string filePath)
		{
			var payload = new CreatePageMetricContract()
			{
				Status = Domain.Enums.MetricStatus.OK,
				MetaJson =  JsonSerializer.Serialize(new
				{
					path = filePath,
				})
			};

			var response = await apiService.PostPageMetric(payload);
			if (!response.IsSuccess)
			{
				Console.WriteLine($"Error sending API request: {response.ErrorMessage}");
			}
		}
	}
}
