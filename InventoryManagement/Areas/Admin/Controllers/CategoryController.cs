using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using InventoryManagement.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                // A category need to be created
                category.Status = true;
                return View(category);
            }
            category = await _unitofWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.Id == 0)
                {
                    await _unitofWork.Category.Add(category);
                    TempData[StaticDefinitions.Success] = "Categoría creada exitosamente.";
                }else
                {
                    _unitofWork.Category.Update(category);
                    TempData[StaticDefinitions.Success] = "Categoría actulizada exitosamente.";
                }

                await _unitofWork.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[StaticDefinitions.Error] = "Error al guardar categoría.";
            return View(category);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var all = await _unitofWork.Category.GetAll();

            return Json(new {data = all});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitofWork.Category.Get(id);
            if (category == null)
            {
                return Json(new { success = false, message = "Categoría no encontrada." });
            }
            _unitofWork.Category.Remove(category);
            await _unitofWork.Save();

            return Json(new { success = true, message = "Categoría borrada exitosamente." });
        }

        [ActionName("ValidateName")]
        public async Task<IActionResult> ValidateName(string name, int id = 0)
{
            bool value = false;
            var list = await _unitofWork.Category.GetAll();
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
