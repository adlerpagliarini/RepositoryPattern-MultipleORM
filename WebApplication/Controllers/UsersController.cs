using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.DBConfiguration.Dapper;

namespace WebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepositoryEFCore<User> repositoryEFCoreUser;
        private readonly IRepositoryDapper<User> repositoryDapperUser;
        private readonly IRepositoryDapper<ToDoList> repositoryDapperTodoList;

        public UsersController(IRepositoryEFCore<User> efCoreUser, IRepositoryDapper<User> dapperUser, IDataServiceFactory dapperFactory)
        {
            repositoryEFCoreUser = efCoreUser;
            repositoryDapperUser = dapperUser;
            repositoryDapperTodoList = dapperFactory.CreateInstance<ToDoList>();
        }

        // GET: Users
        public IActionResult Index()
        {
            return View(repositoryEFCoreUser.GetAllQueryable().Take(10));
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
