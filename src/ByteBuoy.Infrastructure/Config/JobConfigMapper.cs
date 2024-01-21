using ByteBuoy.Domain.Entities.Config;
using Riok.Mapperly.Abstractions;
using static ByteBuoy.Infrastructure.Config.JobConfigReader;

namespace ByteBuoy.Infrastructure.Config
{
	[Mapper]
	internal partial class JobConfigMapper
	{
		public partial JobConfig ConfigDtoToConfig(ConfigDto configDto);
		public partial ActionConfig ConfigDtoToConfig2(ConfigDto configDto);
	}
}
