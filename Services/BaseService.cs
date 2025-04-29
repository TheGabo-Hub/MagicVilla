using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MagicVilla_utilidad;
using MagicVillaWeb.Modelos;
using Newtonsoft.Json;

namespace MagicVillaWeb.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            this.responseModel = new();
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                //*Cambié cliente por client
                var client = _httpClient.CreateClient("MagicApI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                //* Esto inicializa una uri de nuestro APIrequest
                //* que tiene la propiedad url
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Datos != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Datos), Encoding.UTF8, "application/json");
                } //? fin del if
                // switch
                switch (apiRequest.APITipo)
                {
                    case DS.APITipo.POST: //*Usando MagicVilla_utilidad
                        message.Method = HttpMethod.Post;
                        break;
                    case DS.APITipo.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case DS.APITipo.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                } //* fin del swtich
                HttpResponseMessage apiResponse = null!;
                // Enviamos el mensaje al servicio, enviamos la solicitud 
                // recibimos el contenido de la respuesta
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                try
                {
                    APIResponse response = JsonConvert.DeserializeObject<APIResponse>(apiContent)!;
                    if (apiResponse.StatusCode == HttpStatusCode.BadRequest || apiResponse.StatusCode == HttpStatusCode.NoContent)
                    {
                        response.statusCode = HttpStatusCode.BadRequest;
                        response.IsExitoso = false;
                        var res = JsonConvert.SerializeObject(response);
                        var obj = JsonConvert.DeserializeObject<T>(res);
                        return obj!;
                    }
                }
                catch (System.Exception)
                {

                    var errorResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return errorResponse!;
                } //? fin del try interno
                var ApiResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return ApiResponse!;
            } //? fin del try 
            catch (System.Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string>{
                        Convert.ToString(ex.Message)
                    },
                    IsExitoso = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var responseEx = JsonConvert.DeserializeObject<T>(res);
                return responseEx!;
            }    //fin del catch 
        }
    }
}