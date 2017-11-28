using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class MvcFuncionario_Departamento
    {
        public int id { get; set; }
        public int FuncionarioID { get; set; }
        public int DepartamentoID { get; set; }
    }
}