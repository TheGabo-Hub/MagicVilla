using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicvillaWeb.Modelos.Dto;

namespace MagicVillaWeb.Services
{
    public interface IVillaService
    {
        Task<T> ObtenerTodos<T>();
        Task<T> Obtener<T>(int id);
        Task<T> Crear<T>(villaCreateDto dto);
        Task<T> Actualizar<T>(VillaUpdateDto dto);
        Task<T> Remover<T>(int id);


    }
}