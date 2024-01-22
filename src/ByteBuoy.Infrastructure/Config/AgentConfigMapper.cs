using ByteBuoy.Domain.Entities.Config;
using ByteBuoy.Domain.Entities.Config.Jobs;
using Riok.Mapperly.Abstractions;

namespace ByteBuoy.Infrastructure.Config
{
	[Mapper]
	internal partial class AgentConfigMapper
	{
		internal partial AgentConfig AgentConfigDtoToAgentConfig(AgentConfigDto configDto);
		internal partial FilesExistsConfig JobDtoToFilesExistsConfig(JobConfigDto configDto);
		internal partial FilesCopyConfig JobDtoToFilesCopyConfig(JobConfigDto configDto);
		internal partial FilesMoveJobConfig JobDtoToFilesMoveConfig(JobConfigDto configDto);
		internal partial FilesHashesConfig JobDtoToFilesHashesConfig(JobConfigDto configDto);
		internal partial SshUploadConfig JobDtoToSshUploadConfig(JobConfigDto configDto);
		internal partial CommandLineConfig JobDtoToCommandLineConfig(JobConfigDto configDto);
	}
}
