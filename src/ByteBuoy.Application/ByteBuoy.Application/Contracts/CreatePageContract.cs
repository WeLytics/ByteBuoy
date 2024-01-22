using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.Contracts
{
	public class CreatePageContract
	{
		public string Title { get; set; } = null!;
		public string? Slug { get; set; }
	}
}
