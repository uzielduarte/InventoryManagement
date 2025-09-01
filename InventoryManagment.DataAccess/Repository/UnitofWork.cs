using InventoryManagement.DataAccess.Data;
using InventoryManagement.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _db;
        public IWarehouseRepository Warehouse { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IBrandRepository Brand { get; private set; }
        public IProductRepository Product { get; private set; }

        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            Warehouse = new WarehouseRepository(_db);
            Category = new CategoryRepository(_db);
            Brand = new BrandRepository(_db);
            Product = new ProductRepository(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
