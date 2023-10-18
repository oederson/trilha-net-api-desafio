﻿using System.ComponentModel.DataAnnotations;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        [Required(ErrorMessage ="Data requerida")]
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
    }
}