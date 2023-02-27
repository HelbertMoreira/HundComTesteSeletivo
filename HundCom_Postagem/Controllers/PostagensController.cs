using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HundCom_Postagem.Services;
using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using HundCom_Postagem.Data.Dtos.Topics;

namespace HundCom_Postagem.Controllers
{
    public class PostagensController : Controller
    {
        private IMapper _mapper;
        private IPostagemServices _services;
        private ITopicoServices _topicoServices;

        public PostagensController(IMapper mapper, IPostagemServices services, ITopicoServices topicoServices)
        {
            _mapper = mapper;
            _services = services;
            _topicoServices = topicoServices;
        }

        
        public async Task<IActionResult> PaginaInicial(int? id, string? searchPosts)
        {
            return View(await _services.ListarTodosAsPostagens(id, searchPosts));
        }

        [HttpGet()]
        [Route("ListaPostagemPaginada")]
        [Authorize(Roles = "admin, regular")]
        public async Task<IActionResult> ListaPostagemPaginada(
            [FromServices] AppDbContext context,
            [FromRoute] int skip = 0,
            [FromRoute] int take = 2)
        {
            var totalposts = await context
                .Postagens
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return View(totalposts);
        }


        public IActionResult AdicionarPostagem()
        { 
            List<ReadTopcDto> topico = _topicoServices.ListarTodosOsTopicosCadastrados(null, null).Result;

            var topicoPostagemViewModel = new TopicosPostagensViewModel()
            {
                ListaTopicos = _mapper.Map<List<ReadTopcDto>>(topico)
            };
            return View(topicoPostagemViewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdicionarPostagem([Bind("Id,Titulo,Conteudo,DataPostagem,TopicoId")] CreatePostDto postagem)
        {
            _services.AdicionaPostagem(postagem);
            return RedirectToAction(nameof(PaginaInicial));
        }


        public IActionResult Edit(int id)
        {
            if (id == null) return NotFound();

            var topico = _mapper.Map<ReadPostDto>(_services.ListarTodosAsPostagens(id, null));

            if (topico != null)            
                return View(topico);            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Titulo,Conteudo,DataPostagem,TopicoId")] UpdatePostDto postagem)
        {
            if (id != postagem.Id) return NotFound();

            try
            {
                _services.AtualizarPostagemCadastrada(postagem.Id, postagem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostagemExists(postagem.Id))
                    return NotFound();
                throw;                
            }
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();

            var topico = _services.ListarTodosAsPostagens(id, null);

            if (topico == null)            
                return NotFound(); 
            return View(topico);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            _services.DeletaPostagemCadastrado(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PostagemExists(int id)
        {
            return _services.ListarTodosAsPostagens(id, null) is null? false : true;
        }
    }
}
