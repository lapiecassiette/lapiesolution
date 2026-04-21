using System;
using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using    Microsoft.AspNetCore.Http.Abstractions;
public class DashboardController : Controller
{
    private readonly IProductService _service;

    public IActionResult Index()
    {
        return View();
    }
}