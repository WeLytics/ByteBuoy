namespace ByteBuoy.Agent.JobExecution.JobActions
{
	internal interface IJobAction
	{
		Task ExecuteAsync(JobExecutionContext jobExecutionContext);
	}
}
