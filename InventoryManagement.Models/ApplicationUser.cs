using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Los nombres son requeridos.")]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Los apellidos son requeridos.")]
        [MaxLength(80)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "La dirección es requerida.")]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required(ErrorMessage = "La ciudad es requerida.")]
        [MaxLength(60)]
        public string City { get; set; }
        [Required(ErrorMessage = "El país es requerido.")]
        [MaxLength(60)]
        public string Country { get; set; }

        [NotMapped] // Existirá en el modelo, pero no se agregará como una columna en la taba de la base de datos.
        public string Role {  get; set; }
    }
}
