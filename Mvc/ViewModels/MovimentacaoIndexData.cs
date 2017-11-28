using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.ViewModels
{
    public class MovimentacaoIndexData
    {
        public IEnumerable<mvcFuncionarioModel> Funcionarios { get; set; }
        public IEnumerable<MvcMovimentacaoModel> Movimentacao { get; set; }
    }
}