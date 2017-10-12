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

namespace DrinksAutomat.Controllers.api
{
    public class AmountController : ApiController
    {
        private AutomatDbContext db = new AutomatDbContext();

        // GET: api/Amount
        public IHttpActionResult Getamounts()
        {

            
            var amounts = db.amounts.GroupBy(g => g.naminal.num).Select(s => new { key = s.Key, value = s.Count() })
                .ToDictionary(x => x.key, x => x.value);

            var naminalsAmounts = db.naminals.GroupBy(g => g.num).Select(s => new { key = s.Key, value = 0 })
                .ToDictionary(x => x.key, x => x.value);

            foreach(KeyValuePair<int,int> val in naminalsAmounts.ToList())
            {
                naminalsAmounts[val.Key] = amounts.ContainsKey(val.Key) ? amounts[val.Key] : 0;
            }
            return Ok(naminalsAmounts);
        }

        // GET: api/Amount/5
        [ResponseType(typeof(Amount))]
        public IHttpActionResult GetAmount(int id)
        {
            Amount amount = db.amounts.Find(id);
            if (amount == null)
            {
                return NotFound();
            }

            return Ok(amount);
        }

        // PUT: api/Amount/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAmount(int id, Amount amount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != amount.id)
            {
                return BadRequest();
            }

            db.Entry(amount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.Accepted);
        }

        // POST: api/Amount
        [ResponseType(typeof(Amount))]
        public IHttpActionResult PostAmount(Dictionary<int,int> amount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Database.ExecuteSqlCommand("truncate table dbo.amounts");

            foreach(KeyValuePair<int,int> nums in amount)
            {
                Naminal _naminal = db.naminals.FirstOrDefault(n => n.num == nums.Key);
                for (int k = 0; k < nums.Value; k++)
                        db.amounts.Add(new Amount() { naminal = _naminal });
            }

            db.SaveChanges();

            return StatusCode(HttpStatusCode.Accepted);
        }

        // DELETE: api/Amount/5
        [ResponseType(typeof(Amount))]
        public IHttpActionResult DeleteAmount(int id)
        {
            Amount amount = db.amounts.Find(id);
            if (amount == null)
            {
                return NotFound();
            }

            db.amounts.Remove(amount);
            db.SaveChanges();

            return Ok(amount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AmountExists(int id)
        {
            return db.amounts.Count(e => e.id == id) > 0;
        }
    }
}