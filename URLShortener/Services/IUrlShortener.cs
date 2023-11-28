namespace URLShortener.Services;

public interface IUrlShortener
{
    bool IsUrlValid(string url);
    string GenerateShortUrlCode();
    string CombineShortUrl(string schema, string host, string shortUrlCode);
}