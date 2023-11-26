namespace URLShortener.Models;

public class UrlMapping
{
    public long Id { get; set; }
    public string? LongUrl { get; set; }
    public string? ShortUrl { get; set; }
    public long ClickCounter { get; set; }
}