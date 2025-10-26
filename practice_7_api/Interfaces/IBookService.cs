using Microsoft.AspNetCore.Mvc;
using practice_7_api.Requests;

namespace practice_7_api.Interfaces
{
    public interface IBookService
    {
        Task<IActionResult> GetAllBooksAsync();
        Task<IActionResult> CreateNewBookAsync(CreateNewBook newBook);
        Task<IActionResult> GetBookByIdAsync(int id);
        Task<IActionResult> GetBooksByGenreAsync(int id);
        Task<IActionResult> GetBookByTitleAndAuthorAsync(string title, string author_name);
        Task<IActionResult> GetBookCountById(int id);
        Task<IActionResult> PutBookAsync(int id, UpdateBook updatedBook);
        Task<IActionResult> DeleteBookAsync(int id);
    }
}
