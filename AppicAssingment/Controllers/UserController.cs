using AppicAssingment.Data;
using AppicAssingment.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppicAssingment.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UserController> _logger;

        public UserController(ApplicationDbContext dbContext, ILogger<UserController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<User> objs = AppicAssingment.Models.User.GetValidUsers(_dbContext.Users);
            return View(objs);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User obj)
        {
            if (!obj.IsValid())
            {
                TempData["error"] = "The user must have all fields completed";
                return View();
            }

            _dbContext.Users.Add(obj);
            _dbContext.SaveChanges();

            TempData["success"] = "User created successfully";
            _logger.LogInformation($"User created successfully. {obj}");
            return RedirectToAction(nameof(Index));
        }

        // GET
        public IActionResult Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                TempData["error"] = "User not found";
                return NotFound();
            }

            var objFromDb = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (objFromDb == null)
            {
                TempData["error"] = "User not found";
                return NotFound();
            }

            return View(objFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User obj)
        {
            if (!obj.IsValid())
            {
                TempData["error"] = "The user must have all fields completed";
                _logger.LogWarning($"Object not valid. {obj}");
                return View();
            }

            _dbContext.Users.Update(obj);
            _dbContext.SaveChanges();

            TempData["success"] = "User updated successfully";
            _logger.LogInformation($"User updated successfully. {obj}");
            return RedirectToAction(nameof(Index));
        }

        // GET
        public IActionResult Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                TempData["error"] = "User cannot be deleted without provided id";
                return NotFound();
            }

            var UserFromDb = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (UserFromDb == null)
            {
                TempData["error"] = "User not found";
                return NotFound();
            }

            return View(UserFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Guid? id)
        {
            var obj = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                TempData["error"] = "User not found";
                return NotFound();
            }

            _dbContext.Remove(obj);
            _dbContext.SaveChanges();

            TempData["success"] = "User deleted successfully";
            _logger.LogInformation($"User deleted successfully. {obj}");
            return RedirectToAction(nameof(Index));
        }
    }
}
