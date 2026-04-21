using LaPieCassiette.Domain.Models;
using LaPieCassiette.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

using System;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetSuppliersAsync()
    {
        return await _context.Users
            .Where(u => u.Role == UserRole.Supplier)
            .ToListAsync();
    }
}