using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LaPieCassiette.Application;
using LaPieCassiette.Domain.Models;

namespace LaPieCassiette.Web.Controllers;

public class HomeController : Controller
{
 

    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }
    public async Task<IActionResult> Menu()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }
}