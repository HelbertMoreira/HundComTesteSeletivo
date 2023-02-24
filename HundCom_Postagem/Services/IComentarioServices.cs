using FluentResults;
using HundCom_Postagem.Data.Dtos.Comments;

namespace HundCom_Postagem.Services
{
    public interface IComentarioServices
    {
        ReadCommentDto AdicionarComentario(CreateCommentDto comentarioDto);
        Task<List<ReadCommentDto>> ListarComentariosPorPostagem(int? idPostagem);
        Task<List<ReadCommentDto>> ListarComentariosPorUsuario(string autor);
        Result DeletaComentarioCadastrado(int id);
        ReadCommentDto ListarComentariosPorId(int id);
    }
}
