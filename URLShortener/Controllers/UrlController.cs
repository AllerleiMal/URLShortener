using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Controllers;

public class UrlController : Controller
{
    public UrlController()
    {
    }

    [ActionName("Index")]
    [HttpGet]
    public async Task<IActionResult> GetUrlMappingList()
    {
        return View("Index");
    }

    [ActionName("AddUrl")]
    [HttpGet]
    public async Task<IActionResult> AddUrl()
    {
        return View("AddUrl");
    }
    
}