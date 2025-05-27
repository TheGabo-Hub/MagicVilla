using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MagicVillaWeb.Modelos.Dto
{
    public class NumeroVillaDto
    {
       [Required]
         public int VillaNo { get; set; }
       [Required]
        public int VillaId { get; set; }
        
        public string  DetalleESpecial { get; set; } = string.Empty; // inicializamos una cadena vacia
        // Para crear una navegacion al modelo VillaDto creamos una propiedad
        // de tipo VillaDto le ponemos Villa de nombre
        public VillaDto Villa { get; set; } = null!;
    }

}