using AutoMapper;
using FluentResults;
using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HundCom_Postagem.Services.ImplementationServices
{
    public class PostagemServicesImplementation : IPostagemServices
    {
        private IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly AuthenticatedUser _usuario;

        public PostagemServicesImplementation(IMapper mapper, AppDbContext context, AuthenticatedUser usuario)
        {
            _mapper = mapper;
            _context = context;
            _usuario = usuario;
        }


        public async Task<List<ReadPostDto>> ListarTodosAsPostagens(int? id, string? searchPosts)
        {
            var posts = from obj
                          in _context.Postagens
                          select obj;

            if (!string.IsNullOrEmpty(searchPosts))
                posts = posts.Where(x => x.Conteudo.Contains(searchPosts));

            if (id != null)
                posts = posts.Where(x => x.TopicoId == id);

            var postagens = await posts
                .Include(x => x.Topico)
                .Include(x => x.Comentarios)
                .ToListAsync();

            return _mapper.Map<List<ReadPostDto>>(postagens);
        }       

        public Task<ReadPostDto> AdicionaPostagem(CreatePostDto postagemDto)
        {
            Postagem postagem = _mapper.Map<Postagem>(postagemDto);            

            var usuario = string.IsNullOrEmpty(_usuario.Name) ? "Visitante": _usuario.Name;
            postagem.DataPostagem = DateTime.Now;
            postagem.Autor = usuario;

            _context.Postagens.Add(postagem);
            _context.SaveChanges();

            return _mapper.Map<Task<ReadPostDto>>(postagem);
        }

        public Result AtualizarPostagemCadastrada(int id, UpdatePostDto updatePostDto)
        {
            var postagem = ListarTodosAsPostagens(id, null);

            if (postagem == null)
                return Result.Fail("Postagem não encontrado");            

            _mapper.Map<Postagem>(postagem);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletaPostagemCadastrado(int id)
        {
            var postagem = ListarTodosAsPostagens(id, null);

            if (postagem == null)            
                return Result.Fail("Postagem não encontrada");
            
            _context.Remove(postagem);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
