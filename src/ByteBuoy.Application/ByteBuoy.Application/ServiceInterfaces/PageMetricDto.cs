using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.ServiceInterfaces
{
	public class PageMetricDto
	{
		public int Id { get; set; }
		public decimal? Value { get; set; }
		public string? ValueString { get; set; }
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public MetricStatus Status { get; set; }

		public string? MetaJson { get; set; }

	}
}
