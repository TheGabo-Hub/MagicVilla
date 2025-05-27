using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicVillaWeb.Modelos.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVillaWeb.Modelos.ViewModel
{
    public class NumeroVillaViewModel
    {
         // ctor
         // Creamos un constructor para instanciar e inicializar al número villa  del tipo NumeroVillaCreateDto
         // hacemos esto porque necesitamos siempre tener instanciado e inicializado apenas se 
         // cargue nuestro nuestro NumeroVillaViewModel.
         public NumeroVillaViewModel()
         {
             NumeroVilla = new NumeroVillaCreateDto(); // using MagicVillaWeb.Models.Dto;
       }
         //creamos la propieda NumeroVilla 
        public NumeroVillaCreateDto NumeroVilla { get; set; } 
        // Vamos a utilizar una lista en la cual nosotros queremos
        // que se carguen todas las villas para poderlas seleccionar mediante un combo,
        // mediante un dropdown o un set list. Es una propiedad IEnunerable de tipo SelecListItem

        // using Microsoft.AspNetCore.Mvc.Rendering;
         public IEnumerable<SelectListItem>? VillaList { get; set; } = null!; 
    }
}