﻿using System.ComponentModel.DataAnnotations;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.DTOS
{
    public class CriarTarefaDto
    {
        [Required(ErrorMessage ="O titulo é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Data { get; set; }
        
        public EnumStatusTarefa Status { get; set; }
    }
}