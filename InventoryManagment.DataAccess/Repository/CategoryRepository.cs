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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var categoryDB = _db.Categories.FirstOrDefault(w => w.Id == category.Id);
            if (categoryDB != null)
            {
                categoryDB.Name = category.Name;
                categoryDB.Description = category.Description;
                categoryDB.Status = category.Status;

                _db.SaveChanges();
            }
        }
    }
}
