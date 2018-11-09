using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration.Dapper;
using Infrastructure.Repositories.Dapper;
using System.Collections.Generic;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.EFCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Infrastructure.DBConfiguration.EFCore;

namespace WebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository repositoryDapperUser;
        private EntityFrameworkUser repositoryEntityFrameworkUser;
        public UsersController(IUserRepository dapperUser, ApplicationContext applicationContext)
        {
            repositoryDapperUser = dapperUser;
            repositoryEntityFrameworkUser = new EntityFrameworkUser(applicationContext);
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var x = new List<User>()
            {
                new User(){ Id = 1313905, Name = "AAA"},
                new User(){ Id = 1313904, Name = "BB"},
                new User(){ Id = 1313903, Name = "CCC"}
            };

            var z = repositoryEntityFrameworkUser.GetAll().Take(10);

            repositoryEntityFrameworkUser.Update(x[0]);
            repositoryEntityFrameworkUser.UpdateRange(x);
            await repositoryEntityFrameworkUser.CommitAsync();
            //repositoryEntityFrameworkUser.Dispose();
            /*
            repositoryDapperUser.AddRange(x);
            await repositoryDapperUser.AddRangeAsync(x);

            repositoryDapperUser.Update(x[0]);
            await repositoryDapperUser.UpdateAsync(x[1]);
            await repositoryDapperUser.UpdateRangeAsync(x);

            repositoryDapperUser.Remove(3);
            repositoryDapperUser.Remove(x[0]);
            repositoryDapperUser.RemoveRange(x);
            await repositoryDapperUser.RemoveAsync(1313851);
            await repositoryDapperUser.RemoveAsync(y[1]);
            await repositoryDapperUser.RemoveRangeAsync(y);*/

            var xx = repositoryDapperUser.GetAll();
            var xy = await repositoryDapperUser.GetAllAsync();

            return View(z.Take(5));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await repositoryDapperUser.GetByIdAsync(id);
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
                await repositoryDapperUser.AddAsync(user);
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

            var user = await repositoryDapperUser.GetByIdAsync(id);
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
                    await repositoryDapperUser.UpdateAsync(user);
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

            var user = await repositoryDapperUser.GetByIdAsync(id);
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
            await repositoryDapperUser.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return repositoryDapperUser.GetByIdAsync(id) != null ? true : false;
        }
    }
}
