using System.ComponentModel.DataAnnotations;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage ="Necessario informar o tiulo")]
        public string Titulo { get; set; }
        [Required(ErrorMessage ="Necessario informar a descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage ="Data requerida")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage ="Necessario informar o status")]
        public EnumStatusTarefa Status { get; set; }
    }
}