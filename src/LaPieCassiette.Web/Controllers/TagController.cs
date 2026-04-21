using LaPieCassiette.Application.DTOs;
using LaPieCassiette.Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
public class TagController : Controller
{
    private readonly ITagRepository _tagRepository;

    public TagController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Redirect(Request.Headers["Referer"].ToString());

        var tag = new Tag(name);

        await _tagRepository.AddAsync(tag);

        return Redirect(Request.Headers["Referer"].ToString());
    }

}
