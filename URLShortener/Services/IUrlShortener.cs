namespace URLShortener.Services;

public interface IUrlShortener
{
    bool IsUrlValid(string url);
    string GenerateShortUrlCode();
    string CombineShortUrl(string scheme, string host, string shortUrlCode);
}