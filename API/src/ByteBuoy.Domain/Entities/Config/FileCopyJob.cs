namespace ByteBuoy.Domain.Entities.Config
{
    // Specific Job Types
    public class FileCopyJob : Job
    {
        public List<string> Sources { get; set; } = [];
        public List<string> Targets { get; set; } = [];

        public override void Execute()
        {
            // Implement file copying logic
            Console.WriteLine($"Executing FileCopy: {Description}");
        }
    }
}
