using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly JrDbContext _context;

        public BookController()
        {
            _context = new JrDbContext();
        }

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<JR_DB.Book>> GetList()
        {
            IEnumerable<JR_DB.Book> book = await _context.Books
                                                      .Select(b =>
                                                        new JR_DB.Book
                                                        {
                                                            IdBook = b.IdBook,
                                                            NameBook = b.NameBook,
                                                            AuthorBook = b.AuthorBook,
                                                            BookPublish = b.BookPublish,
                                                            DateBook = b.DateBook,
                                                            Calificacion = b.Calificacion,
                                                            PictureBook = b.PictureBook,
                                                            IdCategorie = b.IdCategorie,
                                                            IdUser = b.IdUser,

                                                        }).ToListAsync();

            return book;
        }

        //POR ID PARA TRAER UNA SOLA CATEGORIA
        [Route("GetList/{idCategorie}")]
        [HttpGet]
        public async Task<ActionResult> GetListRegion(int idCategorie)
        {
            IEnumerable<JR_DB.Book> book = await (_context.Books
                                                     .Where(b => b.IdCategorie == idCategorie)
                                                     .Select(b =>
                                                      new JR_DB.Book
                                                      {
                                                          IdBook = b.IdBook,
                                                          NameBook = b.NameBook,
                                                          AuthorBook = b.AuthorBook,
                                                          BookPublish = b.BookPublish,
                                                          DateBook = b.DateBook,
                                                          Calificacion = b.Calificacion,
                                                          PictureBook = b.PictureBook,
                                                          IdCategorie = b.IdCategorie,
                                                          IdUser = b.IdUser

                                                      }).ToListAsync());
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        //CREAR BOOK
        [Route("Set")]
        [HttpPost]
        public async Task<JR_DB.GeneralResult> Set(JR_DB.Book book)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.Book newBook = new Models.Book
                {
                    NameBook = book.NameBook,
                    AuthorBook = book.AuthorBook,
                    BookPublish = book.BookPublish,
                    DateBook = book.DateBook,
                    Calificacion = book.Calificacion,
                    PictureBook = book.PictureBook,
                    IdCategorie = book.IdCategorie,
                    IdUser = book.IdUser
                };

                _context.Books.Add(newBook);
                await _context.SaveChangesAsync();
                return generalResult;
            }
            catch (Exception)
            {
                return generalResult;
                throw;
            }
        }

        //OBTNER POR ID BOOK
        [Route("GetByID/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetByID(int id)
        {
            if (id == null || _context.CategorieBooks == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(book);
            }
        }

        //EDITAR BOOK
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, JR_DB.Book book)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.Book newBook = new Models.Book
                {
                    IdBook = book.IdBook,
                    NameBook = book.NameBook,
                    AuthorBook = book.AuthorBook,
                    BookPublish = book.BookPublish,
                    DateBook = book.DateBook,
                    Calificacion = book.Calificacion,
                    PictureBook = book.PictureBook,
                    IdCategorie = book.IdCategorie,
                    IdUser = book.IdUser
                };

                _context.Update(newBook);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR BOOK
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            if (_context.Books == null)
            {
                return NotFound("Entity set 'JrDbContext.Books'  is null.");
            }

            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
}
