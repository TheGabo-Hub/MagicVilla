using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MagicVilla_utilidad.DS;

// namespace MagicVilla_utilidad;

// //* Definicion 
// public class DS
// {
//     public enum APITipo
//     {
//         //* Verbos
//         GET, // Obtener o consultar
//         POST, // Crear 
//         PUT, // Modificar
//         DELETE //Eliminar
//     }
// }


namespace MagicVillaWeb.Modelos
{
    public class APIRequest
    {
        //Solicitud 
        public APITipo APITipo { get; set; } = APITipo.GET; //Asignamos un valor inicial
        public String Url { get; set; } = string.Empty; //cadena vacia
        public object Datos { get; set; } = null!;

    }
}