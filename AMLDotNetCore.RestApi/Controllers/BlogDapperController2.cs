using AMLDotNetCore.RestApi.DataModels;
using AMLDotNetCore.RestApi.ViewModels;
using AMLDotNetCore.Shared;
using Dapper;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AMLDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController2 : ControllerBase
    {
        private readonly string _connectionString;

        public BlogDapperController2(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
        }

        private readonly DapperService _dapperService;

        public BlogDapperController2()
        {
            _dapperService = new DapperService(_connectionString);
        }
        private bool BlogExists(int id)
        {
                string query = @"SELECT * FROM tbl_blog WHERE DeleteFlag = 0 AND BlogId = @BlogId";
                var blog = _dapperService.Query<BlogDataModel>(query, new { BlogId = id });
                return blog != null;
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog where DeleteFlag = 0;";
            List<BlogDataModel> lst = _dapperService.Query<BlogDataModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            if (!BlogExists(id))
            {
                return NotFound("Blog not found.");
            }

            string query = @"select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId";

            var item = _dapperService.Query<BlogDataModel>(query, new BlogDataModel
            {
                BlogId = id
            });

            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);



        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                    ([BlogTitle]
                    ,[BlogAuthot]
                    ,[BlogContent]
                    ,[DeleteFlag])
            VALUES
                    (@BlogTitle
                    ,@BlogAuthot
                    ,@BlogContent
                    ,0)";
            
                int result = _dapperService.Execute(query, new BlogDataModel
                {
                    BlogTitle = blog.BlogTitle,
                    BlogAuthot = blog.BlogAuthot,
                    BlogContent = blog.BlogContent
                });
                return Ok(result == 1 ? "Saving Successful " : "Saving failed");
            
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            if (!BlogExists(id))
            {
                return NotFound("Blog not found.");
            }

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                       ,[BlogAuthot] = @BlogAuthot
                       ,[BlogContent] = @BlogContent
                       ,[DeleteFlag] = 0
                  WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new BlogDataModel
            {
                BlogId = id,
                BlogTitle = blog.BlogTitle!,
                BlogAuthot = blog.BlogAuthot!,
                BlogContent = blog.BlogContent!,
            });
            return Ok(result == 1 ? "Updating Successful." : "Updating Fail");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {

            if (!BlogExists(id))
            {
                return NotFound("Blog not found.");
            }

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                            SET [DeleteFlag] = 1
                            WHERE BlogId = @BlogId";
            int result = _dapperService.Execute(query, new BlogDataModel { BlogId = id });

            return Ok(result == 0 ? "Deleting Blog Failed !" : "Successfully Deleted Blog");

        }
    }
}
