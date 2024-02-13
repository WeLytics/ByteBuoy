using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.ServiceInterfaces
{
	public class PageMetricGroupDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string? Description { get; set; }

		public string? GroupBy { get; set; }

		public MetricInterval? MetricInterval { get; set; }
		public MetricStatus GroupStatus { get; set; }
		public List<PageMetricBucketDto> BucketValues { get; set; } = [];
		public List<PageMetricSubGroupDto> SubGroups { get; set; } = [];
	}
}
