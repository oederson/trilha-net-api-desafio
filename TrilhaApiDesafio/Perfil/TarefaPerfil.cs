using AutoMapper;
using TrilhaApiDesafio.DTOS;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Perfil
{
    public class TarefaPerfil : Profile
    {
        public TarefaPerfil()
        {
            CreateMap<CriarTarefaDto, Tarefa>();
            CreateMap<AtualizarTarefaDto, Tarefa>();
            CreateMap<Tarefa, AtualizarTarefaDto>();
            CreateMap<Tarefa, LeiaTarefaDto>();
        }
    }
}
