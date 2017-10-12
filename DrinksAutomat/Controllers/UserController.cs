using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinksAutomat.Models;

namespace DrinksAutomat.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            AutomatDbContext db = new AutomatDbContext();
            db.Database.ExecuteSqlCommand("truncate table dbo.amountpaids");



            //db.drinks.Add(new Drink() { name = "Coca-cola", cost = 18, count = 3 });
            //db.drinks.Add(new Drink() { name = "Fanta", cost = 18, count = 0 });
            //db.drinks.Add(new Drink() { name = "Sprite", cost = 15, count = 8 });
            //db.drinks.Add(new Drink() { name = "Pepsi", cost = 19, count = 2 });
            //db.drinks.Add(new Drink() { name = "Сок", cost = 21, count = 3 });
            //List<Naminal> _naminals = new List<Naminal>() { new Naminal {num = 1 },
            //                                                new Naminal {num = 2 },
            //                                                new Naminal {num = 5 },
            //                                                new Naminal {num = 10 }};
            //db.naminals.AddRange(_naminals);

            //List<Amount> _amount = new List<Amount>();
            //for (int k = 0; k < 10; k++)
            //    db.amounts.Add(new Amount() { naminal = _naminals.FirstOrDefault(n => n.num == 1) });
            //for (int k = 0; k < 10; k++)
            //    db.amounts.Add(new Amount() { naminal = _naminals.FirstOrDefault(n => n.num == 2) });
            //for (int k = 0; k < 10; k++)
            //    db.amounts.Add(new Amount() { naminal = _naminals.FirstOrDefault(n => n.num == 5) });
            //for (int k = 0; k < 10; k++)
            //    db.amounts.Add(new Amount() { naminal = _naminals.FirstOrDefault(n => n.num == 10) });

            ////List<Amount> _amountPaid = new List<Amount>();
            ////_amountPaid.Add(new Amount() { naminal = _naminals.FirstOrDefault(n => n.num == 10) });

            ////db.automats.Add(new Automat() { amount = _amount, amountPaid = _amountPaid, naminals = _naminals, drinks = _drinks });
            //db.SaveChanges();

            return View();
        }
    }
}