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
using AgendaTelefonica.Models;

namespace AgendaTelefonica.Controllers
{
    public class agendaController : ApiController
    {
        private cadastroEntities db = new cadastroEntities();

        // GET: api/agenda
        public IQueryable<agenda> Getagenda()
        {
            return db.agenda;
        }

        // GET: api/agenda/5
        [ResponseType(typeof(agenda))]
        public IHttpActionResult Getagenda(int id)
        {
            agenda agenda = db.agenda.Find(id);
            if (agenda == null)
            {
                return NotFound();
            }

            return Ok(agenda);
        }

        // PUT: api/agenda/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putagenda(int id, agenda agenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agenda.id)
            {
                return BadRequest();
            }

            db.Entry(agenda).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!agendaExists(id))
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

        // POST: api/agenda
        [ResponseType(typeof(agenda))]
        public IHttpActionResult Postagenda(agenda agenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.agenda.Add(agenda);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agenda.id }, agenda);
        }

        // DELETE: api/agenda/5
        [ResponseType(typeof(agenda))]
        public IHttpActionResult Deleteagenda(int id)
        {
            agenda agenda = db.agenda.Find(id);
            if (agenda == null)
            {
                return NotFound();
            }

            db.agenda.Remove(agenda);
            db.SaveChanges();

            return Ok(agenda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool agendaExists(int id)
        {
            return db.agenda.Count(e => e.id == id) > 0;
        }
    }
}