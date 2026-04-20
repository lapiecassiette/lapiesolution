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

 

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ProductDto dto, IFormFile? image)
    {
        Console.WriteLine("CATEGORY DTO = " + dto.Category);

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
            Price = product.Price,
            Category = product.Category,
            ImagePath = product.ImagePath
        };

        return View(dto);
    }
    [HttpPost]
    public IActionResult Edit(int id, ProductDto dto)
    {
        _service.Update(id, dto);
        return RedirectToAction("Products");
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return RedirectToAction("Products");
    }
    [HttpPost]
    public IActionResult TogglePublish(int id)
    {
        _service.TogglePublish(id);
        return RedirectToAction("Products");
    }
    public IActionResult Products(int page = 1)
    {
        int pageSize = 10;

        var products = _service.GetProducts();

        var paged = products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(products.Count / (double)pageSize);

        return View(paged);
    }
}
