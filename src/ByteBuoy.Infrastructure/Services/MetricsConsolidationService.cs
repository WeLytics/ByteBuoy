using System.Text.Json;
using ByteBuoy.Application.ServiceInterfaces;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Enums;
using ByteBuoy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ByteBuoy.Infrastructure.Services
{
	public class MetricsConsolidationService(ByteBuoyDbContext dbContext) : IMetricsConsolidationService
	{
		private readonly ByteBuoyDbContext _dbContext = dbContext;
		private readonly MetricsConsolidationMappers _mappers = new();

		public async Task<PageMetricConsolidationDto> ConsolidateMetricsAsync(Page page)
		{
			var result = new PageMetricConsolidationDto();

			var metricGroups = await _dbContext.MetricGroups.Where(r => r.Page == page).ToListAsync();
			{
				foreach (var metricGroup in metricGroups)
				{
					result.MetricsGroups.Add(await LoadMetricGroupAsync(page, metricGroup));
				}
			}

			return result;
		}

		internal async Task<PageMetricGroupDto> LoadMetricGroupAsync(Page page, MetricGroup metricGroup)
		{
			ArgumentNullException.ThrowIfNull(metricGroup);

			var result = new PageMetricGroupDto();

			var metricsFilter = GetDateFilterLimit(page.Created, metricGroup.MetricInterval);

			var metrics = await _dbContext.Metrics.Where(r => r.MetricGroup == metricGroup && r.Created >= metricsFilter)
												   .OrderByDescending(r => r.Created)
												   .ToListAsync();

			_mappers.MetricGroupToPageMetricGroupDto(metricGroup, result);
			result.BucketValues.AddRange(GenerateBucketMapping(metricGroup, metricsFilter, metrics));
			result.SubGroups.AddRange(GenerateSubGroups(metricGroup, metricsFilter, metrics));

			return result;
		}

		private class LabelCache
		{
			public string GroupTitle { get; set; } = null!;
			public string? GroupValue { get; set; }
			public List<Metric> Metrics { get; set; } = [];
		}

		private List<PageMetricSubGroupDto> GenerateSubGroups(MetricGroup metricGroup, DateTime metricsFilter, List<Metric> metrics)
		{
			var result = new List<PageMetricSubGroupDto>();

			if (string.IsNullOrEmpty(metricGroup.GroupBy))
				return result;

			var labelCache = new List<LabelCache>();
			var labelName = metricGroup.GroupBy.Replace("label:", "");

			foreach (var metric in metrics)
			{
				if (string.IsNullOrEmpty(metric.MetaJson))
					continue;

				JsonDocument? jsonDoc = null;

				try
				{
					jsonDoc = JsonDocument.Parse(metric.MetaJson);
				}
				catch (JsonException e)
				{
					Console.WriteLine($"Invalid JSON: {e.Message}");
					throw;
				}
				catch (Exception e)
				{
					Console.WriteLine($"Unexpected error: {e.Message}");
					throw;
				}


				var root = jsonDoc!.RootElement;
				var labelNode = root.GetProperty("labels");


				var labels = labelNode.EnumerateObject();
				foreach (var label in labels)
				{
					if (label.Name == labelName)
					{
						var value = metric.ValueString ?? metric.Value.ToString();
						var subGroup = labelCache.SingleOrDefault(r => r.GroupTitle == label.Value.GetString() &&
																   r.GroupValue == value);

						if (subGroup == null)
						{
							subGroup = new LabelCache
							{
								GroupTitle = label.Value.GetString()!,
								GroupValue = value
							};
							labelCache.Add(subGroup);
						}
						subGroup.Metrics.Add(metric);
					}
				}
			}

			foreach (var subGroup in labelCache)
			{
				var mapping = GenerateBucketMapping(metricGroup, metricsFilter, subGroup.Metrics);
				result.Add(new PageMetricSubGroupDto
				{
					GroupTitle = subGroup.GroupTitle,
					GroupValue = subGroup.GroupValue,
					GroupByValues = mapping
				});
			}

			return result;
		}

		private List<PageMetricBucketDto> GenerateBucketMapping(MetricGroup metricGroup, DateTime metricsFilter, List<Metric> metrics)
		{
			List<PageMetricBucketDto> result;

			// generate buckets based on the metric interval
			result = GenerateBuckets(metricsFilter, DateTime.UtcNow, metricGroup.MetricInterval);

			// iterate through each bucket and add the filtered metrics
			foreach (var bucket in result)
			{
				var bucketMetrics = metrics.Where(r => r.Created >= bucket.Start && r.Created < bucket.End).ToList();
				foreach (var metric in bucketMetrics)
				{
					var metricCopy = _mappers.MetricToPageMetricDto(metric);
					if (metricCopy.Id == 0)
						metricCopy.Id = bucketMetrics.IndexOf(metric);
					bucket.Metrics.Add(metricCopy);
				}
			}

			// calculate bucket status based on bucket value metrics
			foreach (var bucket in result)
			{
				bucket.Status = CalculateBucketStatus(bucket);
			}
			return result;
		}

		private static MetricStatus CalculateBucketStatus(PageMetricBucketDto bucket)
		{
			if (bucket.Metrics.Count == 0)
				return MetricStatus.NoData;

			if (bucket.Metrics.Any(r => r.Status == MetricStatus.Error))
				return MetricStatus.Error;

			if (bucket.Metrics.Any(r => r.Status == MetricStatus.Warning))
				return MetricStatus.Warning;

			return MetricStatus.Success;
		}

		private static List<PageMetricBucketDto> GenerateBuckets(DateTime start, DateTime utcNow, MetricInterval metricInterval)
		{
			var result = new List<PageMetricBucketDto>();
			var end = utcNow;
			if (metricInterval == MetricInterval.Hour)
			{
				while (start < end)
				{
					result.Add(new PageMetricBucketDto
					{
						Start = start,
						End = start.AddHours(1),
						Value = start.Date.ToString()
					});
					start = start.AddHours(1);
				}
			}
			else if (metricInterval == MetricInterval.Day)
			{
				start = start.Date;
				end = end.Date.AddDays(1).AddMicroseconds(-1);
				while (start < end)
				{
					result.Add(new PageMetricBucketDto
					{
						Start = start,
						End = start.AddDays(1).AddMicroseconds(-1),
						Value = start.Date.ToShortDateString()
					});
					start = start.AddDays(1);
				}
			}
			else if (metricInterval == MetricInterval.Week)
			{
				while (start < end)
				{
					result.Add(new PageMetricBucketDto
					{
						Start = start,
						End = start.AddDays(7),
						Value = $"{start.Date} - {end.Date}"
					});
					start = start.AddDays(7);
				}
			}
			else if (metricInterval == MetricInterval.Month)
			{
				while (start < end)
				{
					result.Add(new PageMetricBucketDto
					{
						Start = start,
						End = start.AddMonths(1),
						Value = $"{start.Date.Month} {start.Date.Year}"
					});
					start = start.AddMonths(1);
				}
			}
			else if (metricInterval == MetricInterval.Year)
			{
				while (start < end)
				{
					result.Add(new PageMetricBucketDto
					{
						Start = start,
						End = start.AddYears(1),
						Value = $"{start.Date.Year}"
					});
					start = start.AddYears(1);
				}
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

		private static DateTime GetCurrentBucketDateLimit(MetricInterval metricInterval)
		{
			var start = DateTime.UtcNow;
			if (metricInterval == MetricInterval.Hour)
				return start.AddHours(-1);
			else if (metricInterval == MetricInterval.Day)
				return start.Date;
			else if (metricInterval == MetricInterval.Week)
				return start.Date.AddDays(-7);
			else if (metricInterval == MetricInterval.Month)
				return start.Date.AddMonths(-1);
			else if (metricInterval == MetricInterval.Year)
				return start.Date.AddYears(-1);

			throw new InvalidOperationException("Invalid metric interval");
		}

		private async Task<MetricStatus> GetCurrentPageStatus(Page page)
		{
			var metrics = await _dbContext.Metrics.Where(r => r.Page == page &&
														 r.Created > DateTime.UtcNow.AddHours(-24))
							.Include(r => r.MetricGroup)
							.ToListAsync();

			var metricFilterDate = DateTime.UtcNow.AddSeconds(-1);
			MetricStatus? result = null;

			var groupedMetrics = metrics
								.GroupBy(m => m.MetricGroup)
								.Select(g => g.Key!)
								.ToList();

			foreach (var metricGroup in groupedMetrics)
			{
				var metricsFilter = GetCurrentBucketDateLimit(metricGroup!.MetricInterval);
				var metricsFiltered = metrics.Where(r => r.MetricGroup == metricGroup && r.Created >= metricsFilter).ToList();

				if (metricsFiltered.Count == 0)
				{
					result = MetricStatus.NoData;
					continue;	
				}

				if (metricsFiltered.Any(r => r.Status == MetricStatus.Error))
					return MetricStatus.Error;

				if (metricsFiltered.Any(r => r.Status == MetricStatus.Warning))
					return MetricStatus.Warning;
			}

			return result ?? MetricStatus.Success;
		}

		public async Task UpdatePageStatus(Page page)
		{
			var status = await GetCurrentPageStatus(page);
			page.PageStatus = status;
			await _dbContext.SaveChangesAsync();
		}
	}
}
