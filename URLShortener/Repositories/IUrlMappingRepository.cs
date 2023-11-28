using URLShortener.Models;

namespace URLShortener.Repositories;

public interface IUrlMappingRepository
{
    Task<UrlMapping?> GetUrlMappingAsync(string shortUrlCode);
    Task<UrlMapping?> GetUrlMappingAsync(int id);
    Task AddUrlMappingAsync(UrlMapping? mapping);
    Task<bool> DeleteUrlMappingAsync(int id);
    IQueryable<UrlMapping> GetUrlMappingsQuery();
    Task SaveChangesAsync();
}