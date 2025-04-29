using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicvillaWeb.Modelos.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVillaWeb.Modelos.ViewModel
{
    public class NumerovillaUpdateViewModel
    {
        public NumeroVillaUpdateDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem>? villaList { get; set; } = null!;
        public NumerovillaUpdateViewModel()
        {
            NumeroVilla = new NumeroVillaUpdateDto();
        }
    }
}