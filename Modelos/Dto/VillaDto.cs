using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
// El objetivo es ocultar los datos
namespace MagicvillaWeb.Modelos.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(30, ErrorMessage = "El nombre debe tener una longitud maxima de 30 caracteres")]
        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string? ImagenURL { get; set; }
        public string? Amenidad { get; set; }

    }
}