﻿using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalificacionController : ControllerBase
    {
        private readonly JrDbContext _context;

        public CalificacionController()
        {
            _context = new JrDbContext();
        }

        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<JR_DB.Calificacion>> GetList()
        {
            IEnumerable<JR_DB.Calificacion> calificacion = await _context.Calificacions
                                                      .Select(cl =>
                                                        new JR_DB.Calificacion
                                                        {
                                                            LimiteInferior = cl.LimiteInferior,
                                                            LimiteSuperior = cl.LimiteSuperior,
                                                            IdUser = cl.IdUser

                                                        }).ToListAsync();

            return calificacion;
        }

        //CREAR CALIFICACION
        [Route("Set")]
        [HttpPost]
        public async Task<JR_DB.GeneralResult> Set(JR_DB.Calificacion calificacion)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.Calificacion newCalificacion = new Models.Calificacion
                {
                    LimiteInferior = calificacion.LimiteInferior,
                    LimiteSuperior = calificacion.LimiteSuperior,
                    IdUser = calificacion.IdUser
                };

                _context.Calificacions.Add(newCalificacion);
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
        public async Task<ActionResult> GetByID(int id, int calificacionBook)
        {
            if (id == null || _context.Calificacions == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacions.FindAsync(id);

            
            if (calificacion == null)
            {
                return NotFound();
            }
            else
            {
                if (calificacionBook >= calificacion.LimiteInferior && calificacionBook <= calificacion.LimiteSuperior)
                {
                    return Ok(calificacion);
                }

                return Ok(false);
            }
        }

        //EDITAR CALIFICACION 
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, JR_DB.Calificacion calificacion)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.Calificacion newCalificacion = new Models.Calificacion
                {
                    LimiteInferior = calificacion.LimiteInferior,
                    LimiteSuperior = calificacion.LimiteSuperior,
                    IdUser = calificacion.IdUser
                };

                _context.Update(newCalificacion);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR CALIFICACION
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            if (_context.Calificacions == null)
            {
                return NotFound("Entity set 'JrDbContext.Calificacions'  is null.");
            }

            var calificacion = await _context.Calificacions.FindAsync(id);
            if (calificacion != null)
            {
                _context.Calificacions.Remove(calificacion);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
}
