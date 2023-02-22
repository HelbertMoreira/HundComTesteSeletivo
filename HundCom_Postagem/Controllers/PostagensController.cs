using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HundCom_Postagem.Models.Entities;
using AutoMapper;
using HundCom_Postagem.Services;
using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Models.ViewModels;

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

        
        public IActionResult PaginaInicial(int? id, string? searchPosts)
        {
            return View(_services.ListarTodosAsPostagensCadastrados(id, searchPosts));
        }


        public IActionResult AdicionarPostagem()
        {            
            var topicoPostagemViewModel = new TopicosPostagensViewModel()
            {
                ListaTopicos = _topicoServices.ListarTodosOsTopicosCadastrados()
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

            var topico = _mapper.Map<ReadPostDto>(_services.ListarTodosAsPostagensCadastrados(id, null));

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
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();            

            var topico = _services.ListarTodosAsPostagensCadastrados(id, null)
                .FirstOrDefault(m => m.Id == id);

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
            return _services.ListarTodosAsPostagensCadastrados(id, null) != null ? true: false;
        }
    }
}
