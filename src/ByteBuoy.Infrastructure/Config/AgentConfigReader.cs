using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using ByteBuoy.Domain.Entities.Config;
using YamlDotNet.Core;

namespace ByteBuoy.Infrastructure.Config
{
	public partial class AgentConfigReader
    {
		public List<AgentConfigValidationError> ValidationErrors = [];
		public bool HasErrors { get => ValidationErrors.Count > 0; }

		public async Task<AgentConfig?> ReadAgentConfigAsync(string yamlPath)
        {
            var yamlContent = await File.ReadAllTextAsync(yamlPath);

            return ReadConfigText(yamlContent);
        }

		public AgentConfig? ReadConfigText(string yamlContent)
        {
			ValidationErrors.Clear();
            var deserializer = new DeserializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                //.WithTypeConverter(new JobTypeConverter())
                                .Build();

            try
            {
                var configDto = deserializer.Deserialize<AgentConfigDto>(yamlContent);
				var mapper = new AgentConfigMapper();
				var result = mapper.AgentConfigDtoToAgentConfig(configDto);
				TryLoadingTaskActions(configDto, result);

                return result;
            }
            catch (YamlException ex)
            {
                AddValidationError(ex);
                return null;
            }
        }

		private void AddValidationError(Exception exception)
		{
			var error = exception.InnerException?.Message ?? exception.Message;
			var location  = exception is YamlException yamlException ? yamlException.Start.ToString() : string.Empty;
			AddValidationError(location + error);
		}

		private void AddValidationError(string message, string location = "")
		{
			Console.WriteLine($"Error reading config: {message}");
			ValidationErrors.Add(new AgentConfigValidationError(location, message));
		}

		private void TryLoadingTaskActions(AgentConfigDto jobDto, AgentConfig result)
		{
			result.Tasks = [];
			var mapper = new AgentConfigMapper();
			foreach (var job in jobDto.Tasks)
			{
				switch (job.Action)
				{
					case "filesCopy@v1":
						var filesCopyJob = mapper.JobDtoToFilesCopyConfig(job);
						result.Tasks.Add(filesCopyJob);
						break;
					case "filesMove@v1":
						var filesMoveJob = mapper.JobDtoToFilesMoveConfig(job);
						result.Tasks.Add(filesMoveJob);
						break;
					case "filesExists@v1":
						var filesExistsJob = mapper.JobDtoToFilesExistsConfig(job);
						result.Tasks.Add(filesExistsJob);
						break;
					case "commandLine@v1":
						var commandLineConfig = mapper.JobDtoToCommandLineConfig(job);
						result.Tasks.Add(commandLineConfig);
						break;
					case "sshUpload@v1":
						var sshUploadJob = mapper.JobDtoToSshUploadConfig(job);
						result.Tasks.Add(sshUploadJob);
						break;
					default:
						AddValidationError("Invalid action: " + job.Action);
						break;
				}
			}
		}
	}
}
