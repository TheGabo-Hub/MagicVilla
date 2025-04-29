using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MagicvillaWeb.Modelos.Dto
{
    public class NumeroVillaUpdateDto
    {
        [Required]
        public int villaNo { get; set; }
        [Required]
        public int villaId { get; set; }
        public string DetallEspecial { get; set; } = string.Empty;
    }
}