using Inwentax.Data;
using Inwentax.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inwentax.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserViewModel.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _context.UserViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Email,Password,Role")] CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser { UserName = createUserViewModel.Email, Email = createUserViewModel.Email, EmailConfirmed = true };
                var identityResult = await _userManager.CreateAsync(identityUser, createUserViewModel.Password);

                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, createUserViewModel.Role);

                    var userViewModel = new UserViewModel
                    {
                        Id = identityUser.Id,
                        Name = createUserViewModel.Name,
                        Surname = createUserViewModel.Surname,
                        Email = createUserViewModel.Email,
                        Role = createUserViewModel.Role
                    };

                    _context.Add(userViewModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(createUserViewModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _context.UserViewModel.FindAsync(id);
            if (userViewModel == null)
            {
                return NotFound();
            }
            return View(userViewModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Surname,Email,Role")] UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userViewModel);
                    await _context.SaveChangesAsync();
                    var identityUser = await _userManager.FindByIdAsync(userViewModel.Id);

                    if (identityUser != null)
                    {
                        identityUser.Email = userViewModel.Email;
                        identityUser.UserName = userViewModel.Email;

                        await _userManager.UpdateAsync(identityUser);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserViewModelExists(userViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _context.UserViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userViewModel = await _context.UserViewModel.FindAsync(id);
            if (userViewModel != null)
            {
                var identityUser = await _userManager.FindByIdAsync(id);
                if (identityUser != null)
                {
                    await _userManager.DeleteAsync(identityUser);
                }

                _context.UserViewModel.Remove(userViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserViewModelExists(string id)
        {
            return _context.UserViewModel.Any(e => e.Id == id);
        }
    }
}
