using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class MvcDepartamentoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Esse campo é obrigatório")]
        [StringLength(100,MinimumLength =1)]
        public string Nome { get; set; }
    }
}