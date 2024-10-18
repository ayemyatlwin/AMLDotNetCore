using AMLDotNetCore.TaskManagementRestAPI.DataModels;
using AMLDotNetCore.TaskManagementRestAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace AMLDotNetCore.TaskManagementRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class taskCategoryController : ControllerBase
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=TaskManagementDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        private bool CategoryExists(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM tbl_TaskCategory WHERE DeleteFlag = 0 AND CategoryID = @CategoryID";
                var category = db.Query<CategoryDataModel>(query, new CategoryDataModel { CategoryID=id}).FirstOrDefault();
                return category != null;
            }
        }

        #region readAllCategory
        [HttpGet]
        public IActionResult GetTaskCategoryList()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [CategoryID]
                                 ,[CategoryName]
                                 ,[DeleteFlag]
                                 FROM [dbo].[Tbl_TaskCategory] where DeleteFlag=0";
                var lst = db.Query(query).ToList();
                return Ok(lst);


            }

        }

        #endregion readAllCategory

        #region createCategory

        [HttpPost]
        public IActionResult CreateTaskCategory(CategoryDataModel model)
        {
            string query = @"INSERT INTO [dbo].[Tbl_TaskCategory]
           ([CategoryName]
           ,[DeleteFlag])
            VALUES
           (@CategoryName
           ,0);";
            using (IDbConnection db= new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new CategoryViewModel
                {
                    CategoryName = model.CategoryName

                });
                string message = result == 1 ? "Successully created new category!" : "Failed to create category!";
                return Ok(message);
            }
        }

        #endregion createCategory

        #region getCategoryByID
        [HttpGet("{id}")]

        public IActionResult GetCategoryByID(int id)
        {
            if (!CategoryExists(id))
            {
                return NotFound("Category Id not found!");
            }
            string query = @"SELECT [CategoryID]
                            ,[CategoryName]
                            ,[DeleteFlag]
                            FROM [dbo].[Tbl_TaskCategory] where DeleteFlag = 0 and CategoryID = @CategoryID";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var item = db.Query(query,new CategoryDataModel { CategoryID=id}).FirstOrDefault();
                if (item is null)
                {
                    return BadRequest("There is no matching data...");
                }
                return Ok(item);
            }

        }

        #endregion getCategoryByID

        #region updateCategory

        [HttpPut("{id}")]

        public IActionResult UpdateCategory(int id, CategoryDataModel model) 
        {
            if (!CategoryExists(id))
            {
                NotFound(" Category ID not found!");
            }
            string query = @"UPDATE [dbo].[Tbl_TaskCategory]
                            SET [CategoryName] = @CategoryName
                            ,[DeleteFlag] = 0
                            WHERE CategoryID=@CategoryID";
            using (IDbConnection db=new SqlConnection(_connectionString) )
            {
                var result = db.Execute(query, new CategoryDataModel
                {
                    CategoryID = id,
                    CategoryName = model.CategoryName
                });
                string message = result == 1 ? "Successully updated category!" : "Failed to update category!";
                return Ok(message);

            }
        }


        #endregion updateCategory

        #region deleteCategory

        [HttpDelete("{id}")]

        public IActionResult DeleteCategory(int id) 
        {
            if (!CategoryExists(id))
            {
                return NotFound("Category ID not found.");
            }
            string query = @"UPDATE [dbo].[Tbl_TaskCategory]
                            SET [DeleteFlag] = 1
                            WHERE CategoryID = @CategoryID";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new CategoryDataModel
                {
                    CategoryID = id
                });

                string message = result == 1 ? "Successfully deleted category!" : "Failed to delete!.";
                return Ok(message);


            }

        }

        #endregion deleteCategory

    }
}
