using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Domain.Models;

using Microsoft.AspNetCore.Http;
using    Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
public class PurchaseController : Controller
{
    private readonly IPurchaseRepository _repository;
    private readonly IUserRepository _userRepository;
    public PurchaseController(IPurchaseRepository repository, IUserRepository userrepository)
    {
        _repository = repository;
        _userRepository = userrepository;
    }

    public async Task<IActionResult> Index()
    {
        var purchases = await _repository.GetAllAsync();
        return View(purchases.OrderByDescending(p => p.Date).ToList());
    }

    public async Task<IActionResult> Create()
    {
        var suppliers = await _userRepository.GetSuppliersAsync();

        ViewBag.Suppliers = suppliers.Select(s => new SelectListItem
        {
            Value = s.Id.ToString(),
            Text = s.Name
        });

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Purchase purchase)
    {
        // 🔥 validation fournisseur
        if (purchase.SupplierId == 0)
        {
            ModelState.AddModelError("", "Choisir un fournisseur");

            // 🔁 recharger la liste sinon le select casse
            var suppliers = await _userRepository.GetSuppliersAsync();

            ViewBag.Suppliers = suppliers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            });

            return View(purchase);
        }

        await _repository.AddAsync(purchase);

        return RedirectToAction("Index");
    }
}