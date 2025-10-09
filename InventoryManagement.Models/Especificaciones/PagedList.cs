using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models.Especificaciones
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                TotalPages = (int) Math.Ceiling(count / (double) pageSize)
            };
            
            // Agrega los elementos de la coleccion al final de la lista
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> entity, int pageNumber, int pageSize)
        {
            var count = entity.Count();
            var items = entity.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
