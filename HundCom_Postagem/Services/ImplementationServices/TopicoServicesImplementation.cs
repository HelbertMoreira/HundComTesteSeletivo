using AutoMapper;
using FluentResults;
using HundCom_Postagem.Data.Dtos.Topics;
using HundCom_Postagem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HundCom_Postagem.Services.ImplementationServices
{
    public class TopicoServicesImplementation : ITopicoServices
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly AuthenticatedUser _usuario;

        public TopicoServicesImplementation(IMapper mapper, AppDbContext context, AuthenticatedUser usuario)
        {
            _mapper = mapper;
            _context = context;
            _usuario = usuario;
        }       

        public async Task<List<ReadTopcDto>> ListarTodosOsTopicosCadastrados(int? idTopico, string? searchTopico)
        {
            var topicos = from topico
                          in _context.Topicos
                        select topico;

            if (!string.IsNullOrEmpty(searchTopico))
                topicos = topicos.Where(x => x.Tema!.Contains(searchTopico));

            if (idTopico != null)
                topicos = topicos.Where(x => x.Id == idTopico);

            List<Topico> result = await topicos
                .Include(x => x.ListaPostagens)
                .ToListAsync();

            return _mapper.Map<List<ReadTopcDto>>(result);
        }

        public async Task<ReadTopcDto> AdicionaTopico(CreateTopcDto topicoDto)
        {
            var topico = _mapper.Map<Topico>(topicoDto);

            if (!string.IsNullOrEmpty(_usuario.Name))
            {
                topico.Autor = _usuario.Name;
                topico.AutorRole = _usuario.Role;
            }
            else
            {
                topico.Autor = "Visitante";
                topico.AutorRole = "Visitante";
            }
            
            _context.Topicos.Add(topico);
            _context.SaveChanges();
            return _mapper.Map<ReadTopcDto>(topico);
        }

        public Result DeletaTopicoCadastrado(int id)
        {
            Topico topico = _context.Topicos.FirstOrDefault(x => x.Id == id);
            if (topico == null)
            {
                return Result.Fail("Tópico não encontrado");
            }
            _context.Remove(topico);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
