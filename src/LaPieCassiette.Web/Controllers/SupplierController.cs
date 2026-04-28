using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Application.Services;
using LaPieCassiette.Domain.Models;
using LaPieCassiette.Infrastructure.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaPieCassiette.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SupplierService _supplierService;
        public SupplierController(AppDbContext context, SupplierService supplierService)
        {
            _context = context;
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
            .Include(u => u.Contact)
            .Where(u => u.Role == UserRole.Supplier)
            .ToListAsync();

            var dtos = users.Select(u => _supplierService.MapToDto(u)).ToList();

            return View(dtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            user.Role = UserRole.Supplier;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users
                .Include(u => u.Contact)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound();

            var dto = new SupplierDto
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Contact?.Phone,
                Address = user.Contact?.Address
            };

            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SupplierDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Contact)
                .FirstOrDefaultAsync(u => u.Id == dto.Id);

            if (user == null) return NotFound();

            // 🔹 update user
            user.Name = dto.Name;

            // 🔹 contact
            if (user.Contact == null)
            {
                user.Contact = new UserContact
                {
                    UserId = user.Id
                };
            }

            user.Contact.Phone = dto.Phone;
            user.Contact.Address = dto.Address;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.Users.FindAsync(id);

            if (supplier != null)
            {
                _context.Users.Remove(supplier);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}