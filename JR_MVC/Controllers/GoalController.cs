using JR_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace JR_MVC.Controllers
{
    public class GoalController : Controller
    {
        private readonly ILogger<GoalController> _logger;

        public GoalController(ILogger<GoalController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowGoal()
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            IEnumerable<JR_DB.Goal> goals = await Functions.APIServiceGoal.GoalGetList(token.token);
            IEnumerable<JR_DB.Book> books = await Functions.APIServiceBook.BookGetList(token.token);
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            int contadorlibros = 0;
            JR_DB.Goal goalUser = new JR_DB.Goal();
            goalUser.GoalBook = 0;
            goalUser.Progress = 0;
            bool encontrado = false;
            foreach (var bk in books)
            {
                if (bk.IdCategorie == 0 && bk.IdUser == idUsuario)
                {
                    contadorlibros += 1;
                }
            }

            foreach (var gl in goals)
            {
                if (gl.IdUser == idUsuario)
                {
                    goalUser = gl;
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                goalUser.Progress = (contadorlibros * 100) / goalUser.GoalBook;
                await Functions.APIServiceGoal.GoalEdit(goalUser, goalUser.IdGoal, token.token);
                return View(goalUser);
            }
            else
            {
                ViewBag.NoGoal = "No ha ingresado ninguna meta con este usuario";
                return View(goalUser);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateGoal()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGoal([Bind("IdGoal,GoalBook, Progress, idUser")] JR_DB.Goal goal)
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            IEnumerable<JR_DB.Goal> goals = await Functions.APIServiceGoal.GoalGetList(token.token);
            bool encontrado = false;
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);
            foreach (var gl in goals)
            {
                if (gl.IdUser == idUsuario)
                {
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                ViewBag.GoalCreate = "Ya existe una meta creada con este usuario";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    goal.IdUser = idUsuario;
                    await Functions.APIServiceGoal.GoalSet(goal, token.token);
                    return RedirectToAction("ShowGoal", "Goal");
                }
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditGoal(int id)
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            JR_DB.Goal goal = await Functions.APIServiceGoal.GetGoalByID(id, token.token);
            
            return View(goal);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditGoal(int id, [Bind("IdGoal,GoalBook, Progress, idUser")] JR_DB.Goal goal)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            if (id != goal.IdGoal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                goal.IdGoal = id;
                goal.IdUser = idUsuario;
                await Functions.APIServiceGoal.GoalEdit(goal, id,token.token);
              
                return RedirectToAction(nameof(ShowGoal));
            }
            return View(goal);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteGoal(int id)
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            JR_DB.Goal goal = await Functions.APIServiceGoal.GetGoalByID(id, token.token);

            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        
        [HttpPost, ActionName("DeleteGoal")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            JR_DB.Tokens token = await Functions.APIServiceUser.Login(
            new JR_DB.Tokens
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            if (id != 0)
            {
                await Functions.APIServiceGoal.GoalDelete(id, token.token);
            }

            
            return RedirectToAction(nameof(ShowGoal));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}