using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicVillaWeb.Modelos.Dto;

namespace MagicVillaWeb.Services.IServices
{
    public interface INumeroVillaService
    {
        // definimos los metodos que vamos a utilizar
        // Obtener todo los de la lista 
        Task<T> ObtenerTodos<T>();
        // obtener un solo registro
        Task<T> Obtener<T>(int id);
        // crear una nueva villa que se va conectar con nuestra WEB_API
        Task<T> Crear<T>(NumeroVillaCreateDto dto); // using MagicVillaWeb.Models.Dto;
        Task<T> Actualizar<T>(NumeroVillaUpdate dto);
        // para remover un registro
        Task<T> Remover<T>(int id);

    }
}