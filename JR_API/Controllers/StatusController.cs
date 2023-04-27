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
    public class StatusController : ControllerBase
    {
        private readonly JrDbContext _context;

        public StatusController()
        {
            _context = new JrDbContext();
        }

        //OBTENER INFORMACION DE LOS USUARIOS
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                    IdStatus = status.IdStatus,
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
