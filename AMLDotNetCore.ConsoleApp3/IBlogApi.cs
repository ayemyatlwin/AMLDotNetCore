using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.ConsoleApp3
{
    public  interface IBlogApi
    {
        [Get("/api/blogs")]
        Task<List<BlogModel>> GetBlogs();

        [Get("/api/blogs/{id}")]
        Task<BlogModel> EditBlog(int id);

        [Post("/api/blogs")]
        Task<BlogModel> CreateBlog(BlogModel model);

        [Put("/api/blogs/{id}")]
        Task<BlogModel> UpdateBlog(int id, BlogModel model);

        [Delete("/api/blogs/{id}")]
        Task<BlogModel> DeleteBlog(int id);
    }

    public  class BlogModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthot { get; set; } = null!;

        public string BlogContent { get; set; } = null!;

        public bool DeleteFlag { get; set; }
    }
}
