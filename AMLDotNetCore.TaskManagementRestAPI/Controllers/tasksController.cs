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

        #region getTaskDetail
        [HttpGet("{id}")]

        public IActionResult GetTaskDetail(int id)
        {
            if (!TaskExists(id))
            {
                return NotFound("Task ID not found!");

            }
            string query = @"SELECT * FROM [dbo].[Tbl_ToDoList] where DeleteFlag=0 AND TaskID=@TaskID;";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var item = db.Query(query, new TasksDataModel
                { TaskID = id }).FirstOrDefault();
                if (item is null)
                {
                    return BadRequest("There is no matching data...");
                }
                return Ok(item);
            }
        }

        #endregion getTaskDetail

        #region createTask
        [HttpPost]
        public IActionResult CreateTask(TasksDataModel model)
        {
            if (string.IsNullOrEmpty(model.Status))
            {
                model.Status = "Pending";
            }
            else
            {
                if (!allowedStatusList.Contains(model.Status))
                {
                    return BadRequest("Invalid task status.");
                }
            }
            if (model.PriorityLevel < 1 || model.PriorityLevel > 5)
            {
                return BadRequest("Invalid priority level.It must be between 1 to 5.");
            }
            DateTime? completedDate = null;
            if (model.Status == "Completed")
            {
                completedDate = DateTime.Now;
            }
            string query = @" INSERT INTO [dbo].[Tbl_ToDoList]
                     ([TaskTitle],
                        [TaskDescription],
                        [CategoryID],
                        [PriorityLevel],
                        [Status],
                        [DueDate],
                        [CreatedDate],
                        [CompletedDate],
                        [DeleteFlag])
                     VALUES
                           (@TaskTitle,
                            @TaskDescription,
                            @CategoryID,
                            @PriorityLevel,
                            @Status,
                            @DueDate,
                            GETDATE(),         
                            @CompletedDate,    
                            0)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new
                {
                    model.TaskTitle,
                    model.TaskDescription,
                    model.CategoryID,
                    model.PriorityLevel,
                    model.Status,
                    model.DueDate,
                    CompletedDate = completedDate
                });

                string message = result == 1 ? "Task successfully created!" : "Failed to create task!";
                return Ok(message);
            }
        }

        #endregion createTask

        #region updateTask
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TasksDataModel model)
        {
            if (!TaskExists(id))
            {
                return NotFound("Task ID not found!");

            }
            if (!allowedStatusList.Contains(model.Status))
            {
                return BadRequest("Invalid task status.");
            }
            if (model.PriorityLevel < 1 || model.PriorityLevel > 5)
            {
                return BadRequest("Invalid priority level.It must be between 1 to 5.");
            }
            string query = @"
                 UPDATE [dbo].[Tbl_ToDoList]
                 SET 
                     [TaskTitle] = @TaskTitle,
                     [TaskDescription] = @TaskDescription,
                     [CategoryID] = @CategoryID,
                     [PriorityLevel] = @PriorityLevel,
                     [Status] = @Status,
                     [DueDate] = @DueDate,
                     [CompletedDate] = CASE WHEN @Status = 'Completed' THEN @CompletedDate ELSE CompletedDate END,
                     [DeleteFlag] = 0
                 WHERE DeleteFlag = 0 AND TaskID = @TaskID;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var result = db.Execute(query, new
                {
                    TaskID = id,
                    TaskTitle = model.TaskTitle,
                    TaskDescription = model.TaskDescription,
                    CategoryID = model.CategoryID,
                    PriorityLevel = model.PriorityLevel,
                    Status = model.Status,
                    DueDate = model.DueDate,
                    CompletedDate = DateTime.Now,
                });
                string message = result == 1 ? "Task successfully updated!" : "Failed to update the task!";
                return Ok(message);
            }
        }
        #endregion updateTask



    }
}
