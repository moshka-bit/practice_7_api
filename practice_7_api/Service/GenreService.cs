using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice_7_api.DatabaseContext;
using practice_7_api.Interfaces;
using practice_7_api.Models;
using practice_7_api.Requests;
using System.Reflection.PortableExecutable;

namespace practice_7_api.Service
{
    public class GenreService : IGenreService
    {
        private readonly ContextDb _context;

        public GenreService(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateNewGenreAsync(CreateNewGenre NewGenre)
        {
            if(NewGenre.genre_name == null)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Название жанра не должно быть пустым"
                });
            }

            var genre = new Genre()
            {
                genre_name = NewGenre.genre_name
            };

            await _context.AddAsync(genre);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status=true
            });
        }

        public async Task<IActionResult> DeleteGenreAsync(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var genres = await _context.Genres.ToListAsync();
            var our_genre = genres.FirstOrDefault(g => g.genre_id == id);

            if (our_genre == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого жанра"
                });
            }

            _context.Genres.Remove(our_genre);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<IActionResult> GetAllGenresAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            return new OkObjectResult(new
            {
                data = new { genres = genres },
                status = true
            });
        }

        public async Task<IActionResult> UpdateGenreAsync(int id, UpdateGenre NewGenre)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var existing_genre = await _context.Genres.FirstOrDefaultAsync(g => g.genre_id == id);

            if (existing_genre == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого жанра"
                });
            }

            if(string.IsNullOrEmpty(NewGenre.genre_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет названия жанра"
                });
            }

            existing_genre.genre_name = NewGenre.genre_name;

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }
    }
}