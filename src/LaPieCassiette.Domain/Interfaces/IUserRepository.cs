using System;
using System.Collections.Generic;
using LaPieCassiette.Domain.Models;
public interface IUserRepository
{
    Task<List<User>> GetSuppliersAsync();
}