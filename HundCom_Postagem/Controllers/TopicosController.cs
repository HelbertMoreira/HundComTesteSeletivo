using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HundCom_Postagem.Services;
using HundCom_Postagem.Data.Dtos.Topics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HundCom_Postagem.Models.ViewModels;
using AutoMapper.QueryableExtensions;
using HundCom_Postagem.Services.ImplementationServices;

namespace HundCom_Postagem.Controllers
{
    public class TopicosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITopicoServices _services;
        private readonly AppDbContext _context;
        private readonly AuthenticatedUser _usuario;


        public TopicosController(IMapper mapper, ITopicoServices services, AppDbContext context, AuthenticatedUser usuario)
        {
            _mapper = mapper;
            _services = services;
            _context = context;
            _usuario = usuario;
        }


        public async Task<IActionResult> PaginaInicial(int? pageNumber)
        {
            var usuario = _usuario.Name;

            int pageSize = 2;

            var topicoQueryable = _context.Topicos
                .Include(x => x.ListaPostagens)
                .ProjectTo<ReadTopcDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            return View(await ListaPaginada<ReadTopcDto>.CreateAsync(topicoQueryable, pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> BuscarTopicoPorNome(string searchTopico, int? pageNumber)
        {
            int pageSize = 2;

            var result = _context.Topicos
                    .Include(x => x.ListaPostagens)
                    .Where(x => x.Tema!.Contains(searchTopico))
                    .ProjectTo<ReadTopcDto>(_mapper.ConfigurationProvider)
            .AsQueryable(); ;

            return View("PaginaInicial", await ListaPaginada<ReadTopcDto>.CreateAsync(result, pageNumber ?? 1, pageSize));
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
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ReadTopcDto> AdicionarTopicoNovo(CreateTopcDto topico)
        {
            var usuario = _usuario.Name;
            return await _services.AdicionaTopico(topico);
        }


        [HttpGet]
        [Authorize(Roles = "admin, regular")]
        public async Task<List<ReadTopcDto>> BuscarTopicoPorNomeNovo(string searchTopico)
        {
            var usuario = _usuario.Name;

            return await _services.ListarTodosOsTopicosCadastrados(null, searchTopico);
        }
    }
}
