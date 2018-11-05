using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.DBConfiguration.Dapper;
using Infrastructure.Repositories.Dapper;
using System.Collections.Generic;

namespace WebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepositoryEFCore<User> repositoryEFCoreUser;
        private readonly RepositoryDapperUser repositoryDPUser;

        public UsersController(IRepositoryEFCore<User> efCoreUser, IDataServiceFactory dapperFactory, RepositoryDapperUser repositorydpuser)
        {
            repositoryEFCoreUser = efCoreUser;
            //repositoryDapperTodoList = dapperFactory.CreateInstance<ToDoList>();
            repositoryDPUser = repositorydpuser;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            //return View(repositoryEFCoreUser.GetAllQueryable().Take(10));
            var x = new List<User>()
            {
                new User(){ Id = 1313857, Name = "AA"},
                new User(){ Id = 1313858, Name = "BB"},
                new User(){ Id = 1313859, Name = "CC"}
            };

            var y = new List<User>()
            {
                new User(){ Id = 1313853, Name = "AA"},
                new User(){ Id = 1313852, Name = "BB"},
                new User(){ Id = 1313851, Name = "CC"}
            };

            /*repositoryDPUser.AddRange(x);
            await repositoryDPUser.AddRangeAsync(x);*/

            /*repositoryDPUser.Update(x[0]);
            await repositoryDPUser.UpdateAsync(x[1]);
            await repositoryDPUser.UpdateRangeAsync(x);*/

            repositoryDPUser.Remove(3);
            repositoryDPUser.Remove(x[0]);
            repositoryDPUser.RemoveRange(x);
            await repositoryDPUser.RemoveAsync(1313851);
            await repositoryDPUser.RemoveAsync(y[1]);
            await repositoryDPUser.RemoveRangeAsync(y);

            return View(repositoryDPUser.GetAll());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await repositoryEFCoreUser.GetByIdAsync(id);
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
                await repositoryEFCoreUser.AddAsync(user);
                await repositoryEFCoreUser.CommitAsync();
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

            var user = await repositoryEFCoreUser.GetByIdAsync(id);
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
                    repositoryEFCoreUser.Update(user);
                    await repositoryEFCoreUser.CommitAsync();
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

            var user = await repositoryEFCoreUser.GetByIdAsync(id);
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
            repositoryEFCoreUser.Remove(id);
            await repositoryEFCoreUser.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return repositoryEFCoreUser.GetByIdAsync(id) != null ? true : false;
        }
    }
}
