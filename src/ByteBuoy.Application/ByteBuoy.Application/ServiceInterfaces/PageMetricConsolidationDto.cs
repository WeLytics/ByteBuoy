using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Enums;

namespace ByteBuoy.Application.ServiceInterfaces
{
	public class PageMetricConsolidationDto
	{
		public List<PageMetricGroupDto> MetricGroups { get; set; } = [];
	}
}
