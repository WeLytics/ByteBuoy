using ByteBuoy.Core.JobExecution.JobActions;
using ByteBuoy.Domain.Entities.Config.Tasks;

namespace ByteBuoy.Core.JobExecution
{
	public class JobExecutionStep
	{
		public int TaskId { get; set; }
		public IJobAction jobAction { get; set; } = null!;
		public TaskConfig Config { get; set; } = null!;
	}
}
