using System.ComponentModel.DataAnnotations;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.DTOS
{
    public class LeiaTarefaDto 
    { 
        public int Id { get; set; }
        public string Titulo { get; set; }
        
        public string Descricao { get; set; }
        
        public DateTime Data { get; set; }
        [EnumDataType(typeof(EnumStatusTarefa))]
        public string? Status { get; set; }
    }
}
