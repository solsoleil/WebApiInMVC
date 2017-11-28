using Mvc.Models;
using Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Deslogar()
        {
            if (Session["UserId"] != null)
            {
                Session["UserId"] = null;
                Session["UserName"] = null;
                Session.Clear();
            }
            else
            {
            }
            return RedirectToAction("Index");
        }


        public ActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logar(mvcFuncionarioModel user)
        {

            HttpResponseMessage responseAllFunc = GlobalVariables.WebApiClient.GetAsync("Funcionario").Result;
            IEnumerable<mvcFuncionarioModel> allFunc = responseAllFunc.Content.ReadAsAsync<IEnumerable<mvcFuncionarioModel>>().Result;
            mvcFuncionarioModel novoFunc = new mvcFuncionarioModel();

            foreach (var allFuncAux in allFunc)
            {
               if(allFuncAux.Login == user.Login && allFuncAux.Senha == user.Senha)
                {
                    novoFunc = allFuncAux;
                    Session["UserId"] = novoFunc.Id;
                    Session["UserName"] = novoFunc.Nome;
                }
               else
                {

                }
            }

            if(novoFunc.Id > 0)
            {
                return RedirectToAction("LoggedIn");
            }
            else
            {
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if(Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logar");
            }
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            FuncionarioIndexData viewModel = new FuncionarioIndexData();
            if (id == 0)
            {
                HttpResponseMessage response2 = GlobalVariables.WebApiClient.GetAsync("Departamento").Result;
                viewModel.Departamentos = response2.Content.ReadAsAsync<IEnumerable<MvcDepartamentoModel>>().Result;
                return View(viewModel);
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Funcionario/" + id.ToString()).Result;
                HttpResponseMessage response2 = GlobalVariables.WebApiClient.GetAsync("Departamento").Result;
                //viewModel.Funcionarios = response.Content.ReadAsAsync<IEnumerable<mvcFuncionarioModel>>().Result;
                viewModel.FuncionarioSelecionado = response.Content.ReadAsAsync<mvcFuncionarioModel>().Result;
                viewModel.Departamentos = response2.Content.ReadAsAsync<IEnumerable<MvcDepartamentoModel>>().Result;
                //return View(response.Content.ReadAsAsync<mvcFuncionarioModel>().Result);
                return View(viewModel);

            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(FuncionarioIndexData func)
        {
            //GET ALL FUNCIONARIO_DEPARTAMENTOS e armazena na variavel allFuncDep
            HttpResponseMessage responseAllFuncDep = GlobalVariables.WebApiClient.GetAsync("Funcionario_Departamento").Result;
            IEnumerable<MvcFuncionario_Departamento> allFuncDep = responseAllFuncDep.Content.ReadAsAsync<IEnumerable<MvcFuncionario_Departamento>>().Result;

            //Preapara uma lista para armazenar os funcionario_departamento que possuam id do funcionario
            List<MvcFuncionario_Departamento> depComFunc = new List<MvcFuncionario_Departamento>();
            mvcFuncionarioModel novoFunc = new mvcFuncionarioModel();

            //GET ALL FUNCIONARIOS
            
            var func_ = func.FuncionarioSelecionado;

            //Variavel para auxilar na criação de novo funcionario 
            //(Vai armazenar o funcionario novo que foi criado pra auxiliar na adiciao na tabela funcionario_departamento
            var funcDep_ = new MvcFuncionario_Departamento();

            //Pega os valores selecionados dos departamentos na criação de um novo funcionario 
            var mystr = Request.Form["departamentos"];

            //Verifica se algum departamento foi selecionado
            //Se sim => começa a adicao nas tabelas
            //Se não (== null) gera um erro
            if (mystr == null)
            {
                TempData["FailMessage"] = "Não foi possível cadastrar o usuário! É necessário escolher um departamento!";
            }
            else
            {
                //Pega os divide o retorno dos departamentos em uma String[]
                var depValues2 = mystr.Split(',');

                //Armazena o id do funcionario na variavel criada para auxiliar a adicao na tabela Funcionario_departamento
                funcDep_.FuncionarioID = func_.Id;

                //Em todas as instancias de Funcionario_departamento 
                //=> procura as que possuem o id do funcionario e armazena eles na variavel depComFunc
                foreach (var dep in allFuncDep)
                {
                    if (dep.FuncionarioID == func_.Id)
                    {
                        depComFunc.Add(dep);
                    }
                }
                //Caso o funcionario ainda não possua id, significa que é a criação de um novo
                if (func_.Id == 0)
                {


                    //Verifica se já existe instancia na tabela com o mesmo login

                    HttpResponseMessage responseAllFunc = GlobalVariables.WebApiClient.GetAsync("Funcionario").Result;
                    IEnumerable<mvcFuncionarioModel> allFunc = responseAllFunc.Content.ReadAsAsync<IEnumerable<mvcFuncionarioModel>>().Result;

                    bool verificadorCreate = true;

                    foreach (var funcAux in allFunc)
                    {
                        if (funcAux.Login == func_.Login)
                        {
                            verificadorCreate = false;
                        }
                    }
                    //Se VerificadorCreate == false, significa que já existe um funcionario com o login registrado
                    if(verificadorCreate == false)
                    {
                        TempData["FailMessage"] = "Não foi possível cadastrar o usuário! Um funcionário já está usando esse Login!";
                    }
                    else
                    {
                        //Adiciona o funcionario na tabela Funcionario
                        HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Funcionario", func_).Result;

                        //GET ALL FUNCIONARIOS (novamente) e armazena na variavel allFunc
                        responseAllFunc = GlobalVariables.WebApiClient.GetAsync("Funcionario").Result;
                        allFunc = responseAllFunc.Content.ReadAsAsync<IEnumerable<mvcFuncionarioModel>>().Result;

                        //Passa por todos os funcionario e procura os que possuem o mesmo login da variavel que o usuario passou na criação/edicao do funcionario
                        //Ou seja, na label Login de criação
                        //Então armazena ele na variavel novoFunc
                        foreach (var funcAux in allFunc)
                        {
                            if (funcAux.Login == func_.Login)
                            {
                                novoFunc = funcAux;
                            }
                        }
                        //Armazena a variavel do usuario criado na funcDep_.Funcionario (que sera utilizado para a adicao na tabela Funcionario_Departamento
                        funcDep_.FuncionarioID = novoFunc.Id;
                        //Passa por todas as instancias de Departamentos selecionados adicionando na tabela Funcionario_Departamento
                        for (int i = 0; i < depValues2.Length; i++)
                        {
                            funcDep_.DepartamentoID = Convert.ToInt32(depValues2[i]);
                            HttpResponseMessage responseDep = GlobalVariables.WebApiClient.PostAsJsonAsync("Funcionario_Departamento", funcDep_).Result;
                        }
                        TempData["SuccessMessage"] = "Salvo com sucesso!";
                    }
                }
                else
                {
                    //Caso o func_id != 0, significa que é uma atualizacão na tabela

                    //OBS: Como todos os usuarios efetuam todas as açoes, não é necessario fazer uma validação de quem esta atualizando a tabela 

                    //Atualiza as informacoes do funcionario selecionado na tabela Funcionario
                    HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Funcionario/" + func_.Id, func_).Result;
                    //Pega as informacoes na tabela Funcionario_Departamento que possuem 
                    //o id do funcionario sendo atualizado e os deleta para criar um novo com os valores atualizados
                    for (int i = 0; i < depComFunc.Count; i++)
                    {
                        HttpResponseMessage responseDelete = GlobalVariables.WebApiClient.DeleteAsync("Funcionario_Departamento/" + depComFunc[i].id.ToString()).Result;

                    }
                    for (int i = 0; i < depValues2.Length; i++)
                    {
                        funcDep_.DepartamentoID = Convert.ToInt32(depValues2[i]);
                        HttpResponseMessage responseDep = GlobalVariables.WebApiClient.PostAsJsonAsync("Funcionario_Departamento", funcDep_).Result;
                    }



                    TempData["SuccessMessage"] = "Atualizado com sucesso!";
                }
            }
            return RedirectToAction("Index");
        }
    }
}