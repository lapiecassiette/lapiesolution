using LaPieCassiette.Application;
using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Domain.Models;
using LaPieCassiette.Infrastructure.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace LaPieCassiette.Web.Controllers;

public class HomeController : Controller
{
 

    private readonly IProductService _productService;
    private readonly AppDbContext _context;
    public HomeController(IProductService productService, AppDbContext context)
    {
        _productService = productService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var suppliers = await _context.Users
            .Include(u => u.Contact)
            .Where(u => u.Role == UserRole.Supplier)
            .ToListAsync();

        var dtos = suppliers.Select(s => new SupplierDto
        {
            Id = s.Id,
            Name = s.Name,
            Phone = s.Contact?.Phone,
            Address = s.Contact?.Address
        }).ToList();

        return View(dtos);
    }
    public async Task<IActionResult> MenuDuJour()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }
    public async Task<IActionResult> Commander()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }
    public IActionResult Concept()
    {
        return View();
    }
}