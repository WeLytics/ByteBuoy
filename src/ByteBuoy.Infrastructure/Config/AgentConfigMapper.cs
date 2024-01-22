using ByteBuoy.Domain.Entities.Config;
using ByteBuoy.Domain.Entities.Config.Jobs;
using Riok.Mapperly.Abstractions;
using static ByteBuoy.Infrastructure.Config.AgentConfigReader;

namespace ByteBuoy.Infrastructure.Config
{
	[Mapper]
	internal partial class AgentConfigMapper
	{
		public partial AgentConfig AgentConfigDtoToAgentConfig(AgentConfigDto configDto);
		public partial FilesExistsConfig JobDtoToFileExistsConfig(JobConfigDto configDto);
		public partial FilesCopyConfig JobDtoToFilesCopyConfig(JobConfigDto configDto);
		public partial FilesMoveJobConfig JobDtoToFilesMoveConfig(JobConfigDto configDto);
	}
}
