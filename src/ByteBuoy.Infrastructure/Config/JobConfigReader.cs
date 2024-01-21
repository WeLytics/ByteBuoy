using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using ByteBuoy.Domain.Entities.Config;
using YamlDotNet.Core;

namespace ByteBuoy.Infrastructure.Config
{
    public class JobConfigReader
    {
		public List<JobConfigValidationError> ValidationErrors = [];
		public bool HasErrors { get => ValidationErrors.Count > 0; }

		public async Task<JobConfig?> ReadConfigAsync(string yamlPath)
        {
            var yamlContent = await File.ReadAllTextAsync(yamlPath);

            return ReadConfigText(yamlContent);
        }

		public class ConfigDto
		{
			public int Version { get; set; }
			public string Host { get; set; }
			public string Apikey { get; set; }
			public List<JobDto> Jobs { get; set; }
		}

		public class JobDto
		{
			public string Name { get; set; }
			public string Action { get; set; }
			public bool? ContinueOnError { get; set; }
			public List<string> Commands { get; set; } // for bashCommand@v1
			public List<string> Sources { get; set; } // for sftpConnection@v1
			public List<string> Targets { get; set; } // for sftpConnection@v1
			public string SftpHost { get; set; } // for sftpConnection@v1
			public string Username { get; set; } // for sftpConnection@v1
			public List<string> Paths { get; set; } // for checkHashes@v1, checkFiles@v1
			public Dictionary<string, string> Labels { get; set; }
		}

		public class Path
		{
			public string PathValue { get; set; }
		}

		public class Label
		{
			public string Customer { get; set; }
		}



		public JobConfig? ReadConfigText(string yamlContent)
        {
			ValidationErrors.Clear();
            var deserializer = new DeserializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                //.WithTypeConverter(new JobTypeConverter())
                                .Build();

            try
            {
                var config = deserializer.Deserialize<ConfigDto>(yamlContent);
                return null;
            }
            catch (YamlException ex)
            {
                Console.WriteLine($"Error reading config: {ex.InnerException?.Message ?? ex.Message}");
				ValidationErrors.Add(new JobConfigValidationError(ex.Start.ToString(), ex.InnerException?.Message ?? ex.Message));
                return null;
            }
        }
    }
}
