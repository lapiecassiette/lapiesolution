using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPieCassiette.Domain.Models
{
    public class Formula
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int PiocheCount { get; private set; }
        public decimal Price { get; private set; }

        private Formula() { }

        public Formula(string name, int count, decimal price)
        {
            Name = name;
            PiocheCount = count;
            Price = price;
        }
    }
}