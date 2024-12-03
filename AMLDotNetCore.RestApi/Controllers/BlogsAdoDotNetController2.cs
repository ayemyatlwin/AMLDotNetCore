using AMLDotNetCore.DataBase.Models;
using AMLDotNetCore.RestApi.DataModels;
using AMLDotNetCore.RestApi.ViewModels;
using AMLDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AMLDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController2 : ControllerBase
    {
        private readonly string _connectionString;

        public BlogsAdoDotNetController2(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
        }

        private readonly AdoDotNetService _adoDotNetService;

        public BlogsAdoDotNetController2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModels> lst = new List<BlogViewModels>();
            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthot]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {

                lst.Add(new BlogViewModels
                {
                    Id = Convert.ToInt32(dr["BlogId"]),
                    Title = Convert.ToString(dr["BlogTitle"]),
                    Author = Convert.ToString(dr["BlogAuthot"]),
                    Content = Convert.ToString(dr["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"]),
                });
            }
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogId(int id)
        {

            string query = @"SELECT [BlogId]
                   ,[BlogTitle]
                   ,[BlogAuthot]
                   ,[BlogContent]
                   ,[DeleteFlag]
               FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";
            var dt = _adoDotNetService.Query(query,
            new SqlParameterModel("@BlogId", id));

            if (dt.Rows.Count == 0)
            {
                return NotFound();

            }
            DataRow dr = dt.Rows[0];
            var item = new BlogViewModels
            {
                Id = Convert.ToInt32(dr["BlogId"]),
                Title = Convert.ToString(dr["BlogTitle"]),
                Author = Convert.ToString(dr["BlogAuthot"]),
                Content = Convert.ToString(dr["BlogContent"]),
                DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"]),
            };

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
            int result = _adoDotNetService.Execute(query,
            new SqlParameterModel("@BlogTitle", blog.BlogTitle),
            new SqlParameterModel("@BlogAuthot", blog.BlogAuthot),
            new SqlParameterModel("@BlogContent", blog.BlogContent));

            return Ok(result == 1 ? "Saving Successful " : "Saving faileds");

        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModels blog)
        {


            string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                       ,[BlogAuthot] = @BlogAuthot
                       ,[BlogContent] = @BlogContent
                       ,[DeleteFlag] = 0
                  WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query,
           new SqlParameterModel("@BlogId", id),
           new SqlParameterModel("@BlogTitle", blog.Title!),
           new SqlParameterModel("@BlogAuthot", blog.Author!),
           new SqlParameterModel("@BlogContent", blog.Content!));

            return Ok(result == 1 ? "Updating Successful" : "Updating Failed");

        }

      


        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                            SET [DeleteFlag] = 1
                            WHERE BlogId = @id";

            int result = _adoDotNetService.Execute(query,
             new SqlParameterModel("@id", id));

            return Ok(result == 0 ? "Deleting Blog Failed! " : "Successfully Deleted Blog");
        }


    }
}
