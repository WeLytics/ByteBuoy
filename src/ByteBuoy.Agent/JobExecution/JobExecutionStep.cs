using ByteBuoy.Agent.JobExecution.JobActions;
using ByteBuoy.Domain.Entities.Config.Jobs;

namespace ByteBuoy.Agent.JobExecution
{
	internal class JobExecutionStep
	{
		public int JobId { get; set; }
		public IJobAction jobAction { get; set; }	
		public JobConfig Config { get; set; }
	}
}
