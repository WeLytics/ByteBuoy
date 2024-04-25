using RestSharp;

namespace ByteBuoy.Core.Services
{
	internal class ApiResponse<T>
	{
		public T? Data { get; set; }
		public bool IsSuccess { get; set; }
		public string? ErrorMessage { get; set; }
		public RestResponse? Response { get; internal set; }
	}
}
