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
using DrinksAutomat.Models;

namespace DrinksAutomat.Controllers
{
    public class NaminalsController : ApiController
    {
        private AutomatDbContext db = new AutomatDbContext();

        // GET: api/Naminals
        public IQueryable<Naminal> Getnaminals()
        {
            return db.naminals;
        }

        // GET: api/Naminals/5
        [ResponseType(typeof(Naminal))]
        public IHttpActionResult GetNaminal(int id)
        {
            Naminal naminal = db.naminals.Find(id);
            if (naminal == null)
            {
                return NotFound();
            }

            return Ok(naminal);
        }

        // PUT: api/Naminals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNaminal(int id, Naminal naminal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != naminal.id)
            {
                return BadRequest();
            }

            db.Entry(naminal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NaminalExists(id))
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

        // POST: api/Naminals
        [ResponseType(typeof(Naminal))]
        public IHttpActionResult PostNaminal(Naminal naminal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.naminals.Add(naminal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = naminal.id }, naminal);
        }

        // DELETE: api/Naminals/5
        [ResponseType(typeof(Naminal))]
        public IHttpActionResult DeleteNaminal(int id)
        {
            Naminal naminal = db.naminals.Find(id);
            if (naminal == null)
            {
                return NotFound();
            }

            db.naminals.Remove(naminal);
            db.SaveChanges();

            return Ok(naminal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NaminalExists(int id)
        {
            return db.naminals.Count(e => e.id == id) > 0;
        }
    }
}