using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class MvcMovimentacao_Funcionario
    {
        public int Id { get; set; }
        public int FuncionarioID { get; set; }
        public int MovimentacaoID { get; set; }
    }
}