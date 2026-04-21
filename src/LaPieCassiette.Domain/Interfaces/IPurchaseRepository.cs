using System;
using System.Collections.Generic;
using LaPieCassiette.Domain.Models;
public interface IPurchaseRepository
{
    Task<List<Purchase>> GetAllAsync();

    Task AddAsync(Purchase purchase);

    Task<Purchase?> GetByIdAsync(int id);

    Task DeleteAsync(int id);
}
