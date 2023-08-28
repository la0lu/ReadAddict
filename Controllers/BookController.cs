using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReadAddict.Controllers
{
    public class BookController : Controller
    {
        [Authorize(Roles ="Admin")]
        public IActionResult AddBook()
        {
            return View();
        }
    }
}
