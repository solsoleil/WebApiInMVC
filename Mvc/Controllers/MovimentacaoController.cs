using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class MovimentacaoController : Controller
    {
        // GET: Movimentacao
        public ActionResult Index()
        {
            //Pega todos os dados da tabela Movimentacao
            IEnumerable<MvcMovimentacaoModel> movList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Movimentacao").Result;
            movList = response.Content.ReadAsAsync<IEnumerable<MvcMovimentacaoModel>>().Result;
            //Envia os dados para a view
            return View(movList);
        }
        

        public ActionResult AddOrEdit(int id = 0)
        {
            //Se o id = 0, significa que é uma criação
            if(id==0)
            {
                //envia para a view um model vazio
                return View(new MvcMovimentacaoModel());
            }
            else
            {
                //se o id != 0, significa que é um update
                //procura os dados com o id recebido na tabela Movimentacao
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Movimentacao/"+id.ToString()).Result;
                //envia os dados encontrados para a view
                return View(response.Content.ReadAsAsync<MvcMovimentacaoModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(MvcMovimentacaoModel mov)
        {
            //Verifica se existe um usuario logado
            if(Session["UserId"] != null )
            {
                //Se o id == 0, significa que é a criaçao de um novo dado
                if(mov.Id ==0)
                {
                    //envia o id do usuario logado para o model
                    mov.FuncionarioID = int.Parse(Session["UserId"].ToString());
                    //Adiciona o novo dado na tabela Movimentacao
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Movimentacao", mov).Result;
                    //Cria uma mensagem de sucesso
                    TempData["SuccessMessage"] = "Movimentacao realizada";
                }
                else
                {
                    //if id != 0, significa que é a atualizacao de um dado na tabela

                    //envia o id do usuario logado para o model
                    mov.FuncionarioID = int.Parse(Session["UserId"].ToString());
                    //Atualiza o dado pelo id recebido na tabela Movimentacao
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Movimentacao/"+mov.Id, mov).Result;
                    //Cria uma mensagem de sucesso
                    TempData["SuccessMessage"] = "Movimentacao atualizada";

                }

            }
            else
            {
                //Caso não exista um usuario logado, envia uma mensagem de error
                TempData["FailMessage"] = "Movimentacao nãp realizada! Usuário não logado!";
                TempData["SuccessMessage"] = "Movimentacao não realizada! Usuário não logado!";
            }
            //redireciona para a janela Movimentacao/Index
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            //Deleta os dados com o id recebido na tabela Movimentacao
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Movimentacao/"+id.ToString()).Result;
            //Cria uma mensagem de sucesso
            TempData["SuccessMessage"] = "Movimentacao deletada!";
            //redireciona para a janela Movimentacao/Index
            return RedirectToAction("Index");
        }
    }
}