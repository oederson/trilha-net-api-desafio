using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TesteEuEstudo.FakesParaMock
{
    public class AppDbContextTest
    {
        private readonly OrganizadorContext _dbContext;
        public AppDbContextTest()
        {
            var options = new DbContextOptionsBuilder<OrganizadorContext>()
            .UseInMemoryDatabase(databaseName: "NaMemoriaAppDatabase")
            .Options;

            _dbContext = new OrganizadorContext(options);

            InitializeData();

        }
        private void InitializeData()
        {
            var tarefa1 = new Tarefa
            {
                Titulo = "Completar o projeto",
                Descricao = "Concluir todas as tarefas do projeto X",
                Data = DateTime.Now.AddDays(7), 
                Status = EnumStatusTarefa.Pendente
            };

            var tarefa2 = new Tarefa
            {
                Titulo = "Reunião com a equipe",
                Descricao = "Agendar uma reunião com a equipe de desenvolvimento",
                Data = DateTime.Now.AddDays(2), 
                Status = EnumStatusTarefa.Pendente
            };

            var tarefa3 = new Tarefa
            {
                Titulo = "Preparar apresentação",
                Descricao = "Preparar slides e documentos para a apresentação",
                Data = new DateTime(2023, 12, 30), 
                Status = EnumStatusTarefa.Pendente
            };
            _dbContext.Tarefas.Add(tarefa1);
            _dbContext.Tarefas.Add(tarefa2);
            _dbContext.Tarefas.Add(tarefa3);
            _dbContext.SaveChanges();

        }

        public OrganizadorContext GetContext()
        {
            return _dbContext;
        }
    }
}
