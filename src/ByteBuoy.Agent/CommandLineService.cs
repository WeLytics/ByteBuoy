namespace ByteBuoy.Agent.Services
{
    internal class CommandLineService : ICommandLineService
    {
        public bool DryRun { get; }
        public string FilePath { get; } = "bytebuoy.yml";

        internal CommandLineService(string[]? args)
        {
            if (args == null)
                return;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--dryrun":
                    case "-d":
                        DryRun = true;
                        break;
                    case "--file":
                    case "-f":
                        if (i + 1 < args.Length)
                        {
                            FilePath = args[++i];
                        }
                        break;
                }
            }
        }

    }
}
