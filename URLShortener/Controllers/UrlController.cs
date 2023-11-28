using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Models;
using URLShortener.Repositories;
using URLShortener.Services;

namespace URLShortener.Controllers;

public class UrlController(IUrlMappingRepository repository, IUrlShortener urlShortener) : Controller
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
        var urlMappings = repository.GetUrlMappingsQuery();

        var result = await urlMappings.ToDataSourceResultAsync(request);
        return Json(result);
    }

    [HttpDelete("url/delete")]
    [ActionName("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeletionSucceeded = await repository.DeleteUrlMappingAsync(id);
    
        if (!isDeletionSucceeded)
        {
            return NotFound();
        }
    
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
        
        if (!urlShortener.IsUrlValid(model.LongUrl))
        {
            ModelState.AddModelError("LongUrl", "Please enter a valid URL.");
            return View("AddUrl", model);
        }


        var shortUrlCode = urlShortener.GenerateShortUrlCode();
        var shortUrl = urlShortener.CombineShortUrl(
            HttpContext.Request.Scheme, 
            HttpContext.Request.Host.ToString(), 
            shortUrlCode
            );
        
        model.ShortUrl = shortUrl;

        await repository.AddUrlMappingAsync(new UrlMapping
        {
            LongUrl = model.LongUrl,
            ShortUrlCode = shortUrlCode,
            ClickCounter = 0
        });
        
        return View("AddUrl", model);
    }
}