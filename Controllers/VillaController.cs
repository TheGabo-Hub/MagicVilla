using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MagicvillaWeb.Modelos.Dto;
using MagicVillaWeb.Modelos;
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

        public VillaController(IVillaService villaService, IMapper mappers)
        {
            _villaService = villaService;
            _mappers = mappers;
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> villalist = new();
            var response = await _villaService.ObtenerTodos<APIResponse>();
            if (Response == null && response.IsExitoso)
            {
                villalist = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)!)!;
            }
            return View(villalist);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}