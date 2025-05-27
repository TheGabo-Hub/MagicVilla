using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MagicVillaWeb.Modelos.Dto
{
    public class NumeroVillaUpdate
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalleESpecial { get; set; } = string.Empty;
    }
}