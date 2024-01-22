using ByteBuoy.Agent.Helpers;
using ByteBuoy.Agent.Services;
using ByteBuoy.Domain.Entities.Config.Jobs;

namespace ByteBuoy.Agent.JobExecution.JobActions
{
	internal class FilesHashesAction(FilesHashesConfig config, ApiService apiService) : IJobAction
	{
		public async Task ExecuteAsync()
		{
			foreach (var source in config.Paths)
			{


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
