using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AMLDotNetCore.RestApi.ViewModels;
using AMLDotNetCore.RestApi.DataModels;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AMLDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=BlogManagementDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        private bool BlogExists(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM tbl_blog WHERE DeleteFlag = 0 AND BlogId = @BlogId";
                var blog = db.Query<BlogDataModel>(query, new { BlogId = id }).FirstOrDefault();
                return blog != null;
            }
        }

        [HttpGet]
        public IActionResult GetBlog()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0;";
                var lst = db.Query<BlogDataModel>(query).ToList();
                return Ok(lst);
            }
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel model)
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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                int result = db.Execute(query, new BlogViewModels
                {
                    //Id = model.BlogId,
                    Author = model.BlogAuthot,
                    Content = model.BlogContent,
                    Title = model.BlogTitle,

                });
                string message = result == 1 ? "Saving Successful." : "Saving Failed.";
                return Ok(message);

            }

        }

        [HttpPut("{id}")]

        public IActionResult UpdateBlog(int id,BlogViewModels model)
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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = id,
                    BlogAuthot = model.Author,
                    BlogContent = model.Content,
                    BlogTitle = model.Title,

                });
                string message = result == 1 ? "Updating Successful." : "Updating Failed.";
                return Ok(message);


            }

        }

        [HttpPatch("{id}")]

        public IActionResult PatchBlog(int id, BlogViewModels model)
        {

            if (!BlogExists(id))
            {
                return NotFound("Blog not found.");
            }

            string conditions = "";
            if (!string.IsNullOrEmpty(model.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(model.Author))
            {
                conditions += " [BlogAuthot] = @BlogAuthot, ";
            }
            if (!string.IsNullOrEmpty(model.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = id,
                    BlogAuthot = model.Author,
                    BlogContent = model.Content,
                    BlogTitle = model.Title,

                });
                string message = result == 1 ? "Updating Successful." : "Updating Failed.";
                return Ok(message);

            }

        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            if (!BlogExists(id))
            {
                return NotFound("Blog not found.");
            }
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId";
                var item = db.Query<BlogDataModel>(query, new BlogDataModel
                {
                    BlogId = id,
                }).FirstOrDefault();
                if (item is null)
                {
                    return BadRequest("There is no matching data...");
                }
                return Ok(item);

            }
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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = id
                });

                string message = result == 1 ? "Deleting Successful." : "Deleting Fail.";
                return Ok(message);


            }
        }

    }
}
