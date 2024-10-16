using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly PortfolioContext _context;

        public HomeController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContactForm(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Your message has been sent. Thank you!" });
            }

            // If the form is not valid, return validation errors
            return Json(new { success = false, errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()) });
        }

    }
}
