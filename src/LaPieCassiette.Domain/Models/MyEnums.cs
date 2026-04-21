using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPieCassiette.Domain.Models
{
    public enum UserRole
    {
        Admin,
        Supplier,
        Customer
    }

    public enum PurchaseCategory
    {
        Cremerie,
        Boucherie,
        Poissonnerie,
        Legumes,
        Epicerie,
        Boissons,
        Autre
    }

    public enum OrderStatus
    {
        Pending,
        Paid,
        Preparing,
        Ready,
        Completed
    }
}
