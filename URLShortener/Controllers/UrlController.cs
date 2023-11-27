using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Models;
using ISession = NHibernate.ISession;

namespace URLShortener.Controllers;

public class UrlController(ISession session) : Controller
{
    [ActionName("Index")]
    [HttpGet]
    public Task<IActionResult> GetUrlMappingList()
    {
        return Task.FromResult<IActionResult>(View("Index"));
    }
    
    [HttpGet("url/getPage")]
    public async Task<IActionResult> Get([DataSourceRequest] DataSourceRequest request)
    {
        var urlMappings = session.Query<UrlMapping>().AsQueryable();

        var result = await urlMappings.ToDataSourceResultAsync(request);
        return Json(result);
    }
    
    [ActionName("AddUrl")]
    [HttpGet]
    public async Task<IActionResult> AddUrl()
    {
        return View("AddUrl");
    }
    
}