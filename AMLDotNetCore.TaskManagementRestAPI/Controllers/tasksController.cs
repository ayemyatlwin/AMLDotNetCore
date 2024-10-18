using AMLDotNetCore.TaskManagementRestAPI.DataModels;
using AMLDotNetCore.TaskManagementRestAPI.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AMLDotNetCore.TaskManagementRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tasksController : ControllerBase
    {
        public readonly string _connectionString = "Data Source=.;Initial Catalog=TaskManagementDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        private static readonly List<string> allowedStatusList = new List<string> { "Pending", "In Progress", "Completed", "Overdue" };
        private bool TaskExists(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM tbl_ToDoList WHERE DeleteFlag = 0 AND TaskID = @TaskID";
                var result = db.Query<TasksDataModel>(query, new TasksDataModel { TaskID = id }).FirstOrDefault();
                return result != null;
            }
        }

        #region getAllTask
        [HttpGet]
        public IActionResult GetTaskList()
        {
            using (IDbConnection db=new SqlConnection(_connectionString))
            {
                string query = @"Select t.*,c.categoryName 
                                 FROM Tbl_ToDoList t 
                                 INNER JOIN Tbl_TaskCategory c
                                 ON t.CategoryID=c.CategoryID where t.DeleteFlag=0;";
                var lst = db.Query<TasksDataModel, string, TasksViewModel>(query, (task, categoryName) => new TasksViewModel
                {
                   TaskID=task.TaskID,
                   TaskTitle=task.TaskTitle,
                   TaskDescription=task.TaskDescription,    
                   CategoryID=task.CategoryID,
                   Category = new CategoryViewModel
                   {
                       CategoryID=task.CategoryID,
                       CategoryName=categoryName
                   },
                    PriorityLevel = task.PriorityLevel,
                    Status = task.Status,
                    CompletedDate = task.CompletedDate,
                    CreatedDate = task.CreatedDate,
                    DueDate = task.DueDate,
                },splitOn:"CategoryName"
                ).ToList();

                return Ok(lst);

            }

        }

        #endregion getAllTask

       


    }
}
