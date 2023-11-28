using Microsoft.AspNetCore.Mvc;
using URLShortener.Repositories;

namespace URLShortener.Controllers;

[Route("")]
public class RedirectController(IUrlMappingRepository repository) : Controller
{
    [HttpGet("{shortUrlCode}")]
    public async Task<IActionResult> RedirectToLongUrl(string shortUrlCode)
    {
        var targetUrlMapping = await repository.GetUrlMappingAsync(shortUrlCode);

        if (targetUrlMapping is null)
        {
            return NotFound();
        }

        targetUrlMapping.ClickCounter++;
        await repository.SaveChangesAsync();

        return Redirect(targetUrlMapping.LongUrl!);
    }
}