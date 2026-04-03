using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;
using System.Linq;

namespace MyApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly MyAppContext _context;

        public TaskService(MyAppContext context)
        {
            _context = context;
        }

        public  List<TaskItem> GetTaskItems (int userId)
        {
            return _context.TaskItems.Where(t => t.UserId == userId).ToList();
        }

        public void Create(int userId, string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Title required");

            var TaskItem = new TaskItem
            {
                Name = title,
                Description = description,
                UserId = userId

            };

            _context.TaskItems.Add(TaskItem);
            _context.SaveChanges();
        }

        public void Delete(int userId, int taskId)
        {
            var TaskItem = _context.TaskItems.FirstOrDefault(t => t.Id == taskId);

            if (TaskItem == null)
                throw new Exception("Not found");

            if (TaskItem.UserId != userId)
                throw new Exception("Unauthorized");

            _context.TaskItems.Remove(TaskItem);
            _context.SaveChanges();
        }

    }
}
