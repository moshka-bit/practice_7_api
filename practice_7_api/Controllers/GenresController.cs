using Microsoft.AspNetCore.Mvc;
using practice_7_api.Interfaces;
using practice_7_api.Requests;

namespace practice_7_api.Controllers
{
    public class GenresController
    {
        private readonly IGenreService _Service1;

        public GenresController(IGenreService Service1)
        {
            _Service1 = Service1;
        }

        [HttpGet]
        [Route("GetAllGenres")]
        public async Task<IActionResult> GetAllGenresAsync()
        {
            return await _Service1.GetAllGenresAsync();
        }


        [HttpPost]
        [Route("CreateNewGenre")]
        public async Task<IActionResult> CreateNewGenre(CreateNewGenre NewGenre)
        {
            return await _Service1.CreateNewGenreAsync(NewGenre);
        }

        [HttpDelete]
        [Route("DeleteGenre")]

        public async Task<IActionResult> DeleteGenreAsync(int id)
        {
            return await _Service1.DeleteGenreAsync(id);
        }

        [HttpPut]
        [Route("PutGenre")]

        public async Task<IActionResult> PutGenreAsync(int id, UpdateGenre updatedGenre)
        {
            return await _Service1.UpdateGenreAsync(id, updatedGenre);
        }

    }
}
