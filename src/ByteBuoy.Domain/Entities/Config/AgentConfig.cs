using ByteBuoy.Domain.Entities.Config.Jobs;

namespace ByteBuoy.Domain.Entities.Config
{
	public class AgentConfig
    {
        public int Version { get; set; }
        public string Host { get; set; } = null!;
        public string Apikey { get; set; } = null!;
        public List<JobConfig> Jobs { get; set; } = [];


        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Host))
                return false;

            if (string.IsNullOrWhiteSpace(Apikey))
                return false;

            if (Jobs == null || Jobs.Count == 0)
                return false;

            return true;
        }
    }
}
