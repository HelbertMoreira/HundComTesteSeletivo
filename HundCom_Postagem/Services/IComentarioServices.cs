using FluentResults;
using HundCom_Postagem.Data.Dtos.Comments;
using HundCom_Postagem.Models.Entities;

namespace HundCom_Postagem.Services
{
    public interface IComentarioServices
    {
        ReadCommentDto AdicionarComentario(CreateCommentDto comentarioDto);
        //List<ReadCommentDto> ListarComentariosPorUsuario(int idUsuario);
        List<ReadCommentDto> ListarComentariosPorPostagem(int idPostagem);
        Result DeletaComentarioCadastrado(int id);
    }
}
