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
    public class BuyController : ApiController
    {
        private AutomatDbContext db = new AutomatDbContext();

        // PUT: api/Buy/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDrink(int id, Drink drink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drink.id)
            {
                return BadRequest();
            }
            var dr = db.drinks.FirstOrDefault(d => d.id == id);
            if (dr == null || dr.count == 0 || db.amountsPaid.Count() == 0)
                return StatusCode(HttpStatusCode.NoContent);

            int sumPaid = db.amountsPaid.Sum(s => s.naminal.num);
            if (sumPaid < drink.cost) // проверка хватает ли внесенных денег
                return StatusCode(HttpStatusCode.NoContent);

            int sum = 0;
            List<AmountPaid> sumsGet = new List<AmountPaid>(); // сколько коппек заберет автомат 
            List<Amount> sumsPut = new List<Amount>();
            List<string> response = new List<string>();
            foreach (AmountPaid am in db.amountsPaid.Include(n => n.naminal).OrderByDescending(o => o.naminal.num))
            {
                sum += am.naminal.num;
                sumsGet.Add(am);
                if (sum >= drink.cost)
                    break;

            }

            if (drink.cost < sumsGet.Sum(s => s.naminal.num)) // если автомат забрал больше чем нужно, возвращает копейками мельче
            {
                int sumPut = sumsGet.Sum(s => s.naminal.num) - drink.cost;
                sum = 0;
                foreach (Amount am in db.amounts.Include(n => n.naminal).OrderByDescending(o => o.naminal.num))
                {
                    sum += am.naminal.num;
                    if (sum > sumPut)
                    {
                        sum -= am.naminal.num;
                        continue;
                    }
                    else if (sum == sumPut)
                    {
                        sumsPut.Add(am);
                        break;
                    }
                    sumsPut.Add(am);
                }
                if (sum != sumPut)
                    response = new List<string>() { "Автомату нечем выдать сдачу." };
            }

            db.amountsPaid.AddRange(sumsPut.Select(s => new AmountPaid() { naminal = s.naminal }).ToList());
            db.amounts.AddRange(sumsGet.Select(s => new Amount() { naminal = s.naminal }).ToList());
            db.amounts.RemoveRange(sumsPut);
            db.amountsPaid.RemoveRange(sumsGet);


            if (dr != null)
                dr.count--;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(response);
        }
        private bool DrinkExists(int id)
        {
            return db.drinks.Count(e => e.id == id) > 0;
        }

    }
}
