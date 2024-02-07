namespace ByteBuoy.Agent.Dtos
{
	internal class GetHealthDto
	{
		public bool IsHealthy { get => Body.Equals("OK"); }
		public string Body { get; set; } = null!;
	}
}
