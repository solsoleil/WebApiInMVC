using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class mvcFuncionarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Esse campo é obrigatório")]
        [StringLength(200, MinimumLength = 1)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [StringLength(20, MinimumLength = 1)]
        public string Login { get; set; }
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [StringLength(20, MinimumLength = 1)]
        public string Senha { get; set; }
    }
}