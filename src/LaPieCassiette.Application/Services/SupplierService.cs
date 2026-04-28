using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPieCassiette.Application.Services
{
    public class SupplierService
    {
        public SupplierDto MapToDto(User user)
        {
            return new SupplierDto
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Contact?.Phone,
                Address = user.Contact?.Address
            };
        }
    }
}
