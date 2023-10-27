using Microsoft.AspNetCore.Mvc;
using Moscu_Diana_Stephani_Lab2.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Moscu_Diana_Stephani_Lab2.Data;
using Moscu_Diana_Stephani_Lab2.Models.LibraryViewModels;

namespace Moscu_Diana_Stephani_Lab2.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        private readonly LibraryContext _context; //adaugat in Lab4, eroare, step 5
        
        public async Task<ActionResult> Statistics() //Adaugat in Lab4
        {
            IQueryable<OrderGroup> data = from order in _context.Orders
                                          group order by order.OrderDate into dateGroup
                                          select new OrderGroup()
                                          {
                                              OrderDate = dateGroup.Key,
                                              BookCount = dateGroup.Count()
                                          };
            return View(await data.AsNoTracking().ToListAsync());
        }

        private readonly ILogger<HomeController> _logger;

       /* public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }  */

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}