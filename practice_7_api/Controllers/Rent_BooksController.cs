using Microsoft.AspNetCore.Mvc;
using practice_7_api.Interfaces;
using practice_7_api.Requests;

namespace practice_7_api.Controllers
{
    public class Rent_BooksController
    {
        private readonly IRent_BookService _Service1;

        public Rent_BooksController(IRent_BookService Service1)
        {
            _Service1 = Service1;
        }

        [HttpPost]
        [Route("CreateNewRent_Book")]
        public async Task<IActionResult> CreateNewRent_Book(CreateNewRent_Book newRent_Book)
        {
            return await _Service1.CreateNewRentAsync(newRent_Book);
        }

        [HttpGet]
        [Route("GetRentsByReaderId")]

        public async Task<IActionResult> GetRentsByReaderId(int id)
        {
            return await _Service1.GetRentsByReaderId(id);
        }

        [HttpGet]
        [Route("GetRentsByBookId")]
        public async Task<IActionResult> GetRentsByBookIdAsync(int id)
        {
            return await _Service1.GetRentsByBookIdAsync(id);
        }

        [HttpGet]
        [Route("GetCurrentRents")]

        public async Task<IActionResult> GetCurrentRentsAsync()
        {
            return await _Service1.GetCurrentRents();
        }

        [HttpPut]
        [Route("PostRentReturn")]

        public async Task<IActionResult> PostRentReturnAsync(int id)
        {
            return await _Service1.PostRentReturnAsync(id);
        }

    }
}
