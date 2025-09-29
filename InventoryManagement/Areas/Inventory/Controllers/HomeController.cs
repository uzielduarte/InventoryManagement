using System.Diagnostics;
using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using InventoryManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofWork;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> productList = await _unitofWork.Product.GetAll();
            return View(productList);
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
