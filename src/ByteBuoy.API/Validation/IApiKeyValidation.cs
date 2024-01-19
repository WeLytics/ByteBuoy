namespace ByteBuoy.API.Validation
{
	public interface IApiKeyValidation
	{
		bool IsValidApiKey(string requestApiKey, string httpMethod);
	}
}
