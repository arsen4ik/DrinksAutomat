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
    public class AmountPaidController : ApiController
    {
        private AutomatDbContext db = new AutomatDbContext();

        // GET: api/AmountPaid
        public int GetamountsPaid()
        {
            return db.amountsPaid.Count() > 0 ? db.amountsPaid.Sum(a => a.naminal.num) : 0;
        }

        // GET: api/AmountPaid/5
        [ResponseType(typeof(AmountPaid))]
        public IHttpActionResult GetAmountPaid(int id)
        {
            AmountPaid amountPaid = db.amountsPaid.Find(id);
            if (amountPaid == null)
            {
                return NotFound();
            }

            return Ok(amountPaid);
        }

        // PUT: api/AmountPaid/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAmountPaid(int id, AmountPaid amountPaid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != amountPaid.id)
            {
                return BadRequest();
            }

            db.Entry(amountPaid).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmountPaidExists(id))
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

        // POST: api/AmountPaid
        [ResponseType(typeof(AmountPaid))]
        public IHttpActionResult PostAmountPaid(Naminal naminalPaid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.amountsPaid.Add(new AmountPaid() { naminal = db.naminals.FirstOrDefault(n => n.num == naminalPaid.num) });
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = naminalPaid.id }, naminalPaid);
        }

        // DELETE: api/AmountPaid/5
        [ResponseType(typeof(AmountPaid))]
        public IHttpActionResult DeleteAmountPaid()
        {
            List<string> changeInfo = db.amountsPaid.GroupBy(g => g.naminal.num).ToList()
                .Select(s => string.Format("Наминалом {0}к - {1}шт.", s.Key, s.Count())).ToList();
            

            db.amountsPaid.RemoveRange(db.amountsPaid.ToList());

            db.SaveChanges();

            return Ok(changeInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AmountPaidExists(int id)
        {
            return db.amountsPaid.Count(e => e.id == id) > 0;
        }
    }
}