
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;
using System.Security.Claims;

namespace MyApp.Controllers
{
    [Authorize]
    public class TaskItemsController : Controller
    {
        private readonly MyAppContext _context;
        public TaskItemsController(MyAppContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        public async Task<IActionResult> Index()
        {
            
            var userId = GetUserId();
            var taskItems = await _context.TaskItems
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return View(taskItems);
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id, string status)
        {
            var userId = GetUserId();

            var taskItem = await _context.TaskItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (taskItem == null) return Forbid();

            taskItem.Status = status;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] TaskItem TaskItem)
        {
            TaskItem.UserId = GetUserId();
            ModelState.Clear();           
            TryValidateModel(TaskItem);

            if (ModelState.IsValid)
            {
                _context.TaskItems.Add(TaskItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(TaskItem);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var userId = GetUserId();
            var taskItem = await _context.TaskItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (taskItem == null) return Forbid();
            return View(taskItem);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price")] TaskItem TaskItem)
        {
            if(ModelState.IsValid)
            {
                _context.Update(TaskItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(TaskItem);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var taskItem = await _context.TaskItems
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (taskItem == null) return Forbid();

            return View(taskItem);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

                var userId = GetUserId();
                var taskItem = await _context.TaskItems
                    .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

                if (taskItem == null) return Forbid();

                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            
        }
    }
}
