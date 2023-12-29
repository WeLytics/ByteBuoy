namespace ByteBuoy.Domain.Entities.Config
{
    public abstract class Job
    {
        public string? Description { get; set; }
        public List<Dictionary<string, string>>? Labels { get; set; }
        public abstract void Execute(); // Abstract execute method for job-specific behavior
    }
}
