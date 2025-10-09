using InventoryManagement.Models.Especificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true);

        PagedList<T> GetAllPaged(Parametros parametros,
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true);

        Task<T> GetFirst(
            Expression<Func<T, bool>> filtro = null,
            string includeProperties = null,
            bool isTracking = true);
        Task Add(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> item);
    }
}
