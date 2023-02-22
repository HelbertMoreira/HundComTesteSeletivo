using Microsoft.AspNetCore.Mvc;
using HundCom_Postagem.Models.ViewModels;
using AutoMapper;
using HundCom_Postagem.Services;
using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Data.Dtos.Comments;
using HundCom_Postagem.Models.Entities;

namespace HundCom_Postagem.Controllers
{
    public class ComentariosController : Controller
    {
        private IMapper _mapper;
        private IComentarioServices _services;
        private IPostagemServices _postagemServices;

        public ComentariosController(IMapper mapper, IComentarioServices services, IPostagemServices postagemServices)
        {
            _mapper = mapper;
            _services = services;
            _postagemServices = postagemServices;
        }

        
        public IActionResult AdicionarComentario(int id)
        {
            var comentarioModel = new CreateCommentDto()
            {
                Postagens = _postagemServices.ListarTodosAsPostagensCadastrados(id, null),
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
        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(AdicionarComentario));
        }
    }
}
