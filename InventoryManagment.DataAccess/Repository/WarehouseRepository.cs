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
    public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
    {
        private readonly ApplicationDbContext _db;

        public WarehouseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Warehouse warehouse)
        {
            var warehouseDB = _db.Warehouses.FirstOrDefault(w => w.Id == warehouse.Id);
            if (warehouseDB != null)
            { 
                warehouseDB.Name = warehouse.Name;
                warehouseDB.Description = warehouse.Description;
                warehouseDB.Status = warehouse.Status;

                _db.SaveChanges();
            }
        }
    }
}
