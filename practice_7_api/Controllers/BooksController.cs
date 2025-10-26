using Microsoft.AspNetCore.Mvc;
using practice_7_api.Interfaces;
using practice_7_api.Requests;

namespace practice_7_api.Controllers
{
    public class BooksController
    {
        private readonly IBookService _Service1;

        public BooksController(IBookService Service1)
        {
            _Service1 = Service1;
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            return await _Service1.GetAllBooksAsync();
        }

        [HttpPost]
        [Route("CreateNewBook")]
        public async Task<IActionResult> CreateNewBook(CreateNewBook newBook)
        {
            return await _Service1.CreateNewBookAsync(newBook);
        }

        [HttpGet]
        [Route("GetBookById")]
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            return await _Service1.GetBookByIdAsync(id);
        }

        [HttpGet]
        [Route("GetBooksByGenre")]

        public async Task<IActionResult> GetBooksByGenreAsync(int id)
        {
            return await _Service1.GetBooksByGenreAsync(id);
        }

        [HttpGet]
        [Route("GetBookByTitleAndAuthor")]

        public async Task<IActionResult> GetBookByTitleAsync(string title, string author_name)
        {
            return await _Service1.GetBookByTitleAndAuthorAsync(title, author_name);
        }

        [HttpGet]
        [Route("GetBookCountById")]

        public async Task<IActionResult> GetBookCountById(int id)
        {
            return await _Service1.GetBookCountById(id);
        }

        [HttpPut]
        [Route("PutBook")]

        public async Task<IActionResult> PutBookAsync(int id, UpdateBook updatedBook)
        {
            return await _Service1.PutBookAsync(id, updatedBook);
        }

        [HttpDelete]
        [Route("DeleteBook")]

        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            return await _Service1.DeleteBookAsync(id);
        }
    }
}
