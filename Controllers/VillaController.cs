using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MagicVillaWeb.Modelos;
using MagicVillaWeb.Modelos.Dto;
using MagicVillaWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MagicVillaWeb.Controllers
{
    [Route("[controller]")]
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mappers;


        public VillaController(IVillaService villaService, IMapper mappers, ILogger<VillaController> logger)
        {
            _villaService = villaService;
            _mappers = mappers;

        }
        // Modificacion 12/05/2025
        [HttpGet("IndexVilla")]

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> villalist = new();
            var response = await _villaService.ObtenerTodos<ApiResponse>();
            if (response != null && response.IsExitoso)
            {

                villalist = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)!)!;

            }
            return View(villalist);
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }

        [HttpGet("CrearVilla")]
        public IActionResult CrearVilla()
        {
            return View();
        }
        [HttpPost("CrearVilla")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearVilla(VillaCreateDto modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.Crear<ApiResponse>(modelo);
                if (response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Villa Creada Exitosamente";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            //regresamos a la vista para indicar que algo fallo
            return View(modelo);
        }
        [HttpGet("ActualizarVilla")]
        public async Task<IActionResult> ActualizarVilla(int villaId)
        {
            var response = await _villaService.Obtener<ApiResponse>(villaId);
            if (response != null && response.IsExitoso)
            {
                VillaDto modelo = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Resultado)!)!;
                return View(_mappers.Map<VillaUpdateDto>(modelo));
            }
            return NotFound();
        }

        [HttpPost("ActualizarVilla")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarVilla(VillaUpdateDto modelo)
        {

            if (ModelState.IsValid)
            {
                var response = await _villaService.Actualizar<ApiResponse>(modelo);
               
                if (response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Villa Actualizada Exitosamente";
                   return RedirectToAction(nameof(IndexVilla));
                }
            }


            return View (modelo);
        }

        [HttpGet("RemoverVilla")]
        public async Task<IActionResult> RemoverVilla(int villaId)
        {
            var response = await _villaService.Obtener<ApiResponse>(villaId);
            if (response != null && response.IsExitoso)
            {
                VillaDto modelo = JsonConvert.DeserializeObject<VillaDto>
                (Convert.ToString(response.Resultado)!)!;
                return View(modelo);
            }
            return NotFound();
        }
        [HttpPost("RemoverVilla")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> RemoverVilla(VillaDto modelo)
        {
            var response = await _villaService.Remover<ApiResponse>(modelo.Id);
            if (response != null && response.IsExitoso)
            {
                TempData["exitoso"] = "Villa Removida Exitosamente!";
                return RedirectToAction(nameof(IndexVilla));
            }
            TempData["error"] = "Ocurrio un error al remover la villa";
            return View(modelo);
        }
    }
}