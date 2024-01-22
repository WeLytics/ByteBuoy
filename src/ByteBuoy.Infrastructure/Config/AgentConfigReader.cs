using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using ByteBuoy.Domain.Entities.Config;
using YamlDotNet.Core;
using ByteBuoy.Domain.Entities.Config.Jobs;

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

		public class Path
		{
			public string PathValue { get; set; }
		}

		public class Label
		{
			public string Customer { get; set; }
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
				TryLoadingActions(configDto, result);

                return null;
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

		private void TryLoadingActions(AgentConfigDto configDto, AgentConfig result)
		{
			result.Jobs = [];
			var mapper = new AgentConfigMapper();
			foreach (var job in configDto.Jobs)
			{
				switch (job.Action)
				{
					case "filesCopy@v1":
						var filesCopyJob = mapper.JobDtoToFileExistsConfig(job);
						result.Jobs.Add(filesCopyJob);
						break;


					case "bashCommand@v1":
						var bashCommandConfig = mapper.JobDtoToFileExistsConfig(job);
						result.Jobs.Add(bashCommandConfig);
						break;


					default:
						AddValidationError("Invalid action: " + job.Action);
						break;
				}
			}
		}
	}
}
