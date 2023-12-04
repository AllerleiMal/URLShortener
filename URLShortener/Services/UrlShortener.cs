namespace URLShortener.Services;

public class UrlShortener : IUrlShortener
{
    public bool IsUrlValid(string? url)
    {
        return url is not null && Uri.IsWellFormedUriString(url, UriKind.Absolute);
    }

    public string GenerateShortUrlCode()
    {
        return Guid.NewGuid().ToString()[..16];
    }

    public string CombineShortUrl(string scheme, string host, string shortUrlCode)
    {
        return $"{scheme}://{host}/{shortUrlCode}";
    }

    public string ParseShortUrlCodeFromUrl(string shortUrl)
    {
        return shortUrl.Trim().Split("/")[^1];
    }
}