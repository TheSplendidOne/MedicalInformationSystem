using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Mis.Data;
using Mis.Models;

namespace Mis.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[HttpPost]
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
