using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicvillaWeb.Modelos.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVillaWeb.Modelos.ViewModel
{
    public class NumerovillaDeleteviewModel
    {
        public NumeroVillaDto NumeroVilla { get; set; }
        public IEnumerable<SelectListItem>? NumeroList { get; set; }
        public NumerovillaDeleteviewModel()
        {
            NumeroVilla = new NumeroVillaDto();
        }
    }
}