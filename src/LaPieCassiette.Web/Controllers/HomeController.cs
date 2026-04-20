using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LaPieCassiette.Application;
using LaPieCassiette.Domain.Models;

namespace LaPieCassiette.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _service;

    public HomeController(IProductService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var products = _service.GetProducts()
                               .Where(p => p.IsPublished)
                               .ToList();

        return View(products);
    }
    public IActionResult Detail(int id)
    {
        var product = _service.GetById(id);

        if (product == null)
            return NotFound();

        return View(product);
    }
    public IActionResult Menu()
    {
        var products = _service.GetProducts()
                               .Where(p => p.IsPublished)
                               .ToList();

        return View(products);
    }
}