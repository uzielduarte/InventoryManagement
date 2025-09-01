using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Número de serie es requerido.")]
        [MaxLength(60)]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Descripción es requerida.")]
        [MaxLength(60)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Precio es requerido.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Costo es requerido.")]
        public double Cost { get; set; }

        public string ImgUrl { get; set; }

        [Required(ErrorMessage = "Estado es requerido.")]
        public bool Status { get; set; }

        // Property
        [Required(ErrorMessage = "Categoría es requerida.")]
        public int CategoryId { get; set; }

        // Navegation
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        // Property
        [Required(ErrorMessage = "Categoría es requerida.")]
        public int BrandId { get; set; }

        // Navegation
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        // Property
        public int? FatherId { get; set; }
        // Navegation
        public virtual Product Father { get; set; }
    }
}
