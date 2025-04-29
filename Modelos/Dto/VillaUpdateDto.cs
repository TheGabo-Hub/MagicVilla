using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MagicvillaWeb.Modelos.Dto
{
    public class VillaUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(30, ErrorMessage = "El nombre debe tener una longitud maxima de 30 caracteres")]
        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set; }
        [Required]
        public string? ImagenURL { get; set; }
        public string? Amenidad { get; set; }

    }
}