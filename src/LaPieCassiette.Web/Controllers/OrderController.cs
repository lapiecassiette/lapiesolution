using LaPieCassiette.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

    public class OrderController : Controller
    {
        public IActionResult Validate(List<int> selectedProductIds)
        {
            if (selectedProductIds == null || !selectedProductIds.Any())
            {
                return RedirectToAction("Menu", "Home");
            }

            // 👉 ici tu fais ton métier
            // sauvegarde / traitement / affichage

            return Content("Assiette validée avec " + selectedProductIds.Count + " pioches");
        }
    }
