using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using URLShortener.Models;
using ISession = NHibernate.ISession;

namespace URLShortener.Controllers;

public class UrlController(ISession session) : Controller
{
    [ActionName("Index")]
    [HttpGet]
    public async Task<IActionResult> GetUrlMappingList()
    {
        ViewData["baseUrl"] = HttpContext.Request.GetDisplayUrl();
        return await Task.FromResult<IActionResult>(View("Index"));
    }
    
    [HttpPost("url/getPage")]
    [ActionName("GetPage")]
    public async Task<IActionResult> Get([DataSourceRequest] DataSourceRequest request)
    {
        var urlMappings = session.Query<UrlMapping>().AsQueryable();

        var result = await urlMappings.ToDataSourceResultAsync(request);
        return Json(result);
    }

    [HttpDelete("url/delete")]
    [ActionName("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var targetMapping = await session.Query<UrlMapping>().FirstOrDefaultAsync(mapping => mapping.Id == id);
    
        if (targetMapping is null)
        {
            return NotFound();
        }
    
        await session.DeleteAsync(targetMapping);
        await session.FlushAsync();
    
        return NoContent();
    }
    
    [ActionName("CreateUrlMapping")]
    [HttpGet]
    public async Task<IActionResult> CreateUrlMapping()
    {
        return await Task.FromResult<IActionResult>(View("AddUrl", new ManageShortUrlViewModel()));
    }

    [HttpPost("url/create")]    
    [ActionName("CreateShortUrl")]
    public async Task<IActionResult> CreateUrlMapping(ManageShortUrlViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("AddUrl", model);
        }
        
        if (!Uri.IsWellFormedUriString(model.ShortUrl, UriKind.Absolute))
        {
            ModelState.AddModelError("LongUrl", "Please enter a valid URL.");
            return View("AddUrl", model);
        }
        
        var shortUrlCode = Guid.NewGuid().ToString()[..16];
        var shortUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/" + shortUrlCode;
        Console.WriteLine(shortUrl);
        model.ShortUrl = shortUrl;
        return View("AddUrl", model);
    }
}