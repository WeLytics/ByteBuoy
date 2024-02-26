using System.Text.Json;
using ByteBuoy.Agent.Dtos;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Domain;
using ByteBuoy.Domain.Entities;
using ByteBuoy.Domain.Entities.Config;
using RestSharp;

namespace ByteBuoy.Agent.Services
{
	internal class ApiService
	{
		private readonly RestClient _client;
		private readonly AgentConfig _agentConfig;

		internal ApiService(AgentConfig agentConfig)
		{
			_agentConfig = agentConfig ?? throw new ArgumentNullException(nameof(agentConfig));
			_client = new RestClient(_agentConfig.Host);
			_client.AddDefaultHeader(Constants.ApiKeyHeaderName, _agentConfig.ApiKey);
		}


		internal async Task<ApiResponse<T>> BaseRequest<T>(string endpoint, object? payload, Method method)
		{
			var request = new RestRequest(endpoint, method);

			if (payload != null)
			{
				var jsonBody = JsonSerializer.Serialize(payload);
				request.AddJsonBody(jsonBody);
			}

			try
			{
				var response = await _client.ExecuteAsync<T>(request);

				return new ApiResponse<T>
				{
					Response = response,
					Data = response.Data,
					IsSuccess = response.IsSuccessful,
					ErrorMessage = response.ErrorMessage
				};
			}
			catch (Exception ex)
			{
				return new ApiResponse<T>
				{
					IsSuccess = false,
					ErrorMessage = ex.Message
				};
			}

		}

		internal async Task<ApiResponse<T>> GetRequest<T>(string endpoint)
		{
			return await BaseRequest<T>(endpoint, null, Method.Get);
		}

		internal async Task<ApiResponse<T>> PostRequest<T>(string endpoint, object payload)
		{
			return await BaseRequest<T>(endpoint, payload, Method.Post);
		}

		internal async Task<ApiResponse<T>> PutRequest<T>(string endpoint, object payload)
		{
			return await BaseRequest<T>(endpoint, payload, Method.Put);
		}


		internal async Task<ApiResponse<CreatePageMetricContract>> PostPageMetric(CreatePageMetricContract payload)
		{
			var endpoint = $"/api/v1/pages/{_agentConfig.Page}/metrics";
			return await PostRequest<CreatePageMetricContract>(endpoint, payload);
		}

		internal async Task<ApiResponse<Job>> CreateJobAsync(CreateJobContract contract)
		{
			var endpoint = $"/api/v1/jobs/";
			return await PostRequest<Job>(endpoint, contract);
		}

		internal async Task<ApiResponse<JobHistory>> CreateJobHistoryAsync(CreateJobHistoryContract contract)
		{
			if (contract.JobId <= 0)
				throw new InvalidDataException("JobId not provided");

			var endpoint = $"/api/v1/jobs/{contract.JobId}/history";
			return await PostRequest<JobHistory>(endpoint, contract);
		}

		internal async Task<ApiResponse<UpdateJobResponseDto>> FinishJobAsync(UpdateJobContract contract)
		{
			var endpoint = $"/api/v1/jobs/{contract.JobId}";
			return await PutRequest<UpdateJobResponseDto>(endpoint, contract);
		}

		internal async Task<bool> IsHealthy()
		{
			var endpoint = $"/health";
			var response = await GetRequest<GetHealthDto>(endpoint);

			return response.Response?.IsSuccessStatusCode == true && response.Response?.Content == "OK";
		}
	}
}
