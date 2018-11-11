using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Collections.Generic;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Repositories.EFCore;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.DBConfiguration.Dapper;
using Microsoft.Extensions.Options;
using Infrastructure.Repositories.Dapper;

namespace WebApplication.Controllers
{
    public class DapperController : Controller
    {
        private readonly IUserRepository userService;

        private readonly IUserRepository userDapper;
        private readonly IUserRepository userEntityFramework;

        public DapperController(IUserRepository userService, IOptions<DataOptionFactory> databaseOptions, ApplicationContext applicationContext)
        {
            this.userService = userService;
            userDapper = new DapperUser(databaseOptions);
            userEntityFramework = new EntityFrameworkUser(applicationContext);
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var result = await userService.GetAllAsync();
            return View(result.Take(10));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] User user)
        {
            if (ModelState.IsValid)
            {
                await userService.AddAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await userService.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await userService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return userService.GetByIdAsync(id) != null ? true : false;
        }
    }
}
