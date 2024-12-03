using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.ConsoleApp3
{
    public class HttpClientExample
    {
        private readonly HttpClient client;
        private readonly string postEndpoint = "https://jsonplaceholder.typicode.com/posts";

        public HttpClientExample()
        {
            client = new HttpClient();
        }

        public async Task ReadAsync()
        {
            var response = await client.GetAsync(postEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }
    }
}
