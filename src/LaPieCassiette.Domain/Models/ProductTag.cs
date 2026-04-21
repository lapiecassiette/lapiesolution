using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPieCassiette.Domain.Models
{
    public class ProductTag
    {
        public int ProductId { get; private set; }
        public Product Product { get; private set; }

        public int TagId { get; private set; }
        public Tag Tag { get; private set; }

        private ProductTag() { } // EF

        public ProductTag(Product product, Tag tag)
        {
            Product = product;
            Tag = tag;

            ProductId = product.Id;
            TagId = tag.Id;
        }
    }
}