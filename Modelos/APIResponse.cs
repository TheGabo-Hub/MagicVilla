using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MagicVillaWeb.Modelos
{
    public class ApiResponse
    {
        //Va a retornar el código de esado de Endpoint
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode statusCode { get; set; }
            

        //Checar si la solicitud es exitosa
        public bool IsExitoso { get; set; } = true;
        
        //Esta va a ser una lista string de todos los errores que se epresenten
        public List <string>? ErrorMessages { get; set; }

        //El resultado es de tipo objetoporque el resultado del endpoint puede ser lista, objeto, etc.
        public object? Resultado { get; set; }
    }
}