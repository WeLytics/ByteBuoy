using ByteBuoy.Core.Helpers;
using ByteBuoy.Core.Services;
using ByteBuoy.Domain.Entities.Config.Tasks;

namespace ByteBuoy.Core.JobExecution.JobActions
{
	internal class FilesCopyAction(FilesCopyConfig config, ApiService apiService) : IJobAction
	{
		private JobExecutionContext _jobExecutionContext;

		public async Task ExecuteAsync(JobExecutionContext jobExecutionContext)
		{
			_jobExecutionContext = jobExecutionContext ?? throw new ArgumentNullException(nameof(jobExecutionContext));

			foreach (var source in config.Sources)
			{
				foreach (var destination in config.Targets)
				{
					await CopyFilesAsync(source, destination);
				}
			}
			return;
		}

		private static async Task CopyFilesAsync(string source, string destination)
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
			}
			catch (IOException ioException)
			{

				throw;
			}


		}
	}
}
