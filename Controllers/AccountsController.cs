using Microsoft.AspNetCore.Mvc;
using RePurpose.Models;

namespace RePurpose.Controllers
{
    public class AccountsController : Controller
    {
        // For now, we'll use a static list as a fake database.
        private static List<AccountsModel> accounts = new List<AccountsModel>
        {
            new AccountsModel { Id = 1, Email = "test1@example.com", Username = "test1", Password = "pass1", Role = "Buyer" },
            new AccountsModel { Id = 2, Email = "test2@example.com", Username = "test2", Password = "pass2", Role = "Manager" }
        };

        // GET: Accounts
        public IActionResult Index()
        {
            return View(accounts);
        }

        // GET: Accounts/Details/5
        public IActionResult Details(int id)
        {
            var account = accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AccountsModel account)
        {
            if (ModelState.IsValid)
            {
                account.Id = accounts.Max(a => a.Id) + 1;
                accounts.Add(account);
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public IActionResult Edit(int id)
        {
            var account = accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AccountsModel account)
        {
            var existingAccount = accounts.FirstOrDefault(a => a.Id == id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingAccount.Email = account.Email;
                existingAccount.Username = account.Username;
                existingAccount.Password = account.Password;
                existingAccount.Role = account.Role;
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public IActionResult Delete(int id)
        {
            var account = accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var account = accounts.FirstOrDefault(a => a.Id == id);
            if (account != null)
            {
                accounts.Remove(account);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
