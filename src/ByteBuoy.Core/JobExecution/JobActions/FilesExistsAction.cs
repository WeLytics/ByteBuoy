using ByteBuoy.Core.Helpers;
using ByteBuoy.Domain.Entities.Config.Tasks;
using ByteBuoy.Core.Services;


namespace ByteBuoy.Core.JobExecution.JobActions
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
			_jobExecutionContext.AddLog($"Checking resolved path: {path}");

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
							_jobExecutionContext.AddLog($"File {file} found");
							await _jobExecutionContext.SendApiRequestPath(_config, _apiService, file);
						}
						else
						{
							_jobExecutionContext.AddLog($"File {file} is ignored");
						}
					}
				}
				else
				{
					_jobExecutionContext.AddErrorLog("Directory does not exist");
				}
			}
			else if (File.Exists(path))
			{
				_jobExecutionContext.AddLog($"Checking path: {path}");
				await _jobExecutionContext.SendApiRequestPath(_config, _apiService, path);	
			}
		}
	}
}
