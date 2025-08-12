using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [MaxLength(60, ErrorMessage = "El nombre no debe exceder los 60 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripcion es requerida.")]
        [MaxLength(100, ErrorMessage = "La descripcion no debe exceder los 60 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El estado es requerido.")]
        public bool Status { get; set; }
    }
}
