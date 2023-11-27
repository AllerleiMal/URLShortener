using FluentNHibernate.Mapping;
using URLShortener.Models;

namespace URLShortener.Mapping;

public class UrlMappingMap : ClassMap<UrlMapping>
{
    public UrlMappingMap()
    {
        Id(x => x.Id);
        Map(x => x.ShortUrl);
        Map(x => x.LongUrl);
        Map(x => x.ClickCounter);
    }
}