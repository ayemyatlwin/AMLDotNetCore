using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.ConsoleApp3
{
    public class RefitExample
    {
        public async Task Run()
        {
            var blogApi= RestService.For<IBlogApi>("https://localhost:7083");
            var model = await blogApi.GetBlogs();
            foreach (var blog in model)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogContent);
                Console.WriteLine(blog.BlogAuthot);
                Console.WriteLine("------------------------");

            }

            try
            {
                var item = await blogApi.EditBlog(1);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No data found.");
                }
            }

            var createBlog = await blogApi.CreateBlog(new BlogModel
            {
                BlogAuthot  ="Peter",
                BlogContent="Refit Example Content",
                BlogTitle="Refit",
                DeleteFlag=false,
            });

            var updateBlog = await blogApi.UpdateBlog(2,new BlogModel
            {
                BlogAuthot = "Julia",
                BlogContent = "Updated Content",
                BlogTitle = "Updated Title",
                DeleteFlag = false,
            });

            var deleteBlog = await blogApi.DeleteBlog(2);
        }
    }
}
