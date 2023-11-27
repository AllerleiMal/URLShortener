using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using URLShortener.Models;
using ISession = NHibernate.ISession;

namespace URLShortener.Controllers;

[Route("")]
public class RedirectController(ISession session) : Controller
{
    [HttpGet("{shortUrlCode}")]
    public async Task<IActionResult> RedirectToLongUrl(string shortUrlCode)
    {
        var allMappings = await session.Query<UrlMapping>().ToListAsync();
        var targetUrlMapping = await session
            .Query<UrlMapping>()
            .FirstOrDefaultAsync(u => u.ShortUrlCode.Equals(shortUrlCode));

        if (targetUrlMapping is null)
        {
            return NotFound();
        }

        targetUrlMapping.ClickCounter++;
        await session.FlushAsync();

        return Redirect(targetUrlMapping.LongUrl!);
    }
}