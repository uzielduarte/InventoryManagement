using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using InventoryManagement.Models.ViewModels;
using InventoryManagement.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitofWork.Product.GetAllDropdownList("Category"),
                BrandList = _unitofWork.Product.GetAllDropdownList("Brand"),
                FatherList = _unitofWork.Product.GetAllDropdownList("Product")
            };

            if(id == null)
            {
                // new product
                productVM.Product.Status = true;
                return View(productVM);
            }
            else
            {
                productVM.Product = await _unitofWork.Product.Get(id.GetValueOrDefault());
                
                if(productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.Product.Id == 0)
                {
                    // create product
                    string upload = webRootPath + StaticDefinitions.ImgRoute;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using(var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.ImgUrl = fileName + extension;
                    await _unitofWork.Product.Add(productVM.Product);
                }
                else // if is updating and there's a new img loaded
                {
                    // Update
                    var product = await _unitofWork.Product.GetFirst(p => p.Id == productVM.Product.Id, isTracking: false);
                    if(files.Count > 0)
                    {
                        string upload = webRootPath + StaticDefinitions.ImgRoute;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        // Remove previous img
                        var oldImg = Path.Combine(upload, product.ImgUrl);
                        if (System.IO.File.Exists(oldImg))
                        {
                            System.IO.File.Delete(oldImg);
                        }

                        using(var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.ImgUrl = fileName + extension;
                    }// if is updating and a new img is not loaded
                    else
                    {
                        productVM.Product.ImgUrl = product.ImgUrl;
                    }
                    _unitofWork.Product.Update(productVM.Product);
                }

                TempData[StaticDefinitions.Success] = "Producto guardado exitosamente";
                await _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }

            productVM.CategoryList = _unitofWork.Product.GetAllDropdownList("Category");
            productVM.BrandList = _unitofWork.Product.GetAllDropdownList("Brand");
            productVM.FatherList = _unitofWork.Product.GetAllDropdownList("Product");

            return View(productVM);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var all = await _unitofWork.Product.GetAll(includeProperties: "Category,Brand");

            return Json(new {data = all});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitofWork.Product.Get(id);
            if (product == null)
            {
                return Json(new { success = false, message = "Producto no encontrada." });
            }
            // Remove img
            string upload = _webHostEnvironment.WebRootPath + StaticDefinitions.ImgRoute;
            var oldImg = Path.Combine(upload, product.ImgUrl);

            if (System.IO.File.Exists(oldImg))
            {
                System.IO.File.Delete(oldImg);
            }

            _unitofWork.Product.Remove(product);
            await _unitofWork.Save();

            return Json(new { success = true, message = "Producto borrado exitosamente." });
        }

        [ActionName("ValidateSerialNumber")]
        public async Task<IActionResult> ValidateSerialNumber(string serie, int id = 0)
{
            bool value = false;
            var list = await _unitofWork.Product.GetAll();
            if (id == 0)
            {
                value = list.Any(b => b.SerialNumber.ToLower().Trim() == serie.ToLower().Trim());
            }
            else
            {
                value = list.Any(b => b.SerialNumber.ToLower().Trim() == serie.ToLower().Trim() && b.Id != id);
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
