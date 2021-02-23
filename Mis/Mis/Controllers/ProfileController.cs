using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mis.Data;
using Mis.Models;

namespace Mis.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index(String userId)
        {
            ApplicationUser user = String.IsNullOrEmpty(userId)
                ? await _userManager.GetUserAsync(User)
                : await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var comments = _context
                .Comment
                .Where(comment => comment.OwnerId == user.Id)
                .Include(comment => comment.Author)
                .Include(comment => comment.Owner)
                .ToList();

            var model = new ProfileViewModel()
            {
                Id = user.Id,
                About = user.About,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                PhoneNumber = user.PhoneNumber,
                Birthday = user.Birthday,
                Comments = comments,
                ProfilePicture = user.ProfilePicture
            };

            return View(model);
        }

        [Authorize(Roles = "Basic")]
        public IActionResult AddComment(String content, String authorId, String ownerId)
        {
            var comment = new Comment()
            {
                Id = Guid.NewGuid().ToString(),
                Content = content,
                AuthorId = authorId,
                OwnerId = ownerId,
                CreationDate = DateTime.UtcNow
            };
            _context.Comment.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index", "Profile", new { userId = ownerId });
        }
    }
}
