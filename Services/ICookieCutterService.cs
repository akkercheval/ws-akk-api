using AkkWebService.DTOs;

namespace AkkWebService.Services;

public interface ICookieCutterService
{
    Task<IEnumerable<CookieCutterDto>> GetAllAsync();
    Task<CookieCutterDto?> GetByIdAsync(long id);
    Task<IEnumerable<CookieCutterDto>> SearchByNameAsync(string name);
    Task<CookieCutterDto> CreateAsync(CreateCookieCutterDto dto);
    Task<CookieCutterDto?> UpdateAsync(long id, UpdateCookieCutterDto dto);
    Task<bool> DeleteAsync(long id);
}
