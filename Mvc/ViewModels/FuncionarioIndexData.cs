using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.ViewModels
{
    public class FuncionarioIndexData
    {
        public IEnumerable<mvcFuncionarioModel> Funcionarios { get; set; }
        public IEnumerable<MvcDepartamentoModel> Departamentos { get; set; }
        public mvcFuncionarioModel FuncionarioSelecionado { get; set; }
    }
}