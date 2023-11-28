using URLShortener.Models;

namespace URLShortener.Repositories;

public interface IUrlMappingRepository
{
    Task<UrlMapping?> GetUrlMappingAsync(string shortUrlCode);
    Task<UrlMapping?> GetUrlMappingAsync(int id);
    void AddUrlMappingAsync(UrlMapping? mapping);
    Task<bool> DeleteUrlMappingAsync(int id);
    IQueryable<UrlMapping> GetUrlMappingsQuery();
    void SaveChangesAsync();
}