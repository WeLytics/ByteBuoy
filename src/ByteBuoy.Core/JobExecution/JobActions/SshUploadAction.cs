using ByteBuoy.Core.Services;
using ByteBuoy.Domain.Entities.Config.Tasks;

namespace ByteBuoy.Core.JobExecution.JobActions
{
	internal class SshUploadAction(SshUploadConfig config, ApiService apiService) : IJobAction
	{
		private JobExecutionContext _jobExecutionContext;

		public async Task ExecuteAsync(JobExecutionContext jobExecutionContext)
		{
			_jobExecutionContext = jobExecutionContext ?? throw new ArgumentNullException(nameof(jobExecutionContext));

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
