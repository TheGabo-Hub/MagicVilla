using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicvillaWeb.Modelos.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVillaWeb.Modelos.ViewModel
{
    public class NumeroillaViewModel
    {
        public NumeroillaViewModel()
        {
            NumeroVilla = new NumeroVillaCreateDto();
        }
        public NumeroVillaCreateDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem>? VillaList { get; set; } = null!;

    }
}