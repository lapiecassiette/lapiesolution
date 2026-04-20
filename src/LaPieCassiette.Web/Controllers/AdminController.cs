using System;
using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using    Microsoft.AspNetCore.Http.Abstractions;
public class AdminController : Controller
{
    private readonly IProductService _service;

    public AdminController(IProductService service)
    {
        _service = service;
    }

    public IActionResult Products()
    {
        var products = _service.GetProducts();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ProductDto dto, IFormFile? image)
    {
        Stream? stream = null;
        string? fileName = null;

        if (image != null)
        {
            stream = image.OpenReadStream();
            fileName = image.FileName;
        }

        _service.Create(dto, stream, fileName);

        return RedirectToAction("Products");
    }
    public IActionResult Edit(int id)
    {
        var product = _service.GetById(id);

        var dto = new ProductDto
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };

        return View(dto);
    }

    [HttpPost]
    public IActionResult Edit(int id, ProductDto dto)
    {
        _service.Update(id, dto);
        return RedirectToAction("Products");
    }
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction("Products");
    }
    public IActionResult TogglePublish(int id)
    {
        _service.TogglePublish(id);
        return RedirectToAction("Products");
    }
}
