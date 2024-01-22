
namespace ByteBuoy.Agent.JobExecution.JobActions
{
	internal interface IJobAction
	{
		Task ExecuteAsync();

		public string ApiEndpoint { get; set; }
	}
}
