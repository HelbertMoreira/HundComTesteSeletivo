using FluentResults;
using HundCom_Postagem.Data.Dtos.Posts;

namespace HundCom_Postagem.Services
{
    public interface IPostagemServices
    {
        Task<ReadPostDto> AdicionaPostagem(CreatePostDto postagemDto);
        Task<List<ReadPostDto>> ListarTodosAsPostagens(int? id, string? searchPosts);
        Result AtualizarPostagemCadastrada(int id, UpdatePostDto postagemDto);
        Result DeletaPostagemCadastrado(int id);
    }
}
