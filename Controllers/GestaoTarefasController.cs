using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    public class GestaoTarefasController : Controller
    {
        private readonly OrganizadorContext _db;
        public GestaoTarefasController(OrganizadorContext db)
        {
            _db = db;            
        }
        public IActionResult Index()
        { 

            IEnumerable<Tarefa> tarefas = _db.Tarefas;
            return View(tarefas);
        }
        public IActionResult Cadastrar() 
        {
            return View();
        }
    }
}
