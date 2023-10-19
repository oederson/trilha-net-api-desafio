using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrilhaApiDesafio.Context;
namespace TesteAgenda
{
    public class TesteAgenda
    {
        [Fact]
        public void TestaConexaoComBancoDeDadosSqlServer()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ConexaoPadrao");

            var options = new DbContextOptionsBuilder<OrganizadorContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var contexto = new OrganizadorContext(options))
            {
                bool conectado;

                // Act
                try
                {
                    conectado = contexto.Database.CanConnect();
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível conectar ao banco de dados de teste");
                }

                // Assert
                Assert.True(conectado);
            }
        }
    }
}