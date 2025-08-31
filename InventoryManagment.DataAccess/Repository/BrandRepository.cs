using InventoryManagement.DataAccess.Data;
using InventoryManagement.DataAccess.Repository.IRepository;
using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _db;

        public BrandRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Brand brand)
        {
            var brandDB = _db.Brands.FirstOrDefault(w => w.Id == brand.Id);
            if (brandDB != null)
            {
                brandDB.Name = brand.Name;
                brandDB.Description = brand.Description;
                brandDB.Status = brand.Status;

                _db.SaveChanges();
            }
        }
    }
}
