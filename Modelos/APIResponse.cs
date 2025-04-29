using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MagicVillaWeb.Modelos
{
    public class APIResponse
    {

        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        //va a retornar el codigo de estado del Endpoint
        public HttpStatusCode statusCode { get; set; }
        //checar si fue exitoso la solicitud
        public bool IsExitoso { get; set; } = true;
        //Esto va a ser una lista de tipo string de todos los errores que se presentan
        public List<String>? ErrorMessages { get; set; }
        //El resultado es de tipo objeto porque el resultado del endpoint puede ser una lista,
        //puede ser un objeto , etc
        public object? Resultado { get; set; }
    }
}