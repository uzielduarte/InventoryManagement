using System.Diagnostics;
using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using InventoryManagement.Models.Especificaciones;
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

        public IActionResult Index(int pageNumber = 1, string search = "", string currentSearch = "")
        {
            if(!String.IsNullOrEmpty(search))
            {
                pageNumber = 1;
            }
            else
            {
                search = currentSearch;
            }

            ViewData["CurrentSearch"] = search;

            if (pageNumber < 1) { pageNumber = 1; }

            Parametros parametros = new Parametros()
            {
                PageNumber = pageNumber,
                PageSize = 4
            };

            var result = _unitofWork.Product.GetAllPaged(parametros);

            if(!String.IsNullOrEmpty(search))
            {
                result = _unitofWork.Product.GetAllPaged(parametros, p => p.Description.Contains(search));
            }

            ViewData["TotalPages"] = result.MetaData.TotalPages;
            ViewData["TotalRecords"] = result.MetaData.TotalCount;
            ViewData["PageSize"] = result.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previous"] = "disabled";
            ViewData["Next"] = "";

            if (pageNumber > 1) { ViewData["Previous"] = "";}
            if (result.MetaData.TotalPages <= pageNumber) { ViewData["Next"] = "disabled"; }

            return View(result);
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
