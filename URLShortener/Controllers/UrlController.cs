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
        ViewData["Title"] = "URL list";
        return await Task.FromResult<IActionResult>(View("Index"));
    }
    
    // [HttpPost("url/getPage")]
    // [ActionName("GetPage")]
    // public async Task<IActionResult> Get([DataSourceRequest] DataSourceRequest request)
    // {
    //     var urlMappings = repository.GetUrlMappingsQuery();
    //
    //     var result = await urlMappings.ToDataSourceResultAsync(request);
    //     return Json(result);
    // }

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
        ViewData["Title"] = "Create short URL";
        return await Task.FromResult<IActionResult>(View("AddUrl", new ManageUrlMappingViewModel()));
    }

    [HttpPost("url/create")]    
    [ActionName("CreateShortUrl")]
    public async Task<IActionResult> CreateUrlMapping(ManageUrlMappingViewModel model)
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

    [HttpGet]
    [ActionName("UpdateUrlMapping")]
    public async Task<IActionResult> UpdateUrlMapping()
    {
        ViewData["updated"] = false;
        ViewData["Title"] = "Update URL mapping";
        return await Task.FromResult<IActionResult>(View("UpdateUrl"));
    }
    
    [HttpPost]
    [ActionName("UpdateUrlMapping")]
    public async Task<IActionResult> UpdateUrlMapping(ManageUrlMappingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("UpdateUrl", model);
        }
        
        if (!urlShortener.IsUrlValid(model.LongUrl))
        {
            ModelState.AddModelError("LongUrl", "Please enter a valid URL.");
        }
        
        if (!urlShortener.IsUrlValid(model.ShortUrl))
        {
            ModelState.AddModelError("ShortUrl", "Please enter a valid URL.");
        }

        if (!ModelState.IsValid)
        {
            return View("UpdateUrl", model);
        }

        var shortUrlCode = urlShortener.ParseShortUrlCodeFromUrl(model.ShortUrl);

        var targetMapping = await repository.GetUrlMappingAsync(shortUrlCode);

        if (targetMapping is null)
        {
            ModelState.AddModelError("ShortUrl", "Mapping with such url is not exist.");
            return View("UpdateUrl", model);
        }

        targetMapping.LongUrl = model.LongUrl;
        await repository.SaveChangesAsync();

        ViewData["updated"] = true;
        return View("UpdateUrl", model);
    }
}