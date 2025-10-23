using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice_7_api.DatabaseContext;
using practice_7_api.Interfaces;
using practice_7_api.Models;
using practice_7_api.Requests;
using System.Reflection.PortableExecutable;

namespace practice_7_api.Service
{
    public class BookService : IBookService
    {
        private readonly ContextDb _context;
        public BookService(ContextDb context)
        {
            _context = context;
        }
        public async Task<IActionResult> CreateNewBookAsync(CreateNewBook newBook)
        {
            if (string.IsNullOrEmpty(newBook.book_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Название книги не может быть пустым"
                });
            }

            if (newBook.count < 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Количество книг не может быть отрицательным"
                });
            }

            if (newBook.author_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У книги должен быть автор"
                });
            }

            if (newBook.count == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У книги должно быть количество экземляров"
                });
            }

            if (newBook.genre_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У книги должен быть жанр"
                });
            }

            var existingBook = await _context.Books.FirstOrDefaultAsync(b => 
                b.book_name.ToLower() == newBook.book_name.ToLower() && 
                b.author_id == newBook.author_id);

            if (existingBook != null)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Такая книга уже существует"
                });
            }


            var book = new Book()
            {
                book_name = newBook.book_name,
                public_date = newBook.public_date,
                description = newBook.description,
                count = newBook.count,
                author_id = newBook.author_id,
                genre_id = newBook.genre_id
            };


            await _context.AddAsync(book);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status=true
            });
        }

        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
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

            _context.Books.Remove(our_book);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Всё хорошо 😎"
            });

        }

        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            return new OkObjectResult(new
            {
                data = new { books = books },
                status = true
            });
        }

        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var books = await _context.Books.FirstOrDefaultAsync(b => b.book_id == id);

            if (books == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такой книги с таким ID"
                });
            }

            return new OkObjectResult(new
            {
                data = books,
                status = true
            });

        }   

        public async Task<IActionResult> GetBookByTitleAndAuthorAsync(string title, int author_id)
        {
            if (string.IsNullOrEmpty(title) && author_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Необходимые поля пустые"
                });
            }

            var query = _context.Books.AsQueryable();

            if (author_id > 0)
            {
                var authorExists = await _context.Athores.AnyAsync(a => a.author_id == author_id);
                if (!authorExists)
                {
                    return new NotFoundObjectResult(new
                    {
                        status = false,
                        message = "Нет такого автора"
                    });
                }
                
                query = query.Where(b => b.author_id == author_id);
            }

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.book_name.ToLower().Contains(title.ToLower()));
            }

            var books = await query.ToListAsync();

            if (!books.Any())
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет книги с таким писателем и названием"
                });
            }

            return new OkObjectResult(new
            {
                data = books,
                status = true
            });
        }

        public async Task<IActionResult> GetBookCountById(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var books = await _context.Books.ToListAsync();

            var our_book = books.FirstOrDefault(b => b.book_id == id);

            if (our_book == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такой книги"
                });
            }

            return new OkObjectResult(new
            {
                data = our_book.count,
                status = true
            });
        }

        public async Task<IActionResult> GetBooksByGenreAsync(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var books = await _context.Books.ToListAsync();

            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.genre_id == id);

            if (genre == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого жанра с таким id"
                });
            }

            var our_book = books.Where(b => b.genre_id == id);

            return new OkObjectResult(new
            {
                data = our_book,
                status = true
            });
        }

        public async Task<IActionResult> PutBookAsync(int id, UpdateBook updatedBook)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.book_id == id);

                if (existingBook == null)
                {
                    return new NotFoundObjectResult(new
                    {
                        status = false,
                        message = "Нет такой книги"
                    });
                }

            if (string.IsNullOrEmpty(updatedBook.book_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Название книги не может быть пустым"
                });
            }

            if (updatedBook.count < 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Количество книг не может быть отрицательным"
                });
            }

            if (updatedBook.author_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У книги должен быть автор"
                });
            }

            if (updatedBook.count == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У книги должно быть количество экземляров"
                });
            }

            if (updatedBook.genre_id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "У книги должен быть жанр"
                });
            }


            existingBook.book_name = updatedBook.book_name;
            existingBook.public_date = updatedBook.public_date;
            existingBook.description = updatedBook.description;
            existingBook.count = updatedBook.count;
            existingBook.author_id = updatedBook.author_id;
            existingBook.genre_id = updatedBook.genre_id;

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Всё хорошо 😎"
            });

        }
    }
}
