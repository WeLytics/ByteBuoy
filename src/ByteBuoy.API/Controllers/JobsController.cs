using ByteBuoy.API.Extensions;
using ByteBuoy.API.Models;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Mappers;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.API.Controllers
{

	[Route("api/v1/jobs")]
	[ApiExplorerSettings(GroupName = "V1")]
	[ApiController]
	public class JobsController(ByteBuoyDbContext context) : ControllerBase
	{
		private readonly ByteBuoyDbContext _context = context;

		// GET: api/v1/jobs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Job>>> GetJobs([FromQuery] QueryParameters queryParameters)
		{
			var query = _context.Jobs.AsQueryable();

			if (queryParameters!= null)
			{
				if (queryParameters.OrderBy.EndsWith("desc"))
				{
					queryParameters.OrderBy = queryParameters.OrderBy[..^4];
					queryParameters.OrderAsc = false;
				}

				var orderBy = queryParameters.OrderBy.Split(",", StringSplitOptions.RemoveEmptyEntries);
				foreach (var order in orderBy)
				{
					switch (order.ToLower().Trim())
					{
						case "finisheddatetime":
							if (queryParameters.OrderAsc)
							{
								query = query.OrderBy(r => r.FinishedDateTime);
							}
							else
							{
								query = query.OrderByDescending(r => r.FinishedDateTime);
							}
							break;
					}
				}
			}

			return await query.ToListAsync();	
		}

		// GET: api/v1/jobs/{jobId}
		[HttpGet("{jobId}")]
		public async Task<ActionResult<Job>> GetJob([FromRoute] int jobId)
		{
			var job = await _context.GetJobById(jobId);
			if (job == null)
				return NotFound();

			return Ok(job);
		}

		// DELETE api/v1/jobs/{jobId}
		[HttpDelete("{jobId}")]
		public async Task<ActionResult<Job>> DeleteJob([FromRoute] int jobId)
		{
			var job = await _context.GetJobById(jobId);
			if (job == null)
				return NotFound();

			_context.Jobs.Remove(job);
			await _context.SaveChangesAsync();	

			return Ok();
		}

		// POST: api/v1/jobs
		[HttpPost]
		public async Task<ActionResult<Job>> PostJob(CreateJobContract createJob)
		{
			var newJob = new JobContractMappers().CreateJobContractToJob(createJob);
			if (newJob.StartedDateTime  == null || newJob.StartedDateTime == DateTime.MinValue)
				newJob.StartedDateTime = DateTime.UtcNow;

			_context.Jobs.Add(newJob);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetJob), new { jobId = newJob.Id }, newJob);
		}


		// PUT: api/v1/jobs/{jobId}
		[HttpPut("{jobId}")]
		public async Task<ActionResult<Job>> UpdateJob(UpdateJobContract updateJob, [FromRoute] int jobId)
		{
			var job = await _context.Jobs.FindAsync(jobId);
			if (job == null)
				return NotFound();

			new JobContractMappers().UpdateJobDtoToJob(updateJob, job);

			await _context.SaveChangesAsync();

			return Ok(job);
		}


		// POST: api/v1/jobs/{jobId}/history
		[HttpPost("{jobId}/history")]
		public async Task<ActionResult<JobHistory>> PostJobHistory(CreateJobHistoryContract createJobHistory, [FromRoute] int jobId)
		{
			var job = await _context.Jobs.FindAsync(jobId);
			if (job == null)
				return NotFound();

			var newJobHistory = new JobHistoryContractMappers().CreateJobHistoryContractToDto(createJobHistory);
			newJobHistory.JobId = jobId;
			_context.JobHistory.Add(newJobHistory);
			await _context.SaveChangesAsync();

			return Ok(newJobHistory);
		}
	}
}
