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
        public ApiResponse responseModel { get ; set ; }
        public IHttpClientFactory _httpClient {get; set;}

        public BaseService(IHttpClientFactory httpClient) 
        {
            _httpClient = httpClient;
            this.responseModel = new();
        }
    
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("MagicApI");
                HttpRequestMessage message= new HttpRequestMessage();
                message.Headers.Add("Accept","application/json");
                message.RequestUri = new Uri (apiRequest.Url);
                if (apiRequest.Datos !=null){
                    message.Content=new StringContent(JsonConvert.SerializeObject(apiRequest.Datos),Encoding.UTF8,"application/json");
                }//Fin del if
                switch(apiRequest.APITipo){

                    case DS.APITipo.POST:
                    message.Method=HttpMethod.Post;
                    break;
                    case DS.APITipo.PUT:
                    message.Method=HttpMethod.Put;
                    break;
                    case DS.APITipo.DELETE:
                    message.Method=HttpMethod.Delete;
                    break;
                    case DS.APITipo.GET:
                    message.Method=HttpMethod.Get;
                    break;


                }
                HttpResponseMessage apiResponse = null!;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                
                try
                {
                    ApiResponse response= JsonConvert.DeserializeObject<ApiResponse>(apiContent)!;
                    if (apiResponse.StatusCode == HttpStatusCode.BadRequest|| apiResponse.StatusCode == HttpStatusCode.NotFound){

                     response.statusCode= HttpStatusCode.BadRequest;
                     response.IsExitoso = false;
                     var res = JsonConvert.SerializeObject(response);
                     var obj= JsonConvert.DeserializeObject<T>(res);
                     return obj!;

                }
                }
                catch (System.Exception)
                {
                    
                    var errorResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return errorResponse!;
                }
                var ApiResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return ApiResponse!;

            }// Fin del try
            catch (System.Exception ex)
            {
                
                var dto= new ApiResponse{
                    ErrorMessages = new List<string>
                    {
                        Convert.ToString(ex.Message)
                    }, IsExitoso = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var responseEx = JsonConvert.DeserializeObject<T>(res);
                return responseEx!;
            }
        }
    }
}