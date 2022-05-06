using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheSupperLog.Data;
using TheSupperLog.Data.Entities;
using TheSupperLog.Models.User;
using TheSupperLog.Services.User;

namespace TheSupperLog.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();

            return View(users);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return null;
            }
            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserCreate model)
        {
            if (model == null) return BadRequest();

            bool wasSuccess = await _userService.CreateUserAsync(model);

            if (wasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            else return UnprocessableEntity();
        }

        //GET EDIT User/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var userQuery = await _userService.GetUserByIdAsync(id);

            var user = new UserEdit();

            user.Id = userQuery.Id;
            user.Username = userQuery.Username;
            user.Email = userQuery.Email;
            user.DateAdded = userQuery.DateAdded;

            return View(user);
        }


        // POST: User/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(UserEdit model)
        {
            bool wasSuccess = await _userService.UpdateUserAsync(model);

            if (wasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }


        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var userQuery = await _userService.GetUserByIdAsync(id);

            var user = new UserDetail();

            user.Id = userQuery.Id;
            user.Username = userQuery.Username;
            user.Email = userQuery.Email;

            return View(user);
        }



        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            bool wasSuccess = await _userService.DeleteUserAsync(id);

            if (wasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            else return BadRequest();
        }
    }
}
