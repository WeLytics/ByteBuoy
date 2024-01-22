using ByteBuoy.Domain;
using ByteBuoy.Domain.Entities.ApiKey;

namespace ByteBuoy.API.Validation
{
	public class ApiKeyValidation : IApiKeyValidation
	{
		private readonly IConfiguration _configuration;

		public ApiKeyValidation(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public bool IsValidApiKey(string requestApiKey, string httpMethod)
		{
			if (string.IsNullOrWhiteSpace(requestApiKey))
				return false;

			var apiKey = _configuration.GetSection(Constants.ApiKeyName).Get<IEnumerable<ApiKey>>();

			if (apiKey == null || !apiKey.Any(r => r.Key.Equals(requestApiKey)))
				return false;

			if ((httpMethod.Equals("POST") || httpMethod.Equals("PUT") || httpMethod.Equals("DELETE")) &&
				apiKey.Any(r => r.Key.Equals(requestApiKey) && r.Permission.Equals("ReadOnly")))
				return false;

			return true;
		}
	}
}
