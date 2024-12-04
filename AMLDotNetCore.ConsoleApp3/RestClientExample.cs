using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMLDotNetCore.ConsoleApp3
{
    public class RestClientExample
    {
        private readonly RestClient _client;
        private readonly string _postEndpoint = "https://jsonplaceholder.typicode.com/posts";

        public RestClientExample()
        {
            _client = new RestClient();
        }

        public async Task ReadAsync()
        {
            RestRequest request = new RestRequest(_postEndpoint,Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
                Console.WriteLine(jsonStr);
            }
        }

        public async Task EditAsync(int id)
        {
            RestRequest request = new RestRequest($"{_postEndpoint}/{id}",Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found!!");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }
        public async Task CreateAsync(int userId, string title, string body)
        {
            PostModel postModel = new PostModel()
            {
                title = title,
                body = body,
                userId = userId,
            };
            RestRequest request = new RestRequest(_postEndpoint, Method.Post);
            request.AddBody(postModel);
            var model = await _client.ExecuteAsync(request);
            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(model.Content!);
            }
        }

        public async Task updateAsync(int id, int userId, string title, string body)
        {
            PostModel postModel = new PostModel()
            {
                id = id,
                title = title,
                body = body,
                userId = userId,
            };
            RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Patch);
            request.AddBody(postModel);
            var model = await _client.ExecuteAsync(request);
            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(model.Content!);
            }
        }

        public async Task deleteAsync(int id)
        {
            RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Delete);

            var model = await _client.ExecuteAsync(request);
            if (model.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found!!");
                return;
            }
            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(model.Content!);
            }
        }

    }
}
