using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.ServiceInterfaces
{
	public class PageMetricBucketDto
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }

		public string Value { get; set; } = null!;	
		public MetricStatus Status { get; set; }

		public List<PageMetricDto> Metrics { get; set; } = [];
	}
}
