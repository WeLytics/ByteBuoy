using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Domain.Entities
{
	public class Metric
	{
		public int Id { get; set; }
		public Page Page { get; set; } = null!;

		public decimal? Value { get; set; }
		public string? ValueString { get; set; }
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public DateTime? Updated { get; set; }
		public DateTime? Deleted { get; set; }


		public MetricStatus Status { get; set; }

		public MetricGroup MetricGroup { get; set; } = null!;
		public string? MetaJson { get; set; }


	}
}
