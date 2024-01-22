using System.Text.Json;
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

		internal async Task<ApiResponse<T>> PostPageMetric<T>(T payload)
		{
			var endpoint = $"/api/v1/pages/{_agentConfig.PageId}/metrics";
			var request = new RestRequest(endpoint, Method.Post);
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
	}
}
