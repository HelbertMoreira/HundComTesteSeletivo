using FluentResults;
using HundCom_Postagem.Data.Dtos.Topics;
using HundCom_Postagem.Models.ViewModels;

namespace HundCom_Postagem.Services
{
    public interface ITopicoServices
    {
        ReadTopcDto AdicionaTopico(CreateTopcDto topicoDto);
        List<ReadTopcDto> ListarTodosOsTopicosCadastrados();
        Task<List<ReadTopcDto>> BuscarTopicoCadastradosPorNome(string nomeTopico);
        ReadTopcDto BuscarTopicoCadastradosPorId(int id);
        Result DeletaTopicoCadastrado(int id);
    }
}
