using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.Contracts
{
	public class UpdatePageMetricGroupContract
	{
		public string? Title { get; set; }
		public string? Description { get; set; }

		public string? GroupBy { get; set; }
		public MetricInterval? MetricInterval { get; set; }
	}
}
