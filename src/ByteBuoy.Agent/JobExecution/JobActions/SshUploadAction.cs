using ByteBuoy.Agent.Services;
using ByteBuoy.Domain.Entities.Config.Jobs;

namespace ByteBuoy.Agent.JobExecution.JobActions
{
	internal class SshUploadAction(SshUploadConfig config, ApiService apiService) : IJobAction
	{
		public async Task ExecuteAsync()
		{
			foreach (var source in config.Sources)
			{
				foreach (var destination in config.Targets)
				{
					await SshFileUpload(source, destination);
				}
			}
			return;
		}

		private async Task SshFileUpload(string source, string destination)
		{
			try
			{


			}
			catch (IOException ioException)
			{

				throw;
			}
		}
	}
}
