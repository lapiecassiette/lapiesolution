using System;
using System.Collections.Generic;
using LaPieCassiette.Domain.Models;
public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
}