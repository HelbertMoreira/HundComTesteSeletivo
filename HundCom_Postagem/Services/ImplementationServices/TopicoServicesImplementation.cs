using AutoMapper;
using FluentResults;
using HundCom_Postagem.Data.Dtos.Posts;
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

        public List<ReadTopcDto> ListarTodosOsTopicosCadastrados()
        {
            var topicos = from obj
                          in _context.Topicos
                        select obj;

            return _mapper.Map<List<ReadTopcDto>>(topicos
                .Include(x => x.ListaPostagens)
                .ToList());
        }

        public ReadTopcDto AdicionaTopico(CreateTopcDto topicoDto)
        {
            Topico topico = _mapper.Map<Topico>(topicoDto);
            
            if (!string.IsNullOrEmpty(_usuario.Name))
            {
                topico.Usuario = _usuario.Name;
                topico.UsuarioRole = _usuario.Role;
            }
            else
            {
                topico.Usuario = "Guest";
                topico.UsuarioRole = "Convidado";
            }
            
            _context.Topicos.Add(topico);
            _context.SaveChanges();
            return _mapper.Map<ReadTopcDto>(topico);
        }

        public ReadTopcDto BuscarTopicoCadastradosPorId(int id)
        {
            return ListarTodosOsTopicosCadastrados().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<ReadTopcDto>> BuscarTopicoCadastradosPorNome(string nomeTopico)
        {
            var topicos = from obj
                          in _context.Topicos
                          select obj;

            if (!string.IsNullOrEmpty(nomeTopico))
            {
                topicos = topicos.Where(x => x.Tema!.Contains(nomeTopico));
            }

            return _mapper.Map<List<ReadTopcDto>>(await topicos
                .Include(x => x.ListaPostagens)
                .ToListAsync());
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
