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
    public class RetoController : ControllerBase
    {
        private readonly JrDbContext _context;

        public RetoController()
        {
            _context = new JrDbContext();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<JR_DB.Reto>> GetList()
        {
            IEnumerable<JR_DB.Reto> reto = await _context.Retos
                                                      .Select(r =>
                                                        new JR_DB.Reto
                                                        {
                                                            IdReto = r.IdReto,
                                                            NombreReto = r.NombreReto,
                                                            Status = r.Status,
                                                            IdUser = r.IdUser,

                                                        }).ToListAsync();

            return reto;
        }

        //CREAR RETO
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Set")]
        [HttpPost]
        public async Task<JR_DB.GeneralResult> Set(JR_DB.Reto reto)
        { 
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.Reto newReto = new Models.Reto
                {
                    NombreReto = reto.NombreReto,
                    Status = reto.Status,
                    IdUser = reto.IdUser,
                };

                _context.Retos.Add(newReto);
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
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var reto = await _context.Retos.FindAsync(id);

            if (reto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(reto);
            }
        }

        //EDITAR RETO
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, JR_DB.Reto reto)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.Reto newReto = new Models.Reto
                {
                    IdReto = reto.IdReto,
                    NombreReto = reto.NombreReto,
                    Status = reto.Status,
                    IdUser = reto.IdUser,
                };

                _context.Update(newReto);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR RETO
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            if (_context.Retos == null)
            {
                return NotFound("Entity set 'JrDbContext.Retos'  is null.");
            }

            var reto = await _context.Retos.FindAsync(id);
            if (reto != null)
            {
                _context.Retos.Remove(reto);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
}
