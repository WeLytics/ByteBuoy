using ByteBuoy.Application.ServiceInterfaces;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Enums;
using ByteBuoy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

namespace ByteBuoy.Infrastructure.Services
{
	public class MetricsConsolidationService(ByteBuoyDbContext dbContext) : IMetricsConsolidationService
	{
		private readonly ByteBuoyDbContext _dbContext = dbContext;

		public async Task<PageMetricConsolidationDto> ConsolidateMetricsAsync(Page page)
		{
			var result = new PageMetricConsolidationDto();

			var metricGroups = await _dbContext.MetricGroups.Where(r => r.Page == page).ToListAsync();
			{
				foreach (var metricGroup in metricGroups)
				{
					result.MetricGroups.Add(await LoadMetricGroupAsync(page, metricGroup));
				}
			}

			return result;
		}

		internal async Task<PageMetricGroupDto> LoadMetricGroupAsync(Page page, MetricGroup metricGroup)
		{
			ArgumentNullException.ThrowIfNull(metricGroup);
			var result = new PageMetricGroupDto();
			var mappers = new MetricsConsolidationMappers();
			var metricsFilter = GetDateFilterLimit(page.Created, metricGroup.MetricInterval);

			var metrics = await _dbContext.Metrics.Where(r => r.MetricGroup == metricGroup && r.Created >= metricsFilter)
												   .OrderBy(r => r.Created)
												   .ToListAsync();

			mappers.MetricGroupToPageMetricGroupDto(metricGroup,result);

			foreach (var metric in metrics)
			{
				result.Metrics.Add(mappers.MetricToPageMetricDto(metric));
			}

			return result;
		}


		private static DateTime GetDateFilterLimit(DateTime pageCreated, MetricInterval metricInterval)
		{
			var result = metricInterval switch
			{
				MetricInterval.Hour => DateTime.UtcNow.AddHours(-24),
				MetricInterval.Day => DateTime.UtcNow.AddDays(-30),
				MetricInterval.Week => DateTime.UtcNow.AddDays(-7 * 10),
				MetricInterval.Month => DateTime.UtcNow.AddMonths(-1 * 12),
				MetricInterval.Year => DateTime.UtcNow.AddYears(-1 * 10),
				_ => throw new ArgumentOutOfRangeException(nameof(metricInterval)),
			};

			if (result < pageCreated)
				result = pageCreated;

			return result;
		}
	}
}
