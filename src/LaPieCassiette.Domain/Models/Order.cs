using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPieCassiette.Domain.Models
{
    public class Order
    {
        public int Id { get; private set; }

        public int FormulaId { get; private set; }
        public Formula Formula { get; private set; }

        public List<OrderItem> Items { get; private set; } = new();

        public bool HasDrink { get; private set; }

        public decimal TotalPrice { get; private set; }

        private Order() { } // EF

        public Order(Formula formula)
        {
            Formula = formula;
            FormulaId = formula.Id;
        }

        public void AddItem(Product product)
        {
            if (Items.Count >= Formula.PiocheCount)
                throw new Exception("Nombre de pioches dépassé");

            Items.Add(new OrderItem(product));
        }

        public void AddDrink()
        {
            HasDrink = true;
        }

        public void CalculateTotal()
        {
            TotalPrice = Formula.Price;

            if (HasDrink)
                TotalPrice += 2;
        }
    }
}