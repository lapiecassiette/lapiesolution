using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPieCassiette.Domain.Models
{
    public class Purchase
    {

        public int Id { get; set; }

        public string Name { get; set; } = "";

        public decimal Quantity { get; set; }
        public string Unit { get; set; } = "kg";

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public PurchaseCategory Category { get; set; }

        // 🔥 IMPORTANT
        public int SupplierId { get; set; }
        public User Supplier { get; set; } = null!;
    }
    }
