using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class Funcionario_DepartamentoController : ApiController
    {
        private DBModels db = new DBModels();

        // GET: api/Funcionario_Departamento
        public IQueryable<Funcionario_Departamento> GetFuncionario_Departamento()
        {
            return db.Funcionario_Departamento;
        }

        // GET: api/Funcionario_Departamento/5
        [ResponseType(typeof(Funcionario_Departamento))]
        public IHttpActionResult GetFuncionario_Departamento(int id)
        {
            Funcionario_Departamento funcionario_Departamento = db.Funcionario_Departamento.Find(id);
            if (funcionario_Departamento == null)
            {
                return NotFound();
            }

            return Ok(funcionario_Departamento);
        }

        // PUT: api/Funcionario_Departamento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFuncionario_Departamento(int id, Funcionario_Departamento funcionario_Departamento)
        {

            if (id != funcionario_Departamento.Id)
            {
                return BadRequest();
            }

            db.Entry(funcionario_Departamento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Funcionario_DepartamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Funcionario_Departamento
        [ResponseType(typeof(Funcionario_Departamento))]
        public IHttpActionResult PostFuncionario_Departamento(Funcionario_Departamento funcionario_Departamento)
        {

            db.Funcionario_Departamento.Add(funcionario_Departamento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = funcionario_Departamento.Id }, funcionario_Departamento);
        }

        // DELETE: api/Funcionario_Departamento/5
        [ResponseType(typeof(Funcionario_Departamento))]
        public IHttpActionResult DeleteFuncionario_Departamento(int id)
        {
            Funcionario_Departamento funcionario_Departamento = db.Funcionario_Departamento.Find(id);
            if (funcionario_Departamento == null)
            {
                return NotFound();
            }

            db.Funcionario_Departamento.Remove(funcionario_Departamento);
            db.SaveChanges();

            return Ok(funcionario_Departamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Funcionario_DepartamentoExists(int id)
        {
            return db.Funcionario_Departamento.Count(e => e.Id == id) > 0;
        }
    }
}