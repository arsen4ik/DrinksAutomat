using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinksAutomat.Models
{
    public class Drink
    {
        public int id { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
        public int count { get; set; }

        public string picture { get; set; }

        //public Automat automat { get; set; }
    }
}