using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMLDotNetCore.ConsoleApp3
{
    public class HttpClientExample
    {
        private readonly HttpClient _client;
        private readonly string postEndpoint = "https://jsonplaceholder.typicode.com/posts";

        public HttpClientExample()
        {
            _client = new HttpClient();
        }

        public async Task ReadAsync()
        {
            var response = await _client.GetAsync(postEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }

        public async Task EditAsync(int id)
        {
            var response = await _client.GetAsync($"{postEndpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found!!");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
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

            string jsonReq = JsonConvert.SerializeObject(postModel);
            var httpContent = new StringContent(jsonReq, Encoding.UTF8, Application.Json);
            var model = await _client.PostAsync(postEndpoint, httpContent);
            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(await model.Content.ReadAsStringAsync());
            }
        }

        public async Task updateAsync(int id, int userId, string title, string body)
        {
            PostModel postModel = new PostModel()
            {
                title = title,
                body = body,
                userId = userId,
            };

            string jsonReq = JsonConvert.SerializeObject(postModel);
            var httpContent = new StringContent(jsonReq, Encoding.UTF8, Application.Json);
            var model = await _client.PostAsync($"postEndpoint/{id}", httpContent);
            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(await model.Content.ReadAsStringAsync());
            }
        }

        public async Task deleteAsync(int id)
        {
            var model = await _client.DeleteAsync($"postEndpoint/{id}");
            if (model.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found!!");
                return;
            }
            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(await model.Content.ReadAsStringAsync());
            }
        }



    }

    public class PostModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
