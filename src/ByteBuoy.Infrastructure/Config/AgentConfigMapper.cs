using ByteBuoy.Domain.Entities.Config;
using ByteBuoy.Domain.Entities.Config.Tasks;
using Riok.Mapperly.Abstractions;

namespace ByteBuoy.Infrastructure.Config
{
	[Mapper]
	internal partial class AgentConfigMapper
	{
		internal partial AgentConfig AgentConfigDtoToAgentConfig(JobDto configDto);
		internal partial FilesExistsConfig JobDtoToFilesExistsConfig(TaskDto configDto);
		internal partial FilesCopyConfig JobDtoToFilesCopyConfig(TaskDto configDto);
		internal partial FilesMoveJobConfig JobDtoToFilesMoveConfig(TaskDto configDto);
		internal partial FilesHashesConfig JobDtoToFilesHashesConfig(TaskDto configDto);
		internal partial SshUploadConfig JobDtoToSshUploadConfig(TaskDto configDto);
		internal partial CommandLineConfig JobDtoToCommandLineConfig(TaskDto configDto);
	}
}
