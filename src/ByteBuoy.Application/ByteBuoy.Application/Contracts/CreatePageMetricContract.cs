using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.Contracts
{
	public class CreatePageMetricContract
	{
		public string PageIdOrSlug { get; set; }
		public int? MetricCategoryId { get; set; }
		public MetricStatus Status { get; set; }
		public decimal? Value { get; set; }
		public string? MetaJson { get; set; }
	}
}
