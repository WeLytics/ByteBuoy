using System.Text.Json;
using ByteBuoy.Agent.Dtos;
using ByteBuoy.Application.Contracts;
using ByteBuoy.Domain;
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


		internal async Task<ApiResponse<T>> BaseRequest<T>(string endpoint, object payload, Method method)
		{
			var request = new RestRequest(endpoint, method);
			var jsonBody = JsonSerializer.Serialize(payload);
			request.AddJsonBody(jsonBody);


			try
			{
				var response = await _client.ExecuteAsync<T>(request);

				return new ApiResponse<T>
				{
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

		internal async Task<ApiResponse<CreateJobResponseDto>> CreateJobAsync(CreateJobContract contract)
		{
			var endpoint = $"/api/v1/jobs/";
			return await PostRequest<CreateJobResponseDto>(endpoint, contract);
		}

		internal async Task<ApiResponse<CreateJobResponseDto>> FinishJobAsync(UpdateJobContract contract)
		{
			var endpoint = $"/api/v1/jobs/";
			return await PutRequest<CreateJobResponseDto>(endpoint, contract);
		}
	}
}
