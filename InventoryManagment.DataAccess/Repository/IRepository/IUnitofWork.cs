using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        IWarehouseRepository Warehouse {  get; }
        ICategoryRepository Category { get; }
        IBrandRepository Brand { get; }

        IProductRepository Product { get; }

        IApplicationUserRepository ApplicationUser { get; }
        Task Save();
    }
}
