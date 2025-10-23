using Microsoft.AspNetCore.Mvc;
using practice_7_api.Requests;

namespace practice_7_api.Interfaces
{
    public interface IRent_BookService
    {
        Task<IActionResult> CreateNewRentAsync(CreateNewRent_Book newRent_book);
        Task<IActionResult> GetRentsByReaderId(int id);
        Task<IActionResult> GetRentsByBookIdAsync(int id);
        Task<IActionResult> GetCurrentRents();
        Task<IActionResult> PostRentReturnAsync(int id);
    }
}
