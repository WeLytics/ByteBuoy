using ByteBuoy.Core.Services;
using ByteBuoy.Domain.Entities.Config.Tasks;
using Renci.SshNet;

namespace ByteBuoy.Core.JobExecution.JobActions
{
	internal class SshUploadAction(SshUploadConfig _config, ApiService _) : IJobAction
	{
		private JobExecutionContext _jobExecutionContext;
		private SftpClient _sftpClient;

		public async Task ExecuteAsync(JobExecutionContext jobExecutionContext)
		{
			_jobExecutionContext = jobExecutionContext ?? throw new ArgumentNullException(nameof(jobExecutionContext));
			InitClient();

			try
			{
				_sftpClient.Connect();
				_jobExecutionContext.AddLog("Connecting to the server...");

				foreach (var source in _config.Sources)
				{
					foreach (var destination in _config.Targets)
					{
						var remoteFileName = Path.GetFileName(source);
						using var localFileStream = File.OpenRead(source);
						_sftpClient.UploadFile(localFileStream, Path.Combine(destination, remoteFileName));
						Console.WriteLine($"Uploaded {remoteFileName} successfully.");
					}
				}
			}

			catch (Exception ex)
			{
				_jobExecutionContext.AddErrorLog($"Failed uploading files: {ex.Message}");
			}
			finally
			{
				if (_sftpClient.IsConnected)
				{
					_sftpClient.Disconnect();
					_jobExecutionContext.AddLog("Disconnected from the server.");
				}
			}

			return;
		}

		private void InitClient()
		{
			if (_config.PrivateKeyPath != null)
			{
				_sftpClient = new SftpClient(new ConnectionInfo(
					_config.Host,
					_config.Port.ToString()!,
					new PrivateKeyAuthenticationMethod(_config.Username, new PrivateKeyFile(_config.PrivateKeyPath))));
			}
			else
			{
				_sftpClient = new SftpClient(new ConnectionInfo(
					_config.Host,
					_config.Port.ToString(),
					new PasswordAuthenticationMethod(_config.Username, _config.Password)));
			}
		}
	}
}
