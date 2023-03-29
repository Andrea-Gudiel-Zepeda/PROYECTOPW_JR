using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly JrDbContext _context;

        public StatusController()
        {
            _context = new JrDbContext();
        }

        //OBTENER INFORMACION DE LOS USUARIOS
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<JR_DB.Status>> GetList()
        {
            IEnumerable<JR_DB.Status> status = await _context.Statuses
                                                      .Select(s =>
                                                        new JR_DB.Status
                                                        {
                                                            IdStatus = s.IdStatus,
                                                            NameStatus = s.NameStatus,
                                                        }).ToListAsync();

            return status;
        }

        //CREAR STATUS
        [Route("Set")]
        [HttpPost]
        public async Task<JR_DB.GeneralResult> Set(JR_DB.Status status)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.Status newStatus = new Models.Status
                {
                    NameStatus = status.NameStatus,
                };

                _context.Statuses.Add(newStatus);
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
            if (id == null || _context.Statuses == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(status);
            }
        }

        //EDITAR STATUS
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, JR_DB.Status status)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.Status newStatus = new Models.Status
                {
                    NameStatus = status.NameStatus,
                };
                _context.Update(newStatus);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR STATUS
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            if (_context.Statuses == null)
            {
                return NotFound("Entity set 'JrDbContext.Status'  is null.");
            }

            var status = await _context.Statuses.FindAsync(id);
            if (status != null)
            {
                _context.Statuses.Remove(status);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
}
