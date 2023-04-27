using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategorieController : ControllerBase
    {
        private readonly JrDbContext _context;

        public CategorieController()
        {
            _context = new JrDbContext();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<JR_DB.CategorieBook>> GetList()
        {
            IEnumerable<JR_DB.CategorieBook> categoriebook = await _context.CategorieBooks
                                                      .Select(cb =>
                                                        new JR_DB.CategorieBook
                                                        {
                                                            IdCategorie = cb.IdCategorie,
                                                            NameCategorie = cb.NameCategorie,

                                                        }).ToListAsync();

            return categoriebook;
        }

        //CREAR CATEGORIA
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Set")]
        [HttpPost]
        public async Task<JR_DB.GeneralResult> Set(JR_DB.CategorieBook categoriebook)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.CategorieBook newCategorieBook = new Models.CategorieBook
                {
                    NameCategorie = categoriebook.NameCategorie,
                };

                _context.CategorieBooks.Add(newCategorieBook);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetByID/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetByID(int id)
        {
            if (id == null || _context.CategorieBooks == null)
            {
                return NotFound();
            }

            var categoriebook = await _context.CategorieBooks.FindAsync(id);

            if (categoriebook == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(categoriebook);
            }
        }

        //EDITAR CATEGORIA
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, JR_DB.CategorieBook categoriebook)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.CategorieBook newCategorieBook = new Models.CategorieBook
                {
                    IdCategorie = categoriebook.IdCategorie,
                    NameCategorie = categoriebook.NameCategorie,
                };

                _context.Update(newCategorieBook);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR CATEGORIA
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            if (_context.CategorieBooks == null)
            {
                return NotFound("Entity set 'JrDbContext.CategorieBooks'  is null.");
            }

            var goal = await _context.CategorieBooks.FindAsync(id);
            if (goal != null)
            {
                _context.CategorieBooks.Remove(goal);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
}
