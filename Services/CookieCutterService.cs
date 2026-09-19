using AkkWebService.Data;
using AkkWebService.DTOs;
using AkkWebService.Models;
using Microsoft.EntityFrameworkCore;

namespace AkkWebService.Services;

public class CookieCutterService : ICookieCutterService
{
    private readonly AkkDbContext _context;

    public CookieCutterService(AkkDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CookieCutterDto>> GetAllAsync()
    {
        return await _context.CookieCutters
            .OrderBy(c => c.Name)
            .Select(c => MapToDto(c))
            .ToListAsync();
    }

    public async Task<CookieCutterDto?> GetByIdAsync(long id)
    {
        var entity = await _context.CookieCutters.FindAsync(id);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<IEnumerable<CookieCutterDto>> SearchByNameAsync(string name)
    {
        return await _context.CookieCutters
            .Where(c => c.Name.Contains(name))
            .OrderBy(c => c.Name)
            .Select(c => MapToDto(c))
            .ToListAsync();
    }

    public async Task<CookieCutterDto> CreateAsync(CreateCookieCutterDto dto)
    {
        var entity = new CookieCutter
        {
            Name = dto.Name,
            LengthInches = dto.LengthInches,
            WidthInches = dto.WidthInches,
            Type = dto.Type,
            Comments = dto.Comments,
            DateAcquired = dto.DateAcquired,
            ImageUrl = dto.ImageUrl
        };

        _context.CookieCutters.Add(entity);
        await _context.SaveChangesAsync();

        return MapToDto(entity);
    }

    public async Task<CookieCutterDto?> UpdateAsync(long id, UpdateCookieCutterDto dto)
    {
        var entity = await _context.CookieCutters.FindAsync(id);
        if (entity is null) return null;

        entity.Name = dto.Name;
        entity.LengthInches = dto.LengthInches;
        entity.WidthInches = dto.WidthInches;
        entity.Type = dto.Type;
        entity.Comments = dto.Comments;
        entity.DateAcquired = dto.DateAcquired;
        entity.ImageUrl = dto.ImageUrl;

        await _context.SaveChangesAsync();

        return MapToDto(entity);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _context.CookieCutters.FindAsync(id);
        if (entity is null) return false;

        _context.CookieCutters.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    private static CookieCutterDto MapToDto(CookieCutter entity)
    {
        return new CookieCutterDto
        {
            CookieCutterId = entity.CookieCutterId,
            Name = entity.Name,
            LengthInches = entity.LengthInches,
            WidthInches = entity.WidthInches,
            Type = entity.Type,
            Comments = entity.Comments,
            DateAcquired = entity.DateAcquired,
            ImageUrl = entity.ImageUrl,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
