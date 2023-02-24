using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HundCom_Postagem.Services;
using HundCom_Postagem.Data.Dtos.Topics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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


        public async Task<IActionResult> PaginaInicial(int? idTopico, string? searchTopico)
        {
            return View(await _services.ListarTodosOsTopicosCadastrados(idTopico, searchTopico));
        }


        [HttpGet()]
        [Route("ListaTopicosPaginada")]
        [Authorize(Roles = "admin, regular")]
        public async Task<IActionResult> ListaTopicosPaginada(
            [FromServices] AppDbContext context,
            [FromRoute] int skip = 0,
            [FromRoute] int take = 2)
        {
            var totalTopicos = await context
                .Topicos
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return View(totalTopicos);
        }


        public async Task<IActionResult> BuscarTopicoPorNome(string searchTopico)
        {
            var result = await _services.ListarTodosOsTopicosCadastrados(null, searchTopico);
            return View("PaginaInicial", result);
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


        public async Task<IActionResult> DeletarTopico(int id)
        {
            var topicos = await _services.ListarTodosOsTopicosCadastrados(null, null);

            ReadTopcDto topicoDto = _mapper.Map<ReadTopcDto>(topicos.FirstOrDefault(x => x.Id == id));

            if (topicoDto == null)
                return NotFound();
            return View(topicoDto);
        }


        [HttpPost, ActionName("DeletarTopico")]
        public IActionResult DeleteConfirmed(int id)
        {
            _services.DeletaTopicoCadastrado(id);
            return RedirectToAction(nameof(PaginaInicial));
        }


        //Rotas que retornam objetos JSON para teste no postman
        //[HttpPost]
        //[Authorize(Roles = "admin")]
        //public ReadTopcDto AdicionarTopicoNovo(CreateTopcDto topico)
        //{

        //    return _services.AdicionaTopico(topico);
        //}


        //[HttpGet]
        //[Authorize(Roles = "admin, regular")]
        //public async Task<List<ReadTopcDto>> BuscarTopicoPorNomeNovo(string searchTopico)
        //{
        //    return await _services.BuscarTopicoCadastradosPorNome(searchTopico);
        //}
    }
}
