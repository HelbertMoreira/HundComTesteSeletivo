using AutoMapper;
using FluentResults;
using HundCom_Postagem.Data.Dtos.Comments;
using HundCom_Postagem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HundCom_Postagem.Services.ImplementationServices
{
    public class ComentarioServicesImplementation : IComentarioServices
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public ComentarioServicesImplementation(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ReadCommentDto AdicionarComentario(CreateCommentDto comentarioDto)
        {
            Comentario comentario = _mapper.Map<Comentario>(comentarioDto);
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();
            return _mapper.Map<ReadCommentDto>(comentario);
        }

        public Result DeletaComentarioCadastrado(int id)
        {
            Comentario comentario = _context.Comentarios.FirstOrDefault(x => x.Id == id);            
            if (comentario == null)
                return Result.Fail("Comentário não encontrado");
            
            _context.Remove(comentario);
            _context.SaveChanges();
            return Result.Ok();
        }

        public List<ReadCommentDto> ListarComentariosPorPostagem(int idPostagem)
        {
            List<ReadCommentDto> comentarios = _mapper.Map<List<ReadCommentDto>>(_context.Comentarios.ToList());
            
            if (idPostagem != null)            
                return (List<ReadCommentDto>)comentarios.Where(x => x.PostagemId == idPostagem);

            return comentarios;
        }

        //public List<ReadCommentDto> ListarComentariosPorUsuario(int idUsuario)
        //{
        //    List<ReadCommentDto> comentarios = _mapper.Map<List<ReadCommentDto>>(_context.Comentarios.ToList());
            
        //    if (idUsuario != null)
        //        return (List<ReadCommentDto>)comentarios.Where(x => x.UserId == idUsuario);

        //    return comentarios;
        //}
    }
}
