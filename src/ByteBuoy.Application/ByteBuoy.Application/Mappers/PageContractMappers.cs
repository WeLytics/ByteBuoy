using ByteBuoy.Application.Contracts;
using ByteBuoy.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ByteBuoy.Application.Mappers
{

	[Mapper]
	public partial class PageContractMappers
	{
		public partial Page CreatePageDtoToPage(CreatePageContract dto);
		public partial Metric CreatePageMetricDtoToPageMetric(CreatePageMetricContract dto);
	}
}
