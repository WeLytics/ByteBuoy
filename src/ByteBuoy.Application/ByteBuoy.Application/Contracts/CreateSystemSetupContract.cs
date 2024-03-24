namespace ByteBuoy.Application.Contracts
{
	public class CreateSystemSetupContract
	{
		public string AdminEmail { get; set; } = null!;
		public string AdminPassword { get; set; } = null!;
		public string PageTitle { get; set; } = null!;
	}
}
