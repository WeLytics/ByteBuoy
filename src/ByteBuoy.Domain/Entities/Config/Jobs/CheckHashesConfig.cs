namespace ByteBuoy.Domain.Entities.Config.Jobs
{
	public class CheckHashesConfig : JobConfig
	{
		public List<string> Paths { get; set; } = [];
	}
}
