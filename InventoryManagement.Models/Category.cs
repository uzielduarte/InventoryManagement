using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nombre es requerido.")]
        [MaxLength(60, ErrorMessage = "Nombre puede tener un longitud máxima de 60 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Descripción es requerido.")]
        [MaxLength(100, ErrorMessage = "Nombre puede tener un longitud máxima de 100 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Estado es requerido.")]
        public bool Status { get; set; }
    }
}
