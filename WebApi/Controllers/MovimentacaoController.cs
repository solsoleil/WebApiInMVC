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
    public class MovimentacaoController : ApiController
    {
        private DBModels db = new DBModels();

        // GET: api/Movimentacao
        public IQueryable<Movimentacao> GetMovimentacao()
        {
            return db.Movimentacao;
        }

        // GET: api/Movimentacao/5
        [ResponseType(typeof(Movimentacao))]
        public IHttpActionResult GetMovimentacao(int id)
        {
            Movimentacao movimentacao = db.Movimentacao.Find(id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return Ok(movimentacao);
        }

        // PUT: api/Movimentacao/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovimentacao(int id, Movimentacao movimentacao)
        {

            if (id != movimentacao.Id)
            {
                return BadRequest();
            }

            db.Entry(movimentacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimentacaoExists(id))
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

        // POST: api/Movimentacao
        [ResponseType(typeof(Movimentacao))]
        public IHttpActionResult PostMovimentacao(Movimentacao movimentacao)
        {
            db.Movimentacao.Add(movimentacao);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movimentacao.Id }, movimentacao);
        }

        // DELETE: api/Movimentacao/5
        [ResponseType(typeof(Movimentacao))]
        public IHttpActionResult DeleteMovimentacao(int id)
        {
            Movimentacao movimentacao = db.Movimentacao.Find(id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            db.Movimentacao.Remove(movimentacao);
            db.SaveChanges();

            return Ok(movimentacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovimentacaoExists(int id)
        {
            return db.Movimentacao.Count(e => e.Id == id) > 0;
        }
    }
}