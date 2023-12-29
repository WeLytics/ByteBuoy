using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using ByteBuoy.Domain.Entities.Config;

namespace ByteBuoy.Infrastructure.Config
{
    public class JobConfigReader
    {

        static void Main(string[] args)
        {
            var yamlPath = "path/to/yourfile.yml";
            var yamlContent = File.ReadAllText(yamlPath);

            var deserializer = new DeserializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                .WithTypeConverter(new JobTypeConverter())
                                .Build();

            var config = deserializer.Deserialize<JobConfig>(yamlContent);

            Console.WriteLine($"Version: {config.Version}");
            Console.WriteLine($"Host: {config.Host}");

            foreach (var job in config.Jobs)
            {
                Console.WriteLine($"Job Type: {job.GetType().Name}");
                job.Execute(); // Polymorphic call to execute the specific job type
            }
        }
    }
}
