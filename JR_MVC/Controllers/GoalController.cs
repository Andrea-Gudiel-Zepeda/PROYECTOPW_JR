using JR_MVC.Models;
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

        public static int IdUser = 0;

        [HttpGet]
        public async Task<IActionResult> ValidacionCredencialesG()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidacionCredencialesG(string email, string password)
        {
            IEnumerable<JR_DB.User> usuario = await Functions.APIServiceUser.UserGetList();
            bool encontrado = false;
            foreach (var us in usuario)
            {
                if (us.Email == email)
                {
                    if (us.Password == password)
                    {
                        IdUser = us.IdUser;
                        encontrado = true;
                        break;
                    }
                    else
                    {
                        ViewBag.error = "La contraseña es incorrecta, ingrese de nuevo";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "No se encontro el usuario, intente de nuevo para poder continuar";
                    return View();
                }
            }

            if (encontrado)
            {
                return RedirectToAction("ShowGoal", "Goal");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ShowGoal()
        {
            IEnumerable<JR_DB.Goal> goals = await Functions.APIServiceGoal.GoalGetList();
            IEnumerable<JR_DB.Book> books = await Functions.APIServiceBook.BookGetList();
            int contadorlibros = 0;
            JR_DB.Goal goalUser = new JR_DB.Goal();
            bool encontrado = false;
            foreach (var bk in books)
            {
                if (bk.IdCategorie == 0 && bk.IdUser == IdUser)
                {
                    contadorlibros += 1;
                }
            }

            foreach (var gl in goals)
            {
                if (gl.IdUser == IdUser)
                {
                    goalUser = gl;
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                goalUser.Progress = (contadorlibros * 100) / goalUser.GoalBook;
                await Functions.APIServiceGoal.GoalEdit(goalUser, goalUser.IdGoal);
                return View(goalUser);
            }
            else
            {
                ViewBag.NoGoal = "No ha ingresado ninguna meta con este usuario";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateGoal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal([Bind("IdGoal,GoalBook, Progress, idUser")] JR_DB.Goal goal)
        {
            IEnumerable<JR_DB.Goal> goals = await Functions.APIServiceGoal.GoalGetList();
            bool encontrado = false;
            foreach (var gl in goals)
            {
                if (gl.IdUser == IdUser)
                {
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                ViewBag.GoalCreate = "Ya existe una meta creada con este usuario";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    goal.IdUser = IdUser;
                    await Functions.APIServiceGoal.GoalSet(goal);
                    //falta el mensaje y direccionar 
                    ViewBag.GoalCreate = "Meta Ingresada correctamente";
                    return RedirectToAction("CreateGoal", "Goal");
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditGoal(int id)
        {
            JR_DB.Goal goal = await Functions.APIServiceGoal.GetGoalByID(id);
            
            return View(goal);
        }

        [HttpPost]
        public async Task<IActionResult> EditGoal(int id, [Bind("IdGoal,GoalBook, Progress, idUser")] JR_DB.Goal goal)
        {
            if (id != goal.IdGoal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                goal.IdUser = IdUser;
                await Functions.APIServiceGoal.GoalEdit(goal, id);
              
                return RedirectToAction(nameof(ShowGoal));
            }
            return View(goal);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGoal(int id)
        {

            JR_DB.Goal goal = await Functions.APIServiceGoal.GetGoalByID(id);

            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        
        [HttpPost, ActionName("DeleteGoal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                await Functions.APIServiceGoal.GoalDelete(id);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowGoal));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}