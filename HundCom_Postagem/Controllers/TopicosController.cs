using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HundCom_Postagem.Services;
using HundCom_Postagem.Data.Dtos.Topics;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using FluentResults;

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
            return View(_services.ListarTodosOsTopicosCadastrados());
        }

        
        public async Task<IActionResult> BuscarTopicoPorNome(string searchTopico)
        {  
            return View("PaginaInicial", await _services.BuscarTopicoCadastradosPorNome(searchTopico));
        }

        public IActionResult AdicionarTopico()
        {
            return View();
        }


        [HttpPost]       
        public IActionResult AdicionarTopico(CreateTopcDto topico)
        {            
            _services.AdicionaTopico(topico);
            return RedirectToAction(nameof(PaginaInicial));
        }

        public IActionResult DeletarTopico(int id)
        {
            var topico = _services
                .ListarTodosOsTopicosCadastrados()
                .FirstOrDefault(m => m.Id == id);

            if (topico == null)
                return NotFound();
            return View(topico);
        }

        
        [HttpPost, ActionName("DeletarTopico")]
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


        // Rotas que retornam objetos JSON para teste no postman
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ReadTopcDto AdicionarNovoTopico(CreateTopcDto topico)
        {
            return _services.AdicionaTopico(topico);
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular")]
        public async Task<List<ReadTopcDto>> BuscarTopicoPorNomeNovo(string searchTopico)
        {
            return await _services.BuscarTopicoCadastradosPorNome(searchTopico);
        }
    }
}
