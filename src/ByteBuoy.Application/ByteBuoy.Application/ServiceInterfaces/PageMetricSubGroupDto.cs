using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.ServiceInterfaces
{
	public class PageMetricSubGroupDto
	{
		public string GroupTitle { get; set; } = null!;
		public string GroupValue { get; set; } = null!;
		public MetricStatus Status { get; set; }

		public List<PageMetricBucketDto> GroupByValues { get; set; } = [];
	}
}
