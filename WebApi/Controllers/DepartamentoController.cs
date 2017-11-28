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
    public class DepartamentoController : ApiController
    {
        private DBModels db = new DBModels();

        // GET: api/Departamento
        public IQueryable<Departamento> GetDepartamento()
        {
            return db.Departamento;
        }

        // GET: api/Departamento/5
        [ResponseType(typeof(Departamento))]
        public IHttpActionResult GetDepartamento(int id)
        {
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return NotFound();
            }

            return Ok(departamento);
        }

        // PUT: api/Departamento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartamento(int id, Departamento departamento)
        {

            if (id != departamento.Id)
            {
                return BadRequest();
            }

            db.Entry(departamento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
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

        // POST: api/Departamento
        [ResponseType(typeof(Departamento))]
        public IHttpActionResult PostDepartamento(Departamento departamento)
        {

            db.Departamento.Add(departamento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = departamento.Id }, departamento);
        }

        // DELETE: api/Departamento/5
        [ResponseType(typeof(Departamento))]
        public IHttpActionResult DeleteDepartamento(int id)
        {
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return NotFound();
            }

            db.Departamento.Remove(departamento);
            db.SaveChanges();

            return Ok(departamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartamentoExists(int id)
        {
            return db.Departamento.Count(e => e.Id == id) > 0;
        }
    }
}