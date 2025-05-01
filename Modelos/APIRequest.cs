using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MagicVilla_utilidad.DS;

namespace MagicVillaWeb.Modelos
{
    public class APIRequest
    {
        //solicitud
        public APITipo APITipo {get; set;} = APITipo.GET; 
        public string Url {get; set;} = string.Empty;
         public object Datos {get; set;} =null!;


    }
        
        
}
        