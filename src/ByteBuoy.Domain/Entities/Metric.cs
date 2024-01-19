using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Domain.Entities
{
	public class Metric
	{
		public int Id { get; set; }
		public required Page Page { get; set; }

		public decimal? Value { get; set; }
		public required DateTime Created { get; set; }
		public DateTime? Updated { get; set; }
		public DateTime? Deleted { get; set; }


		public required MetricStatus Status { get; set; }	

		public int? MetricCategoryId { get; set; }
		public string? MetaJson { get; set; }


	}
}
