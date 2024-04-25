using System.IO;
using ByteBuoy.Application.Contracts;
using System.Text.Json;
using ByteBuoy.Core.Helpers;
using ByteBuoy.Core.Services;
using ByteBuoy.Domain.Entities.Config.Tasks;

namespace ByteBuoy.Core.JobExecution.JobActions
{
	internal class FilesCopyAction(FilesCopyConfig _config, ApiService _apiService) : IJobAction
	{
		private JobExecutionContext _jobExecutionContext = null!;

		public async Task ExecuteAsync(JobExecutionContext jobExecutionContext)
		{
			_jobExecutionContext = jobExecutionContext ?? throw new ArgumentNullException(nameof(jobExecutionContext));

			foreach (var source in _config.Sources)
			{
				if (source.Contains('*') || source.Contains('?'))
				{
					var directory = Path.GetDirectoryName(source) ?? throw new InvalidOperationException();

					var searchPattern = Path.GetFileName(source);

					if (Directory.Exists(directory))
					{
						string[] files = Directory.GetFiles(directory, searchPattern);
						foreach (string sourceFileName in files)
						{
							if (!IOHelper.IsFileIgnored(sourceFileName, _jobExecutionContext.GetGlobalgnoredFiles()))
							{
								foreach (var destination in _config.Targets)
								{
									var targetFileName = Path.Combine(destination, Path.GetFileName(sourceFileName));	
									_jobExecutionContext.AddLog($"Copy file from {sourceFileName} to {targetFileName}");
									await CopyFilesAsync(sourceFileName, targetFileName);
								}
							}
							else
							{
								_jobExecutionContext.AddLog($"File {sourceFileName} is ignored");
							}
						}
					}
					else
					{
						_jobExecutionContext.AddErrorLog("Directory does not exist");
					}
				}
				else if (File.Exists(source))
				{
					foreach (var destination in _config.Targets)
					{
						_jobExecutionContext.AddLog($"Copy files from {source} to {destination}");
						await CopyFilesAsync(source, destination);
					}
				}
			}
			return;
		}

		private async Task CopyFilesAsync(string source, string destination)
		{
			try
			{
				//check if destination is a directory or file, if directory create it if needed
				if (File.Exists(destination))
				{
					//destination is a file, check if it is a directory
					//if it is a directory, append the source filename to it
					var fileInfo = new FileInfo(destination);
					if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
					{
						destination = Path.Combine(destination, Path.GetFileName(source));
					}
				}
				else
				{
					//destination does not exist, check if parent directory exists
					var directoryInfo = new DirectoryInfo(destination);
					if (directoryInfo.Parent?.Exists == false)
					{
						//parent directory does not exist, create it
						directoryInfo.Parent!.Create();
					}
				}

				await IOHelper.CopyFileAsync(source, destination);
				await _jobExecutionContext.SendApiRequestPath(_config, _apiService, destination);
			}
			catch (IOException ioException)
			{
				_jobExecutionContext.AddErrorLog($"Failed copying from {source} to {destination}: {ioException}");
			}
		}
	}
}
