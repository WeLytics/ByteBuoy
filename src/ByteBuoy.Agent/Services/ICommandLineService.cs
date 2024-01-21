namespace ByteBuoy.Agent.Services
{
    internal interface ICommandLineService
    {
        bool DryRun { get; }
        string FilePath { get; }
    }

}