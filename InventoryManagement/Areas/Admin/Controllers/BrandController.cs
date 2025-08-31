using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using InventoryManagement.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public BrandController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Brand brand = new Brand();
            if (id == null)
            {
                // A category need to be created
                brand.Status = true;
                return View(brand);
            }
            brand = await _unitofWork.Brand.Get(id.GetValueOrDefault());
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Brand brand)
        {
            if (ModelState.IsValid)
            {
                if(brand.Id == 0)
                {
                    await _unitofWork.Brand.Add(brand);
                    TempData[StaticDefinitions.Success] = "Marca creada exitosamente.";
                }else
                {
                    _unitofWork.Brand.Update(brand);
                    TempData[StaticDefinitions.Success] = "Marca actulizada exitosamente.";
                }

                await _unitofWork.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[StaticDefinitions.Error] = "Error al guardar marca.";
            return View(brand);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var all = await _unitofWork.Brand.GetAll();

            return Json(new {data = all});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitofWork.Brand.Get(id);
            if (brand == null)
            {
                return Json(new { success = false, message = "Marca no encontrada." });
            }
            _unitofWork.Brand.Remove(brand);
            await _unitofWork.Save();

            return Json(new { success = true, message = "Marca borrada exitosamente." });
        }

        [ActionName("ValidateName")]
        public async Task<IActionResult> ValidateName(string name, int id = 0)
{
            bool value = false;
            var list = await _unitofWork.Brand.GetAll();
            if (id == 0)
            {
                value = list.Any(b => b.Name.ToLower().Trim() == name.ToLower().Trim());
            }
            else
            {
                value = list.Any(b => b.Name.ToLower().Trim() == name.ToLower().Trim() && b.Id != id);
            }

            if (value)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }
        #endregion
    }
}
