using LaPieCassiette.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
public class AdminController : Controller
{
    private readonly IProductService _productService;
    private readonly ITagRepository _tagRepository;
    private readonly IProductRepository _productRepository;

    public AdminController(
     IProductService productService,
     ITagRepository tagRepository,
     IProductRepository productRepository)
    {
        _productService = productService;
        _tagRepository = tagRepository;
        _productRepository = productRepository;
    }
    // LISTE
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    // CREATE GET
    public async Task<IActionResult> Create()
    {
        ViewBag.Tags = new SelectList(
            await _tagRepository.GetAllAsync(),
            "Id",
            "Name"
        );

        return View(new ProductDto());
    }

    // CREATE POST
    [HttpPost]
    public async Task<IActionResult> Create(ProductDto dto, IFormFile? image)
    {
        Console.WriteLine("TAG COUNT = " + dto.TagIds.Count); // 👈 ICI

        if (!ModelState.IsValid)
        {
            ViewBag.Tags = new SelectList(
                await _tagRepository.GetAllAsync(),
                "Id",
                "Name"
            );

            return View(dto);
        }

        await _productService.CreateAsync(
            dto,
            image?.OpenReadStream(),
            image?.FileName
        );

        return RedirectToAction(nameof(Index));
    }
    // EDIT GET
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        var dto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            IsPublished = product.IsPublished,

            // 🔥 IMPORTANT
            TagIds = product.ProductTags
                .Select(pt => pt.TagId)
                .ToList(),

            ExistingImagePath = product.ImagePath
        };

        ViewBag.Tags = new SelectList(
            await _tagRepository.GetAllAsync(),
            "Id",
            "Name"
        );

        return View(dto);
    }
    // EDIT POST
    [HttpPost]
    public async Task<IActionResult> Edit(ProductDto dto, IFormFile? image)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Tags = new SelectList(
                await _tagRepository.GetAllAsync(),
                "Id",
                "Name"
            );

            return View(dto);
        }

        await _productService.UpdateAsync(
            dto.Id, // 🔥 IMPORTANT
            dto,
            image?.OpenReadStream(),
            image?.FileName
        );
        Console.WriteLine("TAG COUNT UPDATE: " + dto.TagIds.Count);
        return RedirectToAction(nameof(Index));
    }
    // DELETE
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // PUBLISH
    public async Task<IActionResult> TogglePublish(int id)
    {
        await _productService.TogglePublishAsync(id);
        return RedirectToAction(nameof(Index));
    }
}