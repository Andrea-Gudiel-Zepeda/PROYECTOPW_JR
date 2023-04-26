using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;


namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly JrDbContext _context;

        public UserController()
        {
            _context = new JrDbContext();
        }

        //OBTENER INFORMACION DE LOS USUARIOS
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<JR_DB.User>> GetList()
        {
            IEnumerable<JR_DB.User> usuario = await _context.Users
                                                      .Select(u =>
                                                        new JR_DB.User
                                                        {
                                                            IdUser = u.IdUser,
                                                            FullName= u.FullName,
                                                            LastName = u.LastName,
                                                            Email= u.Email,
                                                            NumberPhone= u.NumberPhone,
                                                            Password = u.Password

                                                        }).OrderBy(s => s.Email).ToListAsync();

            return usuario;
        }

        //CREAR USUARIO
        [Route("Set")]
        [HttpPost]
        public async Task<JR_DB.GeneralResult> Set(JR_DB.User user)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.User newUser = new Models.User
                {
                    FullName = user.FullName,
                    LastName = user.LastName,
                    Email = user.Email,
                    NumberPhone = user.NumberPhone,
                    Password = user.Password
                };

                _context.Users.Add(newUser);
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
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        //EDITAR USUARIO 
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, JR_DB.User user)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.User newUser = new Models.User
                {
                    IdUser = user.IdUser,
                    FullName = user.FullName,
                    LastName = user.LastName,
                    Email = user.Email,
                    NumberPhone = user.NumberPhone,
                    Password = user.Password,
                };
                _context.Update(newUser);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR USUARIO
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            if (_context.Users == null)
            {
                return NotFound("Entity set 'JrDbContext.Users'  is null.");
            }

            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }

    }
}
