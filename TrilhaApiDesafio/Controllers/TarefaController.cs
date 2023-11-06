using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.DTOS;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Services;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaServices _services;

        public TarefaController(TarefaServices services)
        {
            _services = services;   
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var res = _services.ObterPorId(id);
            return res == null ? NotFound() : Ok(res);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var res = _services.ObterTodos();
            return res == null ? NotFound() : Ok(res);
        }

        [HttpGet("ObterPorTitulo/{titulo}")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var res = _services.ObterPorTitulo(titulo);
            return res.Count() == 0 ? NotFound() : Ok(res);
        }

        [HttpGet("ObterPorData/{data}")]
        public IActionResult ObterPorData(DateTime data)
        {
            var res = _services.ObterPorData(data);
            return res.Count() == 0 ? NotFound() : Ok(res);
        }

        [HttpGet("ObterPorStatus/{status}")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var res = _services.ObterPorStatus(status);
            return res.Count() == 0 ? NotFound() : Ok(res);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] CriarTarefaDto tarefaDto)
        {
            var tarefa = _services.Criar(tarefaDto);
            return tarefa == null? NoContent() : CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] AtualizarTarefaDto tarefaDto)
        {
            var tarefaBanco = _services.Atualizar(id, tarefaDto);          
            return tarefaBanco == null? NotFound(): Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            return _services.Deletar(id) ? NoContent() : NotFound();
        }
    }
}
