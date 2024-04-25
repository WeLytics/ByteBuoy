namespace ByteBuoy.Core.JobExecution.JobActions
{
	public interface IJobAction
	{
		Task ExecuteAsync(JobExecutionContext jobExecutionContext);
	}
}
