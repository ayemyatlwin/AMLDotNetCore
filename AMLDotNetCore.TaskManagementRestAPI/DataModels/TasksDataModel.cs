namespace AMLDotNetCore.TaskManagementRestAPI.DataModels
{
    public class TasksDataModel
    {
        public int TaskID { get; set; }
        public string? TaskTitle { get; set; }

        public string? TaskDescription { get; set; }

        public int CategoryID { get; set; }

        public int PriorityLevel { get; set; }

        public string? Status { get; set; }

        public DateTime DueDate { get; set; }  

        public DateTime CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }
    }

    public class CategoryDataModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool DeleteFlag { get; set; }

    }
}
