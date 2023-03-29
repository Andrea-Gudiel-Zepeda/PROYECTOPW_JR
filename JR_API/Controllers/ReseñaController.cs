using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReseñaController : ControllerBase
    {
        private readonly JrDbContext _context;

        public ReseñaController()
        {
            _context = new JrDbContext();
        }

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<JR_DB.Reseña>> GetList()
        {
            IEnumerable<JR_DB.Reseña> reseña = await _context.Reseñas
                                                      .Select(rs =>
                                                        new JR_DB.Reseña
                                                        {
                                                            IdReseña = rs.IdReseña,
                                                            NameBook = rs.NameBook,
                                                            AuthorBook = rs.AuthorBook,
                                                            GeneroBook = rs.GeneroBook,
                                                            PagesBook = rs.PagesBook,
                                                            PuntuacionBook = rs.PuntuacionBook,
                                                            PuntuacionTrama = rs.PuntuacionTrama,
                                                            PuntuacionPersonajes = rs.PuntuacionPersonajes,
                                                            DescriptionReseña = rs.DescriptionReseña,
                                                            FavoritePhrase = rs.FavoritePhrase,
                                                            IdUser = rs.IdUser

                                                        }).ToListAsync();

            return reseña;
        }

        //CREAR RESEÑA
        [Route("Set")]
        [HttpPost]
        public async Task<JR_DB.GeneralResult> Set(JR_DB.Reseña reseña)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.Reseña newReseña = new Models.Reseña
                {
                    IdReseña = reseña.IdReseña,
                    NameBook = reseña.NameBook,
                    AuthorBook = reseña.AuthorBook,
                    GeneroBook = reseña.GeneroBook,
                    PagesBook = reseña.PagesBook,
                    PuntuacionBook = reseña.PuntuacionBook,
                    PuntuacionTrama = reseña.PuntuacionTrama,
                    PuntuacionPersonajes = reseña.PuntuacionPersonajes,
                    DescriptionReseña = reseña.DescriptionReseña,
                    FavoritePhrase = reseña.FavoritePhrase,
                    IdUser = reseña.IdUser
                };

                _context.Reseñas.Add(newReseña);
                await _context.SaveChangesAsync();
                return generalResult;
            }
            catch (Exception)
            {
                return generalResult;
                throw;
            }
        }

        //OBTNER POR ID
        [Route("GetByID/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetByID(int id)
        {
            if (id == null || _context.Reseñas == null)
            {
                return NotFound();
            }

            var reseña = await _context.Reseñas.FindAsync(id);

            if (reseña == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(reseña);
            }
        }

        //EDITAR RESEÑA
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, JR_DB.Reseña reseña)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.Reseña newReseña = new Models.Reseña
                {
                    IdReseña = reseña.IdReseña,
                    NameBook = reseña.NameBook,
                    AuthorBook = reseña.AuthorBook,
                    GeneroBook = reseña.GeneroBook,
                    PagesBook = reseña.PagesBook,
                    PuntuacionBook = reseña.PuntuacionBook,
                    PuntuacionTrama = reseña.PuntuacionTrama,
                    PuntuacionPersonajes = reseña.PuntuacionPersonajes,
                    DescriptionReseña = reseña.DescriptionReseña,
                    FavoritePhrase = reseña.FavoritePhrase,
                    IdUser = reseña.IdUser
                };

                _context.Update(newReseña);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR RESEÑA
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            if (_context.Reseñas == null)
            {
                return NotFound("Entity set 'JrDbContext.Reseñas'  is null.");
            }

            var reseña = await _context.Reseñas.FindAsync(id);
            if (reseña != null)
            {
                _context.Reseñas.Remove(reseña);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
}
