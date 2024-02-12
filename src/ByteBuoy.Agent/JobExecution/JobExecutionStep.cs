using ByteBuoy.Agent.JobExecution.JobActions;
using ByteBuoy.Domain.Entities.Config.Tasks;

namespace ByteBuoy.Agent.JobExecution
{
	internal class JobExecutionStep
	{
		public int TaskId { get; set; }
		public IJobAction jobAction { get; set; } = null!;
		public TaskConfig Config { get; set; } = null!;
	}
}
