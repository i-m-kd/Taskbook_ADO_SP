namespace ADOTaskbook.Models
{
    public class TaskModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public String AssignedBy { get; set; }
        public String AssignedTo { get; set; }
        public decimal Duration { get; set; }
        public DateTime Date { get; set; }
    }
}
