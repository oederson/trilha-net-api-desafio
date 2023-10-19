using AutoMapper;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.DTOS;
using TrilhaApiDesafio.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;
        private readonly IMapper _mapper;

        public TarefaController(OrganizadorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
                return NotFound();
            var tarefaDto = _mapper.Map<LeiaTarefaDto>(tarefa);
            return Ok(tarefaDto);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var tarefa = _context.Tarefas;
            if (tarefa == null)
                return NotFound();
            var tarefaDto = _mapper.Map<List<LeiaTarefaDto>>(tarefa);
            return Ok(tarefaDto);
        }

        [HttpGet("ObterPorTitulo/{titulo}")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            Console.WriteLine(titulo);
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            if (tarefas == null)
                return NotFound();
            var tarefaDto = _mapper.Map<List<LeiaTarefaDto>>(tarefas);
            return Ok(tarefaDto);
        }

        [HttpGet("ObterPorData/{data}")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            if (tarefa == null)
                return NotFound();
            var tarefaDto = _mapper.Map<List<LeiaTarefaDto>>(tarefa);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus/{status}")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            if (tarefa == null)
                return NotFound();
            var tarefaDto = _mapper.Map<List<LeiaTarefaDto>>(tarefa);
            return Ok(tarefaDto);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] CriarTarefaDto tarefaDto)
        {
            Tarefa tarefa = _mapper.Map<Tarefa>(tarefaDto);

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            _context.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }


        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] AtualizarTarefaDto tarefaDto)
        {
            var tarefaBanco = _context.Tarefas.FirstOrDefault(tarefaBanco => tarefaBanco.Id == id);

            if (tarefaBanco == null)
                return NotFound();

            _mapper.Map(tarefaDto, tarefaBanco);
            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            _context.SaveChanges();
            return Ok(tarefaBanco);
        }
        [HttpPatch("atualizarParte/{id}")]
        public IActionResult AtualizarParcial(int id, [FromBody] JsonPatchDocument <AtualizarTarefaDto> patch)
        {
            var tarefaBanco = _context.Tarefas.FirstOrDefault(tarefaBanco => tarefaBanco.Id == id);

            if (tarefaBanco == null)
                return NotFound();
            var tarefaParaAtualizar = _mapper.Map<AtualizarTarefaDto>(tarefaBanco);
            patch.ApplyTo(tarefaParaAtualizar, ModelState);
            if(!TryValidateModel(tarefaParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(tarefaParaAtualizar, tarefaBanco);
            _context.SaveChanges();
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.FirstOrDefault(tarefaBanco => tarefaBanco.Id == id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
