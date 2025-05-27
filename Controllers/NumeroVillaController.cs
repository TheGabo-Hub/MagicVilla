using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using MagicVillaWeb.Services;
using AutoMapper;
using MagicVillaWeb.Modelos.Dto;
using MagicVillaWeb.Modelos;
using Newtonsoft.Json;
using MagicVillaWeb.Services.IServices;
using MagicVillaWeb.Modelos.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MagicVillaWeb.Controllers
{
    //[Route("[controller]")]
    public class NumeroVillaController : Controller // using Microsoft.AspNetCore.Mvc;
    {
        // Esta propiedad sera de tipo IVillaService para poder acceder a los metodos implementados
        // using MagicVillaWeb.Services;
        private readonly INumeroVillaService _numeroVillaService; // accedemos a todos los umeros villa
        // instalamos el paquete Mapper
        private readonly IMapper _mapper; // using AutoMapper;
        private readonly IVillaService _villaService; // accedemos a todas las villas
        // ctor
        // inyectamos los servicios
        public NumeroVillaController(INumeroVillaService numeroVillaService, IVillaService villaService,IMapper mapper )
        {
            _numeroVillaService = numeroVillaService;
            _mapper = mapper;
            _villaService = villaService;
        }
        [HttpGet("IndexNumeroVilla")]

        public async Task<IActionResult> IndexNumeroVilla()
    {
      // VilaDto using MagicVillaWeb.Models.Dto;
      // vamos  a crear una villaList tipo VillaDto.
      List<NumeroVillaDto> numeroVillaList = new(); // la inicializamos
                                                    // La variable Response es la encargada de traernos toda la lista.
                                                    // APIResponse es la clase que nos retorna sus propiedade y todos los elementos de APIResponse
      var response = await _numeroVillaService.ObtenerTodos<ApiResponse>(); // using MagicVillaWeb.Models;

      if (response != null && response.IsExitoso)
      {
        // using Newtonsoft.Json;
        // Vamos hacer una Deserializacion, porque lo que nos retorna tenemos que ponerlo de tipo ListaDTo
        // ya que lo que recibimos es un Json, hacemos la conversión con un JSON convert.
        // Recordemos que el resultado  es donde están todos los valores que retorna el EndPoint.
        numeroVillaList = JsonConvert.DeserializeObject<List<NumeroVillaDto>>(Convert.ToString(response.Resultado)!)!;
      }
      // pasamos villaList a la vista para que nos la muestre.
      return View(numeroVillaList);
    }
          
      //      // Creamos el Metodo GET para CrearNumeroVilla
      //     // Los métodos cuando no se le definen un tipo normalmente siempre son GET
      //     // Los métodos que normalmente llaman a las vistas son métodos tipo Get.
      [HttpGet("CrearNumeroVilla")]
         public async Task<IActionResult> CrearNumeroVilla() // llama a la vista
         { 
          // hacemos referencia al ViewModel de damos un nombre para instanciarlo
          NumeroVillaViewModel numeroVillaVM = new (); // using MagicVillaWeb.Models.ViewModel;
          // creamos una variable response donde voy almacenar todas las villas
          var response = await _villaService.ObtenerTodos<ApiResponse>();
          // verificamos si el response fue exitoso
          if (response != null && response.IsExitoso)
          {
            //  vamos a traer los datos convertidos, para seleccionar los campos que necesitemos usamos Select
            //  v lo tenemos que convertir en SelectListItem al mimo tipo de dato que esta en VillaList
               numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)!)!
                                          .Select (v=> new SelectListItem // using Microsoft.AspNetCore.Mvc.Rendering;
                                          {
                                            Text = v.Nombre,
                                            Value = v.Id.ToString() // como todo esta en string hay que convertilo en string
                                          });
          }
          // y lo tenemos  que hacer es un retornar todo ViewModel
           return View(numeroVillaVM);
         }

         [HttpPost("CrearNumeroVilla")]
         // Siempre que creemos un método de tipo httPost, tenemos que incluir el ValidateAntiForgeryToken
         // Esto es un método de seguridad para el envío de datos.
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> CrearNumeroVilla(NumeroVillaViewModel modelo) //Encargar de enviar la informacion
         {
             // verificamos si el ModelState es valido 
             // Si el ModelState  es  válido el modelo, en este caso el modelo que estamos trabajando es 
             // NumeroVillaViewModel si está Si están todos los campos requeridos llenos o si algo falla en el modelo,
             //  pues esto me lo va a prevenir.
             if (ModelState.IsValid)
             {
             // creamos una variable response y Utilizamos nuestro servicio usando el metodo crear que debe ser 
             // de tipo APIResponse. Necesitamos tambien enviarle el modelo que se encuentra en ViewModel que
             // se llama NumeroVila al servicio en el método crear
                 var response = await _numeroVillaService.Crear<ApiResponse>(modelo.NumeroVilla);
                 if (response != null && response.IsExitoso)
                 {
                     TempData["exitoso"]="Numero Villa creada Exitosamente!";
                     // si salio todo bien en el envio de los datos y es almacenado la nueva villa
                     // lo único que tendremos que hacer es redireccionar a nuestra vista Index
                    return  RedirectToAction(nameof(IndexNumeroVilla)); // IndexVilla tiene todas las listas de las villas,
                 } // fin del if response
                 else
                 {
                      // si por caso contrario llegase a haber un error,  lista de ErrorMessage lo va a poder controlar
                      // como es una lista comparamos si es mayor a cero quiere decir que esta llena de errores de mensajes
                      // var eleList = response.ErrorMessages.Count!;
                      if (response!.ErrorMessages!.Count>0)
                      {
                        // Si esta llena creo un modelState.AddModelError para agregar el error de la lista
                        // usamos FirstOrDefault para tomar el primer error de la lista.
                        ModelState.AddModelError("ErrorMessage",response.ErrorMessages.FirstOrDefault()!); // using Microsoft.AspNetCore.Mvc.ModelBinding;

                      }
                 }

             } // fin del if ModelState

             // si algo no está correcto en el ModelState, de alguna manera tenemos que volver a cargar la lista de villas
             // es decir, antes de hacer el return view() y mandarle el modelo en el caso de que algo esté incorrecto, 
             // necesitamos cargar la lista de villas  para que llene el drop down que está en la vista

            var res = await _villaService.ObtenerTodos<ApiResponse>();
            if (res != null && res.IsExitoso)
               {
                modelo.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Resultado)!)!
                                          .Select (v=> new SelectListItem // using Microsoft.AspNetCore.Mvc.Rendering;
                                          {
                                            Text = v.Nombre,
                                            Value = v.Id.ToString() // como todo esta en string hay que convertilo en string
                                          });
               }
             // Caso contrario hacemos un return en la  Vista  y pasamos el modelo de regreso 
             // indicándole que algo falló y que se debe de corregir.
           return View(modelo);
         }


       // Metodos para actualizar una Numerovilla
      //   // Con este metodo consultamos a la b.d la villa que queremos modificar
          [HttpGet("ActualizarNumeroVilla")]
          public async Task<IActionResult> ActualizarNumeroVilla(int villaNo)
         {
           
           NumeroVillaUpdateViewModel numeroVillaVM = new();

          var response = await _numeroVillaService.Obtener<ApiResponse>(villaNo); // using MagicVillaWeb.Models;
      
           if (response !=null && response.IsExitoso)
            {
             // using Newtonsoft.Json;
              // Vamos hacer una Deserializacion, porque lo que nos retorna tenemos que ponerlo de tipo VillaDTo
              // ya que lo que recibimos es un Json, hacemos la conversión con un JSON convert.
              // Recordemos que el resultado  es donde están todos los valores que retorna el EndPoint.
              NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado)!)!;
              // Utilizamps AutoMapper para que me convierta el modelo VillaDto a  VillaUpdateDto
              // el cual es el modelo que vamos a trabajar en la vista.
              // la fuente es modelo que es de tipo VillaDto
              numeroVillaVM.NumeroVilla = _mapper.Map<NumeroVillaUpdate>(modelo);
              // return View(); // En la vista enviamos un modelo de tipo VillaUpdateDto
            }
            response = await _villaService.ObtenerTodos<ApiResponse>();
             // verificamos si el response fue exitoso
             if (response != null && response.IsExitoso)
              {
            //  vamos a traer los datos convertidos, para seleccionar los campos que necesitemos usamos Select
            //  v lo tenemos que convertir en SelectListItem al mimo tipo de dato que esta en VillaList
               numeroVillaVM.villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)!)!
                                          .Select (v=> new SelectListItem // using Microsoft.AspNetCore.Mvc.Rendering;
                                          {
                                            Text = v.Nombre,
                                            Value = v.Id.ToString() // como todo esta en string hay que convertilo en string
                                          });
              return View(numeroVillaVM);
          }
             // Caso contrario hacemos NotFounf indicándole que no se encontro el registro villa
             return NotFound();
         }
          // Este metodo se va encargar de actualizar la Villa
         [HttpPost("ActualizarNumeroVilla")]
         [ValidateAntiForgeryToken]
          public async Task<IActionResult> ActualizarNumeroVilla(NumeroVillaUpdateViewModel modelo) // recibe un modelo VillaUpdateDto
         {
             // Verificamos si el ModelState si lo que esta enviando del  modelo que esta trabajando la vista
             // está todo correcto.
           if (ModelState.IsValid)
           {
                 // creamos una variable response que es la que se va a conectar con el servicio para traer el metodo Actualizar
                 var response = await _numeroVillaService.Actualizar<ApiResponse>(modelo.NumeroVilla); // le enviamos el modelo de tipo VillaUpdateDto
                 // si actualiza el registro por medio del servicio Actualizar
                 // Pero siempre tenemos que  comprobar si realmente el response se esta jecutando correctamente
                 if (response != null && response.IsExitoso)
                    {
                       TempData["exitoso"]="Numero Villa actualizada Exitosamente!";
                     // si salio todo bien en el envio de los datos y es actualizo la nueva villa
                     // lo único que tendremos que hacer es redireccionar a nuestra vista IndexNumeroVilla
                    return  RedirectToAction(nameof(IndexNumeroVilla)); // IndexVilla tiene todas las listas de las villas y las muestre,
                     }
                     else {
                       if (response!.ErrorMessages!.Count>0)
                       {
                        // Si esta llena creo un modelState.AddModelError para agregar el error de la lista
                        // usamos FirstOrDefault para tomar el primer error de la lista.
                        ModelState.AddModelError("ErrorMessage",response.ErrorMessages.FirstOrDefault()!);
                       }
                    }
             }// fin del if
               
            // llenamos la lista en caso de un error de
             var res = await _villaService.ObtenerTodos<ApiResponse>();
             if (res != null && res.IsExitoso)
               {
                modelo.villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Resultado)!)!
                                          .Select (v=> new SelectListItem // using Microsoft.AspNetCore.Mvc.Rendering;
                                          {
                                            Text = v.Nombre,
                                            Value = v.Id.ToString() // como todo esta en string hay que convertilo en string
                                          });
               }
            // Caso contrario hacemos NotFounf indicándole que no se encontro el registro villa
              return View(modelo);
         }


         [HttpGet("RemoverNumeroVilla")]
          public async Task<IActionResult> RemoverNumeroVilla(int villaNo)
         {
           
           NumeroVillaDeleteViewModel numeroVillaVM = new();

          var response = await _numeroVillaService.Obtener<ApiResponse>(villaNo); // using MagicVillaWeb.Models;
      
           if (response !=null && response.IsExitoso)
            {
             
              NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado)!)!;

             // Aquí no se necesita mapear porque ya en sí el modelo es NumeroVillaDto.
             // numeroVillaVM.NumeroVilla = _mapper.Map<NumeroVillaUpdateDto>(modelo);
              numeroVillaVM.NumeroVilla = modelo;
            }
            response = await _villaService.ObtenerTodos<ApiResponse>();
             // verificamos si el response fue exitoso
             if (response != null && response.IsExitoso)
              {
            //  vamos a traer los datos convertidos, para seleccionar los campos que necesitemos usamos Select
            //  v lo tenemos que convertir en SelectListItem al mimo tipo de dato que esta en VillaList
               numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)!)!
                                          .Select (v=> new SelectListItem // using Microsoft.AspNetCore.Mvc.Rendering;
                                          {
                                            Text = v.Nombre,
                                            Value = v.Id.ToString() // como todo esta en string hay que convertilo en string
                                          });
              return View(numeroVillaVM);
          }
             // Caso contrario hacemos NotFounf indicándole que no se encontro el registro villa
             return NotFound();
         }
         
         [HttpPost("RemoverNumeroVilla")]
         [ValidateAntiForgeryToken]
          public async Task<IActionResult> RemoverNumeroVilla(NumeroVillaDeleteViewModel modelo)
         {
             var response = await _numeroVillaService.Remover<ApiResponse>(modelo.NumeroVilla.VillaNo);
             if (response != null && response.IsExitoso)
             {
                 TempData["exitoso"]="Numero Villa Removida Exitosamente!";
                 return RedirectToAction(nameof(IndexNumeroVilla));
             }
              TempData["error"]="Ocurrio un error al removerlo";
           // Si llegase a ocurrir algún error, pues simplemente hacemos un return View y pasamos el modelo
             return View(modelo);
         }
    }
}