using System.Linq.Expressions;
using ByteBuoy.API.Extensions;
using ByteBuoy.API.Models;
using ByteBuoy.API.Utilities;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Application.Mappers;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
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
		public async Task<ActionResult<PagedList<Job>>> GetJobs([FromQuery] QueryParameters queryParameters)
		{
			var query = _context.Jobs.AsQueryable();

			// Create a mapping of property names to expressions
			var columnsMap = new Dictionary<string, Expression<Func<Job, object>>>
			{
				["finishedDateTime"] = v => v.FinishedDateTime!,
				["startedDateTime"] = v => v.StartedDateTime!
			};

			query = query.ApplySort(queryParameters.OrderBy, columnsMap);
			int totalRecords = await query.CountAsync();

			// Apply paging
			query = query.ApplyPaging(queryParameters.Page, queryParameters.PageSize);

			// Calculate total pages
			int totalPages = (int)Math.Ceiling(totalRecords / (double)queryParameters.PageSize);
			var paginationMeta = PaginationMetaBuilder.Build(queryParameters.Page, queryParameters.PageSize, totalRecords, totalPages);
			var items = await query.ToListAsync();

			return new PagedList<Job>(items, totalPages, queryParameters.Page, queryParameters.PageSize);	
		}

		// GET: api/v1/jobs/{jobId}
		[HttpGet("{jobId}")]
		public async Task<ActionResult<Job>> GetJob([FromRoute] int jobId)
		{
			var job = await _context.Jobs
							.Include(r => r.JobHistory)
							.SingleOrDefaultAsync(r => r.Id == jobId);
			if (job == null)
				return NotFound();

			return Ok(job);
		}

		// DELETE api/v1/jobs/{jobId}
		[HttpDelete("{jobId}")]
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
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
			if (newJobHistory.CreatedDateTime == DateTime.MinValue)
				newJobHistory.CreatedDateTime = DateTime.UtcNow;

			_context.JobHistory.Add(newJobHistory);
			await _context.SaveChangesAsync();

			return Ok(newJobHistory);
		}
	}
}
