using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicVilla_utilidad;
using MagicVillaWeb.Modelos.Dto;
using MagicVillaWeb.Services.IServices;

namespace MagicVillaWeb.Services
{
  public class NumeroVillaService : BaseService, INumeroVillaService
    {
        
        public readonly  new IHttpClientFactory _httpClient; 
        private string _villaUrl;
        //ctor
        // inyectamos los servicios
        public NumeroVillaService(IHttpClientFactory httpClient, IConfiguration configuration): base(httpClient)
        {
            _httpClient =  httpClient;
            _villaUrl= configuration.GetValue<string>("ServiceUrls:API_URL")!;
        }
        public Task<T> Actualizar<T>(NumeroVillaUpdate dto)
        {
             return SendAsync<T> (new Modelos.APIRequest(){
                APITipo = DS.APITipo.PUT, // using MagicVilla_utilidad;
                Datos = dto,
                Url=_villaUrl + "/api/NumeroVilla/" + dto.VillaNo
            });
        }

        public Task<T> Crear<T>(NumeroVillaCreateDto dto)
        {
            return SendAsync<T> (new Modelos.APIRequest(){
                APITipo = DS.APITipo.POST, // using MagicVilla_utilidad;
                Datos = dto,
                Url=_villaUrl + "/api/NumeroVilla"
            });
        }

        public Task<T> Obtener<T>(int id)
        {
            return SendAsync<T> (new Modelos.APIRequest(){
                APITipo = DS.APITipo.GET, // using MagicVilla_utilidad;
                Url=_villaUrl + "/api/NumeroVilla/" + id
            });
        }

        public Task<T> ObtenerTodos<T>()
        {
            return SendAsync<T> (new Modelos.APIRequest(){
                APITipo = DS.APITipo.GET, // using MagicVilla_utilidad;
                Url=_villaUrl + "/api/NumeroVilla"
            });
        }
        public Task<T> Remover<T>(int id)
        {
            return SendAsync<T> (new Modelos.APIRequest(){
                APITipo = DS.APITipo.DELETE, // using MagicVilla_utilidad;
                Url=_villaUrl + "/api/NumeroVilla/" + id
            });
        }
    }
}