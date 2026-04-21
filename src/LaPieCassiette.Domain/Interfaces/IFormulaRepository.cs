using System;
using System.Collections.Generic;
using LaPieCassiette.Domain.Models;
public interface IFormulaRepository
{
    Task<List<Formula>> GetAllAsync();
    Task<Formula?> GetByIdAsync(int id);
}