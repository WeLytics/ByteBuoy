namespace ByteBuoy.Domain.Entities.Config
{
    public class CheckHashJob : Job
    {
        public List<string> Paths { get; set; } = [];

        public override void Execute()
        {
            // Implement check hash logic
            Console.WriteLine($"Executing CheckHash: {Description}");
        }
    }
}
