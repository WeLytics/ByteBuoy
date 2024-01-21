namespace ByteBuoy.Domain.Entities.Config.Actions
{
	public class CheckHashesConfig : ActionConfig
	{
		public List<string> Paths { get; set; } = [];
	}
}
