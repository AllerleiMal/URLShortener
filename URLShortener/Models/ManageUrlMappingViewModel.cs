using System.ComponentModel.DataAnnotations;

namespace URLShortener.Models;

public class ManageUrlMappingViewModel
{
    [DataType(DataType.Url)]
    public string? LongUrl { get; set; }
    [DataType(DataType.Url)]
    public string? ShortUrl { get; set; }
}