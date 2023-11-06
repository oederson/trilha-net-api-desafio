using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.DTOS;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services
{
    public class TarefaServices
    {
        private readonly OrganizadorContext _context;
        private readonly IMapper _mapper;
        public TarefaServices(OrganizadorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public LeiaTarefaDto ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            var tarefa =  _context.Tarefas.Find(id);
            if (tarefa == null)
                return null;
            var tarefaDto = _mapper.Map<LeiaTarefaDto>(tarefa);
            return tarefaDto;
        }
        public List<LeiaTarefaDto> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var tarefa = _context.Tarefas;
            if (tarefa == null)
                return null;
            var tarefaDto = _mapper.Map<List<LeiaTarefaDto>>(tarefa);
            return tarefaDto;
        }
        public List<LeiaTarefaDto> ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            Console.WriteLine(titulo);
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            if (tarefas == null)
                return null;
            var tarefaDto = _mapper.Map<List<LeiaTarefaDto>>(tarefas);
            return tarefaDto;
        }
        public List<LeiaTarefaDto> ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            if (tarefa == null)
                return null;
            var tarefaDto = _mapper.Map<List<LeiaTarefaDto>>(tarefa);
            return tarefaDto;
        }
        public List<LeiaTarefaDto> ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            if (tarefa == null)
                return null;
            var tarefaDto = _mapper.Map<List<LeiaTarefaDto>>(tarefa);
            return tarefaDto;
        }
        public Tarefa Criar(CriarTarefaDto tarefaDto)
        {
            Tarefa tarefa = _mapper.Map<Tarefa>(tarefaDto);
            if (tarefa.Titulo == null || tarefa.Descricao == null || tarefa.Status == null || tarefa.Data == null) return null;
            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            _context.Add(tarefa);
            _context.SaveChanges();
            return tarefa;
        }
        public Tarefa Atualizar(int id, AtualizarTarefaDto tarefaDto)
        {
            var tarefaBanco = _context.Tarefas.FirstOrDefault(tarefaBanco => tarefaBanco.Id == id);
            if (tarefaBanco == null)
                return null;
            _mapper.Map(tarefaDto, tarefaBanco);
            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            _context.Update(tarefaBanco);
            _context.SaveChanges();
            return tarefaBanco;
        }
        public bool Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.FirstOrDefault(tarefaBanco => tarefaBanco.Id == id);

            if (tarefaBanco == null)
                return false;

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Remove(tarefaBanco);
            _context.SaveChanges();
            return true;
        }

    }
}
