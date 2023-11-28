using System.ComponentModel.DataAnnotations;

namespace URLShortener.Models;

public class ManageShortUrlViewModel
{
    [Url]
    public string? LongUrl { get; set; }
    [Url]
    public string? ShortUrl { get; set; }
}