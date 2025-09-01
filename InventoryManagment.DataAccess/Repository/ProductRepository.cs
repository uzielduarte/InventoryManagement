using InventoryManagement.DataAccess.Data;
using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            if(obj == "Category")
            {
                return _db.Categories.Where(c => c.Status == true).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
            }
            if(obj == "Brand")
            {
                return _db.Brands.Where(c => c.Status == true).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
            }
            if(obj == "Product")
            {
                return _db.Products.Where(c => c.Status == true).Select(c => new SelectListItem
                {
                    Text = c.Description,
                    Value = c.Id.ToString()
                });
            }

            return null;
        }

        public void Update(Product product)
        {
            var productDB = _db.Products.FirstOrDefault(w => w.Id == product.Id);
            if (productDB != null)
            {
                if(product.ImgUrl != null)
                {
                    productDB.ImgUrl = product.ImgUrl;
                }
                productDB.SerialNumber = product.SerialNumber;
                productDB.Description = product.Description;
                productDB.Cost = product.Cost;
                productDB.Price = product.Price;
                productDB.CategoryId = product.CategoryId;
                productDB.BrandId = product.BrandId;
                productDB.FatherId = product.FatherId;
                productDB.Status = product.Status;

                _db.SaveChanges();
            }
        }
    }
}
