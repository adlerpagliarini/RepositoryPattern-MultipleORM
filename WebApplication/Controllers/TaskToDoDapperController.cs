using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Interfaces.Services.Domain;
using Microsoft.AspNetCore.Routing;

namespace WebApplication.Controllers
{
    public class TaskToDoDapperController : Controller
    {
        private readonly ITaskToDoDapperService taskToDoService;

        public TaskToDoDapperController(ITaskToDoDapperService taskToDoService)
        {
            this.taskToDoService = taskToDoService;
        }

        // GET: TaskToDo/Create
        public IActionResult Create(int? userId)
        {
            ViewData["UserId"] = userId;
            return View();
        }

        // POST: TaskToDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Start,DeadLine,UserId")] TaskToDo taskToDo)
        {
            if (ModelState.IsValid)
            {

                await taskToDoService.AddAsync(taskToDo);
                return RedirectToAction("Index",
                      new RouteValueDictionary(
                          new { controller = "Dapper", action = "Index", Id = taskToDo.UserId }));
            }
            ViewData["UserId"] = taskToDo.UserId;
            return View(taskToDo);
        }

        // GET: TaskToDo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskToDo = await taskToDoService.GetByIdAsync(id);
            if (taskToDo == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = taskToDo.UserId;
            return View(taskToDo);
        }

        // POST: TaskToDo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Start,DeadLine,UserId")] TaskToDo taskToDo)
        {
            if (id != taskToDo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await taskToDoService.UpdateAsync(taskToDo);
                return RedirectToAction("Index",
                      new RouteValueDictionary(
                          new { controller = "Dapper", action = "Index", Id = taskToDo.UserId }));
            }
            ViewData["UserId"] = taskToDo.UserId;
            return View(taskToDo);
        }

        // GET: TaskToDo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskToDo = await taskToDoService.GetByIdAsync(id);

            if (taskToDo == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = taskToDo.UserId;
            return View(taskToDo);
        }

        // POST: TaskToDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int userId)
        {
            var taskToDo = await taskToDoService.RemoveAsync(id);
            return RedirectToAction("Index",
                      new RouteValueDictionary(
                          new { controller = "Dapper", action = "Index", Id = userId }));
        }

        // POST: TaskToDo/Complete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id, int userId)
        {
            await taskToDoService.UpdateStatusAsync(id, true);
            return RedirectToAction("Index",
                      new RouteValueDictionary(
                          new { controller = "Dapper", action = "Index", Id = userId }));
        }
    }
}
