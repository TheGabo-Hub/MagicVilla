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
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,IVillaService villaService, IMapper mapper )
        {
            _logger = logger;
            _villaService =villaService;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDto> villaList= new();
            var response = await _villaService.ObtenerTodos<ApiResponse>();
            if(response!=null && response.IsExitoso){

                villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)!)!;

            }
            return View(villaList);
        }

        public IActionResult Privacy(){
            return View();
        }
    }
}