using ByteBuoy.Application.Contracts;
using ByteBuoy.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ByteBuoy.Application.Mappers
{

	[Mapper]
	public partial class JobHistoryContractMappers
	{
		public partial JobHistory CreateJobHistoryContractToDto(CreateJobHistoryContract dto);
	}
}
