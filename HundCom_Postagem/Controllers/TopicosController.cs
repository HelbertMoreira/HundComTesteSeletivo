using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HundCom_Postagem.Services;
using HundCom_Postagem.Data.Dtos.Topics;

namespace HundCom_Postagem.Controllers
{
    public class TopicosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITopicoServices _services;

        public TopicosController(IMapper mapper, ITopicoServices services)
        {
            _mapper = mapper;
            _services = services;
        }

        
        public IActionResult PaginaInicial()
        {
            var teste = _services.ListarTodosOsTopicosCadastrados();
            string aaa = "";
            return View(_services.ListarTodosOsTopicosCadastrados());
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTopicoPorNome(string? searchTopico)
        {  
            return View("PaginaInicial", await _services.BuscarTopicoCadastradosPorNome(searchTopico));
        }


        public IActionResult AdicionarTopico()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdicionarTopico(CreateTopcDto topico)
        {
            _services.AdicionaTopico(topico);
            return RedirectToAction(nameof(PaginaInicial));
        }        

       
        public async Task<IActionResult> DeletarTopico(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topico = _services.ListarTodosOsTopicosCadastrados()
                .FirstOrDefault(m => m.Id == id);
            if (topico == null)
            {
                return NotFound();
            }

            return View(topico);
        }

        
        [HttpPost, ActionName("DeletarTopico")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var topico = _services.BuscarTopicoCadastradosPorId(id);
            if (topico != null)
            {
                _services.DeletaTopicoCadastrado(topico.Id);
                return RedirectToAction(nameof(PaginaInicial));
            }
            return RedirectToAction(nameof(PaginaInicial));
        }


        private bool TopicoExists(int id)
        {
            return _services.ListarTodosOsTopicosCadastrados().Any(e => e.Id == id);
        }
    }
}
