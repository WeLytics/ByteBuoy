using ByteBuoy.Application.Contracts;
using ByteBuoy.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ByteBuoy.Application.Mappers
{

	[Mapper]
	public partial class JobContractMappers
	{
		public partial Job CreateJobContractToJob(CreateJobContract dto);
		public partial void UpdateJobDtoToJob(UpdateJobContract dto, Job job);
	}
}
