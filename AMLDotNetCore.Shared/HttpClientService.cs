using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMLDotNetCore.Shared
{
	public class HttpClientService : IHttpClientService
	{
		private readonly HttpClient _httpClient;

		public HttpClientService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<T> SendAsync<T>(string url, EnumHttpMethod method, object? data = null)
		{
			var request = new HttpRequestMessage(new HttpMethod(method.ToString()), url);
			if (data != null)
			{
				var jsonStr = JsonConvert.SerializeObject(data);
				request.Content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);

			}

			var response = await _httpClient.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				var jsonStr = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(jsonStr)!;
			}
			return default!;
		}

	}
}
