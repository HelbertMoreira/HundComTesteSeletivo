using AutoMapper;
using FluentResults;
using HundCom_Postagem.Data.Dtos.Comments;
using HundCom_Postagem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HundCom_Postagem.Services.ImplementationServices
{
    public class ComentarioServicesImplementation : IComentarioServices
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly AuthenticatedUser _usuario;

        public ComentarioServicesImplementation(IMapper mapper, AppDbContext context, AuthenticatedUser usuario = null)
        {
            _mapper = mapper;
            _context = context;
            _usuario = usuario;
        }

        public ReadCommentDto AdicionarComentario(CreateCommentDto comentarioDto)
        {
            var comentario = _mapper.Map<Comentario>(comentarioDto);

            var usuarioLogado = string.IsNullOrEmpty(_usuario.Name) ? "Visitante" : _usuario.Name;

            comentario.Autor = usuarioLogado;
            comentario.DataComentario = DateTime.Now;
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();

            ReadCommentDto ReadComentarioDto = _mapper.Map<ReadCommentDto>(comentario);

            return ReadComentarioDto;
        }
        

        public async Task<List<ReadCommentDto>> ListarComentariosPorPostagem(int? idPostagem)
        {
            var comentarios = 
                from comentario
                in _context.Comentarios
                select comentario;

            if (idPostagem != null)
                comentarios = comentarios.Where(x => x.PostagemId == idPostagem);            

            var result = await comentarios
                .Include(x => x.Postagem)
                .ToListAsync();

            return _mapper.Map<List<ReadCommentDto>>(result);
        }

        public async Task<List<ReadCommentDto>> ListarComentariosPorUsuario(string autor)
        {
            var comentarios =
                from comentario
                in _context.Comentarios
                select comentario;

            if (string.IsNullOrEmpty(autor))
                comentarios = comentarios.Where(x => x.Autor == autor);

            var result = await comentarios
                .Include(x => x.Postagem)
                .ToListAsync();

            return _mapper.Map<List<ReadCommentDto>>(result);
        }

        public Result DeletaComentarioCadastrado(int id)
        {
            var comentario = _context.Comentarios.FirstOrDefault(x => x.Id == id);

            if (comentario == null)
                return Result.Fail("Comentário não encontrado");

            _context.Remove(comentario);
            _context.SaveChanges();
            return Result.Ok();
        }

        public ReadCommentDto ListarComentariosPorId(int id)
        {
            var readComentDto = _context.Comentarios.FirstOrDefault(x => x.Id == id);
            string UsuarioLogado = _usuario.Name;
                        
            if (!string.IsNullOrEmpty(UsuarioLogado))
            {
                if (UsuarioLogado == readComentDto?.Autor)
                    return null;
            }
            return _mapper.Map<ReadCommentDto>(readComentDto);

        }
    }
}
