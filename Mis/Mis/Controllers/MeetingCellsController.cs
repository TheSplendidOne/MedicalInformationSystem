using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Mis.Data;
using Mis.Models;

namespace Mis.Controllers
{
    public class MeetingCellsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public MeetingCellsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMeetingCells(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("Error");
            }
            return View(new AddMeetingCellsViewModel() {DoctorId = userId});
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMeetingCells(AddMeetingCellsViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.DoctorId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.DoctorId} cannot be found";
                return View("Error");
            }

            var tempTime = model.StartTime;
            while (tempTime < model.EndTime)
            {
                var newCell = new MeetingCell()
                {
                    Id = Guid.NewGuid().ToString(),
                    DoctorId = model.DoctorId,
                    MeetingDateTime = model.MeetingDate.Add(tempTime.TimeOfDay)
                };
                _context.MeetingCell.Add(newCell);
                tempTime = tempTime.AddMinutes(model.MeetingTimeSpan);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Profile", new {userId = model.DoctorId});
        }

        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> ShowMeetingCells(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("Error");
            }

            return View(new ShowMeetingCellsViewModel()
            {
                DoctorId = userId,
                Cells = _context
                    .MeetingCell
                    .Where(cell => cell.MeetingDateTime > DateTime.UtcNow)
                    .Where(cell => cell.DoctorId == userId)
                    .OrderBy(cell => cell.MeetingDateTime)
                    .ToList()
            });
        }

        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> Register(string cellId, string doctorId)
        {
            var user = await _userManager.GetUserAsync(User);
            var cell = _context.MeetingCell.Find(cellId);
            cell.PatientId = user.Id;
            _context.Update(cell);
            _context.SaveChanges();
            return RedirectToAction("ShowMeetingCells", new { userId = doctorId });
        }

        [Authorize(Roles = "Basic")]
        public IActionResult CancelMeeting(string cellId, string doctorId)
        {
            var cell = _context.MeetingCell.Find(cellId);
            cell.PatientId = null;
            _context.Update(cell);
            _context.SaveChanges();
            return RedirectToAction("ShowMeetingCells", new { userId = doctorId });
        }
    }
}
