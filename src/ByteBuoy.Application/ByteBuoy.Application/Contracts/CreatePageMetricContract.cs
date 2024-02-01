using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.Contracts
{
	public class CreatePageMetricContract
	{
		public int? MetricGroupId { get; set; }
		public MetricStatus Status { get; set; }
		public decimal? Value { get; set; }
		public string? ValueString { get; set; }
		public string? MetaJson { get; set; }
	}
}
