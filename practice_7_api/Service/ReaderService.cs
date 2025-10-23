using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice_7_api.DatabaseContext;
using practice_7_api.Interfaces;
using practice_7_api.Models;
using practice_7_api.Requests;
using System.Reflection.PortableExecutable;

namespace practice_7_api.Service
{
    public class ReaderService : IReaderService
    {
        private readonly ContextDb _context;

        public ReaderService(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateNewReaderAsync(CreateNewReader newReader)
        {

            if (string.IsNullOrEmpty(newReader.reader_last_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет фамилии"
                });
            }

            if (string.IsNullOrEmpty(newReader.reader_first_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет имени"
                });
            }


            if (newReader.reader_date_birth == new DateOnly(00001, 01, 01))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет даты рождения"
                });
            }

            if (string.IsNullOrEmpty(newReader.email))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет почты"
                });
            }

            if (string.IsNullOrEmpty(newReader.phone))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет номера телефона"
                });
            }

            var reader = new Reader()
            {
                reader_last_name = newReader.reader_last_name,
                reader_first_name = newReader.reader_first_name,
                reader_date_birth = newReader.reader_date_birth,
                phone = newReader.phone,
                email = newReader.email
            };

            await _context.AddAsync(reader);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });

        }

        public async Task<IActionResult> DeleteReaderAsync(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var readers = await _context.Readers.ToListAsync();
            var our_reader = readers.FirstOrDefault(r => r.reader_id == id);

            if (our_reader == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого читателя"
                });
            }

            _context.Readers.Remove(our_reader);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });

        }

        public async Task<IActionResult> GetAllReadersAsync()
        {
            var readers = await _context.Readers.ToListAsync();
            return new OkObjectResult(new
            {
                data = new { readers = readers },
                status = true
            });
        }

        public async Task<IActionResult> GetReaderBooksByIdASync(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var readers = await _context.Readers.ToListAsync();
            var our_reader = readers.FirstOrDefault(r => r.reader_id == id);

            if (our_reader == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого читателя"
                });
            }

            var rents = await _context.Rent_Books.Where(r => r.reader_id == id).Include(rb => rb.Book).ToListAsync();
            var books = rents.Select(r => r.Book).ToList();
            
            return new OkObjectResult(new
            {
                data = new { books = books },
                status = true
            });
        }

        public async Task<IActionResult> GetReaderByIdAsync(int id)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
                });
            }

            var readers = await _context.Readers.ToListAsync();
            var our_reader = readers.FirstOrDefault(r => r.reader_id == id);

            if (our_reader == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Нет такого читателя"
                });
            }

            return new OkObjectResult(new
            {
                data = our_reader,
                status = true
            });
        }

        public async Task<IActionResult> PutReaderAsync(int id, UpdateReader updatedReader)
        {
            if (id == 0)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Проблемы с Id"
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

            if (string.IsNullOrEmpty(updatedReader.reader_last_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет фамилии"
                });
            }

            if (string.IsNullOrEmpty(updatedReader.reader_first_name))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет имени"
                });
            }

            if (updatedReader.reader_date_birth == new DateOnly(00001, 01, 01))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет даты рождения"
                });
            }

            if (string.IsNullOrEmpty(updatedReader.email))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет почты"
                });
            }

            if (string.IsNullOrEmpty(updatedReader.phone))
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Нет номера телефона"
                });
            }

            existingReader.reader_last_name = updatedReader.reader_last_name;
            existingReader.reader_first_name = updatedReader.reader_first_name;
            existingReader.reader_date_birth = updatedReader.reader_date_birth;
            existingReader.phone = updatedReader.phone;
            existingReader.email = updatedReader.email;

            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });

        }
    }
}
