using Microsoft.AspNetCore.Mvc;
using practice_7_api.Requests;

namespace practice_7_api.Interfaces
{
    public interface IGenreService
    {
        Task<IActionResult> GetAllGenresAsync();
        Task<IActionResult> CreateNewGenreAsync(CreateNewGenre NewGenre);
        Task<IActionResult> DeleteGenreAsync(int id);
        Task<IActionResult> UpdateGenreAsync(int id, UpdateGenre NewGenre);
    }
}
