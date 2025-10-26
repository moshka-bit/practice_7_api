using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using practice_7_api.DatabaseContext;
using practice_7_api.Interfaces;
using practice_7_api.Models;
using practice_7_api.Requests;
using System.Data;
using System.Reflection.PortableExecutable;

namespace practice_7_api.Service
{
    public class Rent_BookService : IRent_BookService
    {
        private readonly ContextDb _context;

        public Rent_BookService(ContextDb context)
        {
            _context = context;
        }
        public async Task<IActionResult> CreateNewRentAsync(CreateNewRent_Book newRent_book)
        {
            if (newRent_book.reader_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Должен быть читатель"
                });
            }

            if (newRent_book.book_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Должная быть книга"
                });
            }

            if (newRent_book.start_date == new DateOnly(00001, 01, 01) || newRent_book.end_date == new DateOnly(00001, 01, 01))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет даты"
                });
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.book_id == newRent_book.book_id);
            if (book == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Книга не найдена"
                });
            }

            if (book.count == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "К сожалению книги нет в наличии"
                });
            }

            var rent_book = new Rent_Book()
            {
                reader_id = newRent_book.reader_id,
                book_id = newRent_book.book_id,
                start_date = newRent_book.start_date,
                end_date = newRent_book.end_date,
                status_id = 1
            };

            book.count -= 1;

            await _context.AddAsync(rent_book);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<IActionResult> GetCurrentRents()
        {
            var rents = await _context.Rent_Books.Where(r => r.end_date >= DateOnly.FromDateTime(DateTime.Now)).ToListAsync();
            return new OkObjectResult(new
            {
                data = new { rents = rents },
                status = true
            });
        }

        public async Task<IActionResult> GetRentsByBookIdAsync(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблема с Id"
                });
            }

            var books = await _context.Books.ToListAsync();

            var our_book = books.FirstOrDefault(b => b.book_id == id);

            if (our_book == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такой книги с таким ID"
                });
            }

            var rents = await _context.Rent_Books.Where(r => r.book_id == id).ToListAsync();

            if(rents.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Такая книга не участвует в арендах"
                });
            }

            return new OkObjectResult(new
            {
                data = new { rents = rents },
                status = true
            });
        }

        public async Task<IActionResult> GetRentsByReaderId(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблема с Id"
                });
            }

            var existingReader = await _context.Readers.FirstOrDefaultAsync(r => r.reader_id == id);

            if (existingReader == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого читателя"
                });
            }

            var rents = await _context.Rent_Books.Where(r => r.reader_id == id).ToListAsync();

            if(rents.Count == 0)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет аренд для такого читателя"
                });
            }

            return new OkObjectResult(new
            {
                data = new { rents = rents },
                status = true
            });
        }

        public async Task<IActionResult> PostRentReturnAsync(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var existing_rent = await _context.Rent_Books.FirstOrDefaultAsync(r => r.rent_book_id == id);

            if (existing_rent == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такой аренды"
                });
            }

            if (existing_rent.status_id != 1)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Неподходящий статус для возврата"
                });
            }

            existing_rent.status_id = 2;

            var book = await _context.Books.FirstOrDefaultAsync(b => b.book_id == existing_rent.book_id);
            if (book != null)
            {
                book.count += 1;
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });

        }
    }
}
