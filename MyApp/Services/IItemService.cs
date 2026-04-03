using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Services
{
    public interface ITaskService
    {
        List<TaskItem> GetTaskItems(int userId);
        void Create(int userId, string title, string description);
        void  Delete(int userId, int taskId);
    }
}
