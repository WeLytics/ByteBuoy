using Riok.Mapperly.Abstractions;
using ByteBuoy.Application.ServiceInterfaces;
using ByteBuoy.Domain.Entities;

namespace ByteBuoy.Infrastructure.Services
{

	[Mapper]
	internal partial class MetricsConsolidationMappers
	{
		internal partial void MetricGroupToPageMetricGroupDto(MetricGroup metricGroup, PageMetricGroupDto dto);
		internal partial PageMetricDto MetricToPageMetricDto(Metric metric);
	}
}
