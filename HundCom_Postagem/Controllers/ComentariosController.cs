using Microsoft.AspNetCore.Mvc;
using HundCom_Postagem.Models.ViewModels;
using AutoMapper;
using HundCom_Postagem.Services;
using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Data.Dtos.Comments;
using HundCom_Postagem.Models.Entities;
using HundCom_Postagem.Services.ImplementationServices;

namespace HundCom_Postagem.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IComentarioServices _services;
        private readonly IPostagemServices _postagemServices;

        public ComentariosController(IMapper mapper, IComentarioServices services, IPostagemServices postagemServices)
        {
            _mapper = mapper;
            _services = services;
            _postagemServices = postagemServices;
        }

        
        public IActionResult AdicionarComentario(int id)
        {
            var postagens =_postagemServices.ListarTodosAsPostagens(id, null).Result;

            var comentarioModel = new CreateCommentDto()
            {
                Postagens = _mapper.Map<List<ReadPostDto>>(postagens),
                postagemId = id,
                DataComentario = DateTime.Now                
            };
            return View(comentarioModel);
        }


        public IActionResult ListarComentariosPorPostagem(int idPostagem)
        {
            return View(_services.ListarComentariosPorPostagem(idPostagem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCommentDto comentario)
        {
            _services.AdicionarComentario(comentario);
            return RedirectToAction("PaginaInicial", "Postagens", null);
        }
        
        public IActionResult Delete(int id)
        {
            var resultado =  _services.ListarComentariosPorId(id);
            if (resultado != null)
                return View(resultado);
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return RedirectToAction("PaginaInicial", "Postagens", null);
        }
    }
}
