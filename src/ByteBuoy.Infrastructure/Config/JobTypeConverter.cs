using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ByteBuoy.Infrastructure.Config
{
	// Custom type converter to handle different job types
	public class JobTypeConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return typeof(Domain.Entities.Config.ActionConfig).IsAssignableFrom(type);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            // Implement logic to read YAML and instantiate appropriate Job type
            // This is where you'll need to look at the YAML structure to decide which Job class to instantiate
            return new();
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            // Implement write logic if necessary
        }
    }
}
