using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class MvcMovimentacaoModel
    {
        public int Id { get; set; }
        public int FuncionarioID { get; set; }
        [Required(ErrorMessage ="Esse campo é obrigadório")]
        [StringLength(500, MinimumLength = 1)]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Esse campo é obrigadório")]
        public decimal Valor { get; set; }
    }
}