
using ByteBuoy.Domain.Entities.Config;

namespace ByteBuoy.Agent.JobExecution.JobActions
{
	internal interface IJobAction
	{
		Task ExecuteAsync();
	}
}
