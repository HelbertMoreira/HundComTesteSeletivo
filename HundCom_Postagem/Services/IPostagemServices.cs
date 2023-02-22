using FluentResults;
using HundCom_Postagem.Data.Dtos.Posts;

namespace HundCom_Postagem.Services
{
    public interface IPostagemServices
    {
        ReadPostDto AdicionaPostagem(CreatePostDto postagemDto);
        List<ReadPostDto> ListarTodosAsPostagensCadastrados(int? id, string? searchPosts);
        IEnumerable<ReadPostDto> ListarPostagemCadastradosPorNome(string nomePostagem);
        Result AtualizarPostagemCadastrada(int id, UpdatePostDto postagemDto);
        Result DeletaPostagemCadastrado(int id);
    }
}
