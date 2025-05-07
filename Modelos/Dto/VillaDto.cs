using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MagicVillaWeb.Modelos.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [MaxLength (30, ErrorMessage = "El nombre debe tener una longitud máxima de 30 caracteres")]
        public string? Nombre { get; set; } = null!;

        public int Ocupantes { get; set; }
        public string? Detalle { get; set; } // ? que permite admitir nulos 
        public double Tarifa { get; set; } // ? que permite admitir nulos 
        public int MetrosCuadrados { get; set; }
         public string? ImageUrl { get; set; }
         public string? Amenidad { get; set; }
         public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

    }
}