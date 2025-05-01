using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicVilla_utilidad;
using MagicVillaWeb.Modelos;
using MagicVillaWeb.Modelos.Dto;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MagicVillaWeb.Services
{
    public class VillaServices : BaseService, IVillaService
    {
        public readonly new IHttpClientFactory _httpClient;
        private string _villaUrl;
        public VillaServices(IHttpClientFactory httpClient,
        IConfiguration configuration): base(httpClient)
        {
            _httpClient = httpClient;
            _villaUrl = configuration.GetValue<String>("ServiceUrls:API_URL")!;
        }

        public Task<T> Actualizar<T>(VillaUpdateDto dto)
        {
           return SendAsync<T> (new Modelos.APIRequest{
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _villaUrl + "/api/villa" + dto.Id
            });
        }

        public Task<T> Crear<T>(VillaCreateDto dto)
        {
            return SendAsync<T> (new Modelos.APIRequest{
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _villaUrl + "/api/villa"
            });
        }

        public Task<T> Obtener<T>(int id)
        {
            return SendAsync<T> (new Modelos.APIRequest{
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "/api/villa" + id
            });
        }

        public Task<T> ObtenerTodos<T>()
        {
            return SendAsync<T> (new Modelos.APIRequest{
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "/api/villa"
            });
        }

        public Task<T> Remover<T>(int id)
        {
            return SendAsync<T> (new Modelos.APIRequest{
                APITipo = DS.APITipo.DELETE,
                Url = _villaUrl + "/api/villa"+ id
            });
        }
    }
}