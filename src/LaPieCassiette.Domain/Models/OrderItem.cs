using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPieCassiette.Domain.Models
{

    public class OrderItem
    {
        public int Id { get; private set; }

        public int ProductId { get; private set; }
        public Product Product { get; private set; }

        public int OrderId { get; private set; }
        public Order Order { get; private set; }

        private OrderItem() { } // EF

        public OrderItem(Product product)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            ProductId = product.Id;
        }
    }
}