namespace ByteBuoy.Domain.Entities.Config.Tasks
{
	public class FilesCopyConfig : TaskConfig
	{
		public bool OverWrite { get; set; }	
		public List<string> Sources { get; set; } = [];
		public List<string> Targets { get; set; } = [];
	}
}
