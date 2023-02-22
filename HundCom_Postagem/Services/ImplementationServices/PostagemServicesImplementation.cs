using AutoMapper;
using FluentResults;
using HundCom_Postagem.Data.Dtos.Posts;
using HundCom_Postagem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HundCom_Postagem.Services.ImplementationServices
{
    public class PostagemServicesImplementation : IPostagemServices
    {
        private IMapper _mapper;
        private readonly AppDbContext _context;

        public PostagemServicesImplementation(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<ReadPostDto> ListarTodosAsPostagensCadastrados(int? id, string? searchPosts)
        {
            var posts = from obj
                          in _context.Postagens
                          select obj;

            if (!string.IsNullOrEmpty(searchPosts))
                posts = posts.Where(x => x.Conteudo.Contains(searchPosts));

            if (id != null)
                posts = posts.Where(x => x.TopicoId == id);

            var teste = posts
                .Include(x => x.Topico)
                .Include(x => x.Comentarios)
                .ToList();

            return _mapper.Map<List<ReadPostDto>>(posts
                .Include(x => x.Topico)
                .Include(x => x.Comentarios)
                .ToList());
        }       

        public ReadPostDto AdicionaPostagem(CreatePostDto postagemDto)
        {
            Postagem postagem = _mapper.Map<Postagem>(postagemDto);
            postagem.DataPostagem = DateTime.Now;
            _context.Postagens.Add(postagem);
            _context.SaveChanges();
            return _mapper.Map<ReadPostDto>(postagem);
        }

        public Result AtualizarPostagemCadastrada(int id, UpdatePostDto updatePostDto)
        {
            Postagem postagem = BuscarPostagemCadastradaPorId(id);
            if (postagem == null)
            {
                return Result.Fail("Postagem não encontrado");
            }
            //_context.Update(topico);
            _mapper.Map(updatePostDto, postagem);
            _context.SaveChanges();
            return Result.Ok();
        }

        private Postagem BuscarPostagemCadastradaPorId(int id)
        {
            var postagem = _context.Postagens.FirstOrDefault(m => m.Id == id);

            if (postagem == null)  
                return null;
            return postagem;
        }

        public Result DeletaPostagemCadastrado(int id)
        {
            Postagem postagem = BuscarPostagemCadastradaPorId(id);
            if (postagem == null)
            {
                return Result.Fail("Postagem não encontrada");
            }
            _context.Remove(postagem);
            _context.SaveChanges();
            return Result.Ok();
        }

        public IEnumerable<ReadPostDto> ListarPostagemCadastradosPorNome(string nomePostagem)
        {
            return _mapper.Map<List<ReadPostDto>>(_context.Postagens.Where(p => p.Conteudo.Contains(nomePostagem)));
        }
    }
}
