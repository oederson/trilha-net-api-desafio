using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteEuEstudo.FakesParaMock;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Controllers;
using TrilhaApiDesafio.DTOS;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Services;

namespace TesteAgenda
{
    public class TesteAgenda
    {
        private readonly OrganizadorContext _context;        
        private readonly TarefaServices _tarefaServices;
        private readonly TarefaController _tarefaController;
        private readonly IMapper mapper;
        public TesteAgenda()
        {
            var initializer = new AppDbContextTest();
            _context = initializer.GetContext();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CriarTarefaDto, Tarefa>();
                cfg.CreateMap<AtualizarTarefaDto, Tarefa>();
                cfg.CreateMap<LeiaTarefaDto, Tarefa>();
                cfg.CreateMap<Tarefa, LeiaTarefaDto>();
            });
            
            mapper = configuration.CreateMapper();
            _tarefaServices = new TarefaServices(_context, mapper);
            _tarefaController = new TarefaController(_tarefaServices);
            
        }
        [Fact]
        public void  ObterTarefaPorId()
        {
            //Arrange o banco de dados em memoria tem 3 tarefas cadastradas
            //Act
            var result = _tarefaController.ObterPorId(2);
            //Assert
            Assert.IsType<OkObjectResult>(result);
            
        }
        [Fact]
        public void NaoDeveObterTarefaPorId()
        {
            //Arrange o banco de dados em memoria tem 3 tarefas cadastradas
            //Act
            var result = _tarefaController.ObterPorId(99999);
            //Assert
            Assert.IsType<NotFoundResult>(result);           
        }
        [Fact]
        public void ObterTodasTarefas()
        {
            //Arrange o banco de dados em memoria tem 3 tarefas cadastradas
            //Act
            var result = _tarefaController.ObterTodos();
            //Assert
            Assert.IsType<OkObjectResult>(result);            
        }
        [Fact]
        public void ObterTarefaPorTitulo()
        {
            //Arrange o banco de dados em memoria tem 1 tafera com o titulo "Completar o projeto" cadastrada
            //Act
            var result = _tarefaController.ObterPorTitulo("Completar o projeto");
            //Assert
            Assert.IsType<OkObjectResult>(result);            
        }
        [Fact]
        public void ObterTarefaPorTituloQueNaoExiste()
        {
            //Arrange o banco de dados em memoria nao tem 1 tafera com o titulo "Nao deve encontrar" cadastrada
            //Act
            var result = _tarefaController.ObterPorTitulo("Nao deve encontrar");
            //Assert
            Assert.IsType<NotFoundResult>(result);            
        }
        [Fact]
        public void ObterPorData()
        {
            //Arrange o banco de dados em memoria tem 1 tafera com a data  "2023-12-30" cadastrada
            DateTime data = new DateTime(2023, 12, 30);
            //Act
            var res = _tarefaController.ObterPorData(data);
            //Assert
            Assert.IsType<OkObjectResult>(res);            
        }
        [Fact]
        public void NaoDeveObterPorData()
        {
            //Arrange o banco de dados em memoria nao tem 1 tafera com a data  "2024-12-30" cadastrada
            DateTime data = new DateTime(2029, 12, 30);
            //Act
            var res = _tarefaController.ObterPorData(data);
            //Assert
            Assert.IsType<NotFoundResult>(res);            
        }
        [Fact]
        public void ObterPorStatus()
        {
            //Arrange o banco de dados em memoria tem 3 taferas como pendente

            //Act
            var res = _tarefaController.ObterPorStatus(EnumStatusTarefa.Pendente);

            //Assert
            Assert.IsType<OkObjectResult>(res);            
        }
        [Fact]
        public void ObterPorStatusDeveRetornarNotFound()
        {
            //Arrange o banco de dados em memoria nao tem taferas como Finalizado

            //Act
            var res = _tarefaController.ObterPorStatus(EnumStatusTarefa.Finalizado);

            //Assert
            Assert.IsType<NotFoundResult>(res);           
        }
        [Fact]
        public void CriarTarefa()
        {
            //Arrange
            CriarTarefaDto tarefaDto = new CriarTarefaDto() 
            {
                Titulo = "Teste",
                Descricao = "Teste",
                Data = DateTime.Now.AddDays(2),
                Status = EnumStatusTarefa.Pendente
            };
            //Act
            var tarefa = _tarefaController.Criar(tarefaDto);
            //Assert
            Assert.IsType<CreatedAtActionResult>(tarefa);            
        }
        [Fact]
        public void NaoDeveCriarTarefaSeAlgumCampoForNulo()
        {
            //Arrange
            CriarTarefaDto tarefaDto = new CriarTarefaDto();

            //Act
            var tarefa = _tarefaController.Criar(tarefaDto);
            //Assert
            Assert.IsType<NoContentResult>(tarefa);            
        }
        [Fact]
        public void AtualizarTarefa()
        {
            //Arrange
            int id = 3;
            AtualizarTarefaDto tarefaDto = new AtualizarTarefaDto()
            {
                Titulo = "Atualizando",
                Descricao = "Atualizando",
                Status = EnumStatusTarefa.Pendente
            }; 
            //Act
            var tarefaBanco = _tarefaController.Atualizar(id, tarefaDto);
            //Assert
            Assert.IsType<OkObjectResult>(tarefaBanco);           
        }
        [Fact]
        public void NaoDeveAtualizarTarefaSeNaoExistir()
        {
            //Arrange
            int id = 999;
            AtualizarTarefaDto tarefaDto = new AtualizarTarefaDto()
            {
                Titulo = "Atualizando",
                Descricao = "Atualizando",
                Status = EnumStatusTarefa.Finalizado
            };
            //Act
            var tarefaBanco = _tarefaController.Atualizar(id, tarefaDto);
            //Assert
            Assert.IsType<NotFoundResult>(tarefaBanco);            
        }
        [Fact]
        public void DeletarPorID()
        {
            //Arrange
            int id = 1;
            //Act
            var result = _tarefaController.Deletar(id);
            //Assert
            Assert.IsType<NoContentResult>(result);            
        }
        [Fact]
        public void NaoDeveDeletarUmIdInexistente()
        {
            //Arrange
            int id = 100;
            //Act
            var result = _tarefaController.Deletar(id);
            //Assert
            Assert.IsType<NotFoundResult>(result);           
        }

    }
}