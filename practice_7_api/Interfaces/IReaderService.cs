using Microsoft.AspNetCore.Mvc;
using practice_7_api.Requests;

namespace practice_7_api.Interfaces
{
    public interface IReaderService
    {
        Task<IActionResult> GetAllReadersAsync();
        Task<IActionResult> CreateNewReaderAsync(CreateNewReader newReader);
        Task<IActionResult> GetReaderByIdAsync(int id);
        Task<IActionResult> GetReaderBooksByIdASync(int id);
        Task<IActionResult> DeleteReaderAsync(int id);
        Task<IActionResult> PutReaderAsync(int id, UpdateReader updatedReader);
    }
}
