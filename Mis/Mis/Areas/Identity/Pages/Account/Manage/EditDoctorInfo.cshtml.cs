using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mis.Data;
using Mis.Models;

namespace Mis.Areas.Identity.Pages.Account.Manage
{
    public class EditDoctorInfoModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public EditDoctorInfoModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Position")]
            public string Position { get; set; }

            [Display(Name = "Hospital")]
            public string Hospital { get; set; }
        }

        private void Load(ApplicationUser user)
        {
            var doctorInfo = _context.DoctorInfo.Find(user.Id);

            Input = new InputModel()
            {
                Position = doctorInfo.Position,
                Hospital = _context.Hospital.Find(doctorInfo.HospitalId).Name
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Load(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var doctorInfo = _context.DoctorInfo.Find(user.Id);
            doctorInfo.Position = Input.Position;
            _context.DoctorInfo.Update(doctorInfo);
            _context.SaveChanges();
            return RedirectToPage();
        }
    }
}
