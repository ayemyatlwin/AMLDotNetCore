namespace AMLDotNetCore.TaskManagementRestAPI.ViewModels
{
    public class TasksViewModel
    {
        public int TaskID { get; set; }
        public string TaskTitle { get; set; }

        public string TaskDescription { get; set; }

        public int CategoryID { get; set; }

        public CategoryViewModel Category { get; set; }


        public int PriorityLevel { get; set; }

        public string Status { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public bool DeleteFlag { get; set; }

    }

    public class CategoryViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

}
