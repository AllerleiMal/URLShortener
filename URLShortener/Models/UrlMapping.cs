namespace URLShortener.Models;

public class UrlMapping
{
    public virtual long Id { get; set; }
    public virtual string? LongUrl { get; set; }
    public virtual string? ShortUrlCode { get; set; }
    public virtual long ClickCounter { get; set; }
}