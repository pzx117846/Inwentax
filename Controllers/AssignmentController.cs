using Inwentax.Data;
using Inwentax.Migrations;
using Inwentax.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Inwentax.Controllers
{
    [Authorize]
    public class AssignmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Assignment
        public async Task<IActionResult> Index()
        {
            ViewData["UsersList"] = _context.UserViewModel.ToList();
            var assignmentsUser = _context.Assignments
                .Include(a => a.Laptop)
                .Include(a => a.Phone);
            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var myAssignments = assignmentsUser.Where(a => a.UserId == userId);
                return View(await myAssignments.ToListAsync());
            }
            return View(await assignmentsUser.ToListAsync());
        }

        // GET: Assignment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["UsersList"] = _context.UserViewModel.ToList();

            var assignment = await _context.Assignments
                .Include(a => a.Laptop)
                .Include(a => a.Phone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignment/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var availableLaptops = _context.Laptops
                    .Where(l => l.Status == "Bufor")
                    .Select(l => new
                    {
                        Id = l.Id,
                        DisplayFullLaptop = (l.Brand ?? "") + " " + (l.Model ?? "") + " - (" + (l.serial_number ?? "") + ")"
                    })
                    .ToList();

            var availablePhones = _context.Phone
                    .Where(p => p.Status == "Bufor")
                    .Select(p => new
                    {
                        Id = p.Id,
                        DisplayFullPhone = (p.Brand ?? "") + " " + (p.Model ?? "") + " - (" + (p.Imei ?? "") + ")"
                    })
                    .ToList();

            var users = _context.UserViewModel
                   .Select(u => new
                   {
                       Id = u.Id,
                       DisplayFullName = (u.Name ?? "") + " " + (u.Surname ?? "")
                   })
                   .ToList();

            ViewData["UserId"] = new SelectList(users, "Id", "DisplayFullName");
            ViewData["LaptopId"] = new SelectList(availableLaptops, "Id", "DisplayFullLaptop");
            ViewData["PhoneId"] = new SelectList(availablePhones, "Id", "DisplayFullPhone");
            return View();
        }

        // POST: Assignment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,UserId,LaptopId,PhoneId,date_from,date_to,Status")] Assignment assignment)
        {
            assignment.date_from = DateTime.Today;
            ModelState.Remove("date_from");
            ModelState.Remove("date_to");
            ModelState.Remove("UserId");
            ModelState.Remove("Laptop");
            ModelState.Remove("Phone");

            if (ModelState.IsValid)
            {
                var laptop = await _context.Laptops.FindAsync(assignment.LaptopId);
                if (laptop != null)
                {
                    laptop.Status = "Wydany";
                    _context.Update(laptop);
                }

                var phone = await _context.Phone.FindAsync(assignment.PhoneId);
                if (phone != null)
                {
                    phone.Status = "Wydany";
                    _context.Update(phone);
                }

                assignment.Status = "Wydane";
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var availableLaptops = _context.Laptops.Where(l => l.Status == "Bufor").ToList();
            var availablePhones = _context.Phone.Where(p => p.Status == "Bufor").ToList();
            var users = _context.UserViewModel.ToList();

            ViewData["UserId"] = new SelectList(users, "Id", "Name", assignment.UserId);
            ViewData["LaptopId"] = new SelectList(_context.Laptops, "Id", "Brand", assignment.LaptopId);
            ViewData["PhoneId"] = new SelectList(_context.Phone, "Id", "Brand", assignment.PhoneId);
            return View(assignment);
        }

        // GET: Assignment/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            var users = _context.UserViewModel.Select(u => new
            {
                Id = u.Id,
                DisplayFullName = u.Name + " " + u.Surname
            }).ToList();

            var availableLaptops = _context.Laptops
            .Where(l => l.Status == "Bufor" || l.Id == assignment.LaptopId)
            .Select(l => new
            {
                Id = l.Id,
                DisplayFullLaptop = l.Brand + " " + l.Model + " - (" + l.serial_number + ")"
            })
            .ToList();

            var availablePhones = _context.Phone
            .Where(p => p.Status == "Bufor" || p.Id == assignment.PhoneId)
            .Select(p => new
            {
                Id = p.Id,
                DisplayFullPhone = p.Brand + " " + p.Model + " - (" + p.Imei + ")"
            })
            .ToList();

            ViewData["UserId"] = new SelectList(users, "Id", "DisplayFullName", assignment.UserId);
            ViewData["LaptopId"] = new SelectList(availableLaptops, "Id", "DisplayFullLaptop", assignment.LaptopId);
            ViewData["PhoneId"] = new SelectList(availablePhones, "Id", "DisplayFullPhone", assignment.PhoneId);
            return View(assignment);
        }

        // POST: Assignment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,LaptopId,PhoneId,date_from,date_to,Status")] Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Laptop");
            ModelState.Remove("Phone");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Id))
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
            ViewData["LaptopId"] = new SelectList(_context.Laptops, "Id", "Brand", assignment.LaptopId);
            ViewData["PhoneId"] = new SelectList(_context.Phone, "Id", "Brand", assignment.PhoneId);
            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReturnEquipment(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }

            assignment.Status = "Zwrócone";
            assignment.date_to = DateTime.Today;
            _context.Update(assignment);

            var laptop = await _context.Laptops.FindAsync(assignment.LaptopId);
            if (laptop != null)
            {
                laptop.Status = "Bufor";
                _context.Update(laptop);
            }

            var phone = await _context.Phone.FindAsync(assignment.PhoneId);
            if (phone != null)
            {
                phone.Status = "Bufor";
                _context.Update(phone);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Assignment/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["UsersList"] = _context.UserViewModel.ToList();

            var assignment = await _context.Assignments
                .Include(a => a.Laptop)
                .Include(a => a.Phone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment != null)
            {
                var laptop = await _context.Laptops.FindAsync(assignment.LaptopId);
                if (laptop != null)
                {
                    laptop.Status = "Bufor";
                    _context.Update(laptop);
                }

                var phone = await _context.Phone.FindAsync(assignment.PhoneId);
                if (phone != null)
                {
                    phone.Status = "Bufor";
                    _context.Update(phone);
                }

                _context.Assignments.Remove(assignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.Id == id);
        }
    }
}
