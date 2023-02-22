using HundCom_Postagem.Models;
using HundCom_Postagem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HundCom_Postagem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITopicoServices _topicoServices;

        public HomeController(ILogger<HomeController> logger, ITopicoServices topicoServices)
        {
            _logger = logger;
            _topicoServices = topicoServices;
        }

        public IActionResult Index()
        {
            return View(_topicoServices.ListarTodosOsTopicosCadastrados());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}