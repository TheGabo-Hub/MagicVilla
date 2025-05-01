using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicVillaWeb.Modelos.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVillaWeb.Modelos.ViewModel
{
    public class NumeroVillaDeleteViewModel
    {
        public NumeroVillaDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem>? VillaList { get; set; }= null!;
        public NumeroVillaDeleteViewModel()
        {
            NumeroVilla= new NumeroVillaDto();
            
        }
    }
}