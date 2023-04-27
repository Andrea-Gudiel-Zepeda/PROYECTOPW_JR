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
    public class GoalController : ControllerBase
    {
        private readonly JrDbContext _context;

        public GoalController()
        {
            _context = new JrDbContext();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<JR_DB.Goal>> GetList()
        {
            IEnumerable<JR_DB.Goal> goal = await _context.Goals
                                                      .Select(g =>
                                                        new JR_DB.Goal
                                                        {
                                                            IdGoal = g.IdGoal,
                                                            GoalBook = g.GoalBook,
                                                            Progress = g.Progress,
                                                            IdUser = g.IdUser,

                                                        }).ToListAsync();

            return goal;
        }

        //CREAR GOAL
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Set")]
        [HttpPost]
        public async Task<JR_DB.GeneralResult> Set(JR_DB.Goal goal)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.Goal newGoal = new Models.Goal
                {
                    GoalBook = goal.GoalBook,
                    Progress = goal.Progress,
                    IdUser = goal.IdUser,
                };

                _context.Goals.Add(newGoal);
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
            if (id == null || _context.Goals == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals.FindAsync(id);

            if (goal == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(goal);
            }
        }

        //EDITAR GOAL
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, JR_DB.Goal goal)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.Goal newGoal = new Models.Goal
                {
                    IdGoal = goal.IdGoal,
                    GoalBook = goal.GoalBook,
                    Progress = goal.Progress,
                    IdUser = goal.IdUser,
                };

                _context.Update(newGoal);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR GOAL
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            JR_DB.GeneralResult generalResult = new JR_DB.GeneralResult
            {
                Result = false
            };

            if (_context.Goals == null)
            {
                return NotFound("Entity set 'JrDbContext.Goals'  is null.");
            }

            var goal = await _context.Goals.FindAsync(id);
            if (goal != null)
            {
                _context.Goals.Remove(goal);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
}
