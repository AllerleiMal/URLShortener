using URLShortener.Models;

namespace URLShortener.Repositories;

public interface IUrlMappingRepository
{
    Task<UrlMapping?> GetUrlMappingAsync(string shortUrlCode);
    Task<UrlMapping?> GetUrlMappingAsync(int id);
    Task AddUrlMappingAsync(UrlMapping? mapping);
    Task<bool> DeleteUrlMappingAsync(int id);
    Task<long> GetRecordsAmountAsync();
    Task<List<UrlMapping>> GetPaginatedDataAsync(int pageSize, int pageNumber);
    Task SaveChangesAsync();
}