using ByteBuoy.Domain.Entities;

namespace ByteBuoy.Application.ServiceInterfaces
{
	public interface IMetricsConsolidationService
	{
		Task<PageMetricConsolidationDto> ConsolidateMetricsAsync(Page page);
	}
}
