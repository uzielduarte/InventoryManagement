using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using InventoryManagement.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WarehouseController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public WarehouseController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Warehouse warehouse = new Warehouse();
            if (id == null)
            {
                // A warehouse need to be created
                warehouse.Status = true;
                return View(warehouse);
            }
            warehouse = await _unitofWork.Warehouse.Get(id.GetValueOrDefault());
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                if(warehouse.Id == 0)
                {
                    await _unitofWork.Warehouse.Add(warehouse);
                    TempData[StaticDefinitions.Success] = "Bodega creada exitosamente.";
                }else
                {
                    _unitofWork.Warehouse.Update(warehouse);
                    TempData[StaticDefinitions.Success] = "Bodega actulizada exitosamente.";
                }

                await _unitofWork.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[StaticDefinitions.Error] = "Error al guardar bodega.";
            return View(warehouse);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var all = await _unitofWork.Warehouse.GetAll();

            return Json(new {data = all});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var warehouse = await _unitofWork.Warehouse.Get(id);
            if (warehouse == null)
            {
                return Json(new { success = false, message = "Bodega no encontrada." });
            }
            _unitofWork.Warehouse.Remove(warehouse);
            await _unitofWork.Save();

            return Json(new { success = true, message = "Bodega borrada exitosamente." });
        }

        [ActionName("ValidateName")]
        public async Task<IActionResult> ValidateName(string name, int id = 0)
{
            bool value = false;
            var list = await _unitofWork.Warehouse.GetAll();
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
