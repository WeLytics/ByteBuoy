using ByteBuoy.Domain.Entities.Config.Tasks;

namespace ByteBuoy.Domain.Entities.Config
{
	public class AgentConfig
    {
        public decimal Version { get; set; }
        public string Host { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string Page { get; set; } = null!;
		public string? Description { get; set; }
        public List<TaskConfig> Tasks { get; set; } = [];


        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Host))
                return false;

			if (Version <= 0)
				return false;

			if (string.IsNullOrWhiteSpace(ApiKey))
                return false;

			if (string.IsNullOrWhiteSpace(Page))
				return false;


			if (Tasks == null || Tasks.Count == 0)
                return false;

            return true;
        }
    }
}
