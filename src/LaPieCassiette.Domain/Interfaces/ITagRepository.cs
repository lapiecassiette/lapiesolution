using System;
using System.Collections.Generic;
using LaPieCassiette.Domain.Models;
public interface ITagRepository
{
    Task<List<Tag>> GetAllAsync();
    Task AddAsync(Tag tag);
}