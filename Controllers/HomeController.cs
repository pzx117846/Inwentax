using Microsoft.AspNetCore.Mvc;

namespace Inwentax.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return View("IndexAdmin");
                }

                if (User.IsInRole("User"))
                {
                    return View("IndexUser");
                }
            }    
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}
