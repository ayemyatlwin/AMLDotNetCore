using AMLDotNetCore.DataBase.Models;
using AMLDotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;

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


        [HttpGet("id")]

        public IActionResult GetBlogById(int id)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
                   ,[BlogTitle]
                   ,[BlogAuthot]
                   ,[BlogContent]
                   ,[DeleteFlag]
               FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                string message = " There is no matching data !";
                return BadRequest(message);
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogViewModels()
            {
                Id = Convert.ToInt32(dr["BlogId"]),
                Title = Convert.ToString(dr["BlogTitle"]),
                Author = Convert.ToString(dr["BlogAuthot"]),
                Content = Convert.ToString(dr["BlogContent"])
            };
            return Ok(item);
        }


        [HttpPut("id")]
        public IActionResult UpdateBlog (int id ,BlogViewModels model)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                       ,[BlogAuthot] = @BlogAuthot
                       ,[BlogContent] = @BlogContent
                       ,[DeleteFlag] = 0
                  WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", model.Title);
            cmd.Parameters.AddWithValue("@BlogAuthot", model.Author);
            cmd.Parameters.AddWithValue("@BlogContent", model.Content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            return Ok(message);

        }

        [HttpPatch("id")]

        public IActionResult PatchBlog(int id , BlogViewModels blog)
        {
            string conditions = "";
            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthot] = @BlogAuthot, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters!");
            }

            conditions= conditions.Substring(0, conditions.Length - 2);

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query,connection);

            cmd.Parameters.AddWithValue("@BlogId", id);

            if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);

            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthot", blog.Author);

            }

            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            }

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");





        }

        [HttpDelete("id")]

        public IActionResult DeletePost(int id)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                            SET [DeleteFlag] = 1
                            WHERE BlogId = @id";

            //string query = @"DELETE FROM [dbo].[Tbl_Blog]
            //    WHERE BlogId = @id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result == 1 ? "Delete Successful." : "Delete Failed.";

            return Ok(message);


        }


    }
}
