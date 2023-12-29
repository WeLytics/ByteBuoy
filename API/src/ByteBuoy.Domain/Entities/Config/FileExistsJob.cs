namespace ByteBuoy.Domain.Entities.Config
{
    // Specific Job Types
    public class FileExistsJob : Job
    {
        public List<string> Sources { get; set; } = [];
        public List<string> Targets { get; set; } = [];

        public override void Execute()
        {
            
            Console.WriteLine($"Executing FileExistsJob: {Description}");
        }
    }
}
