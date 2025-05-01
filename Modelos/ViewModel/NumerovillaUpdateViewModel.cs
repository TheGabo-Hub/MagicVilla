using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicVillaWeb.Modelos.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVillaWeb.Modelos.ViewModel
{
    public class NumeroVillaUpdateViewModel
    {
        public NumeroVillaUpdateDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem>? villaList {get; set;} = null!;
        public NumeroVillaUpdateViewModel()
        {

            NumeroVilla= new NumeroVillaUpdateDto();
            
        }
    }
}