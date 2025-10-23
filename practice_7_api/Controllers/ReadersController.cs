using Microsoft.AspNetCore.Mvc;
using practice_7_api.Interfaces;
using practice_7_api.Requests;

namespace practice_7_api.Controllers
{
    public class ReadersController
    {
        private readonly IReaderService _Service1;

        public ReadersController(IReaderService Service1)
        {
            _Service1 = Service1;
        }

        [HttpGet]
        [Route("GetAllReaders")]
        public async Task<IActionResult> GetAllReadersAsync()
        {
            return await _Service1.GetAllReadersAsync();
        }

        [HttpPost]
        [Route("CreateNewReader")]
        public async Task<IActionResult> CreateNewReader(CreateNewReader newReader)
        {
            return await _Service1.CreateNewReaderAsync(newReader);
        }

        [HttpGet]
        [Route("GetReaderById")]

        public async Task<IActionResult> GetReaderByIdAsync(int id)
        {
            return await _Service1.GetReaderByIdAsync(id);
        }

        [HttpGet]
        [Route("GetReaderBooksById")]

        public async Task<IActionResult> GetReaderBooksByIdASync(int id)
        {
            return await _Service1.GetReaderBooksByIdASync(id);
        }

        [HttpDelete]
        [Route("DeleteReader")]

        public async Task<IActionResult> DeleteReaderAsync(int id)
        {
            return await _Service1.DeleteReaderAsync(id);
        }

        [HttpPut]
        [Route("PutReader")]

        public async Task<IActionResult> PutReaderAsync(int id, UpdateReader updateReader)
        {
            return await _Service1.PutReaderAsync(id, updateReader);
        }
    }
}
