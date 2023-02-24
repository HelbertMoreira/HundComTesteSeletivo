using FluentResults;
using HundCom_Postagem.Data.Dtos.Topics;

namespace HundCom_Postagem.Services
{
    public interface ITopicoServices
    {
        Task<ReadTopcDto> AdicionaTopico(CreateTopcDto topicoDto);
        Task<List<ReadTopcDto>> ListarTodosOsTopicosCadastrados(int? idTopico, string? searchTopico);
        Result DeletaTopicoCadastrado(int id);
    }
}
