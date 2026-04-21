using LaPieCassiette.Domain.Models;
using LaPieCassiette.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

using System;
public class TagRepository : ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        return await _context.Tags.ToListAsync();
    }
    public async Task AddAsync(Tag tag)
    {
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
    }
}
