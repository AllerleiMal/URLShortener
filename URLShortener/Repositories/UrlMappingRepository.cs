﻿using NHibernate.Linq;
using URLShortener.Models;
using ISession = NHibernate.ISession;

namespace URLShortener.Repositories;

public class UrlMappingRepository(ISession session) : IUrlMappingRepository
{
    public async Task<UrlMapping?> GetUrlMappingAsync(string shortUrlCode)
    {
        return await session.Query<UrlMapping>()
            .FirstOrDefaultAsync(mapping => mapping.ShortUrlCode.Equals(shortUrlCode));
    }

    public async Task<UrlMapping?> GetUrlMappingAsync(int id)
    {
        return await session.Query<UrlMapping>()
            .FirstOrDefaultAsync(mapping => mapping.Id == id);
    }

    public async void AddUrlMappingAsync(UrlMapping? mapping)
    {
        if (mapping is null)
        {
            return;
        }

        await session.SaveAsync(mapping);
        await session.FlushAsync();
    }

    public async Task<bool> DeleteUrlMappingAsync(int id)
    {
        var targetMapping = await GetUrlMappingAsync(id);

        if (targetMapping is null)
        {
            return false;
        }

        await session.DeleteAsync(targetMapping);
        await session.FlushAsync();
        return true;
    }

    public IQueryable<UrlMapping> GetUrlMappingsQuery()
    {
        return session.Query<UrlMapping>();
    }

    public async void SaveChangesAsync()
    {
        await session.FlushAsync();
    }
}