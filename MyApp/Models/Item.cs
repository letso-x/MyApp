namespace MyApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Status { get; set; } = "pending";

        public int UserId { get; set; }
        public User? User { get; set; }
    }

    
}
