using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPieCassiette.Domain.Models
{
    public class UserContact
    {
        public int Id { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }

        // 🔥 lien User
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }

}
