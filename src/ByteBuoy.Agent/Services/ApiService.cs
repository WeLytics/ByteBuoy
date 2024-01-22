using ByteBuoy.Agent.JobExecution.JobActions;
using ByteBuoy.Domain;
using RestSharp;

namespace ByteBuoy.Agent.Services
{
	internal class ApiService
	{
		private readonly RestClient _client;

		internal ApiService(string host, string apiKey)
		{
			_client = new RestClient(host);
			_client.AddDefaultHeader(Constants.ApiKeyHeaderName, apiKey);
		}

		internal async Task<ApiResponse<T>> PostJobInfo<T>(IJobAction jobAction)
		{
			var request = new RestRequest(jobAction.ApiEndpoint, Method.Get); // Oder Method.Post, je nach Job

			// Fügen Sie spezifische Parameter oder Header hinzu, die auf den Job zutreffen
			// Beispiel: request.AddParameter("name", job.Parameter);

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
			catch (System.Exception ex)
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
