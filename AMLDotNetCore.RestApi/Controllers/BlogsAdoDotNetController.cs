using AMLDotNetCore.DataBase.Models;
using AMLDotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AMLDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=BlogManagementDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModels> lst = new List<BlogViewModels>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthot]
                          ,[BlogContent]
                          ,[DeleteFlag]
                      FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //Console.WriteLine(reader["BlogId"]);
                //Console.WriteLine(reader["BlogTitle"]);
                //Console.WriteLine(reader["BlogAuthot"]);
                //Console.WriteLine(reader["BlogContent"]);
                lst.Add(new BlogViewModels
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthot"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                });
            }
            connection.Close();
            return Ok(lst);
        }

        [HttpPost]

        public IActionResult CreateBlog( BlogViewModels model)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
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

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", model.Title);
            cmd.Parameters.AddWithValue("@BlogAuthot", model.Author);
            cmd.Parameters.AddWithValue("@BlogContent", model.Content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            return Ok(message);
        }

      
    }
}
