using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        public ActionResult Index()
        {
            //Pega todas as instancias de departamento na tabela Departamento e cria a view 
            IEnumerable<MvcDepartamentoModel> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Departamento").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<MvcDepartamentoModel>>().Result;
            return View(empList);
        }

        public ActionResult AddorEdit(int id = 0)
        {
            //Caso o id retornado da view seja 0, significa que é uma criaçao de dados
            //Cria a view com um model vazio
            if(id==0)
            {
                //Cria a view com um model vazio
                return View(new MvcDepartamentoModel());
            }
            else
            {
                //Caso o id retornado seja != 0, significa que é o update da tabela

                //Primeiro procura os dados na tabela que sejam iguais ao id retornado
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Departamento/"+id.ToString()).Result;
                //Cria a view enviando os dados da tabela
                return View(response.Content.ReadAsAsync<MvcDepartamentoModel>().Result);
            }
        }
        [HttpPost]

        public ActionResult AddorEdit(MvcDepartamentoModel dep)
        {
            //Se o id retornado for 0, significa que é uma criaçao de dados
            if(dep.Id == 0)
            {
                //Adiciona os valores na tabela
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Departamento", dep).Result;
                //Envia uma msg de sucesso
                TempData["SuccessMessage"] = "Salvo com sucesso!";
            }
            else
            {
                //Caso o id retornado seja != 0, significa que é o update da tabela

                //Atualiza os valores na tavela pegando os dados recebidos da view
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Departamento/"+dep.Id, dep).Result;
                //Envia uma msg de sucesso
                TempData["SuccessMessage"] = "Atualizado com sucesso!";
            }
            //Redireciona para a tela Departamento/Index
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            //Deleta a instancia com a id retornada
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Departamento/"+id.ToString()).Result;
            //Envia uma msg de sucesso
            TempData["SuccessMessage"] = "Deletado com sucesso!";
            //Redireciona para a tela Departamento/Index
            return RedirectToAction("Index");

        }
    }
}