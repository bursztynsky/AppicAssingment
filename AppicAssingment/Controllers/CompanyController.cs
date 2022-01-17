using AppicAssingment.Data;
using AppicAssingment.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppicAssingment.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ApplicationDbContext dbContext, ILogger<CompanyController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Company> objs = AppicAssingment.Models.Company.GetValidUsers(_dbContext.Companies);
            return View(objs);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company obj)
        {
            if (!obj.IsValid())
            {
                TempData["error"] = "The company must have all fields completed";
                return View();
            }

            _dbContext.Companies.Add(obj);
            _dbContext.SaveChanges();

            TempData["success"] = "Company created successfully";
            _logger.LogInformation($"Company created successfully. {obj}");
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

            var objFromDb = _dbContext.Companies.FirstOrDefault(x => x.Id == id);

            if (objFromDb == null)
            {
                TempData["error"] = "User not found";
                return NotFound();
            }

            return View(objFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company obj)
        {
            if (!obj.IsValid())
            {
                TempData["error"] = "The company must have all fields completed";
                _logger.LogWarning($"Object not valid. {obj}");
                return View();
            }

            _dbContext.Companies.Update(obj);
            _dbContext.SaveChanges();

            TempData["success"] = "Company updated successfully";
            _logger.LogInformation($"Company updated successfully. {obj}");
            return RedirectToAction(nameof(Index));
        }

        // GET
        public IActionResult Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                TempData["error"] = "Company cannot be deleted without provided id";
                return NotFound();
            }

            var companyFromDb = _dbContext.Companies.FirstOrDefault(x => x.Id == id);

            if (companyFromDb == null)
            {
                TempData["error"] = "Company not found";
                return NotFound();
            }

            return View(companyFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Guid? id)
        {
            var obj = _dbContext.Companies.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                TempData["error"] = "Company not found";
                return NotFound();
            }

            _dbContext.Remove(obj);
            _dbContext.SaveChanges();

            TempData["success"] = "Company deleted successfully";
            _logger.LogInformation($"Company deleted successfully. {obj}");
            return RedirectToAction(nameof(Index));
        }
    }
}
